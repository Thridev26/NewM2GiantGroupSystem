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
    public partial class ClientValueReportForm : Form
    {
        DBConnect DB1 = new DBConnect();

        public static DateTime SelectedStartDate;
        public static DateTime SelectedEndDate;

        public static string SelectedClientType;

        bool reportReady = false;
        public ClientValueReportForm()
        {
            InitializeComponent();
        }

        private void ClientValueReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                crystalReportViewer1.ReportSource = null;

                tabControl1.DrawMode =
                    TabDrawMode.OwnerDrawFixed;

                tabControl1.DrawItem +=
                    tabControl1_DrawItem;

                tabControl1.ItemSize =
                    new Size(300, 30);

                tabControl1.SizeMode =
                    TabSizeMode.Fixed;

                // Load client types
                cmbClientType.Items.Clear();

                cmbClientType.Items.Add("All");
                cmbClientType.Items.Add("Residential");
                cmbClientType.Items.Add("Commercial");
                cmbClientType.Items.Add("Government");

                cmbClientType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading the report form:\n"
                    + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void tabControl1_DrawItem(
           object sender,
           DrawItemEventArgs e)
        {
            TabPage page =
                tabControl1.TabPages[e.Index];

            Rectangle tabRect =
                tabControl1.GetTabRect(e.Index);

            Font tabFont =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            Color backColor =
                Color.Honeydew;

            Color textColor =
                Color.Black;

            if (e.Index ==
                tabControl1.SelectedIndex)
            {
                backColor =
                    Color.DarkGreen;

                textColor =
                    Color.White;
            }

            using (Brush b =
                new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(
                    b,
                    tabRect);
            }

            using (Pen p =
                new Pen(
                    Color.DarkGreen,
                    1))
            {
                e.Graphics.DrawRectangle(
                    p,
                    tabRect);
            }

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if (e.TabPageIndex == 1 &&
    !reportReady)
            {
                e.Cancel = true;

                MessageBox.Show(
                    "Please select a date range and click Generate Report first.",
                    "Report Not Generated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
        }

        private void LoadClientValueReport()
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
                List<ClientValueDetails> list =
                    new List<ClientValueDetails>();


                // GET DATA FROM DATABASE

                DataSet ds =
                    DB1.ClientValueData(
                        SelectedStartDate,
                        SelectedEndDate,
                        SelectedClientType);


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


                // BUILD REPORT LIST

                foreach (DataRow dr
                    in ds.Tables[0].Rows)
                {
                    ClientValueDetails item =
                        new ClientValueDetails();


                    item.ClientID =
                        Convert.ToInt32(
                            dr["ClientID"]);


                    item.Client =
                        dr["Client"].ToString();


                    item.ClientType =
                        dr["ClientType"].ToString();


                    item.NumberOfRequests =
                        dr["NumberOfRequests"] != DBNull.Value
                        ? Convert.ToInt32(
                            dr["NumberOfRequests"])
                        : 0;


                    item.CompletedJobs =
                        dr["CompletedJobs"] != DBNull.Value
                        ? Convert.ToInt32(
                            dr["CompletedJobs"])
                        : 0;


                    item.TotalAmountQuoted =
                        dr["TotalAmountQuoted"] != DBNull.Value
                        ? Convert.ToDecimal(
                            dr["TotalAmountQuoted"])
                        : 0;


                    item.TotalAmountPaid =
                        dr["TotalAmountPaid"] != DBNull.Value
                        ? Convert.ToDecimal(
                            dr["TotalAmountPaid"])
                        : 0;


                    item.AverageJobValue =
                        dr["AverageJobValue"] != DBNull.Value
                        ? Convert.ToDecimal(
                            dr["AverageJobValue"])
                        : 0;


                    item.CancelledRejectedRequests =
                        dr["CancelledRejectedRequests"] != DBNull.Value
                        ? Convert.ToInt32(
                            dr["CancelledRejectedRequests"])
                        : 0;


                    if (dr["LastJobDate"] != DBNull.Value)
                    {
                        item.LastJobDate =
                            Convert.ToDateTime(dr["LastJobDate"]);
                    }
                    else
                    {
                        item.LastJobDate = DateTime.MinValue;
                    }


                    list.Add(item);
                }


                // CHECK IF RECORDS WERE FOUND

                if (list.Count == 0)
                {
                    MessageBox.Show(
                        "No clients were found for the selected date range.",
                        "No Records",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tabControl1.SelectedIndex = 0;

                    return;
                }
                int totalClients =
    list.Count;

                int totalRequests =
                    list.Sum(x =>
                        x.NumberOfRequests);

                int totalCompletedJobs =
                    list.Sum(x =>
                        x.CompletedJobs);

                decimal totalQuoted =
                    list.Sum(x =>
                        x.TotalAmountQuoted);

                decimal totalPaid =
                    list.Sum(x =>
                        x.TotalAmountPaid);

                int totalCancelledRejected =
                    list.Sum(x =>
                        x.CancelledRejectedRequests);

                decimal totalCompletedJobValue =
    list.Sum(x =>
        x.AverageJobValue *
        x.CompletedJobs);

                decimal overallAverageJobValue = 0;

                if (totalCompletedJobs > 0)
                {
                    overallAverageJobValue =
                        totalCompletedJobValue /
                        totalCompletedJobs;
                }

                ClientValueDetails mostValuableClient =
    list.OrderByDescending(
        x => x.TotalAmountPaid)
        .FirstOrDefault();

                string mostValuableClientStr = "";

                decimal highestClientValue = 0;

                if (mostValuableClient != null)
                {
                    mostValuableClientStr = mostValuableClient.Client;
                    highestClientValue = mostValuableClient.TotalAmountPaid;
                }
                ClientValueDetails topClient =
    list.OrderByDescending(
        x => x.TotalAmountPaid)
        .FirstOrDefault();

                string topClientName = "";

                decimal topClientValue = 0;

                if (topClient != null)
                {
                    topClientName =
                        topClient.Client;

                    topClientValue =
                        topClient.TotalAmountPaid;
                }

                var typeSummary =
    list.GroupBy(x =>
        x.ClientType)
        .Select(g => new
        {
            ClientType = g.Key,

            Clients = g.Count(),

            Requests =
                g.Sum(x =>
                    x.NumberOfRequests),

            CompletedJobs =
                g.Sum(x =>
                    x.CompletedJobs),

            Revenue =
                g.Sum(x =>
                    x.TotalAmountPaid),

            AverageJobValue =
                g.Sum(x =>
                    x.AverageJobValue *
                    x.CompletedJobs)
                /
                (g.Sum(x =>
                    x.CompletedJobs) == 0
                    ? 1
                    : g.Sum(x =>
                        x.CompletedJobs))
        })
        .ToList();



                var bestClientType =
    typeSummary
        .OrderByDescending(x =>
            x.Revenue)
        .FirstOrDefault();

                string highestRevenueClientType = "";

                decimal highestTypeRevenue = 0;

                if (bestClientType != null)
                {
                    highestRevenueClientType =
                        bestClientType.ClientType;

                    highestTypeRevenue =
                        bestClientType.Revenue;
                }
                ClientValueCrystalReport.SetDataSource(list);

                ClientValueCrystalReport.SetParameterValue(
    "StartDate",
    SelectedStartDate.ToString(
        "dd/MM/yyyy"));

                ClientValueCrystalReport.SetParameterValue(
                    "EndDate",
                    SelectedEndDate.ToString(
                        "dd/MM/yyyy"));

                ClientValueCrystalReport.SetParameterValue(
                    "SelectedClientType",
                    SelectedClientType);

                ClientValueCrystalReport.SetParameterValue(
    "TotalClients",
    (double)totalClients);

                ClientValueCrystalReport.SetParameterValue(
                    "TotalRequests",
                    (double)totalRequests);

                ClientValueCrystalReport.SetParameterValue(
                    "TotalCompletedJobs",
                    (double)totalCompletedJobs);

                ClientValueCrystalReport.SetParameterValue(
                    "TotalQuoted",
                    (double)totalQuoted);

                ClientValueCrystalReport.SetParameterValue(
                    "TotalPaid",
                    (double)totalPaid);

                ClientValueCrystalReport.SetParameterValue(
                    "OverallAverageJobValue",
                    (double)overallAverageJobValue);

                ClientValueCrystalReport.SetParameterValue(
                    "TotalCancelledRejected",
                    (double)totalCancelledRejected);

                ClientValueCrystalReport.SetParameterValue(
                    "TopClient",
                    topClientName);

                ClientValueCrystalReport.SetParameterValue(
                    "TopClientValue",
                    (double)topClientValue);
                crystalReportViewer1.ReportSource =
    ClientValueCrystalReport;

                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading client value report:\n"
                    + ex.Message,
                    "Report Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dt1.Value.Date >
                    dt2.Value.Date)
                {
                    MessageBox.Show(
                        "Start date cannot be after the end date.",
                        "Invalid Date Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                if ((dt2.Value.Date -
                    dt1.Value.Date).TotalDays > 365)
                {
                    DialogResult confirm =
                        MessageBox.Show(
                            "The selected date range spans more than a year. Are you sure you want to continue?",
                            "Large Date Range",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                    if (confirm !=
                        DialogResult.Yes)
                    {
                        return;
                    }
                }


                SelectedStartDate =
                    dt1.Value.Date;

                SelectedEndDate =
                    dt2.Value.Date;

                SelectedClientType =
                    cmbClientType.SelectedItem
                    .ToString();


                reportReady = true;


                LoadClientValueReport();


                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error generating the report:\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
                if (dt1.Value.Date >
                    dt2.Value.Date)
                {
                    MessageBox.Show(
                        "Start date cannot be after the end date.",
                        "Invalid Date Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                if ((dt2.Value.Date -
                    dt1.Value.Date).TotalDays > 365)
                {
                    DialogResult confirm =
                        MessageBox.Show(
                            "The selected date range spans more than a year. Are you sure you want to continue?",
                            "Large Date Range",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                    if (confirm !=
                        DialogResult.Yes)
                    {
                        return;
                    }
                }


                SelectedStartDate =
                    dt1.Value.Date;

                SelectedEndDate =
                    dt2.Value.Date;

                SelectedClientType =
                    cmbClientType.SelectedItem
                    .ToString();


                reportReady = true;


                LoadClientValueReport();


                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error generating the report:\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
