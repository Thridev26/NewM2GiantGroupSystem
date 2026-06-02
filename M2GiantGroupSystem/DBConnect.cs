using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2GiantGroupSystem
{
    internal class DBConnect
    {

        SqlConnection con = new SqlConnection(@"Data Source=146.230.177.46;Initial Catalog=GroupWst1;User ID=GroupWst1;Password=dtf39;TrustServerCertificate=True");


        public DataSet WeeklyIncomeData(DateTime d1, DateTime d2)
        {
            string query =
            @"SELECT
        p.paymentID,
        p.paymentDate,
        j.jobID,
        c.clientName + ' ' + c.clientSurname AS ClientName,
        p.amountPaid
      FROM Payment p
      INNER JOIN Job j ON p.jobID = j.jobID
      INNER JOIN Quote q ON j.quoteID = q.quoteID
      INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
      INNER JOIN Client c ON jr.clientID = c.clientID
      WHERE p.paymentDate BETWEEN @StartDate AND @EndDate
    
      ORDER BY p.paymentDate";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@StartDate", d1);
            cmd.Parameters.AddWithValue("@EndDate", d2);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public DataSet WeeklyIncomeSummary(DateTime d1, DateTime d2)
        {
            string query =
            @"SELECT
        SUM(amountPaid) AS TotalIncome
      FROM Payment
      WHERE paymentDate BETWEEN @StartDate AND @EndDate
      ";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@StartDate", d1);
            cmd.Parameters.AddWithValue("@EndDate", d2);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        public DataSet WeeklyExpenseData(DateTime d1, DateTime d2)
        {
            string query =
            @"SELECT
        jobID,
        endDate,
        totalFuelCost,
        totalLabourCost,
        dumpingCost
      FROM Job
      WHERE startDate
            BETWEEN @StartDate AND @EndDate
     
      ORDER BY endDate";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@StartDate",
                d1);

            cmd.Parameters.AddWithValue(
                "@EndDate",
                d2);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataSet ds =
                new DataSet();

            da.Fill(ds);

            return ds;
        }
        public DataSet WeeklyExpenseSummary(DateTime d1, DateTime d2)
        {
            string query =
            @"SELECT
        ISNULL(SUM(totalFuelCost), 0) AS FuelTotal,
        ISNULL(SUM(totalLabourCost), 0) AS LabourTotal,
        ISNULL(SUM(dumpingCost), 0) AS DumpingTotal,
        ISNULL(SUM(
            ISNULL(totalFuelCost,0) +
            ISNULL(totalLabourCost,0) +
            ISNULL(dumpingCost,0)
        ), 0) AS ExpenseTotal
      FROM Job
      WHERE endDate BETWEEN @StartDate AND @EndDate
      AND jobStatus = 'Completed'";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@StartDate",
                d1);

            cmd.Parameters.AddWithValue(
                "@EndDate",
                d2);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataSet ds =
                new DataSet();

            da.Fill(ds);

            return ds;
        }
        public DataSet OutstandingPaymentsData()
        {
            string query =
            @"SELECT
        p.paymentID,
        p.paymentDate,
        p.jobID,
        c.clientName + ' ' + c.clientSurname AS ClientName,
        jr.siteAddress,
        p.amountPaid,
        p.paymentStatus
      FROM Payment p
      INNER JOIN Job j ON p.jobID = j.jobID
      INNER JOIN Quote q ON j.quoteID = q.quoteID
      INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
      INNER JOIN Client c ON jr.clientID = c.clientID
       WHERE p.paymentStatus IN ('Pending', 'Partially Paid') 
      ORDER BY p.paymentDate";

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        public DataSet OutstandingPaymentsSummary()
        {
            string query =
            @"SELECT
        SUM(amountPaid) AS TotalOutstanding
      FROM Payment
      WHERE paymentStatus IN ('Pending', 'Partially Paid')";

            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

    }
}
