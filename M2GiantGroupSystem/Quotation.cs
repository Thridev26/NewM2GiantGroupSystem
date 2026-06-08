using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace M2GiantGroupSystem
{
    public partial class Quotation : Form
    {
        private decimal currentTravelFee = 0.00m;
        int tabIndex;
        // Global flag to temporarily ignore events during form resets        
        public Quotation(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
            this.BackColor = Color.SeaShell; // Soft off-white background for a clean, professional appearance
            tabControl1.BackColor = Color.SeaShell; // Ensure the tab control matches the overall form background

        }

        private void Quotation_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupWst1DataSet.DataTable3' table. You can move, or remove it, as needed.
            this.dataTable3TableAdapter.Fill(this.groupWst1DataSet.DataTable3);
            // TODO: This line of code loads data into the 'groupWst1DataSet.DataTable2' table. You can move, or remove it, as needed.
            this.dataTable2TableAdapter.Fill(this.groupWst1DataSet.DataTable2);
            tabControl1.SelectedIndex = tabIndex;
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
                this.dataTable3TableAdapter.Fill(this.groupWst1DataSet.DataTable3);
                UpdateQuoteCount();
                return;
            }
            else
            {
                dataTable3TableAdapter.FillByQuoteStatus(this.groupWst1DataSet.DataTable3, comboBox2.SelectedItem.ToString());
                UpdateQuoteCount();
            }
        }

        private void UpdateQuoteCount()
        {
            // bindingSource1 is the data source bound to the DataGridView
            int activeQuotesCount = quoteBindingSource.Count;

            textBox3.Text = activeQuotesCount.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Ensure the user selected a status
            if (cboQuoteStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a Quote status before proceeding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. THE JOB REQUEST ID VALIDATION
            int currentJobRequestID = 0;
            if (!int.TryParse(jobRequestIDTextBox.Text, out currentJobRequestID))
            {
                MessageBox.Show("Please select a valid Job Request from the Management Deck before saving.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NEW: Anti-Fraud & Date Validation Guard Rails            
            DateTime today = DateTime.Today;

            // Guard Rail A: Prevent Backdating the Issued Date (Fraud Prevention)
            if (dateIssuedDateTimePicker.Value.Date != today)
            {
                MessageBox.Show("The Date Issued must be exactly today's date. Backdating or future-dating quotation records is strictly prohibited.",
                                "Fraud Prevention Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                // Auto-correct the value back to today for the user
                dateIssuedDateTimePicker.Value = today;
                return;
            }

            // Guard Rail B: Prevent Backdating the Generated Date (Fraud Prevention)
            if (dateGeneratedDateTimePicker.Value.Date != today)
            {
                MessageBox.Show("The Date Generated must match today's date. System logs must remain accurate.",
                                "Fraud Prevention Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                dateGeneratedDateTimePicker.Value = today;
                return;
            }

            // Guard Rail C: Prevent an Expiry Date in the Past
            if (expiryDateDateTimePicker.Value.Date < today)
            {
                MessageBox.Show("The Expiry Date cannot be set to a past date. Please choose a valid future validation timeline.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guard Rail D: Ensure Expiry is at least today or later than Issued Date
            if (expiryDateDateTimePicker.Value.Date < dateIssuedDateTimePicker.Value.Date)
            {
                MessageBox.Show("The Expiry Date cannot be earlier than the Date Issued.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Guard Rail - Prevent Saving with ONLY a Travel Fee

            if (selectedJobsGridView.Rows.Count <= 1)
            {
                MessageBox.Show("You cannot save a quote with only a Travel Call-out fee. Please double-click at least one service item from the requested job types table first.",
                                "Missing Services",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Stops execution before touching the database!
            }

            try
            {
                // EXECUTE THE INSERT
                this.quoteTableAdapter.InsertNewQuote(
                    currentJobRequestID,                                   // @jobRequestID (int)
                    dateIssuedDateTimePicker.Value.ToShortDateString(),    // @dateIssued (string)
                    expiryDateDateTimePicker.Value.ToShortDateString(),    // @expiryDate (string)
                    dateGeneratedDateTimePicker.Value.ToShortDateString(), // @dateGenerated (string)
                    Convert.ToDecimal(txtAmount.Text),                     // @amount (decimal)
                    cboQuoteStatus.SelectedItem.ToString(),                // @quoteStatus (string)
                    string.IsNullOrWhiteSpace(txtFilePath.Text) ? null : txtFilePath.Text // @filePath (string)
                );                

                // Success Feedback
                MessageBox.Show("Your Quote has been successfully created and saved in your database. Go to Edit Quotes to save it as a PDF or Print!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Safe cleaning to match the Clear button fix

                // We just cleanly reset the suggested values without touching Min/Max properties.
                dateIssuedDateTimePicker.Value = DateTime.Today; //
                dateGeneratedDateTimePicker.Value = DateTime.Today; //
                expiryDateDateTimePicker.Value = DateTime.Today.AddDays(30); //

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

                // 1. Refresh the custom data tables that feed your visible UI grids
                this.dataTable3TableAdapter.Fill(this.groupWst1DataSet.DataTable3);
                this.dataTable2TableAdapter.Fill(this.groupWst1DataSet.DataTable2);
                this.quoteTableAdapter.Fill(this.groupWst1DataSet.Quote);

                // 2. Instantly refresh the client list so the SQL 'NOT IN' condition hides the user
                this.jobRequestTableAdapter.Fill(this.groupWst1DataSet.JobRequest);

                // 3. Force the grid to maintain your custom numerical sort layout
                quoteDataGridView.Sort(quoteDataGridView.Columns["QuoteID_T"], System.ComponentModel.ListSortDirection.Ascending);

                // Deactivate save button until the next client selection
                button3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unfortunately an error occurred while saving the quote: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {        
            // 2. ASSIGN NEW DEFAULT VALUES SAFELY            
            dateIssuedDateTimePicker.Value = DateTime.Today;
            dateGeneratedDateTimePicker.Value = DateTime.Today;
            expiryDateDateTimePicker.Value = DateTime.Today.AddDays(30);
            txtAmount.Text = "0.00";
            txtVAT.Text = "0.00";
            txtTotalwithVAT.Text = "0.00";
            txtFilePath.Text = "";
            cboQuoteStatus.SelectedIndex = -1;
            selectedJobsGridView.Rows.Clear();
            txtEditQuoteID.Text = "";
            longitudeTextBox.Text = "";
            latitudeTextBox.Text = "";
            urgencyLevelTextBox.Text = "";
            jobRequestIDTextBox.Text = "";
            cmbSearchColumn.SelectedIndex = -1;
            txtSearchRequests.Text = "";
            dtpSearchDate.Value = DateTime.Today;
            jobTypeTableAdapter.Fill(this.groupWst1DataSet.JobType); // Refresh the job types in case they were modified
            // ADD THIS LINE HERE: Refresh the top grid on Clear too!                                                                     
            this.jobRequestTableAdapter.Fill(this.groupWst1DataSet.JobRequest);
            jobTypeDataGridView.ClearSelection(); // Clear any existing selection to avoid confusion
            selectedJobsGridView.ClearSelection(); // Clear the quote details grid selection as well for a clean slate
            button3.Enabled = false; // Disable the button till the user selects a job request and starts building a quote
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (quoteDataGridView.CurrentRow != null)
            {
                DataGridViewRow selectedRow = quoteDataGridView.CurrentRow;

                // GENTLE USER REMINDER & UI LOCKING: Check if the file path is missing from the grid
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

                // Map available visible columns directly from the selected grid row layout
                txtEditJobRequestID.Text = selectedRow.Cells["QuoteJobRequestID"].Value?.ToString() ?? "";
                txtEditQuoteID.Text = selectedRow.Cells["QuoteID_T"].Value.ToString();
                cmbEditStatus.SelectedItem = selectedRow.Cells["QuoteQuoteStatus"].Value?.ToString();
                txtEditFilePath.Text = checkPath ?? "";

                // Safely parse the total gross amount from the data grid row
                decimal totalFromDatabase = 0;
                if (selectedRow.Cells["QuoteAmount"].Value != null)
                {
                    totalFromDatabase = Convert.ToDecimal(selectedRow.Cells["QuoteAmount"].Value);
                }

                // Reverse-engineer the pricing stack back to split fields (15% VAT rate)
                decimal calculatedSubtotal = totalFromDatabase / 1.15m;
                decimal calculatedVat = totalFromDatabase - calculatedSubtotal;

                // Pre-fill the split UI Textboxes formatted cleanly to two decimal places
                txtEditAmount.Text = calculatedSubtotal.ToString("F2");
                textBox4.Text = calculatedVat.ToString("F2");
                textBox2.Text = totalFromDatabase.ToString("F2"); // Amount with VAT

                // Handle Date Issued safely from the visible grid row layout
                dtpEditIssued.Value = selectedRow.Cells["dateIssued"].Value != DBNull.Value
                    ? Convert.ToDateTime(selectedRow.Cells["dateIssued"].Value)
                    : DateTime.Now;

                // Pull hidden date values directly from memory cache
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
           
        }

        // Hardcoded Business Location Constants 
        private const double BaseLatitude = -29.890840081918007;
        private const double BaseLongitude = 30.905937134956915;
        private const decimal CostPerKilometer = 6.50m; // R6.50 per km for fuel/transport
        private const decimal BaseCallOutFee = 450.00m; // Flat base fee just to drive out

        private decimal CalculateTravelFee(double clientLat, double clientLng)
        {
            // If coordinates are missing or set to our null-flag (0,0), charge a flat fallback fee
            if (clientLat == 0.0 || clientLng == 0.0)
            {
                decimal flatFallbackFee = 850.00m; // Adjust this amount to whatever your group requires!
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

            // Loop through every active row in your grid
            foreach (DataGridViewRow row in selectedJobsGridView.Rows)
            {
                if (row.IsNewRow) continue; // Ignore the blank line at the bottom

                // Find the SUBTOTAL cell for this row and add it to our running total
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (selectedJobsGridView.Columns[cell.ColumnIndex].HeaderText == "Total")
                    {
                        if (cell.Value != null && cell.Value != DBNull.Value)
                        {
                            subTotalAccumulator += Convert.ToDecimal(cell.Value);
                        }
                        break;
                    }
                }
            }

            // Standard South African 15% statutory calculations
            decimal vatAccumulator = subTotalAccumulator * 0.15m;
            decimal grandTotalAccumulator = subTotalAccumulator + vatAccumulator;

            // Push the running live totals straight into your bottom group textboxes
            txtAmount.Text = subTotalAccumulator.ToString("F2");            // Subtotal text box
            txtVAT.Text = vatAccumulator.ToString("F2");                  // VAT text box
            txtTotalwithVAT.Text = grandTotalAccumulator.ToString("F2");   // Grand Total text box
            //decimal subTotalAccumulator = 0.00m;

            //foreach (DataGridViewRow row in selectedJobsGridView.Rows)
            //{
            //    if (!row.IsNewRow && row.Cells[4].Value != null)
            //    {
            //        subTotalAccumulator += Convert.ToDecimal(row.Cells[4].Value);
            //    }
            //}

            //// Calculate tax and grand totals using strict decimal precision
            //decimal vatAccumulator = subTotalAccumulator * 0.15m;
            //decimal grandTotalAccumulator = subTotalAccumulator + vatAccumulator;

            //// Output formatted strings back to your group box controls
            //txtAmount.Text = subTotalAccumulator.ToString("F2");
            //txtVAT.Text = vatAccumulator.ToString("F2");
            //txtTotalwithVAT.Text = grandTotalAccumulator.ToString("F2");
        }

        private void jobTypeDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            
            // Guard Rail - Check if a Client is Selected First       
            if (string.IsNullOrWhiteSpace(jobRequestIDTextBox.Text))
            {
                MessageBox.Show("Please select a client's Job Request from the top table before adding specific services.",
                                "Selection Required",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Stops execution dead in its tracks! The popup will NOT show.
            }

            // 1. Get the details of the service they selected
            string jobName = jobTypeDataGridView.Rows[e.RowIndex].Cells["jobTypeName"].Value.ToString();
            string unitType = jobTypeDataGridView.Rows[e.RowIndex].Cells["rateDescription"].Value.ToString(); // e.g., "Per tree", "Per square meter"
            decimal baseRate = Convert.ToDecimal(jobTypeDataGridView.Rows[e.RowIndex].Cells["jobRate"].Value);

            // 2. Prompt the admin lady with a clear question based on the unit type
            string promptMessage = $"Enter the quantity for {jobName} ({unitType}):";
            string userInput = Interaction.InputBox(promptMessage, "Enter Quantity", "1");

            // If they cancel or leave it empty, stop the addition
            if (string.IsNullOrWhiteSpace(userInput)) return;

            if (decimal.TryParse(userInput, out decimal quantity))
            {
                // 3. Calculate the line item total right here
                decimal lineTotal = baseRate * quantity;

                // 4. Add the row directly to your bottom grid with all fields filled!
                int rowIndex = selectedJobsGridView.Rows.Add();
                DataGridViewRow newRow = selectedJobsGridView.Rows[rowIndex];

                newRow.Cells["colJobType"].Value = jobName;
                newRow.Cells["colBaseRate"].Value = baseRate;
                newRow.Cells["colUnitType"].Value = unitType;
                newRow.Cells["colQuantity"].Value = quantity; // Populates your QTY column perfectly
                newRow.Cells["colTotal"].Value = lineTotal;   // Populates your working SUBTOTAL column
                // 5. Update your bottom group text boxes immediately
                RecalculateGrandTotalFromUI();


                // 6. Cleanly remove the row from the top view once the quantity has been entered and the item is added to the quote details grid

                System.Data.DataRowView currentRowView = (System.Data.DataRowView)jobTypeDataGridView.CurrentRow.DataBoundItem;
                currentRowView.Delete();
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow selectedRow = jobTypeDataGridView.Rows[e.RowIndex];

            //    string jobName = selectedRow.Cells["jobTypeName"].Value?.ToString();
            //    string jobRate = selectedRow.Cells["jobRate"].Value?.ToString();
            //    string unitDescription = selectedRow.Cells[3].Value?.ToString() ?? "Unit Type";

            //    decimal baseRate = Convert.ToDecimal(jobRate);

            //    // Automatically seed the baseline Travel Fee item first if the grid is empty
            //    if (selectedJobsGridView.Rows.Count == 0 && currentTravelFee > 0)
            //    {
            //        // Maps exactly to your designer columns: Job Type, Base Rate, Unit Type, Quantity, Total
            //        selectedJobsGridView.Rows.Add("Travel Call-out", currentTravelFee, "Flat Rate", 1.0, currentTravelFee);
            //    }

            //    // Add the selected item directly to the UI columns collection
            //    selectedJobsGridView.Rows.Add(jobName, baseRate, unitDescription, 1.0, baseRate);

            //    // Update the grand total box
            //    RecalculateGrandTotalFromUI();
            //}
        }

        private void selectedJobsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Fail-safe: Skip header rows or initialization glitches
            if (e.RowIndex < 0) return;

            // Get the exact header text of the column that was just changed
            string columnName = selectedJobsGridView.Columns[e.ColumnIndex].HeaderText;

            // Only run the math if the admin lady edited the QTY column
            if (columnName == "QTY")
            {
                DataGridViewRow row = selectedJobsGridView.Rows[e.RowIndex];

                decimal baseRate = 0;
                decimal quantity = 1;

                // 1. Locate and read the pre-existing RATE cell value populated from your database
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (selectedJobsGridView.Columns[cell.ColumnIndex].HeaderText == "RATE")
                    {
                        if (cell.Value != null && cell.Value != DBNull.Value)
                        {
                            baseRate = Convert.ToDecimal(cell.Value);
                        }
                        break;
                    }
                }

                // 2. Read what the user just typed into the QTY column safely
                if (row.Cells[e.ColumnIndex].Value != null && decimal.TryParse(row.Cells[e.ColumnIndex].Value.ToString(), out decimal parsedQty))
                {
                    quantity = parsedQty;
                }
                else
                {
                    row.Cells[e.ColumnIndex].Value = 1; // Default fallback to 1 if empty or typed incorrectly
                }

                // 3. THE MATHEMATICS: Base Rate x Quantity
                decimal calculatedLineTotal = baseRate * quantity;

                // 4. Update the visual grid's SUBTOTAL column for this row immediately
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (selectedJobsGridView.Columns[cell.ColumnIndex].HeaderText == "SUBTOTAL")
                    {
                        cell.Value = calculatedLineTotal;
                        break;
                    }
                }

                // 5. UPDATE THE SUMMARY: Sum up all rows from the SUBTOTAL column live
                RecalculateGrandTotalFromUI();
            }
            //// Ensure we are looking at a valid data row, not the column header
            //if (e.RowIndex >= 0)
            //{
            //    //Target the 'Quantity' column directly by its physical slot position index (Index 3)
            //    if (e.ColumnIndex == 3)
            //    {
            //        DataGridViewRow currentRow = selectedJobsGridView.Rows[e.RowIndex];

            //        //Extract the updated Quantity entered by the user safely
            //        double quantityInput = 0;
            //        if (currentRow.Cells[3].Value != null)
            //        {
            //            // Safely convert the object cell value to a double-precision number
            //            double.TryParse(currentRow.Cells[3].Value.ToString(), out quantityInput);
            //        }

            //        // Prevent negative inputs from corrupting your financial records
            //        if (quantityInput < 0)
            //        {
            //            MessageBox.Show("The quantity cannot be a negative amount. Resetting line item input to 0.",
            //                            "Validation Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            quantityInput = 0;
            //            currentRow.Cells[3].Value = 0; // Force the cell to reflect the fallback correction visually
            //        }

            //        // 5. Extract the unchangeable Base Rate column value (Index 1)
            //        decimal baseRateValue = 0;
            //        if (currentRow.Cells[1].Value != null)
            //        {
            //            decimal.TryParse(currentRow.Cells[1].Value.ToString(), out baseRateValue);
            //        }

            //        // 6. RUN THE MATHEMATICAL MULTIPLICATION
            //        // Round nicely to 2 decimal places for South African Rand currency standards
            //        decimal recalculatedLineTotal = Math.Round(baseRateValue * (decimal)quantityInput, 2);

            //        // 7. Write the output value back to your visual 'Total' column cell (Index 4)
            //        currentRow.Cells[4].Value = recalculatedLineTotal;

            //        // 8. Force the entire system to tally up all items and refresh your txtAmount display
            //        RecalculateGrandTotalFromUI();
            //    }
            //}
        }

        private void selectedJobsGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (selectedJobsGridView.IsCurrentCellDirty)
            {
                // Instantly commits the keystroke changes to data memory raw value layer
                selectedJobsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 1. VALIDATION: Ensure an active Quote row has actually been loaded from the grid
            if (string.IsNullOrWhiteSpace(txtEditQuoteID.Text))
            {
                MessageBox.Show("No active quote record has been selected or loaded or editing. Please select a row from the table above and click Edit first.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. VALIDATION: Check that the status combo box has a value selected
            if (cmbEditStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid Quote Status before saving changes.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. LOGICAL TIMELINE VALIDATION: Prevent human error by ensuring expiry date isn't set back before issue date
            if (dtpEditExpiry.Value.Date < dtpEditIssued.Value.Date)
            {
                MessageBox.Show("The Expiry Date cannot be earlier than the Date Issued. Please review your date selections.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 4. Extract and parse your UI values into clean data types
                int targetQuoteID = Convert.ToInt32(txtEditQuoteID.Text);

                // Extract short date strings directly from your three UI date picker elements
                string updatedIssued = dtpEditIssued.Value.ToShortDateString();
                string updatedExpiry = dtpEditExpiry.Value.ToShortDateString();
                string updatedGenerated = dtpEditGenerated.Value.ToShortDateString();

                // Pull the total gross value (Inclusive of VAT) back out of your final summary field (textBox2)
                decimal updatedAmount = Convert.ToDecimal(textBox2.Text);

                string updatedStatus = cmbEditStatus.SelectedItem.ToString();
                string updatedPath = string.IsNullOrWhiteSpace(txtEditFilePath.Text) ? null : txtEditFilePath.Text;

                // 5. EXECUTE THE TABLEADAPTER QUERY
                // This perfectly matches the positional sequence defined in your query configuration wizard!
                this.quoteTableAdapter.UpdateQuote(
                    updatedIssued,       // @dateIssued (string)
                    updatedExpiry,       // @expiryDate (string) -> Now successfully gathered from your UI control
                    updatedGenerated,    // @dateGenerated (string) -> Now successfully gathered from your UI control
                    updatedAmount,       // @amount (decimal)
                    updatedStatus,       // @quoteStatus (string)
                    updatedPath,         // @filePath (string)
                    targetQuoteID        // @QuoteID (int - conditional WHERE constraint identifier)
                );

                // 6. DATABASE RE-SYNC AND UI REFRESH
                // Pull down a pristine snapshot to update the local DataTable layout cache
                this.quoteTableAdapter.Fill(this.groupWst1DataSet.Quote);
                UpdateQuoteCount();

                // 7. SUCCESS FEEDBACK
                MessageBox.Show($"Quote record #{targetQuoteID} has been successfully updated.",
                                "System Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while compiling your changes to the Quote: " + ex.Message,
                                "Database Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            
            //THE MAIN BUTTON CLICK HANDLER 
            

            if (quoteDataGridView.CurrentRow == null || quoteDataGridView.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a valid historical quote row from the table.",
                                "No Selection Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = quoteDataGridView.CurrentRow;

                string uniqueQuoteId = selectedRow.Cells["QuoteID_T"].Value?.ToString() ?? "0";
                string jobRequestIdStr = selectedRow.Cells["QuoteJobRequestID"].Value?.ToString() ?? "0";

                string dateIssuedStr = "Pending";
                if (selectedRow.Cells["DateIssued"].Value != null && selectedRow.Cells["DateIssued"].Value != DBNull.Value)
                {
                    if (DateTime.TryParse(selectedRow.Cells["DateIssued"].Value.ToString(), out DateTime parsedDate))
                    {
                        dateIssuedStr = parsedDate.ToString("dd MMMM yyyy");
                    }
                }

                decimal grandTotalAmount = 0.00m;
                if (selectedRow.Cells["QuoteAmount"].Value != null && selectedRow.Cells["QuoteAmount"].Value != DBNull.Value)
                {
                    decimal.TryParse(selectedRow.Cells["QuoteAmount"].Value.ToString(), out grandTotalAmount);
                }

                string fetchedAddress = "No Address Profile Found";

                if (int.TryParse(jobRequestIdStr, out int parsedJobID))
                {
                    var jobRequestTable = this.jobRequestTableAdapter.GetSiteAddress(parsedJobID);

                    if (jobRequestTable != null && jobRequestTable.Rows.Count > 0)
                    {
                        var matchedRow = jobRequestTable[0];

                        if (!matchedRow.IsNull("siteAddress") && !string.IsNullOrWhiteSpace(matchedRow.siteAddress))
                        {
                            fetchedAddress = matchedRow.siteAddress;
                        }
                    }
                    else
                    {
                        fetchedAddress = $"Job Request Reference #{parsedJobID}\nField Site Evaluation Pending";
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF Documents (*.pdf)|*.pdf";
                    saveFileDialog.FileName = $"Quotation_GT_{uniqueQuoteId}.pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string chosenFilePath = saveFileDialog.FileName;

                        BuildArchiveQuotePDF(chosenFilePath, uniqueQuoteId, jobRequestIdStr, dateIssuedStr, grandTotalAmount, fetchedAddress);

                        if (int.TryParse(uniqueQuoteId, out int parsedQuoteID))
                        {
                            this.quoteTableAdapter.UpdateQuoteFilePath(chosenFilePath, parsedQuoteID);
                            selectedRow.Cells["QuoteFilePath"].Value = chosenFilePath;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading quote parameters, compiling PDF, or updating database row record: {ex.Message}",
                                "System Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildArchiveQuotePDF(string outputPath, string quoteId, string jobId, string dateIssued, decimal subTotal, string recipientInfo)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 36f, 36f, 36f, 36f);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
                document.Open();

                BaseColor customBlack = new BaseColor(15, 15, 15);
                BaseColor customGreen = new BaseColor(10, 75, 20);
                BaseColor lightGray = new BaseColor(235, 235, 235);
                BaseColor tableHeaderBg = new BaseColor(10, 10, 10);

                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 28f, customGreen);
                iTextSharp.text.Font headerWhiteFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, BaseColor.WHITE);
                iTextSharp.text.Font bodyWhiteFont = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.WHITE);
                iTextSharp.text.Font bodyBlackFont = FontFactory.GetFont(FontFactory.HELVETICA, 10f, BaseColor.BLACK);
                iTextSharp.text.Font boldBlackFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.BLACK);
                iTextSharp.text.Font headerWhiteFontSmall = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, BaseColor.WHITE);

                decimal grandTotal = subTotal;
                subTotal = grandTotal / 1.15m;
                decimal vatValue = grandTotal - subTotal;

                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100f;
                headerTable.SetWidths(new float[] { 35f, 65f });

                PdfPCell leftHeaderCell = new PdfPCell { BackgroundColor = customBlack, Padding = 20f, Border = PdfPCell.NO_BORDER };
                leftHeaderCell.AddElement(new Paragraph("THE GIANT GROUP\n\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f, BaseColor.WHITE)));
                leftHeaderCell.AddElement(new Paragraph("7 Baumann rd, Head Office\nDurban, Queensburgh Industrial\ninfo@gianttreefelling.co.za\n084 833 1373", bodyWhiteFont));
                headerTable.AddCell(leftHeaderCell);

                PdfPCell rightHeaderCell = new PdfPCell { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, PaddingTop = 30f };
                Paragraph mainTitle = new Paragraph("— QUOTATION —\n\n\n", titleFont);
                mainTitle.Alignment = Element.ALIGN_CENTER;
                rightHeaderCell.AddElement(mainTitle);

                Paragraph quoteMeta = new Paragraph($"Date: {dateIssued}\n", boldBlackFont);
                Paragraph quoteNo = new Paragraph($"Quotation NO. GT – {quoteId}\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f, customGreen));
                quoteMeta.Alignment = Element.ALIGN_RIGHT;
                quoteNo.Alignment = Element.ALIGN_RIGHT;
                rightHeaderCell.AddElement(quoteMeta);
                rightHeaderCell.AddElement(quoteNo);
                headerTable.AddCell(rightHeaderCell);

                document.Add(headerTable);

                // ADJUSTMENT 1: Increased separation gap between the header and the data panel
                document.Add(new iTextSharp.text.Chunk("\n\n\n"));

                PdfPTable bodyTable = new PdfPTable(2);
                bodyTable.WidthPercentage = 100f;
                bodyTable.SetWidths(new float[] { 35f, 65f });

                PdfPCell recipientCell = new PdfPCell { BackgroundColor = customBlack, Padding = 20f, Border = PdfPCell.NO_BORDER };
                recipientCell.AddElement(new Paragraph("R E C I P I E N T\n\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, BaseColor.WHITE)));
                recipientCell.AddElement(new Paragraph(recipientInfo, bodyWhiteFont));
                bodyTable.AddCell(recipientCell);

                PdfPCell itemsGridCell = new PdfPCell { Border = PdfPCell.NO_BORDER, PaddingLeft = 15f };
                PdfPTable innerItemsTable = new PdfPTable(2);
                innerItemsTable.WidthPercentage = 100f;
                innerItemsTable.SetWidths(new float[] { 75f, 25f });

                innerItemsTable.AddCell(new PdfPCell(new Phrase("Description", headerWhiteFont)) { BackgroundColor = tableHeaderBg, Padding = 8f });
                innerItemsTable.AddCell(new PdfPCell(new Phrase("Price", headerWhiteFont)) { BackgroundColor = tableHeaderBg, Padding = 8f, HorizontalAlignment = Element.ALIGN_RIGHT });

                string itemString = $"Tree Felling Services Rendered (Job Request Reference #{jobId})";

                // ADJUSTMENT 2: Added internal padding directly to original cell definitions to increase vertical footprint
                innerItemsTable.AddCell(new PdfPCell(new Phrase(itemString, bodyBlackFont)) { PaddingTop = 14f, PaddingBottom = 14f, PaddingLeft = 8f, PaddingRight = 8f, BackgroundColor = lightGray });
                innerItemsTable.AddCell(new PdfPCell(new Phrase($"R {subTotal:N2}", bodyBlackFont)) { PaddingTop = 14f, PaddingBottom = 14f, PaddingLeft = 8f, PaddingRight = 8f, HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = lightGray });

                innerItemsTable.AddCell(new PdfPCell(new Phrase("All cuttings to be removed to nearest municipal dump site", bodyBlackFont)) { PaddingTop = 14f, PaddingBottom = 14f, PaddingLeft = 8f, PaddingRight = 8f });
                innerItemsTable.AddCell(new PdfPCell(new Phrase("", bodyBlackFont)) { PaddingTop = 14f, PaddingBottom = 14f, PaddingLeft = 8f, PaddingRight = 8f });

                itemsGridCell.AddElement(innerItemsTable);

                PdfPTable totalsTable = new PdfPTable(2);
                totalsTable.WidthPercentage = 65f;
                totalsTable.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalsTable.SpacingBefore = 15f;

                totalsTable.AddCell(new PdfPCell(new Phrase("Sub Total", boldBlackFont)) { Border = PdfPCell.NO_BORDER, Padding = 5f });
                totalsTable.AddCell(new PdfPCell(new Phrase($"R {subTotal:N2}", bodyBlackFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5f });

                totalsTable.AddCell(new PdfPCell(new Phrase("VAT (15%)", boldBlackFont)) { Border = PdfPCell.NO_BORDER, Padding = 5f });
                totalsTable.AddCell(new PdfPCell(new Phrase($"R {vatValue:N2}", bodyBlackFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5f });

                totalsTable.AddCell(new PdfPCell(new Phrase("TOTAL", headerWhiteFontSmall)) { BackgroundColor = customGreen, Padding = 6f });
                totalsTable.AddCell(new PdfPCell(new Phrase($"R {grandTotal:N2}", headerWhiteFontSmall)) { BackgroundColor = customGreen, HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 6f });

                itemsGridCell.AddElement(totalsTable);
                bodyTable.AddCell(itemsGridCell);

                document.Add(bodyTable);

                // ADJUSTMENT 3: Increased structural breaks to push corporate summary down near the page bounds
                document.Add(new iTextSharp.text.Chunk("\n\n\n\n\n"));

                PdfPTable footerPanel = new PdfPTable(1);
                footerPanel.WidthPercentage = 100f;
                PdfPCell footerCell = new PdfPCell { BackgroundColor = lightGray, Padding = 12f, Border = PdfPCell.NO_BORDER };

                footerCell.AddElement(new Paragraph("BANKING DETAILS\n", boldBlackFont));
                footerCell.AddElement(new Paragraph(
                    "Account Holder: Emmans Transport cc t/a TheGiantGroup | Account No: 250898500\n" +
                    "Standard Bank, Cheque account.\n" +
                    "Reg No: 2008/169586/23. Vat No: 4200259861.\n" +
                    "TheGiantGroup, CK No: 2008/169586/23.", bodyBlackFont));

                footerPanel.AddCell(footerCell);
                document.Add(footerPanel);

                // ADJUSTMENT 4: Final minor separation spacing before sign-off block execution
                document.Add(new iTextSharp.text.Chunk("\n\n"));

                PdfPTable signOffTable = new PdfPTable(2);
                signOffTable.WidthPercentage = 100f;

                PdfPCell thanksCell = new PdfPCell(new Paragraph("\"Pursuing excellence in every endeavor.\"", FontFactory.GetFont(FontFactory.TIMES_ITALIC, 11f, customGreen))) { Border = PdfPCell.NO_BORDER, VerticalAlignment = Element.ALIGN_BOTTOM };
                PdfPCell execCell = new PdfPCell(new Paragraph("Shaphan Pillay\nThe Giant Group\nCEO", boldBlackFont)) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };

                signOffTable.AddCell(thanksCell);
                signOffTable.AddCell(execCell);
                document.Add(signOffTable);

                MessageBox.Show($"Quotation PDF for Document GT-{quoteId} has been successfully saved to your device!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while compiling the PDF payload: {ex.Message}", "PDF Rendering Defect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                document.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 1. Initialize the file dialog component
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set the initial directory to the user's Documents folder
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Allow all file extensions as requested
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Title = "Select Quote Document File";

                // 2. Show the dialog window and check if the user actually selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 3. Grab the full file path chosen by the user
                    string selectedFilePath = openFileDialog.FileName;

                    // 4. Assign the string path directly into your UI TextBox
                    txtFilePath.Text = selectedFilePath;
                }
            }
        }

        private void txtEditAmount_TextChanged(object sender, EventArgs e)
        {
            // 1. Check if the subtotal box has valid numeric input
            if (decimal.TryParse(txtEditAmount.Text, out decimal subTotal))
            {
                // 2. Perform live calculations (15% VAT rate)
                decimal vatValue = subTotal * 0.15m;
                decimal grandTotal = subTotal + vatValue;

                // 3. Update the display boxes live, formatted cleanly to 2 decimal places
                textBox4.Text = vatValue.ToString("F2");
                textBox2.Text = grandTotal.ToString("F2"); // Replace with your grand total textbox name
            }
            else
            {
                // 4. Fallback: Clear or set fields to 0.00 if the subtotal textbox is empty or invalid
                textBox4.Text = "0.00";
                textBox2.Text = "0.00"; // Replace with your grand total textbox name
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // 1. Ensure a row is actually selected in your grid
            if (quoteDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a quotation from the list first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = quoteDataGridView.CurrentRow;

                // 2. STRICT CHECK: Reject the operation if the file path column is empty or null
                if (selectedRow.Cells["QuoteFilePath"].Value == null || string.IsNullOrWhiteSpace(selectedRow.Cells["QuoteFilePath"].Value.ToString()))
                {
                    MessageBox.Show("You cannot print this quote because the PDF has not been generated yet.\n\n" +
                                    "Please click the 'Generate and Export as PDF' button first to save the document.",
                                    "Friendly Reminder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Stops the click method execution completely right here!
                }

                // 3. Since the path is guaranteed to exist now, grab it safely
                string targetPdfPath = selectedRow.Cells["QuoteFilePath"].Value.ToString();

                // 4. Open the existing file directly using the system default viewer
                if (File.Exists(targetPdfPath))
                {
                    ProcessStartInfo openPdfShell = new ProcessStartInfo
                    {
                        FileName = targetPdfPath,
                        UseShellExecute = true
                    };
                    Process.Start(openPdfShell);
                }
                else
                {
                    // Catch if the database has a path string, but someone deleted the file off the hard drive
                    MessageBox.Show($"The PDF file path is registered, but the file could not be found on your device at:\n{targetPdfPath}\n\nPlease re-generate the PDF.",
                                    "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while trying to view the quote: " + ex.Message,
                                "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 1. Safety: ensure a quote row is selected
            if (quoteDataGridView.CurrentRow == null || quoteDataGridView.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a valid quotation record from the table list to delete.",
                                "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = quoteDataGridView.CurrentRow;

            // 2. Read primary key (Quote ID) and the related JobRequest ID (if present) so we can acknowledge it
            string quoteIdStr = selectedRow.Cells["QuoteID_T"].Value?.ToString();
            string jobRequestIdStr = selectedRow.Cells["QuoteJobRequestID"].Value?.ToString() ?? "N/A";

            if (string.IsNullOrWhiteSpace(quoteIdStr) || quoteIdStr == "0")
            {
                MessageBox.Show("The selected row does not contain a valid unique primary key ID.",
                                "Data Integrity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Ask the user to confirm deletion and explicitly acknowledge the linked JobRequest
            DialogResult userChoice = MessageBox.Show(
                $"Are you absolutely certain you want to permanently delete Quote ID #{quoteIdStr}?\n\n" +
                $"Linked Job Request ID: {jobRequestIdStr}\n\n" +
                "Important: The Job Request record will remain in the JobRequest table — only the quote entry will be removed.",
                "Confirm Quote Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (userChoice != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int parsedQuoteID = Convert.ToInt32(quoteIdStr);

                // 4. Use the TableAdapter's underlying connection to run a safe parameterized delete.
                //    This avoids relying on designer-generated Delete methods and keeps the operation explicit.
                var sqlConn = (System.Data.SqlClient.SqlConnection)this.quoteTableAdapter.Connection;
                bool openedHere = false;
                if (sqlConn.State != System.Data.ConnectionState.Open)
                {
                    sqlConn.Open();
                    openedHere = true;
                }

                // STEP A: Do not allow the user to delete a quote that has already been assigned to an active job
                
                using (var checkCmd = sqlConn.CreateCommand())
                {
                    // Count how many entries in the Job table rely on this quoteID
                    checkCmd.CommandText = "SELECT COUNT(*) FROM [Job] WHERE quoteID = @QuoteID";
                    checkCmd.Parameters.AddWithValue("@QuoteID", parsedQuoteID);
                    int linkedJobs = (int)checkCmd.ExecuteScalar();

                    if (linkedJobs > 0)
                    {
                        // Beautifully formatted, production-grade error message layout
                        string operationalWarning =
                            $"You may not delete Quote ID #{parsedQuoteID}.\n\n" +
                            "This quotation record is linked to an active Job. ";

                        MessageBox.Show(operationalWarning,
                                        "Quote may not be deleted",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);

                        // Clean up the connection if we opened it in this method
                        if (openedHere)
                        {
                            sqlConn.Close();
                        }

                        return; // Stop right here! Do not proceed to the delete code below.
                    }
                }

                
                // STEP B: If the quote has not been accepted and converted into an active job, allow them to delete it.
                
                using (var cmd = sqlConn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [Quote] WHERE quoteID = @QuoteID";
                    cmd.Parameters.AddWithValue("@QuoteID", parsedQuoteID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        MessageBox.Show($"No quote row was deleted. Quote ID #{parsedQuoteID} may not exist anymore.",
                                        "Delete Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                
                // STEP C: CLEANLY CLOSE THE CONNECTION                
                if (openedHere)
                {
                    sqlConn.Close();
                }

                // 1. Refresh the custom data tables so the deleted row vanishes live
                this.dataTable3TableAdapter.Fill(this.groupWst1DataSet.DataTable3);
                this.dataTable2TableAdapter.Fill(this.groupWst1DataSet.DataTable2);
                this.quoteTableAdapter.Fill(this.groupWst1DataSet.Quote);

                // 2. Force the grid to sort cleanly after the row drops out
                quoteDataGridView.Sort(quoteDataGridView.Columns["QuoteID_T"], System.ComponentModel.ListSortDirection.Ascending);

                // 3. Update counter and restore the Job Request table pool immediately
                UpdateQuoteCount();
                this.jobRequestTableAdapter.Fill(this.groupWst1DataSet.JobRequest);

                // 6. Friendly, explicit feedback for lecturer / user showing we acknowledged the JobRequest link
                MessageBox.Show($"Quote #{parsedQuoteID} has been removed from the Quote table.\n\n" +
                                $"Linked Job Request #{jobRequestIdStr} remains in the JobRequest table and is unaffected by this operation.\n\n" +
                                "If you want to re-create a quote for that Job Request, open the Job Request and generate a new quote.",
                                "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the quote: {ex.Message}",
                                "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSearchColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchRequests.Clear();

            // UI FOCUS: Swap 'YOUR_NEW_BINDING_SOURCE' with your actual custom view BindingSource variable
            dataTable2BindingSource.Filter = "";

            string selectedText = cmbSearchColumn.SelectedItem != null ?
                                  cmbSearchColumn.SelectedItem.ToString() :
                                  cmbSearchColumn.Text.Trim();

            // UI FOCUS: This string "No Filter" must match the default placeholder option text in your ComboBox items
            if (string.IsNullOrWhiteSpace(selectedText) || selectedText == "No Filter")
            {
                txtSearchRequests.Enabled = false;
                txtSearchRequests.Visible = true;
                dtpSearchDate.Enabled = false;
                dtpSearchDate.Visible = false;
                return;
            }

            string dbColumn = GetRealDatabaseColumnName(selectedText);

            // DATABASE FOCUS: These strings must perfectly match the database/DataProperty column names returned by your helper method
            if (dbColumn == "dateRecieved" || dbColumn == "siteEvaluationDate")
            {
                dtpSearchDate.Enabled = true;
                dtpSearchDate.Visible = true;
                txtSearchRequests.Enabled = false;
                txtSearchRequests.Visible = false;

                ApplyDateFilter(dbColumn, dtpSearchDate.Value);
            }
            else if (!string.IsNullOrEmpty(dbColumn))
            {
                txtSearchRequests.Enabled = true;
                txtSearchRequests.Visible = true;
                dtpSearchDate.Enabled = false;
                dtpSearchDate.Visible = false;
            }
        }

        // Reusable translation helper to keep column lookups completely safe from translation errors
        private string GetRealDatabaseColumnName(string uiName)
        {
            // UI FOCUS: The values inside these case quotes must exactly match the items typed into your ComboBox collection dropdown
            switch (uiName)
            {
                // DATABASE FOCUS: The return string values must exactly match the DataPropertyName / DB Column names from your new table query
                case "Name": return "clientName";
                case "Surname": return "clientSurname";
                case "Address": return "siteAddress";
                case "Source": return "requestSource";
                case "Urgency": return "urgencyLevel";
                case "Status": return "status";
                case "DateRecieved": return "dateRecieved";
                case "EvaluationDate": return "siteEvaluationDate";
                default: return null;
            }
        
        }


        // Reusable helper method to keep your background compilation logic clean
        private void ApplyDateFilter(string columnName, DateTime selectedDate)
        {
            // Formats calendar selections cleanly to match standard database storage metrics (YYYY/MM/DD)
            string formattedDate = selectedDate.ToString("yyyy/MM/dd");

            try
            {
                // Parse date values explicitly down to raw string conversions on the dataset layout cache layer
                dataTable2BindingSource.Filter = string.Format("CONVERT({0}, 'System.String') LIKE '%{1}%'", columnName, formattedDate);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Date expression processing exception: " + ex.Message);
            }
        }

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            string currentComboText = cmbSearchColumn.SelectedItem != null ?
                            cmbSearchColumn.SelectedItem.ToString() :
                            cmbSearchColumn.Text.Trim();

            string dbColumn = GetRealDatabaseColumnName(currentComboText);

            // FIXED CONDITIONS: Ensures ongoing calendar changes register correctly
            if (dbColumn == "dateRecieved" || dbColumn == "siteEvaluationDate")
            {
                ApplyDateFilter(dbColumn, dtpSearchDate.Value);
            }
        }

        private void txtSearchRequests_KeyPress(object sender, KeyPressEventArgs e)
        {
            string currentComboText = cmbSearchColumn.Text.Trim();
            string dbColumn = GetRealDatabaseColumnName(currentComboText);

            // If numeric columns are selected, block text characters immediately
            if (dbColumn == "JobRequestID" || dbColumn == "ClientID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; // Drop key stroke input
                }
            }
        }

        private void txtSearchRequests_TextChanged(object sender, EventArgs e)
        {
            string currentComboText = cmbSearchColumn.SelectedItem != null ?
                            cmbSearchColumn.SelectedItem.ToString() :
                            cmbSearchColumn.Text.Trim();

            string userInput = txtSearchRequests.Text.Trim().Replace("'", "''");

            if (string.IsNullOrWhiteSpace(userInput))
            {
                dataTable2BindingSource.Filter = "";
                return;
            }

            string dbColumn = GetRealDatabaseColumnName(currentComboText);

            // FIXED CONDITIONS: Blocks typing rules if either date string is active
            if (string.IsNullOrEmpty(dbColumn) || dbColumn == "dateRecieved" || dbColumn == "siteEvaluationDate")
            {
                return;
            }

            try
            {
                if (dbColumn == "jobRequestID" || dbColumn == "clientID")
                {
                    if (int.TryParse(userInput, out int numericValue))
                    {
                        dataTable2BindingSource.Filter = string.Format("{0} = {1}", dbColumn, numericValue);
                    }
                    else
                    {
                        dataTable2BindingSource.Filter = "1 = 0";
                    }
                }
                else
                {
                    dataTable2BindingSource.Filter = string.Format("{0} LIKE '%{1}%'", dbColumn, userInput);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Text filter exception: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Grab the ID from the selected grid row
                int clickedID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["jobRequestID"].Value);

                // USE THE QUERY BUILDER to fetch the exact row data safely from the DB
                var jobRequestTable = this.jobRequestTableAdapter.GetDataBy2(clickedID);

                if (jobRequestTable.Rows.Count > 0)
                {
                    // Grab the specific typed row from our dataset
                    var selectedJob = jobRequestTable[0];

                    //Populate your UI elements cleanly using the database values
                    jobRequestIDTextBox.Text = selectedJob.jobRequestID.ToString();
                    urgencyLevelTextBox.Text = selectedJob.urgencyLevel;

                    //  NEW ADJUSTED NULL-SAFE VERSION:
                    // Populate your UI elements cleanly using database values (checking for DBNull)
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
                    selectedJobsGridView.Rows.Add("Travel Call-out", currentTravelFee, "Flat Rate", 1.0, currentTravelFee);

                    // 3. Force the DataGridView to completely finish registering the new row layout internally
                    selectedJobsGridView.Refresh();

                    // 4.Run the calculation engine! It sees the travel fee row and locks R255,56 into txtAmount
                    RecalculateGrandTotalFromUI();


                    // Filter the middle grid view to show ONLY the services requested by this client
                    this.jobTypeTableAdapter.FillByID(this.groupWst1DataSet.JobType, clickedID);

                    button3.Enabled = true; // Enable the "Generate Quote" button now that a job request is selected


                    DateTime today = DateTime.Today; //

                    // =================================================================
                    // RELAXED APPROACH: JUST SET SUGGESTED DEFAULTS
                    // =================================================================
                    // Controls stay fully enabled. No MinDate or MaxDate adjustments at all!

                    dateIssuedDateTimePicker.Value = today;
                    dateGeneratedDateTimePicker.Value = today;
                    expiryDateDateTimePicker.Value = today.AddDays(30); // Suggests 30 days, but she can change it!
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
