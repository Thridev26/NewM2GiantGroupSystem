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

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabViewAllocations.TabPages[e.Index];
            Rectangle tabRect = tabViewAllocations.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            Color backColor = Color.Honeydew;
            Color textColor = Color.Black;

            if (e.Index == tabViewAllocations.SelectedIndex)
            {
                backColor = Color.DarkGreen;
                textColor = Color.White;
            }

            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
        private void AllocateAssetStafftoJob_Load(object sender, EventArgs e)
        {
            // 1. UI Setup (Do this once)
            tabViewAllocations.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabViewAllocations.DrawItem += tabControl1_DrawItem;
            tabViewAllocations.ItemSize = new Size(300, 30);
            tabViewAllocations.SizeMode = TabSizeMode.Fixed;
            tabViewAllocations.SelectedIndex = tabIndex;

            // 2. Load the specific Job ID data ONLY
            // ReloadSelectedJob() already calls FillByID, so do not call it again later
            ReloadSelectedJob();

            // 3. Now apply the filters (This is where the "Available" logic happens)
            // This is the SINGLE point of truth for your grid data
            RefreshAllGrids();

            // 4. Final Polish
            ownedAssetDataGridView.ClearSelection();
            hiredAssetDataGridView.ClearSelection();

            // 2. Use the NEW adapter you just created

            GroupWst1DataSetTableAdapters.DataTable1TableAdapter customAdapter = new GroupWst1DataSetTableAdapters.DataTable1TableAdapter();



            // 3. Fetch the data

            // var data = customAdapter.GetDataByInProgress();

            // MessageBox.Show("Selected Job ID: " + AppState.selectedIdCalendar);

            //var data = customAdapter.FillByID(this.groupWst1DataSet.DataTable1,1); // Example: Fetch data for jobID = 1. Adjust as needed.

            // 4. Bind the data

            dataGridView1.AutoGenerateColumns = true;

            //dataGridView1.DataSource = data;         
                  

                int jobId = AppState.selectedIdCalendar;

                if (jobId > 0)

                    MessageBox.Show("Opening allocation form for job ID: " + AppState.selectedIdCalendar);


                if (jobId <= 0)

                {

                    // MessageBox.Show("No job selected from calendar.");

                    loadJobs();

                    return;

                }



                GroupWst1DataSetTableAdapters.DataTable1TableAdapter adapter =

                    new GroupWst1DataSetTableAdapters.DataTable1TableAdapter();



                //  var data = adapter.FillByID(this.groupWst1DataSet.DataTable1,jobId);



                // dataGridView1.AutoGenerateColumns = true;

                // dataGridView1.DataSource = data;

                adapter.FillByID(this.groupWst1DataSet.DataTable1, jobId);



                dataGridView1.AutoGenerateColumns = true;

                dataGridView1.DataSource = this.groupWst1DataSet.DataTable1;

                adapter.FillByID(this.groupWst1DataSet.DataTable1, jobId);



                dataGridView1.DataSource = this.groupWst1DataSet.DataTable1;



                if (this.groupWst1DataSet.DataTable1.Rows.Count > 0)

                {

                    DataRow row = this.groupWst1DataSet.DataTable1.Rows[0];



                    jobIDBox.Text = row["jobID"].ToString();



                    clientNameBox.Text =

                        row["clientName"].ToString() + " " +

                        row["clientSurname"].ToString();



                    addressBox.Text = row["siteAddress"].ToString();



                    statusBox.Text = row["jobStatus"].ToString();

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
                var cellValue = row.Cells["Column1"].Value;

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
                // Use parameters to filter by the current Job ID
                int jobId = AppState.selectedIdCalendar;

                // Load assignments for this specific job
                // NOTE: Ensure your XSD has a FillByJobID query for these adapters
                this.jobAssetAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobAssetAssignment);
                this.jobStaffAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobStaffAssignment);

                // Load "Available" lists
                // Ensure these queries use: WHERE status = 'Active' or similar
                this.ownedAssetTableAdapter.FillByAvailableOwned(this.groupWst1DataSet.OwnedAsset);
                this.hiredAssetTableAdapter.FillByAvailableHired(this.groupWst1DataSet.HiredAsset);

                // Load Staff (Exclude restricted roles here in your SQL query!)
                this.staffTableAdapter.FillByAvailableStaff(this.groupWst1DataSet.Staff);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message);
            }
            //try
            //{
            //    // 1. Refresh the Assignment list first
            //    this.jobAssetAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobAssetAssignment);
            //    this.jobStaffAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobStaffAssignment);

            //    // 2. Then refresh the available assets (the query will re-run)
            //    this.ownedAssetTableAdapter.FillByAvailableOwned(this.groupWst1DataSet.OwnedAsset);
            //    this.hiredAssetTableAdapter.FillByAvailableHired(this.groupWst1DataSet.HiredAsset);

            //    // 3. Refresh "Available" Staff Grid
            //    // Use the new method we just created in the .xsd
            //    this.staffTableAdapter.FillByAvailableStaff(this.groupWst1DataSet.Staff);
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("Error refreshing data: " + ex.Message);
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (staffDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = staffDataGridView.SelectedRows[0];

                // Retrieve the roleID from the selected staff member's row
                int roleId = Convert.ToInt32(selectedRow.Cells["roleID"].Value);

                // Logic: Restrict access levels 5 (Admin) and 6 (Manager)
                // You can check this by comparing the roleID directly
                if (roleId == 10 || roleId == 11)
                {
                    MessageBox.Show("This staff member (Top Management/Admin) cannot be assigned to jobs.",
                                    "Restricted Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
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
                int staffId = (int)row.Cells["SColumn"].Value;
                int jobId = (int)row.Cells["JColumn"].Value;

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
            Calendar calendarForm = new Calendar();

            calendarForm.StartPosition = FormStartPosition.CenterScreen;
            

            calendarForm.ShowDialog();

            if (AppState.selectedIdCalendar > 0)
            {
                ReloadSelectedJob();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadJobs();
        }

        private void loadJobs()
        {
            GroupWst1DataSetTableAdapters.DataTable1TableAdapter adapter =
    new GroupWst1DataSetTableAdapters.DataTable1TableAdapter();

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = adapter.GetDataByInProgress();

            // Optional: clear the job details boxes
            jobIDBox.Clear();
            clientNameBox.Clear();
            addressBox.Clear();
            statusBox.Clear();

        }

        private void AllocateAssetStafftoJob_FormClosed(object sender, FormClosedEventArgs e)
        {
           /// MessageBox.Show("Allocation form closing. Setting ID to -8");
          //  AppState.selectedIdCalendar = -8;
        }

        private void ReloadSelectedJob()
        {
            int jobId = AppState.selectedIdCalendar;

            dataTable1TableAdapter.FillByID(
                this.groupWst1DataSet.DataTable1,
                jobId);

            dataGridView1.DataSource = this.groupWst1DataSet.DataTable1;

            if (this.groupWst1DataSet.DataTable1.Rows.Count > 0)
            {
                DataRow row = this.groupWst1DataSet.DataTable1.Rows[0];

                jobIDBox.Text = row["jobID"].ToString();
                clientNameBox.Text =
                    row["clientName"].ToString() + " " +
                    row["clientSurname"].ToString();

                addressBox.Text = row["siteAddress"].ToString();
                statusBox.Text = row["jobStatus"].ToString();
            }
        }

    }
}
