namespace M2GiantGroupSystem
{
    partial class ForgotPasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtForgotEmail = new System.Windows.Forms.TextBox();
            this.btnSendOTP = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your Email Address:";
            // 
            // txtForgotEmail
            // 
            this.txtForgotEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtForgotEmail.Location = new System.Drawing.Point(463, 87);
            this.txtForgotEmail.Name = "txtForgotEmail";
            this.txtForgotEmail.Size = new System.Drawing.Size(328, 34);
            this.txtForgotEmail.TabIndex = 1;
            // 
            // btnSendOTP
            // 
            this.btnSendOTP.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSendOTP.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSendOTP.Location = new System.Drawing.Point(515, 151);
            this.btnSendOTP.Name = "btnSendOTP";
            this.btnSendOTP.Size = new System.Drawing.Size(276, 60);
            this.btnSendOTP.TabIndex = 2;
            this.btnSendOTP.Text = "Send Verification Code";
            this.btnSendOTP.UseVisualStyleBackColor = false;
            this.btnSendOTP.Click += new System.EventHandler(this.btnSendOTP_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGreen;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(163, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 60);
            this.button1.TabIndex = 3;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ForgotPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(953, 347);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSendOTP);
            this.Controls.Add(this.txtForgotEmail);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ForgotPasswordForm";
            this.Text = "ForgotPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtForgotEmail;
        private System.Windows.Forms.Button btnSendOTP;
        private System.Windows.Forms.Button button1;
    }
}