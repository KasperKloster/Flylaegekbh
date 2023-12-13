using FlyveLægeKBH.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



    public ICommand GetALLPilotsAndCabinCrewCommand { get; set; }
    public ICommand GetALLExaminationsCommand { get; }
}
