using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for EditClaim.xaml
    /// </summary>
    public partial class EditClaim : Window
    {
        private string connectionString = "Data Source=hp820g4\\SQLEXPRESS;Initial Catalog=POE;Integrated Security=True;";
        private int claimID;

        public EditClaim(int claimID)
        {
            InitializeComponent();
            this.claimID = claimID;
            LoadClaimDetails();
        }

        // Method to load claim details for editing
        private void LoadClaimDetails()
        {
            string query = "SELECT ClassTaught, TotalAmount FROM Claims WHERE ClaimID = @ClaimID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClaimID", claimID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ClassTaughtTextBox.Text = reader["ClassTaught"].ToString();
                        TotalAmountTextBox.Text = reader["TotalAmount"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Event handler for saving changes
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string classTaught = ClassTaughtTextBox.Text;
            decimal totalAmount;

            if (decimal.TryParse(TotalAmountTextBox.Text, out totalAmount))
            {
                UpdateClaim(classTaught, totalAmount);
                MessageBox.Show("Claim updated successfully!");
                Close(); // Close the edit window
            }
            else
            {
                MessageBox.Show("Invalid total amount.");
            }
        }

        // Method to update claim in the database
        private void UpdateClaim(string classTaught, decimal totalAmount)
        {
            string query = "UPDATE Claims SET ClassTaught = @ClassTaught, TotalAmount = @TotalAmount WHERE ClaimID = @ClaimID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassTaught", classTaught);
                command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                command.Parameters.AddWithValue("@ClaimID", claimID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
