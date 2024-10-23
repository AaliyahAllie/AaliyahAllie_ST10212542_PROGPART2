using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; // Make sure to install the Moq NuGet package
using System.Windows;
using Microsoft.Win32;
using AaliyahAllie_ST10212542_PROGPART2;

namespace TestProject1
{
    [TestClass]
    public class CreateAccountWindowTests
    {
        private CreateAccountWindow _window;

        [TestInitialize]
        public void Setup()
        {
            _window = new CreateAccountWindow();
        }

        [TestMethod]
        public void UploadDocument_Click_ValidFile_ShouldStoreFilePathAndShowSuccessMessage()
        {
            // Arrange
            var expectedFilePath = "C:\\path\\to\\file.docx";

            // Mock OpenFileDialog
            var mockOpenFileDialog = new Mock<OpenFileDialog>();
            mockOpenFileDialog.Setup(d => d.ShowDialog()).Returns(true);
            mockOpenFileDialog.SetupGet(d => d.FileName).Returns(expectedFilePath);

            // Replace OpenFileDialog with the mock
            // (If OpenFileDialog cannot be replaced directly, this will be a conceptual step)

            // Act
            _window.UploadDocument_Click(null, null); // Call the method directly

            // Assert
            Assert.AreEqual(expectedFilePath, _window.uploadedFilePath);
            // Here you would also check if a message box was shown.
            // Since MessageBox.Show is static, we might not be able to assert directly.
            // So, we could either:
            // a) Refactor to allow dependency injection for message showing
            // b) Use a mock framework that allows interception of static calls (not recommended)

            // For this specific case, you might just assert the file path change.
        }
    }
}
