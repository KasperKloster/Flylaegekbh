using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    // jeg har fjernet abstract.. dette er gjort for at se om jeg kan få selectedPilot/User  til at at være en user istedet for en Pilot
    public class User: IUser
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string SocialSecurityNumber { get; set; }

        public string Tostring2()
        {
            throw new NotImplementedException();
        }
    }
}
