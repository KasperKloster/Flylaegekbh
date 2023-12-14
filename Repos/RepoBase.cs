using FlyveLægeKBH.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FlyveLægeKBH.Repos
{
    public abstract class RepoBase <T>
    {

        //public string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
        public string connectionString = "Server = 10.56.8.36; Database = DB_F23_TEAM_02; User ID = DB_F23_TEAM_02; Password = TEAMDB_DB_02; TrustServerCertificate = true;";

        public enum OperationType
        {
            Create,            
            Delete,            
            Update,
            Get
        }

        /*The purpose of the SetParameters method is to provide a common interface
         * for setting parameters in a SQL command specific to the type T (the entity type). 
         * This method is expected to be implemented by derived classes, and it's responsible for mapping
         * the properties of the entity to the parameters of a SQL command.*/
                
        protected abstract void SetParameters(SqlCommand command, T entity, OperationType operationType);

        protected abstract void SetParameters(SqlCommand command, string identifier, OperationType operationType);




        public virtual void Create(T entity, string storedProcedure)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();

                    using(SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetParameters(command, entity, OperationType.Create);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");

            }
        }

        public virtual string Update(IUser airCrew)
        {
            string message = "";
            if (airCrew is Pilot || airCrew is CabinCrew)
            {
                Pilot pilot = (Pilot)airCrew;


                try
                {

                    //string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("FL2_UpdateAirCrewUser", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@FirstNames", pilot.FirstName);
                            command.Parameters.AddWithValue("@SurName", pilot.SurName);
                            command.Parameters.AddWithValue("@Email", pilot.Email);
                            command.Parameters.AddWithValue("@Phone", pilot.Phone);
                            command.Parameters.AddWithValue("@Address", pilot.Address);
                            command.Parameters.AddWithValue("@SSN", pilot.SocialSecurityNumber);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Update successful
                                message = "Brugeren blev opdateret";
                            }
                            else
                            {
                                message = "Brugeren blev ikke fundet. Intet er opdateret";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = $"Noget gik galt med forbindelsen: {ex}";
                }
                return message;
            }
            return message;
        }

        public virtual void Delete(string identifier, string storedProcedure, OperationType operationType)
        {
            try 
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using(SqlCommand command = new SqlCommand(storedProcedure, connection)) 
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        //command.Parameters.Add("@Identifier", SqlDbType.NVarChar).Value = identifier;
                        SetParameters(command, identifier, operationType);
                        command.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public virtual void Get() { }


        //
        //// FOR DEVELOPMENT: Simulates the logged in user
        //public virtual Dictionary<string, string> GetFirstUser() {
        //    Dictionary<string, string> FirsUser = new Dictionary<string, string>();
        //    try {
        //        string connectionString = ConfigurationManager.ConnectionStrings["MyKey"].ConnectionString;

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM FL2_User", connection))
        //            {
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        // Retrieve data from the reader
        //                        FirsUser.Add("socialSecurityNumber", reader["SocialSecurityNumber"].ToString());
        //                        FirsUser.Add("firstName", reader["FirstNames"].ToString());
        //                        FirsUser.Add("surName", reader["SurName"].ToString());
        //                        FirsUser.Add("email", reader["Email"].ToString());
        //                        FirsUser.Add("phone", reader["Phone"].ToString());
        //                        FirsUser.Add("address", reader["Address"].ToString());
        //                        FirsUser.Add("title", reader["TitleName"].ToString());
        //                    }
        //                }
        //            }
        //        }
        //    } 
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }

        //    return FirsUser;
        //}

    }
}
