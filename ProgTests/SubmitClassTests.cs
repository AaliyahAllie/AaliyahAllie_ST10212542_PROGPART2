using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; // Install Moq package for mocking
using AaliyahAllie_ST10212542_PROGPART2;
using System.Windows.Controls;
using System;

namespace ProgTests
{
    [TestClass]
    public class SubmitClaimTests
    {
        [TestMethod]
        public void CalculateButton_Click_ValidSessions_ShouldCalculateTotalAmount()
        {
            // Arrange
            var submitClaim = new SubmitClaim();
            var sessionsTextBox = new TextBox { Text = "3" }; // 3 sessions
            submitClaim.SessionsTextBox = sessionsTextBox;

            var totalAmountTextBlock = new TextBlock();
            submitClaim.TotalAmountTextBlock = totalAmountTextBlock;

            // Act
            submitClaim.CalculateButton_Click(null, null);

            // Assert
            Assert.AreEqual("R315.00", submitClaim.TotalAmountTextBlock.Text); // 3 * 105 = 315
        }

        [TestMethod]
        public void CalculateButton_Click_InvalidSessions_ShouldThrowException()
        {
            // Arrange
            var submitClaim = new SubmitClaim();
            var sessionsTextBox = new TextBox { Text = "invalid" }; // Invalid input
            submitClaim.SessionsTextBox = sessionsTextBox;

            var totalAmountTextBlock = new TextBlock();
            submitClaim.TotalAmountTextBlock = totalAmountTextBlock;

            // Act & Assert
            var exception = Assert.ThrowsException<Exception>(() =>
                submitClaim.CalculateButton_Click(null, null));

            Assert.AreEqual("Please enter a valid number of sessions.", exception.Message);
        }

        [TestMethod]
        public void UploadDocument_ShouldSetUploadedFilePath_WhenFileSelected()
        {
            // Arrange
            var mockFileDialogService = new Mock<IFileDialogService>();
            mockFileDialogService.Setup(m => m.OpenFileDialog(It.IsAny<string>())).Returns("test.docx");

            var submitClaim = new SubmitClaim(mockFileDialogService.Object);

            // Act
            submitClaim.UploadDocument_Click(null, null);

            // Assert
            Assert.AreEqual("test.docx", submitClaim.UploadedFilePath);
        }
    }
}
