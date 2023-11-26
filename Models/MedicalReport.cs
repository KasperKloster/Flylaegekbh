using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class MedicalReport
    {
        public DateTime DateOfIssue { get; private set; }
        public DateTime CabinCrewExpiryDate { get; private set; }

        public MedicalReport(DateTime dateOfIssue = default, DateTime cabinCrewExpiryDate= default)
        {
            DateOfIssue = dateOfIssue;
            CabinCrewExpiryDate = cabinCrewExpiryDate;
            
        }
    }
}
