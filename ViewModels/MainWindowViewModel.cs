using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Views;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        //----------------------------Properties-----------------------------------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// Properties Explanation:
        /// 
        /// - 'CurrentChildView': Represents the current view for display in 
        ///   the 'MainWindow' ContentContainer.
        ///   
        /// - 'Caption': Represents the text displayed on the header/caption 
        ///   section of the 'MainWindow' ContentContainer.
        ///   
        /// - 'Icon': Represents an icon associated with the child view and is 
        ///   displayed in the header/caption section before the text from the 
        ///   'Caption' property.
        /// </summary>
        //**************************************************************************//

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
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
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
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));

            }
        }

        //----------------------------Commands-----------------------------------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// 'ShowLoginAirCrewCommand' Explanation:
        /// 
        /// - Displays the 'LoginAirCrew' view when triggered.
        /// 
        /// - Initialized with 'CommandBase' in the constructor, associated 
        ///   with 'ExecuteShowLoginAirCrewCommand' method.
        /// 
        /// - When triggered, the method:
        ///   - Creates a new 'LoginAirCrewViewModel' instance.
        ///   - Sets 'Caption' to "Air Crew Login" and 'Icon' accordingly.
        ///   - Establishes a connection to the UI via 'CurrentChildView' property 
        ///     in 'MainWindowViewModel,' displaying 'LoginAirCrew' UserControl.
        /// </summary>
        //**************************************************************************//
        public ICommand ShowLoginAirCrewCommand { get; }       

        public ICommand ShowLoginAMECommand { get; }

        public ICommand ShowCreateAirCrewViewCommand { get; }

        //----------------------------Constructor-----------------------------------------------------------------//
        public MainWindowViewModel() 
        {
            //Initialize commands
            ShowLoginAirCrewCommand = new CommandBase(ExecuteShowLoginAirCrewCommand);
            ShowLoginAMECommand = new CommandBase(ExecuteShowLoginAMECommand);
            ShowCreateAirCrewViewCommand = new CommandBase(ExecuteShowCreateAirCrewViewCommand);
        }

        //----------------------------Methods-----------------------------------------------------------------//

        private void ExecuteShowCreateAirCrewViewCommand(object obj)
        {
            CurrentChildView = new CreateAirCrewViewModel();
            Caption = "Create new AirCrew User";
            Icon = IconChar.UserPlus;
        }
        private void ExecuteShowLoginAMECommand(object obj)
        {
            CurrentChildView = new LoginAMEViewModel();
            Caption = "AME Login";
            Icon = IconChar.UserDoctor;
        }

        private void ExecuteShowLoginAirCrewCommand(object obj)
        {
            CurrentChildView = new LoginAirCrewViewModel();
            Caption = "Air Crew Login";
            Icon = IconChar.PersonCircleCheck;
        }     
    }
}
