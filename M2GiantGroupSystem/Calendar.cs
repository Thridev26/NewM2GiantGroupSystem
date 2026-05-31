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
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
        }
        DateTime currentMonth = DateTime.Now;
        DateTime? selectedDate = null;

        string connStr =
            "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

        private void Calendar_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            for (int y = 2024; y <= 2030; y++)
                comboBoxYear.Items.Add(y.ToString());

            for (int i = 1; i <= 12; i++)
                comboBoxMonth.Items.Add(new DateTime(2026, i, 1).ToString("MMMM"));

            comboBoxYear.SelectedItem = DateTime.Now.Year.ToString();
            comboBoxMonth.SelectedIndex = DateTime.Now.Month - 1;

            SetupGrid();
            RefreshCalendar();
        }
        // -----------------------------
        // GRID SETUP
        // -----------------------------
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

        // -----------------------------
        // REFRESH CALENDAR
        // -----------------------------
        void RefreshCalendar()
        {
            if (comboBoxMonth.SelectedIndex < 0 || comboBoxYear.SelectedItem == null)
                return;

            int month = comboBoxMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(comboBoxYear.SelectedItem);

            DateTime selectedMonth = new DateTime(year, month, 1);
            currentMonth = selectedMonth;

            LoadCalendar(selectedMonth);
        }

        // -----------------------------
        // LOAD CALENDAR
        // -----------------------------
        void LoadCalendar(DateTime month)
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

        // -----------------------------
        // CREATE DAY CELL
        // -----------------------------
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

            int jobCount = GetJobCount(date);
            jobLabel.Text = jobCount + " jobs";
            jobLabel.ForeColor = jobCount == 0 ? Color.Gray : Color.ForestGreen;

            // --- RULE: only 2 colours ---
            // Gray if there is at least 1 job AND no workers assigned on that date
            // Honeydew otherwise (default)
            if (jobCount > 0 && !HasWorkersAssigned(date))
                p.BackColor = Color.Gray;
            else
                p.BackColor = Color.Honeydew;

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

        // -----------------------------
        // GET JOB COUNT
        // -----------------------------
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
        // -----------------------------
        // CLICK DAY → SHOW JOBS
        // -----------------------------
        private void Day_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;

            while (c != null && !(c is Panel))
                c = c.Parent;

            if (c == null || c.Tag == null)
                return;

            selectedDate = (DateTime)c.Tag; // 🔥 STORE IT

            lblSelectedDate.Text = "All jobs on selected date: " + selectedDate.Value.ToString("dd MMMM yyyy");

            LoadJobsForDay(selectedDate.Value);

            pnlDetails.Controls.Clear();

            LoadCalendar(currentMonth); // redraw calendar with highlight
        }

        // -----------------------------
        // LOAD JOBS INTO DATAGRIDVIEW
        // -----------------------------
        void LoadJobsForDay(DateTime date)
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

        // -----------------------------
        // COLOUR ROWS IN DATAGRIDVIEW
        // Rule:
        //   In Progress  → Yellow
        //   Completed    → Green
        //   No workers assigned (any status) → Gray (overrides above)
        //   Anything else → White
        // -----------------------------
        void ColourRows()
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

                // Gray override: job is NOT completed AND no workers assigned
                int jobID = Convert.ToInt32(row.Cells["jobID"].Value);
                DateTime startDate = Convert.ToDateTime(row.Cells["startDate"].Value);

                if (!HasWorkersAssigned(startDate))
                {
                    row.DefaultCellStyle.BackColor = Color.Gainsboro;
                }
            }
        }

        // -----------------------------
        // HAS WORKERS ASSIGNED (by date)
        // -----------------------------
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
        void LoadWorkersForJob(int jobID)
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

                SqlDataReader reader = cmd.ExecuteReader();

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

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCalendar();
        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCalendar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            RefreshCalendar();
        }

        private void dgvJobs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvJobs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            int jobID = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["jobID"].Value);
            lblDetails.Text = "Workers assigned to job id: " + jobID.ToString();
            //MessageBox
            //   .Show("Job ID: " + jobID); 
            LoadWorkersForJob(jobID);
        }
    }
}
