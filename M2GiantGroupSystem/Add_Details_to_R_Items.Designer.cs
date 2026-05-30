namespace M2GiantGroupSystem
{
    partial class Add_Details_to_R_Items
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
            this.tbSearchValue_A = new System.Windows.Forms.TextBox();
            this.lblSearchCriteria_A = new System.Windows.Forms.Label();
            this.dgv_clientJoinJobRequest = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.jobTypeTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobTypeTableAdapter();
            this.requestItemTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.RequestItemTableAdapter();
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            this.jobDetailTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.JobDetailTableAdapter();
            this.btnSave = new System.Windows.Forms.Button();
            this.itemDetailTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.ItemDetailTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientJoinJobRequest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbSearchValue_A
            // 
            this.tbSearchValue_A.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tbSearchValue_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearchValue_A.Location = new System.Drawing.Point(567, 38);
            this.tbSearchValue_A.Name = "tbSearchValue_A";
            this.tbSearchValue_A.Size = new System.Drawing.Size(479, 43);
            this.tbSearchValue_A.TabIndex = 3;
            this.tbSearchValue_A.TextChanged += new System.EventHandler(this.tbSearchValue_A_TextChanged);
            // 
            // lblSearchCriteria_A
            // 
            this.lblSearchCriteria_A.AutoSize = true;
            this.lblSearchCriteria_A.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchCriteria_A.Location = new System.Drawing.Point(12, 41);
            this.lblSearchCriteria_A.Name = "lblSearchCriteria_A";
            this.lblSearchCriteria_A.Size = new System.Drawing.Size(476, 38);
            this.lblSearchCriteria_A.TabIndex = 2;
            this.lblSearchCriteria_A.Text = "Search by name,email or site address";
            // 
            // dgv_clientJoinJobRequest
            // 
            this.dgv_clientJoinJobRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientJoinJobRequest.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgv_clientJoinJobRequest.Location = new System.Drawing.Point(19, 101);
            this.dgv_clientJoinJobRequest.Name = "dgv_clientJoinJobRequest";
            this.dgv_clientJoinJobRequest.RowHeadersWidth = 51;
            this.dgv_clientJoinJobRequest.RowTemplate.Height = 24;
            this.dgv_clientJoinJobRequest.Size = new System.Drawing.Size(1150, 150);
            this.dgv_clientJoinJobRequest.TabIndex = 4;
            this.dgv_clientJoinJobRequest.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientJoinJobRequest_CellClick);
            this.dgv_clientJoinJobRequest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_clientJoinJobRequest_CellContentClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(19, 304);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(905, 308);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // jobTypeTableAdapter1
            // 
            this.jobTypeTableAdapter1.ClearBeforeFill = true;
            // 
            // requestItemTableAdapter1
            // 
            this.requestItemTableAdapter1.ClearBeforeFill = true;
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // jobDetailTableAdapter1
            // 
            this.jobDetailTableAdapter1.ClearBeforeFill = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(86, 690);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(274, 120);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // itemDetailTableAdapter1
            // 
            this.itemDetailTableAdapter1.ClearBeforeFill = true;
            // 
            // Add_Details_to_R_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.dgv_clientJoinJobRequest);
            this.Controls.Add(this.tbSearchValue_A);
            this.Controls.Add(this.lblSearchCriteria_A);
            this.Name = "Add_Details_to_R_Items";
            this.Text = "Add_Details_to_R_Items";
            this.Load += new System.EventHandler(this.Add_Details_to_R_Items_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientJoinJobRequest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSearchValue_A;
        private System.Windows.Forms.Label lblSearchCriteria_A;
        private System.Windows.Forms.DataGridView dgv_clientJoinJobRequest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private GroupWst1DataSetTableAdapters.JobTypeTableAdapter jobTypeTableAdapter1;
        private GroupWst1DataSetTableAdapters.RequestItemTableAdapter requestItemTableAdapter1;
        private GroupWst1DataSet groupWst1DataSet1;
        private GroupWst1DataSetTableAdapters.JobDetailTableAdapter jobDetailTableAdapter1;
        private System.Windows.Forms.Button btnSave;
        private GroupWst1DataSetTableAdapters.ItemDetailTableAdapter itemDetailTableAdapter1;
    }
}