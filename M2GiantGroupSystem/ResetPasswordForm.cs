using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace M2GiantGroupSystem
{
    public partial class ResetPasswordForm : Form
    {
        // 1. Declare the variable at the class level so it is accessible everywhere
        private string _userEmail;
        public ResetPasswordForm(string email)
        {
            InitializeComponent();
            _userEmail = email; // This saves the email so other methods can use it
        }

        private void btnSaveNewPassword_Click(object sender, EventArgs e)
        {
            // 1. Basic empty check
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please fill in both password fields.");
                return;
            }

            // 2. Matching check (Crucial for user experience)
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match. Please retype them carefully.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Perform the secure update
            AuthDB.UpdatePassword(_userEmail, txtNewPassword.Text);

            MessageBox.Show("Password updated successfully! You can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Close the reset flow and return to login
            this.Close();
        }
    }
}
