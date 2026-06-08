namespace M2GiantGroupSystem
{
    partial class client_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(client_MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblSearchResults_A = new System.Windows.Forms.Label();
            this.lblSearchBy_A = new System.Windows.Forms.Label();
            this.tbSearchValue_A = new System.Windows.Forms.TextBox();
            this.lbSearchResults = new System.Windows.Forms.ListBox();
            this.lblSelectCriteria_A = new System.Windows.Forms.Label();
            this.cmbCriteria_A = new System.Windows.Forms.ComboBox();
            this.lblFindClient_A = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.clientBS = new System.Windows.Forms.BindingSource(this.components);
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cboFilterType = new System.Windows.Forms.ComboBox();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.txtSearchV = new System.Windows.Forms.TextBox();
            this.cboSearchColumn = new System.Windows.Forms.ComboBox();
            this.pnlDetailsV = new System.Windows.Forms.Panel();
            this.lblDetailDate = new System.Windows.Forms.Label();
            this.lblDetailType = new System.Windows.Forms.Label();
            this.lblDetailStatus = new System.Windows.Forms.Label();
            this.lblDetailPhone = new System.Windows.Forms.Label();
            this.lblDetailEmail = new System.Windows.Forms.Label();
            this.lblDetailSurname = new System.Windows.Forms.Label();
            this.lblDetailName = new System.Windows.Forms.Label();
            this.lblDetailID = new System.Windows.Forms.Label();
            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.clientTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.ClientTableAdapter();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.pnlDetailsV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 1055);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.btn_addClient);
            this.tabPage1.Controls.Add(this.lbl_enterDetails);
            this.tabPage1.Controls.Add(this.lbl_type);
            this.tabPage1.Controls.Add(this.lbl_status);
            this.tabPage1.Controls.Add(this.lbl_email);
            this.tabPage1.Controls.Add(this.lbl_phone);
            this.tabPage1.Controls.Add(this.lbl_surname);
            this.tabPage1.Controls.Add(this.lbl_name);
            this.tabPage1.Controls.Add(this.cmb_type);
            this.tabPage1.Controls.Add(this.cmb_status);
            this.tabPage1.Controls.Add(this.tb_phone);
            this.tabPage1.Controls.Add(this.tb_email);
            this.tabPage1.Controls.Add(this.tb_surname);
            this.tabPage1.Controls.Add(this.tb_name);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1011);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add client";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btn_addClient
            // 
            this.btn_addClient.BackColor = System.Drawing.Color.DarkGreen;
            this.btn_addClient.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addClient.ForeColor = System.Drawing.Color.White;
            this.btn_addClient.Location = new System.Drawing.Point(811, 706);
            this.btn_addClient.Name = "btn_addClient";
            this.btn_addClient.Size = new System.Drawing.Size(461, 78);
            this.btn_addClient.TabIndex = 42;
            this.btn_addClient.Text = "Add Client";
            this.btn_addClient.UseVisualStyleBackColor = false;
            this.btn_addClient.Click += new System.EventHandler(this.btn_addClient_Click);
            // 
            // lbl_enterDetails
            // 
            this.lbl_enterDetails.AutoSize = true;
            this.lbl_enterDetails.BackColor = System.Drawing.Color.Transparent;
            this.lbl_enterDetails.Font = new System.Drawing.Font("Segoe UI", 31.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_enterDetails.Location = new System.Drawing.Point(773, 43);
            this.lbl_enterDetails.Name = "lbl_enterDetails";
            this.lbl_enterDetails.Size = new System.Drawing.Size(550, 72);
            this.lbl_enterDetails.TabIndex = 41;
            this.lbl_enterDetails.Text = "Enter Client\'s Details";
            // 
            // lbl_type
            // 
            this.lbl_type.AutoSize = true;
            this.lbl_type.BackColor = System.Drawing.Color.Transparent;
            this.lbl_type.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_type.Location = new System.Drawing.Point(329, 515);
            this.lbl_type.Name = "lbl_type";
            this.lbl_type.Size = new System.Drawing.Size(216, 50);
            this.lbl_type.TabIndex = 40;
            this.lbl_type.Text = "Client Type";
            this.lbl_type.Click += new System.EventHandler(this.lbl_type_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.BackColor = System.Drawing.Color.Transparent;
            this.lbl_status.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(329, 604);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(128, 50);
            this.lbl_status.TabIndex = 39;
            this.lbl_status.Text = "Status";
            this.lbl_status.Click += new System.EventHandler(this.lbl_status_Click);
            // 
            // lbl_email
            // 
            this.lbl_email.AutoSize = true;
            this.lbl_email.BackColor = System.Drawing.Color.Transparent;
            this.lbl_email.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_email.Location = new System.Drawing.Point(329, 430);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(378, 50);
            this.lbl_email.TabIndex = 38;
            this.lbl_email.Text = "Client Email Address";
            this.lbl_email.Click += new System.EventHandler(this.lbl_email_Click);
            // 
            // lbl_phone
            // 
            this.lbl_phone.AutoSize = true;
            this.lbl_phone.BackColor = System.Drawing.Color.Transparent;
            this.lbl_phone.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_phone.Location = new System.Drawing.Point(329, 347);
            this.lbl_phone.Name = "lbl_phone";
            this.lbl_phone.Size = new System.Drawing.Size(396, 50);
            this.lbl_phone.TabIndex = 37;
            this.lbl_phone.Text = "Client Phone Number";
            this.lbl_phone.Click += new System.EventHandler(this.lbl_phone_Click);
            // 
            // lbl_surname
            // 
            this.lbl_surname.AutoSize = true;
            this.lbl_surname.BackColor = System.Drawing.Color.Transparent;
            this.lbl_surname.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_surname.Location = new System.Drawing.Point(329, 263);
            this.lbl_surname.Name = "lbl_surname";
            this.lbl_surname.Size = new System.Drawing.Size(287, 50);
            this.lbl_surname.TabIndex = 36;
            this.lbl_surname.Text = "Client Surname";
            this.lbl_surname.Click += new System.EventHandler(this.lbl_surname_Click);
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_name.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(329, 177);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(236, 50);
            this.lbl_name.TabIndex = 35;
            this.lbl_name.Text = "Client Name";
            this.lbl_name.Click += new System.EventHandler(this.lbl_name_Click);
            // 
            // cmb_type
            // 
            this.cmb_type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cmb_type.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Items.AddRange(new object[] {
            "Residential",
            "Commercial",
            "Government"});
            this.cmb_type.Location = new System.Drawing.Point(811, 515);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(461, 53);
            this.cmb_type.TabIndex = 34;
            // 
            // cmb_status
            // 
            this.cmb_status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cmb_status.Enabled = false;
            this.cmb_status.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_status.FormattingEnabled = true;
            this.cmb_status.Items.AddRange(new object[] {
            "Active",
            "Archived "});
            this.cmb_status.Location = new System.Drawing.Point(811, 601);
            this.cmb_status.Name = "cmb_status";
            this.cmb_status.Size = new System.Drawing.Size(461, 53);
            this.cmb_status.TabIndex = 33;
            // 
            // tb_phone
            // 
            this.tb_phone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.tb_phone.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_phone.Location = new System.Drawing.Point(811, 347);
            this.tb_phone.Name = "tb_phone";
            this.tb_phone.Size = new System.Drawing.Size(461, 51);
            this.tb_phone.TabIndex = 32;
            // 
            // tb_email
            // 
            this.tb_email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.tb_email.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_email.Location = new System.Drawing.Point(811, 430);
            this.tb_email.Name = "tb_email";
            this.tb_email.Size = new System.Drawing.Size(461, 51);
            this.tb_email.TabIndex = 31;
            // 
            // tb_surname
            // 
            this.tb_surname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.tb_surname.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_surname.Location = new System.Drawing.Point(811, 260);
            this.tb_surname.Name = "tb_surname";
            this.tb_surname.Size = new System.Drawing.Size(461, 51);
            this.tb_surname.TabIndex = 30;
            // 
            // tb_name
            // 
            this.tb_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.tb_name.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_name.Location = new System.Drawing.Point(811, 177);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(465, 51);
            this.tb_name.TabIndex = 29;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage2.Controls.Add(this.lblSearchResults_A);
            this.tabPage2.Controls.Add(this.lblSearchBy_A);
            this.tabPage2.Controls.Add(this.tbSearchValue_A);
            this.tabPage2.Controls.Add(this.lbSearchResults);
            this.tabPage2.Controls.Add(this.lblSelectCriteria_A);
            this.tabPage2.Controls.Add(this.cmbCriteria_A);
            this.tabPage2.Controls.Add(this.lblFindClient_A);
            this.tabPage2.Controls.Add(this.btnUpdate);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Controls.Add(this.comboBox2);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1916, 1011);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Update client";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // lblSearchResults_A
            // 
            this.lblSearchResults_A.AutoSize = true;
            this.lblSearchResults_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchResults_A.Location = new System.Drawing.Point(811, 181);
            this.lblSearchResults_A.Name = "lblSearchResults_A";
            this.lblSearchResults_A.Size = new System.Drawing.Size(203, 32);
            this.lblSearchResults_A.TabIndex = 58;
            this.lblSearchResults_A.Text = "Search results:";
            // 
            // lblSearchBy_A
            // 
            this.lblSearchBy_A.AutoSize = true;
            this.lblSearchBy_A.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchBy_A.Location = new System.Drawing.Point(1025, 78);
            this.lblSearchBy_A.Name = "lblSearchBy_A";
            this.lblSearchBy_A.Size = new System.Drawing.Size(157, 32);
            this.lblSearchBy_A.TabIndex = 57;
            this.lblSearchBy_A.Text = "Search by..";
            // 
            // tbSearchValue_A
            // 
            this.tbSearchValue_A.Enabled = false;
            this.tbSearchValue_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearchValue_A.Location = new System.Drawing.Point(1031, 118);
            this.tbSearchValue_A.Name = "tbSearchValue_A";
            this.tbSearchValue_A.Size = new System.Drawing.Size(383, 43);
            this.tbSearchValue_A.TabIndex = 56;
            this.tbSearchValue_A.TextChanged += new System.EventHandler(this.tbSearchValue_A_TextChanged);
            // 
            // lbSearchResults
            // 
            this.lbSearchResults.FormattingEnabled = true;
            this.lbSearchResults.ItemHeight = 31;
            this.lbSearchResults.Location = new System.Drawing.Point(1031, 181);
            this.lbSearchResults.Name = "lbSearchResults";
            this.lbSearchResults.ScrollAlwaysVisible = true;
            this.lbSearchResults.Size = new System.Drawing.Size(383, 66);
            this.lbSearchResults.TabIndex = 55;
            this.lbSearchResults.SelectedIndexChanged += new System.EventHandler(this.lbSearchResults_SelectedIndexChanged);
            // 
            // lblSelectCriteria_A
            // 
            this.lblSelectCriteria_A.AutoSize = true;
            this.lblSelectCriteria_A.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectCriteria_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectCriteria_A.Location = new System.Drawing.Point(360, 116);
            this.lblSelectCriteria_A.Name = "lblSelectCriteria_A";
            this.lblSelectCriteria_A.Size = new System.Drawing.Size(309, 38);
            this.lblSelectCriteria_A.TabIndex = 54;
            this.lblSelectCriteria_A.Text = "Select a search criteria";
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
            this.cmbCriteria_A.Location = new System.Drawing.Point(691, 116);
            this.cmbCriteria_A.Name = "cmbCriteria_A";
            this.cmbCriteria_A.Size = new System.Drawing.Size(304, 45);
            this.cmbCriteria_A.TabIndex = 53;
            this.cmbCriteria_A.SelectedIndexChanged += new System.EventHandler(this.cmbCriteria_A_SelectedIndexChanged);
            // 
            // lblFindClient_A
            // 
            this.lblFindClient_A.AutoSize = true;
            this.lblFindClient_A.BackColor = System.Drawing.Color.Transparent;
            this.lblFindClient_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFindClient_A.Location = new System.Drawing.Point(941, 19);
            this.lblFindClient_A.Name = "lblFindClient_A";
            this.lblFindClient_A.Size = new System.Drawing.Size(154, 38);
            this.lblFindClient_A.TabIndex = 52;
            this.lblFindClient_A.Text = "Find client";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(908, 833);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(410, 60);
            this.btnUpdate.TabIndex = 51;
            this.btnUpdate.Text = "Update client details";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(530, 652);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 38);
            this.label1.TabIndex = 50;
            this.label1.Text = "Client Type";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(530, 745);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 38);
            this.label2.TabIndex = 49;
            this.label2.Text = "Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(530, 578);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 38);
            this.label3.TabIndex = 48;
            this.label3.Text = "Client Email Address";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(530, 500);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(300, 38);
            this.label4.TabIndex = 47;
            this.label4.Text = "Client Phone Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(530, 426);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 38);
            this.label5.TabIndex = 46;
            this.label5.Text = "Client Surname";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(530, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 38);
            this.label6.TabIndex = 45;
            this.label6.Text = "Client Name";
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBS, "clientType", true));
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Residential",
            "Commercial",
            "Government"});
            this.comboBox1.Location = new System.Drawing.Point(908, 663);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(410, 45);
            this.comboBox1.TabIndex = 44;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // clientBS
            // 
            this.clientBS.DataMember = "Client";
            this.clientBS.DataSource = this.groupWst1DataSet1;
            this.clientBS.CurrentChanged += new System.EventHandler(this.clientBS_CurrentChanged);
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboBox2
            // 
            this.comboBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBS, "status", true));
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Active",
            "Archived "});
            this.comboBox2.Location = new System.Drawing.Point(908, 742);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(410, 45);
            this.comboBox2.TabIndex = 43;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBS, "phoneNumber", true));
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(908, 500);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(410, 43);
            this.textBox1.TabIndex = 42;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBS, "emailAddress", true));
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(908, 578);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(410, 43);
            this.textBox2.TabIndex = 41;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBS, "clientSurname", true));
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(908, 421);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(410, 43);
            this.textBox3.TabIndex = 40;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBS, "clientName", true));
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(908, 336);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(410, 43);
            this.textBox4.TabIndex = 39;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage3.Controls.Add(this.btnAdd);
            this.tabPage3.Controls.Add(this.btnDelete);
            this.tabPage3.Controls.Add(this.btnEdit);
            this.tabPage3.Controls.Add(this.cboFilterType);
            this.tabPage3.Controls.Add(this.cboFilterStatus);
            this.tabPage3.Controls.Add(this.txtSearchV);
            this.tabPage3.Controls.Add(this.cboSearchColumn);
            this.tabPage3.Controls.Add(this.pnlDetailsV);
            this.tabPage3.Controls.Add(this.dgvClients);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1916, 1011);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "View clients";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(688, 652);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(299, 69);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(321, 652);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(318, 69);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Archive";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(67, 652);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(214, 69);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // cboFilterType
            // 
            this.cboFilterType.FormattingEnabled = true;
            this.cboFilterType.Items.AddRange(new object[] {
            "All Types",
            "Residential",
            "Commercial",
            "Government"});
            this.cboFilterType.Location = new System.Drawing.Point(989, 51);
            this.cboFilterType.Name = "cboFilterType";
            this.cboFilterType.Size = new System.Drawing.Size(202, 39);
            this.cboFilterType.TabIndex = 5;
            this.cboFilterType.SelectedIndexChanged += new System.EventHandler(this.cboFilterType_SelectedIndexChanged);
            // 
            // cboFilterStatus
            // 
            this.cboFilterStatus.FormattingEnabled = true;
            this.cboFilterStatus.Items.AddRange(new object[] {
            "Active",
            "Archived",
            "All Statuses"});
            this.cboFilterStatus.Location = new System.Drawing.Point(688, 51);
            this.cboFilterStatus.Name = "cboFilterStatus";
            this.cboFilterStatus.Size = new System.Drawing.Size(211, 39);
            this.cboFilterStatus.TabIndex = 4;
            this.cboFilterStatus.SelectedIndexChanged += new System.EventHandler(this.cboFilterStatus_SelectedIndexChanged);
            // 
            // txtSearchV
            // 
            this.txtSearchV.Location = new System.Drawing.Point(382, 53);
            this.txtSearchV.Name = "txtSearchV";
            this.txtSearchV.Size = new System.Drawing.Size(244, 38);
            this.txtSearchV.TabIndex = 3;
            this.txtSearchV.TextChanged += new System.EventHandler(this.txtSearchV_TextChanged);
            // 
            // cboSearchColumn
            // 
            this.cboSearchColumn.FormattingEnabled = true;
            this.cboSearchColumn.Location = new System.Drawing.Point(82, 53);
            this.cboSearchColumn.Name = "cboSearchColumn";
            this.cboSearchColumn.Size = new System.Drawing.Size(235, 39);
            this.cboSearchColumn.TabIndex = 2;
            this.cboSearchColumn.SelectedIndexChanged += new System.EventHandler(this.cboSearchColumn_SelectedIndexChanged);
            // 
            // pnlDetailsV
            // 
            this.pnlDetailsV.Controls.Add(this.lblDetailDate);
            this.pnlDetailsV.Controls.Add(this.lblDetailType);
            this.pnlDetailsV.Controls.Add(this.lblDetailStatus);
            this.pnlDetailsV.Controls.Add(this.lblDetailPhone);
            this.pnlDetailsV.Controls.Add(this.lblDetailEmail);
            this.pnlDetailsV.Controls.Add(this.lblDetailSurname);
            this.pnlDetailsV.Controls.Add(this.lblDetailName);
            this.pnlDetailsV.Controls.Add(this.lblDetailID);
            this.pnlDetailsV.Location = new System.Drawing.Point(1423, 128);
            this.pnlDetailsV.Name = "pnlDetailsV";
            this.pnlDetailsV.Size = new System.Drawing.Size(450, 488);
            this.pnlDetailsV.TabIndex = 1;
            // 
            // lblDetailDate
            // 
            this.lblDetailDate.AutoSize = true;
            this.lblDetailDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailDate.Location = new System.Drawing.Point(13, 431);
            this.lblDetailDate.Name = "lblDetailDate";
            this.lblDetailDate.Size = new System.Drawing.Size(89, 26);
            this.lblDetailDate.TabIndex = 13;
            this.lblDetailDate.Text = "label14";
            // 
            // lblDetailType
            // 
            this.lblDetailType.AutoSize = true;
            this.lblDetailType.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailType.Location = new System.Drawing.Point(13, 368);
            this.lblDetailType.Name = "lblDetailType";
            this.lblDetailType.Size = new System.Drawing.Size(89, 26);
            this.lblDetailType.TabIndex = 12;
            this.lblDetailType.Text = "label13";
            // 
            // lblDetailStatus
            // 
            this.lblDetailStatus.AutoSize = true;
            this.lblDetailStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailStatus.Location = new System.Drawing.Point(13, 299);
            this.lblDetailStatus.Name = "lblDetailStatus";
            this.lblDetailStatus.Size = new System.Drawing.Size(89, 26);
            this.lblDetailStatus.TabIndex = 11;
            this.lblDetailStatus.Text = "label12";
            // 
            // lblDetailPhone
            // 
            this.lblDetailPhone.AutoSize = true;
            this.lblDetailPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailPhone.Location = new System.Drawing.Point(13, 236);
            this.lblDetailPhone.Name = "lblDetailPhone";
            this.lblDetailPhone.Size = new System.Drawing.Size(89, 26);
            this.lblDetailPhone.TabIndex = 10;
            this.lblDetailPhone.Text = "label11";
            // 
            // lblDetailEmail
            // 
            this.lblDetailEmail.AutoSize = true;
            this.lblDetailEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailEmail.Location = new System.Drawing.Point(13, 173);
            this.lblDetailEmail.Name = "lblDetailEmail";
            this.lblDetailEmail.Size = new System.Drawing.Size(89, 26);
            this.lblDetailEmail.TabIndex = 9;
            this.lblDetailEmail.Text = "label10";
            // 
            // lblDetailSurname
            // 
            this.lblDetailSurname.AutoSize = true;
            this.lblDetailSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailSurname.Location = new System.Drawing.Point(13, 113);
            this.lblDetailSurname.Name = "lblDetailSurname";
            this.lblDetailSurname.Size = new System.Drawing.Size(76, 26);
            this.lblDetailSurname.TabIndex = 8;
            this.lblDetailSurname.Text = "label9";
            // 
            // lblDetailName
            // 
            this.lblDetailName.AutoSize = true;
            this.lblDetailName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailName.Location = new System.Drawing.Point(13, 57);
            this.lblDetailName.Name = "lblDetailName";
            this.lblDetailName.Size = new System.Drawing.Size(76, 26);
            this.lblDetailName.TabIndex = 7;
            this.lblDetailName.Text = "label8";
            // 
            // lblDetailID
            // 
            this.lblDetailID.AutoSize = true;
            this.lblDetailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailID.Location = new System.Drawing.Point(13, 10);
            this.lblDetailID.Name = "lblDetailID";
            this.lblDetailID.Size = new System.Drawing.Size(76, 26);
            this.lblDetailID.TabIndex = 6;
            this.lblDetailID.Text = "label7";
            // 
            // dgvClients
            // 
            this.dgvClients.ColumnHeadersHeight = 25;
            this.dgvClients.Location = new System.Drawing.Point(67, 128);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.RowHeadersWidth = 51;
            this.dgvClients.RowTemplate.Height = 24;
            this.dgvClients.Size = new System.Drawing.Size(1329, 482);
            this.dgvClients.TabIndex = 0;
            this.dgvClients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClients_CellClick);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage4.Location = new System.Drawing.Point(4, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1916, 1011);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Archive client";
            // 
            // clientTableAdapter1
            // 
            this.clientTableAdapter1.ClearBeforeFill = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1605, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(301, 93);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // client_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tabControl1);
            this.Name = "client_MainForm";
            this.Text = "client_MainForm";
            this.Load += new System.EventHandler(this.client_MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.pnlDetailsV.ResumeLayout(false);
            this.pnlDetailsV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
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
        private System.Windows.Forms.TextBox tb_name;
        private GroupWst1DataSetTableAdapters.ClientTableAdapter clientTableAdapter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblSearchResults_A;
        private System.Windows.Forms.Label lblSearchBy_A;
        private System.Windows.Forms.TextBox tbSearchValue_A;
        private System.Windows.Forms.ListBox lbSearchResults;
        private System.Windows.Forms.Label lblSelectCriteria_A;
        private System.Windows.Forms.ComboBox cmbCriteria_A;
        private System.Windows.Forms.Label lblFindClient_A;
        private GroupWst1DataSet groupWst1DataSet1;
        private System.Windows.Forms.BindingSource clientBS;
        private System.Windows.Forms.ComboBox cboFilterType;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.TextBox txtSearchV;
        private System.Windows.Forms.ComboBox cboSearchColumn;
        private System.Windows.Forms.Panel pnlDetailsV;
        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.Label lblDetailType;
        private System.Windows.Forms.Label lblDetailStatus;
        private System.Windows.Forms.Label lblDetailPhone;
        private System.Windows.Forms.Label lblDetailEmail;
        private System.Windows.Forms.Label lblDetailSurname;
        private System.Windows.Forms.Label lblDetailName;
        private System.Windows.Forms.Label lblDetailID;
        private System.Windows.Forms.Label lblDetailDate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}