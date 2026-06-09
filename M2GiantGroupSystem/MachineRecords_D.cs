using iText.Signatures.Validation.Lotl;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.Signatures.Validation.Lotl.CountrySpecificLotlFetcher;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace M2GiantGroupSystem
{
    public partial class MachineRecords_D : Form
    {
        private string selectedHiredStatus = "";
        private List<int> serviceDueAssets = new List<int>();
        public MachineRecords_D()
        {
            InitializeComponent();
          //  ThemeManager.ThemeChanged += ApplyTheme;
            private void ApplyPermissions()
        {
            int level = UserSession.AccessLevel; // The user Session object is global so this will work 

            // 1. If Owner (6), they already have full access.  
            // We just return early and don't change anything! 
            if (level >= 6) return;

            // 2. If we reach this point, the user is NOT an owner. 
            // Now we apply restrictions for everyone else. 
            switch (level)
            {
                case 5: // Admin: Some locks 
                    Panel1.Enabled = false;
                    panel2.Enabled = false;
                    panel3.Enabled = false;
                    panel4.Enabled = false;
                    break;

                case 4: // Ops Manager: More locks 
                    Panel1.Enabled = false;
                    panel2.Enabled = false;
                    panel3.Enabled = false;
                    panel4.Enabled = false;
                    break;

                default: // Level 3 and below: Complete lockdown – lock all controls if you feel they should not have access
                    Panel1.Enabled = false;
                    panel2.Enabled = false;
                    panel3.Enabled = false;
                    panel4.Enabled = false;
                    break;
            }
        } 
        }

        private void MachineRecords_D_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupWst1DataSet1.OwnedAsset' table. You can move, or remove it, as needed.
            this.ownedAssetTableAdapter1.Fill(this.groupWst1DataSet1.OwnedAsset);
            // TODO: This line of code loads data into the 'groupWst1DataSet1.HiredAsset' table. You can move, or remove it, as needed.
            this.hiredAssetTableAdapter1.Fill(this.groupWst1DataSet1.HiredAsset);
            // TODO: This line of code loads data into the 'groupWst1DataSet.HiredAsset' table. You can move, or remove it, as needed.
            this.hiredAssetTableAdapter1.Fill(this.groupWst1DataSet1.HiredAsset);
            // TODO: This line of code loads data into the 'groupWst1DataSet.OwnedAsset' table. You can move, or remove it, as needed.
            this.ownedAssetTableAdapter1.Fill(this.groupWst1DataSet1.OwnedAsset);
            tabControl1.SelectedIndex = TabIndex;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            //ApplyTheme();


        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            //Base colours (non-selected tabs)
            Color backColor = Color.Honeydew;


            //Highlight ONLY selected tab
            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            // Text always forest green (or you can change for selected if needed)
            Color textColor = Color.Black;

            // Fill background
            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            //BORDER 
            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            // Draw text
            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // adds row from datagridview to textboxes and comboboxes for owned asset

            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dgvOwnedAsset_D.Rows[e.RowIndex];
                txtAssetID_D.Text = row.Cells[0].Value?.ToString().Trim() ?? "";

                txtAsSnumber_d.Text = row.Cells[1].Value?.ToString().Trim() ?? "";

                cmbType_D.Text = row.Cells[2].Value?.ToString().Trim() ?? "";
                cmbType_D.SelectedIndex = cmbType_D.FindStringExact(row.Cells[2].Value?.ToString().Trim() ?? "");

                dtPurchaseDate_D.Value = Convert.ToDateTime(row.Cells[3].Value);

                string conditionValue = row.Cells[4].Value?.ToString().Trim() ?? "";
                foreach (var item in cmbCondit_D.Items)
                {
                    if (item.ToString().Trim() == conditionValue)
                    {
                        cmbCondit_D.SelectedItem = item;
                        break;
                    }
                }

                dtServiceDate_D.Value = Convert.ToDateTime(row.Cells[5].Value);

                string statusValue = row.Cells[6].Value?.ToString().Trim() ?? "";
                foreach (var item in cmbStatus_D.Items)
                {
                    if (item.ToString().Trim() == statusValue)
                    {
                        cmbStatus_D.SelectedItem = item;
                        break;
                    }
                    ;
                }
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvOwnedAsset_D.Rows[e.RowIndex];
            string status = row.Cells[6].Value?.ToString().Trim() ?? "";

            switch (status)
            {
                case "Available":
                    row.Cells[6].Style.BackColor = Color.LightGreen;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                case "In Use":
                    row.Cells[6].Style.BackColor = Color.LightCoral;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                case "Under Maintenance":
                    row.Cells[6].Style.BackColor = Color.LightYellow;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                default:
                    row.Cells[6].Style.BackColor = Color.White;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;

                    if (row.Cells[0].Value != null)
                    {
                        int assetID = Convert.ToInt32(row.Cells[0].Value);
                        if (serviceDueAssets.Contains(assetID))
                        {
                            row.Cells[5].Style.BackColor = Color.Yellow;
                            row.Cells[5].Style.ForeColor = Color.Black;
                        }
                    }

            }
        }

        private void txtSearchOA_D_TextChanged(object sender, EventArgs e)
        {
            ownedAssetTableAdapter1.FillBySerialNumber(groupWst1DataSet1.OwnedAsset, txtSearchOA_D.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                txtAssetID_D.Text = "";

                txtAsSnumber_d.Text = "";

                cmbType_D.SelectedIndex = -1;

                cmbType_D.Text = "";

                dtPurchaseDate_D.Value = DateTime.Now;

                cmbCondit_D.SelectedIndex = -1;
                cmbCondit_D.Text = "";

                dtServiceDate_D.Value = DateTime.Now;

                cmbStatus_D.SelectedIndex = -1;
                cmbStatus_D.Text = "";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtAssetID_D_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddAsset_D_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtSerialno2_D.Text))
            {
                MessageBox.Show("Please enter a Serial Number.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbType2_D.Text))
            {
                MessageBox.Show("Please select an Asset Type.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCondition2_D.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Current Condition.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus2_D.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are the following Asset Details Correct?\n" + "\nSerial Number : " + txtSerialno2_D.Text +
                      "\nAsset Type: " + cmbType2_D.Text + "\nPurchase Date: "
                           + dtPurchaseD2_D.Value.ToShortDateString() + "\nCurrent Condition: " +
                               cmbCondition2_D.SelectedItem.ToString() + "\nNext Service Date: " + dtService2_D.Value.ToShortDateString() +
                                 "\nStatus: " + cmbStatus2_D.SelectedItem.ToString(),
                                      "Confirm Asset Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ownedAssetTableAdapter1.InsertNewOwnedAsset(
                        txtSerialno2_D.Text,
                        cmbType2_D.Text,
                        dtPurchaseD2_D.Value.ToShortDateString(),
                        cmbCondition2_D.SelectedItem.ToString(),
                        dtService2_D.Value.ToShortDateString(),
                        cmbStatus2_D.SelectedItem.ToString()
                    );
                    ownedAssetTableAdapter1.Fill(groupWst1DataSet1.OwnedAsset);

                    AutoCompleteStringCollection serialNumbers = new AutoCompleteStringCollection();
                    foreach (DataRow row in groupWst1DataSet1.OwnedAsset.Rows)
                    {
                        serialNumbers.Add(row["serialNumber"].ToString().Trim());
                    }
                    txtDeleteSN.AutoCompleteCustomSource = serialNumbers;


                    dgvOwnedAsset_D.Refresh();
                    MessageBox.Show("New asset added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding new asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Please edit the details where needed and try again.", "Edit Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void cmbType2_D_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MachineRecords_D_Shown(object sender, EventArgs e)
        {
            cmbType2_D.SelectedIndex = -1;
            cmbCondition2_D.SelectedIndex = -1;
            cmbStatus2_D.SelectedIndex = -1;

            cmbType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;


            AutoCompleteStringCollection serialNumbers = new AutoCompleteStringCollection();
            foreach (DataRow row in groupWst1DataSet1.OwnedAsset.Rows)
            {
                serialNumbers.Add(row["serialNumber"].ToString().Trim());
            }
            txtDeleteSN.AutoCompleteCustomSource = serialNumbers;

            string warningMessage = "";

            foreach (DataRow row in groupWst1DataSet1.OwnedAsset.Rows)
            {
                DateTime nextService = Convert.ToDateTime(row["nextServiceDate"]);
                int daysUntilService = (nextService - DateTime.Now).Days;

                if (daysUntilService <= 30)
                {
                    warningMessage += "\nSerial Number: " + row["serialNumber"].ToString().Trim() +
                                      " — " + daysUntilService + " days remaining";
                }
            }

            if (!string.IsNullOrEmpty(warningMessage))
            {
                MessageBox.Show("The following assets have a service date within 30 days:\n" + warningMessage,
                                "Service Date Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                foreach (DataGridViewRow dgvRow in dgvOwnedAsset_D.Rows)
                {
                    if (dgvRow.Cells[5].Value != null)
                    {
                        DateTime nextService = Convert.ToDateTime(dgvRow.Cells[5].Value);
                        int daysUntilService = (nextService - DateTime.Now).Days;

                        if (daysUntilService <= 30)
                        {
                            dgvRow.Cells[5].Style.BackColor = Color.Yellow;
                            dgvRow.Cells[5].Style.ForeColor = Color.Black;
                        }
                    }
                }

            }
            AutoCompleteStringCollection supplierNames = new AutoCompleteStringCollection();
            foreach (DataRow row in groupWst1DataSet1.HiredAsset.Rows)
            {
                supplierNames.Add(row["supplierName"].ToString().Trim());
            }
            textBox2.AutoCompleteCustomSource = supplierNames;

            foreach (DataRow row in groupWst1DataSet1.OwnedAsset.Rows)
            {
                DateTime nextService = Convert.ToDateTime(row["nextServiceDate"]);
                int daysUntilService = (nextService - DateTime.Now).Days;

                if (daysUntilService <= 30)
                {
                    serviceDueAssets.Add(Convert.ToInt32(row["assetID"]));
                    warningMessage += "\nSerial Number: " + row["serialNumber"].ToString().Trim() +
                                      " — " + daysUntilService + " days remaining";
                }
            }
        }

        // private void btnUpdateAass_D_Click(object sender, EventArgs e)

        private void btnUpdateAsset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                   "Are the following Asset Details Correct?\n" +
                 "\nSerial Number: " + txtAsSnumber_d.Text +
                 "\nAsset Type: " + cmbType_D.Text +
                  "\nPurchase Date: " + dtPurchaseDate_D.Value.ToShortDateString() +
                        "\nCurrent Condition: " + cmbCondit_D.Text +
                  "\nNext Service Date: " + dtServiceDate_D.Value.ToShortDateString() +
                       "\nStatus: " + cmbStatus_D.Text,
                         "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ownedAssetTableAdapter1.UpdateOwnedAsset(
                        txtAsSnumber_d.Text,
                        cmbType_D.Text.ToString(),
                        dtPurchaseDate_D.Value.ToShortDateString(),
                        cmbCondit_D.SelectedItem.ToString(),
                        dtServiceDate_D.Value.ToShortDateString(),
                        cmbStatus_D.SelectedItem.ToString(),
                        int.Parse(txtAssetID_D.Text)
                    );


                    // Refresh the DataGridView

                    ownedAssetTableAdapter1.Fill(groupWst1DataSet1.OwnedAsset);

                    MessageBox.Show("Asset updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);




                }
            }
        }

        private void btnClearNewAsset_d_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {


                txtSerialno2_D.Text = "";

                cmbType2_D.SelectedIndex = -1;

                cmbType_D.Text = "";

                dtPurchaseD2_D.Value = DateTime.Now;

                cmbCondition2_D.SelectedIndex = -1;
                cmbCondition2_D.Text = "";

                dtService2_D.Value = DateTime.Now;

                cmbStatus2_D.SelectedIndex = -1;
                cmbStatus2_D.Text = "";
            }
        }

        private void txtDeleteSN_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeleteSN.Text))
            {
                cmbDeleteAT.Text = "";
                dtDeletP.Value = DateTime.Now;
                cmbDeleteCond.SelectedIndex = -1;
                dtDeleteNS.Value = DateTime.Now;
                cmbDeleteST.SelectedIndex = -1;
                return;
            }

            foreach (DataRow row in groupWst1DataSet1.OwnedAsset.Rows)
            {
                if (row["serialNumber"].ToString().Trim().StartsWith(txtDeleteSN.Text.Trim()))
                {
                    cmbDeleteAT.Text = row["type"].ToString().Trim();
                    txtAssetID2.Text = row["assetID"].ToString();
                    dtDeletP.Value = Convert.ToDateTime(row["purchaseDate"]);

                    foreach (var item in cmbDeleteCond.Items)
                    {
                        if (item.ToString().Trim() == row["currentCondition"].ToString().Trim())
                        {
                            cmbDeleteCond.SelectedItem = item;
                            break;
                        }
                    }

                    dtDeleteNS.Value = Convert.ToDateTime(row["nextServiceDate"]);

                    foreach (var item in cmbDeleteST.Items)
                    {
                        if (item.ToString().Trim() == row["assetStatus"].ToString().Trim())
                        {
                            cmbDeleteST.SelectedItem = item;
                            break;
                        }
                    }

                    break;
                }
            }
        }

        private void btnDeleteRcd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                      "Are you sure you want to delete this asset?\n" +
                               "\nSerial Number: " + txtDeleteSN.Text +
                                    "\nAsset Type: " + cmbDeleteAT.Text +
                                          "\nStatus: " + cmbDeleteST.Text,
                                              "Confirm Delete",
                                                     MessageBoxButtons.YesNo,
                                                                      MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ownedAssetTableAdapter1.DeleteAssetRecord(int.Parse(txtAssetID2.Text));

                    // Refresh the DataGridView
                    ownedAssetTableAdapter1.Fill(this.groupWst1DataSet1.OwnedAsset);

                    // Clear the fields
                    txtDeleteSN.Text = "";

                    MessageBox.Show("Asset deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btncleareDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {


                txtSerialno2_D.Text = "";

                cmbType2_D.SelectedIndex = -1;

                cmbType_D.Text = "";

                dtPurchaseD2_D.Value = DateTime.Now;

                cmbCondition2_D.SelectedIndex = -1;
                cmbCondition2_D.Text = "";

                dtService2_D.Value = DateTime.Now;

                cmbStatus2_D.SelectedIndex = -1;
                cmbStatus2_D.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }







        // HIRED ASSETS -----------------------------------------------------------------------------------------
        private void txtHAsearch_D_TextChanged(object sender, EventArgs e)
        {
            hiredAssetTableAdapter1.FillBySupplierName(groupWst1DataSet1.HiredAsset, txtHAsearch_D.Text);
        }

        private void dgHiredAsset_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgHiredAsset.Rows[e.RowIndex];

            txtHassetID.Text = row.Cells[0].Value?.ToString().Trim() ?? "";
            txtSupplierName.Text = row.Cells[1].Value?.ToString().Trim() ?? "";
            dtHiredate.Value = Convert.ToDateTime(row.Cells[2].Value);

            dtReturn.Value = row.Cells[3].Value == DBNull.Value || row.Cells[3].Value == null
                ? DateTime.Now
                : Convert.ToDateTime(row.Cells[3].Value);

            txtCostHire.Text = row.Cells[4].Value?.ToString().Trim() ?? "";

            string conditionValue = row.Cells[5].Value?.ToString().Trim() ?? "";
            foreach (var item in cmbType.Items)
            {
                if (item.ToString().Trim() == conditionValue)
                {
                    cmbType.SelectedItem = item;
                    break;
                }
            }

            selectedHiredStatus = row.Cells[6].Value?.ToString().Trim() ?? "";
            foreach (var statusItem in cmbStatus.Items)
            {
                if (statusItem.ToString().Trim() == selectedHiredStatus)
                {
                    cmbStatus.SelectedItem = statusItem;
                    break;
                }
            }
        }









        private void dgHiredAsset_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgHiredAsset.Rows[e.RowIndex];
            string status = row.Cells[6].Value?.ToString().Trim() ?? "";

            switch (status)
            {
                case "Active":
                    row.Cells[6].Style.BackColor = Color.LightGreen;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                case "Returned":
                    row.Cells[6].Style.BackColor = Color.LightCoral;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                case "Overdue":
                    row.Cells[6].Style.BackColor = Color.Orange;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                case "Damaged":
                    row.Cells[6].Style.BackColor = Color.LightYellow;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
                default:
                    row.Cells[6].Style.BackColor = Color.White;
                    row.Cells[6].Style.ForeColor = Color.Black;
                    break;
            }
        }


        private void btnHupdate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Are you sure you want to update this asset?",
        "Confirm Update",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    hiredAssetTableAdapter1.UpdateHiredAsset(
                        txtSupplierName.Text,
                        dtHiredate.Value.ToShortDateString(),
                        dtReturn.Value.ToShortDateString(),
                        decimal.Parse(txtCostHire.Text),
                        cmbType.Text,
                        selectedHiredStatus,
                        int.Parse(txtHassetID.Text)
                    );

                    hiredAssetTableAdapter1.Fill(groupWst1DataSet1.HiredAsset);

                    MessageBox.Show("Asset updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHclear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {


                txtHassetID.Text = "";

                txtSupplierName.Text = "";

                dtReturn.Value = DateTime.Now;

                dtReturn.Value = DateTime.Now;

                txtCostHire.Text = "";

                cmbType.SelectedIndex = -1;
                cmbType_D.Text = "";

                cmbStatus.SelectedIndex = -1;
                cmbStatus.Text = "";
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSName2.Text))
            {
                MessageBox.Show("Please enter a Supplier Name.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCost2.Text))
            {
                MessageBox.Show("Please enter a Hire Cost.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtCost2.Text, out _))
            {
                MessageBox.Show("Please enter a valid number for Hire Cost.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbHassetTY.Text))
            {
                MessageBox.Show("Please select an Equipment Type.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus3.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Status.", "Missing Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show(
              "Are the following Asset Details Correct?\n" +
           "\nSupplier Name: " + txtSName2.Text +
             "\nHire Date: " + dtHiRe2.Value.ToShortDateString() +
              "\nReturn Date: " + dtHiRe2.Value.ToShortDateString() +
                  "\nHire Cost: " + txtCost2.Text +
                "\nEquipment Type: " + cmbHassetTY.Text +
               "\nStatus: " + cmbStatus3.Text,
           "Confirm Asset Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    hiredAssetTableAdapter1.InsertHiredAsset(
                        txtSName2.Text,
                        dtHiRe2.Value.ToShortDateString(),
                        dtReturn2.Value.ToShortDateString(),
                        decimal.Parse(txtCost2.Text),
                        cmbHassetTY.Text,
                        cmbStatus3.Text
                    );

                    hiredAssetTableAdapter1.Fill(groupWst1DataSet1.HiredAsset);
                    AutoCompleteStringCollection supplierNames = new AutoCompleteStringCollection();
                    foreach (DataRow row in groupWst1DataSet1.HiredAsset.Rows)
                    {
                        supplierNames.Add(row["supplierName"].ToString().Trim());
                    }
                    textBox2.AutoCompleteCustomSource = supplierNames;

                    MessageBox.Show("Asset added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please edit the details where needed and try again.", "Edit Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnclear2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                txtSName2.Text = "";
                dtHiRe2.Value = DateTime.Now;
                dtReturn2.Value = DateTime.Now;
                txtCost2.Text = "";
                cmbHassetTY.SelectedIndex = -1;
                cmbStatus3.SelectedIndex = -1;
            }
        }

        private void btnDeleteHiredA_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this asset?\n" +
                    "\nSupplier Name: " + textBox2.Text +
                  "\nAsset Type: " + comboBox2.Text +
                   "\nStatus: " + comboBox1.Text,
              "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    hiredAssetTableAdapter1.DeleteHiredAsset(int.Parse(txtHassetID.Text));

                    // Refresh the DataGridView
                    hiredAssetTableAdapter1.Fill(this.groupWst1DataSet1.HiredAsset);

                    // Clear the fields
                    textBox2.Text = "";

                    MessageBox.Show("Asset deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting asset: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                comboBox2.SelectedIndex = -1;
                dateTimePicker2.Value = DateTime.Now;
                dateTimePicker1.Value = DateTime.Now;
                textBox1.Text = "";
                comboBox1.SelectedIndex = -1;
                return;
            }

            foreach (DataRow row in groupWst1DataSet1.HiredAsset.Rows)
            {
                if (row["supplierName"].ToString().Trim().StartsWith(textBox2.Text.Trim()))
                {
                    txtHassetID.Text = row["hiredAssetID"].ToString();
                    dateTimePicker2.Value = Convert.ToDateTime(row["hireDate"]);

                    if (row["returnDate"] != DBNull.Value)
                        dateTimePicker1.Value = Convert.ToDateTime(row["returnDate"]);

                    textBox1.Text = row["hireCost"].ToString().Trim();

                    comboBox2.Text = row["equipmentType"].ToString().Trim();
                    foreach (var item in comboBox1.Items)
                    {
                        if (item.ToString().Trim() == row["hiredAssetStatus"].ToString().Trim())
                        {
                            comboBox1.SelectedItem = item;
                            break;
                        }
                    }

                    break;
                }
            }
        }

        private void btnClearAssedDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to clear all fields?", "Confirm Clear",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                textBox2.Text = "";
                dateTimePicker2.Value = DateTime.Now;
                dateTimePicker1.Value = DateTime.Now;
                textBox1.Text = "";
                comboBox2.SelectedIndex = -1;
                comboBox1.SelectedIndex = -1;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            string warningMessage = "";

            foreach (DataRow row in groupWst1DataSet1.HiredAsset.Rows)
            {
                if (row["returnDate"] != DBNull.Value)
                {
                    DateTime returnDate = Convert.ToDateTime(row["returnDate"]);
                    int daysUntilReturn = (returnDate - DateTime.Now).Days;

                    if (daysUntilReturn <= 7)
                    {
                        warningMessage += "\nSupplier: " + row["supplierName"].ToString().Trim() +
                                          " — " + daysUntilReturn + " days until return";
                    }
                }
            }

            if (!string.IsNullOrEmpty(warningMessage))
            {
                MessageBox.Show("The following assets are due for return within 7 days:\n" + warningMessage,
                                "Return Date Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                foreach (DataGridViewRow dgvRow in dgHiredAsset.Rows)
                {
                    if (dgvRow.Cells[3].Value != null && dgvRow.Cells[3].Value != DBNull.Value)
                    {
                        DateTime returnDate = Convert.ToDateTime(dgvRow.Cells[3].Value);
                        int daysUntilReturn = (returnDate - DateTime.Now).Days;

                        if (daysUntilReturn <= 7)
                        {
                            dgvRow.Cells[3].Style.BackColor = Color.Yellow;
                            dgvRow.Cells[3].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private void txtDeleteSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the same search as TextChanged
                txtDeleteSN_TextChanged(sender, e);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the same search as TextChanged
                textBox2_TextChanged(sender, e);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                string warningMessage = "";

                foreach (DataRow row in groupWst1DataSet1.HiredAsset.Rows)
                {
                    if (row["returnDate"] != DBNull.Value)
                    {
                        DateTime returnDate = Convert.ToDateTime(row["returnDate"]);
                        int daysUntilReturn = (returnDate - DateTime.Now).Days;

                        if (daysUntilReturn <= 7)
                        {
                            warningMessage += "\nSupplier: " + row["supplierName"].ToString().Trim() +
                                              " — " + daysUntilReturn + " days until return";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(warningMessage))
                {
                    MessageBox.Show("The following assets are due for return within 7 days:\n" + warningMessage,
                                    "Return Date Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    foreach (DataGridViewRow dgvRow in dgHiredAsset.Rows)
                    {
                        if (dgvRow.Cells[3].Value != null && dgvRow.Cells[3].Value != DBNull.Value)
                        {
                            DateTime returnDate = Convert.ToDateTime(dgvRow.Cells[3].Value);
                            int daysUntilReturn = (returnDate - DateTime.Now).Days;

                            if (daysUntilReturn <= 7)
                            {
                                dgvRow.Cells[3].Style.BackColor = Color.Yellow;
                                dgvRow.Cells[3].Style.ForeColor = Color.Black;
                            }
                        }
                    }
                }
        //    }protected override void OnFormClosed(FormClosedEventArgs e)
        //{
        //    ThemeManager.ThemeChanged -= ApplyTheme;
        //    base.OnFormClosed(e);
        //}
        //private void ApplyTheme()
        //{
        //    if (ThemeManager.IsDarkMode)
        //        ThemeManager.ApplyTheme(this);
        //}

    }
}
}

















