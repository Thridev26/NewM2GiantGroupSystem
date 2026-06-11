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
    public partial class paymentMain : Form
    {
        private int selectedPaymentID = 0;
        private int selectedJobID = 0;

        private string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;" +
                                 "Persist Security Info=True;User ID=GroupWst1;Password=dtf39;" +
                                 "Encrypt=True;TrustServerCertificate=True";
        int index;
        public paymentMain(int index)
        {
            InitializeComponent();
            this.index = index;
        }

        private void LoadJobLookup(string search)
        {
            // Shows all jobs so the user can pick which job to add a payment for.
            // Includes client info and the quoted amount so they know what to expect.
             // Ensure we're on the first tab where the lookup grid is visible
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT
                            j.jobID             AS [Job ID],
                            c.clientName + ' ' + c.clientSurname AS [Client],
                            c.emailAddress      AS [Email],
                            jr.siteAddress      AS [Site Address],
                            j.jobStatus         AS [Job Status],
                            q.amount            AS [Quoted Amount (R)]
                        FROM Job j
                        INNER JOIN Quote q      ON j.quoteID = q.QuoteID
                        INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                        INNER JOIN Client c     ON jr.clientID = c.clientID
                        WHERE c.clientName    LIKE @search
                           OR c.clientSurname LIKE @search
                           OR jr.siteAddress  LIKE @search
                           OR CAST(j.jobID AS VARCHAR) LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvJobLookup.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error loading jobs:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error loading jobs:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void paymentMain_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = index; // Open the tab that was requested by the caller
            // Tab styling — same pattern as jobRequestMain_A
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(220, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            // View tab grid setup
            dgvPayment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayment.DefaultCellStyle.SelectionBackColor = Color.Green;
            dgvPayment.ReadOnly = true;

            // Job lookup grid setup
            dgvJobLookup.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobLookup.DefaultCellStyle.SelectionBackColor = Color.Green;
            dgvJobLookup.ReadOnly = true;

            // Edit grid setup
            dgvEditPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEditPayments.DefaultCellStyle.SelectionBackColor = Color.Green;
            dgvEditPayments.ReadOnly = true;

            LoadPaymentsView("");
            LoadJobLookup("");
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            Color backColor = Color.Honeydew;
            if (e.Index == tabControl1.SelectedIndex)
                backColor = Color.LightGreen;

            using (Brush b = new SolidBrush(backColor))
                e.Graphics.FillRectangle(b, tabRect);

            using (Pen p = new Pen(Color.DarkGreen, 1))
                e.Graphics.DrawRectangle(p, tabRect);

            TextRenderer.DrawText(e.Graphics, page.Text, tabFont, tabRect,
                Color.Black,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPaymentID = 0;
            selectedJobID = 0;

            // Refresh edit grid whenever user switches to that tab
            if (tabControl1.SelectedIndex == 2)
                LoadPaymentsView_Edit("");
        }
    

private void LoadPaymentsView(string search)
        {
            // Joins Payment → Job → Quote → JobRequest → Client
            // so the grid shows: PaymentID, Client name, Job status,
            // amount paid, method, payment status, payment date
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT
                            p.paymentID         AS [Payment ID],
                            c.clientName + ' ' + c.clientSurname AS [Client],
                            j.jobID             AS [Job ID],
                            j.jobStatus         AS [Job Status],
                            p.amountPaid        AS [Amount (R)],
                            p.paymentMethod     AS [Method],
                            p.paymentStatus     AS [Payment Status],
                            p.paymentDate       AS [Payment Date]
                        FROM Payment p
                        INNER JOIN Job j        ON p.jobID = j.jobID
                        INNER JOIN Quote q      ON j.quoteID = q.QuoteID
                        INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                        INNER JOIN Client c     ON jr.clientID = c.clientID
                        WHERE c.clientName    LIKE @search
                           OR c.clientSurname LIKE @search
                           OR CAST(j.jobID AS VARCHAR) LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvPayment.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error loading payments:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error loading payments:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Same query reused for the Edit tab grid
        private void LoadPaymentsView_Edit(string search)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT
                            p.paymentID         AS [Payment ID],
                            c.clientName + ' ' + c.clientSurname AS [Client],
                            j.jobID             AS [Job ID],
                            p.amountPaid        AS [Amount (R)],
                            p.paymentMethod     AS [Method],
                            p.paymentStatus     AS [Payment Status],
                            p.paymentDate       AS [Payment Date]
                        FROM Payment p
                        INNER JOIN Job j        ON p.jobID = j.jobID
                        INNER JOIN Quote q      ON j.quoteID = q.QuoteID
                        INNER JOIN JobRequest jr ON q.jobRequestID = jr.jobRequestID
                        INNER JOIN Client c     ON jr.clientID = c.clientID
                        WHERE c.clientName    LIKE @search
                           OR c.clientSurname LIKE @search
                           OR CAST(p.paymentID AS VARCHAR) LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvEditPayments.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error loading payments:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error loading payments:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPaymentsView(txtSearch.Text);
        }

        private void txtJobSearch_TextChanged(object sender, EventArgs e)
        {
            LoadJobLookup(txtJobSearch.Text);
        }

        private void dgvJobLookup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJobLookup.Rows[e.RowIndex].Cells["Job ID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                selectedJobID = Convert.ToInt32(cell.Value);
                // Give the user visual confirmation of which job is selected
                lblSelectedID.Text = "Selected Job ID: " + selectedJobID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting job:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            // — Validation —
            if (selectedJobID == 0)
            {
                MessageBox.Show("Please select a job from the table before saving a payment.",
                    "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter the amount paid.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return;
            }
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount < 0)
            {
                MessageBox.Show("Amount must be a valid positive number.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return;
            }
            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment method.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentMethod.Focus();
                return;
            }
            if (cmbPaymentStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment status.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentStatus.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        INSERT INTO Payment (amountPaid, paymentMethod, paymentStatus, paymentDate, jobID)
                        VALUES (@amount, @method, @status, @date, @jobID)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@method", cmbPaymentMethod.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@status", cmbPaymentStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date", dtpPaymentDate.Value.Date);
                        cmd.Parameters.AddWithValue("@jobID", selectedJobID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Payment saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                txtAmount.Text = "";
                cmbPaymentMethod.SelectedIndex = -1;
                cmbPaymentStatus.SelectedIndex = -1;
                dtpPaymentDate.Value = DateTime.Now;
                selectedJobID = 0;
                lblSelectedID.Text = "No job selected";
                LoadJobLookup("");
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while saving payment:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while saving payment:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEditSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPaymentsView_Edit(txtEditSearch.Text);
        }

        private void dgvEditPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvEditPayments.Rows[e.RowIndex].Cells["Payment ID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Payment ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                selectedPaymentID = Convert.ToInt32(cell.Value);
                lblEditSelected.Text = "Selected Payment ID: " + selectedPaymentID;

                // Populate the edit fields from the selected row
                DataGridViewRow row = dgvEditPayments.Rows[e.RowIndex];

                txtEditAmount.Text = row.Cells["Amount (R)"].Value?.ToString() ?? "";

                cmbEditMethod.SelectedItem = cmbEditMethod.Items
                    .Cast<object>()
                    .FirstOrDefault(i => i.ToString() == row.Cells["Method"].Value?.ToString());

                cmbEditStatus.SelectedItem = cmbEditStatus.Items
                    .Cast<object>()
                    .FirstOrDefault(i => i.ToString() == row.Cells["Payment Status"].Value?.ToString());

                if (row.Cells["Payment Date"].Value != null &&
                    row.Cells["Payment Date"].Value != DBNull.Value)
                    dtpEditDate.Value = Convert.ToDateTime(row.Cells["Payment Date"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting payment:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            // — Validation —
            if (selectedPaymentID == 0)
            {
                MessageBox.Show("Please select a payment from the table before saving changes.",
                    "No Payment Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtEditAmount.Text))
            {
                MessageBox.Show("Please enter the amount paid.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditAmount.Focus();
                return;
            }
            if (!decimal.TryParse(txtEditAmount.Text, out decimal amount) || amount < 0)
            {
                MessageBox.Show("Amount must be a valid positive number.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditAmount.Focus();
                return;
            }
            if (cmbEditMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment method.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEditMethod.Focus();
                return;
            }
            if (cmbEditStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment status.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbEditStatus.Focus();
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to save these changes?",
                "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                MessageBox.Show("No changes were saved.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        UPDATE Payment
                        SET amountPaid    = @amount,
                            paymentMethod = @method,
                            paymentStatus = @status,
                            paymentDate   = @date
                        WHERE paymentID   = @paymentID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@method", cmbEditMethod.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@status", cmbEditStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@date", dtpEditDate.Value.Date);
                        cmd.Parameters.AddWithValue("@paymentID", selectedPaymentID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Payment updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                selectedPaymentID = 0;
                lblEditSelected.Text = "Selected Payment ID: —";
                LoadPaymentsView_Edit(txtEditSearch.Text);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while updating payment:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while updating payment:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {




















        }

        private void txtEditAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cmbEditMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbEditStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dtpEditDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    
}


 