using FlyveLægeKBH.Commands;
using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public string SocialSecurityNumber { get; set; }

        public int AppointmentID { get; set; }
        public string PilotCabinCrew_SSN { get; set; }
        public string AME_SSN { get; set; }
        public string ExaminationName { get; set; }
        public TimeSpan StartTime { get; set; }
        public DateTime AppointmentDate { get; set; }


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


        // Constructor 
        public AppointmentViewModel()
        {
            GetBookingsBySSNCommand = new CommandBase(GetBookingsBySSN);
            DeleteAppointmentByIDCommand = new CommandBase(ExecuteDeleteAppointmentByIDCommand);
        }


        // Methods
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
