namespace M2GiantGroupSystem
{
    partial class Maintenance
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
            this.components = new System.ComponentModel.Container();
            this.lbl_status = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvMaintenanceHistory = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboServiceType = new System.Windows.Forms.ComboBox();
            this.cboAssetSelection = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.rtbCompletionDetails = new System.Windows.Forms.RichTextBox();
            this.lbl_enterDetails = new System.Windows.Forms.Label();
            this.dtpServiceDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRepairCost = new System.Windows.Forms.TextBox();
            this.lblLabourCost = new System.Windows.Forms.Label();
            this.lblDumpingCost = new System.Windows.Forms.Label();
            this.MainServiceType = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceHistory)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.BackColor = System.Drawing.Color.Transparent;
            this.lbl_status.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(-131, 485);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(128, 50);
            this.lbl_status.TabIndex = 52;
            this.lbl_status.Text = "Status";
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1017);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Maintenance Log";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvMaintenanceHistory);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(967, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(941, 462);
            this.groupBox3.TabIndex = 85;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Maintenance Log History";
            // 
            // dgvMaintenanceHistory
            // 
            this.dgvMaintenanceHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaintenanceHistory.Location = new System.Drawing.Point(48, 87);
            this.dgvMaintenanceHistory.Name = "dgvMaintenanceHistory";
            this.dgvMaintenanceHistory.RowHeadersWidth = 51;
            this.dgvMaintenanceHistory.RowTemplate.Height = 24;
            this.dgvMaintenanceHistory.Size = new System.Drawing.Size(883, 336);
            this.dgvMaintenanceHistory.TabIndex = 81;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(-32, -82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboServiceType);
            this.groupBox1.Controls.Add(this.cboAssetSelection);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnSaveLog);
            this.groupBox1.Controls.Add(this.rtbCompletionDetails);
            this.groupBox1.Controls.Add(this.lbl_enterDetails);
            this.groupBox1.Controls.Add(this.dtpServiceDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRepairCost);
            this.groupBox1.Controls.Add(this.lblLabourCost);
            this.groupBox1.Controls.Add(this.lblDumpingCost);
            this.groupBox1.Controls.Add(this.MainServiceType);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(931, 803);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter Maintenance Log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(228, 50);
            this.label3.TabIndex = 98;
            this.label3.Text = "Select Asset";
            // 
            // cboServiceType
            // 
            this.cboServiceType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cboServiceType.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServiceType.FormattingEnabled = true;
            this.cboServiceType.Items.AddRange(new object[] {
            "Routine Cleaning",
            "Repair",
            "Software Update ",
            "Inspection"});
            this.cboServiceType.Location = new System.Drawing.Point(396, 210);
            this.cboServiceType.Name = "cboServiceType";
            this.cboServiceType.Size = new System.Drawing.Size(410, 45);
            this.cboServiceType.TabIndex = 97;
            // 
            // cboAssetSelection
            // 
            this.cboAssetSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cboAssetSelection.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAssetSelection.FormattingEnabled = true;
            this.cboAssetSelection.Location = new System.Drawing.Point(396, 129);
            this.cboAssetSelection.Name = "cboAssetSelection";
            this.cboAssetSelection.Size = new System.Drawing.Size(410, 45);
            this.cboAssetSelection.TabIndex = 96;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGreen;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(559, 724);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 56);
            this.button1.TabIndex = 95;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSaveLog.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveLog.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSaveLog.Location = new System.Drawing.Point(25, 724);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(410, 56);
            this.btnSaveLog.TabIndex = 94;
            this.btnSaveLog.Text = "Add Maintenance Log";
            this.btnSaveLog.UseVisualStyleBackColor = false;
            // 
            // rtbCompletionDetails
            // 
            this.rtbCompletionDetails.Location = new System.Drawing.Point(396, 475);
            this.rtbCompletionDetails.Name = "rtbCompletionDetails";
            this.rtbCompletionDetails.Size = new System.Drawing.Size(517, 193);
            this.rtbCompletionDetails.TabIndex = 93;
            this.rtbCompletionDetails.Text = "";
            // 
            // lbl_enterDetails
            // 
            this.lbl_enterDetails.AutoSize = true;
            this.lbl_enterDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbl_enterDetails.Font = new System.Drawing.Font("Segoe UI", 31.2F, System.Drawing.FontStyle.Bold);
            this.lbl_enterDetails.Location = new System.Drawing.Point(115, 39);
            this.lbl_enterDetails.Name = "lbl_enterDetails";
            this.lbl_enterDetails.Size = new System.Drawing.Size(0, 70);
            this.lbl_enterDetails.TabIndex = 92;
            // 
            // dtpServiceDate
            // 
            this.dtpServiceDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpServiceDate.Location = new System.Drawing.Point(396, 365);
            this.dtpServiceDate.Name = "dtpServiceDate";
            this.dtpServiceDate.Size = new System.Drawing.Size(410, 43);
            this.dtpServiceDate.TabIndex = 91;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(16, 540);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 50);
            this.label1.TabIndex = 90;
            this.label1.Text = "Completion Details";
            // 
            // txtRepairCost
            // 
            this.txtRepairCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtRepairCost.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepairCost.Location = new System.Drawing.Point(396, 288);
            this.txtRepairCost.Name = "txtRepairCost";
            this.txtRepairCost.Size = new System.Drawing.Size(410, 43);
            this.txtRepairCost.TabIndex = 89;
            // 
            // lblLabourCost
            // 
            this.lblLabourCost.AutoSize = true;
            this.lblLabourCost.BackColor = System.Drawing.Color.Transparent;
            this.lblLabourCost.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblLabourCost.Location = new System.Drawing.Point(27, 281);
            this.lblLabourCost.Name = "lblLabourCost";
            this.lblLabourCost.Size = new System.Drawing.Size(220, 50);
            this.lblLabourCost.TabIndex = 88;
            this.lblLabourCost.Text = "Repair Cost";
            // 
            // lblDumpingCost
            // 
            this.lblDumpingCost.AutoSize = true;
            this.lblDumpingCost.BackColor = System.Drawing.Color.Transparent;
            this.lblDumpingCost.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.lblDumpingCost.Location = new System.Drawing.Point(27, 355);
            this.lblDumpingCost.Name = "lblDumpingCost";
            this.lblDumpingCost.Size = new System.Drawing.Size(239, 50);
            this.lblDumpingCost.TabIndex = 87;
            this.lblDumpingCost.Text = "Service Date";
            // 
            // MainServiceType
            // 
            this.MainServiceType.AutoSize = true;
            this.MainServiceType.BackColor = System.Drawing.Color.Transparent;
            this.MainServiceType.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold);
            this.MainServiceType.Location = new System.Drawing.Point(25, 203);
            this.MainServiceType.Name = "MainServiceType";
            this.MainServiceType.Size = new System.Drawing.Size(241, 50);
            this.MainServiceType.TabIndex = 86;
            this.MainServiceType.Text = "Service Type";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 1055);
            this.tabControl1.TabIndex = 53;
            // 
            // Maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbl_status);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Maintenance";
            this.Text = "Maintenance";
            this.Load += new System.EventHandler(this.Maintenance_Load);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceHistory)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridView dgvMaintenanceHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboServiceType;
        private System.Windows.Forms.ComboBox cboAssetSelection;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.RichTextBox rtbCompletionDetails;
        private System.Windows.Forms.Label lbl_enterDetails;
        private System.Windows.Forms.DateTimePicker dtpServiceDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRepairCost;
        private System.Windows.Forms.Label lblLabourCost;
        private System.Windows.Forms.Label lblDumpingCost;
        private System.Windows.Forms.Label MainServiceType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}