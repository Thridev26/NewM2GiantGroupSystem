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
                // 1. Capture the result
                bool isSuccess = AuthDB.RequestPasswordReset(txtForgotEmail.Text);

                if (isSuccess)
                {
                    // 2. Success path: Email found
                    MessageBox.Show("An OTP has been sent to your email. Please check your inbox.",
                                    "OTP Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Move to verification
                    this.Hide();
                    VerifyOTPForm verifyForm = new VerifyOTPForm(txtForgotEmail.Text);
                    verifyForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    // 4. Failure path: Email not found
                    MessageBox.Show("The email address you entered does not exist in our database. Please check your spelling.",
                                    "Email Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //    if (string.IsNullOrWhiteSpace(txtForgotEmail.Text))
            //    {
            //        MessageBox.Show("Please enter your email address.");
            //        return;
            //    }

            //    try
            //    {
            //        // This calls the AuthDB method
            //        AuthDB.RequestPasswordReset(txtForgotEmail.Text);

            //        // ALWAYS show the same message regardless of whether the email was found
            //        MessageBox.Show("An OTP has been sent to your email. \nCheck your spam/junk/promotions folder if you don't see it.",
            //                        "Request Processed", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        // Close this form and open the OTP verification form
            //        this.Hide();
            //        VerifyOTPForm verifyForm = new VerifyOTPForm(txtForgotEmail.Text);
            //        verifyForm.ShowDialog();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Full exception details including inner exceptions and stack trace
            //MessageBox.Show("An error occurred:\n\n" + ex.ToString(),
            //                "System Error",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Error);
            //    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
