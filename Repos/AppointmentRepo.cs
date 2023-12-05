﻿using FlyveLægeKBH.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlyveLægeKBH.Repos
{
    public class AppointmentRepo : RepoBase
    {

        public string Create(string pilotCabinCrewSSN, string ameSSN, string examinationName, TimeSpan startTime, DateTime appointmentDate)
        {
            string message;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("CreateAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@PilotCabinCrew_SSN", pilotCabinCrewSSN);
                    command.Parameters.AddWithValue("@AME_SSN", ameSSN);
                    command.Parameters.AddWithValue("@ExaminationName", examinationName);
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        message = "Appointment created successfully.";
                    }
                    catch (Exception ex)
                    {
                        message = "Error creating appointment: " + ex.Message;
                    }
                }
            }
            return message;
        }

        public List<Appointment> GetBySocialSecurityNumber(string ssn)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetBookingsBySSN", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    command.Parameters.AddWithValue("@InputSSN", ssn);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                Appointment appointment = new Appointment
                                {
                                    AppointmentID = (int)reader["AppointmentID"],
                                    PilotCabinCrew_SSN = reader["PilotCabinCrew_SSN"].ToString(),
                                    AME_SSN = reader["AME_SSN"].ToString(),
                                    ExaminationName = reader["ExaminationName"].ToString(),
                                    StartTime = (TimeSpan)reader["StartTime"],
                                    AppointmentDate = (DateTime)reader["AppointmentDate"]
                                };

                                appointments.Add(appointment);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception, e.g., log it
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return appointments;
        }

        /*************************************************************/
        /*          Explanation of Delete Appointment                */
        /*************************************************************/
        /*  The method is designed to delete an appointment from the DB
        on the provided "AppointmentID".
        
        The method executes a stored procedure named 
        "FL2_DeleAppointmentByID" in the databse. This stored procedure
        is handling the actual deletion logic.
        
        The "try-catch" block captures any exception that may occur 
        during the execution of the stored procedure. If an error 
        occurs, an error message is generated and returnd.

            Result meddage - Depending on the succes or failure
        of the deletion operation, the method reutrns a secriptive message
        indicating the outcome. If successful, the message confirms the 
        successful deletion. otherwise, it provides details of the 
        encountered error.            
                                                                     */
        /*************************************************************/

        
        public string DeleteAppointment(int appointmentID) 
        {
            string message;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("FL2_DeleteAppointmentByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AppointmentID", appointmentID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        message = "Appointment was deleted successfully.";
                    }
                    catch (Exception ex)
                    {
                        message = "Error creating appointment: " + ex.Message;
                    }
                }

            }

            return message;
        
        }

    }


}
