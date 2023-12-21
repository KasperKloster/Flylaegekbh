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
    //*****************************************************************************//
    /// <summary>
    /// ViewModel for creating aircrew entities, including pilots and cabin crew.
    /// </summary>
    //****************************************************************************//
    public class CreateAirCrewViewModel : ViewModelBase
    {
        //------------Fields--------------------------------------------------------//
        public string? FirstNames { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? SocialSecurityNumber { get; set; }

        //public Titles Title { get; set; }

        public string? Title { get; set; }
        public string? ML_CertificateNumber { get; set; }
        public DateTime ML_DateOfIssue { get; set; } = DateTime.Now;
        public DateTime ML_Class1SinglePilotExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_Class1ExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_Class2ExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_LAPLExpiryDate { get; set; } = DateTime.Now;
        public DateTime ML_ElectroCardiogramRecentDate { get; set; } = DateTime.Now;
        public DateTime ML_AudiogramRecentDate { get; set; } = DateTime.Now;

        public DateTime MR_DateOfIssue { get; set; } = DateTime.Now;
        public DateTime MR_CabinCrewExpiryDate { get; set; } = DateTime.Now;

        public ICommand CreateAirCrewCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAirCrewViewModel"/> class.
        /// </summary>
        public CreateAirCrewViewModel()
        {
            CreateAirCrewCommand = new CommandBase(CreateAirCrew);
        }

        //------------------------------------------Methods------------------------------------------------------//

        /// ------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Creates an aircrew entity based on the provided information and adds it to the database.
        /// </summary>
        /// <remarks>
        /// This method determines whether the aircrew is a pilot or cabin crew based on the specified title.
        /// It then calls the appropriate repository method to add the aircrew to the database.
        /// </remarks>
        /// <param name="obj">An optional parameter.</param>
        /// ------------------------------------------------------------------------------------------------------/

        private void CreateAirCrew(object obj)
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
                        Title, ML_CertificateNumber, ML_DateOfIssue, ML_Class1SinglePilotExpiryDate, ML_Class1ExpiryDate,
                        ML_Class2ExpiryDate, ML_LAPLExpiryDate, ML_ElectroCardiogramRecentDate, ML_AudiogramRecentDate));
                }

            

        }
    }
}
