using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels
{
    public class ProfileViewModel: ViewModelBase
    {
        //----------Fields----------------------------

        // The is a fully implemented property that uses OnPropertyChanged, as this property changes after an execution of a method.
        private string userInfo;

        public string UserInfo
        {
            get { return userInfo; } set { userInfo = value; OnPropertyChanged(nameof(UserInfo)); }
        }
        public string SocialSecurityNUmber { get; set; }

        public ICommand GetAllInfoCommand { get; set; }

        public ProfileViewModel()
        {
            GetAllInfoCommand = new CommandBase(GetAllInfo);//, CanGetAllInfo);
            
        }

        private bool CanGetAllInfo(object obj)
        {
            return true;
        }

        private void GetAllInfo(object obj)
        {
            UserInfo = PilotRepo.GetAirCrewInformation(SocialSecurityNUmber);
        }
    }
}
