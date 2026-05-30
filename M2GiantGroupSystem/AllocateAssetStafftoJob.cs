using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class AllocateAssetStafftoJob : Form
    {
        public AllocateAssetStafftoJob()
        {
            InitializeComponent();
        }

        private void jobBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.jobBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.groupWst1DataSet);

        }

        private void AllocateAssetStafftoJob_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupWst1DataSet.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.groupWst1DataSet.DataTable1);
            // Force this to true in code if you can't find the designer property
            dataGridView1.AutoGenerateColumns = true;
            try
            {
                // TODO: This line of code loads data into the 'groupWst1DataSet.JobStaffAssignment' table. You can move, or remove it, as needed.
                this.jobStaffAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobStaffAssignment);
                // TODO: This line of code loads data into the 'groupWst1DataSet.Staff' table. You can move, or remove it, as needed.
                this.staffTableAdapter.Fill(this.groupWst1DataSet.Staff);
                // TODO: This line of code loads data into the 'groupWst1DataSet.JobAssetAssignment' table. You can move, or remove it, as needed.
                this.jobAssetAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobAssetAssignment);
                // TODO: This line of code loads data into the 'groupWst1DataSet.HiredAsset' table. You can move, or remove it, as needed.
                this.hiredAssetTableAdapter.Fill(this.groupWst1DataSet.HiredAsset);
                // TODO: This line of code loads data into the 'groupWst1DataSet.OwnedAsset' table. You can move, or remove it, as needed.
                this.ownedAssetTableAdapter.Fill(this.groupWst1DataSet.OwnedAsset);

                // 2. Use the NEW adapter you just created
                GroupWst1DataSetTableAdapters.DataTable1TableAdapter customAdapter = new GroupWst1DataSetTableAdapters.DataTable1TableAdapter();

                // 3. Fetch the data
                var data = customAdapter.GetDataByInProgress();

                // 4. Bind the data
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                // This will tell you EXACTLY why it is crashing
                MessageBox.Show("Error loading data: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Create your adapter instance
            GroupWst1DataSetTableAdapters.DataTable1TableAdapter adapter = new GroupWst1DataSetTableAdapters.DataTable1TableAdapter();

            // Check if the box is empty (if so, show all, otherwise filter)
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                dataGridView1.DataSource = adapter.GetDataByInProgress();
            }
            else
            {
                // Pass the textbox text as the parameter to your SQL query
                dataGridView1.DataSource = adapter.GetDataBySearch(txtSearch.Text);
            }
        }
    }
}
