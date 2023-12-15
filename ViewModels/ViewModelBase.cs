using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    /*************************************************************/
    /*          Explanation of ViewModelBase Class               */
    /*************************************************************/
    /*  The ViewModelBase class serves as a base class for other 
    view model classes in the application. Its a common 
    practice to create a base class to encapsulate shared 
    functionality, and in this case, it provides a simple 
    implementation of the INotifyPropertyChanged interface.
    
        The 'INotifyPropertyChanged' interface is part of the .NET
    framwork and is particularly important in the context of data
    binding and MVVM.

        Definition of the PropertyChanged Event:
    'INotifyPropertyChanged' defines an event named 'PropertyChanged'
    This event is raised when the value of a property changes,
    allowing external subscribers (UI elements) to be notified of
    the change.
    
        The OnPropertyChanged Method:
    This methid is a convenience method provided by the 
    'ViewModelBase' class. It simplifies the process of raising the 
    'PropertyChanged' event for a specific property. It should
    be called from property setters in derived classes to notify
    subscribers that a property has changed                      */
    /*************************************************************/

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }



    //########## her prøver jeg at se om jeg kan undgå noget dublicated code##############
    /* så jeg har tilføjet felter og commands som jeg tror kan bruges i flere ViewModels */


    /*************************************************************/
    /*          Explanation of AppointmentRepo                   */
    /*************************************************************/
    /*  Creating a property appointmentRepo of type AppointmentRepo
     reduses duplicated code, and makes it easyer to refer to the
     AppointmentRepo class further down in the code              */
    /*************************************************************/
    public AppointmentRepo appointmentRepo = new AppointmentRepo();

    public CabinCrewRepo cabinCrewRepo = new CabinCrewRepo();
    public string FirstNames { get; set; }
    public string SurName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string SocialSecurityNumber { get; set; }

    //public Titles Title { get; set; }

    public string Title { get; set; }

    public string ML_CertificateNumber { get; set; }
    public DateTime ML_DateOfIssue { get; set; } = DateTime.Now;
    public DateTime ML_Class1SinglePilotExpiryDate { get; set; } = DateTime.Now;
    public DateTime ML_Class1ExpiryDate { get; set; } = DateTime.Now;
    public DateTime ML_Class2ExpiryDate { get; set; } = DateTime.Now;
    public DateTime ML_LAPLExpiryDate { get; set; } = DateTime.Now;
    public DateTime ML_ElectroCardiogramRecentDate { get; set; } = DateTime.Now;
    public DateTime ML_AudiogramRecentDate { get; set; } = DateTime.Now;

    public DateTime MR_DateOfIssue { get; set; } = DateTime.Now;
    public DateTime MR_CabinCrewExpiryDate { get; set; } = DateTime.Now;

    private Pilot selectedPilot;

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

    //this field is used as the source to display all Pilots in the DB.
    //This allows us to get the Pilots PrimaryKey (SocialSecurityNumber) and all other information
    //about the pilot based on the name from the menuto this list is bound to.
    //furthere more this simulate wich user is loged in, and are performing actions in the IT-system. 
    private List<Pilot> allPilots;
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

    //this field is used as the source to display all Cabin Crews in the DB.
    //this serves and allow exatly the same as the Pilot-list, but it is only Cabin Crew objects.        
    private List<CabinCrew> allCabinCrews;
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

    //public ICommand GetALLPilotsAndCabinCrewCommand { get; set; }
    //    public ICommand GetALLExaminationsCommand { get; }

    protected virtual void HandleException(Exception ex, string customMessage = null)
    {
        // Log the exception instead of displaying in a MessageBox
        // Optionally, you can log it to a file or a logging framework
        Debug.WriteLine($"Error: {customMessage ?? ex.Message}");
        MessageBox.Show($"Der er sket en fejl: {ex.Message}");
    }

    public void LoadAllPilotsAndCabinCrews()
    {
        try
        {

            var (pilots, cabinCrews) = appointmentRepo.GetAllPilotsAndCabinCrews();

            AllPilots = pilots.OfType<Pilot>().ToList();
            AllCabinCrews = cabinCrews.OfType<CabinCrew>().ToList();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }

    }
}
