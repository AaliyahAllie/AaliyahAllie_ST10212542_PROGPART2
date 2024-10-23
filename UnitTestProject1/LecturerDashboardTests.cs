using AaliyahAllie_ST10212542_PROGPART2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Threading; // Make sure this using directive is present

namespace UnitTest
{
    [TestClass]
    public class LecturerDashboardTests
    {
        private LecturerDashboard _lecturerDashboard; // Make it nullable

        [TestInitialize]
        public void Setup()
        {
            // Initialize the LecturerDashboard instance
            _lecturerDashboard = new LecturerDashboard();
        }

        [TestMethod]
        public void TestTimerTick()
        {
            // Act: Call the Timer_Tick method
            _lecturerDashboard.Timer_Tick(null, EventArgs.Empty);

            // Assert: Verify the expected outcomes
            // Here you should check if LoadClaimStatus was called or any other assertions you want
        }

        [TestMethod]
        public void TestConstructorInitializesCorrectly()
        {
            // Verify that the instance is created
            Assert.IsNotNull(_lecturerDashboard);
        }
    }
}
