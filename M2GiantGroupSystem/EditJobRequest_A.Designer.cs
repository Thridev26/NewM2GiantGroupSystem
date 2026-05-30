namespace M2GiantGroupSystem
{
    partial class EditJobRequest_A
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
            this.lblSearchCriteria_A = new System.Windows.Forms.Label();
            this.tbSearchValue_A = new System.Windows.Forms.TextBox();
            this.dgv_clientJoinJobRequest = new System.Windows.Forms.DataGridView();
            this.tbSiteAddress = new System.Windows.Forms.TextBox();
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            this.tbLong = new System.Windows.Forms.TextBox();
            this.tbLat = new System.Windows.Forms.TextBox();
            this.tbDateRecieved = new System.Windows.Forms.TextBox();
            this.lblSAddress = new System.Windows.Forms.Label();
            this.lblDateRecieved = new System.Windows.Forms.Label();
            this.lblLong = new System.Windows.Forms.Label();
            this.lblLat = new System.Windows.Forms.Label();
            this.cmbRS = new System.Windows.Forms.ComboBox();
            this.cmbUL = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.jobRequestTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobRequestTableAdapter();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnMap = new System.Windows.Forms.Button();
            this.lbl_ID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientJoinJobRequest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearchCriteria_A
            // 
            this.lblSearchCriteria_A.AutoSize = true;
            this.lblSearchCriteria_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchCriteria_A.Location = new System.Drawing.Point(12, 57);
            this.lblSearchCriteria_A.Name = "lblSearchCriteria_A";
            this.lblSearchCriteria_A.Size = new System.Drawing.Size(476, 38);
            this.lblSearchCriteria_A.TabIndex = 0;
            this.lblSearchCriteria_A.Text = "Search by name,email or site address";
            // 
            // tbSearchValue_A
            // 
            this.tbSearchValue_A.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbSearchValue_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearchValue_A.Location = new System.Drawing.Point(567, 54);
            this.tbSearchValue_A.Name = "tbSearchValue_A";
            this.tbSearchValue_A.Size = new System.Drawing.Size(479, 43);
            this.tbSearchValue_A.TabIndex = 1;
            this.tbSearchValue_A.TextChanged += new System.EventHandler(this.tbSearchValue_A_TextChanged);
            // 
            // dgv_clientJoinJobRequest
            // 
            this.dgv_clientJoinJobRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientJoinJobRequest.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgv_clientJoinJobRequest.Location = new System.Drawing.Point(12, 140);
            this.dgv_clientJoinJobRequest.Name = "dgv_clientJoinJobRequest";
            this.dgv_clientJoinJobRequest.RowHeadersWidth = 51;
            this.dgv_clientJoinJobRequest.RowTemplate.Height = 24;
            this.dgv_clientJoinJobRequest.Size = new System.Drawing.Size(1150, 150);
            this.dgv_clientJoinJobRequest.TabIndex = 2;
            this.dgv_clientJoinJobRequest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientJoinJobRequest_CellClick);
            this.dgv_clientJoinJobRequest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientJoinJobRequest_CellContentClick);
            // 
            // tbSiteAddress
            // 
            this.tbSiteAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbSiteAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.siteAddress", true));
            this.tbSiteAddress.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSiteAddress.Location = new System.Drawing.Point(385, 341);
            this.tbSiteAddress.Name = "tbSiteAddress";
            this.tbSiteAddress.Size = new System.Drawing.Size(479, 43);
            this.tbSiteAddress.TabIndex = 3;
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tbLong
            // 
            this.tbLong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbLong.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.longitude", true));
            this.tbLong.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLong.Location = new System.Drawing.Point(385, 414);
            this.tbLong.Name = "tbLong";
            this.tbLong.Size = new System.Drawing.Size(479, 43);
            this.tbLong.TabIndex = 4;
            // 
            // tbLat
            // 
            this.tbLat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbLat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.latitude", true));
            this.tbLat.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLat.Location = new System.Drawing.Point(385, 483);
            this.tbLat.Name = "tbLat";
            this.tbLat.Size = new System.Drawing.Size(479, 43);
            this.tbLat.TabIndex = 5;
            // 
            // tbDateRecieved
            // 
            this.tbDateRecieved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbDateRecieved.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.dateRecieved", true));
            this.tbDateRecieved.Enabled = false;
            this.tbDateRecieved.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDateRecieved.Location = new System.Drawing.Point(385, 722);
            this.tbDateRecieved.Name = "tbDateRecieved";
            this.tbDateRecieved.Size = new System.Drawing.Size(479, 43);
            this.tbDateRecieved.TabIndex = 6;
            // 
            // lblSAddress
            // 
            this.lblSAddress.AutoSize = true;
            this.lblSAddress.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSAddress.Location = new System.Drawing.Point(67, 341);
            this.lblSAddress.Name = "lblSAddress";
            this.lblSAddress.Size = new System.Drawing.Size(166, 38);
            this.lblSAddress.TabIndex = 7;
            this.lblSAddress.Text = "Site address";
            // 
            // lblDateRecieved
            // 
            this.lblDateRecieved.AutoSize = true;
            this.lblDateRecieved.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateRecieved.Location = new System.Drawing.Point(71, 722);
            this.lblDateRecieved.Name = "lblDateRecieved";
            this.lblDateRecieved.Size = new System.Drawing.Size(187, 38);
            this.lblDateRecieved.TabIndex = 8;
            this.lblDateRecieved.Text = "Date recieved";
            // 
            // lblLong
            // 
            this.lblLong.AutoSize = true;
            this.lblLong.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLong.Location = new System.Drawing.Point(71, 414);
            this.lblLong.Name = "lblLong";
            this.lblLong.Size = new System.Drawing.Size(141, 38);
            this.lblLong.TabIndex = 9;
            this.lblLong.Text = "Longitude";
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLat.Location = new System.Drawing.Point(71, 483);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(116, 38);
            this.lblLat.TabIndex = 10;
            this.lblLat.Text = "Latitude";
            // 
            // cmbRS
            // 
            this.cmbRS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbRS.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.requestSource", true));
            this.cmbRS.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRS.FormattingEnabled = true;
            this.cmbRS.Items.AddRange(new object[] {
            "Whatsapp",
            "Phone call",
            "Walk in",
            "Other"});
            this.cmbRS.Location = new System.Drawing.Point(385, 553);
            this.cmbRS.Name = "cmbRS";
            this.cmbRS.Size = new System.Drawing.Size(479, 45);
            this.cmbRS.TabIndex = 11;
            this.cmbRS.SelectedIndexChanged += new System.EventHandler(this.cmbRS_SelectedIndexChanged);
            // 
            // cmbUL
            // 
            this.cmbUL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbUL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.urgencyLevel", true));
            this.cmbUL.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUL.FormattingEnabled = true;
            this.cmbUL.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Not specified"});
            this.cmbUL.Location = new System.Drawing.Point(385, 616);
            this.cmbUL.Name = "cmbUL";
            this.cmbUL.Size = new System.Drawing.Size(479, 45);
            this.cmbUL.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 553);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 38);
            this.label1.TabIndex = 13;
            this.label1.Text = "Request source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(67, 619);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 38);
            this.label2.TabIndex = 14;
            this.label2.Text = "Urgency level";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.groupWst1DataSet1, "JobRequest.siteEvaluationDate", true));
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(385, 784);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(479, 43);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 772);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(326, 38);
            this.label3.TabIndex = 16;
            this.label3.Text = "Set a site evaluation date";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.Location = new System.Drawing.Point(395, 871);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(469, 59);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // jobRequestTableAdapter1
            // 
            this.jobRequestTableAdapter1.ClearBeforeFill = true;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupWst1DataSet1, "JobRequest.status", true));
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Evaluation date set",
            "Site Evaluated",
            "Quote sent",
            "Quote Accepted",
            "Cancelled"});
            this.cmbStatus.Location = new System.Drawing.Point(385, 671);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(479, 45);
            this.cmbStatus.TabIndex = 18;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(71, 678);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(91, 38);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Status";
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(999, 601);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(141, 60);
            this.btnMap.TabIndex = 20;
            this.btnMap.Text = "Display map";
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // lbl_ID
            // 
            this.lbl_ID.AutoSize = true;
            this.lbl_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ID.Location = new System.Drawing.Point(948, 341);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(176, 32);
            this.lbl_ID.TabIndex = 21;
            this.lbl_ID.Text = "Selected id:";
            // 
            // EditJobRequest_A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.lbl_ID);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbUL);
            this.Controls.Add(this.cmbRS);
            this.Controls.Add(this.lblLat);
            this.Controls.Add(this.lblLong);
            this.Controls.Add(this.lblDateRecieved);
            this.Controls.Add(this.lblSAddress);
            this.Controls.Add(this.tbDateRecieved);
            this.Controls.Add(this.tbLat);
            this.Controls.Add(this.tbLong);
            this.Controls.Add(this.tbSiteAddress);
            this.Controls.Add(this.dgv_clientJoinJobRequest);
            this.Controls.Add(this.tbSearchValue_A);
            this.Controls.Add(this.lblSearchCriteria_A);
            this.Name = "EditJobRequest_A";
            this.Text = "EditJobRequest_A";
            this.Load += new System.EventHandler(this.EditJobRequest_A_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientJoinJobRequest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearchCriteria_A;
        private System.Windows.Forms.TextBox tbSearchValue_A;
        private System.Windows.Forms.DataGridView dgv_clientJoinJobRequest;
        private System.Windows.Forms.TextBox tbSiteAddress;
        private System.Windows.Forms.TextBox tbLong;
        private System.Windows.Forms.TextBox tbLat;
        private System.Windows.Forms.TextBox tbDateRecieved;
        private System.Windows.Forms.Label lblSAddress;
        private System.Windows.Forms.Label lblDateRecieved;
        private System.Windows.Forms.Label lblLong;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.ComboBox cmbRS;
        private System.Windows.Forms.ComboBox cmbUL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private GroupWst1DataSet groupWst1DataSet1;
        private GroupWst1DataSetTableAdapters.JobRequestTableAdapter jobRequestTableAdapter1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Label lbl_ID;
    }
}