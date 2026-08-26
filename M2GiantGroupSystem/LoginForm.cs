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
        // Add these at the top of your class
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public LoginForm()
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
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
            string query = @"SELECT s.staffID, s.passwordHash, r.accessLevel, s.userName, s.firstName, s.lastName 
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
                                    UserSession.StaffID = Convert.ToInt32(reader["staffID"]);
                                    UserSession.AccessLevel = Convert.ToInt32(reader["accessLevel"]);
                                    UserSession.UserName = reader["userName"].ToString();
                                    UserSession.FirstName = reader["firstName"].ToString(); // Save the first name
                                    UserSession.LastName = reader["lastName"].ToString();

                                    // 7. Success - Navigate to Main Menu
                                    MessageBox.Show($"Welcome back, {UserSession.FirstName} {UserSession.LastName}.", "System Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 form1 = new Form1(UserSession.FirstName, UserSession.LastName, UserSession.AccessLevel.ToString());
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // This opens the Forgot Password form
            ForgotPasswordForm forgotPass = new ForgotPasswordForm();
            forgotPass.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
        }       

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.ThemeChanged -= ApplyTheme;
            base.OnFormClosed(e);
        }
        private void ApplyTheme()
        {
            if (ThemeManager.IsDarkMode)
                ThemeManager.ApplyTheme(this);
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
    }
}
