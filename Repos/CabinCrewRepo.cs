using FlyveLægeKBH.Models;
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

    public class CabinCrewRepo : RepoBase<CabinCrew>

    {
        //--------------------Methods------------------------------------------------------------------
        protected override void SetParameters(SqlCommand command, CabinCrew entity, OperationType operationType)
        {

            /*Add parameters specific to Delete operation*/


            switch (operationType)
            {
                case OperationType.Delete:
                    command.Parameters.AddWithValue("@SocialSecurityNumber", entity.SocialSecurityNumber);
                    break;
            }
        }

        protected override void SetParameters(SqlCommand command, string identifier, OperationType operationType)
        {
            throw new NotImplementedException();
        }

        // for the time being, this method is static, so that we can acces it and do test without making an instance of the whole class. This might change later on.
        public string CreateCabinCrew(string firstName, string surName, string email, string phone,string address,
        string socialSecurityNumber, string title, DateTime dateOfIssue, DateTime cabinCrewExpiryDate)
        {
            try
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
                string connectionString = "Server = 10.56.8.36; Database = DB_F23_TEAM_02; User ID = DB_F23_TEAM_02; Password = TEAMDB_DB_02; TrustServerCertificate = true;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("FL2_InsertAirCrewAndMedicalReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@FirstNames", firstName);
                        command.Parameters.AddWithValue("@SurName", surName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Address", address);
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
                HandleException(ex);
                return $"Error: {ex.Message}";
            }
        }

        public string DeleteCabinCrew(string socialSecurityNumber)
        {
            string connectionString = "Server = 10.56.8.36; Database = DB_F23_TEAM_02; User ID = DB_F23_TEAM_02; Password = TEAMDB_DB_02; TrustServerCertificate = true;";

            //string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
            string deleteQueryMedicalReport = "DELETE FROM [dbo].[FL2_MedicalReport] WHERE [SocialSecurityNumber] = @socialSecurityNumber";
            string deleteQueryCabinCrew = "DELETE FROM [FL2_User] WHERE [SocialSecurityNumber] = @socialSecurityNumber";

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
                    HandleException(ex);
                    return $"Error: {ex.Message}";
                }
            }
        }
    }
}
