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
       CabinCrews, as they both implements this interface
    
     
     Using an interface makes it easier to have a lose coupled 
     and high cohesion through out the system and alle the classes 
     that impolements it. this is a good way to future proof the app */
    //***********************************************************//

    //**************************************************************************//
    /// <summary>
    /// Interface: IUser
    /// 
    /// - This interface is currently used for creating a list that contains different types of classes.
    /// 
    /// - It facilitates loose coupling and high cohesion by providing a common interface for classes that implement it.
    /// 
    /// - The interface is implemented by classes such as 'Pilot' and 'CabinCrew' in the 'AirCrewViewModel'.
    /// 
    /// - This approach enhances flexibility and future-proofing of the application.
    /// </summary>
    //**************************************************************************//

    public interface IUser
    {
        public string Tostring2();
    }
}
