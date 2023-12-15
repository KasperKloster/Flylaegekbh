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
        
        //------------------------------------------Menthos------------------------------------------------//


        /// <summary>
        /// Creates an aircrew entity based on the provided information and adds it to the database.
        /// </summary>
        /// <remarks>
        /// This method determines whether the aircrew is a pilot or cabin crew based on the specified title.
        /// It then calls the appropriate repository method to add the aircrew to the database.
        /// </remarks>
        private void CreateAirCrew(object obj)
        {
            try
            {
                // Check the title to determine if it's a CabinCrew or a Pilot
                if (Title == "CabinCrew")
                {
                    // Create and add a CabinCrew entity to the database
                    MessageBox.Show(cabinCrewRepo.CreateCabinCrew(FirstNames, SurName, Email, Phone, Address, 
                        SocialSecurityNumber, Title, MR_DateOfIssue, MR_CabinCrewExpiryDate));
                }
                else
                {
                    // Create and add a Pilot entity to the database
                    MessageBox.Show(pilotRepo.CreatePilot(FirstNames, SurName, Email, Phone, Address, SocialSecurityNumber, 
                        Title, ML_CertificateNumber, ML_DateOfIssue, ML_Class1SinglePilotExpiryDate,ML_Class1ExpiryDate, 
                        ML_Class2ExpiryDate, ML_LAPLExpiryDate, ML_ElectroCardiogramRecentDate, ML_AudiogramRecentDate));
                }
                //MessageBox.Show(ML_DateOfIssue.ToString());
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the aircrew creation process
                HandleException(ex, "Der er desværre sket en fejl. Personen blev ikke oprettet.");
            }
        }
    }
}
