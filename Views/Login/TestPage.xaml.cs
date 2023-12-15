using FlyveLægeKBH.Repos;
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

namespace FlyveLægeKBH.Views.Login
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : UserControl
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void Get_all_info_test_btn_Click(object sender, RoutedEventArgs e)
        {
            string socialSecurityNumber = Get_all_info_test_tbx.Text;

            Get_all_info_test_card.Text = PilotRepo.GetAirCrewInformation(socialSecurityNumber);
            //MessageBox.Show(PilotRepo.GetAirCrewInformation(socialSecurityNumber));
        }

        private void Create_booking_test_Click(object sender, RoutedEventArgs e)
        {
            string pilotCabinCrewSSN = "123456-7890";
            string ameSSN = "121212-1212";
            string examinationName = "Cabin Crew Fornyelse";
            TimeSpan startTime = TimeSpan.Parse("12:00");
            DateTime appointmentDate = DateTime.Now;

            AppointmentRepo appointmentRepo = new AppointmentRepo();
            MessageBox.Show(appointmentRepo.Create(pilotCabinCrewSSN, ameSSN, examinationName, startTime, appointmentDate));
        }

        //private void Delete_AirCrew_test_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    PilotRepo pilotRepo = new PilotRepo();
        //    MessageBox.Show(pilotRepo.DeletePilot(Delete_airCrew_test_tbx.Text));
        //}
    }
}
