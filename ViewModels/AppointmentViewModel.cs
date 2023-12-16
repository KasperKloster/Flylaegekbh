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
        // Fields

        /*************************************************************/
        /*               Explanation of List fields                  */
        /*************************************************************/
        /*  all these list fields serve the purpse of displaying 
        data collected from the DB in som kind of menu, where the user 
        should be abel to select a item from                         */
        /*************************************************************/

        //this field is used as the source to display all appointments belongin to specifik aircrew 
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

        //this field is used as the source to display all authorizedAMEs by ExaminationName 
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



        //this field is used as the source to display all Examinations in the DB.
        //this allows us to populate the Examinations menu the user can choose from.
        //and get all the information like Pric and DurationInMin of the Examination based on the selected object.
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



        /*************************************************************/
        /*               Explanation of selected fields              */
        /*************************************************************/
        /*  all these these fields named selected serves the purpose
        of changing/setting the right value to the desired property
        based on the selected item/object from the menus wich was 
        populated with oure list-fields.     
        
        This is done so we can parse the right values to Commands in 
        the ViewModel, again based on the selection in the 
        menus/dropdowns.                                             */
        /*************************************************************/

        //this field is used to bind the selectedExamination objects ExaminationName to the ExaminationName property.
        //This ensures that we cand parss the selectedExamination ExaminationName to other Actions throug the property ExaminationName.
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




        //this field is used for the selectedStartTime in the manue/combobox with the name chooseTime.
        //and is the value the create new appointment is using.
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



        /*************************************************************/
        /*          Explanation of the following fields              */
        /*************************************************************/
        /*  These fields are the "Main" fields, corosponding to the 
         Appointment model class.
        
         The above listede fields/propery (List and selected) we can
        define as helper properys for these Main fields              */
        /*************************************************************/


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

        //this field is used to display the list of availabelStartTimes based on the selected date from the datepicker.
        //This is kept as a string value for now and then convertede in the create action to SQL.dataType time to be insert  in the DB.
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



        //The field SocialSecurityNumber are not used in this iteration
        //for more explanation see comments under the GetBookingsBySSN commandmethod for more explanation

        //private string socialSecurityNumber;
        //public string SocialSecurityNumber
        //{
        //    get
        //    {
        //        return socialSecurityNumber;
        //    }
        //    set
        //    {
        //        socialSecurityNumber = value;
        //        OnPropertyChanged(nameof(SocialSecurityNumber));

        //    }

        //}


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
        public ICommand GetBookingsBySSNCommand { get; set; }
        public ICommand GetFutureAppointmentsCommand { get; }
        public ICommand DeleteAppointmentByIDCommand { get; }

        /*************************************************************/
        /*      Explanation of DeleteAppointmentByIDCommand          */
        /*************************************************************/
        /*  This command expect AppointmentID as a CommanParameter
        We set that expectation in the "AppointmentView.Xaml" under the
        "Delete_Appointments_btn" by saying:

       CommandParameter="
        {
            Binding ElementName=Appointments_listView, 
            Path=SelectedItem.AppointmentID
        }"

        This means that the selected item from the listView is of type
        object and the object have a field called AppointmentID. The 
        value of AppointmentID is an int so to run/Execute this command 
        we need to passe an int called AppointmentID.

        This AppointmentID is the AppointmentID to be deleted.
        Since we are showing the Appointments in a ListView in the GUI
        we can set the listView SelectionMode to Singel. This is because
        a listView is by default set to multiselect, and we would like 
        to ensure that only one appointment object is selected and 
        deleted at a time so we set the SelectionMode="Singel".

        The DeleteAppointmentByIDCommand calles a methid called
        ExecuteDeleteAppointmentByIDCommand. This method first checks
        if the obj there is parsed is of type int appointmentID.
        If true then it creates an instance of the AppointmentRepo 
        class and call the method "DeleteAppointment" to delete
        the appointment in the DB-tabel.

        "DeleteAppointment" returns a string to show a status if
        it was successful or an error occurred trying to delete the
        appointment from the DB. That is why this 
        "ExecuteDeleteAppointmentByIDCommand" calls the
         "DeleteAppointment" inside a MessageBox.Show();

        After the status message is shown in the GUI and the user
        Presses okay the 

        To refresh the listView the method 
        "GetBySocialSecurityNumber" is called wich returns alle
        the appointments by a SocialSecurityNumber and ensuring that
        the ViewModel's Appointments property is updated with the 
        latest list of appointments
                                                                     */
        /*************************************************************/

        public ICommand UpdateAppointmentCommand { get; }
        /*************************************************************/
        /*          Explanation of UpdateAppointmentCommand          */
        /*************************************************************/
        /*  This commands expects the SelectedItem from the 
        "Appointments_ListView". So before pressing the 
        "Edit_appointment_btn" the user needs to press an items in the 
        listview above, wich gets pased as a parameter to this command.

        We created a tow-way binding between the "Appointments_ListView"
        and the textBox's underneath. This binding makes it posible for
        the user to enter the new inputs for the underlying Appointment 
        object wich then will be updated. The Binding 
        looks like this

         Text="
            {
                Binding SelectedItem.(name of the textbox),
                ElementName=Appointments_listView
            }"

        So when the "Edit_appointment_btn" is pressed the 
        "UpdateAppointmentCommand" calls the method
        "ExecuteUpdateAppointmentCommand" wich first checks if
        if the command parameter is an Appointment object. If it is
        then it creates an instance of the AppointmentRepo class
        and calls the UpdateAppointment method.                      */
        /*************************************************************/

        public ICommand GetAuthoriazedAMEByExaminationCommand { get; }
        public ICommand GetALLPilotsAndCabinCrewCommand { get; }
        public ICommand GetALLExaminationsCommand { get; }

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

            // this is outcommentet because they are from a nother iteration.
            //GetALLPilotsAndCabinCrewCommand = new CommandBase(ExecuteGetALLPilotsAndCabinCrewCommand);
            //GetALLExaminationsCommand = new CommandBase(ExecuteGetALLExaminationsCommand);
            //GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
        }

        //------------------------Methods-------------------------------------------------------------------------------------------------------------//

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
        private void ExecuteGetFutureAppointmentsCommand()
        {
            try
            {
                Appointments = appointmentRepo.GetFutureAppointments(PilotCabinCrew_SSN);
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while retrieving future appointments.");
            }
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
        private void ExecuteCreateNewAppointmentCommand()
        {
            try
            {
                MessageBox.Show(appointmentRepo.Create(PilotCabinCrew_SSN, AME_SSN, ExaminationName, TimeSpan.Parse(SelectedStartTime), AppointmentDate));
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while creating a new appointment.");
            }
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
        private void ExecuteGetAvailableStartTimesCommand()
        {
            try
            {
                StartTime = appointmentRepo.GetAvailableTimesForAME(AME_SSN, DateOnly.FromDateTime(AppointmentDate));
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while retrieving available start times.");
            }
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
            try
            {
                AllExaminations = appointmentRepo.GetAllExaminations();
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while loading all examinations.");
            }
        }

        //private void ExecuteGetALLExaminationsCommand(object obj)
        //{

        //    AllExaminations = appointmentRepo.GetAllExaminations();
        //}



        //private void ExecuteGetALLPilotsAndCabinCrewCommand(object obj)
        //{
        //    try
        //    {
        //        AppointmentRepo appointmentRepo = new AppointmentRepo();
        //        var (pilots, cabinCrews) = appointmentRepo.GetAllPilotsAndCabinCrews();

        //        AllPilots = pilots;
        //        AllCabinCrews = cabinCrews;
        //    }
        //    catch (Exception ex) 
        //    {
        //        MessageBox.Show($"Der skete en fejl under indlæsning af alle piloter og Cabin Crews. Error: {ex.Message}");
        //    }

        //}

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
        private void ExecuteGetAuthoriazedAMEByExaminationCommand()
        {
            try
            {
                AuthorizedAMEs = appointmentRepo.GetAuthorizedAMEsByExamination(ExaminationName);
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while retrieving authorized AMEs.");
            }
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
            try
            {
                // Check if the command parameter is an Appointment object
                if (obj is Appointment selectedAppointment)
                {
                    MessageBox.Show(appointmentRepo.UpdateAppointment(selectedAppointment));

                    // Refresh the list after the selected item was updated
                    Appointments = appointmentRepo.GetFutureAppointments(PilotCabinCrew_SSN);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while updating the appointment.");
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
            try
            {
                // Check if the command parameter is an integer (AppointmentID)
                if (obj is int appointmentID)
                {
                    MessageBox.Show(appointmentRepo.DeleteAppointment(appointmentID));

                    // Refresh the list after the selected item was deleted
                    Appointments = appointmentRepo.GetFutureAppointments(PilotCabinCrew_SSN);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "An error occurred while deleting the appointment.");
            }
        }


        // this method GetBookingsBySSN is a method from a earlyer iteration and are not longer used.
        // It is changed to the ExecuteGetFutureAppointmentsCommand method
        // For readability concider deleting this

        //private void GetBookingsBySSN(object obj)
        //{
        //    AppointmentRepo appointmentRepo = new AppointmentRepo();

        //    Appointments = appointmentRepo.GetBySocialSecurityNumber(SocialSecurityNumber);
        //}

    }
}
