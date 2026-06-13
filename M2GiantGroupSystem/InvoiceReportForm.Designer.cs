namespace M2GiantGroupSystem
{
    partial class InvoiceReportForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSearchByInoiceR = new System.Windows.Forms.Label();
            this.txtSearchJobs = new System.Windows.Forms.TextBox();
            this.jobsReportsDgv = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.InvoiceReport1 = new M2GiantGroupSystem.InvoiceReport();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jobsReportsDgv)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 1055);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblSearchByInoiceR);
            this.tabPage1.Controls.Add(this.txtSearchJobs);
            this.tabPage1.Controls.Add(this.jobsReportsDgv);
            this.tabPage1.Location = new System.Drawing.Point(4, 46);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1005);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Select Job";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(83, 831);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(554, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Will only allow to generate invoice after a job is selected";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGreen;
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(807, 793);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(446, 99);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate Invoice";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1008, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "JobID selected:";
            // 
            // lblSearchByInoiceR
            // 
            this.lblSearchByInoiceR.AutoSize = true;
            this.lblSearchByInoiceR.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchByInoiceR.Location = new System.Drawing.Point(81, 59);
            this.lblSearchByInoiceR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchByInoiceR.Name = "lblSearchByInoiceR";
            this.lblSearchByInoiceR.Size = new System.Drawing.Size(631, 38);
            this.lblSearchByInoiceR.TabIndex = 2;
            this.lblSearchByInoiceR.Text = "Search by client name,surname or site address:";
            // 
            // txtSearchJobs
            // 
            this.txtSearchJobs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.txtSearchJobs.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchJobs.Location = new System.Drawing.Point(88, 120);
            this.txtSearchJobs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchJobs.Name = "txtSearchJobs";
            this.txtSearchJobs.Size = new System.Drawing.Size(641, 43);
            this.txtSearchJobs.TabIndex = 1;
            this.txtSearchJobs.TextChanged += new System.EventHandler(this.txtSearchJobs_TextChanged);
            // 
            // jobsReportsDgv
            // 
            this.jobsReportsDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.jobsReportsDgv.BackgroundColor = System.Drawing.Color.White;
            this.jobsReportsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jobsReportsDgv.Location = new System.Drawing.Point(88, 210);
            this.jobsReportsDgv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.jobsReportsDgv.Name = "jobsReportsDgv";
            this.jobsReportsDgv.ReadOnly = true;
            this.jobsReportsDgv.RowHeadersWidth = 51;
            this.jobsReportsDgv.RowTemplate.Height = 24;
            this.jobsReportsDgv.Size = new System.Drawing.Size(1758, 536);
            this.jobsReportsDgv.TabIndex = 0;
            this.jobsReportsDgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.jobsReportsDgv_CellClick);
            this.jobsReportsDgv.Click += new System.EventHandler(this.jobsReportsDgv_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage2.Controls.Add(this.crystalReportViewer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 46);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(1916, 1005);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Invoice";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(4, 5);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.InvoiceReport1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1908, 995);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // InvoiceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "InvoiceReportForm";
            this.Text = "InvoiceReportForm";
            this.Load += new System.EventHandler(this.InvoiceReportForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jobsReportsDgv)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView jobsReportsDgv;
        private System.Windows.Forms.Label lblSearchByInoiceR;
        private System.Windows.Forms.TextBox txtSearchJobs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private InvoiceReport InvoiceReport1;
        private System.Windows.Forms.Label label2;
    }
}