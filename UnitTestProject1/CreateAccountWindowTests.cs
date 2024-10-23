using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;

namespace AaliyahAllie_ST10212542_PROGPART2.Tests
{
    [TestClass]
    public class CreateAccountWindowTests
    {
        private CreateAccountWindow _createAccountWindow;

        [TestInitialize]
        public void Setup()
        {
            _createAccountWindow = new CreateAccountWindow();
        }

        [TestMethod]
        public void CreateAccountButton_Click_ValidInput_ShouldShowSuccessMessage()
        {
            
            SetTextBoxValue("John", "FirstNameTextBox");
            SetTextBoxValue("Doe", "LastNameTextBox");
            SetTextBoxValue("john.doe@example.com", "EmailTextBox");
            SetTextBoxValue("1234567890", "PhoneNumberTextBox");
            SetPasswordValue("password");
            SetComboBoxSelection("User");

            // Act
            _createAccountWindow.CreateAccountButton_Click(null, null);

            // Assert
            MessageBoxResult result = MessageBox.Show("Account successfully created."); // Replace with your method to capture message box
            Assert.AreEqual(MessageBoxResult.OK, result);
        }

        [TestMethod]
        public void CreateAccountButton_Click_InvalidInput_ShouldShowErrorMessage()
        {
            // Arrange
            // Simulate invalid input (e.g., empty first name)
            SetTextBoxValue("", "FirstNameTextBox");

            // Act
            _createAccountWindow.CreateAccountButton_Click(null, null);

            // Assert
            // Again, capture the message box and assert the message
            MessageBoxResult result = MessageBox.Show("Please fill out all fields."); // Replace with your method to capture message box
            Assert.AreEqual(MessageBoxResult.OK, result);
        }

        // Helper method to set TextBox values using reflection
        private void SetTextBoxValue(string value, string textBoxName)
        {
            var textBox = (TextBox)_createAccountWindow.GetType().GetProperty(textBoxName).GetValue(_createAccountWindow);
            textBox.Text = value;
        }

        // Helper method to set PasswordBox value using reflection
        private void SetPasswordValue(string password)
        {
            var passwordBox = (PasswordBox)_createAccountWindow.GetType().GetProperty("PasswordBox").GetValue(_createAccountWindow);
            passwordBox.Password = password;
        }

        // Helper method to set ComboBox selection using reflection
        private void SetComboBoxSelection(string selection)
        {
            var comboBox = (ComboBox)_createAccountWindow.GetType().GetProperty("RoleComboBox").GetValue(_createAccountWindow);
            comboBox.SelectedItem = new ComboBoxItem { Content = selection };
        }
    }
}
