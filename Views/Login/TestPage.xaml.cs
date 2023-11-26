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
    }
}
