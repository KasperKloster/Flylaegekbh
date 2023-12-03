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

namespace FlyveLægeKBH.Views.AirCrewSubjects.Profile
{
    /// <summary>
    /// Interaction logic for AirCrewView.xaml
    /// </summary>
    public partial class AirCrewView : UserControl
    {
        public AirCrewView()
        {
            AirCrewViewModel airCrewViewModel = new AirCrewViewModel();
            InitializeComponent();
            DataContext = airCrewViewModel;
        }
    }
}
