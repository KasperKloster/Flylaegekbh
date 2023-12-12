using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlyveLægeKBH.ViewModels
{
    public class AppointmentViewModel: ViewModelBase
    {
        //----------Fields----------------------------
        

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

        //this field is used as the source to display all Pilots in the DB to simulate wich user is login and are performing actions 
        private List<Pilot> allPilots;
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

        //this field is used as the source to display all Cabin Crews in the DB to simulate wich user is login and are performing actions 
        private List<CabinCrew> allCabinCrews;
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

        //this field is used as the source to display all Examinations in the DB to populate the Examinations menu the user can choose from 
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

        //this field is used to bind the selectedExamination objects ExaminationName to the ExaminationName property.
        //This ensures that we cand parss the selectedExamination ExaminationName to other Actions throug the property ExaminationName.
        private AME selectedAME;

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

        // fields/properties changes for the Update function --> now we can binde to the properties so when edit btn is click we get the selected object.
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

        //this field is used for the selectedStartTime in the combobox and is the value the create new appointment should use.
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

        private string ame_SNN;
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

        private string socialSecurityNumber;
        public string SocialSecurityNumber
        {
            get
            {
                return socialSecurityNumber;
            }
            set
            {
                socialSecurityNumber = value;
                OnPropertyChanged(nameof(SocialSecurityNumber));
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


        private AppointmentRepo appointmentRepo = new AppointmentRepo();




        // Commands
        public ICommand GetBookingsBySSNCommand { get; set; }
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

        // Constructor 
        public AppointmentViewModel()
        {
            // This is just to set the default value to the current date for the datepicker
            AppointmentDate = DateTime.Now;

            GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
            DeleteAppointmentByIDCommand = new CommandBase(ExecuteDeleteAppointmentByIDCommand);
            UpdateAppointmentCommand = new CommandBase(ExecuteUpdateAppointmentCommand);
            GetAuthoriazedAMEByExaminationCommand = new CommandBase(ExecuteGetAuthoriazedAMEByExaminationCommand);
            GetALLPilotsAndCabinCrewCommand = new CommandBase(ExecuteGetALLPilotsAndCabinCrewCommand);
            GetALLExaminationsCommand = new CommandBase(ExecuteGetALLExaminationsCommand);
            GetAvailableStartTimesCommand = new CommandBase(ExecuteGetAvailableStartTimesCommand);
        }

        private void ExecuteGetAvailableStartTimesCommand(object obj)
        {

            StartTime = appointmentRepo.GetAvailableTimesForAME(AME_SSN, DateOnly.FromDateTime(AppointmentDate));
            

        }

        private void ExecuteGetALLExaminationsCommand(object obj)
        {
            AppointmentRepo appointmentRepo = new AppointmentRepo();
            AllExaminations = appointmentRepo.GetAllExaminations();
        }

        private void ExecuteGetALLPilotsAndCabinCrewCommand(object obj)
        {
            try
            {
                AppointmentRepo appointmentRepo = new AppointmentRepo();
                var (pilots, cabinCrews) = appointmentRepo.GetAllPilotsAndCabinCrews();

                AllPilots = pilots;
                AllCabinCrews = cabinCrews;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Der skete en fejl under indlæsning af alle piloter og Cabin Crews. Error: {ex.Message}");
            }
            
        }

        private void ExecuteGetAuthoriazedAMEByExaminationCommand(object obj)
        {
                       
                AppointmentRepo appointmentRepo = new AppointmentRepo();
                AuthorizedAMEs = appointmentRepo.GetAuthorizedAMEsByExamination(ExaminationName);                
               
        }


        // Methods

        private void ExecuteUpdateAppointmentCommand(object obj)
        {
            // check if the command parameter is an Appointment object
            if (obj is Appointment selectedAppointment)
            {
                AppointmentRepo appointmentRepo = new AppointmentRepo();
                MessageBox.Show(appointmentRepo.UpdateAppointment(selectedAppointment));

            }            

            
        }
        private void ExecuteDeleteAppointmentByIDCommand(object obj)
        {
            // check if the command parameter is an integer (AppointmentID)
            if(obj is int appointmentID)
            {
                AppointmentRepo appointmentRepo = new AppointmentRepo();
                MessageBox.Show(appointmentRepo.DeleteAppointment(appointmentID));

                // To refrehs the list after the selectede item was deleted
                Appointments = appointmentRepo.GetBySocialSecurityNumber(SocialSecurityNumber);
            }
           
        }

        private void GetBookingsBySSN(object obj)
        {
            AppointmentRepo appointmentRepo = new AppointmentRepo();

            Appointments = appointmentRepo.GetBySocialSecurityNumber(SocialSecurityNumber);
        }

    }
}
