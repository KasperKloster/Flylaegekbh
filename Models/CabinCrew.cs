using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class CabinCrew: AirCrew
    {
        //---------------------Fields--------------------------------------------------------------
        //Rest of the fields are inherited from from AirCrew Base class
        public Titles Title { get; private set; }
        public MedicalReport MedicalReport { get; set; }

        //----------------------Constructor-----------------------------------------------------------
        public CabinCrew(string firstNames, string surName, string email, string phone, string ssn, DateTime dateOfIssue, DateTime cabinCrewExpiryDate) 
        {
            Title = Titles.CabinCrew;
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            SocialSecurityNumber = ssn;
            MedicalReport = new MedicalReport(dateOfIssue, cabinCrewExpiryDate); //instantiation and setting of MR
        }

        public CabinCrew(string firstNames, string surName, string email, string phone, string address, string ssn)
        {
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            Address = address;
            SocialSecurityNumber = ssn;
        }

        //--------------------Methods------------------------------------------------------------------
        public override string ToString()
        {
            return $"{FirstName} {SurName} {Title} {Email} {Phone} {SocialSecurityNumber} MedicalReport Date of Issue: {MedicalReport.DateOfIssue}";

        }

    }
}
