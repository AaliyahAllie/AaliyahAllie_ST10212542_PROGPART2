using Moq;
using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace AaliyahAllie_ST10212542_PROGPART2.Tests
{
    [TestFixture]
    public class ClaimManagerTests
    {
        private Mock<SqlConnection> mockConnection;
        private ClaimManager claimManager;

        [SetUp]
        public void Setup()
        {
            // Initialize your mock objects or set up resources
            mockConnection = new Mock<SqlConnection>();
            claimManager = new ClaimManager();
        }

        [Test]
        public void CalculateTotalAmount_ShouldReturnCorrectAmount()
        {
            // Arrange
            int numberOfSessions = 5;
            decimal expectedTotalAmount = 525; // 5 * 105

            // Act
            decimal actualTotalAmount = claimManager.CalculateTotalAmount(numberOfSessions);

            // Assert
            Assert.AreEqual(expectedTotalAmount, actualTotalAmount);
        }

        [Test]
        public void LoadClaimDetails_ShouldReturnCorrectClaimDetails_WhenClaimExists()
        {
            // Arrange
            int claimID = 1;
            var expectedClaimDetails = ("Math", 10, 1050m); // ClassTaught, NumberOfSessions, TotalAmount
            Mock<SqlCommand> mockCommand = new Mock<SqlCommand>();

            // You would mock the reader and command execution if using Moq. Here we simulate behavior.
            var mockReader = new Mock<SqlDataReader>();
            mockReader.SetupSequence(reader => reader.Read())
                .Returns(true) // First call returns true to simulate a record found
                .Returns(false); // No more records after

            mockReader.Setup(reader => reader["ClassTaught"]).Returns("Math");
            mockReader.Setup(reader => reader["NumberOfSessions"]).Returns(10);
            mockReader.Setup(reader => reader["TotalAmount"]).Returns(1050m);

            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var actualClaimDetails = claimManager.LoadClaimDetails(claimID);

            // Assert
            Assert.IsNotNull(actualClaimDetails);
            Assert.AreEqual(expectedClaimDetails, actualClaimDetails.Value);
        }

        [Test]
        public void LoadClaimDetails_ShouldReturnNull_WhenClaimDoesNotExist()
        {
            // Arrange
            int claimID = 999; // Non-existent claim

            // Simulate no data found by returning false on Read call
            Mock<SqlCommand> mockCommand = new Mock<SqlCommand>();
            var mockReader = new Mock<SqlDataReader>();
            mockReader.Setup(reader => reader.Read()).Returns(false); // No record found

            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);

            // Act
            var claimDetails = claimManager.LoadClaimDetails(claimID);

            // Assert
            Assert.IsNull(claimDetails);
        }

        [Test]
        public void UpdateClaim_ShouldThrowException_WhenUpdateFails()
        {
            // Arrange
            int claimID = 1;
            string classTaught = "Math";
            int numberOfSessions = 10;
            decimal totalAmount = 1050m;

            Mock<SqlCommand> mockCommand = new Mock<SqlCommand>();
            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Throws(new Exception("Update failed"));

            // Act & Assert
            Assert.Throws<Exception>(() => claimManager.UpdateClaim(claimID, classTaught, numberOfSessions, totalAmount));
        }

        [Test]
        public void UpdateClaim_ShouldNotThrowException_WhenUpdateSucceeds()
        {
            // Arrange
            int claimID = 1;
            string classTaught = "Math";
            int numberOfSessions = 10;
            decimal totalAmount = 1050m;

            Mock<SqlCommand> mockCommand = new Mock<SqlCommand>();
            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Returns(1); // Simulate successful update

            // Act & Assert
            Assert.DoesNotThrow(() => claimManager.UpdateClaim(claimID, classTaught, numberOfSessions, totalAmount));
        }
    }
}
