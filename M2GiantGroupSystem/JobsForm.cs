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
            ThemeManager.ThemeChanged += ApplyTheme;
            InitializeComponent();
            tabIndex = tab_index;
        }

        private void ClearUpdateFormLayout()
        {
            selectedJobID = 0;
            txtUpdateJobID.Clear();
            dtpUpdateStartDate.Value = DateTime.Today;
            dtpUpdateEndDate.Value = DateTime.Today;
            cboUpdateJobStatus.SelectedIndex = -1;
            txtUpdateFuelCost.Clear();
            txtUpdateLabourCost.Clear();
            txtUpdateDumpingCost.Clear();
            txtUpdateQuoteID.Clear();
        }

        void ApplyStatusColoring(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["Status"] != null && row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString();
                    switch (status)
                    {
                        case "Completed":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "In Progress":
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                        case "Not Started":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
                }
            }
        }

        private void ViewJobDetailsForm_Load(object sender, EventArgs e)
        {
            SetupGridStyles();
            JobProgressFilter.Items.Clear();
            JobProgressFilter.Items.AddRange(new string[] { "All", "Not Started", "In Progress", "Completed" });
            JobProgressFilter.SelectedIndex = 0;
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

                dgvJoin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvJoin.Columns["Client Name"].FillWeight = 60;
                dgvJoin.Columns["Client Surname"].FillWeight = 60;
                dgvJoin.Columns["Site Address"].FillWeight = 150;
                dgvJoin.Columns["Date Received"].FillWeight = 70;
                dgvJoin.Columns["Quote File"].FillWeight = 120;
                dgvJoin.RowTemplate.Height = 32;
                dgvJoin.ColumnHeadersHeight = 40;

                if (dgvJoin.Columns["QuoteID"] != null)
                    dgvJoin.Columns["QuoteID"].Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadAcceptedQuotes(txtSearchQuote.Text);
        }

        private void JobsForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            SetupGridStyles();

            // UI Defaults
            tabControl1.SelectedIndex = tabIndex;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;

            cboJobStatus.Items.AddRange(new string[] { "Not Started", "In Progress", "Completed" });
            cboJobStatus.SelectedIndex = 0;

            cmbCriteriaSeach.Items.Clear();
            cmbCriteriaSeach.Items.AddRange(new string[] { "Name", "Surname", "Job Status", "Site Adress" });

            JobProgressFilter.Items.AddRange(new string[] { "All", "Not Started", "In Progress", "Completed" });
            JobProgressFilter.SelectedIndex = 0;

            // Load Data
            loadAcceptedQuotes();
            LoadJobs();
            LoadTimeSlotsForDate(dtpStartDate.Value);
        }

        // --------------------------------------------------------------------------------------------------------
        // DATAGRIDVIEW SINGLE CLICK EVENT
        // --------------------------------------------------------------------------------------------------------
        private void dgvJoin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value == DBNull.Value ||
                dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value == null)
                return;

            selectedQuoteID = Convert.ToInt32(dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value);

            if (txtSelectedQuoteID != null)
                txtSelectedQuoteID.Text = selectedQuoteID.ToString();

            MessageBox.Show("Quote Selected! ID: " + selectedQuoteID, "Quote Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --------------------------------------------------------------------------------------------------------
        // DYNAMIC TIME SLOT CHECKBOXES (FILTERS OUT BOOKED SLOTS WITH FIXED WIDTH GRID WRAPPING)
        // --------------------------------------------------------------------------------------------------------
        private void LoadTimeSlotsForDate(DateTime selectedDate)
        {
            pnlTimeSlots.Controls.Clear();
            pnlTimeSlots.AutoScroll = true;
            pnlTimeSlots.AutoSize = false;

            var bookedSlots = new HashSet<int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Fetch booked slot IDs for selected date context
                    string bookedSql = @"
                        SELECT jts.timeSlotID
                        FROM JobTimeSlot jts
                        INNER JOIN Job j ON jts.jobID = j.jobID
                        WHERE CAST(j.startDate AS DATE) = @d";

                    using (var cmd = new SqlCommand(bookedSql, conn))
                    {
                        cmd.Parameters.Add("@d", System.Data.SqlDbType.Date).Value = selectedDate.Date;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    bookedSlots.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    // 2. Fetch master definitions and process horizontal flow layouts
                    string slotsSql = "SELECT timeSlotID, startTime, endTime FROM TimeSlot ORDER BY startTime";
                    using (var cmdSlots = new SqlCommand(slotsSql, conn))
                    {
                        using (var reader = cmdSlots.ExecuteReader())
                        {
                            int xPos = 15;
                            int yPos = 20;
                            int horizontalGap = 135;
                            int verticalGap = 35;
                            bool anyAdded = false;

                            while (reader.Read())
                            {
                                if (reader.IsDBNull(reader.GetOrdinal("timeSlotID")))
                                    continue;

                                int slotID = reader.GetInt32(reader.GetOrdinal("timeSlotID"));

                                if (bookedSlots.Contains(slotID))
                                    continue;

                                TimeSpan start = TimeSpan.Zero;
                                TimeSpan end = TimeSpan.Zero;

                                int startIdx = reader.GetOrdinal("startTime");
                                int endIdx = reader.GetOrdinal("endTime");

                                if (!reader.IsDBNull(startIdx))
                                {
                                    object sVal = reader.GetValue(startIdx);
                                    if (sVal is TimeSpan span) start = span;
                                    else if (sVal is DateTime time) start = time.TimeOfDay;
                                }

                                if (!reader.IsDBNull(endIdx))
                                {
                                    object eVal = reader.GetValue(endIdx);
                                    if (eVal is TimeSpan span) end = span;
                                    else if (eVal is DateTime time) end = time.TimeOfDay;
                                }

                                var cb = new CheckBox
                                {
                                    AutoSize = false,
                                    Width = 115,  // Provides safety from truncations like '08:00 -'
                                    Height = 25,
                                    Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                                    Tag = slotID,
                                    Text = string.Format("{0:00}:{1:00} - {2:00}:{3:00}",
                                        start.Hours, start.Minutes,
                                        end.Hours, end.Minutes),
                                    TextAlign = ContentAlignment.MiddleLeft,
                                    Location = new Point(xPos, yPos)
                                };

                                pnlTimeSlots.Controls.Add(cb);
                                anyAdded = true;

                                // Slide layout target to the right
                                xPos += horizontalGap;

                                // Wrap row breaks gracefully when panel bounds are exceeded
                                if (xPos + horizontalGap > pnlTimeSlots.ClientSize.Width)
                                {
                                    xPos = 15;
                                    yPos += verticalGap;
                                }
                            }

                            if (!anyAdded)
                            {
                                pnlTimeSlots.Controls.Add(new Label
                                {
                                    Text = "All time slots are booked for this date.",
                                    AutoSize = true,
                                    ForeColor = Color.Red,
                                    Location = new Point(15, 15),
                                    Font = new Font("Segoe UI", 11f, FontStyle.Bold)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available time slots: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpStartDate_ValueChanged_1(object sender, EventArgs e)
        {
            //LoadTimeSlotsForDate(dtpStartDate.Value);
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
                return;

            try
            {
                decimal fuel = string.IsNullOrWhiteSpace(txtFuelCost.Text) ? 0.00m : Convert.ToDecimal(txtFuelCost.Text);
                decimal labour = string.IsNullOrWhiteSpace(txtLabourCost.Text) ? 0.00m : Convert.ToDecimal(txtLabourCost.Text);
                decimal dumping = string.IsNullOrWhiteSpace(txtDumpingCost.Text) ? 0.00m : Convert.ToDecimal(txtDumpingCost.Text);

                jobID = Convert.ToInt32(jobTableAdapter1.InsertQuery(
                    dtpStartDate.Value.Date.ToString("yyyy-MM-dd"),
                    dtpEndDate.Value.ToString("yyyy-MM-dd"),
                    cboJobStatus.SelectedItem.ToString(),
                    fuel,
                    labour,
                    dumping,
                    selectedQuoteID
                ));

                foreach (Control control in pnlTimeSlots.Controls)
                {
                    if (control is CheckBox cb && cb.Checked)
                    {
                        timeSlotID = Convert.ToInt32(cb.Tag);
                        jobTimeSlotTableAdapter1.Insert(jobID, timeSlotID);
                    }
                }

                decimal totalAmount = fuel + labour + dumping;

                using (PaymentTableAdapter paymentAdapter = new PaymentTableAdapter())
                {
                    paymentAdapter.InsertQuery(
                        DateTime.Today.ToString("yyyy-MM-dd"),
                        totalAmount,
                        "Pending Method",
                        "Unpaid",
                        jobID
                    );
                }

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
            // ClearFormLayout();
        }

        private void ClearFormLayout()
        {
            selectedQuoteID = 0;

            if (txtSelectedQuoteID != null) txtSelectedQuoteID.Clear();
            if (txtSearchQuote != null) txtSearchQuote.Clear();

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
                backColor = Color.LightGreen;

            Color textColor = Color.Black;

            using (Brush b = new SolidBrush(backColor))
                e.Graphics.FillRectangle(b, tabRect);

            using (Pen p = new Pen(Color.DarkGreen, 1))
                e.Graphics.DrawRectangle(p, tabRect);

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private void lblJobStatus_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFormLayout();
        }

        private void txtSearchJobV_TextChanged(object sender, EventArgs e)
        {
            string selectedStatus = JobProgressFilter.SelectedItem?.ToString() ?? "All";
            LoadJobs(selectedStatus, txtSearchJobV.Text);
        }

        private void JobProgressFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = JobProgressFilter.SelectedItem?.ToString() ?? "All";
            LoadJobs(selectedStatus, txtSearchJobV.Text);
        }

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvJobs.Rows[e.RowIndex].Cells["ID"].Value == DBNull.Value) return;

            DataGridViewRow row = dgvJobs.Rows[e.RowIndex];
            selectedJobID = Convert.ToInt32(row.Cells["ID"].Value);

            lbJobID.Text = "Job ID: " + selectedJobID.ToString();
            jobStatuslb.Text = "Status: " + row.Cells["Status"].Value?.ToString();
            lblClientJobName.Text = "Client: " + row.Cells["Client Name"].Value?.ToString() + " " + row.Cells["Client Surname"].Value?.ToString();
            siteaddresslb.Text = "Site Address: " + row.Cells["Address"].Value?.ToString();

            if (row.Cells["Start Date"].Value != DBNull.Value && row.Cells["Start Date"].Value != null)
            {
                DateTime startDate = Convert.ToDateTime(row.Cells["Start Date"].Value);
                jobStartDatelb.Text = "Start Date: " + startDate.ToString("yyyy/MM/dd");
            }
            else
            {
                jobStartDatelb.Text = "Start Date: N/A";
            }

            if (row.Cells["End Date"].Value != DBNull.Value && row.Cells["End Date"].Value != null)
            {
                DateTime endDate = Convert.ToDateTime(row.Cells["End Date"].Value);
                jobEndDatelb.Text = "End Date: " + endDate.ToString("yyyy/MM/dd");
            }
            else
            {
                jobEndDatelb.Text = "End Date: N/A";
            }

            decimal fuel = row.Cells["Fuel Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Fuel Cost"].Value) : 0.00m;
            decimal labour = row.Cells["Labour Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Labour Cost"].Value) : 0.00m;
            decimal dumping = row.Cells["Dumping Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Dumping Cost"].Value) : 0.00m;

            lblFuelJobCost.Text = "Total Fuel Cost: " + fuel.ToString("C2");
            lblJobLabourCost.Text = "Total Labour Cost: " + labour.ToString("C2");
            lblDumpJobCost.Text = "Dumping Cost: " + dumping.ToString("C2");

            lblJobQuoteID.Text = "Linked Quote ID: " + row.Cells["Quote ID"].Value?.ToString();

            string status = row.Cells["Status"].Value?.ToString();
            if (status == "Completed") jobStatuslb.ForeColor = Color.Green;
            else if (status == "In Progress") jobStatuslb.ForeColor = Color.Orange;
            else jobStatuslb.ForeColor = Color.Red;
        }

        void SetupGridStyles()
        {
            foreach (DataGridView dgv in new[] { dgvJobs, dgvUpdateJob, dgvJoin })
            {
                dgv.ReadOnly = true;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AllowUserToAddRows = false;
                dgv.BackgroundColor = Color.FromArgb(155, 198, 138);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
                dgv.EnableHeadersVisualStyles = false;

                dgv.DefaultCellStyle.SelectionBackColor = Color.Green;
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            }
        }

        void LoadJobs(string statusFilter = "All", string searchTerm = "")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
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
                    LEFT JOIN Quote q ON j.quoteID = q.QuoteID
                    LEFT JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                    LEFT JOIN Client c ON jr.clientID = c.clientID
                    WHERE 1=1";

                if (statusFilter != "All" && !string.IsNullOrWhiteSpace(statusFilter))
                    sql += " AND j.jobStatus = @status";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                    sql += " AND (c.clientName LIKE @search OR c.clientSurname LIKE @search OR jr.siteAddress LIKE @search)";

                sql += " ORDER BY j.startDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                if (statusFilter != "All" && !string.IsNullOrWhiteSpace(statusFilter))
                    da.SelectCommand.Parameters.AddWithValue("@status", statusFilter);

                if (!string.IsNullOrWhiteSpace(searchTerm))
                    da.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvJobs.DataSource = dt;
                dgvUpdateJob.DataSource = dt;

                foreach (DataGridView dgv in new[] { dgvJobs, dgvUpdateJob })
                {
                    if (dgv.Columns["ID"] != null) dgv.Columns["ID"].Visible = false;
                    if (dgv.Columns["Fuel Cost"] != null) dgv.Columns["Fuel Cost"].Visible = false;
                    if (dgv.Columns["Labour Cost"] != null) dgv.Columns["Labour Cost"].Visible = false;
                    if (dgv.Columns["Dumping Cost"] != null) dgv.Columns["Dumping Cost"].Visible = false;
                    if (dgv.Columns["Quote ID"] != null) dgv.Columns["Quote ID"].Visible = false;
                }
            }

            ApplyStatusColoring(dgvJobs);
            ApplyStatusColoring(dgvUpdateJob);
        }

        private void JobAddBtn_Click(object sender, EventArgs e)
        {
            if (tabControl1 == null || tabControl1.TabPages == null || tabControl1.TabPages.Count == 0)
                return;

            TabPage target = null;
            try
            {
                target = tabControl1.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp != null && tp.Name == "tabPage1");
            }
            catch
            {
                target = null;
            }

            if (target != null)
                tabControl1.SelectedTab = target;
            else
                tabControl1.SelectedIndex = 0;

            tabControl1.Focus();
        }

        private void JobEditBtn_Click(object sender, EventArgs e)
        {
            if (tabControl1 == null || tabControl1.TabPages == null || tabControl1.TabPages.Count < 3)
                return;

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
                    MessageBox.Show("Job archived successfully!", "Archive Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error archiving job: " + ex.Message, "Archive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void searchJobUp_Click(object sender, EventArgs e)
        {
            string criteria = cmbCriteriaSeach.SelectedItem?.ToString();
            string searchTerm = txtSearchUpdate.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            if (string.IsNullOrWhiteSpace(criteria))
            {
                MessageBox.Show("Please select a search criteria from the dropdown.");
                return;
            }

            try
            {
                string sql = @"
                    SELECT 
                        j.jobID           AS [Job ID], 
                        c.clientName      AS [Name], 
                        c.clientSurname   AS [Surname], 
                        j.jobStatus       AS [Status], 
                        jr.siteAddress    AS [Address]
                    FROM Job j
                    INNER JOIN Quote q ON j.quoteID = q.QuoteID
                    INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                    INNER JOIN Client c ON jr.clientID = c.clientID
                    WHERE ";

                switch (criteria)
                {
                    case "Name": sql += "c.clientName LIKE @val"; break;
                    case "Surname": sql += "c.clientSurname LIKE @val"; break;
                    case "Job Status": sql += "j.jobStatus LIKE @val"; break;
                    case "Site Adress": sql += "jr.siteAddress LIKE @val"; break;
                    default:
                        MessageBox.Show("Invalid search criteria selected.");
                        return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@val", "%" + searchTerm + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgvUpdateJob.DataSource = dt;
                        dgvUpdateJob.ColumnHeadersVisible = true;

                        if (dgvUpdateJob.Columns["Job ID"] != null)
                            dgvUpdateJob.Columns["Job ID"].Visible = false;
                    }
                    else
                    {
                        dgvUpdateJob.DataSource = null;
                        MessageBox.Show("No records found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message);
            }
        }

        private void dgvUpdateJob_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || dgvUpdateJob.Rows[e.RowIndex].Cells["ID"].Value == DBNull.Value || dgvUpdateJob.Rows[e.RowIndex].Cells["ID"].Value == null)
                    return;

                DataGridViewRow row = dgvUpdateJob.Rows[e.RowIndex];

                selectedJobID = Convert.ToInt32(row.Cells["ID"].Value);
                txtUpdateJobID.Text = selectedJobID.ToString();

                if (row.Cells["Start Date"].Value != DBNull.Value && row.Cells["Start Date"].Value != null)
                    dtpUpdateStartDate.Value = Convert.ToDateTime(row.Cells["Start Date"].Value);

                if (row.Cells["End Date"].Value != DBNull.Value && row.Cells["End Date"].Value != null)
                    dtpUpdateEndDate.Value = Convert.ToDateTime(row.Cells["End Date"].Value);
                else
                    dtpUpdateEndDate.Value = DateTime.Today;

                string statusValue = row.Cells["Status"].Value?.ToString();
                if (cboUpdateJobStatus.Items.Contains(statusValue))
                    cboUpdateJobStatus.SelectedItem = statusValue;
                else
                    cboUpdateJobStatus.SelectedIndex = -1;

                decimal fuel = row.Cells["Fuel Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Fuel Cost"].Value) : 0.00m;
                decimal labour = row.Cells["Labour Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Labour Cost"].Value) : 0.00m;
                decimal dumping = row.Cells["Dumping Cost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["Dumping Cost"].Value) : 0.00m;

                txtUpdateFuelCost.Text = fuel.ToString("F2");
                txtUpdateLabourCost.Text = labour.ToString("F2");
                txtUpdateDumpingCost.Text = dumping.ToString("F2");

                txtUpdateQuoteID.Text = row.Cells["Quote ID"].Value?.ToString();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Grid Column Error: {ex.Message}\n\nPlease check that your SQL column names match your CellClick names exactly.",
                                "Column Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading selected row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUpdateJobID.Text) || selectedJobID == 0)
            {
                MessageBox.Show("Please select a job from the search results grid first.", "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboUpdateJobStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid Job Status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to save changes to Job ID: {selectedJobID}?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                decimal fuel = string.IsNullOrWhiteSpace(txtUpdateFuelCost.Text) ? 0.00m : Convert.ToDecimal(txtUpdateFuelCost.Text);
                decimal labour = string.IsNullOrWhiteSpace(txtUpdateLabourCost.Text) ? 0.00m : Convert.ToDecimal(txtUpdateLabourCost.Text);
                decimal dumping = string.IsNullOrWhiteSpace(txtUpdateDumpingCost.Text) ? 0.00m : Convert.ToDecimal(txtUpdateDumpingCost.Text);

                jobTableAdapter1.UpdateJobQuery(
                    dtpUpdateStartDate.Value.Date.ToString("yyyy-MM-dd"),
                    dtpUpdateEndDate.Value.Date.ToString("yyyy-MM-dd"),
                    cboUpdateJobStatus.SelectedItem.ToString(),
                    fuel,
                    labour,
                    dumping,
                    selectedJobID
                );

                MessageBox.Show($"Job ID {selectedJobID} updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string selectedStatus = JobProgressFilter.SelectedItem?.ToString() ?? "All";
                LoadJobs(selectedStatus, txtSearchJobV.Text);

                ClearUpdateFormLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating job details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_enterDetails_Click(object sender, EventArgs e) { }

        private void tabPage1_Click(object sender, EventArgs e) { }

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

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTimeSlotsForDate(dtpStartDate.Value);
        }
    }
}