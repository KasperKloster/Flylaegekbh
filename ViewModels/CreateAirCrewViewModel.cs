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


        public ICommand CreateAirCrewCommand { get; set; }

        public CreateAirCrewViewModel()
        {
            CreateAirCrewCommand = new CommandBase(CreateAirCrew);
        }

        private void CreateAirCrew(object obj)
        {
            PilotRepo pilotRepo = new PilotRepo();
            if (Title == "CabinCrew")
            {
                MessageBox.Show(cabinCrewRepo.CreateCabinCrew(FirstNames, SurName, Email, Phone, Address, SocialSecurityNumber, Title, MR_DateOfIssue, MR_CabinCrewExpiryDate));
            }
            else
            {
                MessageBox.Show(pilotRepo.CreatePilot(FirstNames, SurName, Email, Phone,Address, SocialSecurityNumber, Title, ML_CertificateNumber, ML_DateOfIssue, ML_Class1SinglePilotExpiryDate,
                    ML_Class1ExpiryDate, ML_Class2ExpiryDate, ML_LAPLExpiryDate, ML_ElectroCardiogramRecentDate, ML_AudiogramRecentDate));
            }
            //MessageBox.Show(ML_DateOfIssue.ToString());
        }
    }
}
