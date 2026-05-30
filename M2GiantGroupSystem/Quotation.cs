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

        private void button2_Click(object sender, EventArgs e)
        {
            dateIssuedDateTimePicker.Value = DateTime.Now;
            expiryDateDateTimePicker.Value = DateTime.Now.AddDays(30);
            dateGeneratedDateTimePicker.Value = DateTime.Now;
            txtAmount.Text = "0.00";
            txtFilePath.Text = "";
            cboQuoteStatus.SelectedIndex = -1;
            selectedJobsGridView.Rows.Clear();
            txtEditQuoteID.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (quoteDataGridView.CurrentRow != null)
            {
                DataGridViewRow selectedRow = quoteDataGridView.CurrentRow;

                // 1. GENTLE USER REMINDER & UI LOCKING: Check if the file path is missing from the grid
                string checkPath = selectedRow.Cells["QuoteFilePath"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(checkPath))
                {
                    // Inform the user why they can't touch the file path or click browse yet
                    MessageBox.Show("Note: No generated PDF file path is associated with this record yet.\n\n" +
                                    "You will not be able to manually edit the file path layout or click the 'Browse' button right now. " +
                                    "Please save this quote as a PDF first to lock in its storage directory path link.",
                                    "Document Path Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Lock down the file path input controls dynamically
                    txtEditFilePath.ReadOnly = true;
                    button4.Enabled = false; // Make sure this matches your actual Browse Button control name!
                }
                else
                {
                    // Unlock them completely if an active path already exists
                    txtEditFilePath.ReadOnly = false;
                    button4.Enabled = true;
                }

                // 2. Map available visible columns directly from the selected grid row layout
                txtEditJobRequestID.Text = selectedRow.Cells["QuoteJobRequestID"].Value?.ToString() ?? "";
                txtEditQuoteID.Text = selectedRow.Cells["QuoteQuoteID"].Value.ToString();
                cmbEditStatus.SelectedItem = selectedRow.Cells["QuoteQuoteStatus"].Value?.ToString();
                txtEditFilePath.Text = checkPath ?? "";

                // 3. Safely parse the total gross amount from the data grid row
                decimal totalFromDatabase = 0;
                if (selectedRow.Cells["QuoteAmount"].Value != null)
                {
                    totalFromDatabase = Convert.ToDecimal(selectedRow.Cells["QuoteAmount"].Value);
                }

                // 4. Reverse-engineer the pricing stack back to split fields (15% VAT rate)
                decimal calculatedSubtotal = totalFromDatabase / 1.15m;
                decimal calculatedVat = totalFromDatabase - calculatedSubtotal;

                // 5. Pre-fill your split UI Textboxes formatted cleanly to two decimal places
                txtEditAmount.Text = calculatedSubtotal.ToString("F2");
                textBox4.Text = calculatedVat.ToString("F2");
                textBox2.Text = totalFromDatabase.ToString("F2"); // Amount with VAT

                // 6. Handle Date Issued safely from the visible grid row layout
                dtpEditIssued.Value = selectedRow.Cells["dateIssued"].Value != DBNull.Value
                    ? Convert.ToDateTime(selectedRow.Cells["dateIssued"].Value)
                    : DateTime.Now;

                // 7. HIDDEN DATASET BYPASS: Pull hidden date values directly from memory cache
                try
                {
                    int targetQuoteID = Convert.ToInt32(txtEditQuoteID.Text);

                    // Query the strongly-typed master dataset table to locate the full backend record row
                    var masterRow = this.groupWst1DataSet.Quote.FindByQuoteID(targetQuoteID);

                    if (masterRow != null)
                    {
                        // Direct indexer checking for Expiry Date to completely bypass CS1061 compile errors
                        if (masterRow["expiryDate"] == DBNull.Value || masterRow["expiryDate"] == null)
                        {
                            dtpEditExpiry.Value = DateTime.Now.AddDays(30); // Default fallback
                        }
                        else
                        {
                            dtpEditExpiry.Value = masterRow.expiryDate;
                        }

                        // Direct indexer checking for Date Generated to completely bypass CS1061 compile errors
                        if (masterRow["dateGenerated"] == DBNull.Value || masterRow["dateGenerated"] == null)
                        {
                            dtpEditGenerated.Value = DateTime.Now; // Default fallback
                        }
                        else
                        {
                            dtpEditGenerated.Value = masterRow.dateGenerated;
                        }
                    }
                    else
                    {
                        // Fallback safe defaults if the row record isn't loaded locally
                        dtpEditExpiry.Value = DateTime.Now.AddDays(30);
                        dtpEditGenerated.Value = DateTime.Now;
                    }
                }
                catch
                {
                    // Fail-safe default catch to ensure form does not crash on empty values
                    dtpEditExpiry.Value = DateTime.Now.AddDays(30);
                    dtpEditGenerated.Value = DateTime.Now;
                }
            }
            else
            {
                MessageBox.Show("Please select a quote row from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
