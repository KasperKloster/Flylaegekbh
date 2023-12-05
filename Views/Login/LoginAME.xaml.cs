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
    /// Interaction logic for LoginAME.xaml
    /// </summary>
    public partial class LoginAME : UserControl
    {
        LoginAMEViewModel _loginAMEViewModel = new LoginAMEViewModel();
        public LoginAME()
        {
            InitializeComponent();
            DataContext = _loginAMEViewModel;
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Hide();
            AMEMainView ameMainView = new AMEMainView();
            ameMainView.Show();

        }
    }
}
