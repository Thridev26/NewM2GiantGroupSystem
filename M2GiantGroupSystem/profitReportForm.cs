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
    public partial class profitReportForm : Form
    {
        public profitReportForm()
        {
            InitializeComponent();
        }
        DBConnect DB1 = new DBConnect();

        public DateTime SelectedWeekStart;
        public DateTime SelectedWeekEnd;

        bool reportReady = false;
        private void profitReportForm_Load(object sender, EventArgs e)
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
                MessageBox.Show(
                    "Error loading the report form:\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            reportReady = false;
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            reportReady = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt1.Value.Date > dt2.Value.Date)
                {
                    MessageBox.Show(
                        "Start date cannot be after the end date.",
                        "Invalid Date Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                SelectedWeekStart = dt1.Value.Date;
                SelectedWeekEnd = dt2.Value.Date;

                reportReady = true;

                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error processing date selection:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void LoadProfitReport()
        {
            if (SelectedWeekStart == DateTime.MinValue ||
                SelectedWeekEnd == DateTime.MinValue)
            {
                MessageBox.Show(
                    "Please select a date range before viewing the report.",
                    "No Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                tabControl1.SelectedIndex = 0;

                return;
            }

            try
            {
                DataSet ds = DB1.WeeklyProfitData(
       SelectedWeekStart,
       SelectedWeekEnd);
                if (ds == null ||
    ds.Tables.Count == 0)
                {
                    MessageBox.Show(
                        "No data was returned from the database.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    tabControl1.SelectedIndex = 0;

                    return;
                }
                List<WeeklyProfitDetails> list =
    new List<WeeklyProfitDetails>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int jobID = 0;

                    if (dr["jobID"] != DBNull.Value)
                    {
                        int.TryParse(
                            dr["jobID"].ToString(),
                            out jobID);
                    }

                    DateTime jobDate =
                        dr["JobDate"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(dr["JobDate"]);

                    decimal income =
                        dr["Income"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["Income"]);

                    decimal fuelCost =
                        dr["FuelCost"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["FuelCost"]);

                    decimal dumpingCost =
                        dr["DumpingCost"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr["DumpingCost"]);

                    list.Add(
                        new WeeklyProfitDetails()
                        {
                            JobID = jobID,
                            Date = jobDate,
                            Income = income,
                            FuelCost = fuelCost,
                            DumpingCost = dumpingCost
                        });
                }
                if (list.Count == 0)
                {
                    MessageBox.Show(
                        "No profit records were found for the selected date range.",
                        "No Records",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tabControl1.SelectedIndex = 0;

                    return;
                }
                WeeklyProfitCrystalReport1.SetDataSource(list);
                decimal totalIncome =
    list.Sum(x => x.Income);

                decimal totalFuel =
                    list.Sum(x => x.FuelCost);

                decimal totalDumping =
                    list.Sum(x => x.DumpingCost);

                decimal totalExpense =
                    list.Sum(x => x.TotalExpense);

                decimal totalProfit =
                    list.Sum(x => x.Profit);

                WeeklyProfitCrystalReport1.SetParameterValue(
    "dateStart",
    SelectedWeekStart.ToString("dd/MM/yyyy"));

                WeeklyProfitCrystalReport1.SetParameterValue(
                    "dateEnd",
                    SelectedWeekEnd.ToString("dd/MM/yyyy"));

                WeeklyProfitCrystalReport1.SetParameterValue(
                    "TotalIncome",
                    totalIncome);

                WeeklyProfitCrystalReport1.SetParameterValue(
                    "TotalFuel",
                    totalFuel);

                WeeklyProfitCrystalReport1.SetParameterValue(
                    "TotalDumping",
                    totalDumping);

                WeeklyProfitCrystalReport1.SetParameterValue(
                    "TotalExpense",
                    totalExpense);

                WeeklyProfitCrystalReport1.SetParameterValue(
                    "TotalProfit",
                    totalProfit);

               
                crystalReportViewer1.ReportSource =
    WeeklyProfitCrystalReport1;

            }//try
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred while generating the report:\n" +
                    ex.Message,
                    "Report Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                tabControl1.SelectedIndex = 0;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1 && !reportReady)
            {
                e.Cancel = true;

                MessageBox.Show(
                    "Please select a date range and click Generate Report first.",
                    "Report Not Generated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (e.TabPageIndex == 1 && reportReady)
            {
                LoadProfitReport();

                reportReady = false;
            }
        }
    }
}
