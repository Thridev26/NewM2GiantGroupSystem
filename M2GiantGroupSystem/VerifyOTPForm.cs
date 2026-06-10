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
    public partial class VerifyOTPForm : Form
    {
        private string _userEmail;
        public VerifyOTPForm(string email)
        {
            InitializeComponent();
            _userEmail = email;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (AuthDB.VerifyOTP(_userEmail, txtOTP.Text))
            {
                MessageBox.Show("OTP Verified successfully!");

                // Proceed to the final step: Setting the new password
                ResetPasswordForm resetForm = new ResetPasswordForm(_userEmail);
                resetForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid or expired OTP. Please try again.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
