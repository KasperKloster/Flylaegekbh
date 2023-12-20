using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels
{
    public class AppointmentViewModel: ViewModelBase
    {
        //------------------------------------------Fields------------------------------------------------------//

        //**************************************************************************//
        /// <summary>
        /// List Fields Explanation:
        /// 
        /// - Serve the purpose of displaying DB-collected data in a menu.
        /// 
        /// - Allow users to select items from the displayed lists.
        /// </summary>
        //**************************************************************************//

        private List<Appointment> appointments;
        public List<Appointment> Appointments
        {
            get 
            { 
                return appointments; 
            } 
            set 
            { 
                appointments = value; 
                OnPropertyChanged(nameof(Appointments)); 
            }
        }

        private List<AME> authorizedAMEs;
        public List<AME> AuthorizedAMEs
        {
            get
            {
                return authorizedAMEs;
            }
            set
            {
                authorizedAMEs = value;
                OnPropertyChanged(nameof(AuthorizedAMEs));
            }
        }

        private List<Examination> allExaminations;
        public List<Examination> AllExaminations
        {
            get
            {
                return allExaminations;
            }
            set
            {
                allExaminations = value;
                OnPropertyChanged(nameof(AllExaminations));
            }
        }

        private List<CabinCrew>? allCabinCrews;
        public List<CabinCrew> AllCabinCrews
        {
            get
            {
                return allCabinCrews;
            }
            set
            {
                allCabinCrews = value;
                OnPropertyChanged(nameof(AllCabinCrews));
            }
        }

        private List<string> startTime;
        public List<string> StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        private List<Pilot>? allPilots;
        public List<Pilot> AllPilots
        {
            get
            {
                return allPilots;
            }
            set
            {
                allPilots = value;
                OnPropertyChanged(nameof(AllPilots));
            }
        }

        //**************************************************************************//
        /// <summary>
        /// Explanation of 'Selected' Fields:
        /// 
        /// - Fields named 'Selected' are used to change/set the correct value 
        ///   for the desired property based on the selected item/object from 
        ///   populated menus.
        /// 
        /// - Facilitates parsing the correct values to ViewModel Commands 
        ///   according to the user's selection in menus/dropdowns.
        /// </summary>
        //**************************************************************************//

        private Examination selectedExamination;

        public Examination SelectedExamination
        {
            get 
            { 
                return selectedExamination; 
            }
            set 
            { 
                selectedExamination = value;
                OnPropertyChanged(nameof(SelectedExamination));

                ExaminationName = selectedExamination?.ExaminationName; 
            }
        }

        private string selectedStartTime;

        public string SelectedStartTime
        {
            get
            {
                return selectedStartTime;
            }
            set
            {
                selectedStartTime = value;
                OnPropertyChanged(nameof(SelectedStartTime));
            }
        }

        private Pilot? selectedPilot;

        public Pilot SelectedPilot
        {
            get
            {
                return selectedPilot;
            }
            set
            {
                selectedPilot = value;
                OnPropertyChanged(nameof(SelectedPilot));
            }
        }

        private AME? selectedAME;
        public AME SelectedAME
        {
            get
            {
                return selectedAME;
            }
            set
            {
                selectedAME = value;
                OnPropertyChanged(nameof(SelectedAME));

                AME_SSN = selectedAME?.SocialSecurityNumber;
            }
        }

        //**************************************************************************//
        /// <summary>
        /// Explanation of "Main" Fields:
        /// 
        /// - These fields correspond to the 'Appointment' model class.
        /// 
        /// - The listed fields/properties above (List and Selected) can be 
        ///   defined as helper properties for these main fields.
        /// </summary>
        //**************************************************************************//

        private DateTime appointmentDate;
        public DateTime AppointmentDate
        {
            get
            {
                return appointmentDate;
            }
            set
            {
                appointmentDate = value;
                OnPropertyChanged(nameof(AppointmentDate));
            }
        }        

        private string examinationName;
        public string ExaminationName
        {
            get
            {
                return examinationName;
            }
            set
            {
                examinationName = value;
                OnPropertyChanged(nameof(ExaminationName));
            }
        }

        private string pilotCabinCrew_SNN;
        public string PilotCabinCrew_SSN
        {
            get
            {
                return pilotCabinCrew_SNN;
            }
            set
            {
                pilotCabinCrew_SNN = value;
                OnPropertyChanged(nameof(PilotCabinCrew_SSN));
            }
        }

        private string? ame_SNN;
        public string AME_SSN
        {
            get
            {
                return ame_SNN;
            }
            set
            {
                ame_SNN = value;
                OnPropertyChanged(nameof(AME_SSN));
            }
        }

        private string appointmentID;
        public string AppointmentID
        {
            get
            {
                return appointmentID;
            }
            set
            {
                appointmentID = value;
                OnPropertyChanged(nameof(AppointmentID));
            }
        }     
        
        // --------------------Commands-------------------------------------------------/
        public ICommand GetFutureAppointmentsCommand { get; }

        //**************************************************************************//
        /// <summary>
        /// Business Purpose of 'DeleteAppointmentByIDCommand':
        /// 
        /// - Enables the deletion of an appointment by expecting 'AppointmentID' as
        ///   a parameter, ensuring single selection.
        /// 
        /// - Calls 'ExecuteDeleteAppointmentByIDCommand,' which:
        ///   - Identifies the 'AppointmentID' to delete.
        ///   - Removes the appointment from the DB using 'AppointmentRepo.'
        ///   - Displays the result in a MessageBox for user feedback.
        ///   - Refreshes the listView and updates the ViewModel's 'Appointments' 
        ///     property by calling 'GetBySocialSecurityNumber.'
        /// </summary>
        //**************************************************************************//
        public ICommand DeleteAppointmentByIDCommand { get; }
        public ICommand? GetAuthoriazedAMEByExaminationCommand { get; }
        public ICommand? GetALLPilotsAndCabinCrewCommand { get; }
        public ICommand? GetALLExaminationsCommand { get; }

        //**************************************************************************//
        /// <summary>
        /// Business Purpose of 'UpdateAppointmentCommand':
        /// 
        /// - Expects the 'SelectedItem' from the "Appointments_ListView" as a 
        ///   parameter, requiring a prior selection.
        /// 
        /// - Uses two-way binding between the listView and the underlying 
        ///   textBoxes, allowing users to enter new inputs for the appointment 
        ///   to be updated.
        /// 
        /// - When the "Edit_appointment_btn" is pressed, 'UpdateAppointmentCommand'
        ///   calls 'ExecuteUpdateAppointmentCommand,' which:
        ///   - Verifies that the command parameter is an 'Appointment' object.
        ///   - If true, creates an instance of 'AppointmentRepo' and calls 
        ///     'UpdateAppointment' to update the appointment in the DB.
        /// </summary>
        //**************************************************************************//

        public ICommand UpdateAppointmentCommand { get; }

        public ICommand GetAvailableStartTimesCommand { get; }
        public ICommand CreateNewAppointmentCommand { get; }

        // ----------------------------Constructor----------------------------------------------------------------------// 
        public AppointmentViewModel()
        {
            // Initialize default values
            AppointmentDate = DateTime.Now;
            LoadAllPilotsAndCabinCrews();
            LoadAllExaminations();

            //Initialize commands                      
            DeleteAppointmentByIDCommand = new CommandBase(ExecuteDeleteAppointmentByIDCommand);
            UpdateAppointmentCommand = new CommandBase(ExecuteUpdateAppointmentCommand);
            GetAuthoriazedAMEByExaminationCommand = new CommandBase(ExecuteGetAuthoriazedAMEByExaminationCommand);
            GetAvailableStartTimesCommand = new CommandBase(ExecuteGetAvailableStartTimesCommand);
            CreateNewAppointmentCommand = new CommandBase(ExecuteCreateNewAppointmentCommand);
            GetFutureAppointmentsCommand = new CommandBase(ExecuteGetFutureAppointmentsCommand);
        }

        //------------------------Methods-------------------------------------------------------------------------------------------------------------//
        /// -----------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Loads all pilots and cabin crews from the data source and updates the respective collections.
        /// </summary>
        /// <remarks>
        /// This method retrieves the list of pilots and cabin crews from the appointment repository.
        /// It then updates the <see cref="AllPilots"/> and <see cref="AllCabinCrews"/> collections with the results.
        /// </remarks>
        /// -----------------------------------------------------------------------------------------------------------------------/
        public void LoadAllPilotsAndCabinCrews()
        {
                // Retrieve pilots and cabin crews from the data source
                var (pilots, cabinCrews) = appointmentRepo.GetAllPilotsAndCabinCrews();

                // Update the AllPilots and AllCabinCrews collections with the results
                AllPilots = pilots.OfType<Pilot>().ToList();
                AllCabinCrews = cabinCrews.OfType<CabinCrew>().ToList();
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Executes the command to retrieve future appointments for the current pilot or cabin crew.
        /// </summary>
        /// <remarks>
        /// This method calls the <c>GetFutureAppointments</c> method from the <c>AppointmentRepo</c> to
        /// retrieve and update the list of future appointments for the current pilot or cabin crew.
        /// If an exception occurs during the retrieval process, an error message is displayed.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void ExecuteGetFutureAppointmentsCommand(object obj)
        {
                PilotCabinCrew_SSN = SelectedPilot.SocialSecurityNumber; 
                Appointments = appointmentRepo.GetFutureAppointments(PilotCabinCrew_SSN);
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Executes the command to create a new appointment.
        /// </summary>
        /// <remarks>
        /// This method calls the <c>Create</c> method from the <c>AppointmentRepo</c> to create a new appointment
        /// with the provided information. If the appointment creation is successful, a success message is displayed;
        /// otherwise, an error message is shown.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void ExecuteCreateNewAppointmentCommand(object obj)
        {
                PilotCabinCrew_SSN = SelectedPilot.SocialSecurityNumber;
                MessageBox.Show(appointmentRepo.Create(PilotCabinCrew_SSN, AME_SSN, ExaminationName, TimeSpan.Parse(SelectedStartTime), AppointmentDate));    
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Executes the command to retrieve available start times for the selected AME and date.
        /// </summary>
        /// <remarks>
        /// This method calls the <c>GetAvailableTimesForAME</c> method from the <c>AppointmentRepo</c> to
        /// retrieve and update the list of available start times for the selected AME and date.
        /// If an exception occurs during the retrieval process, an error message is displayed.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void ExecuteGetAvailableStartTimesCommand(object obj)
        {
                StartTime = appointmentRepo.GetAvailableTimesForAME(AME_SSN, DateOnly.FromDateTime(AppointmentDate));
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Loads all available examinations.
        /// </summary>
        /// <remarks>
        /// This method calls the <c>GetAllExaminations</c> method from the <c>AppointmentRepo</c> to
        /// retrieve and update the list of all available examinations.
        /// If an exception occurs during the retrieval process, an error message is displayed.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void LoadAllExaminations()
        {
                AllExaminations = appointmentRepo.GetAllExaminations();
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Executes the command to retrieve authorized AMEs for the selected examination.
        /// </summary>
        /// <remarks>
        /// This method calls the <c>GetAuthorizedAMEsByExamination</c> method from the <c>AppointmentRepo</c> to
        /// retrieve and update the list of authorized AMEs for the selected examination.
        /// If an exception occurs during the retrieval process, an error message is displayed.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void ExecuteGetAuthoriazedAMEByExaminationCommand(object obj)
        {
                AuthorizedAMEs = appointmentRepo.GetAuthorizedAMEsByExamination(ExaminationName);
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Executes the command to update the selected appointment.
        /// </summary>
        /// <param name="obj">Object parameter representing the selected appointment.</param>
        /// <remarks>
        /// This method checks if the provided object is an <c>Appointment</c> and calls the <c>UpdateAppointment</c>
        /// method from the <c>AppointmentRepo</c> to update the selected appointment. After the update, it displays
        /// the result message. If an exception occurs during the update process, an error message is shown.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void ExecuteUpdateAppointmentCommand(object obj)
        {
                // Check if the command parameter is an Appointment object
                if (obj is Appointment selectedAppointment)
                {
                    MessageBox.Show(appointmentRepo.UpdateAppointment(selectedAppointment));

                    // Refresh the list after the selected item was updated
                    Appointments = appointmentRepo.GetFutureAppointments(PilotCabinCrew_SSN);
                }            
        }

        /// -------------------------------------------------------------------------------------------------------------------------/
        /// <summary>
        /// Executes the command to delete the appointment by its ID.
        /// </summary>
        /// <param name="obj">Object parameter representing the appointment ID.</param>
        /// <remarks>
        /// This method checks if the provided object is an integer (appointment ID) and calls the
        /// <c>DeleteAppointment</c> method from the <c>AppointmentRepo</c> to delete the appointment by ID.
        /// After the deletion, it displays the result message. If an exception occurs during the deletion process,
        /// an error message is shown.
        /// </remarks>
        /// -------------------------------------------------------------------------------------------------------------------------/
        private void ExecuteDeleteAppointmentByIDCommand(object obj)
        {
                // Check if the command parameter is an integer (AppointmentID)
                if (obj is int appointmentID)
                {
                    MessageBox.Show(appointmentRepo.DeleteAppointment(appointmentID));

                    // Refresh the list after the selected item was deleted
                    Appointments = appointmentRepo.GetFutureAppointments(PilotCabinCrew_SSN);
                }
        }
    }
}
