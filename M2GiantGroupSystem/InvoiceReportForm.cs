using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace M2GiantGroupSystem
{
    public partial class InvoiceReportForm : Form
    {
        int index;
        public InvoiceReportForm(int selectedIndex)
        {
            InitializeComponent();
            index = selectedIndex;
        }
        private bool _allowReportTab = false;
        public static int SelectedJobID;
        DBConnect DB1 = new DBConnect();
        private bool _reportLoading = false;  // ADD THIS FLAG

        public void runQuery(TextBox t, DataGridView dgv)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT 
                        j.jobID,
                        c.clientName + ' ' + c.clientSurname AS ClientName,
                        jr.siteAddress,
                        j.startDate,
                        j.endDate,
                        j.jobStatus,
                        SUM(p.amountPaid) AS TotalPaid,
                        q.amount AS QuoteAmount
                    FROM Job j
                    INNER JOIN Quote q ON j.quoteID = q.QuoteID
                    INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                    INNER JOIN Client c ON jr.clientID = c.clientID
                    LEFT JOIN Payment p ON j.jobID = p.jobID
                    WHERE j.jobStatus = 'Completed'
                    AND (c.clientName LIKE '%" + t.Text + @"%'
                    OR c.clientSurname LIKE '%" + t.Text + @"%'
                    OR jr.siteAddress LIKE '%" + t.Text + @"%')
                    GROUP BY j.jobID, c.clientName, c.clientSurname,
                             jr.siteAddress, j.startDate, j.endDate,
                             j.jobStatus, q.amount
                    ORDER BY j.startDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
                dgv.Columns[0].Width = 80;   // jonID
                dgv.Columns[1].Width = 250;  // Name
                dgv.Columns[2].Width = 400;  //address
                dgv.Columns[3].Width = 100;  //sdate
                dgv.Columns[4].Width = 100;  //edate
                dgv.Columns[5].Width = 100;  //status
            }
        }

        private void InvoiceReportForm_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = index;
            //set tab page 2 to disabled until a job is selected
                tabControl1.TabPages[1].Enabled = false;

            runQuery(txtSearchJobs, jobsReportsDgv);
            jobsReportsDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            jobsReportsDgv.DefaultCellStyle.SelectionBackColor = Color.Green;

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void txtSearchJobs_TextChanged(object sender, EventArgs e)
        {
            runQuery(txtSearchJobs, jobsReportsDgv);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void jobsReportsDgv_Click(object sender, EventArgs e)
        {
            label1.Text= "Job ID selected: " + jobsReportsDgv.CurrentRow.Cells["jobID"].Value.ToString();
            SelectedJobID= Convert.ToInt32(jobsReportsDgv.CurrentRow.Cells["jobID"].Value);
            button1.Enabled = true;
            //enable tab page 2
             tabControl1.TabPages[1].Enabled = true;
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

        private void jobsReportsDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            try
            {
                List<InvoiceDetails3> list = new List<InvoiceDetails3>();
                DataSet ds = DB1.InvoiceData();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data found for the selected job.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    list.Add(new InvoiceDetails3()
                    {

                        JobTypeName = dr["jobTypeName"].ToString(),
                        JobRate = Convert.ToDecimal(dr["jobRate"]),
                        DetailValue = Convert.ToDecimal(dr["DetailValue"]),
                        LineTotal = Convert.ToDecimal(dr["LineTotal"]),

                    });
                }

                // set data source BEFORE setting parameter values
                InvoiceReport1.SetDataSource(list);

                DataRow first = ds.Tables[0].Rows[0];

                InvoiceReport1.SetParameterValue("ClientName", first["ClientName"].ToString());
                InvoiceReport1.SetParameterValue("SiteAddress", first["siteAddress"].ToString());
                InvoiceReport1.SetParameterValue("JobID", SelectedJobID);
                InvoiceReport1.SetParameterValue("StartDate", Convert.ToDateTime(first["startDate"]).ToString("dd/MM/yyyy"));
                InvoiceReport1.SetParameterValue("EndDate", Convert.ToDateTime(first["endDate"]).ToString("dd/MM/yyyy"));
                InvoiceReport1.SetParameterValue("QuoteAmount", Convert.ToDecimal(first["QuoteAmount"]));
                InvoiceReport1.SetParameterValue("TotalReceived", Convert.ToDecimal(first["TotalReceived"]));
                InvoiceReport1.SetParameterValue("LineItemsSubtotal", Convert.ToDecimal(first["LineItemsSubtotal"]));
                InvoiceReport1.SetParameterValue("TravelFee", Convert.ToDecimal(first["TravelFee"]));
                InvoiceReport1.SetParameterValue("VATAmount", Convert.ToDecimal(first["VATAmount"]));

                decimal balance = Convert.ToDecimal(first["QuoteAmount"])
                                - Convert.ToDecimal(first["TotalReceived"]);
                InvoiceReport1.SetParameterValue("BalanceOutstanding", balance);

               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            crystalReportViewer1.ReportSource = InvoiceReport1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedJobID == 0)
            {
                MessageBox.Show("Please select a job first.",
                    "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InvoiceReport1.Refresh();
            crystalReportViewer1_Load(sender, e);

            _allowReportTab = true;
            tabControl1.SelectedIndex = 1;
            _allowReportTab = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // tabControl1.TabPages[1].Enabled = false;
            //button1.Enabled = false;
           
            
        }
        private void LoadInvoiceReport()
        {
            if (SelectedJobID == 0) return;
            if (_reportLoading) return;  // STOP RE-ENTRY

            _reportLoading = true;

           
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 1 && !_allowReportTab)
            {
                e.Cancel = true;
                MessageBox.Show("Click 'Generate Report' to view the invoice report.",
                    "No Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
