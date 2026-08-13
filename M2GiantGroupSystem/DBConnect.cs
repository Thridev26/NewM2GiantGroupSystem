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
        //-------------------------------------JobTypeProfitabilityData-------------------------------------------------------------------------
        public DataSet JobTypeProfitabilityData(DateTime d1, DateTime d2)
        {
            string query =
     @"
    WITH LabourCosts AS
    (
        SELECT
            jsa.jobID,

            SUM(
                jsa.hoursWorked *
                (s.dailyRate / 8.0)
            ) AS LabourCost

        FROM JobStaffAssignment jsa

        INNER JOIN Staff s
            ON jsa.staffID = s.staffID

        GROUP BY jsa.jobID
    ),

    AssetCosts AS
    (
        SELECT
            ja.jobID,

            SUM(
                CASE
                    WHEN ja.hiredAssetID IS NOT NULL
                    THEN ha.hireCost
                    ELSE 0
                END
            ) AS AssetCost

        FROM JobAssetAssignment ja

        LEFT JOIN HiredAsset ha
            ON ja.hiredAssetID = ha.hiredAssetID

        GROUP BY ja.jobID
    )

    SELECT
        jt.jobTypeName AS JobType,

        COUNT(DISTINCT j.jobID)
            AS NumberOfJobs,

        SUM(q.amount)
            AS TotalRevenue,

        SUM(
            ISNULL(lc.LabourCost, 0)
        ) AS LabourCost,

        SUM(
            j.totalFuelCost
        ) AS FuelCost,

        SUM(
            j.dumpingCost
        ) AS DumpingCost,

        SUM(
            ISNULL(ac.AssetCost, 0)
        ) AS AssetCost

    FROM Job j

    INNER JOIN Quote q
        ON j.quoteID = q.QuoteID

    INNER JOIN JobRequest jr
        ON q.jobRequestID = jr.jobRequestID

    INNER JOIN RequestItem ri
        ON jr.jobRequestID = ri.jobRequestID

    INNER JOIN JobType jt
        ON ri.jobTypeID = jt.jobTypeID

    LEFT JOIN LabourCosts lc
        ON j.jobID = lc.jobID

    LEFT JOIN AssetCosts ac
        ON j.jobID = ac.jobID

    WHERE j.startDate
        BETWEEN @StartDate AND @EndDate

    AND j.jobStatus = 'Completed'

    GROUP BY jt.jobTypeName

    ORDER BY jt.jobTypeName;
    ";

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

        public DataSet QuoteConversionData(
            DateTime startDate,
            DateTime endDate)
        {
            string query =
            @"SELECT
        jr.requestSource AS RequestSource,

        COUNT(DISTINCT jr.jobRequestID)
            AS TotalRequests,

        COUNT(DISTINCT q.QuoteID)
            AS QuotesGenerated,

        COUNT(DISTINCT CASE
            WHEN q.quoteStatus IN ('Sent', 'Accepted', 'Rejected')
            THEN q.QuoteID
        END) AS QuotesSent,

        COUNT(DISTINCT CASE
            WHEN q.quoteStatus = 'Accepted'
            THEN q.QuoteID
        END) AS QuotesAccepted,

        COUNT(DISTINCT CASE
            WHEN q.quoteStatus = 'Rejected'
            THEN q.QuoteID
        END) AS QuotesRejected,

        COUNT(DISTINCT CASE
            WHEN jr.status = 'Cancelled'
            THEN jr.jobRequestID
        END) AS RequestsCancelled,

        COUNT(DISTINCT j.jobID)
            AS JobsCreated,

        ISNULL(AVG(q.amount), 0)
            AS AverageQuotedValue,

        ISNULL(SUM(CASE
            WHEN q.quoteStatus = 'Accepted'
            THEN q.amount
            ELSE 0
        END), 0) AS AcceptedQuoteRevenue

      FROM JobRequest jr

      LEFT JOIN Quote q
          ON jr.jobRequestID = q.jobRequestID

      LEFT JOIN Job j
          ON q.QuoteID = j.quoteID

      WHERE jr.dateRecieved
            BETWEEN @StartDate AND @EndDate

      GROUP BY jr.requestSource

      ORDER BY jr.requestSource";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@StartDate",
                startDate);

            cmd.Parameters.AddWithValue(
                "@EndDate",
                endDate);

            SqlDataAdapter da =
                new SqlDataAdapter(cmd);

            DataSet ds =
                new DataSet();

            da.Fill(ds);

            return ds;
        }
    }
}
