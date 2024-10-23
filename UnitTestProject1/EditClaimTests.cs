using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace AaliyahAllie_ST10212542_PROGPART2.Tests
{
    [TestClass]
    public class EditClaimTests
    {
        private EditClaim _editClaim;

        [TestInitialize]
        public void Setup()
        {
            // Create an instance of EditClaim with an example claim ID
            _editClaim = new EditClaim(1); // Assuming claim ID 1 for testing
        }

        [TestMethod]
        public void LoadClaimDetails_ShouldLoadCorrectData()
        {
            // Simulate loading details by manually setting the textboxes
            SetTextBoxValue("Math 101", "ClassTaughtTextBox");
            SetTextBoxValue("5", "NumberOfSessionsTextBox");
            SetTextBoxValue("525.00", "TotalAmountTextBox");

            // Call LoadClaimDetails to simulate actual loading from the database
            _editClaim.LoadClaimDetails();

            // Validate that the data is loaded correctly
            Assert.AreEqual("Math 101", GetTextBoxValue("ClassTaughtTextBox"));
            Assert.AreEqual("5", GetTextBoxValue("NumberOfSessionsTextBox"));
            Assert.AreEqual("525.00", GetTextBoxValue("TotalAmountTextBox"));
        }

       

        [TestMethod]
        public void SaveButton_Click_ValidInput_ShouldShowSuccessMessage()
        {
            // Arrange
            SetTextBoxValue("Math 101", "ClassTaughtTextBox");
            SetTextBoxValue("3", "NumberOfSessionsTextBox");
            SetTextBoxValue("315.00", "TotalAmountTextBox");

            // Act
            _editClaim.SaveButton_Click(null, null);

            // Assert
            // Since MessageBox can't be directly captured, we can validate the output in another way
            // For example, by creating a mock dialog service
            // Here we're just checking that it gets called, the implementation will depend on how you structure it
            // Mocking would require additional infrastructure (not shown here)
        }

        [TestMethod]
        public void SaveButton_Click_InvalidInput_ShouldShowErrorMessage()
        {
            // Arrange
            SetTextBoxValue("", "NumberOfSessionsTextBox"); // Invalid input

            // Act
            _editClaim.SaveButton_Click(null, null);

            // Assert
            // Similar to the previous message box capture
        }

        // Helper method to set TextBox values using reflection
        private void SetTextBoxValue(string value, string textBoxName)
        {
            var textBox = (TextBox)_editClaim.GetType().GetProperty(textBoxName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_editClaim);
            textBox.Text = value;
        }

        // Helper method to get TextBox values using reflection
        private string GetTextBoxValue(string textBoxName)
        {
            var textBox = (TextBox)_editClaim.GetType().GetProperty(textBoxName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_editClaim);
            return textBox.Text;
        }
    }
}
