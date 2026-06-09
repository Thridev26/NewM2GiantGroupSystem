using M2GiantGroupSystem.GroupWst1DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class JobsForm : Form
    {
        int tabIndex;
        DataTable jobTable;
        int selectedJobID;

        // Global variables to store IDs during the capture process
        int selectedQuoteID = 0;
        int jobID;
        int timeSlotID;

        // Connection string reused for the custom queries
        string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

        public JobsForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }
        private void ViewJobDetailsForm_Load(object sender, EventArgs e)
        {
            // Setup grid visual layouts first...
            SetupGrid();

            // Populate your filter ComboBox cleanly
            JobProgressFilter.Items.Clear();
            JobProgressFilter.Items.AddRange(new string[] { "All", "Not Started", "In Progress", "Completed" });
            JobProgressFilter.SelectedIndex = 0; // Selects "All" by default

            // Load initial table format
            LoadJobs("All", "");
        }

        // --------------------------------------------------------------------------------------------------------
        // VIEW ACCEPTED QUOTES (WITH SEARCH)
        // --------------------------------------------------------------------------------------------------------
        public void loadAcceptedQuotes(string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT 
                        q.QuoteID,
                        c.clientName AS [Client Name],
                        c.clientSurname AS [Client Surname],
                        jr.siteAddress AS [Site Address],
                        jr.dateRecieved AS [Date Received],
                        q.filePath AS [Quote File]
                    FROM Quote q
                    INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                    INNER JOIN Client c ON jr.clientID = c.clientID
                    WHERE q.quoteStatus = 'Accepted'";

                // Apply filter if they type something in textBox1
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    sql += " AND (c.clientName LIKE @s OR c.clientSurname LIKE @s OR jr.siteAddress LIKE @s)";
                }

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    da.SelectCommand.Parameters.AddWithValue("@s", "%" + searchTerm + "%");
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvJoin.DataSource = dt;
                // --- COPY AND PASTE THIS BLOCK RIGHT AFTER: dgvJoin.DataSource = dt; ---

                // 1. Force the grid to fill the entire width of the layout cleanly
                dgvJoin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 2. Set individual proportions so longer text has breathing room
                dgvJoin.Columns["Client Name"].FillWeight = 60;
                dgvJoin.Columns["Client Surname"].FillWeight = 60;
                dgvJoin.Columns["Site Address"].FillWeight = 150; // Double width for long addresses
                dgvJoin.Columns["Date Received"].FillWeight = 70;
                dgvJoin.Columns["Quote File"].FillWeight = 120;    // Extra room for the file path

                // 3. Give row text some padding vertically so lines aren't squished together
                dgvJoin.RowTemplate.Height = 32;
                dgvJoin.ColumnHeadersHeight = 40;
                // -----------------------------------------------------------------------

                if (dgvJoin.Columns["QuoteID"] != null)
                {
                    dgvJoin.Columns["QuoteID"].Visible = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Instantly filter the grid as the user types
            loadAcceptedQuotes(txtSearchQuote.Text);
        }
       
        private void JobsForm_Load(object sender, EventArgs e)
        {
            
            tabControl1.SelectedIndex = tabIndex;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            cboJobStatus.Items.Clear();
            cboJobStatus.Items.AddRange(new string[] { "Not Started", "In Progress", "Completed" });
            cboJobStatus.SelectedIndex = 0;

            txtFuelCost.Text = "0.00";
            txtLabourCost.Text = "0.00";
            txtDumpingCost.Text = "0.00";

            dgvJoin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJoin.DefaultCellStyle.SelectionBackColor = Color.Green;

            loadAcceptedQuotes();
            LoadTimeSlotsForDate(dtpStartDate.Value);
            LoadJobs();
            SetupGrid();
            ColourJobRows();

        }

        // --------------------------------------------------------------------------------------------------------
        // DATAGRIDVIEW SINGLE CLICK EVENT 
        // --------------------------------------------------------------------------------------------------------
        private void dgvJoin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value == DBNull.Value ||
                dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value == null)
            {
                return;
            }

            selectedQuoteID = Convert.ToInt32(dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value);

            if (txtSelectedQuoteID != null)
            {
                txtSelectedQuoteID.Text = selectedQuoteID.ToString();
            }

            MessageBox.Show("Quote Selected! ID: " + selectedQuoteID, "Quote Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --------------------------------------------------------------------------------------------------------
        // DYNAMIC TIME SLOT CHECKBOXES (FILTERS OUT BOOKED SLOTS)
        // --------------------------------------------------------------------------------------------------------
        private void LoadTimeSlotsForDate(DateTime selectedDate)
        {
            pnlTimeSlots.Controls.Clear();
            List<int> bookedSlots = new List<int>();

            try
            {
                // 1. Find all time slots already booked for this specific date
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT jts.timeSlotID 
                        FROM JobTimeSlot jts
                        INNER JOIN Job j ON jts.jobID = j.jobID
                        WHERE CAST(j.startDate AS DATE) = @d";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@d", selectedDate.Date);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bookedSlots.Add(Convert.ToInt32(reader["timeSlotID"]));
                            }
                        }
                    }
                }

                // 2. Load all possible slots
                this.timeSlotTableAdapter1.Fill(this.groupWst1DataSet1.TimeSlot);

                // 3. Draw the checkboxes, skipping any that are in the 'bookedSlots' list
                foreach (var slot in this.groupWst1DataSet1.TimeSlot)
                {
                    if (bookedSlots.Contains(slot.timeSlotID))
                    {
                        continue; // Skip it entirely
                    }

                    CheckBox cb = new CheckBox();
                    string start = slot.startTime.ToString(@"hh\:mm");
                    string end = slot.endTime.ToString(@"hh\:mm");

                    cb.Text = $"{start} - {end}";
                    cb.AutoSize = true;
                    cb.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                    cb.Tag = slot.timeSlotID;

                    pnlTimeSlots.Controls.Add(cb);
                }

                // 4. Show a friendly warning if everything is booked
                if (pnlTimeSlots.Controls.Count == 0)
                {
                    Label lblFull = new Label();
                    lblFull.Text = "All time slots are booked for this date.";
                    lblFull.AutoSize = true;
                    lblFull.ForeColor = Color.Red;
                    pnlTimeSlots.Controls.Add(lblFull);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading time slots: " + ex.Message);
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTimeSlotsForDate(dtpStartDate.Value);
        }

        // --------------------------------------------------------------------------------------------------------
        // CAPTURE JOB AND SCHEDULE
        // --------------------------------------------------------------------------------------------------------
        private void btnSaveJob_Click(object sender, EventArgs e)
        {
            if (selectedQuoteID == 0)
            {
                MessageBox.Show("Please select an accepted Quote from the table first.");
                return;
            }

            if (cboJobStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please ensure a Job Status is selected.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to schedule this job?", "Confirm Job Scheduling", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                decimal fuel = string.IsNullOrWhiteSpace(txtFuelCost.Text) ? 0.00m : Convert.ToDecimal(txtFuelCost.Text);
                decimal labour = string.IsNullOrWhiteSpace(txtLabourCost.Text) ? 0.00m : Convert.ToDecimal(txtLabourCost.Text);
                decimal dumping = string.IsNullOrWhiteSpace(txtDumpingCost.Text) ? 0.00m : Convert.ToDecimal(txtDumpingCost.Text);

                // 1. Save the job and grab the generated jobID
                jobID = Convert.ToInt32(jobTableAdapter1.InsertQuery(
                    dtpStartDate.Value.Date.ToString("yyyy-MM-dd"),
                    dtpEndDate.Value.ToString("yyyy-MM-dd"),
                    cboJobStatus.SelectedItem.ToString(),
                    fuel,
                    labour,
                    dumping,
                    selectedQuoteID
                ));

                // 2. Save the assigned time slots linked to that jobID
                foreach (Control control in pnlTimeSlots.Controls)
                {
                    if (control is CheckBox cb && cb.Checked)
                    {
                        timeSlotID = Convert.ToInt32(cb.Tag);
                        jobTimeSlotTableAdapter1.Insert(jobID, timeSlotID);
                    }
                }

                // -----------------------------------------------------------------------------------------
                // 3. AUTOMATICALLY ADD RECORD TO PAYMENT TABLE
                // -----------------------------------------------------------------------------------------
                // Calculate the initial total cost of the job to assign to the payment amount
                decimal totalAmount = fuel + labour + dumping;

                using (PaymentTableAdapter paymentAdapter = new PaymentTableAdapter())
                {
                    paymentAdapter.InsertQuery(
                        DateTime.Today.ToString("yyyy-MM-dd"), // paymentDate as string in correct format
                        totalAmount,
                        "Pending Method",
                        "Unpaid",
                        jobID
                    );
                }
                // -----------------------------------------------------------------------------------------

                MessageBox.Show("Job Scheduled and Payment Profile created successfully! Job ID: " + jobID);

                ClearFormLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving job or generating payment details: " + ex.Message);
            }
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            //ClearFormLayout();
        }

        private void ClearFormLayout()
        {
            selectedQuoteID = 0;

            if (txtSelectedQuoteID != null) txtSelectedQuoteID.Clear();
            if (txtSearchQuote != null) txtSearchQuote.Clear(); // Clears the search box

            txtFuelCost.Text = "0.00";
            txtLabourCost.Text = "0.00";
            txtDumpingCost.Text = "0.00";
            cboJobStatus.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            foreach (Control c in pnlTimeSlots.Controls)
            {
                if (c is CheckBox cb) cb.Checked = false;
            }

            if (dgvJoin != null) dgvJoin.ClearSelection();

            // Refresh slots for today
            LoadTimeSlotsForDate(DateTime.Today);
        }
        
        // --------------------------------------------------------------------------------------------------------
        // CUSTOM TAB DRAWING
        // --------------------------------------------------------------------------------------------------------
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Color backColor = Color.Honeydew;

            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            Color textColor = Color.Black;

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

        private void lblJobStatus_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFormLayout();
        }

        private void txtSearchJobV_TextChanged(object sender, EventArgs e)
        {
            // Pass the selected status item and the typed search string
            string selectedStatus = JobProgressFilter.SelectedItem?.ToString() ?? "All";
            LoadJobs(selectedStatus, txtSearchJobV.Text);
        }

        // Assuming your ComboBox is named JobProgressFilter
        private void JobProgressFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pass the newly selected status item and the typed search string
            string selectedStatus = JobProgressFilter.SelectedItem?.ToString() ?? "All";
            LoadJobs(selectedStatus, txtSearchJobV.Text);
        }

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Prevent crash if they click the header row or an empty row
            if (e.RowIndex < 0 || dgvJobs.Rows[e.RowIndex].Cells["ID"].Value == DBNull.Value) return;

            DataGridViewRow row = dgvJobs.Rows[e.RowIndex];
            selectedJobID = Convert.ToInt32(row.Cells["ID"].Value);

            // 2. Core Job Identifiers & Status
            lbJobID.Text = "Job ID: " + selectedJobID.ToString();
            jobStatuslb.Text = "Status: " + row.Cells["Status"].Value?.ToString();

            // 3. Client & Location Details
            lblClientJobName.Text = "Client: " + row.Cells["Client Name"].Value?.ToString() + " " + row.Cells["Client Surname"].Value?.ToString();
            siteaddresslb.Text = "Site Address: " + row.Cells["Address"].Value?.ToString();

            // 4. Safely format the Start Date
            if (row.Cells["Start Date"].Value != DBNull.Value && row.Cells["Start Date"].Value != null)
            {
                DateTime startDate = Convert.ToDateTime(row.Cells["Start Date"].Value);
                jobStartDatelb.Text = "Start Date: " + startDate.ToString("yyyy/MM/dd");
            }
            else
            {
                jobStartDatelb.Text = "Start Date: N/A";
            }

            // 5. Safely format the End Date
            if (row.Cells["End Date"].Value != DBNull.Value && row.Cells["End Date"].Value != null)
            {
                DateTime endDate = Convert.ToDateTime(row.Cells["End Date"].Value);
                jobEndDatelb.Text = "End Date: " + endDate.ToString("yyyy/MM/dd");
            }
            else
            {
                jobEndDatelb.Text = "End Date: N/A";
            }

            // 6. Financial Costs Formatting (Formats decimal numbers to currency: e.g., R1,250.00)
            decimal fuel = row.Cells["Fuel Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Fuel Cost"].Value) : 0.00m;
            decimal labour = row.Cells["Labour Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Labour Cost"].Value) : 0.00m;
            decimal dumping = row.Cells["Dumping Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Dumping Cost"].Value) : 0.00m;

            lblFuelJobCost.Text = "Total Fuel Cost: " + fuel.ToString("C2");
            lblJobLabourCost.Text = "Total Labour Cost: " + labour.ToString("C2");
            lblDumpJobCost.Text = "Dumping Cost: " + dumping.ToString("C2");
           // lblDetailTotalCost.Text = "Overall Job Cost: " + (fuel + labour + dumping).ToString("C2");

            // 7. System Tracking Keys
            lblJobQuoteID.Text = "Linked Quote ID: " + row.Cells["Quote ID"].Value?.ToString();

            // 8. Colour code the status text color dynamically
            string status = row.Cells["Status"].Value?.ToString();
            if (status == "Completed") jobStatuslb.ForeColor = Color.Green;
            else if (status == "In Progress") jobStatuslb.ForeColor = Color.Orange;
            else jobStatuslb.ForeColor = Color.Red; // Not Started
        }
        void SetupGrid()
        {
            dgvJobs.ReadOnly = true;
            dgvJobs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobs.MultiSelect = false;
            dgvJobs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJobs.RowHeadersVisible = false;
            dgvJobs.AllowUserToAddRows = false;
            dgvJobs.BackgroundColor = Color.FromArgb(155, 198, 138);
            dgvJobs.DefaultCellStyle.SelectionBackColor = Color.Green;
            dgvJobs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            dgvJobs.EnableHeadersVisualStyles = false;
        }
        void LoadJobs(string statusFilter = "All", string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Base SQL Query selecting all needed details
                string sql = @"
    SELECT 
        j.jobID           AS [ID],
        c.clientName      AS [Client Name],
        c.clientSurname   AS [Client Surname],
        jr.siteAddress    AS [Address],
        j.startDate       AS [Start Date],
        j.endDate         AS [End Date], 
        j.jobStatus       AS [Status],
        j.totalFuelCost   AS [Fuel Cost],
        j.totalLabourCost AS [Labour Cost],
        j.dumpingCost     AS [Dumping Cost],
        j.quoteID         AS [Quote ID]
    FROM Job j
    INNER JOIN Quote q ON j.quoteID = q.QuoteID
    INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
    INNER JOIN Client c ON jr.clientID = c.clientID
    WHERE 1=1"; // 'WHERE 1=1' is a trick that allows us to dynamically append 'AND' conditions safely

                // 1. Handle the ComboBox Status Filter
                if (statusFilter != "All" && !string.IsNullOrWhiteSpace(statusFilter))
                {
                    sql += " AND j.jobStatus = @status";
                }

                // 2. Handle the TextBox Search Filter
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    sql += " AND (c.clientName LIKE @search OR c.clientSurname LIKE @search OR jr.siteAddress LIKE @search)";
                }

                sql += " ORDER BY j.startDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                // Assign parameters safely to avoid crashes
                if (statusFilter != "All" && !string.IsNullOrWhiteSpace(statusFilter))
                {
                    da.SelectCommand.Parameters.AddWithValue("@status", statusFilter);
                }
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    da.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                }

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvJobs.DataSource = dt;

                // Hide structural tracking columns
                if (dgvJobs.Columns["ID"] != null) dgvJobs.Columns["ID"].Visible = false;
                if (dgvJobs.Columns["Fuel Cost"] != null) dgvJobs.Columns["Fuel Cost"].Visible = false;
                if (dgvJobs.Columns["Labour Cost"] != null) dgvJobs.Columns["Labour Cost"].Visible = false;
                if (dgvJobs.Columns["Dumping Cost"] != null) dgvJobs.Columns["Dumping Cost"].Visible = false;
                if (dgvJobs.Columns["Quote ID"] != null) dgvJobs.Columns["Quote ID"].Visible = false;
            }
            ColourJobRows();
        }

        void ColourJobRows()
        {
            foreach (DataGridViewRow row in dgvJobs.Rows)
            {
                string status = row.Cells["Status"].Value?.ToString();

                // Example: Color based on progress
                if (status == "Completed") row.DefaultCellStyle.BackColor = Color.LightGreen;
                else if (status == "In Progress") row.DefaultCellStyle.BackColor = Color.LightYellow;
                else if (status == "Not Started") row.DefaultCellStyle.BackColor = Color.LightCoral;
                else row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void JobAddBtn_Click(object sender, EventArgs e)
        {
            // Defensive checks
            if (tabControl1 == null || tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
                return;

            // Try to find a TabPage named "tabPage1"
            TabPage target = null;
            try
            {
                target = tabControl1.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp != null && tp.Name == "tabPage1");
            }
            catch
            {
                // Ignore LINQ issues and fallback to index selection below
                target = null;
            }

            if (target != null)
            {
                tabControl1.SelectedTab = target;
            }
            else
            {
                // Fallback to the first tab
                tabControl1.SelectedIndex = 0;
            }

            // Ensure the tab control has focus so the UI updates visibly
            tabControl1.Focus();
        }

        private void JobEditBtn_Click(object sender, EventArgs e)
        {
            // Defensive checks
            if (tabControl1 == null || tabControl1.TabPages == null || tabControl1.TabPages.Count < 3)
                return;

            // Navigate to tabPage3 (index 2)
            tabControl1.SelectedIndex = 2;
            tabControl1.Focus();
        }

        private void ArchiverJobBtn_Click(object sender, EventArgs e)
        {
            if (selectedJobID == 0)
            {
                MessageBox.Show("Please select a job to archive.", "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to archive this job record? Job ID: " + selectedJobID,
                "Confirm Job Archive",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Archive logic will be implemented here
                    MessageBox.Show("Job archived successfully!", "Archive Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error archiving job: " + ex.Message, "Archive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
