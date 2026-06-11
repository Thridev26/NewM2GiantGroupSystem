using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
        }

        int jobID;
        DateTime currentMonth = DateTime.Now;
        DateTime? selectedDate = null;

        string connStr =
            "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

        // ─────────────────────────────────────────────
        // FORM LOAD
        // ─────────────────────────────────────────────
        private void Calendar_Load(object sender, EventArgs e)
        {
            try
            {
                for (int y = 2024; y <= 2030; y++)
                    comboBoxYear.Items.Add(y.ToString());

                for (int i = 1; i <= 12; i++)
                    comboBoxMonth.Items.Add(new DateTime(2026, i, 1).ToString("MMMM"));

                comboBoxYear.SelectedItem = DateTime.Now.Year.ToString();
                comboBoxMonth.SelectedIndex = DateTime.Now.Month - 1;

                SetupGrid();
                RefreshCalendar();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading the calendar:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading the calendar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // GRID SETUP
        // ─────────────────────────────────────────────
        void SetupGrid()
        {
            tlpCalendar.Controls.Clear();
            tlpCalendar.ColumnStyles.Clear();
            tlpCalendar.RowStyles.Clear();

            for (int i = 0; i < 7; i++)
                tlpCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));

            for (int i = 0; i < 6; i++)
                tlpCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));
        }

        // ─────────────────────────────────────────────
        // REFRESH CALENDAR
        // ─────────────────────────────────────────────
        void RefreshCalendar()
        {
            if (comboBoxMonth.SelectedIndex < 0 || comboBoxYear.SelectedItem == null)
                return;

            try
            {
                int month = comboBoxMonth.SelectedIndex + 1;
                int year = Convert.ToInt32(comboBoxYear.SelectedItem);

                DateTime selectedMonth = new DateTime(year, month, 1);
                currentMonth = selectedMonth;

                LoadCalendar(selectedMonth);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while refreshing the calendar:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while refreshing the calendar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // LOAD CALENDAR
        // ─────────────────────────────────────────────
        void LoadCalendar(DateTime month)
        {
            try
            {
                lblMonth.Text = month.ToString("MMMM yyyy");
                tlpCalendar.Controls.Clear();

                DateTime firstDay = new DateTime(month.Year, month.Month, 1);
                int startOffset = (int)firstDay.DayOfWeek;
                int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

                for (int i = 0; i < startOffset; i++)
                {
                    Panel blank = new Panel();
                    blank.BorderStyle = BorderStyle.None;
                    tlpCalendar.Controls.Add(blank);
                }

                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime date = new DateTime(month.Year, month.Month, day);
                    Panel dayCell = CreateDayCell(date, month.Month);
                    tlpCalendar.Controls.Add(dayCell);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading the calendar:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading the calendar:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // CREATE DAY CELL
        // ─────────────────────────────────────────────
        Panel CreateDayCell(DateTime date, int currentMonthNum)
        {
            Panel p = new Panel();
            p.Dock = DockStyle.Fill;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Tag = date;
            p.Click += Day_Click;

            Label lbl = new Label();
            lbl.Text = date.Day.ToString();
            lbl.Dock = DockStyle.Top;
            lbl.AutoSize = false;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.ForeColor = Color.Black;
            lbl.Click += Day_Click;

            Label jobLabel = new Label();
            jobLabel.Dock = DockStyle.Bottom;
            jobLabel.TextAlign = ContentAlignment.MiddleCenter;
            jobLabel.Click += Day_Click;

            // Default safe values in case DB calls fail
            int jobCount = 0;
            bool hasWorkers = false;

            try
            {
                jobCount = GetJobCount(date);
                hasWorkers = HasWorkersAssigned(date);
            }
            catch (SqlException sqlEx)
            {
                // DB unavailable — show cell with unknown state rather than crashing
                p.BackColor = Color.WhiteSmoke;
                jobLabel.Text = "?";
                jobLabel.ForeColor = Color.Gray;
                p.Controls.Add(lbl);
                p.Controls.Add(jobLabel);
                return p;
            }
            catch (Exception ex)
            {
                p.BackColor = Color.WhiteSmoke;
                jobLabel.Text = "?";
                jobLabel.ForeColor = Color.Gray;
                p.Controls.Add(lbl);
                p.Controls.Add(jobLabel);
                return p;
            }

            jobLabel.Text = jobCount + " jobs";
            jobLabel.ForeColor = jobCount == 0 ? Color.Gray : Color.ForestGreen;

            if (jobCount > 0 && !hasWorkers)
                p.BackColor = Color.Gray;
            else
                p.BackColor = Color.FromArgb(192, 255, 192);

            p.Controls.Add(lbl);
            p.Controls.Add(jobLabel);

            if (selectedDate.HasValue && date.Date == selectedDate.Value.Date)
            {
                p.BackColor = Color.SteelBlue;
                lbl.ForeColor = Color.White;
                jobLabel.ForeColor = Color.White;
            }

            return p;
        }

        // ─────────────────────────────────────────────
        // GET JOB COUNT
        // ─────────────────────────────────────────────
        int GetJobCount(DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM Job
                    WHERE CAST(startDate AS DATE) = @date";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date.Date);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        // ─────────────────────────────────────────────
        // HAS WORKERS ASSIGNED
        // ─────────────────────────────────────────────
        bool HasWorkersAssigned(DateTime date)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"
                    SELECT COUNT(*)
                    FROM Job j
                    INNER JOIN JobStaffAssignment js ON j.jobID = js.jobID
                    WHERE CAST(j.startDate AS DATE) = @date";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date.Date);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        // ─────────────────────────────────────────────
        // DAY CLICK
        // ─────────────────────────────────────────────
        private void Day_Click(object sender, EventArgs e)
        {
            try
            {
                Control c = sender as Control;

                while (c != null && !(c is Panel))
                    c = c.Parent;

                if (c == null || c.Tag == null)
                    return;

                selectedDate = (DateTime)c.Tag;

                lblSelectedDate.Text = "All jobs on selected date: " +
                    selectedDate.Value.ToString("dd MMMM yyyy");

                LoadJobsForDay(selectedDate.Value);

                pnlDetails.Controls.Clear();
                lblDetails.Text = "Double-click a job to see its assigned workers.";

                LoadCalendar(currentMonth);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading jobs for selected date:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while selecting date:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // LOAD JOBS FOR DAY
        // ─────────────────────────────────────────────
        void LoadJobsForDay(DateTime date)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT
                            jobID,
                            startDate,
                            endDate,
                            jobStatus,
                            quoteID,
                            dumpingCost,
                            totalFuelCost,
                            totallabourCost
                        FROM Job
                        WHERE CAST(startDate AS DATE) = @date
                        ORDER BY jobID";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@date", date.Date);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvJobs.DataSource = dt;

                    ColourRows();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading jobs:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading jobs:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // COLOUR ROWS
        // ─────────────────────────────────────────────
        void ColourRows()
        {
            try
            {
                foreach (DataGridViewRow row in dgvJobs.Rows)
                {
                    if (row.IsNewRow) continue;

                    string status = row.Cells["jobStatus"].Value?.ToString();

                    switch (status)
                    {
                        case "In Progress":
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            break;
                        case "Not Started":
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            break;
                        case "Completed":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }

                    // Guard both cells before converting
                    var jobIDCell = row.Cells["jobID"];
                    var startDateCell = row.Cells["startDate"];

                    if (jobIDCell.Value == null || jobIDCell.Value == DBNull.Value) continue;
                    if (startDateCell.Value == null || startDateCell.Value == DBNull.Value) continue;

                    if (!DateTime.TryParse(startDateCell.Value.ToString(), out DateTime startDate))
                        continue;

                    try
                    {
                        if (!HasWorkersAssigned(startDate))
                            row.DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                    catch
                    {
                        // If the worker check fails for one row, skip it rather than
                        // crashing the whole colouring pass
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while colouring rows:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // LOAD WORKERS FOR JOB
        // ─────────────────────────────────────────────
        void LoadWorkersForJob(int jobID)
        {
            try
            {
                pnlDetails.Controls.Clear();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT s.firstName, s.contactNumber, js.hoursWorked
                        FROM JobStaffAssignment js
                        INNER JOIN Staff s ON js.staffID = s.staffID
                        WHERE js.jobID = @jobID";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@jobID", jobID);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int y = 10;
                        bool foundWorkers = false;

                        while (reader.Read())
                        {
                            foundWorkers = true;

                            Label lbl = new Label();
                            lbl.Text =
                                reader["firstName"].ToString() +
                                " | Contact: " + reader["contactNumber"].ToString() +
                                " | Hours: " + reader["hoursWorked"].ToString();

                            lbl.AutoSize = true;
                            lbl.Location = new Point(10, y);
                            pnlDetails.Controls.Add(lbl);
                            y += 25;
                        }

                        if (!foundWorkers)
                        {
                            Label lblNoWorkers = new Label();
                            lblNoWorkers.Text = "No workers assigned to this job.";
                            lblNoWorkers.AutoSize = true;
                            lblNoWorkers.ForeColor = Color.Black;
                            lblNoWorkers.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                            lblNoWorkers.Location = new Point(10, 10);
                            pnlDetails.Controls.Add(lblNoWorkers);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading workers:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading workers:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // GRID DOUBLE CLICK — load workers
        // ─────────────────────────────────────────────
        private void dgvJobs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJobs.Rows[e.RowIndex].Cells["jobID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobID = Convert.ToInt32(cell.Value);
                lblDetails.Text = "Workers assigned to job ID: " + jobID.ToString();
                LoadWorkersForJob(jobID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error selecting job:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // NAVIGATE TO ALLOCATE FORM
        // ─────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            if (jobID == 0)
            {
                MessageBox.Show("Please double-click a job first before navigating to allocate assets.",
                    "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                AppState.selectedIdCalendar = jobID;

                MessageBox.Show("Calendar set ID to: " + AppState.selectedIdCalendar);

                Form1 mdi = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                if (mdi == null)
                {
                    MessageBox.Show("Could not find the main window. Please try again.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                mdi.FormSetup(new AllocateAssetStafftoJob(0));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while navigating:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // COMBO CHANGED
        // ─────────────────────────────────────────────
        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
            => RefreshCalendar();

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
            => RefreshCalendar();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                currentMonth = currentMonth.AddMonths(1);

                // Keep combos in sync with the navigation
                comboBoxYear.SelectedItem = currentMonth.Year.ToString();
                comboBoxMonth.SelectedIndex = currentMonth.Month - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error navigating to next month:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // STUB HANDLERS
        // ─────────────────────────────────────────────
        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvJobs_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
    }
}