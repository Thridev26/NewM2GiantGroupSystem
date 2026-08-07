namespace M2GiantGroupSystem
{
    partial class JobSitePhotos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobSitePhotos));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnUpload = new System.Windows.Forms.Button();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rbAfter = new System.Windows.Forms.RadioButton();
            this.rbBefore = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dgvJobsAdd = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelectedJob = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dgvJobs = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.pbLargeView = new System.Windows.Forms.PictureBox();
            this.flpThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobsAdd)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLargeView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1924, 1055);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage1.Controls.Add(this.btnUpload);
            this.tabPage1.Controls.Add(this.pbPreview);
            this.tabPage1.Controls.Add(this.btnBrowse);
            this.tabPage1.Controls.Add(this.rbAfter);
            this.tabPage1.Controls.Add(this.rbBefore);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 46);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1916, 1005);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add job site photo";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.DarkGreen;
            this.btnUpload.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(1261, 778);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(398, 71);
            this.btnUpload.TabIndex = 76;
            this.btnUpload.Text = "Upload image";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.Image = ((System.Drawing.Image)(resources.GetObject("pbPreview.Image")));
            this.pbPreview.Location = new System.Drawing.Point(1101, 309);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(741, 401);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 75;
            this.pbPreview.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(219)))), ((int)(((byte)(117)))));
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(1101, 187);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(741, 68);
            this.btnBrowse.TabIndex = 74;
            this.btnBrowse.Text = "Browse photos";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // rbAfter
            // 
            this.rbAfter.AutoSize = true;
            this.rbAfter.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAfter.Location = new System.Drawing.Point(1309, 709);
            this.rbAfter.Name = "rbAfter";
            this.rbAfter.Size = new System.Drawing.Size(124, 54);
            this.rbAfter.TabIndex = 73;
            this.rbAfter.Text = "After";
            this.rbAfter.UseVisualStyleBackColor = true;
            // 
            // rbBefore
            // 
            this.rbBefore.AutoSize = true;
            this.rbBefore.Checked = true;
            this.rbBefore.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBefore.Location = new System.Drawing.Point(1101, 709);
            this.rbBefore.Name = "rbBefore";
            this.rbBefore.Size = new System.Drawing.Size(149, 54);
            this.rbBefore.TabIndex = 72;
            this.rbBefore.TabStop = true;
            this.rbBefore.Text = "Before";
            this.rbBefore.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 31.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1182, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(570, 70);
            this.label10.TabIndex = 71;
            this.label10.Text = "Add a photo for a job ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.dgvJobsAdd);
            this.panel2.Location = new System.Drawing.Point(66, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 779);
            this.panel2.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(323, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 41);
            this.label3.TabIndex = 52;
            this.label3.Text = "Find job";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(476, 38);
            this.label5.TabIndex = 47;
            this.label5.Text = "Search by name,email or site address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(219)))), ((int)(((byte)(117)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(26, 588);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 50);
            this.label6.TabIndex = 51;
            this.label6.Text = "JobID selected:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(25, 130);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(469, 43);
            this.textBox2.TabIndex = 48;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dgvJobsAdd
            // 
            this.dgvJobsAdd.AllowUserToAddRows = false;
            this.dgvJobsAdd.AllowUserToDeleteRows = false;
            this.dgvJobsAdd.AllowUserToResizeRows = false;
            this.dgvJobsAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobsAdd.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvJobsAdd.Location = new System.Drawing.Point(23, 188);
            this.dgvJobsAdd.Name = "dgvJobsAdd";
            this.dgvJobsAdd.ReadOnly = true;
            this.dgvJobsAdd.RowHeadersWidth = 51;
            this.dgvJobsAdd.RowTemplate.Height = 24;
            this.dgvJobsAdd.Size = new System.Drawing.Size(954, 350);
            this.dgvJobsAdd.TabIndex = 49;
            this.dgvJobsAdd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobsAdd_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.BackColor = System.Drawing.Color.SeaShell;
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.pbLargeView);
            this.tabPage2.Controls.Add(this.flpThumbnails);
            this.tabPage2.Location = new System.Drawing.Point(4, 46);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1916, 1005);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View photos";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGreen;
            this.button2.Enabled = false;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(1253, 868);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 64);
            this.button2.TabIndex = 74;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGreen;
            this.button1.Enabled = false;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(1038, 868);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 64);
            this.button1.TabIndex = 73;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblSelectedJob);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.dgvJobs);
            this.panel1.Location = new System.Drawing.Point(66, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 779);
            this.panel1.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(323, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 41);
            this.label1.TabIndex = 52;
            this.label1.Text = "Find job";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(476, 38);
            this.label2.TabIndex = 47;
            this.label2.Text = "Search by name,email or site address";
            // 
            // lblSelectedJob
            // 
            this.lblSelectedJob.AutoSize = true;
            this.lblSelectedJob.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(219)))), ((int)(((byte)(117)))));
            this.lblSelectedJob.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedJob.Location = new System.Drawing.Point(26, 588);
            this.lblSelectedJob.Name = "lblSelectedJob";
            this.lblSelectedJob.Size = new System.Drawing.Size(285, 50);
            this.lblSelectedJob.TabIndex = 51;
            this.lblSelectedJob.Text = "JobID selected:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(25, 130);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(469, 43);
            this.textBox1.TabIndex = 48;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dgvJobs
            // 
            this.dgvJobs.AllowUserToAddRows = false;
            this.dgvJobs.AllowUserToDeleteRows = false;
            this.dgvJobs.AllowUserToResizeRows = false;
            this.dgvJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJobs.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvJobs.Location = new System.Drawing.Point(23, 188);
            this.dgvJobs.Name = "dgvJobs";
            this.dgvJobs.ReadOnly = true;
            this.dgvJobs.RowHeadersWidth = 51;
            this.dgvJobs.RowTemplate.Height = 24;
            this.dgvJobs.Size = new System.Drawing.Size(879, 350);
            this.dgvJobs.TabIndex = 49;
            this.dgvJobs.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJobs_CellClick_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1032, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(665, 38);
            this.label4.TabIndex = 72;
            this.label4.Text = "Click on a site photo to allow editng and deleting";
            // 
            // pbLargeView
            // 
            this.pbLargeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.pbLargeView.Image = ((System.Drawing.Image)(resources.GetObject("pbLargeView.Image")));
            this.pbLargeView.Location = new System.Drawing.Point(1038, 348);
            this.pbLargeView.Name = "pbLargeView";
            this.pbLargeView.Size = new System.Drawing.Size(711, 501);
            this.pbLargeView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLargeView.TabIndex = 71;
            this.pbLargeView.TabStop = false;
            // 
            // flpThumbnails
            // 
            this.flpThumbnails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(198)))), ((int)(((byte)(138)))));
            this.flpThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpThumbnails.Location = new System.Drawing.Point(1038, 76);
            this.flpThumbnails.Name = "flpThumbnails";
            this.flpThumbnails.Size = new System.Drawing.Size(711, 230);
            this.flpThumbnails.TabIndex = 70;
            // 
            // JobSitePhotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.tabControl1);
            this.Name = "JobSitePhotos";
            this.Text = "JobSitePhotos";
            this.Load += new System.EventHandler(this.JobSitePhotos_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobsAdd)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLargeView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSelectedJob;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dgvJobs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbLargeView;
        private System.Windows.Forms.FlowLayoutPanel flpThumbnails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dgvJobsAdd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RadioButton rbAfter;
        private System.Windows.Forms.RadioButton rbBefore;
        private System.Windows.Forms.Label label10;
    }
}