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
                // This calls the AuthDB method
                AuthDB.RequestPasswordReset(txtForgotEmail.Text);

                // ALWAYS show the same message regardless of whether the email was found
                MessageBox.Show("If a matching account was found, an OTP has been sent to your email.",
                                "Request Processed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close this form and open the OTP verification form
                this.Hide();
                VerifyOTPForm verifyForm = new VerifyOTPForm(txtForgotEmail.Text);
                verifyForm.Show();
            }
            catch (Exception ex)
            {
                // Full exception details including inner exceptions and stack trace
        MessageBox.Show("An error occurred:\n\n" + ex.ToString(),
                        "System Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
    }
}
