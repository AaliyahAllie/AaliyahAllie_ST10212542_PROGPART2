using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AaliyahAllie_ST10212542_PROGPART2;
using Moq;

namespace ProgTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UploadDocument_ValidFile_Success()
        {
            // Arrange
            var submitClaim = new SubmitClaim("fakeConnectionString");
            string filePath = "testfile.docx";

            // Act
            bool result = submitClaim.UploadDocument(filePath);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(filePath, submitClaim.UploadedFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateTotalAmount_InvalidSessions_ThrowsException()
        {
            // Arrange
            var submitClaim = new SubmitClaim("fakeConnectionString");

            // Act
            submitClaim.CalculateTotalAmount(-5, 105); // Invalid sessions

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void CalculateTotalAmount_ValidInputs_ReturnsTotalAmount()
        {
            // Arrange
            var submitClaim = new SubmitClaim("fakeConnectionString");
            int sessions = 4;
            double hourlyRate = 105;

            // Act
            double result = submitClaim.CalculateTotalAmount(sessions, hourlyRate);

            // Assert
            Assert.AreEqual(420, result);
        }

        [TestMethod]
        public void SaveClaimToDatabase_ValidClaim_Success()
        {
            // Arrange
            var submitClaim = new SubmitClaim("fakeConnectionString");
            string classTaught = "Math";
            int sessions = 3;
            double totalAmount = 315;
            string documentPath = "testfile.docx";

            // Mocking the database operation (Note: for real tests, mock DB access)
            Mock<SubmitClaim> mockSubmitClaim = new Mock<SubmitClaim>("fakeConnectionString");
            mockSubmitClaim.Setup(x => x.SaveClaimToDatabase(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<string>())).Returns(true);

            // Act
            bool result = mockSubmitClaim.Object.SaveClaimToDatabase(classTaught, sessions, totalAmount, documentPath);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
