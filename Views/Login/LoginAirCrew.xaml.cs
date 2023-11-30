using FlyveLægeKBH.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlyveLægeKBH.Views
{
    /// <summary>
    /// Interaction logic for LoginAirCrew.xaml
    /// </summary>
    public partial class LoginAirCrew : UserControl
    {
        /*************************************************************/
        /*             Explanation of this UserControl               */
        /*************************************************************/
        /*  This UserControl represents a specific section of the
        application. This is the section simulating what screen
        a AirCrew will se when they whant to login to the
        Flylægerne KBH's Booking system.                             */
        /*************************************************************/



        /*************************************************************/
        /*  Explanation of instance creation LoginAirCrewViewModel   */
        /*************************************************************/
        /*  We create an instance of the LoginAirCrewViewModel class
        within the 'LoginAirCrew' UserControl. Now we can appaly the
        nessecary data, commands and behavior specifics to the the
        login functionality for the AirCrew in the 
        LoginAirCrewViewModel                                        */
        /*************************************************************/

        LoginAirCrewViewModel _loginAirCrewViewModel = new LoginAirCrewViewModel();


        /*************************************************************/
        /*                Explanation of DataContex                  */
        /*************************************************************/
        /*  By setting the 'DataContext' we are enabling a connection
        between the XAML elements in 'LoginAirCrew' UserControl and the
        properties/methods in the 'LoginAirCrewViewModel'. This allows
        to use data binding expressions in the XAML to display and 
        manipulate data                                              */
        /*************************************************************/
        public LoginAirCrew()
        {
            InitializeComponent();
            DataContext = _loginAirCrewViewModel;
        }


        /*************************************************************/
        /*            Explanation of OnLoginButtonClick              */
        /*************************************************************/
        /*  This methode opens the 'AirCrewMainview'-window and hide
        the 'MainWindow'. In a real-life scenario we would need to 
        add the use of Commands, Login Authentication and Data Handling

        we could make use of an Service-class. we could call it 
        NavigationService and then inject that in the 
        'LoginAirCrewViewModel' That would keep the codebehinde file 
        clean and also keep the seperation of concerns clearner for the
        VieModel class                                               */
        /*************************************************************/
        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            // Hide the MainWindow
            Application.Current.MainWindow.Hide();

            // open the AirCrewMainView window
            AirCrewMainview airCrewMainview = new AirCrewMainview();
            airCrewMainview.Show();            
        }

        
    }
}
