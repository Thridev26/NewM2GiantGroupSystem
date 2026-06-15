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
using System.Globalization;
using System.Text.RegularExpressions;

namespace M2GiantGroupSystem
{
    public partial class Staff : Form
    {
        int tabIndex;
        public Staff(int tab_index)
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
            tabIndex = tab_index;
            ApplyPermissions();
        }
        string _connectionString = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;" +
                                 "Persist Security Info=True;User ID=GroupWst1;Password=dtf39;" +
                                 "Encrypt=True;TrustServerCertificate=True";

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
                    btnAddStaff.Enabled = false; // Admins can edit but not add new staff
                    dailyRateTextBox.ReadOnly = true; // Admins can edit staff but not change their daily rate
                    cmbEditStatus.Enabled = false; // Admins can edit staff but not change their active/inactive status
                    break;

                case 4: // Ops Manager: More locks
                    btnSave.Enabled = true;
                    btnAddStaff.Enabled = false;                    
                    cmbRoleEdit.Enabled = false;
                    dailyRateTextBox.ReadOnly = true; // Ops Managers can view but not change daily rates
                    cmbEditStatus.Enabled = false; // Ops Managers can view but not change their active/inactive status
                    if (tabControl1.TabPages.Contains(tabPage3))
                    {
                        tabControl1.TabPages.Remove(tabPage3);
                    }
                    tabControl1.Refresh(); // Refresh the tab control to reflect changes immediately
                    break;

                default: // Level 3 and below: Complete lockdown – lock all controls if you feel they should not have access
                    btnSave.Enabled = true;
                    btnAddStaff.Enabled = false;                    
                    cmbRoleEdit.Enabled = false;
                    dailyRateTextBox.ReadOnly = true;
                    cmbEditStatus.Enabled = false; // Regular staff can view but not change their active/inactive status
                    if (tabControl1.TabPages.Contains(tabPage3))
                    {
                        tabControl1.TabPages.Remove(tabPage3);
                    }
                    tabControl1.Refresh(); // Refresh the tab control to reflect changes immediately
                    break;
            
            }

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

        private bool IsUsernameTaken(string username)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;";
            string query = "SELECT COUNT(*) FROM Staff WHERE username = @Username";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the parameter to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@Username", username);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Returns true if username exists
                }
            }
        }

        private bool DoesValueExist(string columnName, string value)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;";
            // We use string interpolation carefully here for the column name
            string query = $"SELECT COUNT(*) FROM Staff WHERE {columnName} = @Value";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Value", value);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
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
            ApplyTheme();
            dgvStaffInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStaffInfo.DefaultCellStyle.SelectionBackColor = Color.Green;
            staffDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            staffDataGridView.DefaultCellStyle.SelectionBackColor = Color.Green;
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
            LoadJobStaffAssignments();
            dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStaff.DefaultCellStyle.SelectionBackColor = Color.Green;
            btnSaveHours.Enabled = false;

            label5.Text = "Save button will only be enabled\nwhen a staff is selected";
            label6.Text = "Only the 'Hours Worked' column is editable.\nAll other columns are locked for security.\nDouble click the hours worked column to edit it.";


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
            staffIDTextBox1.Clear();
            dailyRateTextBox1.Clear();
            firstNameTextBox1.Clear();
            lastNameTextBox1.Clear();
            userNameTextBox1.Clear();
            passwordHashTextBox1.Clear(); // This will be the NEW password
            contactNumberTextBox1.Clear();
            cmbAddStatus.SelectedIndex = -1; // Deselects the combo box
            cmbRoleAdd.SelectedIndex = -1; // Deselects the combo box
            emailAddressTextBox1.Clear(); // Add this
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
                    emailAddressTextBox.Text,
                    selectedStatus,
                    decimal.Parse(dailyRateTextBox.Text),
                    (int)cmbRoleEdit.SelectedValue,
                    false
                );

                MessageBox.Show("Staff updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearEditFields();
                // Refresh BOTH grids
                dgvStaffInfo.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);
                staffDataGridView.DataSource = StaffDB.GetAllStaff();               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearEditFields()
        {
            staffIDTextBox.Clear();
            dailyRateTextBox.Clear();
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            userNameTextBox.Clear();
            passwordHashTextBox.Clear(); // This will be the NEW password
            contactNumberTextBox.Clear();
            cmbEditStatus.SelectedIndex = -1; // Deselects the combo box
            cmbRoleEdit.SelectedIndex = -1; // Deselects the combo box
            emailAddressTextBox.Clear(); // Add this
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

                string rawInput = dailyRateTextBox1.Text.Trim();
                // 2.Safe Number Conversion
                //decimal dailyRate;
                // Use InvariantCulture to ensure the parser always accepts '.' as the decimal separator
                if (!decimal.TryParse(rawInput, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dailyRate))
                {
                    throw new Exception("Please enter a valid numeric value for the Daily Rate (e.g., 1000 or 1000.00).");
                }
                // Call Save
                StaffDB.SaveStaff(
                    null,
                    firstNameTextBox1.Text,
                    lastNameTextBox1.Text,
                    userNameTextBox1.Text,
                    passwordHashTextBox1.Text,
                    contactNumberTextBox1.Text,
                    emailAddressTextBox1.Text,
                    cmbAddStatus.SelectedItem.ToString(),
                    dailyRate,
                    (int)cmbRoleAdd.SelectedValue,
                    true
                );

                MessageBox.Show("New staff member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAddFields();
                // Refresh BOTH grids (assigning to their respective targets)
                dgvStaffInfo.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);
                staffDataGridView.DataSource = StaffDB.GetAllStaff();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Addition failed: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStaffInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 1. Ensure the user clicked a valid row
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvStaffInfo.Rows[e.RowIndex];

                    // 2. Safe Helper: We use ? to handle potential nulls 
                    // and ?? "" to ensure we always get a string, never a crash.
                    staffIDTextBox.Text = row.Cells["staffID"].Value?.ToString() ?? "";
                    firstNameTextBox.Text = row.Cells["firstName"].Value?.ToString() ?? "";
                    lastNameTextBox.Text = row.Cells["lastName"].Value?.ToString() ?? "";
                    userNameTextBox.Text = row.Cells["userName"].Value?.ToString() ?? "";
                    contactNumberTextBox.Text = row.Cells["contactNumber"].Value?.ToString() ?? "";
                    emailAddressTextBox.Text = row.Cells["emailAddress"].Value?.ToString() ?? "";
                    dailyRateTextBox.Text = row.Cells["dailyRate"].Value?.ToString() ?? "";

                    // 3. Status ComboBox Logic
                    string statusFromGrid = row.Cells["staffStatus"].Value?.ToString() ?? "";
                    if (cmbEditStatus.Items.Contains(statusFromGrid))
                    {
                        cmbEditStatus.SelectedItem = statusFromGrid;
                    }
                    else
                    {
                        cmbEditStatus.SelectedIndex = -1;
                    }

                    // 4. Role ComboBox Logic
                    // Checking if the value is not null before assigning to prevent UI exceptions
                    var roleValue = row.Cells["roleID"].Value;
                    if (roleValue != null && roleValue != DBNull.Value)
                    {
                        cmbRoleEdit.SelectedValue = roleValue;
                    }
                    else
                    {
                        cmbRoleEdit.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // UI events can occasionally throw unexpected exceptions; catch them to prevent app exit.
                MessageBox.Show("An error occurred while loading staff details: " + ex.Message,
                                "Data Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //// Check if the user clicked a valid data row (not the header)
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dgvStaffInfo.Rows[e.RowIndex];

            //    // Fill the Edit textboxes (using your specific naming convention)
            //    staffIDTextBox.Text = row.Cells["staffID"].Value.ToString();
            //    firstNameTextBox.Text = row.Cells["firstName"].Value.ToString();
            //    lastNameTextBox.Text = row.Cells["lastName"].Value.ToString();
            //    userNameTextBox.Text = row.Cells["userName"].Value.ToString();
            //    contactNumberTextBox.Text = row.Cells["contactNumber"].Value.ToString();
            //    emailAddressTextBox.Text = row.Cells["emailAddress"].Value.ToString();
            //    string statusFromGrid = row.Cells["staffStatus"].Value.ToString();

            //    // This forces the ComboBox to show the item that matches the grid text
            //    if (cmbEditStatus.Items.Contains(statusFromGrid))
            //    {
            //        cmbEditStatus.SelectedItem = statusFromGrid;
            //    }
            //    else
            //    {
            //        cmbEditStatus.SelectedIndex = -1; // Or handle as an error if the status is something unexpected
            //    }
            //    dailyRateTextBox.Text = row.Cells["dailyRate"].Value.ToString();

            //    // Match the role ID in the ComboBox
            //    cmbRoleEdit.SelectedValue = row.Cells["roleID"].Value;
            //}
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            ClearEditFields();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAddFields();
        }

       

        private void dgvStaff_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvStaff_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void dgvStaff_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSaveHours_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0) return;

            var row = dgvStaff.SelectedRows[0];
            int staffId = Convert.ToInt32(row.Cells["Staff ID"].Value);
            int jobId = Convert.ToInt32(row.Cells["Job ID"].Value);
            string raw = row.Cells["Hours Worked"].Value?.ToString() ?? "";

            if (!decimal.TryParse(raw, out decimal hours) || hours < 0)
            {
                MessageBox.Show("Please enter a valid positive number for hours worked.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"UPDATE JobStaffAssignment 
                           SET hoursWorked = @hours 
                           WHERE staffID = @staffID AND jobID = @jobID";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@hours", hours);
                        cmd.Parameters.AddWithValue("@staffID", staffId);
                        cmd.Parameters.AddWithValue("@jobID", jobId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Hours worked updated.", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadJobStaffAssignments(); // refresh grid
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error saving hours:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving hours:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void dgvStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSaveHours.Enabled = e.RowIndex >= 0;
            //change column backcolour to indicate edit mode
            foreach (DataGridViewColumn col in dgvStaff.Columns)
                col.DefaultCellStyle.BackColor = col.HeaderText == "Hours Worked" ? Color.LightYellow : Color.White;


        }
        private void LoadJobStaffAssignments(string search = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string sql = @"
                SELECT
                    jsa.staffID                                     AS [Staff ID],
                    jsa.jobID                                       AS [Job ID],
                    CONCAT(s.firstName, ' ', s.lastName)            AS [Name],
                    r.roleName                                      AS [Role],
                    jsa.assignmentDate                              AS [Date],
                    j.startDate                                     AS [Job Start Date],
                    jr.siteAddress                                  AS [Site Address],
                    jsa.hoursWorked                                 AS [Hours Worked]
                FROM JobStaffAssignment jsa
                JOIN Staff       s  ON jsa.staffID      = s.staffID
                JOIN Role        r  ON s.roleID         = r.roleID
                JOIN Job         j  ON jsa.jobID        = j.jobID
                JOIN Quote       q  ON j.quoteID        = q.QuoteID
                JOIN JobRequest  jr ON q.jobRequestID   = jr.jobRequestID
                WHERE s.firstName  LIKE @search
                   OR s.lastName   LIKE @search
                ORDER BY jsa.assignmentDate DESC, s.lastName";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvStaff.DataSource = dt;

                        foreach (DataGridViewColumn col in dgvStaff.Columns)
                            col.ReadOnly = col.HeaderText != "Hours Worked";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error loading staff assignments:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error loading staff assignments:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadJobStaffAssignments(textBox1.Text.Trim());
        }

        private void userNameTextBox1_Leave(object sender, EventArgs e)
        {
            string inputUsername = userNameTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(inputUsername)) return;

            if (IsUsernameTaken(inputUsername))
            {
                MessageBox.Show("This username is already taken. Please choose another.",
                                "Username Taken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                userNameTextBox1.Focus(); // Put the cursor back in the box
            }
        }

        private void contactNumberTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contactNumberTextBox1.Text)) return;

            // 1. Format Check (Regex)
            string phonePattern = @"^(\+27|27|0)[6-8][0-9]{8}$";
            if (!Regex.IsMatch(contactNumberTextBox1.Text.Trim(), phonePattern))
            {
                MessageBox.Show("Please enter a valid South African contact number (e.g., 0821234567).", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactNumberTextBox1.Focus();
                return; // Stop here if format is wrong
            }

            if (DoesValueExist("contactNumber", contactNumberTextBox1.Text.Trim()))
            {
                MessageBox.Show("This contact number is already in use.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactNumberTextBox1.Focus();
            }
        }

        private void emailAddressTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailAddressTextBox1.Text)) return;

            // 1. Format Check (Regex)
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(emailAddressTextBox1.Text.Trim(), emailPattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailAddressTextBox1.Focus();
                return; // Stop here if format is wrong
            }

            if (DoesValueExist("emailAddress", emailAddressTextBox1.Text.Trim()))
            {
                MessageBox.Show("This email address is already registered.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailAddressTextBox1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // If the checkbox is checked, hide the asterisks (show the text)
            if (checkBox1.Checked)
            {
                passwordHashTextBox.UseSystemPasswordChar = false;
            }
            // If it's unchecked, bring the asterisks back
            else
            {
                passwordHashTextBox.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // If the checkbox is checked, hide the asterisks (show the text)
            if (checkBox2.Checked)
            {
                passwordHashTextBox1.UseSystemPasswordChar = false;
            }
            // If it's unchecked, bring the asterisks back
            else
            {
                passwordHashTextBox1.UseSystemPasswordChar = true;
            }
        }

        private void userNameTextBox_Leave(object sender, EventArgs e)
        {
            string inputUsername = userNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(inputUsername)) return;

            if (IsUsernameTaken(inputUsername))
            {
                MessageBox.Show("This username is already taken. Please choose another.",
                                "Username Taken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                userNameTextBox.Focus(); // Put the cursor back in the box
            }
        }

        private void contactNumberTextBox_Leave(object sender, EventArgs e)
        {
            // 1. Format Check (Regex)
            string phonePattern = @"^(\+27|27|0)[6-8][0-9]{8}$";
            if (!Regex.IsMatch(contactNumberTextBox.Text.Trim(), phonePattern))
            {
                MessageBox.Show("Please enter a valid South African contact number (e.g., 0821234567).", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactNumberTextBox.Focus();
                return; // Stop here if format is wrong
            }
        }

        private void emailAddressTextBox_Leave(object sender, EventArgs e)
        {
            // 1. Format Check (Regex)
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(emailAddressTextBox.Text.Trim(), emailPattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                emailAddressTextBox.Focus();
                return; // Stop here if format is wrong
            }
        }
    }
}
