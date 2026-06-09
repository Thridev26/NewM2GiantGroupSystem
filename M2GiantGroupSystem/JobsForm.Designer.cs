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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtSelectedQuoteID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvJoin = new System.Windows.Forms.DataGridView();
            this.btnSaveJob = new System.Windows.Forms.Button();
            this.pnlTimeSlots = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTimeSlots = new System.Windows.Forms.Label();
            this.txtDumpingCost = new System.Windows.Forms.TextBox();
            this.txtLabourCost = new System.Windows.Forms.TextBox();
            this.txtFuelCost = new System.Windows.Forms.TextBox();
            this.cboJobStatus = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblLabourCost = new System.Windows.Forms.Label();
            this.lblDumpingCost = new System.Windows.Forms.Label();
            this.lblFuelCost = new System.Windows.Forms.Label();
            this.lblJobStatus = new System.Windows.Forms.Label();
            this.lbl_EndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lbl_enterDetails = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.jobTimeSlotTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobTimeSlotTableAdapter();
            this.jobTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobTableAdapter();
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            this.timeSlotTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.TimeSlotTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
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
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.txtSelectedQuoteID);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.dgvJoin);
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
            // 
            // txtSelectedQuoteID
            // 
            this.txtSelectedQuoteID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtSelectedQuoteID.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSelectedQuoteID.Location = new System.Drawing.Point(269, 415);
            this.txtSelectedQuoteID.Name = "txtSelectedQuoteID";
            this.txtSelectedQuoteID.Size = new System.Drawing.Size(410, 43);
            this.txtSelectedQuoteID.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 50);
            this.label1.TabIndex = 61;
            this.label1.Text = "Quote ID";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGreen;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(460, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 56);
            this.button1.TabIndex = 60;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1592, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(324, 93);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // dgvJoin
            // 
            this.dgvJoin.AllowUserToAddRows = false;
            this.dgvJoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJoin.Location = new System.Drawing.Point(685, 262);
            this.dgvJoin.Name = "dgvJoin";
            this.dgvJoin.RowHeadersWidth = 51;
            this.dgvJoin.RowTemplate.Height = 24;
            this.dgvJoin.Size = new System.Drawing.Size(407, 226);
            this.dgvJoin.TabIndex = 58;
            this.dgvJoin.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJoin_CellClick);
            // 
            // btnSaveJob
            // 
            this.btnSaveJob.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSaveJob.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveJob.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSaveJob.Location = new System.Drawing.Point(13, 464);
            this.btnSaveJob.Name = "btnSaveJob";
            this.btnSaveJob.Size = new System.Drawing.Size(410, 56);
            this.btnSaveJob.TabIndex = 57;
            this.btnSaveJob.Text = "Add Job";
            this.btnSaveJob.UseVisualStyleBackColor = false;
            // 
            // pnlTimeSlots
            // 
            this.pnlTimeSlots.Location = new System.Drawing.Point(685, 97);
            this.pnlTimeSlots.Name = "pnlTimeSlots";
            this.pnlTimeSlots.Size = new System.Drawing.Size(350, 149);
            this.pnlTimeSlots.TabIndex = 56;
            // 
            // lblTimeSlots
            // 
            this.lblTimeSlots.AutoSize = true;
            this.lblTimeSlots.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeSlots.Font = new System.Drawing.Font("Segoe UI", 31.2F, System.Drawing.FontStyle.Bold);
            this.lblTimeSlots.Location = new System.Drawing.Point(662, 15);
            this.lblTimeSlots.Name = "lblTimeSlots";
            this.lblTimeSlots.Size = new System.Drawing.Size(681, 70);
            this.lblTimeSlots.TabIndex = 55;
            this.lblTimeSlots.Text = "Select Available Time Slots";
            // 
            // txtDumpingCost
            // 
            this.txtDumpingCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtDumpingCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDumpingCost.Location = new System.Drawing.Point(269, 361);
            this.txtDumpingCost.Name = "txtDumpingCost";
            this.txtDumpingCost.Size = new System.Drawing.Size(410, 43);
            this.txtDumpingCost.TabIndex = 54;
            // 
            // txtLabourCost
            // 
            this.txtLabourCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtLabourCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLabourCost.Location = new System.Drawing.Point(269, 301);
            this.txtLabourCost.Name = "txtLabourCost";
            this.txtLabourCost.Size = new System.Drawing.Size(410, 43);
            this.txtLabourCost.TabIndex = 53;
            // 
            // txtFuelCost
            // 
            this.txtFuelCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtFuelCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFuelCost.Location = new System.Drawing.Point(269, 251);
            this.txtFuelCost.Name = "txtFuelCost";
            this.txtFuelCost.Size = new System.Drawing.Size(410, 43);
            this.txtFuelCost.TabIndex = 52;
            // 
            // cboJobStatus
            // 
            this.cboJobStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cboJobStatus.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboJobStatus.FormattingEnabled = true;
            this.cboJobStatus.Items.AddRange(new object[] {
            "Not Started",
            "In Progress",
            "Completed"});
            this.cboJobStatus.Location = new System.Drawing.Point(269, 190);
            this.cboJobStatus.Name = "cboJobStatus";
            this.cboJobStatus.Size = new System.Drawing.Size(410, 45);
            this.cboJobStatus.TabIndex = 51;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(269, 147);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(300, 22);
            this.dtpEndDate.TabIndex = 50;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(269, 97);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(300, 22);
            this.dtpStartDate.TabIndex = 49;
            // 
            // lblLabourCost
            // 
            this.lblLabourCost.AutoSize = true;
            this.lblLabourCost.BackColor = System.Drawing.Color.Transparent;
            this.lblLabourCost.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblLabourCost.Location = new System.Drawing.Point(4, 294);
            this.lblLabourCost.Name = "lblLabourCost";
            this.lblLabourCost.Size = new System.Drawing.Size(230, 50);
            this.lblLabourCost.TabIndex = 48;
            this.lblLabourCost.Text = "Labour Cost";
            // 
            // lblDumpingCost
            // 
            this.lblDumpingCost.AutoSize = true;
            this.lblDumpingCost.BackColor = System.Drawing.Color.Transparent;
            this.lblDumpingCost.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblDumpingCost.Location = new System.Drawing.Point(4, 354);
            this.lblDumpingCost.Name = "lblDumpingCost";
            this.lblDumpingCost.Size = new System.Drawing.Size(270, 50);
            this.lblDumpingCost.TabIndex = 47;
            this.lblDumpingCost.Text = "Dumping Cost";
            // 
            // lblFuelCost
            // 
            this.lblFuelCost.AutoSize = true;
            this.lblFuelCost.BackColor = System.Drawing.Color.Transparent;
            this.lblFuelCost.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblFuelCost.Location = new System.Drawing.Point(4, 244);
            this.lblFuelCost.Name = "lblFuelCost";
            this.lblFuelCost.Size = new System.Drawing.Size(180, 50);
            this.lblFuelCost.TabIndex = 46;
            this.lblFuelCost.Text = "Fuel Cost";
            // 
            // lblJobStatus
            // 
            this.lblJobStatus.AutoSize = true;
            this.lblJobStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblJobStatus.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblJobStatus.Location = new System.Drawing.Point(4, 183);
            this.lblJobStatus.Name = "lblJobStatus";
            this.lblJobStatus.Size = new System.Drawing.Size(200, 50);
            this.lblJobStatus.TabIndex = 45;
            this.lblJobStatus.Text = "Job Status";
            this.lblJobStatus.Click += new System.EventHandler(this.lblJobStatus_Click);
            // 
            // lbl_EndDate
            // 
            this.lbl_EndDate.AutoSize = true;
            this.lbl_EndDate.BackColor = System.Drawing.Color.Transparent;
            this.lbl_EndDate.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lbl_EndDate.Location = new System.Drawing.Point(4, 125);
            this.lbl_EndDate.Name = "lbl_EndDate";
            this.lbl_EndDate.Size = new System.Drawing.Size(178, 50);
            this.lbl_EndDate.TabIndex = 44;
            this.lbl_EndDate.Text = "End Date";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblStartDate.Location = new System.Drawing.Point(4, 75);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(197, 50);
            this.lblStartDate.TabIndex = 43;
            this.lblStartDate.Text = "Start Date";
            // 
            // lbl_enterDetails
            // 
            this.lbl_enterDetails.AutoSize = true;
            this.lbl_enterDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbl_enterDetails.Font = new System.Drawing.Font("Segoe UI", 31.2F, System.Drawing.FontStyle.Bold);
            this.lbl_enterDetails.Location = new System.Drawing.Point(65, 15);
            this.lbl_enterDetails.Name = "lbl_enterDetails";
            this.lbl_enterDetails.Size = new System.Drawing.Size(440, 70);
            this.lbl_enterDetails.TabIndex = 42;
            this.lbl_enterDetails.Text = "Enter Job Details";
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
            // jobTimeSlotTableAdapter1
            // 
            this.jobTimeSlotTableAdapter1.ClearBeforeFill = true;
            // 
            // jobTableAdapter1
            // 
            this.jobTableAdapter1.ClearBeforeFill = true;
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // timeSlotTableAdapter1
            // 
            this.timeSlotTableAdapter1.ClearBeforeFill = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvJoin;
        private GroupWst1DataSetTableAdapters.JobTimeSlotTableAdapter jobTimeSlotTableAdapter1;
        private GroupWst1DataSetTableAdapters.JobTableAdapter jobTableAdapter1;
        private GroupWst1DataSet groupWst1DataSet1;
        private GroupWst1DataSetTableAdapters.TimeSlotTableAdapter timeSlotTableAdapter1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSelectedQuoteID;
    }
}