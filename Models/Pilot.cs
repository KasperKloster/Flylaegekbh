using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    /// <summary>
    /// Represents a pilot with additional information related to aviation.
    /// </summary>
    public class Pilot: User, IUser
    {

        //---------------------Fields--------------------------------------------------------------
        //Rest of the fields are inherited from from AirCrew Base class
        public string UserTitle { get; set; } = Title.Titles[1];
        public MedicalLicense MedicalLicense { get; set; }

        //----------------------Constructor-----------------------------------------------------------
        public Pilot()
        {
            
        }
        public Pilot(string firstNames, string surName, string email, string phone, string ssn, DateTime dateOfIssue, 
            DateTime class1SinglePilotExpiryDate, DateTime class1ExpiryDate, DateTime class2ExpiryDate, 
            DateTime laplExpiryDate, DateTime electroCardiogramRecentDate, DateTime audiogramRecentDate)
        {
           // Title = Title.Pilot;
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            SocialSecurityNumber = ssn;
            //Instantiation and setting of MR
            MedicalLicense = new MedicalLicense(dateOfIssue, class1SinglePilotExpiryDate, class1ExpiryDate, class2ExpiryDate, laplExpiryDate, electroCardiogramRecentDate, audiogramRecentDate);
        }

        public Pilot(string firstNames, string surName, string email, string phone, string address, string ssn)
        {
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            Address = address;
            SocialSecurityNumber = ssn;
        }
    }
}
