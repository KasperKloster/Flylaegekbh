﻿using FlyveLægeKBH.Models;
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


        public static string CreatePilot(string firstName, string surName, string email, string phone, string address,
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

                    using (SqlCommand command = new SqlCommand("FL_2InsertAirCrewAndMedicalLicense", connection))
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


        public static string GetAirCrewInformation(string socialSecurityNumber)
        {
            CabinCrew cb = new CabinCrew();
            Pilot p = new Pilot();
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("FL2_GetAirCrewInformation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter
                        command.Parameters.AddWithValue("@SocialSecurityNumber", socialSecurityNumber);

                        // Execute the stored procedure
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                // Retrieve data from the reader
                                string result = $"cpr nummer: {reader["SocialSecurityNumber"]}\nfulde navn: {reader["FirstNames"]} {reader["SurName"]}\n" +
                                                $"email: {reader["Email"]}\ntelefon nummer: {reader["Phone"]}\nadresse: {reader["Address"]}\n" +
                                                $"title: {reader["TitleName"]}\n" +
                                                $"certificate nummer: {reader["CertificateNumber"]}\n udstedelsesdato. {reader["DateOfIssue"]} \n" +
                                                $"klasse 1 single pilot udløbsdato: {reader["Class1SinglePilotExpiryDate"]} \nklasse 1 udløbsdato: {reader["Class1ExpiryDate"]} \n" +
                                                $"klasse 1 udløbsdato: {reader["Class2ExpiryDate"]} \nLAPL 1 udløbsdato: {reader["LAPLExpiryDate"]} \n" +
                                                $"Electro kardiogram: {reader["ElectroCardiogramRecentDate"]} \nAudiogram: {reader["AudiogramRecentDate"]} \n" +
                                                $"MR nummer: {reader["MRID"]} \n MR ustedelsesdato: {reader["MedicalReportDateOfIssue"]} \n MR udløbsdato: {reader["CabinCrewExpiryDate"]}";

                                return result;
                            }
                            else
                            {
                                return "AirCrew member not found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        

        public static string DeletePilot(string socialSecurityNumber)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

            string deleteQueryMedicalLicense = "DELETE FROM [FL2_MedicalLicense] WHERE [SocialSecurityNumber] = @socialSecurityNumber";

            string deleteQueryCabinCrew = "DELETE FROM [FL2_User] WHERE [SocialSecurityNumber] = @socialSecurityNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(deleteQueryMedicalLicense, connection))
                    {
                        cmd.Parameters.Add("@socialSecurityNumber", System.Data.SqlDbType.NVarChar).Value = socialSecurityNumber;
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(deleteQueryCabinCrew, connection))
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
