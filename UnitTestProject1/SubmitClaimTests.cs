using AaliyahAllie_ST10212542_PROGPART2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace UnitTest
{
    [TestClass]
    public class SubmitClaimTests
    {
        private SubmitClaim _submitClaim; // Make it nullable

        [TestInitialize]
        public void Setup()
        {
            // Initialize the SubmitClaim instance
            _submitClaim = new SubmitClaim();
        }

        

        private string GetTextBoxValue(string propertyName)
        {
            var textBox = typeof(SubmitClaim).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_submitClaim);
            return (textBox as TextBox)?.Text;
        }

        private string GetTextBlockValue(string propertyName)
        {
            var textBlock = typeof(SubmitClaim).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_submitClaim);
            return (textBlock as TextBlock)?.Text;
        }

        private void SetTextBoxValue(string propertyName, string value)
        {
            var textBox = typeof(SubmitClaim).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_submitClaim);
            if (textBox is TextBox tb)
            {
                tb.Text = value;
            }
        }

        private void SetTextBlockValue(string propertyName, string value)
        {
            var textBlock = typeof(SubmitClaim).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_submitClaim);
            if (textBlock is TextBlock tb)
            {
                tb.Text = value;
            }
        }

        [TestMethod]
        public void TestConstructorInitializesCorrectly()
        {
            // Verify that the instance is created
            Assert.IsNotNull(_submitClaim);
        }
    }
}
