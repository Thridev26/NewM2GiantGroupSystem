using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace M2GiantGroupSystem
{
    public partial class AllocateAssetStafftoJob : Form
    {
        int tabIndex;
        public  AllocateAssetStafftoJob(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
            ThemeManager.ThemeChanged += ApplyTheme;
        }
        public int selectedJobId;
        private void jobBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.jobBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.groupWst1DataSet);

        }

        private void ApplyPermissions()
        {
            int level = UserSession.AccessLevel; // The user Session object is global so this will work 

            // 1. If Owner (6), they already have full access.  
            // We just return early and don't change anything! 
            if (level >= 6) return;

            // 2. If we reach this point, the user is NOT an owner. 
            // Now we apply restrictions for everyone else. 
            switch (level)
            {
                case 5: // Admin: Some locks                     
                    break;

                case 4: // Ops Manager: More locks 
                    if (tabViewAllocations.TabPages.Contains(tabPage1) && tabViewAllocations.TabPages.Contains(tabPage2))
                    {
                        tabViewAllocations.TabPages.Remove(tabPage1);
                        tabViewAllocations.TabPages.Remove(tabPage2);
                    }
                    tabViewAllocations.Refresh(); // Refresh the tab control to reflect changes immediately

                   
                    break;

                default: // Level 3 and below: Complete lockdown – lock all controls if you feel they should not have access
                    if (tabViewAllocations.TabPages.Contains(tabPage1) && tabViewAllocations.TabPages.Contains(tabPage2))
                    {
                        tabViewAllocations.TabPages.Remove(tabPage1);
                        tabViewAllocations.TabPages.Remove(tabPage2);
                    }
                    tabViewAllocations.Refresh(); // Refresh the tab control to reflect changes immediately
                    
                    break;
            }
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
            ApplyPermissions();
            ApplyTheme();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Green;
            ownedAssetDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ownedAssetDataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
            hiredAssetDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            hiredAssetDataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
            jobAssetAssignmentDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            jobAssetAssignmentDataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
            staffDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            staffDataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
            jobStaffAssignmentDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            jobStaffAssignmentDataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
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
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                jobIDBox.Text = row.Cells["jobID"].Value?.ToString() ?? "";
                clientNameBox.Text = ($"{row.Cells["clientName"].Value} {row.Cells["clientSurname"].Value}").Trim();
                addressBox.Text = row.Cells["siteAddress"].Value?.ToString() ?? "";
                statusBox.Text = row.Cells["jobStatus"].Value?.ToString() ?? "";

                // Parse and store so RefreshAllGrids can use it
                if (int.TryParse(jobIDBox.Text, out selectedJobId))
                {
                    AppState.selectedIdCalendar = selectedJobId;
                    RefreshAllGrids();  // This re-runs all three queries with the new @jobID
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading job details: " + ex.Message,
                                "UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validation: Check if a job is selected
                if (string.IsNullOrEmpty(jobIDBox.Text))
                {
                    MessageBox.Show("Select a job first!");
                    return;
                }

                // 2. Logic for Owned Asset
                if (ownedAssetDataGridView.SelectedRows.Count > 0)
                {
                    var row = ownedAssetDataGridView.SelectedRows[0];
                    string assetName = row.Cells["type"].Value?.ToString() ?? "Unknown Asset";

                    var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        // Use int.TryParse for safety
                        if (int.TryParse(jobIDBox.Text, out int jobId) && int.TryParse(row.Cells["assetID"].Value?.ToString(), out int ownedId))
                        {
                            jobAssetAssignmentTableAdapter.Insert1(jobId, ownedId, null, DateTime.Now.Date.ToString());
                            RefreshAllGrids();
                        }
                        else
                        {
                            MessageBox.Show("Error: Invalid Job or Asset ID format.");
                        }
                    }
                }
                //// 3. Logic for Hired Asset
                //else if (hiredAssetDataGridView.SelectedRows.Count > 0)
                //{
                //    var row = hiredAssetDataGridView.SelectedRows[0];
                //    string assetName = row.Cells["equipmentType"].Value?.ToString() ?? "Unknown Asset";

                //    var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
                //    if (confirm == DialogResult.Yes)
                //    {
                //        if (int.TryParse(jobIDBox.Text, out int jobId) && int.TryParse(row.Cells["hiredAssetID"].Value?.ToString(), out int hiredId))
                //        {
                //            jobAssetAssignmentTableAdapter.Insert1(jobId, null, hiredId, DateTime.Now.ToString());
                //            RefreshAllGrids();
                //        }
                //        else
                //        {
                //            MessageBox.Show("Error: Invalid Job or Asset ID format.");
                //        }
                //    }
                //}
                else
                {
                    MessageBox.Show("Please select an asset from the Owned Asset grid.");
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message, "Database Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //// Check if a job is selected
            //if (string.IsNullOrEmpty(jobIDBox.Text)) { MessageBox.Show("Select a job first!"); return; }

            //// Logic for Owned Asset
            //if (ownedAssetDataGridView.SelectedRows.Count > 0)
            //{
            //    var row = ownedAssetDataGridView.SelectedRows[0];
            //    string assetName = row.Cells["type"].Value.ToString();

            //    var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
            //    if (confirm == DialogResult.Yes)
            //    {
            //        int ownedId = (int)row.Cells["assetID"].Value;
            //        jobAssetAssignmentTableAdapter.Insert1(int.Parse(jobIDBox.Text), ownedId, null, DateTime.Now.ToString());
            //        RefreshAllGrids();
            //    }
            //}
            //// Logic for Hired Asset
            //else if (hiredAssetDataGridView.SelectedRows.Count > 0)
            //{
            //    var row = hiredAssetDataGridView.SelectedRows[0];
            //    string assetName = row.Cells["equipmentType"].Value.ToString();

            //    var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
            //    if (confirm == DialogResult.Yes)
            //    {
            //        int hiredId = (int)row.Cells["hiredAssetID"].Value;
            //        jobAssetAssignmentTableAdapter.Insert1(int.Parse(jobIDBox.Text), null, hiredId, DateTime.Now.ToString());
            //        RefreshAllGrids();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please select an asset from either the Owned or Hired grid.");
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (jobAssetAssignmentDataGridView.SelectedRows.Count > 0)
                {
                    var row = jobAssetAssignmentDataGridView.SelectedRows[0];
                    var cellValue = row.Cells["AssignmentID"].Value;

                    if (cellValue != null && cellValue != DBNull.Value)
                    {
                        if (int.TryParse(cellValue.ToString(), out int assignmentId))
                        {
                            var confirm = MessageBox.Show("Are you sure you want to unassign this assignment?",
                                                          "Confirm Unassign", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (confirm == DialogResult.Yes)
                            {
                                // ✅ Secure approach: pulling from App.config
                                string connStr = ConfigurationManager.ConnectionStrings["GroupWst1ConnString"].ConnectionString;

                                using (SqlConnection conn = new SqlConnection(connStr))
                                using (SqlCommand cmd = new SqlCommand(
                                    "DELETE FROM JobAssetAssignment WHERE AssignmentID = @AssignmentID", conn))
                                {
                                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                }

                                RefreshAllGrids();
                                MessageBox.Show("Assignment has been successfully unassigned.",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The selected ID is not a valid number.",
                                "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The selected row does not have a valid ID.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.",
                        "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: Could not delete record. It may be linked to other data.\n\nDetails: " + sqlEx.Message,
                    "Database Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message,
                    "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        

        private void RefreshAllGrids()
        {
            int jobId = AppState.selectedIdCalendar;
            selectedJobId = jobId;

            if (jobId <= 0) return;

            try
            {
                // ✅ Secure approach: pulling from App.config
                string connStr = ConfigurationManager.ConnectionStrings["GroupWst1ConnString"].ConnectionString;

                string staffSQL = @"
            SELECT s.staffID, s.firstName, s.lastName, s.contactNumber, s.dailyRate, s.roleID, r.roleName, j.startDate AS jobStartDate
            FROM Staff AS s
            INNER JOIN Role AS r ON s.roleID = r.roleID
            LEFT OUTER JOIN JobStaffAssignment AS jsa ON s.staffID = jsa.staffID AND jsa.jobID = @jobID
            LEFT OUTER JOIN Job AS j ON j.jobID = @jobID
            WHERE s.staffStatus = 'Active' AND r.roleID NOT IN (10, 11) AND jsa.staffID IS NULL";

                string hiredSQL = @"
            SELECT ha.hiredAssetID, ha.equipmentType, ha.supplierName, ha.hireDate, ha.hireCost, ha.hiredAssetStatus, j.startDate AS jobStartDate
            FROM HiredAsset AS ha
            LEFT OUTER JOIN JobAssetAssignment AS jaa ON ha.hiredAssetID = jaa.hiredAssetID AND jaa.jobID = @jobID
            LEFT OUTER JOIN Job AS j ON j.jobID = @jobID
            WHERE ha.hiredAssetStatus NOT IN ('Returned', 'Damaged', 'Overdue') AND jaa.hiredAssetID IS NULL";

                string ownedSQL = @"
            SELECT oa.assetID, oa.type, oa.serialNumber, oa.currentCondition, oa.assetStatus, j.startDate AS jobStartDate
            FROM OwnedAsset AS oa
            LEFT OUTER JOIN JobAssetAssignment AS jaa ON oa.assetID = jaa.ownedAssetID AND jaa.jobID = @jobID
            LEFT OUTER JOIN Job AS j ON j.jobID = @jobID
            WHERE oa.assetStatus NOT IN ('Retired', 'Under Maintenance') AND jaa.ownedAssetID IS NULL";

                string assetAssignSQL = @"
    SELECT 
        jaa.AssignmentID,
        jaa.jobID,
        COALESCE(jaa.ownedAssetID, jaa.hiredAssetID) AS assetID,
        COALESCE(oa.type, ha.equipmentType)           AS assetDescription,
        CASE 
            WHEN jaa.ownedAssetID IS NOT NULL THEN 'Owned'
            WHEN jaa.hiredAssetID IS NOT NULL THEN 'Hired'
        END AS assetCategory,
        jaa.fuelUsed,
        jaa.assignmentDate
    FROM JobAssetAssignment jaa
    LEFT JOIN OwnedAsset oa ON jaa.ownedAssetID = oa.assetID
    LEFT JOIN HiredAsset ha ON jaa.hiredAssetID = ha.hiredAssetID
    WHERE jaa.jobID = @jobID";

                string staffAssignSQL = @"
SELECT 
    jsa.staffID,
    s.firstName,
    s.lastName,
    jsa.jobID,
    jsa.hoursWorked,
    jsa.assignmentDate
FROM JobStaffAssignment jsa
INNER JOIN Staff s
    ON jsa.staffID = s.staffID
WHERE jsa.jobID = @jobID";

                // Run each query the same way as jobRequestMain_A
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Available Staff
                    using (SqlCommand cmd = new SqlCommand(staffSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@jobID", jobId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        staffDataGridView.DataSource = dt;
                    }

                    // Available Hired Assets
                    using (SqlCommand cmd = new SqlCommand(hiredSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@jobID", jobId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        hiredAssetDataGridView.DataSource = dt;
                    }

                    // Available Owned Assets
                    using (SqlCommand cmd = new SqlCommand(ownedSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@jobID", jobId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        ownedAssetDataGridView.DataSource = dt;
                    }

                    // Asset Assignments for this job
                    using (SqlCommand cmd = new SqlCommand(assetAssignSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@jobID", jobId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        jobAssetAssignmentDataGridView.DataSource = dt;
                    }

                    // Staff Assignments for this job
                    using (SqlCommand cmd = new SqlCommand(staffAssignSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@jobID", jobId);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        jobStaffAssignmentDataGridView.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while refreshing grids:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while refreshing grids:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Safety Check: Ensure a job and a staff member are selected
                if (string.IsNullOrWhiteSpace(jobIDBox.Text) || staffDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a Job and a Staff member.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectedRow = staffDataGridView.SelectedRows[0];

                // 2. Validate Role Access
                // Safely parse roleId to avoid crashes if the column value is null or malformed
                if (int.TryParse(selectedRow.Cells["roleID"].Value?.ToString(), out int roleId))
                {
                    if (roleId == 10 || roleId == 11)
                    {
                        MessageBox.Show("This staff member (Top Management/Admin) cannot be assigned to jobs.",
                                        "Restricted Assignment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 3. Confirmation
                string name = selectedRow.Cells["firstName"].Value?.ToString() ?? "Unknown";
                var confirm = MessageBox.Show($"Assign {name} to this job?", "Confirm Assignment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    // 4. Safe Parsing for IDs
                    if (int.TryParse(selectedRow.Cells["staffID"].Value?.ToString(), out int staffId) &&
                        int.TryParse(jobIDBox.Text, out int jobId))
                    {
                        // 5. Execute Insert
                        jobStaffAssignmentTableAdapter.InsertStaff(staffId, jobId, DateTime.Now.Date.ToString());

                        // 6. Update UI
                        RefreshAllGrids();
                        MessageBox.Show("Staff member assigned successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error: Invalid ID format detected.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Database error during assignment: " + sqlEx.Message, "Database Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Check if a row is selected
                if (jobStaffAssignmentDataGridView.SelectedRows.Count > 0)
                {
                    var row = jobStaffAssignmentDataGridView.SelectedRows[0];

                    // 2. Safe Parsing: Using TryParse instead of explicit (int) casting 
                    // prevents crashes if the cell contains unexpected data
                    if (int.TryParse(row.Cells["staffID"].Value?.ToString(), out int staffId) &&
                        int.TryParse(row.Cells["jobID"].Value?.ToString(), out int jobId))
                    {
                        // 3. Confirmation
                        var confirm = MessageBox.Show("Remove this staff member from the job?",
                                                      "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirm == DialogResult.Yes)
                        {
                            // 4. Execute Delete
                            jobStaffAssignmentTableAdapter.DeleteStaff(staffId, jobId);

                            // 5. Update UI
                            RefreshAllGrids();
                            MessageBox.Show("Staff member removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The selected row contains invalid ID data.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an assignment row from the right-hand grid to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Database error: Could not remove assignment. \n\nDetails: " + sqlEx.Message,
                                "Database Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //// 1. Check if a row is selected in the ASSIGNMENT grid (the one on the right)
            //if (jobStaffAssignmentDataGridView.SelectedRows.Count > 0)
            //{
            //    // 2. Get data from the Assignment grid
            //    var row = jobStaffAssignmentDataGridView.SelectedRows[0];
            //    int staffId = (int)row.Cells["SColumn"].Value;
            //    int jobId = (int)row.Cells["JColumn"].Value;

            //    // 3. Confirmation
            //    var confirm = MessageBox.Show("Remove this staff member from the job?", "Confirm Removal", MessageBoxButtons.YesNo);

            //    if (confirm == DialogResult.Yes)
            //    {
            //        // 4. Execute Delete using the query created in the XSD
            //        jobStaffAssignmentTableAdapter.DeleteStaff(staffId, jobId);

            //        // 5. Update UI
            //        RefreshAllGrids();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please select an assignment row from the right-hand grid to remove.");
            //}
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.ThemeChanged -= ApplyTheme;
            base.OnFormClosed(e);
        }
        private void ApplyTheme()
        {
            if (ThemeManager.IsDarkMode)
                ThemeManager.ApplyTheme(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validation: Check if a job is selected
                if (string.IsNullOrEmpty(jobIDBox.Text))
                {
                    MessageBox.Show("Select a job first!");
                    return;
                }

                
                // 2. Logic for Hired Asset
                if (hiredAssetDataGridView.SelectedRows.Count > 0)
                {
                    var row = hiredAssetDataGridView.SelectedRows[0];
                    string assetName = row.Cells["equipmentType"].Value?.ToString() ?? "Unknown Asset";

                    var confirm = MessageBox.Show($"Assign {assetName} to this job?", "Confirm", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        if (int.TryParse(jobIDBox.Text, out int jobId) && int.TryParse(row.Cells["hiredAssetID"].Value?.ToString(), out int hiredId))
                        {
                            jobAssetAssignmentTableAdapter.Insert1(jobId, null, hiredId, DateTime.Now.ToString());
                            RefreshAllGrids();
                        }
                        else
                        {
                            MessageBox.Show("Error: Invalid Job or Asset ID format.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an asset from the Hired Asset grid.");
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message, "Database Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
