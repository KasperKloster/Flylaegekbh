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
    public class AppointmentViewModel: ViewModelBase
    {
        //----------Fields----------------------------


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


        public string SocialSecurityNumber { get; set; }

        public int AppointmentID { get; set; }
        public string PilotCabinCrew_SSN { get; set; }
        public string AME_SSN { get; set; }
        public string ExaminationName { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime AppointmentDate { get; set; }


        public ICommand GetBookingsBySSNCommand { get; set; }


        public AppointmentViewModel()
        {
            GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
        }

        private void GetBookingsBySSN(object obj)
        {
            AppointmentRepo appointmentRepo = new AppointmentRepo();

            Appointments = appointmentRepo.GetBySocialSecurityNumber(SocialSecurityNumber);
        }

    }
}
