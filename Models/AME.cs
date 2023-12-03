using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class AME : User
    {
        //---------------------Fields--------------------------------------------------------------        
        public string UserTitle { get; private set; } = Title.Titles[0];
        public AuthorizationLevelEnum AuthorizationLevel { get; private set; }

        //---------------------Constructor--------------------------------------------------------------        
        public AME(AuthorizationLevelEnum authorizationLevel) {
            AuthorizationLevel = authorizationLevel;
        }
    }
}
