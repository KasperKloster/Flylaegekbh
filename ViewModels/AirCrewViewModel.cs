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

    //this field, Appointments, is used as the source to display all appointments belongin to specific aircrew 
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

    //this field, AllCabinCrews, is used as the source to display all Cabin Crews in the DB.
    //this serves and allow exatly the same as the Pilot-list, but it is only Cabin Crew objects.        
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

    //this field, AllPilots, is used as the source to display all Pilots in the DB.
    //This allows us to get the Pilots PrimaryKey (SocialSecurityNumber) and all other information
    //about the pilot based on the name from the menuto this list is bound to.
    //furthere more this simulate wich user is loged in, and are performing actions in the IT-system. 
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

    //this field, SelectedPilot, is used to bind the selectedPilot objects SocialSecurityNumber to the PilotCabinCrew_SSN property.
    //This ensures that we cand parss the selectedPilot SocialSecurityNumber to other Actions throug the property PilotCabinCrew_SSN.
    //private Pilot selectedPilot;
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

            //PilotCabinCrew_SSN = selectedPilot?.SocialSecurityNumber;
        }
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
        try
        {
            // Retrieve pilots and cabin crews from the data source
            var (pilots, cabinCrews) = appointmentRepo.GetAllPilotsAndCabinCrews();

            // Update the AllPilots and AllCabinCrews collections with the results
            AllPilots = pilots.OfType<Pilot>().ToList();
            AllCabinCrews = cabinCrews.OfType<CabinCrew>().ToList();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the data retrieval
            HandleException(ex, "An error occurred while loading pilots and cabin crews.");
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
    private void GetAllInfo(object obj)
    {
        try
        {
            // Retrieve general information about the selected pilot
            this.UserInfo = pilotRepo.GetAirCrewInformation(this.SelectedPilot.SocialSecurityNumber);

            // Retrieve and update upcoming appointments for the selected pilot
            GetBookingsBySSN(obj);

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
    private void GetBookingsBySSN(object obj)
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
    private void ExecuteDeleteAirCrewUserCommand(object obj)
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
