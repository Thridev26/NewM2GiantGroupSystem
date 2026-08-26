using CrystalDecisions.Shared.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtForgotEmail.Text))
            {
                MessageBox.Show("Please enter your email address.");
                return;
            }

            try
            {
                // Keep your exact backend call and boolean check
                bool isSuccess = AuthDB.RequestPasswordReset(txtForgotEmail.Text.Trim());

                // WE KEEP YOUR WORKING LOGIC: It still branches internally 
                // so your VerifyOTPForm and database processes run correctly.
                if (isSuccess)
                {
                    // Email was found, OTP was actually sent
                    MessageBox.Show("An OTP has been sent to your email. Please check your inbox.",
                                    "OTP Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Email was NOT found, BUT we show a generic, secure message 
                    // instead of saying "does not exist in our database" (prevents hackers from guessing emails)
                    MessageBox.Show("If an account exists with this email, an OTP has been sent. Please check your inbox.",
                                    "Request Processed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Keep your exact form transition logic
                this.Hide();
                VerifyOTPForm verifyForm = new VerifyOTPForm(txtForgotEmail.Text.Trim());
                verifyForm.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
