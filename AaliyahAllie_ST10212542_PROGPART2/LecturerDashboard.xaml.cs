using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AaliyahAllie_ST10212542_PROGPART2
{
    /// <summary>
    /// Interaction logic for LecturerDashboard.xaml
    /// </summary>
    public partial class LecturerDashboard : Window
    {
        private string connectionString = "Data Source=hp820g4\\SQLEXPRESS;Initial Catalog=POE;Integrated Security=True;";

        public LecturerDashboard()
        {
            InitializeComponent();
            LoadClaimStatus();
        }

        private void SubmitClaim_Click(object sender, RoutedEventArgs e)
        {
            SubmitClaim submitClaim = new SubmitClaim();
            submitClaim.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SupportingDocs Docs = new SupportingDocs();
            Docs.Show();
        }

        // Event handler for the View Claims button
        private void ViewClaims_Click(object sender, RoutedEventArgs e)
        {
            ViewClaims viewClaims = new ViewClaims();
            viewClaims.Show();
            LoadClaimStatus(); // Load claims status when the button is clicked
        }

        // Method to load claim status into the ListView
        private void LoadClaimStatus()
        {
            string query = "SELECT ClassTaught, TotalAmount, ClaimStatus FROM Claims"; // Adjust the query as necessary

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                ClaimStatusListView.ItemsSource = dataTable.DefaultView; // Set the data source for the ListView
            }
        }
    }
}
