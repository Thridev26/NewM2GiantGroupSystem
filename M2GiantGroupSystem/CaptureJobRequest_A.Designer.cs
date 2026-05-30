namespace M2GiantGroupSystem
{
    partial class CaptureJobRequest_A
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
            this.lblFindClient_A = new System.Windows.Forms.Label();
            this.cmbCriteria_A = new System.Windows.Forms.ComboBox();
            this.lblSelectCriteria_A = new System.Windows.Forms.Label();
            this.lbSearchResults = new System.Windows.Forms.ListBox();
            this.tbName_A = new System.Windows.Forms.TextBox();
            this.tbSearchValue_A = new System.Windows.Forms.TextBox();
            this.lblSearchBy_A = new System.Windows.Forms.Label();
            this.lblSearchResults_A = new System.Windows.Forms.Label();
            this.tbEmail_A = new System.Windows.Forms.TextBox();
            this.tbAddress_A = new System.Windows.Forms.TextBox();
            this.lblName_A = new System.Windows.Forms.Label();
            this.lblSurname_A = new System.Windows.Forms.Label();
            this.lblAddress_A = new System.Windows.Forms.Label();
            this.cmbRequestSource_A = new System.Windows.Forms.ComboBox();
            this.cmbUrgencyLevel_A = new System.Windows.Forms.ComboBox();
            this.lblRequestSource_A = new System.Windows.Forms.Label();
            this.lblUrgencyLevel_A = new System.Windows.Forms.Label();
            this.clbItems = new System.Windows.Forms.CheckedListBox();
            this.lblSelectItems = new System.Windows.Forms.Label();
            this.btnCapture = new System.Windows.Forms.Button();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            this.clientTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.ClientTableAdapter();
            this.jobRequestTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobRequestTableAdapter();
            this.jobTypeTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobTypeTableAdapter();
            this.requestItemTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.RequestItemTableAdapter();
            this.tbLat_A = new System.Windows.Forms.TextBox();
            this.tbLong_A = new System.Windows.Forms.TextBox();
            this.lblLat_A = new System.Windows.Forms.Label();
            this.lblLong_A = new System.Windows.Forms.Label();
            this.btnDisplayMap_A = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFindClient_A
            // 
            this.lblFindClient_A.AutoSize = true;
            this.lblFindClient_A.BackColor = System.Drawing.Color.Transparent;
            this.lblFindClient_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFindClient_A.Location = new System.Drawing.Point(695, 58);
            this.lblFindClient_A.Name = "lblFindClient_A";
            this.lblFindClient_A.Size = new System.Drawing.Size(154, 38);
            this.lblFindClient_A.TabIndex = 0;
            this.lblFindClient_A.Text = "Find client";
            this.lblFindClient_A.Click += new System.EventHandler(this.lblFindClient_A_Click);
            // 
            // cmbCriteria_A
            // 
            this.cmbCriteria_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCriteria_A.FormattingEnabled = true;
            this.cmbCriteria_A.Items.AddRange(new object[] {
            "Name",
            "Surname",
            "Email",
            "Phone Number"});
            this.cmbCriteria_A.Location = new System.Drawing.Point(393, 173);
            this.cmbCriteria_A.Name = "cmbCriteria_A";
            this.cmbCriteria_A.Size = new System.Drawing.Size(285, 45);
            this.cmbCriteria_A.TabIndex = 1;
            this.cmbCriteria_A.SelectedIndexChanged += new System.EventHandler(this.cmbCriteria_A_SelectedIndexChanged);
            // 
            // lblSelectCriteria_A
            // 
            this.lblSelectCriteria_A.AutoSize = true;
            this.lblSelectCriteria_A.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectCriteria_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectCriteria_A.Location = new System.Drawing.Point(43, 173);
            this.lblSelectCriteria_A.Name = "lblSelectCriteria_A";
            this.lblSelectCriteria_A.Size = new System.Drawing.Size(309, 38);
            this.lblSelectCriteria_A.TabIndex = 2;
            this.lblSelectCriteria_A.Text = "Select a search criteria";
            // 
            // lbSearchResults
            // 
            this.lbSearchResults.FormattingEnabled = true;
            this.lbSearchResults.ItemHeight = 16;
            this.lbSearchResults.Location = new System.Drawing.Point(842, 235);
            this.lbSearchResults.Name = "lbSearchResults";
            this.lbSearchResults.Size = new System.Drawing.Size(479, 52);
            this.lbSearchResults.TabIndex = 4;
            this.lbSearchResults.SelectedIndexChanged += new System.EventHandler(this.lbSearchResults_SelectedIndexChanged);
            // 
            // tbName_A
            // 
            this.tbName_A.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "clientName", true));
            this.tbName_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbName_A.Location = new System.Drawing.Point(393, 358);
            this.tbName_A.Name = "tbName_A";
            this.tbName_A.Size = new System.Drawing.Size(532, 43);
            this.tbName_A.TabIndex = 5;
            // 
            // tbSearchValue_A
            // 
            this.tbSearchValue_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearchValue_A.Location = new System.Drawing.Point(842, 173);
            this.tbSearchValue_A.Name = "tbSearchValue_A";
            this.tbSearchValue_A.Size = new System.Drawing.Size(479, 43);
            this.tbSearchValue_A.TabIndex = 6;
            this.tbSearchValue_A.TextChanged += new System.EventHandler(this.tbSearchValue_A_TextChanged);
            // 
            // lblSearchBy_A
            // 
            this.lblSearchBy_A.AutoSize = true;
            this.lblSearchBy_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchBy_A.Location = new System.Drawing.Point(847, 124);
            this.lblSearchBy_A.Name = "lblSearchBy_A";
            this.lblSearchBy_A.Size = new System.Drawing.Size(157, 32);
            this.lblSearchBy_A.TabIndex = 7;
            this.lblSearchBy_A.Text = "Search by..";
            this.lblSearchBy_A.Click += new System.EventHandler(this.lblSearchBy_A_Click);
            // 
            // lblSearchResults_A
            // 
            this.lblSearchResults_A.AutoSize = true;
            this.lblSearchResults_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchResults_A.Location = new System.Drawing.Point(633, 235);
            this.lblSearchResults_A.Name = "lblSearchResults_A";
            this.lblSearchResults_A.Size = new System.Drawing.Size(203, 32);
            this.lblSearchResults_A.TabIndex = 8;
            this.lblSearchResults_A.Text = "Search results:";
            // 
            // tbEmail_A
            // 
            this.tbEmail_A.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "emailAddress", true));
            this.tbEmail_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmail_A.Location = new System.Drawing.Point(393, 442);
            this.tbEmail_A.Name = "tbEmail_A";
            this.tbEmail_A.Size = new System.Drawing.Size(532, 43);
            this.tbEmail_A.TabIndex = 9;
            // 
            // tbAddress_A
            // 
            this.tbAddress_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAddress_A.Location = new System.Drawing.Point(393, 522);
            this.tbAddress_A.Name = "tbAddress_A";
            this.tbAddress_A.Size = new System.Drawing.Size(532, 43);
            this.tbAddress_A.TabIndex = 10;
            // 
            // lblName_A
            // 
            this.lblName_A.AutoSize = true;
            this.lblName_A.BackColor = System.Drawing.Color.Transparent;
            this.lblName_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName_A.Location = new System.Drawing.Point(105, 361);
            this.lblName_A.Name = "lblName_A";
            this.lblName_A.Size = new System.Drawing.Size(174, 38);
            this.lblName_A.TabIndex = 12;
            this.lblName_A.Text = "Client name";
            // 
            // lblSurname_A
            // 
            this.lblSurname_A.AutoSize = true;
            this.lblSurname_A.BackColor = System.Drawing.Color.Transparent;
            this.lblSurname_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurname_A.Location = new System.Drawing.Point(105, 442);
            this.lblSurname_A.Name = "lblSurname_A";
            this.lblSurname_A.Size = new System.Drawing.Size(214, 38);
            this.lblSurname_A.TabIndex = 13;
            this.lblSurname_A.Text = "Client surname";
            // 
            // lblAddress_A
            // 
            this.lblAddress_A.AutoSize = true;
            this.lblAddress_A.BackColor = System.Drawing.Color.Transparent;
            this.lblAddress_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress_A.Location = new System.Drawing.Point(105, 525);
            this.lblAddress_A.Name = "lblAddress_A";
            this.lblAddress_A.Size = new System.Drawing.Size(174, 38);
            this.lblAddress_A.TabIndex = 14;
            this.lblAddress_A.Text = "Site address";
            // 
            // cmbRequestSource_A
            // 
            this.cmbRequestSource_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRequestSource_A.FormattingEnabled = true;
            this.cmbRequestSource_A.Items.AddRange(new object[] {
            "Whatsapp",
            "Phone call",
            "Walk in",
            "Other"});
            this.cmbRequestSource_A.Location = new System.Drawing.Point(393, 616);
            this.cmbRequestSource_A.Name = "cmbRequestSource_A";
            this.cmbRequestSource_A.Size = new System.Drawing.Size(532, 45);
            this.cmbRequestSource_A.TabIndex = 15;
            // 
            // cmbUrgencyLevel_A
            // 
            this.cmbUrgencyLevel_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUrgencyLevel_A.FormattingEnabled = true;
            this.cmbUrgencyLevel_A.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High",
            "Not specified"});
            this.cmbUrgencyLevel_A.Location = new System.Drawing.Point(393, 702);
            this.cmbUrgencyLevel_A.Name = "cmbUrgencyLevel_A";
            this.cmbUrgencyLevel_A.Size = new System.Drawing.Size(532, 45);
            this.cmbUrgencyLevel_A.TabIndex = 16;
            // 
            // lblRequestSource_A
            // 
            this.lblRequestSource_A.AutoSize = true;
            this.lblRequestSource_A.BackColor = System.Drawing.Color.Transparent;
            this.lblRequestSource_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestSource_A.Location = new System.Drawing.Point(105, 623);
            this.lblRequestSource_A.Name = "lblRequestSource_A";
            this.lblRequestSource_A.Size = new System.Drawing.Size(214, 38);
            this.lblRequestSource_A.TabIndex = 17;
            this.lblRequestSource_A.Text = "Request source";
            // 
            // lblUrgencyLevel_A
            // 
            this.lblUrgencyLevel_A.AutoSize = true;
            this.lblUrgencyLevel_A.BackColor = System.Drawing.Color.Transparent;
            this.lblUrgencyLevel_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrgencyLevel_A.Location = new System.Drawing.Point(105, 702);
            this.lblUrgencyLevel_A.Name = "lblUrgencyLevel_A";
            this.lblUrgencyLevel_A.Size = new System.Drawing.Size(194, 38);
            this.lblUrgencyLevel_A.TabIndex = 18;
            this.lblUrgencyLevel_A.Text = "Urgency level";
            // 
            // clbItems
            // 
            this.clbItems.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbItems.FormattingEnabled = true;
            this.clbItems.Items.AddRange(new object[] {
            "Tree Felling",
            "Grass Cutting",
            "Tree Planting",
            "Vegetation Clearance",
            "Hedge Trimming"});
            this.clbItems.Location = new System.Drawing.Point(393, 781);
            this.clbItems.Name = "clbItems";
            this.clbItems.Size = new System.Drawing.Size(555, 80);
            this.clbItems.TabIndex = 19;
            // 
            // lblSelectItems
            // 
            this.lblSelectItems.AutoSize = true;
            this.lblSelectItems.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectItems.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectItems.Location = new System.Drawing.Point(57, 793);
            this.lblSelectItems.Name = "lblSelectItems";
            this.lblSelectItems.Size = new System.Drawing.Size(313, 38);
            this.lblSelectItems.TabIndex = 20;
            this.lblSelectItems.Text = "Select requested items";
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCapture.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapture.Location = new System.Drawing.Point(393, 904);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(555, 65);
            this.btnCapture.TabIndex = 21;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataMember = "Client";
            this.clientBindingSource.DataSource = this.groupWst1DataSet1;
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // clientTableAdapter1
            // 
            this.clientTableAdapter1.ClearBeforeFill = true;
            // 
            // jobRequestTableAdapter1
            // 
            this.jobRequestTableAdapter1.ClearBeforeFill = true;
            // 
            // jobTypeTableAdapter1
            // 
            this.jobTypeTableAdapter1.ClearBeforeFill = true;
            // 
            // requestItemTableAdapter1
            // 
            this.requestItemTableAdapter1.ClearBeforeFill = true;
            // 
            // tbLat_A
            // 
            this.tbLat_A.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "emailAddress", true));
            this.tbLat_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLat_A.Location = new System.Drawing.Point(1281, 538);
            this.tbLat_A.Name = "tbLat_A";
            this.tbLat_A.Size = new System.Drawing.Size(274, 43);
            this.tbLat_A.TabIndex = 22;
            // 
            // tbLong_A
            // 
            this.tbLong_A.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "emailAddress", true));
            this.tbLong_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLong_A.Location = new System.Drawing.Point(972, 538);
            this.tbLong_A.Name = "tbLong_A";
            this.tbLong_A.Size = new System.Drawing.Size(274, 43);
            this.tbLong_A.TabIndex = 23;
            // 
            // lblLat_A
            // 
            this.lblLat_A.AutoSize = true;
            this.lblLat_A.BackColor = System.Drawing.Color.Transparent;
            this.lblLat_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLat_A.Location = new System.Drawing.Point(1341, 497);
            this.lblLat_A.Name = "lblLat_A";
            this.lblLat_A.Size = new System.Drawing.Size(125, 38);
            this.lblLat_A.TabIndex = 24;
            this.lblLat_A.Text = "Latitude";
            // 
            // lblLong_A
            // 
            this.lblLong_A.AutoSize = true;
            this.lblLong_A.BackColor = System.Drawing.Color.Transparent;
            this.lblLong_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLong_A.Location = new System.Drawing.Point(965, 497);
            this.lblLong_A.Name = "lblLong_A";
            this.lblLong_A.Size = new System.Drawing.Size(150, 38);
            this.lblLong_A.TabIndex = 25;
            this.lblLong_A.Text = "Longitude";
            // 
            // btnDisplayMap_A
            // 
            this.btnDisplayMap_A.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDisplayMap_A.Location = new System.Drawing.Point(1582, 538);
            this.btnDisplayMap_A.Name = "btnDisplayMap_A";
            this.btnDisplayMap_A.Size = new System.Drawing.Size(162, 43);
            this.btnDisplayMap_A.TabIndex = 26;
            this.btnDisplayMap_A.Text = "Display map";
            this.btnDisplayMap_A.UseVisualStyleBackColor = false;
            this.btnDisplayMap_A.Click += new System.EventHandler(this.btnDisplayMap_A_Click);
            // 
            // CaptureJobRequest_A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.btnDisplayMap_A);
            this.Controls.Add(this.lblLong_A);
            this.Controls.Add(this.lblLat_A);
            this.Controls.Add(this.tbLong_A);
            this.Controls.Add(this.tbLat_A);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.lblSelectItems);
            this.Controls.Add(this.clbItems);
            this.Controls.Add(this.lblUrgencyLevel_A);
            this.Controls.Add(this.lblRequestSource_A);
            this.Controls.Add(this.cmbUrgencyLevel_A);
            this.Controls.Add(this.cmbRequestSource_A);
            this.Controls.Add(this.lblAddress_A);
            this.Controls.Add(this.lblSurname_A);
            this.Controls.Add(this.lblName_A);
            this.Controls.Add(this.tbAddress_A);
            this.Controls.Add(this.tbEmail_A);
            this.Controls.Add(this.lblSearchResults_A);
            this.Controls.Add(this.lblSearchBy_A);
            this.Controls.Add(this.tbSearchValue_A);
            this.Controls.Add(this.tbName_A);
            this.Controls.Add(this.lbSearchResults);
            this.Controls.Add(this.lblSelectCriteria_A);
            this.Controls.Add(this.cmbCriteria_A);
            this.Controls.Add(this.lblFindClient_A);
            this.Name = "CaptureJobRequest_A";
            this.Text = "CaptureJobRequest_A";
            this.Load += new System.EventHandler(this.CaptureJobRequest_A_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFindClient_A;
        private System.Windows.Forms.ComboBox cmbCriteria_A;
        private System.Windows.Forms.Label lblSelectCriteria_A;
        private System.Windows.Forms.ListBox lbSearchResults;
        private System.Windows.Forms.TextBox tbName_A;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private GroupWst1DataSet groupWst1DataSet1;
        private GroupWst1DataSetTableAdapters.ClientTableAdapter clientTableAdapter1;
        private System.Windows.Forms.TextBox tbSearchValue_A;
        private System.Windows.Forms.Label lblSearchBy_A;
        private System.Windows.Forms.Label lblSearchResults_A;
        private System.Windows.Forms.TextBox tbEmail_A;
        private System.Windows.Forms.TextBox tbAddress_A;
        private System.Windows.Forms.Label lblName_A;
        private System.Windows.Forms.Label lblSurname_A;
        private System.Windows.Forms.Label lblAddress_A;
        private System.Windows.Forms.ComboBox cmbRequestSource_A;
        private System.Windows.Forms.ComboBox cmbUrgencyLevel_A;
        private System.Windows.Forms.Label lblRequestSource_A;
        private System.Windows.Forms.Label lblUrgencyLevel_A;
        private System.Windows.Forms.CheckedListBox clbItems;
        private System.Windows.Forms.Label lblSelectItems;
        private System.Windows.Forms.Button btnCapture;
        private GroupWst1DataSetTableAdapters.JobRequestTableAdapter jobRequestTableAdapter1;
        private GroupWst1DataSetTableAdapters.JobTypeTableAdapter jobTypeTableAdapter1;
        private GroupWst1DataSetTableAdapters.RequestItemTableAdapter requestItemTableAdapter1;
        private System.Windows.Forms.TextBox tbLat_A;
        private System.Windows.Forms.TextBox tbLong_A;
        private System.Windows.Forms.Label lblLat_A;
        private System.Windows.Forms.Label lblLong_A;
        private System.Windows.Forms.Button btnDisplayMap_A;
    }
}