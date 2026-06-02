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
        public static DateTime SelectedWeekStart;
        public static DateTime SelectedWeekEnd;

        private void WeeklyExpensesReportForm_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
           
            List<WeeklyExpenseDetails> list =
            new List<WeeklyExpenseDetails>();

            DataSet ds =
                DB1.WeeklyExpenseData(SelectedWeekStart,SelectedWeekEnd);
            MessageBox.Show("Rows found: " + ds.Tables[0].Rows.Count);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(
                    new WeeklyExpenseDetails()
                    {
                        JobID =
                            Convert.ToInt32(
                                dr["jobID"]),

                        EndDate =
                            
                        dr["endDate"] == DBNull.Value
                            ? DateTime.MinValue
                            : Convert.ToDateTime(dr["endDate"]),

                        FuelCost =
                          dr["totalFuelCost"] == DBNull.Value
                           ? 0
                           : Convert.ToDecimal(dr["totalFuelCost"]),

                        LabourCost =
                           dr["totalLabourCost"] == DBNull.Value
                               ? 0
                               : Convert.ToDecimal(dr["totalLabourCost"]),

                        DumpingCost =
                           dr["dumpingCost"] == DBNull.Value
                               ? 0
                               : Convert.ToDecimal(dr["dumpingCost"])
                    });
            }

            expensesReport1.SetDataSource(list);

            DataSet summary =
                DB1.WeeklyExpenseSummary(SelectedWeekStart, SelectedWeekEnd);

            decimal totalExpense = 0;
            decimal fuelTotal = 0;
            decimal labourTotal = 0;
            decimal dumpingTotal = 0;

            if (summary.Tables[0].Rows.Count > 0)
            {
                fuelTotal =
                    Convert.ToDecimal(
                        summary.Tables[0]
                        .Rows[0]["FuelTotal"]);

                labourTotal =
                    Convert.ToDecimal(
                        summary.Tables[0]
                        .Rows[0]["LabourTotal"]);

                dumpingTotal =
                    Convert.ToDecimal(
                        summary.Tables[0]
                        .Rows[0]["DumpingTotal"]);

                totalExpense =
                    Convert.ToDecimal(
                        summary.Tables[0]
                        .Rows[0]["ExpenseTotal"]);
            }

            expensesReport1
                .SetParameterValue(
                    "WeekStart",
                    SelectedWeekStart
                    .ToString("dd/MM/yyyy"));

            expensesReport1
                .SetParameterValue(
                    "WeekEnd",
                    SelectedWeekEnd
                    .ToString("dd/MM/yyyy"));
            MessageBox.Show(
    $"Fuel={fuelTotal}\n" +
    $"Labour={labourTotal}\n" +
    $"Dumping={dumpingTotal}\n" +
    $"Expense={totalExpense}");
            expensesReport1
                .SetParameterValue(
                    "FuelTotal",
                    fuelTotal);

            expensesReport1
                .SetParameterValue(
                    "LabourTotal",
                    labourTotal);

            expensesReport1
                .SetParameterValue(
                    "DumpingTotal",
                    dumpingTotal);

            expensesReport1
                .SetParameterValue(
                    "ExpenseTotal",
                    totalExpense);

            crystalReportViewer1.ReportSource =
                expensesReport1;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //make sure date pickers arent null
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
