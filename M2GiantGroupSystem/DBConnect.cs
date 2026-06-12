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
            startDate,
            ISNULL(totalFuelCost,   0) AS totalFuelCost,
            ISNULL(totalLabourCost, 0) AS totalLabourCost,
            ISNULL(dumpingCost,     0) AS dumpingCost
          FROM Job
          WHERE startDate >= @StartDate
          AND startDate < DATEADD(DAY, 1, @EndDate)
          ORDER BY startDate";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StartDate", d1.Date);
            cmd.Parameters.AddWithValue("@EndDate", d2.Date);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        public DataSet WeeklyExpenseSummary(DateTime d1, DateTime d2)
        {
            string query =
        @"SELECT
            ISNULL(SUM(totalFuelCost),   0) AS FuelTotal,
            ISNULL(SUM(totalLabourCost), 0) AS LabourTotal,
            ISNULL(SUM(dumpingCost),     0) AS DumpingTotal,
            ISNULL(SUM(
                ISNULL(totalFuelCost,   0) +
                ISNULL(totalLabourCost, 0) +
                ISNULL(dumpingCost,     0)
            ), 0) AS ExpenseTotal
          FROM Job
          WHERE startDate >= @StartDate
          AND startDate < DATEADD(DAY, 1, @EndDate)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@StartDate", d1.Date);
            cmd.Parameters.AddWithValue("@EndDate", d2.Date);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
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

        public DataSet InvoiceData()
        {
            string query = @"SELECT 
    j.jobID,
    c.clientName + ' ' + c.clientSurname AS ClientName,
    jr.siteAddress,
    j.startDate,
    j.endDate,
    jt.jobTypeName,
    jt.jobRate,
    CAST(id.detailValue AS DECIMAL(10,2)) AS DetailValue,
    CAST(id.detailValue AS DECIMAL(10,2)) * jt.jobRate AS LineTotal,
    q.amount AS QuoteAmount,
    ISNULL(pay.TotalReceived, 0) AS TotalReceived,

    -- Sum of all line items for this job
    SUM(CAST(id.detailValue AS DECIMAL(10,2)) * jt.jobRate) 
        OVER (PARTITION BY j.jobID) AS LineItemsSubtotal,

    -- Work backwards: SubtotalBeforeVAT = QuoteAmount / 1.15
    ROUND(q.amount / 1.15, 2) AS SubtotalBeforeVAT,

    -- VAT = QuoteAmount - SubtotalBeforeVAT
    ROUND(q.amount - (q.amount / 1.15), 2) AS VATAmount,

    -- TravelFee = SubtotalBeforeVAT - LineItemsSubtotal
    ROUND(
        (q.amount / 1.15) - 
        SUM(CAST(id.detailValue AS DECIMAL(10,2)) * jt.jobRate) 
            OVER (PARTITION BY j.jobID)
    , 2) AS TravelFee

FROM Job j
INNER JOIN Quote q      ON j.quoteID = q.QuoteID
INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
INNER JOIN Client c     ON jr.clientID = c.clientID
INNER JOIN RequestItem ri ON jr.jobRequestID = ri.jobRequestID
INNER JOIN JobType jt   ON ri.jobTypeID = jt.jobTypeID
INNER JOIN ItemDetail id ON ri.requestItemID = id.requestItemID
LEFT JOIN (
    SELECT jobID, SUM(amountPaid) AS TotalReceived
    FROM Payment
    GROUP BY jobID
) pay ON j.jobID = pay.jobID
WHERE j.jobID = @JobID
AND (
    (jt.jobTypeName = 'Tree Felling'         AND id.jobDetailID = 1)  OR
    (jt.jobTypeName = 'Grass Cutting'        AND id.jobDetailID = 6)  OR
    (jt.jobTypeName = 'Tree Planting'        AND id.jobDetailID = 10) OR
    (jt.jobTypeName = 'Vegetation Clearance' AND id.jobDetailID = 14) OR
    (jt.jobTypeName = 'Hedge Trimming'       AND id.jobDetailID = 18)
)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@JobID", InvoiceReportForm.SelectedJobID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
