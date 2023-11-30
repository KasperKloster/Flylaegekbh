using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels
{
    public class CreateAirCrewViewModel: ViewModelBase
    {
        //------------Fields--------------------------------------------------------

        
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string SocialSecurityNumber { get; set; }

        //public Titles Title { get; set; }

        public string Title { get; set; }

        public string ML_CertificateNumber { get; set; }
        public DateTime ML_DateOfIssue { get; set; }= DateTime.Now;
        public DateTime ML_Class1SinglePilotExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_Class1ExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_Class2ExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_LAPLExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_ElectroCardiogramRecentDate { get; set; } = DateTime.Now;
        public DateTime ML_AudiogramRecentDate { get; set; } = DateTime.Now;

        public DateTime MR_DateOfIssue { get; set; } = DateTime.Now;
        public DateTime MR_CabinCrewExpiryDate { get; set; } = DateTime.Now;

        public ICommand CreateAirCrewCommand { get; set; }

        public CreateAirCrewViewModel()
        {
            CreateAirCrewCommand = new CommandBase(CreateAirCrew);
        }

        private void CreateAirCrew(object obj)
        {
            if (Title == "CabinCrew")
            {
                //MessageBox.Show(CabinCrewRepo.CreateCabinCrew(FirstName = FirstName, SurName = SurName, Email = Email, Phone = Phone, SocialSecurityNumber = SocialSecurityNumber, Title = Title, MR_DateOfIssue = MR_DateOfIssue, MR_CabinCrewExpiryDate = MR_CabinCrewExpiryDate));
                MessageBox.Show(CabinCrewRepo.CreateCabinCrew(FirstName, SurName, Email, Phone, SocialSecurityNumber, Title, MR_DateOfIssue, MR_CabinCrewExpiryDate));

            }
            else
            {
                MessageBox.Show(PilotRepo.CreatePilot(FirstName, SurName, Email, Phone, SocialSecurityNumber, Title, ML_CertificateNumber, ML_DateOfIssue, ML_Class1SinglePilotExpiryDate,
                    ML_Class1ExpiryDate, ML_Class2ExpiryDate, ML_LAPLExpiryDate, ML_ElectroCardiogramRecentDate, ML_AudiogramRecentDate));
            }
            //MessageBox.Show(ML_DateOfIssue.ToString());
        }
    }
}
