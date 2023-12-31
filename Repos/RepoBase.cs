﻿using FlyveLægeKBH.Models;
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

        /// <summary>
        /// Sets parameters for a SQL command based on the provided entity and operation type.
        ///  * The purpose of the SetParameters method is to provide a common interface
        ///  * for setting parameters in a SQL command specific to the type T(the entity type). 
        ///  * This method is expected to be implemented by derived classes, and it's responsible for mapping
        ///  * the properties of the entity to the parameters of a SQL command.*/
        /// </summary>
        /// <param name="command">The SQL command to set parameters for.</param>
        /// <param name="entity">The entity for which parameters are set.</param>
        /// <param name="operationType">The type of database operation being performed.</param>       
        protected abstract void SetParameters(SqlCommand command, T entity, OperationType operationType);

        /// <summary>
        /// Overloading. Sets parameters for a SQL command based on the provided identifier and operation type.
        /// </summary>
        /// <param name="command">The SQL command to set parameters for.</param>
        /// <param name="identifier">The identifier for the database operation.</param>
        /// <param name="operationType">The type of database operation being performed.</param>
        protected abstract void SetParameters(SqlCommand command, string identifier, OperationType operationType);


        /// -----------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Handles exceptions by logging the error message to the debug output and showing a user-friendly message.
        /// Optionally, logs the exception to a file or a logging framework.
        /// </summary>
        /// <param name="ex">The exception to be handled.</param>
        /// <param name="customMessage">An optional custom error message. If provided, it will be used instead of the exception message.</param>
        /// -----------------------------------------------------------------------------------------------------------------------/
        protected virtual void HandleException(Exception ex, string customMessage = null)
        {
            Debug.WriteLine($"Error: {customMessage ?? ex.Message}");

            // Display a user-friendly error message
            MessageBox.Show($"Der er sket en fejl: {customMessage ?? ex.Message}");
        }


        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <param name="storedProcedure">The stored procedure to execute for the create operation.</param>
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
                HandleException(ex);

            }
        }

        /// <summary>
        /// Updates an existing user entity in the database.
        /// </summary>
        /// <param name="airCrew">The aircrew entity to be updated.</param>
        /// <returns>A string message indicating the result of the update operation.</returns>
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
                    HandleException(ex,message);
                }
                return message;
            }
            return message;
        }

        /// <summary>
        /// Deletes an entity from the database based on its identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the entity to be deleted.</param>
        /// <param name="storedProcedure">The stored procedure to execute for the delete operation.</param>
        /// <param name="operationType">The type of database operation being performed.</param>
        public virtual void Delete(string identifier, string storedProcedure, OperationType operationType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetParameters(command, identifier, operationType);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Brugeren kunne ikek slettes.");
            }
        }
        public virtual void Get() { }       
    }
}
