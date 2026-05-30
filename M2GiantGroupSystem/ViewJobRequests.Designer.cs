namespace M2GiantGroupSystem
{
    partial class ViewJobRequests
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
            this.dgvJoin = new System.Windows.Forms.DataGridView();
            this.lblFilter_A = new System.Windows.Forms.Label();
            this.cmbFilter_A = new System.Windows.Forms.ComboBox();
            this.cmbStatus_A = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblDateRecieved = new System.Windows.Forms.Label();
            this.cmbJobType = new System.Windows.Forms.ComboBox();
            this.lblJobType = new System.Windows.Forms.Label();
            this.btnFilter_A = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJoin)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvJoin
            // 
            this.dgvJoin.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvJoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJoin.GridColor = System.Drawing.Color.Green;
            this.dgvJoin.Location = new System.Drawing.Point(163, 236);
            this.dgvJoin.Name = "dgvJoin";
            this.dgvJoin.RowHeadersWidth = 51;
            this.dgvJoin.RowTemplate.Height = 24;
            this.dgvJoin.Size = new System.Drawing.Size(1479, 415);
            this.dgvJoin.TabIndex = 0;
            // 
            // lblFilter_A
            // 
            this.lblFilter_A.AutoSize = true;
            this.lblFilter_A.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter_A.Location = new System.Drawing.Point(159, 69);
            this.lblFilter_A.Name = "lblFilter_A";
            this.lblFilter_A.Size = new System.Drawing.Size(212, 23);
            this.lblFilter_A.TabIndex = 1;
            this.lblFilter_A.Text = "Select criteria to filter by";
            // 
            // cmbFilter_A
            // 
            this.cmbFilter_A.FormattingEnabled = true;
            this.cmbFilter_A.Items.AddRange(new object[] {
            "Status",
            "Job Type",
            "Date recieved"});
            this.cmbFilter_A.Location = new System.Drawing.Point(408, 69);
            this.cmbFilter_A.Name = "cmbFilter_A";
            this.cmbFilter_A.Size = new System.Drawing.Size(201, 24);
            this.cmbFilter_A.TabIndex = 2;
            this.cmbFilter_A.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_A_SelectedIndexChanged);
            // 
            // cmbStatus_A
            // 
            this.cmbStatus_A.FormattingEnabled = true;
            this.cmbStatus_A.Items.AddRange(new object[] {
            "Pending",
            "Evaluation date set",
            "Site evaluated",
            "Quote sent",
            "Quote accepted",
            "Cancelled"});
            this.cmbStatus_A.Location = new System.Drawing.Point(695, 68);
            this.cmbStatus_A.Name = "cmbStatus_A";
            this.cmbStatus_A.Size = new System.Drawing.Size(298, 24);
            this.cmbStatus_A.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(692, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 16);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Select status";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(1087, 68);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // lblDateRecieved
            // 
            this.lblDateRecieved.AutoSize = true;
            this.lblDateRecieved.Location = new System.Drawing.Point(1084, 40);
            this.lblDateRecieved.Name = "lblDateRecieved";
            this.lblDateRecieved.Size = new System.Drawing.Size(131, 16);
            this.lblDateRecieved.TabIndex = 6;
            this.lblDateRecieved.Text = "Select date recieved";
            // 
            // cmbJobType
            // 
            this.cmbJobType.FormattingEnabled = true;
            this.cmbJobType.Items.AddRange(new object[] {
            "Tree Felling",
            "Grass Cutting",
            "Tree Planting",
            "Vegetation Clearance",
            "Hedge Trimming"});
            this.cmbJobType.Location = new System.Drawing.Point(1398, 68);
            this.cmbJobType.Name = "cmbJobType";
            this.cmbJobType.Size = new System.Drawing.Size(244, 24);
            this.cmbJobType.TabIndex = 7;
            // 
            // lblJobType
            // 
            this.lblJobType.AutoSize = true;
            this.lblJobType.Location = new System.Drawing.Point(1404, 40);
            this.lblJobType.Name = "lblJobType";
            this.lblJobType.Size = new System.Drawing.Size(96, 16);
            this.lblJobType.TabIndex = 8;
            this.lblJobType.Text = "Select job type";
            // 
            // btnFilter_A
            // 
            this.btnFilter_A.Location = new System.Drawing.Point(258, 149);
            this.btnFilter_A.Name = "btnFilter_A";
            this.btnFilter_A.Size = new System.Drawing.Size(242, 45);
            this.btnFilter_A.TabIndex = 9;
            this.btnFilter_A.Text = "Search";
            this.btnFilter_A.UseVisualStyleBackColor = true;
            this.btnFilter_A.Click += new System.EventHandler(this.btnFilter_A_Click);
            // 
            // ViewJobRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.btnFilter_A);
            this.Controls.Add(this.lblJobType);
            this.Controls.Add(this.cmbJobType);
            this.Controls.Add(this.lblDateRecieved);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus_A);
            this.Controls.Add(this.cmbFilter_A);
            this.Controls.Add(this.lblFilter_A);
            this.Controls.Add(this.dgvJoin);
            this.Name = "ViewJobRequests";
            this.Text = "ViewJobRequests";
            this.Load += new System.EventHandler(this.ViewJobRequests_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJoin;
        private System.Windows.Forms.Label lblFilter_A;
        private System.Windows.Forms.ComboBox cmbFilter_A;
        private System.Windows.Forms.ComboBox cmbStatus_A;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblDateRecieved;
        private System.Windows.Forms.ComboBox cmbJobType;
        private System.Windows.Forms.Label lblJobType;
        private System.Windows.Forms.Button btnFilter_A;
    }
}