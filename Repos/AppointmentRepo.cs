using FlyveLægeKBH.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

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
                using (SqlCommand command = new SqlCommand("FL2_GetBookingsBySSN", connection))
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
                        MessageBox.Show($"Error: {ex.Message}");
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

            Result message - Depending on the succes or failure
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

        /*************************************************************/
        /*          Explanation of Update Appointment                */
        /*************************************************************/
        /*  The purpose of this method is to update an existing 
            appointment in the database based on the provided 
            "Appointment" object.

            The method initiates a stored procedure named 
            "FL2_UpdateAppointment" in the database, which contains the 
            logic for handling the update operation.

            Parameters:
            @AppointmentID: The unique identifier of the appointment 
              to be updated.
            @PilotCabinCrew_SSN: The Social Security Number of the 
              pilot or cabin crew associated with the appointment.
            @AME_SSN: The Social Security Number of the Aviation 
              Medical Examiner (AME) responsible for the appointment.
            @ExaminationName: The name of the examination associated 
              with the appointment.
            @StartTime: The start time of the appointment.
            @AppointmentDate: The date of the appointment.

            Try-Catch Block:
            The method is wrapped in a try-catch block to handle any 
              exceptions that may occur during the execution of the 
              stored procedure.
            If the update operation is successful, the method sets 
              the message to "Appointment updated successfully."
            If an error occurs, the catch block captures the 
              exception, and the message is set to "Error updating 
              appointment: " followed by the details of the exception.
            
            The method returns a string message indicating the outcome 
              of the update operation. If successful, it confirms the 
              update; otherwise, it provides details of the encountered 
              error.

        /*************************************************************/

        public string UpdateAppointment(Appointment appointment)
        {
            string message;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("FL2_UpdateAppointment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@AppointmentID", appointment.AppointmentID);
                    command.Parameters.AddWithValue("@PilotCabinCrew_SSN", appointment.PilotCabinCrew_SSN);
                    command.Parameters.AddWithValue("@AME_SSN", appointment.AME_SSN);
                    command.Parameters.AddWithValue("@ExaminationName", appointment.ExaminationName);
                    command.Parameters.AddWithValue("@StartTime", appointment.StartTime);
                    command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        message = "Appointment updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        message = "Error updating appointment: " + ex.Message;
                    }
                }
            }
            return message;
        }

        public List<AME> GetAuthorizedAMEsByExamination (string examinationName)
        {
            List<AME> authorizedAMEs = new List<AME>();

            using (SqlConnection connection = new SqlConnection(connectionString ))
            {
                using ( SqlCommand command = new SqlCommand("FL2_GetAuthoriazedAMEByExamination", connection) ) 
                {
                    command.CommandType= CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ExaminationName", examinationName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read())                                
                            {
                                AME ame = new AME();
                                {
                                    ame.FirstName = reader["FirstNames"].ToString();
                                    ame.SurName = reader["Surname"].ToString();
                                    ame.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();
                                }
                                
                                authorizedAMEs.Add(ame);

                            }
                        }

                        if (authorizedAMEs.Count == 0)
                        {
                            MessageBox.Show("Der er ingen godkendte AME'er for den valgte undersøgelse. Prøv en anden undersøgelse");
                        }
                        else
                        {
                            MessageBox.Show("Godkendte AME'er blev succesfuldt indlæst for den angivne undersøgelse. Vælg en AME i Dropdown menuen");
                        }
                    }
                    catch 
                    (Exception ex) 
                    {
                        MessageBox.Show($"Der skete en fejl under indhentningen af authorizedAMEs. Error: {ex.Message}");
                    }
                }
            }

            return authorizedAMEs;
        }

        public (List<Pilot> pilots, List<CabinCrew> cabinCrews) GetAllPilotsAndCabinCrews()
        {
            List<Pilot> pilots = new List<Pilot>();
            List<CabinCrew> cabinCrews = new List<CabinCrew>();

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                using(SqlCommand command = new SqlCommand("FL2_GetALLPilotsAndCabinCrew", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try 
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string titleName = reader["TitleName"].ToString();

                                if (titleName.Equals("Pilot", StringComparison.OrdinalIgnoreCase))
                                {
                                    Pilot pilot = new Pilot();
                                    {
                                        pilot.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();
                                        pilot.FirstName = reader["FirstNames"].ToString();
                                    };
                                    pilots.Add(pilot);
                                }
                                else if (titleName.Equals("CabinCrew", StringComparison.OrdinalIgnoreCase))
                                {
                                    CabinCrew cabinCrew = new CabinCrew();
                                    {
                                        cabinCrew.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();
                                        cabinCrew.FirstName = reader["FirstNames"].ToString();
                                    };
                                    cabinCrews.Add(cabinCrew);
                                }
                            }

                            MessageBox.Show("Pilots og Cabin Crews  blev indlæst, du kan nu vælge fra en af de to dropdown menuer.");
                        }                    
                    }
                    catch(Exception ex) 
                    {
                        MessageBox.Show($"Der skete en fejl under indhentningen af GetAllPilotsAndCabinCrews. Error: {ex.Message}");
                    }
                   
                }
            
            }
            return (pilots, cabinCrews);
        }

        public List<Examination> GetAllExaminations() 
        {
            List<Examination> examinations = new List<Examination>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("FL2_GetAllExaminations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Examination examination = new Examination();
                                {
                                    examination.ExaminationName = (string)reader["ExaminationName"];
                                    examination.Price = (decimal)reader["Price"];
                                    examination.DurationInMin = (int)reader["DurationInMin"];
                                }

                                examinations.Add(examination);

                            }
                        }

                        MessageBox.Show($"Undersøgelser blev indlæst. Du kan nu vælge en undersøgelse i undersøgelses menuen og derefter finde en AME");

                        
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show($"Doprdown menu med ExaminationNames kunne ikke indlæses. Error: {ex.Message}");
                    }
                }
            }
            return examinations;
        }

        public List<string> GetAvailableTimesForAME (string ame_ssn, DateOnly appointmentDate)
        {
            List<string> times = new List<string>();

            using(SqlConnection connection = new SqlConnection( connectionString)) 
            {
                using(SqlCommand command = new SqlCommand("FL2_GetAvailableTimesForAME", connection)) 
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AME_SSN", ame_ssn);
                    command.Parameters.AddWithValue("@AppointmentDate", appointmentDate);

                    try
                    {
                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read()) 
                            {                                
                                string startTimeValue = reader["StartTime"].ToString();
                                times.Add(startTimeValue);  
                            }
                        }

                        MessageBox.Show($"Ledige tider indlæst. Du kan nu vælge en tid i menuen");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Der skete en fejl under indlæsningen af ledige tider. Error: {ex.Message}");
                    }
                }
            }
            return times;
        }
        

    }
}


 




