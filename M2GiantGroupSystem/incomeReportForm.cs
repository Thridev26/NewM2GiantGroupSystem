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
    public partial class incomeReportForm : Form
    {
        public static DateTime SelectedWeekStart;
        public static DateTime SelectedWeekEnd;

        DBConnect DB1 = new DBConnect();
        public incomeReportForm()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
           // LoadIncomeReport();
        }

        private void button1_Click(object sender, EventArgs e)
        { //make sure date pickers arent null
          // Null guard (defensive — WinForms DateTimePicker always has a value,
          // but kept for safety in case the control is ever swapped out)
            if (dt1.Value == null || dt2.Value == null)
            {
                MessageBox.Show(
                    "Please select both a start date and an end date.",
                    "Missing Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Date order check
            if (dt1.Value.Date > dt2.Value.Date)
            {
                MessageBox.Show(
                    "The start date cannot be after the end date. Please correct the date range.",
                    "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dt1.Focus();
                return;
            }

            // Prevent future date ranges — no payments can exist yet
            if (dt1.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show(
                    "The start date cannot be in the future. Please select a past or current date range.",
                    "Invalid Start Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dt1.Focus();
                return;
            }

            // Warn if the range is unusually wide (over 1 year) — likely a mistake
            if ((dt2.Value.Date - dt1.Value.Date).TotalDays > 365)
            {
                DialogResult confirm = MessageBox.Show(
                    "The selected date range spans more than a year. Are you sure you want to generate this report?",
                    "Large Date Range", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;
            }

            SelectedWeekStart = dt1.Value.Date;
            SelectedWeekEnd = dt2.Value.Date;
            reportReady = true;

            tabControl1.SelectedIndex = 1;

        }

        private void incomeReportForm_Load(object sender, EventArgs e)
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            Color backColor = Color.Honeydew;
            Color textColor = Color.Black;

            if (e.Index == tabControl1.SelectedIndex)
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
        bool reportReady = false;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1 && !reportReady)
            {
                e.Cancel = true;
                MessageBox.Show("Please select a date range and click Generate Report first.",
                    "Report Not Generated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (e.TabPageIndex == 1 && reportReady)
            {
                LoadIncomeReport();
                reportReady = false; // reset so next date change requires Generate again
            }
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            bool reportReady = false;
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            bool reportReady = false;
        }
        private void LoadIncomeReport()
        {
            // paste the entire contents of crystalReportViewer1_Load here
            // (everything inside the try block)
            try
            {
                // Guard: ensure dates were actually set before the report tab loads
                if (SelectedWeekStart == DateTime.MinValue || SelectedWeekEnd == DateTime.MinValue)
                {
                    MessageBox.Show(
                        "Report dates have not been set. Please select a date range first.",
                        "No Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                DataSet ds = DB1.WeeklyIncomeData(SelectedWeekStart, SelectedWeekEnd);

                if (ds == null || ds.Tables.Count == 0)
                {
                    MessageBox.Show(
                        "No data was returned from the database for the selected date range.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                List<WeeklyIncomeDetails> list = new List<WeeklyIncomeDetails>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        list.Add(new WeeklyIncomeDetails()
                        {
                            PaymentID = Convert.ToInt32(dr["paymentID"]),
                            PaymentDate = Convert.ToDateTime(dr["paymentDate"]),
                            JobID = Convert.ToInt32(dr["jobID"]),
                            ClientName = dr["ClientName"].ToString(),
                            AmountPaid = Convert.ToDecimal(dr["amountPaid"])
                        });
                    }
                    catch (Exception rowEx)
                    {
                        // Skip malformed rows and continue rather than crashing the whole report
                        MessageBox.Show(
                            "A row in the income data could not be read and was skipped:\n" + rowEx.Message,
                            "Row Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                if (list.Count == 0)
                {
                    MessageBox.Show(
                        "No payment records were found for the selected date range.\n\n" +
                        $"From: {SelectedWeekStart:dd/MM/yyyy}  To: {SelectedWeekEnd:dd/MM/yyyy}",
                        "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                incomeReport1.SetDataSource(list);

                // Fetch summary totals
                DataSet summary = DB1.WeeklyIncomeSummary(SelectedWeekStart, SelectedWeekEnd);

                decimal totalIncome = 0;

                if (summary != null &&
                    summary.Tables.Count > 0 &&
                    summary.Tables[0].Rows.Count > 0 &&
                    summary.Tables[0].Rows[0]["TotalIncome"] != DBNull.Value)
                {
                    totalIncome = Convert.ToDecimal(summary.Tables[0].Rows[0]["TotalIncome"]);
                }

                incomeReport1.SetParameterValue("WeekStart", SelectedWeekStart.ToString("dd/MM/yyyy"));
                incomeReport1.SetParameterValue("WeekEnd", SelectedWeekEnd.ToString("dd/MM/yyyy"));
                incomeReport1.SetParameterValue("TotalIncome", totalIncome);

                crystalReportViewer1.ReportSource = incomeReport1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error while loading the income report:\n" + ex.Message,
                    "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl1.SelectedIndex = 0;
            }
        }
    }
}
