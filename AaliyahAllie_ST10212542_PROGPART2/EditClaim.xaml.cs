using System;
using System.Data.SqlClient;
using System.Windows;

namespace AaliyahAllie_ST10212542_PROGPART2
{
    public partial class EditClaim : Window
    {
        private string connectionString = "Data Source=hp820g4\\SQLEXPRESS;Initial Catalog=POE;Integrated Security=True;";
        private int claimID;
        private decimal sessionCost = 105; // Example cost per session

        public EditClaim(int claimID)
        {
            InitializeComponent();
            this.claimID = claimID;
            LoadClaimDetails();
        }

        private void LoadClaimDetails()
        {
            string query = "SELECT ClassTaught, NumberOfSessions, TotalAmount FROM Claims WHERE ClaimID = @ClaimID";

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
                        NumberOfSessionsTextBox.Text = reader["NumberOfSessions"].ToString();
                        TotalAmountTextBox.Text = reader["TotalAmount"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void NumberOfSessionsTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(NumberOfSessionsTextBox.Text, out int numberOfSessions))
            {
                decimal totalAmount = numberOfSessions * sessionCost;
                TotalAmountTextBox.Text = totalAmount.ToString("F2"); // Format to 2 decimal places
            }
            else
            {
                TotalAmountTextBox.Text = "0.00"; // Reset if input is invalid
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string classTaught = ClassTaughtTextBox.Text;
            int numberOfSessions;
            decimal totalAmount;

            if (int.TryParse(NumberOfSessionsTextBox.Text, out numberOfSessions) &&
                decimal.TryParse(TotalAmountTextBox.Text, out totalAmount))
            {
                UpdateClaim(classTaught, numberOfSessions, totalAmount);
                MessageBox.Show("Claim updated successfully!");
                Close();
            }
            else
            {
                MessageBox.Show("Invalid input for number of sessions or total amount.");
            }
        }

        private void UpdateClaim(string classTaught, int numberOfSessions, decimal totalAmount)
        {
            string query = "UPDATE Claims SET ClassTaught = @ClassTaught, NumberOfSessions = @NumberOfSessions, TotalAmount = @TotalAmount WHERE ClaimID = @ClaimID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassTaught", classTaught);
                command.Parameters.AddWithValue("@NumberOfSessions", numberOfSessions);
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