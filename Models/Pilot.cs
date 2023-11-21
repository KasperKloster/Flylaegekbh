using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class Pilot: AirCrew
    {

        //---------------------Fields--------------------------------------------------------------
        //Rest of the fields are inherited from from AirCrew Base class
        public Titles Title { get; private set; }
        public MedicalLicense MedicalLicense { get; set; }

        //----------------------Constructor-----------------------------------------------------------
        public Pilot(string firstNames, string surName, string email, string phone, string ssn, DateTime dateOfIssue, 
            DateTime class1SinglePilotExpiryDate, DateTime class1ExpiryDate, DateTime class2ExpiryDate, 
            DateTime laplExpiryDate, DateTime electroCardiogramRecentDate, DateTime audiogramRecentDate)
        {
            Title = Titles.Pilot;
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            SocialSecurityNumber = ssn;
            //Instantiation and setting of MR
            MedicalLicense = new MedicalLicense(dateOfIssue, class1SinglePilotExpiryDate, class1ExpiryDate, class2ExpiryDate, laplExpiryDate, electroCardiogramRecentDate, audiogramRecentDate);


        }

        //--------------------Methods------------------------------------------------------------------
        public override string ToString() 
        {
            return $"{FirstName} {SurName} {Title} {Email} {Phone} {SocialSecurityNumber} Medical Date of Issue: {MedicalLicense.DateOfIssue}";
          
        }

    }
}
