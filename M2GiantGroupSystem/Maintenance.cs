using M2GiantGroupSystem.GroupWst1DataSetTableAdapters;
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
    public partial class Maintenance : Form
    {
        int tabIndex;
        int selectedLogID = 0;

        public Maintenance(int tabIndex)
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
            this.tabIndex = tabIndex; // FIXED: Corrected assignment rule contexts
        }

        // --------------------------------------------------------------------------------------------------------
        // FORM INITIALIZATION
        // --------------------------------------------------------------------------------------------------------
        private void Maintenance_Load(object sender, EventArgs e)
        {
            // Cleanly delegate all initialization logic to the centralized setup method
            InitializeMaintenanceFormLayout();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;

            // Set tab screen matching configuration context rules on launch
            if (tabControl1.TabPages.Count > tabIndex)
            {
                tabControl1.SelectedIndex = tabIndex;
            }

            ApplyTheme();
        }

        private void InitializeMaintenanceFormLayout()
        {
            try
            {
                // 1. Apply the Giant Group signature grid visual styles
                dgvMaintenanceHistory.ReadOnly = true;
                dgvMaintenanceHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMaintenanceHistory.MultiSelect = false;
                dgvMaintenanceHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMaintenanceHistory.RowHeadersVisible = false;
                dgvMaintenanceHistory.AllowUserToAddRows = false;

                // Match the aesthetic of the Jobs form
                dgvMaintenanceHistory.BackgroundColor = Color.FromArgb(155, 198, 138);
                dgvMaintenanceHistory.DefaultCellStyle.SelectionBackColor = Color.Green;
                dgvMaintenanceHistory.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvMaintenanceHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
                dgvMaintenanceHistory.EnableHeadersVisualStyles = false;

                // Set clean padding dimensions
                dgvMaintenanceHistory.RowTemplate.Height = 32;
                dgvMaintenanceHistory.ColumnHeadersHeight = 40;

                // 2. Populate Service Type Dropdown menu options
                cboServiceType.Items.Clear();
                cboServiceType.Items.AddRange(new string[] { "Inspection", "Routine Maintenance", "Emergency Repair", "Part Replacement" });
                cboServiceType.SelectedIndex = 0;

                // 3. Fetch active database asset profiles for your selection dropdown
                OwnedAssetTableAdapter assetAdapter = new OwnedAssetTableAdapter();
                GroupWst1DataSet.OwnedAssetDataTable assetTable = assetAdapter.GetData();

                DataTable displayTable = new DataTable();
                displayTable.Columns.Add("assetID", typeof(int));
                displayTable.Columns.Add("DisplayText", typeof(string));

                foreach (var row in assetTable)
                {
                    DataRow newRow = displayTable.NewRow();
                    newRow["assetID"] = row.assetID;
                    newRow["DisplayText"] = $"{row.type} (S/N: {row.serialNumber})"; // e.g., "Truck (S/N: SN12345)"
                    displayTable.Rows.Add(newRow);
                }

                cboAssetSelection.DataSource = displayTable;
                cboAssetSelection.ValueMember = "assetID";
                cboAssetSelection.DisplayMember = "DisplayText";

                // 4. Fill history logs data grid rows
                RefreshMaintenanceGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing maintenance layout: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --------------------------------------------------------------------------------------------------------
        // DATA PROCESSING UTILITIES
        // --------------------------------------------------------------------------------------------------------
        private void RefreshMaintenanceGrid()
        {
            using (MaintenanceLogTableAdapter logAdapter = new MaintenanceLogTableAdapter())
            {
                dgvMaintenanceHistory.DataSource = logAdapter.GetData();
            }

            // Hide structural data system IDs
            if (dgvMaintenanceHistory.Columns["logID"] != null)
                dgvMaintenanceHistory.Columns["logID"].Visible = false;

            if (dgvMaintenanceHistory.Columns["assetID"] != null)
                dgvMaintenanceHistory.Columns["assetID"].Visible = false;

            // Presentation human-readable header transformations
            if (dgvMaintenanceHistory.Columns["serviceType"] != null)
                dgvMaintenanceHistory.Columns["serviceType"].HeaderText = "Service Type";

            if (dgvMaintenanceHistory.Columns["repairCost"] != null)
                dgvMaintenanceHistory.Columns["repairCost"].HeaderText = "Repair Cost";

            if (dgvMaintenanceHistory.Columns["serviceDate"] != null)
                dgvMaintenanceHistory.Columns["serviceDate"].HeaderText = "Service Date";

            if (dgvMaintenanceHistory.Columns["completionDetails"] != null)
                dgvMaintenanceHistory.Columns["completionDetails"].HeaderText = "Completion Details";
        }

        private void ClearFormLayout()
        {
            selectedLogID = 0; // Reset active reference locks
            if (cboAssetSelection.Items.Count > 0) cboAssetSelection.SelectedIndex = 0;
            if (cboServiceType.Items.Count > 0) cboServiceType.SelectedIndex = 0;
            txtRepairCost.Text = "0.00";
            dtpServiceDate.Value = DateTime.Today;
            rtbCompletionDetails.Clear();
            dgvMaintenanceHistory.ClearSelection();
        }

        // --------------------------------------------------------------------------------------------------------
        // ACTION CLICK & ROW SELECTION HANDLERS
        // --------------------------------------------------------------------------------------------------------
       
        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            // 1. Defensive input validation routines
            if (cboAssetSelection.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid asset being logged.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbCompletionDetails.Text))
            {
                MessageBox.Show("Please fill out description details for the completion log task notes.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Safely capture input properties from active layout elements
                int selectedAssetID = Convert.ToInt32(cboAssetSelection.SelectedValue);
                string serviceType = cboServiceType.SelectedItem.ToString();
                string serviceDate = dtpServiceDate.Value.ToString("yyyy-MM-dd");
                string details = rtbCompletionDetails.Text.Trim();

                decimal cost = 0.00m;
                if (!string.IsNullOrWhiteSpace(txtRepairCost.Text))
                {
                    decimal.TryParse(txtRepairCost.Text, out cost);
                }

                // 3. Commit new log records directly via Dataset TableAdapter component logic
                this.maintenanceLogTableAdapter1.InsertQuery(serviceType, cost, serviceDate, details, selectedAssetID);

                MessageBox.Show("Maintenance activity successfully logged for this asset entry!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Force synchronization visual grid state refreshes
                RefreshMaintenanceGrid();
                ClearFormLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to record maintenance logs entry mapping profiles: " + ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 1. Guard check selection constraints
            if (selectedLogID == 0)
            {
                MessageBox.Show("Please select a historical maintenance row entry from the grid first to update.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboAssetSelection.SelectedValue == null)
            {
                MessageBox.Show("Please ensure an asset is selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbCompletionDetails.Text))
            {
                MessageBox.Show("Please provide detailed task notes inside completion details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to save modifications to Maintenance Log ID: {selectedLogID}?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                // 2. Parse field items cleanly
                string type = cboServiceType.SelectedItem?.ToString() ?? "Inspection";
                string dateStr = dtpServiceDate.Value.ToString("yyyy-MM-dd");
                string noteDetails = rtbCompletionDetails.Text.Trim();
                int assetID = Convert.ToInt32(cboAssetSelection.SelectedValue);

                decimal cost = 0.00m;
                if (!string.IsNullOrWhiteSpace(txtRepairCost.Text))
                {
                    decimal.TryParse(txtRepairCost.Text, out cost);
                }

                // 3. Execute update statement via your designer-dragged visual TableAdapter query module!
                this.maintenanceLogTableAdapter1.UpdateQuery(
                    type,
                    cost,
                    dateStr,
                    noteDetails,
                    assetID,
                    selectedLogID
                );

                MessageBox.Show($"Maintenance Log entry {selectedLogID} successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Force grid updates and reset inputs
                RefreshMaintenanceGrid();
                ClearFormLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes onto the database engine: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear all form fields to default values for a new log entry
            ClearFormLayout();
        }

        // --------------------------------------------------------------------------------------------------------
        // THEME AND STYLE OWNER RENDERING ROUTINES
        // --------------------------------------------------------------------------------------------------------
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Color backColor = Color.Honeydew;

            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            Color textColor = Color.Black;

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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.ThemeChanged -= ApplyTheme;
            base.OnFormClosed(e);
        }

        private void ApplyTheme()
        {
            if (ThemeManager.IsDarkMode)
                ThemeManager.ApplyTheme(this);
        }

        private void dgvMaintenanceHistory_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Prevent crash if they click the header row or empty canvas areas
                if (e.RowIndex < 0 || dgvMaintenanceHistory.Rows[e.RowIndex].Cells["logID"].Value == DBNull.Value || dgvMaintenanceHistory.Rows[e.RowIndex].Cells["logID"].Value == null)
                    return;

                DataGridViewRow row = dgvMaintenanceHistory.Rows[e.RowIndex];

                // 1. Extract structural tracking IDs
                selectedLogID = Convert.ToInt32(row.Cells["logID"].Value);
                int associatedAssetID = Convert.ToInt32(row.Cells["assetID"].Value);

                // 2. Map date parameters safely
                if (row.Cells["serviceDate"].Value != DBNull.Value && row.Cells["serviceDate"].Value != null)
                {
                    dtpServiceDate.Value = Convert.ToDateTime(row.Cells["serviceDate"].Value);
                }

                // 3. Select matching service string in the combo box list
                string serviceTypeVal = row.Cells["serviceType"].Value?.ToString();
                if (cboServiceType.Items.Contains(serviceTypeVal))
                {
                    cboServiceType.SelectedItem = serviceTypeVal;
                }

                // 4. Map selection combo box back using index keys
                cboAssetSelection.SelectedValue = associatedAssetID;

                // 5. Format costs safely to decimal text ranges
                decimal cost = row.Cells["repairCost"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["repairCost"].Value) : 0.00m;
                txtRepairCost.Text = cost.ToString("F2");

                // 6. Complete description details mapping profiles
                rtbCompletionDetails.Text = row.Cells["completionDetails"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching selected maintenance record row: {ex.Message}", "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}