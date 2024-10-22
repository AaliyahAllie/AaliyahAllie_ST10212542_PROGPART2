﻿using System.Windows;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace AaliyahAllie_ST10212542_PROGPART2
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the values from the form
            string role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string firstName = FirstNameTextBox.Text.Trim();
            string lastName = LastNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();  // This should be hashed in a real system!
            string phoneNumber = PhoneNumberTextBox.Text.Trim();

            // Validate form input
            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please fill out all fields.");
                return;
            }

            try
            {
                // Address of SQL server and database 
                string connectionString = "Data Source=hp820g4\\SQLEXPRESS;Initial Catalog=POE;Integrated Security=True;";

                // Establish connection
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Open Connection
                    con.Open();

                    // Hash the password (this is just a placeholder, implement a real hashing algorithm)
                    string passwordHash = password; // Replace this with a real hash function for security

                    // SQL Query with all required fields, including email
                    string query = "INSERT INTO AccountUser (FirstName, LastName, Email, PhoneNumber, PasswordHash, AccountType) " +
                                   "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @PasswordHash, @AccountType)";

                    // Execute query with parameters
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Email", email);  // Add the email parameter
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@AccountType", role);

                        cmd.ExecuteNonQuery();
                    }

                    // Close Connection
                    con.Close();
                }

                MessageBox.Show("Account successfully created.");
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
