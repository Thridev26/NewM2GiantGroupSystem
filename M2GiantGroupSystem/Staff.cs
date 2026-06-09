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
            string query = "SELECT roleID, roleName FROM Role"; // Adjust if your table/column names differ
            using (SqlConnection con = new SqlConnection("YourConnectionString")) // Use your actual string
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the ComboBox
                cmbRole.DisplayMember = "roleName"; // What the user sees
                cmbRole.ValueMember = "roleID";     // What gets saved to the database
                cmbRole.DataSource = dt;
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
            // TODO: This line of code loads data into the 'groupWst1DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.groupWst1DataSet.Staff);
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;

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

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Get the Role ID
            int selectedRoleID = (int)cmbRole.SelectedValue;

            // 2. Decide if we are sending a password or not
            // We only pass the text if the user actually typed something new
            string passToSend = string.IsNullOrWhiteSpace(passwordHashTextBox.Text) ? null : passwordHashTextBox.Text;

            // 3. Call SaveStaff
            StaffDB.SaveStaff(
                int.Parse(staffIDTextBox.Text),
                firstNameTextBox.Text,
                lastNameTextBox.Text,
                userNameTextBox.Text,
                passToSend, // Using the new variable that handles empty strings
                contactNumberTextBox.Text,
                staffStatusTextBox.Text,
                decimal.Parse(dailyRateTextBox.Text),
                selectedRoleID,
                false
            );

            MessageBox.Show("Staff updated successfully.");

            // Refresh your GridView
            dgvStaffInfo.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);
        }

        private void ClearFields()
        {
            firstNameTextBox1.Clear();
            lastNameTextBox1.Clear();
            userNameTextBox1.Clear();
            passwordHashTextBox1.Clear(); // This will be the NEW password
            contactNumberTextBox1.Clear();
            cmbRole.SelectedIndex = -1; // Deselects the combo box
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(userNameTextBox1.Text) || string.IsNullOrWhiteSpace(passwordHashTextBox1.Text))
            {
                MessageBox.Show("Username and Password are required.");
                return;
            }

            // Call the same Save method, but pass 'true' for isNew
            StaffDB.SaveStaff(
                null, // No ID needed for new records
                firstNameTextBox1.Text,
                lastNameTextBox1.Text,
                userNameTextBox1.Text,
                passwordHashTextBox1.Text, // The class handles hashing this
                contactNumberTextBox1.Text,
                "Active",         // Default status
                0.00m,            // Default rate
                (int)cmbRole.SelectedValue,
                true              // isNew = true tells it to INSERT
            );

            MessageBox.Show("New staff member added successfully!");
            ClearFields();

            // Refresh the grid so the user sees the new person immediately
            dgvStaffInfo.DataSource = StaffDB.GetStaffForUser(UserSession.StaffID, UserSession.AccessLevel);
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
                staffStatusTextBox.Text = row.Cells["staffStatus"].Value.ToString();
                dailyRateTextBox.Text = row.Cells["dailyRate"].Value.ToString();

                // Match the role ID in the ComboBox
                cmbRole.SelectedValue = row.Cells["roleID"].Value;
            }
        }
    }
}
