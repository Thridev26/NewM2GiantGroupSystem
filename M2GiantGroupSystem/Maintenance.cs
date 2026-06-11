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
        public Maintenance(int tabIndex)
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme; // ADD THIS
            tabIndex = tabIndex;
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
            ApplyTheme(); // ADD THIS

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
            if (cboAssetSelection.Items.Count > 0) cboAssetSelection.SelectedIndex = 0;
            if (cboServiceType.Items.Count > 0) cboServiceType.SelectedIndex = 0;
            txtRepairCost.Text = "0.00";
            dtpServiceDate.Value = DateTime.Today;
            rtbCompletionDetails.Clear();
        }

        // --------------------------------------------------------------------------------------------------------
        // ACTION CLICK EVENT INTERFACES
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
                string serviceDate = dtpServiceDate.Value.ToString("yyyy-MM-dd"); // FIX: Use DateTime, not string
                string details = rtbCompletionDetails.Text.Trim();

                decimal cost = 0.00m;
                if (!string.IsNullOrWhiteSpace(txtRepairCost.Text))
                {
                    decimal.TryParse(txtRepairCost.Text, out cost);
                }

                // 3. Commit new log records directly via Dataset TableAdapter
                using (MaintenanceLogTableAdapter logAdapter = new MaintenanceLogTableAdapter())
                {
                    logAdapter.InsertQuery(serviceType, cost, serviceDate, details, selectedAssetID); // Convert DateTime to string
                }

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

        private void button1_Click(object sender, EventArgs e)
        {
            //clear all form fields to default values for a new log entry
            ClearFormLayout();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 1. Get the current page and its bounding rectangle coordinates
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            // 2. Set up the typography and fallback background color (Honeydew)
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Color backColor = Color.Honeydew;

            // 3. THE FIX: If the tab is currently selected, paint it with the signature bright green!
            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            Color textColor = Color.Black;

            // 4. Fill the tab background rectangle canvas
            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            // 5. Draw the neat dark green border lines around the tab shape
            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            // 6. Center and draw the text string cleanly inside the tab boundary
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
    }
}