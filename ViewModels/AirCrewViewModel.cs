using System;
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
using FlyveLægeKBH.Commands.AirCrew;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;


namespace FlyveLægeKBH.ViewModels;

class AirCrewViewModel : ViewModelBase
{
    //------------------------------Fields------------------------------------------------------------------------------------//    
    public PilotRepo pilotRepo = new PilotRepo();
    public CabinCrewRepo cabinCrewRepo = new CabinCrewRepo();
    AppointmentRepo appointmentRepo = new AppointmentRepo();

    // There are some fully implemented property that uses OnPropertyChanged, as this property changes after an execution of a method.
    
    private string userInfo;

    //this field is used as source to display all info stored of a user/aircrew 
    public string UserInfo
    {
        get { return userInfo; }
        set { userInfo = value; OnPropertyChanged(nameof(UserInfo)); }
    }



    //this field is used as source to display all historical bookings and changes of bookings 
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
    public ICommand GetAllInfoCommand { get; set; }
    public ICommand DeleteAirCrewUserCommand { get; }
    public ICommand GetBookingsBySSNCommand { get; set; }
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
    public void UpdateAirCrew()
    {
        string message = "";

        try
        {
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
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "Det lykkedes ikke at opdatere brugeren");
        }
    }

    ///-------------------------------------------------------------------------------------
    /// <summary>
    /// Updates the information of a Pilot user.
    /// </summary>
    /// <returns>A string message indicating the result of the update operation.</returns>
    /// ------------------------------------------------------------------------------------
    private string UpdatePilotUser()
    {
        string message = "";
        try
        {
            // Perform the update for the selected Pilot
            message = pilotRepo.Update(SelectedPilot);
        }
        catch (Exception ex)
        {
            // Handle exceptions specific to updating a Pilot
            HandleException(ex, "Det lykkedes ikke at opdatere Piloten.");
        }
        return message;
    }

    /// ------------------------------------------------------------------------------------
    /// <summary>
    /// Updates the information of a CabinCrew user.
    /// </summary>
    /// <returns>A string message indicating the result of the update operation.</returns>
    /// ------------------------------------------------------------------------------------
    private string UpdateCabinCrewUser()
    {
        string message = "";
        try
        {
            // Perform the update for the selected CabinCrew
            message = cabinCrewRepo.Update(SelectedPilot);
        }
        catch (Exception ex)
        {
            // Handle exceptions specific to updating a CabinCrew
            HandleException(ex, "Det lykkedes ikke at opdatere Cabine personalet.");
        }
        return message;
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
    private void GetAllInfo()
    {
        try
        {
            // Retrieve general information about the selected pilot
            this.UserInfo = pilotRepo.GetAirCrewInformation(this.SelectedPilot.SocialSecurityNumber);

            // Retrieve and update upcoming appointments for the selected pilot
            GetBookingsBySSN();

            // Retrieve and update appointments history for the selected pilot
            GetAppointmentsHistoryBySSN();
        }
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "An error occurred while retrieving aircrew information.");
        }
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
    private void GetBookingsBySSN()
    {
        try
        {
            // Retrieve and update upcoming appointments for the selected pilot
            Appointments = appointmentRepo.GetBySocialSecurityNumber(this.SelectedPilot.SocialSecurityNumber);
        }
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "An error occurred while retrieving upcoming appointments.");
        }
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
        try
        {
            // Retrieve and update appointments history for the selected pilot
            BookingHistory = appointmentRepo.GetAppointmentsHistoryBySSN(this.SelectedPilot.SocialSecurityNumber);
        }
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "An error occurred while retrieving appointments history.");
        }
    }



    /*************************************************************/
    /*        Explanation of DeleteAirCrewUserCommand            */
    /*************************************************************/
    /*                                                           */
    /*************************************************************/




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
    private void ExecuteDeleteAirCrewUserCommand()
    {
        try
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
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "An error occurred while deleting the aircrew user.");
        }
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
        try
        {
            // Delete the selected Cabin Crew user
            MessageBox.Show(cabinCrewRepo.DeleteCabinCrew(SelectedPilot.SocialSecurityNumber));
        }
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "An error occurred while deleting the Cabin Crew user.");
        }
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
        try
        {
            // Delete the selected Pilot user
            MessageBox.Show(pilotRepo.DeletePilot(SelectedPilot.SocialSecurityNumber));
        }
        catch (Exception ex)
        {
            // Handle exceptions and display an error message
            HandleException(ex, "An error occurred while deleting the Pilot user.");
        }
    }

}
