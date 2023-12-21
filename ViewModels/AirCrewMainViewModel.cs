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
        //**************************************************************************//
        /// <summary>
        /// Explanation of AirCrewMainViewModel:
        /// 
        /// - This class is essentially a copy of MainWindowViewModel, specifically
        ///   designed for the AirCrewMainView instead of MainWindow.
        /// 
        /// - The purpose is to organize ChildViews related to the AirCrewMainView.
        /// 
        /// - While using a dedicated service or helper class for routing and 
        ///   navigation is often considered a better practice, it might be 
        ///   beyond the scope of our project. Therefore, we handle routing 
        ///   and navigation in this way.
        /// 
        /// - Note: Using a dedicated service or helper class for routing and 
        ///   navigation is an alternative approach that may provide better 
        ///   maintainability, especially in larger or more complex projects.
        /// </summary>
        //**************************************************************************//

        //------------------------------Properties------------------------------------------------------------------------------------//    

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

        //------------------------------Commands------------------------------------------------------------------------------------// 
        public ICommand ShowAirCrewViewCommand { get; }
        public ICommand ShowAppointmentViewCommand { get; }

        //------------------------------Constructor------------------------------------------------------------------------------------// 
        public AirCrewMainViewModel()
        {
            ShowAirCrewViewCommand = new CommandBase(ExecuteShowAirCrewViewCommand);
            ShowAppointmentViewCommand = new CommandBase(ExecuteShowAppointmentViewCommand);
        }

        //------------------------------Methods------------------------------------------------------------------------------------// 
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
