namespace M2GiantGroupSystem
{
    partial class View_Clients_A
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupWst1DataSet1 = new M2GiantGroupSystem.GroupWst1DataSet();
            this.groupWst1DataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.clientTableAdapter1 = new M2GiantGroupSystem.GroupWst1DataSetTableAdapters.ClientTableAdapter();
            this.clientIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientSurnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateAddedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clientIDDataGridViewTextBoxColumn,
            this.clientNameDataGridViewTextBoxColumn,
            this.clientSurnameDataGridViewTextBoxColumn,
            this.clientTypeDataGridViewTextBoxColumn,
            this.emailAddressDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.dateAddedDataGridViewTextBoxColumn,
            this.phoneNumberDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.groupWst1DataSet1BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(65, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(613, 235);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupWst1DataSet1
            // 
            this.groupWst1DataSet1.DataSetName = "GroupWst1DataSet";
            this.groupWst1DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupWst1DataSet1BindingSource
            // 
            this.groupWst1DataSet1BindingSource.DataMember = "Client";
            this.groupWst1DataSet1BindingSource.DataSource = this.groupWst1DataSet1;
            // 
            // clientTableAdapter1
            // 
            this.clientTableAdapter1.ClearBeforeFill = true;
            // 
            // clientIDDataGridViewTextBoxColumn
            // 
            this.clientIDDataGridViewTextBoxColumn.DataPropertyName = "clientID";
            this.clientIDDataGridViewTextBoxColumn.HeaderText = "clientID";
            this.clientIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clientIDDataGridViewTextBoxColumn.Name = "clientIDDataGridViewTextBoxColumn";
            this.clientIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.clientIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // clientNameDataGridViewTextBoxColumn
            // 
            this.clientNameDataGridViewTextBoxColumn.DataPropertyName = "clientName";
            this.clientNameDataGridViewTextBoxColumn.HeaderText = "clientName";
            this.clientNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clientNameDataGridViewTextBoxColumn.Name = "clientNameDataGridViewTextBoxColumn";
            this.clientNameDataGridViewTextBoxColumn.Width = 125;
            // 
            // clientSurnameDataGridViewTextBoxColumn
            // 
            this.clientSurnameDataGridViewTextBoxColumn.DataPropertyName = "clientSurname";
            this.clientSurnameDataGridViewTextBoxColumn.HeaderText = "clientSurname";
            this.clientSurnameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clientSurnameDataGridViewTextBoxColumn.Name = "clientSurnameDataGridViewTextBoxColumn";
            this.clientSurnameDataGridViewTextBoxColumn.Width = 125;
            // 
            // clientTypeDataGridViewTextBoxColumn
            // 
            this.clientTypeDataGridViewTextBoxColumn.DataPropertyName = "clientType";
            this.clientTypeDataGridViewTextBoxColumn.HeaderText = "clientType";
            this.clientTypeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.clientTypeDataGridViewTextBoxColumn.Name = "clientTypeDataGridViewTextBoxColumn";
            this.clientTypeDataGridViewTextBoxColumn.Width = 125;
            // 
            // emailAddressDataGridViewTextBoxColumn
            // 
            this.emailAddressDataGridViewTextBoxColumn.DataPropertyName = "emailAddress";
            this.emailAddressDataGridViewTextBoxColumn.HeaderText = "emailAddress";
            this.emailAddressDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.emailAddressDataGridViewTextBoxColumn.Name = "emailAddressDataGridViewTextBoxColumn";
            this.emailAddressDataGridViewTextBoxColumn.Width = 125;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "status";
            this.statusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.Width = 125;
            // 
            // dateAddedDataGridViewTextBoxColumn
            // 
            this.dateAddedDataGridViewTextBoxColumn.DataPropertyName = "dateAdded";
            this.dateAddedDataGridViewTextBoxColumn.HeaderText = "dateAdded";
            this.dateAddedDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dateAddedDataGridViewTextBoxColumn.Name = "dateAddedDataGridViewTextBoxColumn";
            this.dateAddedDataGridViewTextBoxColumn.Width = 125;
            // 
            // phoneNumberDataGridViewTextBoxColumn
            // 
            this.phoneNumberDataGridViewTextBoxColumn.DataPropertyName = "phoneNumber";
            this.phoneNumberDataGridViewTextBoxColumn.HeaderText = "phoneNumber";
            this.phoneNumberDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.phoneNumberDataGridViewTextBoxColumn.Name = "phoneNumberDataGridViewTextBoxColumn";
            this.phoneNumberDataGridViewTextBoxColumn.Width = 125;
            // 
            // View_Clients_A
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "View_Clients_A";
            this.Text = "View_Clients_A";
            this.Load += new System.EventHandler(this.View_Clients_A_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupWst1DataSet1BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private GroupWst1DataSet groupWst1DataSet1;
        private System.Windows.Forms.BindingSource groupWst1DataSet1BindingSource;
        private GroupWst1DataSetTableAdapters.ClientTableAdapter clientTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientSurnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateAddedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneNumberDataGridViewTextBoxColumn;
    }
}