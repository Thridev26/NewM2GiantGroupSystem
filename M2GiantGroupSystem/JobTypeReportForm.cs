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
    public partial class JobTypeReportForm : Form
    {

        DBConnect DB1 = new DBConnect();

        public static DateTime SelectedStartDate;
        public static DateTime SelectedEndDate;
        bool reportReady = false;
        public JobTypeReportForm()
        {
            InitializeComponent();
        }


        private void JobTypeReportForm_Load(object sender, EventArgs e)
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
                e.Graphics.FillRectangle(b, tabRect);

            using (Pen p = new Pen(Color.DarkGreen, 1))
                e.Graphics.DrawRectangle(p, tabRect);

            TextRenderer.DrawText(e.Graphics, page.Text, tabFont, tabRect, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

        }

        private void button1_Click(object sender, EventArgs e)
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

                if ((dt2.Value.Date - dt1.Value.Date).TotalDays > 365)
                {
                    DialogResult confirm = MessageBox.Show(
                        "The selected date range spans more than a year. Are you sure you want to continue?",
                        "Large Date Range",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm != DialogResult.Yes)
                        return;
                }

                SelectedStartDate = dt1.Value.Date;
                SelectedEndDate = dt2.Value.Date;

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
                LoadProfitabilityReport();

                reportReady = false;
            }
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            reportReady = false;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           // reportReady = false;
        }


        private void LoadProfitabilityReport()
        {
            if (SelectedStartDate == DateTime.MinValue ||
                SelectedEndDate == DateTime.MinValue)
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
                List<jobProfits> list =
    new List<jobProfits>();

                DataSet ds =
                    DB1.JobTypeProfitabilityData(
                        SelectedStartDate,
                        SelectedEndDate);

                if (ds == null || ds.Tables.Count == 0)
                {
                    MessageBox.Show(
                        "No data was returned from the database. The report cannot be generated.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    tabControl1.SelectedIndex = 0;

                    return;
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string jobType = "";

                    if (dr["JobType"] != DBNull.Value)
                        jobType = dr["JobType"].ToString();

                    int numberOfJobs = 0;

                    if (dr["NumberOfJobs"] != DBNull.Value)
                        int.TryParse(
                            dr["NumberOfJobs"].ToString(),
                            out numberOfJobs);

                    decimal jobTypeRevenue = 0;

                    if (dr["TotalRevenue"] != DBNull.Value)
                        jobTypeRevenue =
                            Convert.ToDecimal(dr["TotalRevenue"]);

                    decimal labourCost = 0;

                    if (dr["LabourCost"] != DBNull.Value)
                        labourCost =
                            Convert.ToDecimal(dr["LabourCost"]);

                    decimal fuelCost = 0;

                    if (dr["FuelCost"] != DBNull.Value)
                        fuelCost =
                            Convert.ToDecimal(dr["FuelCost"]);

                    decimal dumpingCost = 0;

                    if (dr["DumpingCost"] != DBNull.Value)
                        dumpingCost =
                            Convert.ToDecimal(dr["DumpingCost"]);

                    decimal assetCost = 0;

                    if (dr["AssetCost"] != DBNull.Value)
                        assetCost =
                            Convert.ToDecimal(dr["AssetCost"]);

                    list.Add(
                        new jobProfits()
                        {
                            JobType = jobType,
                            NumberOfJobs = numberOfJobs,
                            TotalRevenue = jobTypeRevenue,
                            LabourCost = labourCost,
                            FuelCost = fuelCost,
                            DumpingCost = dumpingCost,
                            AssetCost = assetCost
                        });
                }

                if (list.Count == 0)
                {
                    MessageBox.Show(
                        "No completed jobs were found for the selected date range.",
                        "No Records",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tabControl1.SelectedIndex = 0;

                    return;
                }

                JobTypeProfitability1.SetDataSource(list);

                decimal totalRevenue =
   list.Sum(x => x.TotalRevenue);

                decimal totalLabour =
                    list.Sum(x => x.LabourCost);

                decimal totalFuel =
                    list.Sum(x => x.FuelCost);

                decimal totalDumping =
                    list.Sum(x => x.DumpingCost);

                decimal totalAsset =
                    list.Sum(x => x.AssetCost);

                decimal totalCost =
                    list.Sum(x => x.TotalCost);

                decimal totalProfit =
                    list.Sum(x => x.Profit);

                decimal overallMargin = 0;

                if (totalRevenue > 0)
                {
                    overallMargin =
                        (totalProfit / totalRevenue) * 100;
                }
                jobProfits mostProfitable =
    list.OrderByDescending(x => x.Profit)
        .FirstOrDefault();

                string mostProfitableJobType = "";

                decimal highestProfit = 0;

                if (mostProfitable != null)
                {
                    mostProfitableJobType =
                        mostProfitable.JobType;

                    highestProfit =
                        mostProfitable.Profit;
                }

                JobTypeProfitability1.SetParameterValue(
    "StartDate",
    SelectedStartDate.ToString("dd/MM/yyyy"));

                JobTypeProfitability1.SetParameterValue(
                    "EndDate",
                    SelectedEndDate.ToString("dd/MM/yyyy"));

                JobTypeProfitability1.SetParameterValue(
                    "TotalRevenue",
                    totalRevenue);

                JobTypeProfitability1.SetParameterValue(
                    "TotalLabour",
                    totalLabour);

                JobTypeProfitability1.SetParameterValue(
                    "TotalFuel",
                    totalFuel);

                JobTypeProfitability1.SetParameterValue(
                    "TotalDumping",
                    totalDumping);

                JobTypeProfitability1.SetParameterValue(
                    "TotalAsset",
                    totalAsset);

                JobTypeProfitability1.SetParameterValue(
                    "TotalCost",
                    totalCost);

                JobTypeProfitability1.SetParameterValue(
                    "TotalProfit",
                    totalProfit);

                JobTypeProfitability1.SetParameterValue(
                    "OverallMargin",
                    overallMargin);

                JobTypeProfitability1.SetParameterValue(
                    "MostProfitableJobType",
                    mostProfitableJobType);

                JobTypeProfitability1.SetParameterValue(
                    "HighestProfit",
                    highestProfit);

                crystalReportViewer1.ReportSource =
    JobTypeProfitability1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Error loading profitability report:\n" + ex.Message,
                    "Report Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            reportReady = false;
        }
    }
}
