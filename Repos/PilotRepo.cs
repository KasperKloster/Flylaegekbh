using FlyveLægeKBH.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FlyveLægeKBH.Repos
{
    public class PilotRepo: RepoBase
    {


        //--------------------Methods------------------------------------------------------------------


        public static string CreatePilot(string firstName,string surName, string email, string phone,
        string socialSecurityNumber, string title, string certificateNumber, DateTime dateOfIssue, DateTime class1SinglePilotExpiryDate, 
        DateTime class1ExpiryDate, DateTime class2ExpiryDate, DateTime laplExpiryDate, DateTime electroCardiogramRecentDate, DateTime audiogramRecentDate)
        {
            //string connectionString = "Data Source=(localdb)\\localtest;Initial Catalog=flyvelægeKBH;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertAirCrewAndMedicalLicense", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@SurName", surName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@SocialSecurityNumber", socialSecurityNumber);
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@CertificateNumber", certificateNumber);
                        command.Parameters.AddWithValue("@DateOfIssue", dateOfIssue);
                        command.Parameters.AddWithValue("@Class1SinglePilotExpiryDate", class1SinglePilotExpiryDate);
                        command.Parameters.AddWithValue("@Class1ExpiryDate", class1ExpiryDate);
                        command.Parameters.AddWithValue("@Class2ExpiryDate", class2ExpiryDate);
                        command.Parameters.AddWithValue("@LAPLExpiryDate", laplExpiryDate);
                        command.Parameters.AddWithValue("@ElectroCardiogramRecentDate", electroCardiogramRecentDate);
                        command.Parameters.AddWithValue("@AudiogramRecentDate", audiogramRecentDate);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }
                return "Pilot created successfully";
            }
            catch (Exception ex) 
            {
                return $"Error: {ex.Message}";
            }

        }
    }
}
