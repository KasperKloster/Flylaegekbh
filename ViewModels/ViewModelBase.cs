using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
