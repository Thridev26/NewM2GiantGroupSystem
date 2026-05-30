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
    public partial class Quotation : Form
    {
        private decimal currentTravelFee = 0.00m;
        public Quotation()
        {
            InitializeComponent();

        }

        private void Quotation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupWst1DataSet.Quote' table. You can move, or remove it, as needed.
            this.quoteTableAdapter.Fill(this.groupWst1DataSet.Quote);
            // TODO: This line of code loads data into the 'groupWst1DataSet.JobType' table. You can move, or remove it, as needed.
            this.jobTypeTableAdapter.Fill(this.groupWst1DataSet.JobType);
            // TODO: This line of code loads data into the 'groupWst1DataSet.JobRequest' table. You can move, or remove it, as needed.
            this.jobRequestTableAdapter.Fill(this.groupWst1DataSet.JobRequest);

            // 2. FORCE THE GRID TO SORT NUMERICALLY FROM LOWEST TO HIGHEST
            // This organizes the rows cleanly by Quote ID so they display sequentially (1, 2, 3...)
            quoteDataGridView.Sort(quoteDataGridView.Columns["QuoteID_T"], System.ComponentModel.ListSortDirection.Ascending);
            UpdateQuoteCount();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "No Filter")
            {
                this.quoteTableAdapter.Fill(this.groupWst1DataSet.Quote);
                UpdateQuoteCount();
                return;
            }
            else
            {
                quoteTableAdapter.FillByQuoteStatus(this.groupWst1DataSet.Quote, comboBox2.SelectedItem.ToString());
                UpdateQuoteCount();
            }
        }

        private void UpdateQuoteCount()
        {
            // bindingSource1 is the data source bound to your DataGridView
            int activeQuotesCount = quoteBindingSource.Count;

            textBox3.Text = activeQuotesCount.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. VALIDATION: Ensure the user selected a status
            if (cboQuoteStatus.SelectedItem == null)
            {
                MessageBox.Show("Please add valid data to create a new quote.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. THE JOB REQUEST ID:
            int currentJobRequestID = 0;
            if (!int.TryParse(jobRequestIDTextBox.Text, out currentJobRequestID))
            {
                MessageBox.Show("Please select a valid Job Request from the Management Deck before saving.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 3. EXECUTE THE INSERT
                // This maps exactly to the (int, string, string, string, decimal, string, string) parameters
                this.quoteTableAdapter.InsertNewQuote(
                    currentJobRequestID,                                    // @jobRequestID (int)
                    dateIssuedDateTimePicker.Value.ToShortDateString(),    // @dateIssued (string)
                    expiryDateDateTimePicker.Value.ToShortDateString(),    // @expiryDate (string)
                    dateGeneratedDateTimePicker.Value.ToShortDateString(), // @dateGenerated (string)
                    Convert.ToDecimal(txtAmount.Text),                     // @amount (decimal)
                    cboQuoteStatus.SelectedItem.ToString(),                // @quoteStatus (string)
                    string.IsNullOrWhiteSpace(txtFilePath.Text) ? null : txtFilePath.Text // @filePath (string - handles empty paths nicely)
                );

                quoteTableAdapter.Fill(this.groupWst1DataSet.Quote);
                // Success Feedback
                MessageBox.Show("Quote records saved to the database successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dateIssuedDateTimePicker.Value = DateTime.Now;
                expiryDateDateTimePicker.Value = DateTime.Now.AddDays(30);
                dateGeneratedDateTimePicker.Value = DateTime.Now;
                txtAmount.Text = "0.00";
                txtFilePath.Text = "";
                cboQuoteStatus.SelectedIndex = -1;
                selectedJobsGridView.Rows.Clear();
                jobRequestIDTextBox.Text = "";
                longitudeTextBox.Text = "";
                latitudeTextBox.Text = "";
                urgencyLevelTextBox.Text = "";
                txtVAT.Text = "0.00";
                txtTotalwithVAT.Text = "0.00";
            }
            catch (Exception ex)
            {
                // Catch database constraint violations or type-casting issues safely
                MessageBox.Show("An error occurred while saving the quote: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
