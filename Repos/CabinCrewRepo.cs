using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyveLægeKBH.Repos
{

    public class CabinCrewRepo : RepoBase

    {
        //--------------------Methods------------------------------------------------------------------


        // for the time being, this method is static, so that we can acces it and do test without making an instance of the whole class. This might change later on.

        public static string CreateCabinCrew(string firstName, string surName, string email, string phone,
        string socialSecurityNumber, string title, DateTime dateOfIssue, DateTime cabinCrewExpiryDate)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertAirCrewAndMedicalReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@SurName", surName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@SocialSecurityNumber", socialSecurityNumber);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@DateOfIssue", dateOfIssue);
                        command.Parameters.AddWithValue("@CabinCrewExpiryDate", cabinCrewExpiryDate);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }

                return "Added Cabin Crew successfully";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string DeleteCabinCrew(string socialSecurityNumber)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
            string deleteQueryMedicalReport = "DELETE FROM [FL_MedicalReport] WHERE [SocialSecurityNumber] = @socialSecurityNumber";
            string deleteQueryCabinCrew = "DELETE FROM [FL_AirCrew] WHERE [SocialSecurityNumber] = @socialSecurityNumber";

            using(SqlConnection connection = new SqlConnection( connectionString)) 
            {
                try
                {
                    connection.Open();

                    using(SqlCommand cmd = new SqlCommand(deleteQueryMedicalReport, connection)) 
                    {
                        cmd.Parameters.Add("@socialSecurityNumber", System.Data.SqlDbType.NVarChar).Value = socialSecurityNumber;
                        cmd.ExecuteNonQuery();
                    }

                    using(SqlCommand cmd = new SqlCommand(deleteQueryCabinCrew, connection))
                    {
                        cmd.Parameters.Add("@socialSecurityNumber", System.Data.SqlDbType.NVarChar).Value = socialSecurityNumber;
                        cmd.ExecuteNonQuery();
                    }

                    return $"Cabin Crew with ssn: {socialSecurityNumber} has been deleted";

                }
                catch (Exception ex)
                {

                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}
