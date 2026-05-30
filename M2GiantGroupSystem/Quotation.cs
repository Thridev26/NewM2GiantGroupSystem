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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Grab the ID from the selected grid row
                int clickedID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["jobRequestID"].Value);

                // 2. USE THE QUERY BUILDER to fetch the exact row data safely from the DB
                var jobRequestTable = this.jobRequestTableAdapter.GetDataBy2(clickedID);

                if (jobRequestTable.Rows.Count > 0)
                {
                    // Grab the specific typed row from our dataset
                    var selectedJob = jobRequestTable[0];

                    // 3. Populate your UI elements cleanly using the database values
                    jobRequestIDTextBox.Text = selectedJob.jobRequestID.ToString();
                    urgencyLevelTextBox.Text = selectedJob.urgencyLevel;

                    //  NEW ADJUSTED NULL-SAFE VERSION:
                    // 3. Populate your UI elements cleanly using database values (checking for DBNull)
                    jobRequestIDTextBox.Text = selectedJob.jobRequestID.ToString();
                    urgencyLevelTextBox.Text = selectedJob.urgencyLevel;

                    // Check if longitude is NULL in database
                    if (selectedJob["longitude"] == DBNull.Value || string.IsNullOrWhiteSpace(selectedJob["longitude"].ToString()))
                    {
                        longitudeTextBox.Text = "N/A";
                    }
                    else
                    {
                        longitudeTextBox.Text = Convert.ToDouble(selectedJob.longitude).ToString();
                    }

                    // Check if latitude is NULL in database
                    if (selectedJob["latitude"] == DBNull.Value || string.IsNullOrWhiteSpace(selectedJob["latitude"].ToString()))
                    {
                        latitudeTextBox.Text = "N/A";
                    }
                    else
                    {
                        latitudeTextBox.Text = Convert.ToDouble(selectedJob.latitude).ToString();
                    }

                    // 4. Extract coordinates safely. If NULL, pass placeholder coordinates (like 0, 0) to flag fallback behavior.
                    double clientLat = (selectedJob["latitude"] != DBNull.Value) ? Convert.ToDouble(selectedJob.latitude) : 0.0;
                    double clientLng = (selectedJob["longitude"] != DBNull.Value) ? Convert.ToDouble(selectedJob.longitude) : 0.0;

                    currentTravelFee = CalculateTravelFee(clientLat, clientLng);

                    // 5. Update the UI Amount box
                    txtAmount.Text = currentTravelFee.ToString("F2");

                    MessageBox.Show($" You have selected Job Request #{clickedID} \nCalculated Travel Fee: R{currentTravelFee}",
                                    "System Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 1. Wipe out any old job listings in the grid if switching to a new client request
                    selectedJobsGridView.Rows.Clear();

                    // 2. Add the calculated Travel Fee as the foundational first row item in the grid
                    // This maps exactly to your designer layout slots: Job Type, Base Rate, Unit Type, Quantity, Total
                    selectedJobsGridView.Rows.Add("Transport & Travel Call-out", currentTravelFee, "Flat Rate", 1.0, currentTravelFee);

                    // 3. Force the DataGridView to completely finish registering the new row layout internally
                    selectedJobsGridView.Refresh();

                    // 4.Run the calculation engine! It sees the travel fee row and locks R255,56 into txtAmount
                    RecalculateGrandTotalFromUI();

                    // ===================================================================================
                    // STEP 4: Filter the middle grid view to show ONLY the services requested by this client
                    // ===================================================================================
                    this.jobTypeTableAdapter.FillByID(this.groupWst1DataSet.JobType, clickedID);
                }
            }
        }

        // 1. Hardcoded Business Location Constants 
        private const double BaseLatitude = -29.890840081918007;
        private const double BaseLongitude = 30.905937134956915;
        private const decimal CostPerKilometer = 4.50m; // R4.50 per km for fuel/transport
        private const decimal BaseCallOutFee = 150.00m; // Flat base fee just to drive out

        private decimal CalculateTravelFee(double clientLat, double clientLng)
        {
            // If coordinates are missing or set to our null-flag (0,0), charge a flat fallback fee
            if (clientLat == 0.0 || clientLng == 0.0)
            {
                decimal flatFallbackFee = 250.00m; // Adjust this amount to whatever your group requires!
                return flatFallbackFee;
            }

            // Otherwise, execute your normal mathematical distance logic
            double dLat = (clientLat - BaseLatitude) * (Math.PI / 180.0);
            double dLng = (clientLng - BaseLongitude) * (Math.PI / 180.0);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(BaseLatitude * (Math.PI / 180.0)) * Math.Cos(clientLat * (Math.PI / 180.0)) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distanceInKm = 6371.0 * c;

            decimal transportCost = BaseCallOutFee + (Convert.ToDecimal(distanceInKm) * 2 * CostPerKilometer);

            return Math.Round(transportCost, 2);
        }

        private void RecalculateGrandTotalFromUI()
        {
            decimal subTotalAccumulator = 0.00m;

            foreach (DataGridViewRow row in selectedJobsGridView.Rows)
            {
                if (!row.IsNewRow && row.Cells[4].Value != null)
                {
                    subTotalAccumulator += Convert.ToDecimal(row.Cells[4].Value);
                }
            }

            // 1. Calculate tax and grand totals using strict decimal precision
            decimal vatAccumulator = subTotalAccumulator * 0.15m;
            decimal grandTotalAccumulator = subTotalAccumulator + vatAccumulator;

            // 2. Output formatted strings back to your group box controls
            txtAmount.Text = subTotalAccumulator.ToString("F2");
            txtVAT.Text = vatAccumulator.ToString("F2");
            txtTotalwithVAT.Text = grandTotalAccumulator.ToString("F2");
        }

        private void jobTypeDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = jobTypeDataGridView.Rows[e.RowIndex];

                string jobName = selectedRow.Cells["jobTypeName"].Value?.ToString();
                string jobRate = selectedRow.Cells["jobRate"].Value?.ToString();
                string unitDescription = selectedRow.Cells[3].Value?.ToString() ?? "Per Unit";

                decimal baseRate = Convert.ToDecimal(jobRate);

                // 1. Automatically seed the baseline Travel Fee item first if the grid is empty
                if (selectedJobsGridView.Rows.Count == 0 && currentTravelFee > 0)
                {
                    // Maps exactly to your designer columns: Job Type, Base Rate, Unit Type, Quantity, Total
                    selectedJobsGridView.Rows.Add("Transport & Travel Call-out", currentTravelFee, "Flat Rate", 1.0, currentTravelFee);
                }

                // 2. Add the selected item directly to the UI columns collection
                selectedJobsGridView.Rows.Add(jobName, baseRate, unitDescription, 1.0, baseRate);

                // 3. Update the grand total box
                RecalculateGrandTotalFromUI();
            }
        }

        private void selectedJobsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        { // 1.Ensure we are looking at a valid data row, not the column header
            if (e.RowIndex >= 0)
            {
                // 2. Target the 'Quantity' column directly by its physical slot position index (Index 3)
                if (e.ColumnIndex == 3)
                {
                    DataGridViewRow currentRow = selectedJobsGridView.Rows[e.RowIndex];

                    // 3. Extract the updated Quantity entered by the user safely
                    double quantityInput = 0;
                    if (currentRow.Cells[3].Value != null)
                    {
                        // Safely convert the object cell value to a double-precision number
                        double.TryParse(currentRow.Cells[3].Value.ToString(), out quantityInput);
                    }

                    // 4. CRITERIA RULE CHECK: Prevent negative inputs from corrupting your financial records
                    if (quantityInput < 0)
                    {
                        MessageBox.Show("Quantity cannot be a negative amount. Resetting line item input to 0.",
                                        "Validation Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        quantityInput = 0;
                        currentRow.Cells[3].Value = 0; // Force the cell to reflect the fallback correction visually
                    }

                    // 5. Extract the unchangeable Base Rate column value (Index 1)
                    decimal baseRateValue = 0;
                    if (currentRow.Cells[1].Value != null)
                    {
                        decimal.TryParse(currentRow.Cells[1].Value.ToString(), out baseRateValue);
                    }

                    // 6. RUN THE MATHEMATICAL MULTIPLICATION
                    // Round nicely to 2 decimal places for South African Rand currency standards
                    decimal recalculatedLineTotal = Math.Round(baseRateValue * (decimal)quantityInput, 2);

                    // 7. Write the output value back to your visual 'Total' column cell (Index 4)
                    currentRow.Cells[4].Value = recalculatedLineTotal;

                    // 8. Force the entire system to tally up all items and refresh your txtAmount display
                    RecalculateGrandTotalFromUI();
                }
            }
        }

        private void selectedJobsGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Forces the cell to commit its changes the instant the user hits Enter or clicks away
            if (selectedJobsGridView.IsCurrentCellDirty)
            {
                selectedJobsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
