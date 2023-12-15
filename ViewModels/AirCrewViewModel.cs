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
    // Fields    
    public PilotRepo pilotRepo = new PilotRepo();
    public CabinCrewRepo cabinCrewRepo = new CabinCrewRepo();
    AppointmentRepo appointmentRepo = new AppointmentRepo();


    // FOR DEVELOPMENT: Simulates the logged in user  
    //private string title;
    //private string socialSecurityNumber;



    //private List<IUser> users;

    //public List<IUser> Users
    //{
    //    get { return users; }
    //    set { if (users != value) { users = value; OnPropertyChanged(nameof(Users)); } }
    //}


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



    public AirCrewViewModel()
    {
        // Initialize commands
        DeleteAirCrewUserCommand = new CommandBase(ExecuteDeleteAirCrewUserCommand);
        GetAllInfoCommand = new CommandBase(GetAllInfo);
        UpdateAirCrewUserCommand = new CommandBase(UpdateAirCrew);
        GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
        //GetALLPilotsAndCabinCrewCommand = new CommandBase(ExecuteGetALLPilotsAndCabinCrewCommand);

        // Load pilots and cabin crew when the view model is created
        LoadAllPilotsAndCabinCrews();
    }

    //----------------------------- Commands------------------------------------------------//
    public ICommand UpdateAirCrewUserCommand { get; }
    public ICommand GetAllInfoCommand { get; set; }
    public ICommand DeleteAirCrewUserCommand { get; }
    public ICommand GetBookingsBySSNCommand { get; set; }





    //----------------------------- Methods------------------------------------------------//

    //********************************************************************************************************//
    /*By adding the LoadPilotsAndCabinCrews method to the constructor, we ensure that when an instance of 
     * AirCrewViewModel is created, it automatically loads the pilots and cabin crew into the Users property.*/
    //********************************************************************************************************//



    public void UpdateAirCrew(object obj)
    {

        string message = "";
        if (SelectedPilot.UserTitle == "Pilot")
        {
            message = updatePilotUser();
        }
        if (SelectedPilot.UserTitle == "CabinCrew")
        {
            message = updateCabinCrewUser();
        }
        MessageBox.Show(message);

        LoadAllPilotsAndCabinCrews();
    }

    private string updatePilotUser()
    {
        return pilotRepo.Update(SelectedPilot);
    }

    private string updateCabinCrewUser()
    {
        return cabinCrewRepo.Update(SelectedPilot);
    }

    private void GetAllInfo(object obj)
    {
        this.UserInfo = pilotRepo.GetAirCrewInformation(this.SelectedPilot.SocialSecurityNumber);


        GetBookingsBySSN(this.SelectedPilot.SocialSecurityNumber);
        GetAppointmentsHistoryBySSN(this.SelectedPilot.SocialSecurityNumber);


    }

    private void GetBookingsBySSN(object obj)
    {
        

        Appointments = appointmentRepo.GetBySocialSecurityNumber(this.SelectedPilot.SocialSecurityNumber);


    }

    private void GetAppointmentsHistoryBySSN(object obj)
    {
        BookingHistory = appointmentRepo.GetAppointmentsHistoryBySSN(this.SelectedPilot.SocialSecurityNumber);

    }


    /*************************************************************/
    /*        Explanation of DeleteAirCrewUserCommand            */
    /*************************************************************/
    /*                                                           */
    /*************************************************************/



    // Kristians udgave af brugen af commands med CommandBase Class

    //Initialize commands in the ctor scroll up to see it.

    // Methods
    private void ExecuteDeleteAirCrewUserCommand(object obj)
    {
        if (SelectedPilot.UserTitle == "Pilot")
        {
            DeletePilotUser();
        }
        if (SelectedPilot.UserTitle == "CabinCrew")
        {
            DeleteCabinCrewUser();
        }
        LoadAllPilotsAndCabinCrews();
    }

    private void DeleteCabinCrewUser()
    {
        MessageBox.Show(cabinCrewRepo.DeleteCabinCrew(SelectedPilot.SocialSecurityNumber));        
    }

    private void DeletePilotUser()
    {
        MessageBox.Show(pilotRepo.DeletePilot(SelectedPilot.SocialSecurityNumber));
    }
}
