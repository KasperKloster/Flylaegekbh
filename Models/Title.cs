using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    public class Title
    {
        public static string[] Titles { get; set; } = new string[]
        {
            "AME", "Pilot", "CabinCrew"
        };

        //public static List<string> Titles { get; set; }=new List<string>();

        
        //public Title() {
   
        //    Titles.Add("AME");
        //    Titles.Add("Pilot");
        //    Titles.Add("CabinCrew");
        //}

        public static void AddToTitles(string name)
        {
            Titles.Append(name);
        }
        
    }
}
