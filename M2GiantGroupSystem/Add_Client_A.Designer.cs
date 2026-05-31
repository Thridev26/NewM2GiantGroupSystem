namespace M2GiantGroupSystem
{
    partial class Add_Client_A
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
            this.btn_addClient = new System.Windows.Forms.Button();
            this.lbl_enterDetails = new System.Windows.Forms.Label();
            this.lbl_type = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_phone = new System.Windows.Forms.Label();
            this.lbl_surname = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.cmb_status = new System.Windows.Forms.ComboBox();
            this.tb_phone = new System.Windows.Forms.TextBox();
            this.tb_email = new System.Windows.Forms.TextBox();
            this.tb_surname = new System.Windows.Forms.TextBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.clientTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.ClientTableAdapter();
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_addClient
            // 
            this.btn_addClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_addClient.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addClient.Location = new System.Drawing.Point(589, 729);
            this.btn_addClient.Name = "btn_addClient";
            this.btn_addClient.Size = new System.Drawing.Size(410, 56);
            this.btn_addClient.TabIndex = 28;
            this.btn_addClient.Text = "Add Client";
            this.btn_addClient.UseVisualStyleBackColor = false;
            this.btn_addClient.Click += new System.EventHandler(this.btn_addClient_Click_1);
            // 
            // lbl_enterDetails
            // 
            this.lbl_enterDetails.AutoSize = true;
            this.lbl_enterDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbl_enterDetails.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_enterDetails.Location = new System.Drawing.Point(610, 89);
            this.lbl_enterDetails.Name = "lbl_enterDetails";
            this.lbl_enterDetails.Size = new System.Drawing.Size(339, 45);
            this.lbl_enterDetails.TabIndex = 27;
            this.lbl_enterDetails.Text = "Enter Client\'s Details";
            // 
            // lbl_type
            // 
            this.lbl_type.AutoSize = true;
            this.lbl_type.BackColor = System.Drawing.Color.Transparent;
            this.lbl_type.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_type.Location = new System.Drawing.Point(226, 522);
            this.lbl_type.Name = "lbl_type";
            this.lbl_type.Size = new System.Drawing.Size(163, 38);
            this.lbl_type.TabIndex = 26;
            this.lbl_type.Text = "Client Type";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.BackColor = System.Drawing.Color.Transparent;
            this.lbl_status.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(226, 615);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(98, 38);
            this.lbl_status.TabIndex = 25;
            this.lbl_status.Text = "Status";
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.BackColor = System.Drawing.Color.Transparent;
            this.lbl_email.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_email.Location = new System.Drawing.Point(226, 437);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(285, 38);
            this.lbl_email.TabIndex = 24;
            this.lbl_email.Text = "Client Email Address";
            // 
            // lbl_phone
            // 
            this.lbl_phone.AutoSize = true;
            this.lbl_phone.BackColor = System.Drawing.Color.Transparent;
            this.lbl_phone.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_phone.Location = new System.Drawing.Point(226, 354);
            this.lbl_phone.Name = "lbl_phone";
            this.lbl_phone.Size = new System.Drawing.Size(300, 38);
            this.lbl_phone.TabIndex = 23;
            this.lbl_phone.Text = "Client Phone Number";
            // 
            // lbl_surname
            // 
            this.lbl_surname.AutoSize = true;
            this.lbl_surname.BackColor = System.Drawing.Color.Transparent;
            this.lbl_surname.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_surname.Location = new System.Drawing.Point(226, 270);
            this.lbl_surname.Name = "lbl_surname";
            this.lbl_surname.Size = new System.Drawing.Size(218, 38);
            this.lbl_surname.TabIndex = 22;
            this.lbl_surname.Text = "Client Surname";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_name.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(226, 184);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(179, 38);
            this.lbl_name.TabIndex = 21;
            this.lbl_name.Text = "Client Name";
            // 
            // cmb_type
            // 
            this.cmb_type.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Items.AddRange(new object[] {
            "Residential",
            "Commercial",
            "Government"});
            this.cmb_type.Location = new System.Drawing.Point(589, 522);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(410, 45);
            this.cmb_type.TabIndex = 20;
            // 
            // cmb_status
            // 
            this.cmb_status.Enabled = false;
            this.cmb_status.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_status.FormattingEnabled = true;
            this.cmb_status.Items.AddRange(new object[] {
            "Active",
            "Archived "});
            this.cmb_status.Location = new System.Drawing.Point(589, 612);
            this.cmb_status.Name = "cmb_status";
            this.cmb_status.Size = new System.Drawing.Size(410, 45);
            this.cmb_status.TabIndex = 19;
            // 
            // tb_phone
            // 
            this.tb_phone.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_phone.Location = new System.Drawing.Point(589, 354);
            this.tb_phone.Name = "tb_phone";
            this.tb_phone.Size = new System.Drawing.Size(410, 43);
            this.tb_phone.TabIndex = 18;
            // 
            // tb_email
            // 
            this.tb_email.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_email.Location = new System.Drawing.Point(589, 437);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(410, 43);
            this.tb_email.TabIndex = 17;
            // 
            // tb_surname
            // 
            this.tb_surname.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_surname.Location = new System.Drawing.Point(589, 265);
            this.tb_surname.Name = "tb_surname";
            this.tb_surname.Size = new System.Drawing.Size(410, 43);
            this.tb_surname.TabIndex = 16;
            // 
            // tb_name
            // 
            this.tb_name.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_name.Location = new System.Drawing.Point(589, 184);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(410, 43);
            this.tb_name.TabIndex = 15;
            // 
            // clientTableAdapter1
            // 
            this.clientTableAdapter1.ClearBeforeFill = true;
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Add_Client_A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.btn_addClient);
            this.Controls.Add(this.lbl_enterDetails);
            this.Controls.Add(this.lbl_type);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.lbl_email);
            this.Controls.Add(this.lbl_phone);
            this.Controls.Add(this.lbl_surname);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.cmb_type);
            this.Controls.Add(this.cmb_status);
            this.Controls.Add(this.tb_phone);
            this.Controls.Add(this.tb_email);
            this.Controls.Add(this.tb_surname);
            this.Controls.Add(this.tb_name);
            this.Name = "Add_Client_A";
            this.Text = "Add_Client_A";
            this.Load += new System.EventHandler(this.Add_Client_A_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_addClient;
        private System.Windows.Forms.Label lbl_enterDetails;
        private System.Windows.Forms.Label lbl_type;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_phone;
        private System.Windows.Forms.Label lbl_surname;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.ComboBox cmb_type;
        private System.Windows.Forms.ComboBox cmb_status;
        private System.Windows.Forms.TextBox tb_phone;
        private System.Windows.Forms.TextBox tb_email;
        private System.Windows.Forms.TextBox tb_surname;
        private GroupWst1DataSetTableAdapters.ClientTableAdapter clientTableAdapter1;
        private GroupWst1DataSet groupWst1DataSet1;
        private System.Windows.Forms.TextBox tb_name;

        }
}