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
               
                MessageBox.Show("Job Scheduled successfully! Job ID: " + jobID);

                ClearFormLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving job: " + ex.Message);
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

        }

        private void JobProgressFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}