namespace M2GiantGroupSystem
{
    partial class WeeklyExpensesReportForm
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
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.expensesReport1 = new M2GiantGroupSystem.expensesReport();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dt2
            // 
            this.dt2.Location = new System.Drawing.Point(44, 145);
            this.dt2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dt2.Name = "dt2";
            this.dt2.Size = new System.Drawing.Size(502, 34);
            this.dt2.TabIndex = 0;
            // 
            // dt1
            // 
            this.dt1.Location = new System.Drawing.Point(44, 56);
            this.dt1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(502, 34);
            this.dt1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1242, 788);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.dt1);
            this.tabPage1.Controls.Add(this.dt2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1234, 759);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Select date range";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 401);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(502, 184);
            this.button1.TabIndex = 2;
            this.button1.Text = "generate report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage2.Controls.Add(this.crystalReportViewer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 37);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(1234, 747);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Expenses report";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(4, 5);
            this.crystalReportViewer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.expensesReport1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1226, 737);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelWidth = 275;
            this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // WeeklyExpensesReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1242, 788);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WeeklyExpensesReportForm";
            this.Text = "WeeklyExpensesReportForm";
            this.Load += new System.EventHandler(this.WeeklyExpensesReportForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dt2;
        private System.Windows.Forms.DateTimePicker dt1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private expensesReport expensesReport1;
        private System.Windows.Forms.Button button1;
    }
}