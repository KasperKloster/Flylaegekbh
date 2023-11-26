using FlyveLægeKBH.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FlyveLægeKBH.Repos
{
    public abstract class RepoBase
    { 
    
        public string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
        public virtual void Create() { }

        public virtual void Update(AirCrew airCrew) {

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateAirCrewUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstNames", airCrew.FirstName); 
                        command.Parameters.AddWithValue("@SurName", airCrew.SurName); 
                        command.Parameters.AddWithValue("@Email", airCrew.Email); 
                        command.Parameters.AddWithValue("@Phone", airCrew.Phone); 
                        command.Parameters.AddWithValue("@Address", airCrew.Address); 
                        command.Parameters.AddWithValue("@SSN", airCrew.SocialSecurityNumber);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update successful
                            Console.WriteLine("Row updated successfully.");
                        }
                        else
                        {
                            // No rows updated (SSNTEST not found, or no changes)
                            Console.WriteLine("No matching rows found or no changes made.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public virtual void Delete() { }
        public virtual void Get() { }

        // FOR DEVELOPMENT: Simulates the logged in user
        public virtual Dictionary<string, string> GetFirstUser() {
            Dictionary<string, string> FirsUser = new Dictionary<string, string>();
            try {
                string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM FL_AirCrew", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Retrieve data from the reader
                                FirsUser.Add("socialSecurityNumber", reader["SocialSecurityNumber"].ToString());
                                FirsUser.Add("firstName", reader["FirstNames"].ToString());
                                FirsUser.Add("surName", reader["SurName"].ToString());
                                FirsUser.Add("email", reader["Email"].ToString());
                                FirsUser.Add("phone", reader["Phone"].ToString());
                                FirsUser.Add("address", reader["Address"].ToString());
                                FirsUser.Add("title", reader["TitleName"].ToString());
                            }
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return FirsUser;
        }

    }
}
