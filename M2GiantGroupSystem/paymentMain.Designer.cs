namespace M2GiantGroupSystem
{
    partial class paymentMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(paymentMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvPayment = new System.Windows.Forms.DataGridView();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSelectedID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvJobLookup = new System.Windows.Forms.DataGridView();
            this.txtJobSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSaveEdit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvEditPayments = new System.Windows.Forms.DataGridView();
            this.txtEditSearch = new System.Windows.Forms.TextBox();
            this.cmbEditStatus = new System.Windows.Forms.ComboBox();
            this.cmbEditMethod = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtEditAmount = new System.Windows.Forms.TextBox();
            this.lblEditSelected = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobLookup)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtSearch);
            this.tabPage1.Controls.Add(this.dgvPayment);
            this.tabPage1.Controls.Add(this.pictureBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 37);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1014);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "View Payments";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(89, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(555, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search by client name / job ID:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(98, 113);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(544, 57);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvPayment
            // 
            this.dgvPayment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.dgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.dgvPayment.Location = new System.Drawing.Point(98, 210);
            this.dgvPayment.Name = "dgvPayment";
            this.dgvPayment.RowHeadersWidth = 51;
            this.dgvPayment.RowTemplate.Height = 24;
            this.dgvPayment.Size = new System.Drawing.Size(1758, 536);
            this.dgvPayment.TabIndex = 2;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::M2GiantGroupSystem.Properties.Resources.logo__bg_removed__EDITED;
            this.pictureBox3.Location = new System.Drawing.Point(1688, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(220, 84);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.lblSelectedID);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.btnAddPayment);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.cmbPaymentMethod);
            this.tabPage2.Controls.Add(this.txtAmount);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 37);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1916, 1014);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Add Payment";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label14.Location = new System.Drawing.Point(1189, 578);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(293, 38);
            this.label14.TabIndex = 12;
            this.label14.Text = "date will appear here";
            // 
            // lblSelectedID
            // 
            this.lblSelectedID.AutoSize = true;
            this.lblSelectedID.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedID.ForeColor = System.Drawing.Color.Orange;
            this.lblSelectedID.Location = new System.Drawing.Point(683, 145);
            this.lblSelectedID.Name = "lblSelectedID";
            this.lblSelectedID.Size = new System.Drawing.Size(389, 50);
            this.lblSelectedID.TabIndex = 11;
            this.lblSelectedID.Text = "No payment selected";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1261, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(382, 50);
            this.label12.TabIndex = 10;
            this.label12.Text = "Add payment details";
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.BackColor = System.Drawing.Color.DarkGreen;
            this.btnAddPayment.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPayment.ForeColor = System.Drawing.Color.White;
            this.btnAddPayment.Location = new System.Drawing.Point(1184, 641);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(561, 62);
            this.btnAddPayment.TabIndex = 9;
            this.btnAddPayment.Text = "Save Payment";
            this.btnAddPayment.UseVisualStyleBackColor = false;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::M2GiantGroupSystem.Properties.Resources.logo__bg_removed__EDITED;
            this.pictureBox1.Location = new System.Drawing.Point(1688, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.panel1.Controls.Add(this.dgvJobLookup);
            this.panel1.Controls.Add(this.txtJobSearch);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(26, 207);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1046, 519);
            this.panel1.TabIndex = 3;
            // 
            // dgvJobLookup
            // 
            this.dgvJobLookup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJobLookup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobLookup.Location = new System.Drawing.Point(31, 125);
            this.dgvJobLookup.Name = "dgvJobLookup";
            this.dgvJobLookup.ReadOnly = true;
            this.dgvJobLookup.RowHeadersWidth = 51;
            this.dgvJobLookup.RowTemplate.Height = 24;
            this.dgvJobLookup.Size = new System.Drawing.Size(986, 361);
            this.dgvJobLookup.TabIndex = 2;
            this.dgvJobLookup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobLookup_CellClick);
            // 
            // txtJobSearch
            // 
            this.txtJobSearch.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobSearch.Location = new System.Drawing.Point(28, 57);
            this.txtJobSearch.Name = "txtJobSearch";
            this.txtJobSearch.Size = new System.Drawing.Size(742, 47);
            this.txtJobSearch.TabIndex = 1;
            this.txtJobSearch.TextChanged += new System.EventHandler(this.txtJobSearch_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(725, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Search for a job using client name, surname, site address or job ID:";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Items.AddRange(new object[] {
            "Cash",
            "Card",
            "EFT",
            "Bank Transfer"});
            this.cmbPaymentMethod.Location = new System.Drawing.Point(1184, 332);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(555, 58);
            this.cmbPaymentMethod.TabIndex = 6;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(1184, 219);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(555, 57);
            this.txtAmount.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1177, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 38);
            this.label6.TabIndex = 4;
            this.label6.Text = "Amount (R):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1177, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(252, 38);
            this.label5.TabIndex = 3;
            this.label5.Text = "Payment Method:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1183, 527);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(481, 38);
            this.label3.TabIndex = 1;
            this.label3.Text = "Payment Date: (set to today\'s date)";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.btnSaveEdit);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.cmbEditStatus);
            this.tabPage3.Controls.Add(this.cmbEditMethod);
            this.tabPage3.Controls.Add(this.pictureBox2);
            this.tabPage3.Controls.Add(this.txtEditAmount);
            this.tabPage3.Controls.Add(this.lblEditSelected);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Location = new System.Drawing.Point(4, 37);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1916, 1014);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Edit Payment";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1198, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(378, 50);
            this.label13.TabIndex = 15;
            this.label13.Text = "Edit payment details";
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSaveEdit.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveEdit.ForeColor = System.Drawing.Color.White;
            this.btnSaveEdit.Location = new System.Drawing.Point(1133, 583);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new System.Drawing.Size(504, 76);
            this.btnSaveEdit.TabIndex = 14;
            this.btnSaveEdit.Text = "Save Changes";
            this.btnSaveEdit.UseVisualStyleBackColor = false;
            this.btnSaveEdit.Click += new System.EventHandler(this.btnSaveEdit_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dgvEditPayments);
            this.panel2.Controls.Add(this.txtEditSearch);
            this.panel2.Location = new System.Drawing.Point(41, 217);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(974, 442);
            this.panel2.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(913, 50);
            this.label7.TabIndex = 15;
            this.label7.Text = "Payment Lookup by client name,surname or job ID:";
            // 
            // dgvEditPayments
            // 
            this.dgvEditPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEditPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEditPayments.Location = new System.Drawing.Point(35, 160);
            this.dgvEditPayments.Name = "dgvEditPayments";
            this.dgvEditPayments.ReadOnly = true;
            this.dgvEditPayments.RowHeadersWidth = 51;
            this.dgvEditPayments.RowTemplate.Height = 24;
            this.dgvEditPayments.Size = new System.Drawing.Size(899, 245);
            this.dgvEditPayments.TabIndex = 7;
            this.dgvEditPayments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEditPayments_CellClick);
            // 
            // txtEditSearch
            // 
            this.txtEditSearch.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEditSearch.Location = new System.Drawing.Point(35, 71);
            this.txtEditSearch.Name = "txtEditSearch";
            this.txtEditSearch.Size = new System.Drawing.Size(899, 57);
            this.txtEditSearch.TabIndex = 6;
            this.txtEditSearch.TextChanged += new System.EventHandler(this.txtEditSearch_TextChanged);
            // 
            // cmbEditStatus
            // 
            this.cmbEditStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cmbEditStatus.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEditStatus.FormattingEnabled = true;
            this.cmbEditStatus.Items.AddRange(new object[] {
            "Pending",
            "Paid ",
            "Partially Paid",
            "Cancelled"});
            this.cmbEditStatus.Location = new System.Drawing.Point(1133, 493);
            this.cmbEditStatus.Name = "cmbEditStatus";
            this.cmbEditStatus.Size = new System.Drawing.Size(504, 58);
            this.cmbEditStatus.TabIndex = 12;
          //  this.cmbEditStatus.SelectedIndexChanged += new System.EventHandler(this.cmbEditStatus_SelectedIndexChanged);
            // 
            // cmbEditMethod
            // 
            this.cmbEditMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.cmbEditMethod.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEditMethod.FormattingEnabled = true;
            this.cmbEditMethod.Items.AddRange(new object[] {
            "Cash",
            "Card",
            "EFT",
            "Bank Transfer"});
            this.cmbEditMethod.Location = new System.Drawing.Point(1133, 342);
            this.cmbEditMethod.Name = "cmbEditMethod";
            this.cmbEditMethod.Size = new System.Drawing.Size(504, 58);
            this.cmbEditMethod.TabIndex = 11;
            this.cmbEditMethod.SelectedIndexChanged += new System.EventHandler(this.cmbEditMethod_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::M2GiantGroupSystem.Properties.Resources.logo__bg_removed__EDITED;
            this.pictureBox2.Location = new System.Drawing.Point(1688, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(220, 84);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // txtEditAmount
            // 
            this.txtEditAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtEditAmount.Enabled = false;
            this.txtEditAmount.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEditAmount.Location = new System.Drawing.Point(1133, 241);
            this.txtEditAmount.Name = "txtEditAmount";
            this.txtEditAmount.Size = new System.Drawing.Size(504, 31);
            this.txtEditAmount.TabIndex = 10;
            this.txtEditAmount.Text = "Cannot edit, to add a payment for a job use the add payment";
            this.txtEditAmount.TextChanged += new System.EventHandler(this.txtEditAmount_TextChanged);
            // 
            // lblEditSelected
            // 
            this.lblEditSelected.AutoSize = true;
            this.lblEditSelected.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditSelected.ForeColor = System.Drawing.Color.Orange;
            this.lblEditSelected.Location = new System.Drawing.Point(634, 154);
            this.lblEditSelected.Name = "lblEditSelected";
            this.lblEditSelected.Size = new System.Drawing.Size(390, 50);
            this.lblEditSelected.TabIndex = 0;
            this.lblEditSelected.Text = "Selected Payment ID:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1126, 425);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(229, 38);
            this.label8.TabIndex = 1;
            this.label8.Text = "Payment Status:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1126, 292);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(252, 38);
            this.label10.TabIndex = 3;
            this.label10.Text = "Payment Method:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1126, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 38);
            this.label9.TabIndex = 2;
            this.label9.Text = "Amount (R):";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // paymentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "paymentMain";
            this.Text = "paymentMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.paymentMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobLookup)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEditPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dgvJobLookup;
        private System.Windows.Forms.TextBox txtJobSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAddPayment;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSaveEdit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvEditPayments;
        private System.Windows.Forms.ComboBox cmbEditStatus;
        private System.Windows.Forms.TextBox txtEditSearch;
        private System.Windows.Forms.ComboBox cmbEditMethod;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtEditAmount;
        private System.Windows.Forms.Label lblEditSelected;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblSelectedID;
        private System.Windows.Forms.Label label14;
    }
}