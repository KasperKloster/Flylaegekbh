using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FlyveLægeKBH.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string PilotCabinCrew_SSN { get; set; }
        public string AME_SSN { get; set; }
        public string ExaminationName { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Appointment()
        {
            
        }
    }
    


}