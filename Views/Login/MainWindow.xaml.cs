using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlyveLægeKBH
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // to allow us to use the events of the operating system we need to import the 32 to libary
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        //Control panel behavior methods
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //with the 32 libary importet, we can now define the handle of the window
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        //for the maximized window function to work on evry monitor and if people use multiple screens with differend reselutions we need to update the maximum height of the window by the Mouse
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            // to presist the drag function when window is maximazied we need to specifie the height to match the primary screen the program is running at.
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        // the three control buttons events
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string firstName = "x";
            string SurName = "y";
            string Email = "x@y";
            string Phone = "123456";
            string Address = "home alone";
            string SocialSecurityNumber = "121212-2121";
            Titles title = Titles.Pilot;

            string cetificateNumber = "123";
            DateTime dateOfIssue = DateTime.Parse("01/01/2020");
            DateTime class1SinglePilotExpiryDate = DateTime.Parse("01/01/2021");
            DateTime class1ExpiryDate = DateTime.Parse("01/01/2021");
            DateTime class2ExpiryDate = DateTime.Parse("01/01/2025");
            DateTime laplExpiryDate = DateTime.Parse("01/01/2025");
            DateTime electroCardiogramRecentDate = DateTime.Parse("01/01/2019");
            DateTime audiogramRecentDate = DateTime.Parse("01/01/2020");

            MedicalLicense medicalLicense = new MedicalLicense(dateOfIssue, class1SinglePilotExpiryDate, class1ExpiryDate, class2ExpiryDate, laplExpiryDate, electroCardiogramRecentDate, audiogramRecentDate);
            PilotRepo.CreatePilot(firstName, SurName, Email, Phone, SocialSecurityNumber, title.ToString(), cetificateNumber, dateOfIssue, class1SinglePilotExpiryDate, class1ExpiryDate, class2ExpiryDate, laplExpiryDate, electroCardiogramRecentDate, audiogramRecentDate);
        }
    }
}
