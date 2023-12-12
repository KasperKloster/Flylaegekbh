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
    private string firstNames;
    private string surName;
    private string email;
    private string phone;
    private string address;
    
    // Needs improvement
    
    //private string roadNumber;
    //private int postalCode;
    //private string city;

    // FOR DEVELOPMENT: Simulates the logged in user  
    private string title;
    private string socialSecurityNumber;

    // Properties
    public string FirstNames
    {
        get { return firstNames; }
        set
        {
            if (firstNames != value)
            {
                firstNames = value;
                OnPropertyChanged(nameof(FirstNames));
            }
        }
    }
    public string SurName
    {
        get { return surName;}
        set 
        {
            if (surName != value)
            {
                surName = value; 
                OnPropertyChanged(nameof(SurName));
            } 
        }
    }
    public string Email
    {
        get { return email; }
        set
        {
            if (email != value)
            {
                email = value; 
                OnPropertyChanged(nameof(Email));
            }
        }
    }
    public string Phone
    {
        get { return phone; }
        set
        {
            if (phone != value) {                
                phone = value; 
                OnPropertyChanged(nameof(Phone));
            }
        }
    }
    public string Address
    {
        get { return address; }
        set
        {
            if (address != value)
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
    }

    // There are some fully implemented property that uses OnPropertyChanged, as this property changes after an execution of a method.
    private string userInfo;

    //this field is used as source to display all info stored of a user/aircrew 
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
    //this field is used as the source to display all appointments belongin to specifik aircrew 
    private List<Appointment> appointments;
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
    //public string RoadNumber
    //{
    //    get { return roadNumber; }
    //    set
    //    {
    //        if (roadNumber != value)
    //        {
    //            OnPropertyChanged(nameof(RoadNumber));
    //        }
    //    }
    //}
    //public int PostalCode
    //{
    //    get { return postalCode; }
    //    set
    //    {
    //        if (postalCode != value)
    //        {
    //            OnPropertyChanged(nameof(PostalCode));
    //        }
    //    }
    //}
    //public string City
    //{
    //    get { return city; }
    //    set
    //    {
    //        if (city != value)
    //        {
    //            OnPropertyChanged(nameof(City));
    //        }
    //    }
    //}


    public AirCrewViewModel()
    {
        // FOR DEVELOPMENT: Simulates the logged in user                       
        Dictionary<string, string> firstUser = pilotRepo.GetFirstUser();
        this.FirstNames = firstUser["firstName"];
        this.SurName = firstUser["surName"];
        this.Email = firstUser["email"];
        this.Phone = firstUser["phone"];
        this.Address = firstUser["address"];
        
        this.title = firstUser["title"];
        this.socialSecurityNumber = firstUser["socialSecurityNumber"];

        // Initialize commands
        DeleteAirCrewUserCommand = new CommandBase(ExecuteDeleteAirCrewUserCommand);
        GetAllInfoCommand = new CommandBase(GetAllInfo);
        UpdateAirCrewUserCommand = new CommandBase(UpdateAirCrew);
        GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
    }




    // Commands
    public ICommand UpdateAirCrewUserCommand { get; }
    public ICommand GetAllInfoCommand { get; set; }
    public ICommand DeleteAirCrewUserCommand { get; }
    public ICommand GetBookingsBySSNCommand { get; set; }


    public void UpdateAirCrew(object obj)
    {
        string message = "";
        if(title == "Pilot")
        {
            message = updatePilotUser();
        }
        if (title == "CabinCrew")
        {
            message = updateCabinCrewUser();
        }
        MessageBox.Show(message);
    }

    private string updatePilotUser()
    {
        // Creates an pilot object to pass on
        Pilot pilot = new Pilot(
            firstNames : this.firstNames, 
            surName : this.surName, 
            email: this.email, 
            phone: this.phone,
            address: this.Address,
            ssn: this.socialSecurityNumber);
        // Updates in DB. Returns message
        return pilotRepo.Update(pilot);
    }

    private string updateCabinCrewUser()
    {
        CabinCrew cabinCrew = new CabinCrew(
            firstNames: this.firstNames,
            surName: this.surName,
            email: this.email,
            phone: this.phone,
            address: this.Address,
            ssn: this.socialSecurityNumber);

        return cabinCrewRepo.Update(cabinCrew);
    }

    private void GetAllInfo(object obj)
    {
        this.UserInfo = PilotRepo.GetAirCrewInformation(this.socialSecurityNumber);
        GetBookingsBySSN(this.socialSecurityNumber);
        GetAppointmentsHistoryBySSN(this.socialSecurityNumber);
    }

    private void GetBookingsBySSN(object obj)
    {
        AppointmentRepo appointmentRepo = new AppointmentRepo();

        Appointments = appointmentRepo.GetBySocialSecurityNumber(socialSecurityNumber);
    }

    private void GetAppointmentsHistoryBySSN(object obj)
    {
        AppointmentRepo appointmentRepo = new AppointmentRepo();

        BookingHistory = appointmentRepo.GetAppointmentsHistoryBySSN(socialSecurityNumber);
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
        if (title == "Pilot")
        {
            DeletePilotUser();
        }
        if (title == "CabinCrew")
        {
            DeleteCabinCrewUser();
        }
    }

    private void DeleteCabinCrewUser()
    {
        System.Windows.MessageBox.Show(CabinCrewRepo.DeleteCabinCrew(this.socialSecurityNumber));        
    }

    private void DeletePilotUser()
    {
        System.Windows.MessageBox.Show(PilotRepo.DeletePilot(this.socialSecurityNumber));
    }
}
