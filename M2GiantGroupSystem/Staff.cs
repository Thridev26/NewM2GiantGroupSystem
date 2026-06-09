using Microsoft.VisualBasic.ApplicationServices;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class Staff : Form
    {
        int tabIndex;
        public Staff(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
            ApplyPermissions();
        }

        private void ApplyPermissions()
        {
            // Example: Only allow Manager (6) or Admin (5) to see the 'Add Staff' button
            bool canAdd = UserSession.AccessLevel >= 5;
            btnAddStaff.Visible = canAdd;

            // Example: Disable editing for lower levels
            bool canEdit = UserSession.AccessLevel >= 6;
            //txtFirstName.Enabled = canEdit;
            // ... repeat for other fields
        }

        private void LoadRoles()
        {
            string query = "SELECT roleID, roleName FROM Role";
            string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Assign the same data table to both ComboBoxes
                        cmbRoleEdit.DisplayMember = "roleName";
                        cmbRoleEdit.ValueMember = "roleID";
                        cmbRoleEdit.DataSource = dt;

                        cmbRoleAdd.DisplayMember = "roleName";
                        cmbRoleAdd.ValueMember = "roleID";
                        cmbRoleAdd.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No roles found in the database!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading roles: " + ex.Message);
            }
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
        private void Staff_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            // 2. Load Filtered Grid (Restricted view)
            dgvStaffInfo.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);

            // 3. Load Full Grid (Public view)
            staffDataGridView.DataSource = StaffDB.GetAllStaff();
            staffDataGridView.ReadOnly = true; // Security best practice
            // TODO: This line of code loads data into the 'groupWst1DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.groupWst1DataSet.Staff);
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            LoadRoles();            
        }

        private void gbAddStaff_Enter(object sender, EventArgs e)
        {

        }

        private void txtSearchStaff_TextChanged(object sender, EventArgs e)
        {
            // 1. Your updated search query using the partial name match (LIKE)
            string query = "SELECT staffID, firstName, lastName, userName, passwordHash, contactNumber, staffStatus, dailyRate, roleID " +
                           "FROM Staff " +
                           "WHERE firstName LIKE @Search OR lastName LIKE @Search";

            // 2. Define your raw connection string as a plain string variable
            string connString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;";

            // 3. Wrap everything cleanly in using statements to prevent database locks
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // The '%' wildcards look for the typed text anywhere inside the names
                    cmd.Parameters.AddWithValue("@Search", "%" + txtSearchStaff.Text.Trim() + "%");

                    try
                    {
                        con.Open();

                        // 4. Load the filtered data into a data table to refresh your UI
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Change 'dgvStaff' to whatever your DataGridView's actual name is
                        dgvStaffInfo.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            }
        }

        private void ClearAddFields()
        {
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            userNameTextBox.Clear();
            passwordHashTextBox.Clear(); // This will be the NEW password
            contactNumberTextBox.Clear();
            cmbEditStatus.SelectedIndex = -1; // Deselects the combo box
            cmbRoleEdit.SelectedIndex = -1; // Deselects the combo box
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation: Ensure a role is selected
                if (cmbRoleEdit.SelectedValue == null)
                    throw new Exception("Please select a role from the dropdown.");

                // Validation: Ensure staffID is valid
                if (!int.TryParse(staffIDTextBox.Text, out int staffID))
                    throw new Exception("Invalid Staff ID.");

                string passToSend = string.IsNullOrWhiteSpace(passwordHashTextBox.Text) ? null : passwordHashTextBox.Text;
                // Add this check right before you call StaffDB.SaveStaff
                if (cmbEditStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select a status (Active or Inactive) before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method so the crash doesn't happen
                }

                // Now it is safe to convert to string
                string selectedStatus = cmbEditStatus.SelectedItem.ToString();
                StaffDB.SaveStaff(
                    staffID,
                    firstNameTextBox.Text,
                    lastNameTextBox.Text,
                    userNameTextBox.Text,
                    passToSend,
                    contactNumberTextBox.Text,
                    selectedStatus,
                    decimal.Parse(dailyRateTextBox.Text),
                    (int)cmbRoleEdit.SelectedValue,
                    false
                );

                MessageBox.Show("Staff updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAddFields();
                dgvStaffInfo.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearEditFields()
        {
            firstNameTextBox1.Clear();
            lastNameTextBox1.Clear();
            userNameTextBox1.Clear();
            passwordHashTextBox1.Clear(); // This will be the NEW password
            contactNumberTextBox1.Clear();
            cmbAddStatus.SelectedIndex = -1; // Deselects the combo box
            cmbRoleAdd.SelectedIndex = -1; // Deselects the combo box
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(userNameTextBox1.Text) || string.IsNullOrWhiteSpace(passwordHashTextBox1.Text))
                    throw new Exception("Username and Password are required.");

                if (cmbRoleAdd.SelectedValue == null)
                    throw new Exception("Please select a role.");

                // Call Save
                StaffDB.SaveStaff(
                    null,
                    firstNameTextBox1.Text,
                    lastNameTextBox1.Text,
                    userNameTextBox1.Text,
                    passwordHashTextBox1.Text,
                    contactNumberTextBox1.Text,
                    cmbAddStatus.SelectedItem.ToString(),
                    decimal.Parse(dailyRateTextBox.Text),
                    (int)cmbRoleAdd.SelectedValue,
                    true
                );

                MessageBox.Show("New staff member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearEditFields();
                staffDataGridView.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Addition failed: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStaffInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the user clicked a valid data row (not the header)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStaffInfo.Rows[e.RowIndex];

                // Fill the Edit textboxes (using your specific naming convention)
                staffIDTextBox.Text = row.Cells["staffID"].Value.ToString();
                firstNameTextBox.Text = row.Cells["firstName"].Value.ToString();
                lastNameTextBox.Text = row.Cells["lastName"].Value.ToString();
                userNameTextBox.Text = row.Cells["userName"].Value.ToString();
                contactNumberTextBox.Text = row.Cells["contactNumber"].Value.ToString();
                string statusFromGrid = row.Cells["staffStatus"].Value.ToString();

                // This forces the ComboBox to show the item that matches the grid text
                if (cmbEditStatus.Items.Contains(statusFromGrid))
                {
                    cmbEditStatus.SelectedItem = statusFromGrid;
                }
                else
                {
                    cmbEditStatus.SelectedIndex = -1; // Or handle as an error if the status is something unexpected
                }
                dailyRateTextBox.Text = row.Cells["dailyRate"].Value.ToString();

                // Match the role ID in the ComboBox
                cmbRoleEdit.SelectedValue = row.Cells["roleID"].Value;
            }
        }
    }
}
