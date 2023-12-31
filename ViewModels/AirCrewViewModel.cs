﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;


namespace FlyveLægeKBH.ViewModels;
//**************************************************************************//
/// <summary>
/// ViewModel for managing aircrew, including both pilots and cabin crew and
/// used as main souce of display for AircrewView
/// </summary>
//**************************************************************************//
class AirCrewViewModel : ViewModelBase
{
    //------------------------------Fields------------------------------------------------------------------------------------//    
    public PilotRepo pilotRepo = new PilotRepo();
    public CabinCrewRepo cabinCrewRepo = new CabinCrewRepo();
    AppointmentRepo appointmentRepo = new AppointmentRepo();


    //**************************************************************************//
    /// <summary>
    /// List Fields Explanation:
    /// 
    /// - Serve the purpose of displaying DB-collected data in a menu.
    /// 
    /// - Allow users to select items from the displayed lists.
    /// </summary>
    //**************************************************************************//
    
    private List<Appointment>? appointments;
    public List<Appointment> Appointments
    {
        get
        {
            return appointments;
        }
        set
        {
            appointments = value;
            OnPropertyChanged(nameof(Appointments));
        }
    }
       
    private List<CabinCrew>? allCabinCrews;
    public List<CabinCrew> AllCabinCrews
    {
        get
        {
            return allCabinCrews;
        }
        set
        {
            allCabinCrews = value;
            OnPropertyChanged(nameof(AllCabinCrews));
        }
    }
 
    private List<Pilot>? allPilots;
    public List<Pilot> AllPilots
    {
        get
        {
            return allPilots;
        }
        set
        {
            allPilots = value;
            OnPropertyChanged(nameof(AllPilots));
        }
    }

    //**************************************************************************//
    /// <summary>
    /// Explanation of 'Selected' Fields:
    /// 
    /// - Fields named 'Selected' are used to change/set the correct value 
    ///   for the desired property based on the selected item/object from 
    ///   populated menus.
    /// 
    /// - Facilitates parsing the correct values to ViewModel Commands 
    ///   according to the user's selection in menus/dropdowns.
    /// </summary>
    //**************************************************************************//

    private Pilot? selectedPilot;

    public Pilot SelectedPilot
    {
        get
        {
            return selectedPilot;
        }
        set
        {
            selectedPilot = value;
            OnPropertyChanged(nameof(SelectedPilot));
        }
    }

    //**************************************************************************//
    /// <summary>
    /// Explanation of 'UserInfo' and 'BookingHistory' Fields:
    /// 
    /// - These fields serve as sources for displaying information in the user interface.
    /// 
    /// - 'UserInfo' is used to display all stored information of a user/aircrew.
    ///   It is updated after the execution of specific methods, and the OnPropertyChanged
    ///   event ensures that the UI reflects the latest changes.
    /// 
    /// - 'BookingHistory' is used to display historical bookings and changes of bookings.
    ///   Similar to 'UserInfo', it is updated based on certain actions and utilizes the
    ///   OnPropertyChanged event for UI synchronization.
    /// </summary>
    //**************************************************************************//
    
    private string userInfo;

    public string UserInfo
    {
        get { return userInfo; }
        set { userInfo = value; OnPropertyChanged(nameof(UserInfo)); }
    }

    private string bookingHistory;

    public string BookingHistory
    {
        get { return bookingHistory; }
        set { bookingHistory = value; OnPropertyChanged(nameof(BookingHistory)); }
    }

    //---------------------------------------------------Constructors----------------------------------------------//
    public AirCrewViewModel()
    {
        // Initialize commands
        DeleteAirCrewUserCommand = new CommandBase(ExecuteDeleteAirCrewUserCommand);
        GetAllInfoCommand = new CommandBase(GetAllInfo);
        UpdateAirCrewUserCommand = new CommandBase(UpdateAirCrew);
        GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);

        // Load pilots and cabin crew when the view model is created
        LoadAllPilotsAndCabinCrews();
        //********************************************************************************************************//
        /*By adding the LoadPilotsAndCabinCrews method to the constructor, we ensure that when an instance of 
         * AirCrewViewModel is created, it automatically loads the pilots and cabin crew into the Users property.*/
        //********************************************************************************************************//
    }

    //----------------------------- Commands----------------------------------------------------------------------------------------//
    public ICommand UpdateAirCrewUserCommand { get; }
    public ICommand DeleteAirCrewUserCommand { get; }
    public ICommand? GetAllInfoCommand { get; set; }
    public ICommand? GetBookingsBySSNCommand { get; }
    public ICommand? GetALLPilotsAndCabinCrewCommand { get; }

    //----------------------------- Methods-----------------------------------------------------------------------------------------//

    ///------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Updates the selected aircrew user based on the user's title (Pilot or CabinCrew).
    /// </summary>
    /// <remarks>
    /// This method checks the title of the selected aircrew to determine if it's a Pilot or CabinCrew.
    /// It then calls the corresponding private method to perform the user update.
    /// After the update operation, a MessageBox displays the result message, and all pilots and cabin crew members are reloaded.
    /// In case of an exception during the update process, the exception is caught, logged, and an error message is displayed.
    /// </remarks>
    /// ----------------------------------------------------------------------------------------------------------------------------
    public void UpdateAirCrew(object obj)
    {
        string message = "";

            // Check the title of the selected aircrew to determine if it's a Pilot or CabinCrew
            if (SelectedPilot.UserTitle == "Pilot")
            {
                // Update the selected Pilot user
                message = UpdatePilotUser();
            }
            else if (SelectedPilot.UserTitle == "CabinCrew")
            {
                // Update the selected CabinCrew user
                message = UpdateCabinCrewUser();
            }

            // Display the update result message in a MessageBox
            MessageBox.Show(message);

            // Reload all pilots and cabin crew members
            LoadAllPilotsAndCabinCrews();       
    }

    /// -----------------------------------------------------------------------------------------------------------------------/
    /// <summary>
    /// Loads all pilots and cabin crews from the data source and updates the respective collections.
    /// </summary>
    /// <remarks>
    /// This method retrieves the list of pilots and cabin crews from the appointment repository.
    /// It then updates the <see cref="AllPilots"/> and <see cref="AllCabinCrews"/> collections with the results.
    /// </remarks>
    /// -----------------------------------------------------------------------------------------------------------------------/
    public void LoadAllPilotsAndCabinCrews()
    {
            // Retrieve pilots and cabin crews from the data source
            var (pilots, cabinCrews) = appointmentRepo.GetAllPilotsAndCabinCrews();

            // Update the AllPilots and AllCabinCrews collections with the results
            AllPilots = pilots.OfType<Pilot>().ToList();
            AllCabinCrews = cabinCrews.OfType<CabinCrew>().ToList();        
    }

    ///-------------------------------------------------------------------------------------
    /// <summary>
    /// Updates the information of a Pilot user.
    /// </summary>
    /// <returns>A string, indicating the result of the update operation.</returns>
    /// ------------------------------------------------------------------------------------
    private string UpdatePilotUser()
    {
            // Perform the update for the selected Pilot
            return pilotRepo.Update(SelectedPilot);
    }

    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// Updates the information of a CabinCrew user.
    /// </summary>
    /// <returns>A string, indicating the result of the update operation.</returns>
    /// ------------------------------------------------------------------------------------
    private string UpdateCabinCrewUser()
    {
            // Perform the update for the selected CabinCrew
            return cabinCrewRepo.Update(SelectedPilot);
    }

    /// ------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Retrieves all information related to the selected pilot and updates various properties.
    /// </summary>
    /// <remarks>
    /// This method retrieves information about the selected pilot, including general information,
    /// upcoming appointments, and appointments history. The information is then assigned to respective properties
    /// for further use in the application. In case of an exception during the retrieval process,
    /// the exception is caught, logged, and an error message is displayed.
    /// </remarks>
    /// ------------------------------------------------------------------------------------------------------------
    private void GetAllInfo(object obj)
    {
            // Retrieve general information about the selected pilot
            this.UserInfo = pilotRepo.GetAirCrewInformation(this.SelectedPilot.SocialSecurityNumber);

            // Retrieve and update upcoming appointments for the selected pilot
            GetBookingsBySSN(obj);

            // Retrieve and update appointments history for the selected pilot
            GetAppointmentsHistoryBySSN();
    }

    ///-------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Retrieves upcoming appointments for the selected pilot based on their Social Security Number.
    /// </summary>
    /// <remarks>
    /// This method queries the database to retrieve upcoming appointments for the selected pilot.
    /// The appointments are then assigned to the Appointments property for display in the application.
    /// In case of an exception during the retrieval process, the exception is caught, logged,
    /// and an error message is displayed.
    /// </remarks>
    /// --------------------------------------------------------------------------------------------------------
    private void GetBookingsBySSN(object obj)
    {
            // Retrieve and update upcoming appointments for the selected pilot
            Appointments = appointmentRepo.GetBySocialSecurityNumber(this.SelectedPilot.SocialSecurityNumber);
    }

    ///-----------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Retrieves appointments history for the selected pilot based on their Social Security Number.
    /// </summary>
    /// <remarks>
    /// This method queries the database to retrieve appointments history for the selected pilot.
    /// The history information is then assigned to the BookingHistory property for display in the application.
    /// In case of an exception during the retrieval process, the exception is caught, logged,
    /// and an error message is displayed.
    /// </remarks>
    /// ----------------------------------------------------------------------------------------------------------
    private void GetAppointmentsHistoryBySSN()
    {
            // Retrieve and update appointments history for the selected pilot
            BookingHistory = appointmentRepo.GetAppointmentsHistoryBySSN(this.SelectedPilot.SocialSecurityNumber);
    }

    ///------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Executes the command to delete the selected aircrew user based on their user title.
    /// </summary>
    /// <remarks>
    /// This method determines the user title of the selected aircrew (Pilot or CabinCrew) and calls
    /// the corresponding delete method. After the deletion, it reloads all pilots and cabin crews for
    /// an updated view. If an exception occurs during the deletion process, an error message is displayed.
    /// </remarks>
    /// -----------------------------------------------------------------------------------------------------
    private void ExecuteDeleteAirCrewUserCommand(object obj)
    {
            // Determine the user title of the selected aircrew and call the corresponding delete method
            if (SelectedPilot.UserTitle == "Pilot")
            {
                DeletePilotUser();
            }
            else if (SelectedPilot.UserTitle == "CabinCrew")
            {
                DeleteCabinCrewUser();
            }

            // Reload all pilots and cabin crews for an updated view
            LoadAllPilotsAndCabinCrews();
    }

    /// ----------------------------------------------------------------------------------------------------------------------/
    /// <summary>
    /// Deletes the selected Cabin Crew user.
    /// </summary>
    /// <remarks>
    /// This method calls the repository to delete the Cabin Crew user based on the Social Security Number.
    /// After the deletion, a message is displayed using MessageBox.Show. If an exception occurs during the deletion process,
    /// an error message is displayed.
    /// </remarks>
    /// -----------------------------------------------------------------------------------------------------------------------/
    private void DeleteCabinCrewUser()
    {
            // Delete the selected Cabin Crew user
            MessageBox.Show(cabinCrewRepo.DeleteCabinCrew(SelectedPilot.SocialSecurityNumber));
    }

    /// -----------------------------------------------------------------------------------------------------------------------/
    /// <summary>
    /// Deletes the selected Pilot user.
    /// </summary>
    /// <remarks>
    /// This method calls the repository to delete the Pilot user based on the Social Security Number.
    /// After the deletion, a message is displayed using MessageBox.Show. If an exception occurs during the deletion process,
    /// an error message is displayed.
    /// </remarks>
    /// -----------------------------------------------------------------------------------------------------------------------/
    private void DeletePilotUser()
    {
            // Delete the selected Pilot user
            MessageBox.Show(pilotRepo.DeletePilot(SelectedPilot.SocialSecurityNumber));
    }

}
