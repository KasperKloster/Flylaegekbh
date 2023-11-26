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

namespace FlyveLægeKBH.Views.AirCrewSubjects.Profile;

public partial class UpdateAirCrewUserView : UserControl
{
    AirCrewViewModel airCrewViewModel = new AirCrewViewModel();

    public UpdateAirCrewUserView()
    {
        InitializeComponent();
        DataContext = airCrewViewModel;
    }
}
