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
    //**************************************************************************//
    /// <summary>
    /// ViewModelBase Class Explanation:
    /// 
    /// The base class for view models in the application, providing a simple
    /// implementation of the INotifyPropertyChanged interface for data binding
    /// and MVVM.
    /// </summary>
    //**************************************************************************//
    /// <summary>
    /// INotifyPropertyChanged Event:
    /// 
    /// Defines the 'PropertyChanged' event, notifying subscribers (UI elements)
    /// when a property changes.
    /// </summary>
    //**************************************************************************//
    /// <summary>
    /// OnPropertyChanged Method:
    /// 
    /// A convenience method in the 'ViewModelBase' class to simplify raising
    /// the 'PropertyChanged' event for a specific property. Should be called
    /// from property setters in derived classes.
    /// </summary>
    //**************************************************************************//

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //----------------------------Fields-----------------------------------------------------------------//
    //**************************************************************************//
    /// <summary>
    /// Explanation of Repositories:
    /// 
    /// Simplifies code by creating properties for appointment, pilot, and cabin
    /// crew repositories, reducing duplication and enhancing readability.
    /// </summary>
    //**************************************************************************//
    public AppointmentRepo appointmentRepo = new AppointmentRepo();
    public PilotRepo pilotRepo = new PilotRepo();
    public CabinCrewRepo cabinCrewRepo = new CabinCrewRepo();
}
