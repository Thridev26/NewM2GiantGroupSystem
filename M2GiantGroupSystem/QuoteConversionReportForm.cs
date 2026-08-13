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
    public partial class QuoteConversionReportForm : Form
    {

        DBConnect DB1 = new DBConnect();

        public static DateTime SelectedStartDate;
        public static DateTime SelectedEndDate;

        bool reportReady = false;

        public QuoteConversionReportForm()
        {
            InitializeComponent();
        }


        private void QuoteConversionReportForm_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                // Do not load the report when the form opens
                crystalReportViewer1.ReportSource = null;

                // Prevent Crystal Reports from asking
                // the user for parameter values
             //   crystalReportViewer1.EnableParameterPrompt = false;

                tabControl1.DrawMode =
                    TabDrawMode.OwnerDrawFixed;

                tabControl1.DrawItem +=
                    tabControl1_DrawItem;

                tabControl1.ItemSize =
                    new Size(300, 30);

                tabControl1.SizeMode =
                    TabSizeMode.Fixed;
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



        private void tabControl1_Selecting(
            object sender,
            TabControlCancelEventArgs e)
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



        private void LoadQuoteConversionReport()
        {
            if (SelectedStartDate ==
                    DateTime.MinValue ||
                SelectedEndDate ==
                    DateTime.MinValue)
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
                List<QuoteConversionDetails> list =
                    new List<QuoteConversionDetails>();


                // GET DATA FROM DATABASE

                DataSet ds =
                    DB1.QuoteConversionData(
                        SelectedStartDate,
                        SelectedEndDate);


                if (ds == null ||
                    ds.Tables.Count == 0)
                {
                    MessageBox.Show(
                        "No data was returned from the database. The report cannot be generated.",
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
                    string requestSource = "";

                    if (dr["RequestSource"] !=
                        DBNull.Value)
                    {
                        requestSource =
                            dr["RequestSource"].ToString();
                    }


                    int requestCount = 0;

                    if (dr["TotalRequests"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["TotalRequests"].ToString(),
                            out requestCount);
                    }


                    int quotesGenerated = 0;

                    if (dr["QuotesGenerated"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["QuotesGenerated"].ToString(),
                            out quotesGenerated);
                    }


                    int quotesSent = 0;

                    if (dr["QuotesSent"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["QuotesSent"].ToString(),
                            out quotesSent);
                    }


                    int quotesAccepted = 0;

                    if (dr["QuotesAccepted"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["QuotesAccepted"].ToString(),
                            out quotesAccepted);
                    }


                    int quotesRejected = 0;

                    if (dr["QuotesRejected"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["QuotesRejected"].ToString(),
                            out quotesRejected);
                    }


                    int requestsCancelled = 0;

                    if (dr["RequestsCancelled"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["RequestsCancelled"].ToString(),
                            out requestsCancelled);
                    }


                    int jobsCreated = 0;

                    if (dr["JobsCreated"] !=
                        DBNull.Value)
                    {
                        int.TryParse(
                            dr["JobsCreated"].ToString(),
                            out jobsCreated);
                    }


                    decimal averageQuotedValue = 0;

                    if (dr["AverageQuotedValue"] !=
                        DBNull.Value)
                    {
                        averageQuotedValue =
                            Convert.ToDecimal(
                                dr["AverageQuotedValue"]);
                    }


                    decimal acceptedQuoteRevenue = 0;

                    if (dr["AcceptedQuoteRevenue"] !=
                        DBNull.Value)
                    {
                        acceptedQuoteRevenue =
                            Convert.ToDecimal(
                                dr["AcceptedQuoteRevenue"]);
                    }


                    list.Add(
                        new QuoteConversionDetails()
                        {
                            RequestSource =
                                requestSource,

                            TotalRequests =
                                requestCount,

                            QuotesGenerated =
                                quotesGenerated,

                            QuotesSent =
                                quotesSent,

                            QuotesAccepted =
                                quotesAccepted,

                            QuotesRejected =
                                quotesRejected,

                            RequestsCancelled =
                                requestsCancelled,

                            JobsCreated =
                                jobsCreated,

                            AverageQuotedValue =
                                averageQuotedValue,

                            AcceptedQuoteRevenue =
                                acceptedQuoteRevenue
                        });
                }


                // CHECK IF ANY RECORDS WERE FOUND

                if (list.Count == 0)
                {
                    MessageBox.Show(
                        "No job requests were found for the selected date range.",
                        "No Records",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tabControl1.SelectedIndex = 0;

                    return;
                }


                // OVERALL TOTALS

                int totalRequests =
                    list.Sum(x =>
                        x.TotalRequests);


                int totalQuotes =
                    list.Sum(x =>
                        x.QuotesGenerated);


                int totalQuotesSent =
                    list.Sum(x =>
                        x.QuotesSent);


                int totalAccepted =
                    list.Sum(x =>
                        x.QuotesAccepted);


                int totalRejected =
                    list.Sum(x =>
                        x.QuotesRejected);


                int totalCancelled =
                    list.Sum(x =>
                        x.RequestsCancelled);


                int totalJobs =
                    list.Sum(x =>
                        x.JobsCreated);


                decimal totalAcceptedRevenue =
                    list.Sum(x =>
                        x.AcceptedQuoteRevenue);



                // OVERALL CONVERSION RATE

                decimal overallConversionRate =
                    0;

                if (totalRequests > 0)
                {
                    overallConversionRate =
                        ((decimal)totalAccepted /
                        totalRequests) * 100;
                }



                // OVERALL AVERAGE QUOTE

                decimal overallAverageQuote =
                    0;


                int totalQuotesForAverage =
                    list.Sum(x =>
                        x.QuotesGenerated);


                if (totalQuotesForAverage > 0)
                {
                    decimal totalQuoteValue =
                        list.Sum(x =>
                            x.AverageQuotedValue *
                            x.QuotesGenerated);

                    overallAverageQuote =
                        totalQuoteValue /
                        totalQuotesForAverage;
                }



                // MOST SUCCESSFUL SOURCE

                QuoteConversionDetails bestSource =
                    list.OrderByDescending(
                        x => x.ConversionRate)
                        .FirstOrDefault();


                string bestRequestSource = "";

                decimal highestConversionRate = 0;


                if (bestSource != null)
                {
                    bestRequestSource =
                        bestSource.RequestSource;

                    highestConversionRate =
                        bestSource.ConversionRate;
                }



                // ------------------------------------------------
                // SET DATA SOURCE FIRST
                // ------------------------------------------------

                QuoteConversionCrystalReport2.SetDataSource(
                    list);



                // ------------------------------------------------
                // SET PARAMETERS
                // ------------------------------------------------

                QuoteConversionCrystalReport2.SetParameterValue(
                    "StartDate",
                    SelectedStartDate.ToString(
                        "dd/MM/yyyy"));


                QuoteConversionCrystalReport2.SetParameterValue(
                    "EndDate",
                    SelectedEndDate.ToString(
                        "dd/MM/yyyy"));


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalRequests",
                    (double)totalRequests);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalQuotes",
                    (double)totalQuotes);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalQuotesSent",
                    (double)totalQuotesSent);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalAccepted",
                    (double)totalAccepted);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalRejected",
                    (double)totalRejected);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalCancelled",
                    (double)totalCancelled);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "TotalJobs",
                    (double)totalJobs);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "ConversionRate",
                    (double)overallConversionRate);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "AverageQuoteValue",
                    (double)overallAverageQuote);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "AcceptedQuoteRevenue",
                    (double)totalAcceptedRevenue);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "BestRequestSource",
                    bestRequestSource);


                QuoteConversionCrystalReport2.SetParameterValue(
                    "HighestConversionRate",
                    (double)highestConversionRate);



                // ------------------------------------------------
                // LOAD REPORT INTO VIEWER LAST
                // ------------------------------------------------

                crystalReportViewer1.ReportSource =
                    QuoteConversionCrystalReport2;


                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading quote conversion report:\n"
                    + ex.Message,
                    "Report Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void button1_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                // CHECK DATE RANGE

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



                // CHECK IF RANGE IS MORE THAN ONE YEAR

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



                // STORE SELECTED DATES

                SelectedStartDate =
                    dt1.Value.Date;

                SelectedEndDate =
                    dt2.Value.Date;



                // LOAD THE REPORT

                reportReady = true;

                LoadQuoteConversionReport();



                // MOVE TO REPORT TAB

                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error processing date selection:\n"
                    + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void dt1_ValueChanged(
            object sender,
            EventArgs e)
        {
            reportReady = false;
        }



        private void dt2_ValueChanged(
            object sender,
            EventArgs e)
        {
            reportReady = false;
        }



        private void tabPage1_Click(
            object sender,
            EventArgs e)
        {

        }
    }
}