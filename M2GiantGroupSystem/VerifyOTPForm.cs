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
            string status = AuthDB.VerifyOTP(_userEmail, txtOTP.Text);

            switch (status)
            {
                case "Verified":
                    MessageBox.Show("OTP Verified successfully!");

                    // Proceed to the final step: Setting the new password
                    ResetPasswordForm resetForm = new ResetPasswordForm(_userEmail);
                    resetForm.ShowDialog();
                    this.Close();
                    break;

                case "Expired":
                    MessageBox.Show("The OTP you entered has expired. Please request a new one.");
                    break;

                case "Invalid":
                default:
                    MessageBox.Show("The OTP you entered is incorrect. Please double-check your code.");
                    break;
            }
            //if (AuthDB.VerifyOTP(_userEmail, txtOTP.Text))
            //{
            //    MessageBox.Show("OTP Verified successfully!");

            //    // Proceed to the final step: Setting the new password
            //    ResetPasswordForm resetForm = new ResetPasswordForm(_userEmail);
            //    resetForm.ShowDialog();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid or expired OTP. Please try again.");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VerifyOTPForm_Load(object sender, EventArgs e)
        {

        }
    }
    
}
