using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace M2GiantGroupSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // 1. UI Feedback & State Management
            button1.Enabled = false;
            button1.Text = "Authenticating...";
            this.Cursor = Cursors.WaitCursor;

            // 2. Client-Side Validation
            if (string.IsNullOrWhiteSpace(usernameBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Text))
            {
                ResetLoginUI();
                MessageBox.Show("Credentials required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Secure Connection String
            string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;Trust Server Certificate=True;";

            // 4. Asynchronous Database Operation
            string query = @"SELECT s.staffID, s.passwordHash, r.accessLevel, s.userName 
                     FROM Staff s 
                     JOIN Role r ON s.roleID = r.roleID 
                     WHERE s.userName = @userName";

            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = usernameBox.Text.Trim();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string hashFromDb = reader["passwordHash"].ToString();

                                // 5. Advanced Password Verification
                                bool isValid = await Task.Run(() => BCrypt.Net.BCrypt.Verify(passwordBox.Text, hashFromDb));

                                if (isValid)
                                {
                                    // 6. Populate Session Context
                                    UserSession.StaffID = (int)reader["staffID"];
                                    UserSession.AccessLevel = (int)reader["accessLevel"];
                                    UserSession.UserName = reader["userName"].ToString();

                                    // 7. Success - Navigate to Main Menu
                                    MessageBox.Show($"Welcome back, {UserSession.UserName}.", "System Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 form1 = new Form1();
                                    form1.Show();
                                    this.Hide();
                                }
                                else { TriggerFailedLogin(); }
                            }
                            else { TriggerFailedLogin(); }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // This provides the specific SQL error number and message
                string errorMessage = $"Database Error ({ex.Number}): {ex.Message}";
                MessageBox.Show(errorMessage, "Detailed SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { ResetLoginUI(); }
        }

        private void TriggerFailedLogin()
        {
            // Use generic error for security (prevents username harvesting)
            MessageBox.Show("Invalid username or password.", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordBox.Clear();
        }

        private void ResetLoginUI()
        {
            button1.Enabled = true;
            button1.Text = "LOGIN";
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            Application.Exit();

        }

        private void checkPasswordBox_CheckedChanged(object sender, EventArgs e)
        {
            // If the checkbox is checked, hide the asterisks (show the text)
            if (checkPasswordBox.Checked)
            {
                passwordBox.UseSystemPasswordChar = false;
            }
            // If it's unchecked, bring the asterisks back
            else
            {
                passwordBox.UseSystemPasswordChar = true;
            }
        }

        //This method is for demonstration purposes only. It shows how to hash a password and update it in the database.
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    // 1. Generate the secure hash for an existing password
        //    string realPassword = "branch16"; // The password you want the user to have
        //    string secureHash = BCrypt.Net.BCrypt.HashPassword(realPassword);

        //    // 2. Update your database with this new, long, secure string
        //    // Replace '1' with the specific staffID you are updating
        //    string updateQuery = "UPDATE Staff SET passwordHash = @hash WHERE staffID = 5";

        //    using (SqlConnection conn = new SqlConnection("Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;Trust Server Certificate=True"))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@hash", secureHash);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
