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

        /*************************************************************/
        /*                Explanation of Properties                  */
        /*************************************************************/
        /*  'CurrentChildView': Represents the current view that should
        be displayed in the 'MainWindow' ContentContainer
        
            'Caption': Represents the text displayed on the 
        header / caption section of the 'MainWindow' ContentContainer
        
            'Icon': Reoresents a Icon associatede with the child view
        and are also displayed in the header / caption section it is
        placed befor the text from teh Caption property              */
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



        /*************************************************************/
        /*      Explanation of ShowLoginAirCrewCommand               */
        /*************************************************************/
        /*  This command defines the action of displaying 
        the 'LoginAirCrew' view.
        
        In the constructor the 'ShowLoginAirCrewCommand' is initialized
        with a new instance of 'CommandBase' wich implements the ICommand
        interface (for more explanation go to CommandBase class) and it
        is associated with the 'ExecuteShowLoginAirCrewCommand' method

        When the 'ShowLoginAireCrewCommand' is triggered by the click on
        the side-menu button (FlyPersonale), the 
        'ExecuteShowLoginAirCrewCommand' is called.

        This method dose:
            Creates a new instance of 'LoginAirCrewViewModel'
            Sets the 'Caption' property to "Air Crew Login"
            Stes the 'Icon' property to fitting icon of context

        And because we made the property binding to CurrentChildView
        of the 'MainWindowViewModel' this estabilshes a connection between
        'LoginAirCrewViewModel' instance and the UI. There for oure 
        Resource definition that specifices how to render a 
        'LoginAirCrewViewModel' tells the application to use the 
        'LoginAirCrew' UserControl when presenting a 'LoginAirCrewViewModel'
        So when the 'ExecuteShowLoginAirCrewCommand' method is called, 
        it changes the 'CurrentChildView' property, and displays the 
        'LoginAirCrew'

        (for more explanation on this go to MainWindow.Xaml
        and see the comments under Window.DataContext and 
        Window.Resources and DataTemplate)
         */
        /*************************************************************/

        // Commands
        public ICommand ShowLoginAirCrewCommand { get; }

        // Constructor
        public MainWindowViewModel() 
        {
            //Initialize commands
            ShowLoginAirCrewCommand = new CommandBase(ExecuteShowLoginAirCrewCommand);
                      
        }


        // Methods
        private void ExecuteShowLoginAirCrewCommand(object obj)
        {
            CurrentChildView = new LoginAirCrewViewModel();
            Caption = "Air Crew Login";
            Icon = IconChar.PersonCircleCheck;
        }

    }
}
