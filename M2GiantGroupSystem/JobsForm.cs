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

        public JobsForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }

        // --------------------------------------------------------------------------------------------------------
        // VIEW ACCEPTED QUOTES (INNER JOIN)
        // --------------------------------------------------------------------------------------------------------
        public void loadAcceptedQuotes()
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // INNER JOIN connecting Quote -> JobRequest -> Client
                string sql = @"
                    SELECT 
                        q.QuoteID,
                        c.clientName AS [Client Name],
                        c.clientSurname AS [Client Surname],
                        jr.siteAddress AS [Site Address],
                        jr.dateRecieved AS [Date Received],
                        q.filePath AS [Quote File]
                    FROM Quote q
                    INNER JOIN JobRequest jr 
                        ON q.jobRequestID = jr.jobRequestID
                    INNER JOIN Client c 
                        ON jr.clientID = c.clientID
                    WHERE q.quoteStatus = 'Accepted'";

                // EXACT snippet you requested to fill the datagridview
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvJoin.DataSource = dt;

                // Hide the QuoteID column from the user, but keep it accessible for the code
                if (dgvJoin.Columns["QuoteID"] != null)
                {
                    dgvJoin.Columns["QuoteID"].Visible = false;
                }
            }
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

            // Highlight the entire row green when clicked
            dgvJoin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJoin.DefaultCellStyle.SelectionBackColor = Color.Green;

            // Load the quotes into the grid
            loadAcceptedQuotes();

            // Load the time slot checkboxes
            LoadTimeSlotsForDate(dtpStartDate.Value);
        }

        // --------------------------------------------------------------------------------------------------------
        // DATAGRIDVIEW SINGLE CLICK EVENT 
        // --------------------------------------------------------------------------------------------------------
        private void dgvJoin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Prevent crash if the user clicks the header row
            if (e.RowIndex < 0) return;

            // 2. Prevent crash if they click the empty "new" row at the bottom of the grid
            if (dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value == DBNull.Value ||
                dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value == null)
            {
                return; // Just ignore the click entirely
            }

            // 3. Safe to capture the QuoteID from the selected row!
            selectedQuoteID = Convert.ToInt32(dgvJoin.Rows[e.RowIndex].Cells["QuoteID"].Value);

            // 4. Give visual feedback in the text box
            if (txtSelectedQuoteID != null)
            {
                txtSelectedQuoteID.Text = selectedQuoteID.ToString();
            }

            // 5. Show the pop-up message you requested!
            MessageBox.Show("Quote Selected! ID: " + selectedQuoteID, "Quote Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --------------------------------------------------------------------------------------------------------
        // DYNAMIC TIME SLOT CHECKBOXES
        // --------------------------------------------------------------------------------------------------------
        private void LoadTimeSlotsForDate(DateTime selectedDate)
        {
            pnlTimeSlots.Controls.Clear();

            try
            {
                this.timeSlotTableAdapter1.Fill(this.groupWst1DataSet1.TimeSlot);

                foreach (var slot in this.groupWst1DataSet1.TimeSlot)
                {
                    CheckBox cb = new CheckBox();
                    string start = slot.startTime.ToString(@"hh\:mm");
                    string end = slot.endTime.ToString(@"hh\:mm");

                    cb.Text = $"{start} - {end}";
                    cb.AutoSize = true;
                    cb.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                    cb.Tag = slot.timeSlotID;

                    pnlTimeSlots.Controls.Add(cb);
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

            try
            {
                decimal fuel = string.IsNullOrWhiteSpace(txtFuelCost.Text) ? 0.00m : Convert.ToDecimal(txtFuelCost.Text);
                decimal labour = string.IsNullOrWhiteSpace(txtLabourCost.Text) ? 0.00m : Convert.ToDecimal(txtLabourCost.Text);
                decimal dumping = string.IsNullOrWhiteSpace(txtDumpingCost.Text) ? 0.00m : Convert.ToDecimal(txtDumpingCost.Text);

                // Insert into the Job table and capture the new jobID
                jobID = Convert.ToInt32(jobTableAdapter1.InsertQuery(
                    dtpStartDate.Value.Date.ToString("yyyy-MM-dd"),
                    dtpEndDate.Value.ToString("yyyy-MM-dd"),
                    cboJobStatus.SelectedItem.ToString(),
                    fuel,
                    labour,
                    dumping,
                    selectedQuoteID
                ));

                // Loop through the checkboxes to save the selected time slots
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

            // Add this line so the box empties out for the next job!
            if (txtSelectedQuoteID != null) txtSelectedQuoteID.Clear();

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

            if (dgvJoin != null)
            {
                dgvJoin.ClearSelection();
            }
        }

        // --------------------------------------------------------------------------------------------------------
        // CUSTOM TAB DRAWING
        // --------------------------------------------------------------------------------------------------------
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Get the specific TabPage being rendered and its boundary rectangle
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            // Configure the font style
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Base color for inactive tabs (Muted Mint/White mix)
            Color backColor = Color.Honeydew;

            // Highlight color ONLY for the actively selected tab (Solid Light Green)
            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            // Set text color
            Color textColor = Color.Black;

            // 1. Paint the background rectangle
            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            // 2. Paint the custom border outline around the tab
            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            // 3. Paint the Tab text precisely centered horizontally and vertically
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
    }
}