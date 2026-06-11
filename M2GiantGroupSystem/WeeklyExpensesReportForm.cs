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
    public partial class WeeklyExpensesReportForm : Form
    {
        public WeeklyExpensesReportForm()
        {
            InitializeComponent();
        }

        DBConnect DB1 = new DBConnect();
        public  DateTime SelectedWeekStart;
        public  DateTime SelectedWeekEnd;

        private void WeeklyExpensesReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
                tabControl1.DrawItem += tabControl1_DrawItem;
                tabControl1.ItemSize = new Size(300, 30);
                tabControl1.SizeMode = TabSizeMode.Fixed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the report form:\n" + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (dt1.Value == null || dt2.Value == null)
                {
                    MessageBox.Show("Please select both a start and end date.",
                        "Missing Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt1.Value.Date > dt2.Value.Date)
                {
                    MessageBox.Show("Start date cannot be after the end date.",
                        "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dt2.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("End date cannot be in the future. Please select a past or current date.",
                        "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Sanity check: warn if range is unusually large (over a year)
                if ((dt2.Value.Date - dt1.Value.Date).TotalDays > 365)
                {
                    DialogResult confirm = MessageBox.Show(
                        "The selected date range spans more than a year. Are you sure you want to continue?",
                        "Large Date Range", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes) return;
                }

                SelectedWeekStart = dt1.Value.Date;
                SelectedWeekEnd = dt2.Value.Date;
                reportReady = true;

                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error processing date selection:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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
                    e.Graphics.FillRectangle(b, tabRect);

                using (Pen p = new Pen(Color.DarkGreen, 1))
                    e.Graphics.DrawRectangle(p, tabRect);

                TextRenderer.DrawText(e.Graphics, page.Text, tabFont, tabRect, textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            
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
                LoadExpenseReport();
                reportReady = false; // reset so next date change requires Generate again
            }
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            reportReady = false;
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            reportReady = false;
        }

        private void LoadExpenseReport()
        {
            // paste the entire contents of crystalReportViewer1_Load here
            // (everything inside the try block)

            // Guard: dates must have been set by button1_Click before this tab loads.
            // If someone navigates here without selecting dates, SelectedWeekStart
            // and SelectedWeekEnd will be DateTime.MinValue (default).
            if (SelectedWeekStart == DateTime.MinValue || SelectedWeekEnd == DateTime.MinValue)
            {
                MessageBox.Show("Please select a date range before viewing the report.",
                    "No Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 0;
                return;
            }

            try
            {
                // ── Load detail rows ──────────────────────────────────────────
                List<WeeklyExpenseDetails> list = new List<WeeklyExpenseDetails>();

                DataSet ds = DB1.WeeklyExpenseData(SelectedWeekStart, SelectedWeekEnd);

                // Guard: DB call could return null or an empty/missing table
                if (ds == null || ds.Tables.Count == 0)
                {
                    MessageBox.Show("No data was returned from the database. The report cannot be generated.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Each field is individually guarded so one bad row
                    // doesn't crash the entire loop.
                    int jobID = 0;
                    if (dr["jobID"] != DBNull.Value)
                        int.TryParse(dr["jobID"].ToString(), out jobID);

                    DateTime endDate = dr["endDate"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(dr["endDate"]);

                    decimal fuelCost = dr["totalFuelCost"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["totalFuelCost"]);

                    decimal labourCost = dr["totalLabourCost"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["totalLabourCost"]);

                    decimal dumpingCost = dr["dumpingCost"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["dumpingCost"]);

                    list.Add(new WeeklyExpenseDetails()
                    {
                        JobID = jobID,
                        EndDate = endDate,
                        FuelCost = fuelCost,
                        LabourCost = labourCost,
                        DumpingCost = dumpingCost
                    });
                }

                // Guard: SetDataSource will crash if list is null
                if (list.Count == 0)
                {
                    MessageBox.Show("No expense records were found for the selected date range.",
                        "No Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                expensesReport1.SetDataSource(list);

                // ── Load summary totals ───────────────────────────────────────
                DataSet summary = DB1.WeeklyExpenseSummary(SelectedWeekStart, SelectedWeekEnd);

                decimal totalExpense = 0;
                decimal fuelTotal = 0;
                decimal labourTotal = 0;
                decimal dumpingTotal = 0;

                // Guard: summary could also come back null or empty
                if (summary != null && summary.Tables.Count > 0 && summary.Tables[0].Rows.Count > 0)
                {
                    DataRow summaryRow = summary.Tables[0].Rows[0];

                    if (summaryRow["FuelTotal"] != DBNull.Value) fuelTotal = Convert.ToDecimal(summaryRow["FuelTotal"]);
                    if (summaryRow["LabourTotal"] != DBNull.Value) labourTotal = Convert.ToDecimal(summaryRow["LabourTotal"]);
                    if (summaryRow["DumpingTotal"] != DBNull.Value) dumpingTotal = Convert.ToDecimal(summaryRow["DumpingTotal"]);
                    if (summaryRow["ExpenseTotal"] != DBNull.Value) totalExpense = Convert.ToDecimal(summaryRow["ExpenseTotal"]);
                }

                // ── Set report parameters ─────────────────────────────────────
                expensesReport1.SetParameterValue("WeekStart", SelectedWeekStart.ToString("dd/MM/yyyy"));
                expensesReport1.SetParameterValue("WeekEnd", SelectedWeekEnd.ToString("dd/MM/yyyy"));
                expensesReport1.SetParameterValue("FuelTotal", fuelTotal);
                expensesReport1.SetParameterValue("LabourTotal", labourTotal);
                expensesReport1.SetParameterValue("DumpingTotal", dumpingTotal);
                expensesReport1.SetParameterValue("ExpenseTotal", totalExpense);

                crystalReportViewer1.ReportSource = expensesReport1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while generating the report:\n" + ex.Message,
                    "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabControl1.SelectedIndex = 0;
            }
        }
    }
    
}
