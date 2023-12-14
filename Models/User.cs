using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Models
{
    /// <summary>
    /// Represents a user with basic information.
    /// </summary>
    public abstract class User : IUser
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the social security number of the user.
        /// </summary>
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Returns a string representation of the user.
        /// </summary>
        /// <returns>A string representing the user.</returns>
        public string Tostring2()
        {
            return $"{FirstName} {SurName} {Email} {Phone} {Address} {SocialSecurityNumber}";
        }
    }
}
