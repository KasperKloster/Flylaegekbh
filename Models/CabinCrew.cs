﻿using System;
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
        public MedicalReport Medical_Report { get; set; } = new MedicalReport();

        //----------------------Constructor-----------------------------------------------------------
        public CabinCrew()
        {
            
        }
        public CabinCrew(string firstNames, string surName, string email, string phone, string ssn, DateTime dateOfIssue, DateTime cabinCrewExpiryDate) 
        {
            Title = Titles.CabinCrew;
            FirstName = firstNames;
            SurName = surName;
            Email = email;
            Phone = phone;
            SocialSecurityNumber = ssn;
            Medical_Report = new MedicalReport(dateOfIssue, cabinCrewExpiryDate); //instantiation and setting of MR
        }

        //--------------------Methods------------------------------------------------------------------
        public override string ToString()
        {
            return $"{FirstName} {SurName} {Title} {Email} {Phone} {SocialSecurityNumber} Medical_Report Date of Issue: {Medical_Report.DateOfIssue}";

        }

    }
}
