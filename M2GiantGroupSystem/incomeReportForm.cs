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
            List<WeeklyIncomeDetails> list =
new List<WeeklyIncomeDetails>();

            DataSet ds = DB1.WeeklyIncomeData(SelectedWeekStart, SelectedWeekEnd);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new WeeklyIncomeDetails()
                {
                    PaymentID =
                        Convert.ToInt32(dr["paymentID"]),

                    PaymentDate =
                        Convert.ToDateTime(dr["paymentDate"]),

                    JobID =
                        Convert.ToInt32(dr["jobID"]),

                    ClientName =
                        dr["ClientName"].ToString(),

                    AmountPaid =
                        Convert.ToDecimal(dr["amountPaid"])
                });
            }



            incomeReport1.SetDataSource(list);
            MessageBox.Show(
    SelectedWeekStart.ToString() +
    "\n" +
    SelectedWeekEnd.ToString()
);

            DataSet summary =
                DB1.WeeklyIncomeSummary(SelectedWeekStart, SelectedWeekEnd);

            decimal totalIncome = 0;

            if (summary.Tables[0].Rows.Count > 0)
            {
                totalIncome =
                    Convert.ToDecimal(
                        summary.Tables[0]
                        .Rows[0]["TotalIncome"]);
            }


            incomeReport1.SetParameterValue(
                "WeekStart",
                SelectedWeekStart.ToString("dd/MM/yyyy"));

            incomeReport1.SetParameterValue(
                "WeekEnd",
                SelectedWeekEnd.ToString("dd/MM/yyyy"));

            incomeReport1.SetParameterValue(
                "TotalIncome",
                totalIncome);

            crystalReportViewer1.ReportSource = incomeReport1;
        }

        private void button1_Click(object sender, EventArgs e)
        { //make sure date pickers arent null
            if (dt1.Value == null || dt2.Value == null)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }
            if (dt1.Value > dt2.Value)
            {
                MessageBox.Show("Start date cannot be after end date.");
                return;
            }
            SelectedWeekStart = dt1.Value.Date;


            SelectedWeekEnd = dt2.Value.Date;


            tabControl1.SelectedIndex = 1;
        }
    }
}
