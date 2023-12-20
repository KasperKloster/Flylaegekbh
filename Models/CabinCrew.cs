using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    /// <summary>
    /// Represents a cabin crew member with additional medical report information.
    /// </summary>
    public class CabinCrew: User
    {
        //---------------------Fields--------------------------------------------------------------
        //Rest of the fields are inherited from from AirCrew Base class
        
        public string UserTitle { get; set; } = Title.Titles[2];
        public MedicalReport Medical_Report { get; set; } = new MedicalReport();

        //----------------------Constructor-----------------------------------------------------------
        public CabinCrew()
        {
            
        }
        public CabinCrew(string firstNames, string surName, string email, string phone, string ssn, DateTime dateOfIssue, DateTime cabinCrewExpiryDate) 
        {
            //Title = Title.CabinCrew;
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            SocialSecurityNumber = ssn;
            Medical_Report = new MedicalReport(dateOfIssue, cabinCrewExpiryDate); //instantiation and setting of MR
        }

        public CabinCrew(string firstNames, string surName, string email, string phone, string address, string ssn)
        {
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            Address = address;
            SetSSN(ssn);
        }

        //--------------------Methods------------------------------------------------------------------
        public void SetSSN(string ssn)
        {
            SocialSecurityNumber = ssn;
        }
        public override string ToString()
        {
            return $"{FirstName} {SurName} {UserTitle} {Email} {Phone} {SocialSecurityNumber} Medical_Report Date of Issue: {Medical_Report.DateOfIssue}";
        }

    }
}
