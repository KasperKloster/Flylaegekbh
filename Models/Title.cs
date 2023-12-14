using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    /// <summary>
    /// Represents a title with predefined values.
    /// </summary>
    public class Title
    {
        /// <summary>
        /// Gets or sets an array of predefined titles.
        /// </summary>
        public static string[] Titles { get; set; } = new string[]
        {
        "AME", "Pilot", "CabinCrew"
        };

        /// <summary>
        /// Adds a new title to the predefined titles.
        /// </summary>
        /// <param name="name">The name of the title to add.</param>
        public static void AddToTitles(string name)
        {
            Titles.Append(name);
        }
    }
}
