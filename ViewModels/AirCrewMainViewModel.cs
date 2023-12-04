using FlyveLægeKBH.Commands;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels
{
    public class AirCrewMainViewModel : ViewModelBase
    {
        /*************************************************************/
        /*                Explanation of AirCrewMainViewModel        */
        /*************************************************************/
        /*  This is basicly a copi of what MainWindowViewModel, but it 
         is specified for the AirCrewMainview insted of MainWindow.
        
            This allows us to seperate the ChildViews for the MainWindow 
        And for the AirCrewMainview. This way we keep every thing 
        related to AirCrew (Customers) ChildViews in this ViewModel 
        class                  
        
            This is probaly not the most correct or smartes way. As
        mentiont in another comment using a service or a helper class
        handling all logic for routing and navigation is probaly better
        pratices then this. But sinces this could be consider out of 
        scope for oure project we choose to handle routing and navigation
        like this                                                    */
        /*************************************************************/


        // Properties 

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        public ViewModelBase CurrentChildView
        {
            get 
            {
                return _currentChildView; 
            }
            set
            {
                _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView));
            }

        }

        public string Caption
        {
            get 
            {   
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
                

        public IconChar Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        // Commands
        public ICommand ShowAirCrewViewCommand { get; }
        public ICommand ShowAppointmentViewCommand { get; }
        public AirCrewMainViewModel()
        {
            ShowAirCrewViewCommand = new CommandBase(ExecuteShowAirCrewViewCommand);
            ShowAppointmentViewCommand = new CommandBase(ExecuteShowAppointmentViewCommand);
        }

        // Methods
        private void ExecuteShowAirCrewViewCommand(object obj)
        {
            CurrentChildView = new AirCrewViewModel();
            Caption = "My Profile";
            Icon = IconChar.IdBadge;
        }

        private void ExecuteShowAppointmentViewCommand(object obj)
        {
            CurrentChildView = new AppointmentViewModel();
            Caption = "Booking Site";
            Icon = IconChar.CalendarCheck;
        }


    }   
}
