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

        // There are some fully implemented property that uses OnPropertyChanged, as this property changes after an execution of a method.
        private string userInfo;

        //this field is used as source to display all info stored of a user/aircrew 
        public string UserInfo
        {
            get { return userInfo; } 
            set { userInfo = value; OnPropertyChanged(nameof(UserInfo)); }
        }

        //this field is used as the source to display all appointments belongin to specifik aircrew 
        private List<Appointment> appointments;
        public List<Appointment> Appointments
        {
            get 
            { 
                return appointments; 
            } 
            set 
            { 
                appointments = value; 
                OnPropertyChanged(nameof(Appointments)); 
            }
        }


        public string SocialSecurityNUmber { get; set; }

        public int AppointmentID { get; set; }
        public string PilotCabinCrew_SSN { get; set; }
        public string AME_SSN { get; set; }
        public string ExaminationName { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateTime AppointmentDate { get; set; }


        public ICommand GetAllInfoCommand { get; set; }
        public ICommand GetBookingsBySSNCommand { get; set; }


        public ProfileViewModel()
        {
            GetAllInfoCommand = new CommandBase(GetAllInfo);
            GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
        }

        private void GetBookingsBySSN(object obj)
        {
            AppointmentRepo appointmentRepo = new AppointmentRepo();

            Appointments = appointmentRepo.GetBySocialSecurityNumber(SocialSecurityNUmber);
        }

        private void GetAllInfo(object obj)
        {
            UserInfo = PilotRepo.GetAirCrewInformation(SocialSecurityNUmber);
        }
    }
}
