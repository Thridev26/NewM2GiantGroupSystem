namespace M2GiantGroupSystem
{
    partial class JobsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbl_enterDetails = new System.Windows.Forms.Label();
            this.lblLabourCost = new System.Windows.Forms.Label();
            this.lblDumpingCost = new System.Windows.Forms.Label();
            this.lblFuelCost = new System.Windows.Forms.Label();
            this.lblJobStatus = new System.Windows.Forms.Label();
            this.lbl_EndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.cboJobStatus = new System.Windows.Forms.ComboBox();
            this.txtFuelCost = new System.Windows.Forms.TextBox();
            this.txtLabourCost = new System.Windows.Forms.TextBox();
            this.txtDumpingCost = new System.Windows.Forms.TextBox();
            this.lblTimeSlots = new System.Windows.Forms.Label();
            this.pnlTimeSlots = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveJob = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 1055);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSaveJob);
            this.tabPage1.Controls.Add(this.pnlTimeSlots);
            this.tabPage1.Controls.Add(this.lblTimeSlots);
            this.tabPage1.Controls.Add(this.txtDumpingCost);
            this.tabPage1.Controls.Add(this.txtLabourCost);
            this.tabPage1.Controls.Add(this.txtFuelCost);
            this.tabPage1.Controls.Add(this.cboJobStatus);
            this.tabPage1.Controls.Add(this.dtpEndDate);
            this.tabPage1.Controls.Add(this.dtpStartDate);
            this.tabPage1.Controls.Add(this.lblLabourCost);
            this.tabPage1.Controls.Add(this.lblDumpingCost);
            this.tabPage1.Controls.Add(this.lblFuelCost);
            this.tabPage1.Controls.Add(this.lblJobStatus);
            this.tabPage1.Controls.Add(this.lbl_EndDate);
            this.tabPage1.Controls.Add(this.lblStartDate);
            this.tabPage1.Controls.Add(this.lbl_enterDetails);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1026);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add/Edit Job Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1916, 1026);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View Job Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1916, 1026);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Edit Job Details";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbl_enterDetails
            // 
            this.lbl_enterDetails.AutoSize = true;
            this.lbl_enterDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbl_enterDetails.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_enterDetails.Location = new System.Drawing.Point(489, 122);
            this.lbl_enterDetails.Name = "lbl_enterDetails";
            this.lbl_enterDetails.Size = new System.Drawing.Size(281, 45);
            this.lbl_enterDetails.TabIndex = 42;
            this.lbl_enterDetails.Text = "Enter Job Details";
            // 
            // lblLabourCost
            // 
            this.lblLabourCost.AutoSize = true;
            this.lblLabourCost.BackColor = System.Drawing.Color.Transparent;
            this.lblLabourCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabourCost.Location = new System.Drawing.Point(217, 548);
            this.lblLabourCost.Name = "lblLabourCost";
            this.lblLabourCost.Size = new System.Drawing.Size(173, 38);
            this.lblLabourCost.TabIndex = 48;
            this.lblLabourCost.Text = "Labour Cost";
            // 
            // lblDumpingCost
            // 
            this.lblDumpingCost.AutoSize = true;
            this.lblDumpingCost.BackColor = System.Drawing.Color.Transparent;
            this.lblDumpingCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDumpingCost.Location = new System.Drawing.Point(217, 632);
            this.lblDumpingCost.Name = "lblDumpingCost";
            this.lblDumpingCost.Size = new System.Drawing.Size(205, 38);
            this.lblDumpingCost.TabIndex = 47;
            this.lblDumpingCost.Text = "Dumping Cost";
            // 
            // lblFuelCost
            // 
            this.lblFuelCost.AutoSize = true;
            this.lblFuelCost.BackColor = System.Drawing.Color.Transparent;
            this.lblFuelCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFuelCost.Location = new System.Drawing.Point(217, 463);
            this.lblFuelCost.Name = "lblFuelCost";
            this.lblFuelCost.Size = new System.Drawing.Size(137, 38);
            this.lblFuelCost.TabIndex = 46;
            this.lblFuelCost.Text = "Fuel Cost";
            // 
            // lblJobStatus
            // 
            this.lblJobStatus.AutoSize = true;
            this.lblJobStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblJobStatus.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobStatus.Location = new System.Drawing.Point(217, 380);
            this.lblJobStatus.Name = "lblJobStatus";
            this.lblJobStatus.Size = new System.Drawing.Size(152, 38);
            this.lblJobStatus.TabIndex = 45;
            this.lblJobStatus.Text = "Job Status";
            // 
            // lbl_EndDate
            // 
            this.lbl_EndDate.AutoSize = true;
            this.lbl_EndDate.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndDate.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EndDate.Location = new System.Drawing.Point(217, 296);
            this.lbl_EndDate.Name = "lbl_EndDate";
            this.lbl_EndDate.Size = new System.Drawing.Size(136, 38);
            this.lbl_EndDate.TabIndex = 44;
            this.lbl_EndDate.Text = "End Date";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(217, 210);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(151, 38);
            this.lblStartDate.TabIndex = 43;
            this.lblStartDate.Text = "Start Date";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(452, 226);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(300, 22);
            this.dtpStartDate.TabIndex = 49;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(452, 312);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(300, 22);
            this.dtpEndDate.TabIndex = 50;
            // 
            // cboJobStatus
            // 
            this.cboJobStatus.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboJobStatus.FormattingEnabled = true;
            this.cboJobStatus.Items.AddRange(new object[] {
            "Not Started",
            "In Progress",
            "Completed"});
            this.cboJobStatus.Location = new System.Drawing.Point(452, 380);
            this.cboJobStatus.Name = "cboJobStatus";
            this.cboJobStatus.Size = new System.Drawing.Size(410, 45);
            this.cboJobStatus.TabIndex = 51;
            // 
            // txtFuelCost
            // 
            this.txtFuelCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFuelCost.Location = new System.Drawing.Point(452, 463);
            this.txtFuelCost.Name = "txtFuelCost";
            this.txtFuelCost.Size = new System.Drawing.Size(410, 43);
            this.txtFuelCost.TabIndex = 52;
            // 
            // txtLabourCost
            // 
            this.txtLabourCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabourCost.Location = new System.Drawing.Point(452, 548);
            this.txtLabourCost.Name = "txtLabourCost";
            this.txtLabourCost.Size = new System.Drawing.Size(410, 43);
            this.txtLabourCost.TabIndex = 53;
            // 
            // txtDumpingCost
            // 
            this.txtDumpingCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDumpingCost.Location = new System.Drawing.Point(452, 627);
            this.txtDumpingCost.Name = "txtDumpingCost";
            this.txtDumpingCost.Size = new System.Drawing.Size(410, 43);
            this.txtDumpingCost.TabIndex = 54;
            // 
            // lblTimeSlots
            // 
            this.lblTimeSlots.AutoSize = true;
            this.lblTimeSlots.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeSlots.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeSlots.Location = new System.Drawing.Point(1079, 122);
            this.lblTimeSlots.Name = "lblTimeSlots";
            this.lblTimeSlots.Size = new System.Drawing.Size(433, 45);
            this.lblTimeSlots.TabIndex = 55;
            this.lblTimeSlots.Text = "Select Available Time Slots";
            // 
            // pnlTimeSlots
            // 
            this.pnlTimeSlots.Location = new System.Drawing.Point(1110, 197);
            this.pnlTimeSlots.Name = "pnlTimeSlots";
            this.pnlTimeSlots.Size = new System.Drawing.Size(350, 221);
            this.pnlTimeSlots.TabIndex = 56;
            // 
            // btnSaveJob
            // 
            this.btnSaveJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSaveJob.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveJob.Location = new System.Drawing.Point(123, 787);
            this.btnSaveJob.Name = "btnSaveJob";
            this.btnSaveJob.Size = new System.Drawing.Size(410, 56);
            this.btnSaveJob.TabIndex = 57;
            this.btnSaveJob.Text = "Create & Schedule Job";
            this.btnSaveJob.UseVisualStyleBackColor = false;
            // 
            // JobsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tabControl1);
            this.Name = "JobsForm";
            this.Text = "JobsForm";
            this.Load += new System.EventHandler(this.JobsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lbl_enterDetails;
        private System.Windows.Forms.Label lblLabourCost;
        private System.Windows.Forms.Label lblDumpingCost;
        private System.Windows.Forms.Label lblFuelCost;
        private System.Windows.Forms.Label lblJobStatus;
        private System.Windows.Forms.Label lbl_EndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.ComboBox cboJobStatus;
        private System.Windows.Forms.TextBox txtFuelCost;
        private System.Windows.Forms.TextBox txtLabourCost;
        private System.Windows.Forms.TextBox txtDumpingCost;
        private System.Windows.Forms.FlowLayoutPanel pnlTimeSlots;
        private System.Windows.Forms.Label lblTimeSlots;
        private System.Windows.Forms.Button btnSaveJob;
    }
}