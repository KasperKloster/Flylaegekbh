using FlyveLægeKBH.Models;
using FlyveLægeKBH.Repos;
namespace RepoTest1
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Create_Pilot_Test()
        {
            // Arrange
            string firstNames = "John";
            string surName = "Doe";
            string email = "john.doe@example.com";
            string phone = "123-456-7890";
            string address = "123 Main St";
            string socialSecurityNumber = "111111-1111";
            string title = "Pilot";
            string certificateNumber = "CERT123";
            DateTime dateOfIssue = DateTime.Now;
            DateTime class1SinglePilotExpiryDate = DateTime.Now.AddYears(1);
            DateTime class1ExpiryDate = DateTime.Now.AddYears(1);
            DateTime class2ExpiryDate = DateTime.Now.AddYears(5);
            DateTime laplExpiryDate = DateTime.Now.AddYears(5);
            DateTime electroCardiogramRecentDate = DateTime.Now;
            DateTime audiogramRecentDate = DateTime.Now.AddYears(-1);

            // Act
            try
            {
                PilotRepo pilotRepo = new PilotRepo();
                string result = pilotRepo.CreatePilot(
                    firstNames, surName, email, phone, address, socialSecurityNumber, title,
                    certificateNumber, dateOfIssue, class1SinglePilotExpiryDate, class1ExpiryDate,
                    class2ExpiryDate, laplExpiryDate, electroCardiogramRecentDate, audiogramRecentDate
                );


                // Assert
                Assert.AreEqual("Pilot created successfully", result);
            }
            catch ( Exception ex ) 
            { 
                Assert.Fail($"Exception thrown: {ex.Message}");
            }
        }

        [TestMethod]
        public void Delete_Pilot_Test()
        {
            // Arrange
            string socialSecurityNumber = "111111-1111"; 

            // Act
            string result = PilotRepo.DeletePilot(socialSecurityNumber);

            // Assert
            Assert.AreEqual($"Cabin Crew with ssn: {socialSecurityNumber} has been deleted", result);
        }

        [TestMethod]
        public void Create_CabincCrew_Test()
        {
            // Arrange
            string firstName = "Alice";
            string surName = "Smith";
            string email = "alice.smith@example.com";
            string phone = "987-654-3210";
            string address = "456 Oak St";
            string socialSecurityNumber = "222222-2222";
            string title = "CabinCrew";
            DateTime dateOfIssue = DateTime.Now;
            DateTime cabinCrewExpiryDate = DateTime.Now.AddYears(5);

            // Act
            string result = CabinCrewRepo.CreateCabinCrew(
                firstName, surName, email, phone, address, socialSecurityNumber, title,
                dateOfIssue, cabinCrewExpiryDate
                );

            // Assert
            Assert.AreEqual("Added Cabin Crew successfully", result);
        }

        [TestMethod]
        public void Delete_CabincCrew_Test()
        {
            // Arrange
            string socialSecurityNumber = "222222-2222"; // 

            // Act
            string result = CabinCrewRepo.DeleteCabinCrew(socialSecurityNumber);

            // Assert
            Assert.AreEqual($"Cabin Crew with ssn: {socialSecurityNumber} has been deleted", result);
        }

    }
}