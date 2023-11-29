using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class MedicalLicense
    {
        //-----------Fields----------------------------------
        public string CertificateNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime Class1SinglePilotExpiryDate { get; set; }
        public DateTime Class1ExpiryDate { get; set; }
        public DateTime Class2ExpiryDate { get; set; }
        public DateTime LAPLExpiryDate { get; set; }
        public DateTime ElectroCardiogramRecentDate { get; set; }
        public DateTime AudiogramRecentDate { get; set; }

        //------------constructor________________________
        public MedicalLicense(DateTime dateOfIssue, DateTime class1SinglePilotExpiryDate, DateTime class1ExpiryDate, 
            DateTime class2ExpiryDate, DateTime laplExpiryDate, DateTime electroCardiogramRecentDate, DateTime audiogramRecentDate)
        {
            DateOfIssue = dateOfIssue;
            Class1SinglePilotExpiryDate = class1SinglePilotExpiryDate;
            Class1ExpiryDate = class1ExpiryDate;
            Class2ExpiryDate = class2ExpiryDate;
            LAPLExpiryDate = laplExpiryDate;
            ElectroCardiogramRecentDate = electroCardiogramRecentDate;
            AudiogramRecentDate = audiogramRecentDate;
            CertificateNumber = "C1234";
        }
    }
}
