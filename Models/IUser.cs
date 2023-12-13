using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    //***********************************************************//
    /* For now, this interface is used solely to be able to create
       a list that contains different type of classes, which 
       implements this interface.  Ex. we now have a list in our 
       AirCrewViewModel which is a list containing both pilots and 
       CabinCrews, as they both implements this interface*/
    //***********************************************************//

    public interface IUser
    {
        public string Tostring2();
    }
}
