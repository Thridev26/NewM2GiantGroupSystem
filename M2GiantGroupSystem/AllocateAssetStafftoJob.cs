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
        int tabIndex;
        public  AllocateAssetStafftoJob(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }

        private void jobBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.jobBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.groupWst1DataSet);

        }

        private void AllocateAssetStafftoJob_Load(object sender, EventArgs e)
        {
            tabViewAllocations.SelectedIndex = tabIndex; // Set the selected tab based on the passed index
            // TODO: This line of code loads data into the 'groupWst1DataSet.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.groupWst1DataSet.DataTable1);
            // Force this to true in code if you can't find the designer property
            dataGridView1.AutoGenerateColumns = true;

            // Clear the selection so no row is highlighted automatically
            ownedAssetDataGridView.ClearSelection();
            hiredAssetDataGridView.ClearSelection();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the user didn't double-click the header row
            if (e.RowIndex >= 0)
            {
                // Get the specific row that was double-clicked
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Fill the Job ID
                jobIDBox.Text = row.Cells["jobID"].Value.ToString();

                // Combine Name and Surname for the name box
                string fullName = row.Cells["clientName"].Value.ToString() + " " +
                                  row.Cells["clientSurname"].Value.ToString();
                clientNameBox.Text = fullName;

                // Fill the Address and Status
                addressBox.Text = row.Cells["siteAddress"].Value.ToString();
                statusBox.Text = row.Cells["jobStatus"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if a job is selected
            if (string.IsNullOrEmpty(jobIDBox.Text)) { MessageBox.Show("Select a job first!"); return; }

            // Logic for Owned Asset
            if (ownedAssetDataGridView.SelectedRows.Count > 0)
            {
                var row = ownedAssetDataGridView.SelectedRows[0];
                string assetName = row.Cells["type"].Value.ToString();

                var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    int ownedId = (int)row.Cells["assetID"].Value;
                    jobAssetAssignmentTableAdapter.Insert1(int.Parse(jobIDBox.Text), ownedId, null, DateTime.Now.ToString());
                    RefreshAllGrids();
                }
            }
            // Logic for Hired Asset
            else if (hiredAssetDataGridView.SelectedRows.Count > 0)
            {
                var row = hiredAssetDataGridView.SelectedRows[0];
                string assetName = row.Cells["equipmentType"].Value.ToString();

                var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    int hiredId = (int)row.Cells["hiredAssetID"].Value;
                    jobAssetAssignmentTableAdapter.Insert1(int.Parse(jobIDBox.Text), null, hiredId, DateTime.Now.ToString());
                    RefreshAllGrids();
                }
            }
            else
            {
                MessageBox.Show("Please select an asset from either the Owned or Hired grid.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (jobAssetAssignmentDataGridView.SelectedRows.Count > 0)
            {
                var row = jobAssetAssignmentDataGridView.SelectedRows[0];

                // Use the EXACT name you found in the Edit Columns dialog
                var cellValue = row.Cells["assignmentID"].Value;

                if (cellValue != null && cellValue != DBNull.Value)
                {
                    var confirm = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        int assignmentId = Convert.ToInt32(cellValue);
                        jobAssetAssignmentTableAdapter.Delete1(assignmentId);
                        RefreshAllGrids();
                    }
                }
                else
                {
                    MessageBox.Show("The selected row does not have a valid ID.");
                }
            }
        }

        private void RefreshAllGrids()
        {
            try
            {
                // 1. Refresh the Assignment list first
                this.jobAssetAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobAssetAssignment);
                this.jobStaffAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobStaffAssignment);

                // 2. Then refresh the available assets (the query will re-run)
                this.ownedAssetTableAdapter.FillByAvailableOwned(this.groupWst1DataSet.OwnedAsset);
                this.hiredAssetTableAdapter.FillByAvailableHired(this.groupWst1DataSet.HiredAsset);

                // 3. Refresh "Available" Staff Grid
                // Use the new method we just created in the .xsd
                this.staffTableAdapter.FillByAvailableStaff(this.groupWst1DataSet.Staff);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Safety Check: Ensure a job and a staff member are selected
            if (string.IsNullOrEmpty(jobIDBox.Text) || staffDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a Job and a Staff member.");
                return;
            }

            // 2. Confirmation
            var row = staffDataGridView.SelectedRows[0];
            string name = row.Cells["firstName"].Value.ToString();
            var confirm = MessageBox.Show($"Assign {name} to this job?", "Confirm", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                int staffId = (int)row.Cells["staffID"].Value;
                int jobId = int.Parse(jobIDBox.Text);

                // 3. Execute Insert
                jobStaffAssignmentTableAdapter.InsertStaff(staffId, jobId, DateTime.Now.ToString());

                // 4. Update UI
                RefreshAllGrids();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 1. Check if a row is selected in the ASSIGNMENT grid (the one on the right)
            if (jobStaffAssignmentDataGridView.SelectedRows.Count > 0)
            {
                // 2. Get data from the Assignment grid
                var row = jobStaffAssignmentDataGridView.SelectedRows[0];
                int staffId = (int)row.Cells["staffID_T"].Value;
                int jobId = (int)row.Cells["jobID_T"].Value;

                // 3. Confirmation
                var confirm = MessageBox.Show("Remove this staff member from the job?", "Confirm Removal", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    // 4. Execute Delete using the query created in the XSD
                    jobStaffAssignmentTableAdapter.DeleteStaff(staffId, jobId);

                    // 5. Update UI
                    RefreshAllGrids();
                }
            }
            else
            {
                MessageBox.Show("Please select an assignment row from the right-hand grid to remove.");
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Check if the currently selected tab is the one you want
            // Replace "tabViewAllocations" with the actual (Name) property of your tab
            if (tabViewAllocations.SelectedTab.Name == "tab3")
            {
                Calendar calendarForm = new Calendar();
                calendarForm.ShowDialog();
            }
        }
    }
}
