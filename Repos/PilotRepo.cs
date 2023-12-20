using FlyveLægeKBH.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FlyveLægeKBH.Repos
{
    public class PilotRepo: RepoBase<Pilot>
    {
        //**************************************************************************//
        /// <summary>
        /// Explanation of 'PilotRepo' Class:
        /// 
        /// - This class represents the repository for handling operations related to the 'Pilot' entity.
        /// 
        /// - It inherits from 'RepoBase<Pilot>' to leverage the generic repository base class.
        /// 
        /// - The primary purpose is to encapsulate data access logic for the 'Pilot' entity, including CRUD operations
        ///   and specific methods related to pilots.
        /// </summary>
        //**************************************************************************//
             
        //--------------------Methods------------------------------------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// Explanation of 'SetParameters' Method:
        /// 
        /// - This method is an overridden implementation of the abstract method in the base class.
        /// 
        /// - It sets the parameters of a SQL command based on the provided 'Pilot' entity and operation type.
        /// 
        /// - The switch statement determines the operation type, and parameters are added accordingly.
        /// </summary>
        //**************************************************************************//

        protected override void SetParameters(SqlCommand command, Pilot entity, OperationType operationType)
        {                 

            switch (operationType) 
            {
                case OperationType.Delete:                   
                    command.Parameters.AddWithValue("@SocialSecurityNumber", entity.SocialSecurityNumber);                   
                    break;

                case OperationType.Create:
                    command.Parameters.AddWithValue("@SocialSecurityNumber", entity.SocialSecurityNumber);
                    command.Parameters.AddWithValue("@FirstNames", entity.FirstName);
                    command.Parameters.AddWithValue("@SurName", entity.SurName);
                    command.Parameters.AddWithValue("@Email", entity.Email);
                    command.Parameters.AddWithValue("@Phone", entity.Phone);
                    command.Parameters.AddWithValue("@Address", entity.Address);
                    command.Parameters.AddWithValue("@TitleName", entity.UserTitle);
                    break;               
            }
        }

        protected override void SetParameters(SqlCommand command, string identifier, OperationType operationType)
        {
            switch (operationType) 
            {
                case OperationType.Delete:
                    command.Parameters.AddWithValue("@SocialSecurityNumber", identifier);
                    break;
            }
        }

        public string CreatePilot(string firstName, string surName, string email, string phone, string address,
        string socialSecurityNumber, string title, string certificateNumber, DateTime dateOfIssue, DateTime class1SinglePilotExpiryDate,
        DateTime class1ExpiryDate, DateTime class2ExpiryDate, DateTime laplExpiryDate, DateTime electroCardiogramRecentDate, DateTime audiogramRecentDate)
        {
            string toReturn = "";
            connectionString = "Server = 10.56.8.36; Database = DB_F23_TEAM_02; User ID = DB_F23_TEAM_02; Password = TEAMDB_DB_02; TrustServerCertificate = true;";
            try
            {

                //string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("FL2_InsertUserAndMedicalLicense", connection))
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
                toReturn = "Pilot created successfully";
            }
            catch (Exception ex) 
            {
                HandleException(ex);
            }

            return toReturn;
        }

        public string GetAirCrewInformation(string socialSecurityNumber)
        {
            string toReturn = "";
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
                                toReturn = "AirCrew member not found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return toReturn;
        }

        public string DeletePilot( string socialSecurityNumber)
        {
            string toReturn = "";
            try
            {
                string deleteProcedure = "FL2_DeletePilotAndRelatedEntities";
                Delete(socialSecurityNumber, deleteProcedure, OperationType.Delete);

                toReturn = $"Pilot med ssn: {socialSecurityNumber} og relateret data slettet";
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return toReturn;
        }
 

    
    }
}
