using M2GiantGroupSystem.GroupWst1DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace M2GiantGroupSystem
{
    public partial class client_MainForm : Form
    {
        int tabIndex;

        public client_MainForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
            ThemeManager.ThemeChanged += ApplyTheme;
        }
        int numberOfResults;
        string value;
        int clientID;
        private bool formLoaded = false;
        private bool loadingClient = false;
        //view clients
        string connStr =
            "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

        int selectedClientID = -1;
        DataTable clientTable = new DataTable();

        //

        private void client_MainForm_Load(object sender, EventArgs e)
        {
            ApplyTheme(); // ADD THIS
            cmb_status.SelectedIndex = 0;
            dgvClients.ScrollBars= ScrollBars.Both;


            try
            {
                disableInput();
                userTip.Text = "Updating will be disabled until a \n " +
                               "client is selected from the results. \n";
                         
                userTip1.Text = "Select a criteria  \nbefore entering\n a value.";


                cboSearchColumn.Items.AddRange(new string[]
            {
                "Name", "Surname", "Email", "Phone", "Type", "Status", "Date Added"
            });
                cboSearchColumn.SelectedIndex = 0;
                cboFilterStatus.SelectedIndex = 0;
                cboFilterType.SelectedIndex = 0;

                SetupGrid();
                LoadClients();
                ClearDetailsPanel();

                tabControl1.SelectedIndex = tabIndex;
                tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
                tabControl1.DrawItem += tabControl1_DrawItem;
                tabControl1.ItemSize = new Size(300, 30);
                tabControl1.SizeMode = TabSizeMode.Fixed;

                loadingClient = true;
                this.clientTableAdapter1.Fill(this.groupWst1DataSet1.Client);
                ResetOriginalValues();

                tbSearchValue_A.Enabled = false;
                tbSearchValue_A.Text = "Disabled";
                tbSearchValue_A.BackColor = Color.LightGray;

                btnActivate.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;

                loadingClient = false;
                formLoaded = true;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show(
                    "Database error while loading the form:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error while loading the form:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void loadListBox(int i)
        {

            clientID = this.groupWst1DataSet1.Client[i].clientID;
            lbSearchResults.Items.Add(clientID + ":" + value);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btn_addClient_Click(object sender, EventArgs e)
        {
            // — Validation —
            if (string.IsNullOrWhiteSpace(tb_name.Text))
            {
                MessageBox.Show("First name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_name.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_surname.Text))
            {
                MessageBox.Show("Surname is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_surname.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_email.Text))
            {
                MessageBox.Show("Email address is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_email.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_email.Text) ||
     !System.Text.RegularExpressions.Regex.IsMatch(tb_email.Text.Trim(),
     @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address (e.g. name@example.com).",
                    "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_email.Focus();
                return;
            }
            if (!IsPhoneValid(tb_phone.Text))
            {
                MessageBox.Show(
                    "Please enter a valid South African phone number.\n" +
                    "Examples: 0831234567, 031 123 4567, +27831234567",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_phone.Focus();
                return;
            }
            if (cmb_type.SelectedItem == null)
            {
                MessageBox.Show("Please select a client type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_type.Focus();
                return;
            }
            if (cmb_status.SelectedItem == null)
            {
                MessageBox.Show("Please select a client status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_status.Focus();
                return;
            }

            // — Uniqueness check (email) —
            if (IsEmailTaken(tb_email.Text.Trim(), -1))
            {
                MessageBox.Show(
                    "A client with this email address already exists.",
                    "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_email.Focus();
                return;
            }

            // — Length guards matching DB column sizes —
            if (tb_name.Text.Trim().Length > 40)
            {
                MessageBox.Show("First name cannot exceed 40 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tb_surname.Text.Trim().Length > 40)
            {
                MessageBox.Show("Surname cannot exceed 40 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tb_email.Text.Trim().Length > 50)
            {
                MessageBox.Show("Email address cannot exceed 50 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tb_phone.Text.Trim().Length > 15)
            {
                MessageBox.Show("Phone number cannot exceed 15 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to add this client?",
                "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    clientTableAdapter1.InsertQuery(
                        cmb_type.SelectedItem.ToString(),
                        tb_email.Text.Trim(),
                        cmb_status.SelectedItem.ToString(),
                        tb_name.Text.Trim(),
                        tb_surname.Text.Trim(),
                        tb_phone.Text.Trim()
                    );

                    MessageBox.Show("Client added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the add form
                    tb_name.Text = "";
                    tb_surname.Text = "";
                    tb_email.Text = "";
                    tb_phone.Text = "";
                  //  cmb_status.SelectedIndex = -1;
                   // cmb_type.SelectedIndex = -1;

                    LoadClients(); // Refresh the grid

                }
                catch (SqlException sqlEx)
                {
                    // Duplicate email caught at DB level too
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                        MessageBox.Show("A client with this email already exists in the database.",
                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Database error while adding client:\n" + sqlEx.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error while adding client:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Client was not added.");
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // — Validation —
            clientID = selectedClientID;
            if (selectedClientID == -1)
            {
                MessageBox.Show("No client is selected for updating.");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("First name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Surname is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Email address is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            if (!textBox2.Text.Contains("@") || !textBox2.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            if (!IsPhoneValid(textBox1.Text))
            {
                MessageBox.Show(
                    "Please enter a valid South African phone number.\n" +
                    "Examples: 0831234567, 031 123 4567, +27831234567",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a client type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return;
            }
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select a client status.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox2.Focus();
                return;
            }

            // — Uniqueness check: email must not belong to a DIFFERENT client —
            if (IsEmailTaken(textBox2.Text.Trim(), selectedClientID))
            {
                MessageBox.Show(
                    "Another client already has this email address.",
                    "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }

            // — Length guards —
            if (textBox4.Text.Trim().Length > 40)
            {
                MessageBox.Show("First name cannot exceed 40 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox3.Text.Trim().Length > 40)
            {
                MessageBox.Show("Surname cannot exceed 40 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox2.Text.Trim().Length > 50)
            {
                MessageBox.Show("Email cannot exceed 50 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox1.Text.Trim().Length > 15)
            {
                MessageBox.Show("Phone number cannot exceed 15 characters.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to update client details for " + textBox4.Text + " ?",
                "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    clientTableAdapter1.UpdateQuery(
                        textBox4.Text.Trim(),
                        textBox3.Text.Trim(),
                        comboBox1.SelectedItem.ToString(),
                        textBox2.Text.Trim(),
                        comboBox2.SelectedItem.ToString(),
                        textBox1.Text.Trim(),
                        clientID
                    );

                    MessageBox.Show("Client updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // Reset BEFORE clearing — comboBoxes still have values here
                    ResetOriginalValues();

                    // Now safe to clear
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    tbSearchValue_A.Text = "";
                    lbSearchResults.Items.Clear();
                    cmbCriteria_A.SelectedIndex = -1;
                    selectedClientID = -1;  // also reset this so the form is in a clean state
                    disableInput();          // disable update button until another client is selected
                    LoadClients();
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                        MessageBox.Show("A client with this email already exists in the database.",
                            "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Database error while updating client:\n" + sqlEx.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error while updating client:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Client was not updated.");
            }
        }


        private void tbSearchValue_A_TextChanged(object sender, EventArgs e)
        {

            try
            {
                int index = cmbCriteria_A.SelectedIndex;
                switch (index)
                {
                    case 0:
                        lbSearchResults.Items.Clear();
                        clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", "");
                        ResetOriginalValues();
                        numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                        for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", ""); i++)
                        {
                            value = this.groupWst1DataSet1.Client[i].clientName + " " + this.groupWst1DataSet1.Client[i].clientSurname; 
                            loadListBox(i);
                        }
                        break;
                    case 1:
                        lbSearchResults.Items.Clear();
                        clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", tbSearchValue_A.Text, "", "");
                        ResetOriginalValues();
                        numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                        for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", tbSearchValue_A.Text, "", ""); i++)
                        {
                            value = this.groupWst1DataSet1.Client[i].clientName + " " + this.groupWst1DataSet1.Client[i].clientSurname;
                            loadListBox(i);
                        }
                        break;
                    case 2:
                        lbSearchResults.Items.Clear();
                        clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", tbSearchValue_A.Text, "");
                        ResetOriginalValues();
                        numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                        for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", tbSearchValue_A.Text, ""); i++)
                        {
                            value = this.groupWst1DataSet1.Client[i].emailAddress;
                            loadListBox(i);
                        }
                        break;
                    case 3:
                        lbSearchResults.Items.Clear();
                        clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", "", tbSearchValue_A.Text);
                        numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                        for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", "", tbSearchValue_A.Text); i++)
                        {
                            value = this.groupWst1DataSet1.Client[i].phoneNumber;
                            loadListBox(i);
                        }
                        break;
                    default:
                        break;
                }

                loadClientDataIntoTextboxes();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error during search:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during search:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void loadClientDataIntoTextboxes()
        {
            loadingClient = true;

            try
            {
               
                if (numberOfResults == 0)
                {
                    lbSearchResults.Items.Add("Client not found!");
                    return;
                }

                if (lbSearchResults.SelectedIndex > -1)
                {
                    string selectedItem = lbSearchResults.SelectedItem.ToString();
                    string[] parts = selectedItem.Split(':');

                    if (parts.Length < 2 || !int.TryParse(parts[0], out int id))
                    {
                        MessageBox.Show("Could not read the selected client ID.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    selectedClientID = id;  // ADD THIS
                    clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, id);
                    enableInput();
                    ResetOriginalValues();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading client data:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading client data:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingClient = false;
            }
        }

        private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClientDataIntoTextboxes();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!formLoaded || loadingClient)
                return;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!formLoaded || loadingClient)
                return;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formLoaded || loadingClient)
                return;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbCriteria_A_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCriteria_A.SelectedIndex > -1)
            {
                tbSearchValue_A.Enabled = true;
                tbSearchValue_A.Text = "";
                tbSearchValue_A.Focus();
                tbSearchValue_A.BackColor = Color.FromArgb(155, 198, 138);
                lblSearchBy_A.Text = "Enter: " + cmbCriteria_A.SelectedItem.ToString(); // moved inside the if
            }
            else
            {
                lblSearchBy_A.Text = ""; // or whatever default text you want
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //shows it was updated
            if (!formLoaded || loadingClient)
                return;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!formLoaded || loadingClient)
                return;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formLoaded || loadingClient)
                return;

        }


        private void checkIfEdited(System.Windows.Forms.TextBox t)
        {
            if (t.Tag == null)
                return;

            t.BackColor =
                !string.Equals(
                    t.Text,
                    t.Tag.ToString(),
                    StringComparison.OrdinalIgnoreCase)
                ? Color.LightBlue
                : Color.White;
        }
        private void checkIfEdited(System.Windows.Forms.ComboBox c)
        {
            if (c.Tag == null)
                return;

            c.BackColor =
                c.Text != c.Tag.ToString()
                ? Color.LightBlue
                : Color.White;
        }

        private void ResetOriginalValues()
        {
            textBox1.Tag = textBox1.Text;
            textBox2.Tag = textBox2.Text;
            textBox3.Tag = textBox3.Text;
            textBox4.Tag = textBox4.Text;

            comboBox1.Tag = comboBox1.Text;
            comboBox2.Tag = comboBox2.Text;


        }

        private void clientBS_CurrentChanged(object sender, EventArgs e)
        {

        }
        // -----------------------------
        // GRID SETUP
        // -----------------------------
        void SetupGrid()
        {
            dgvClients.ReadOnly = true;
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.MultiSelect = false;
            dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClients.RowHeadersVisible = false;
            dgvClients.AllowUserToAddRows = false;
            dgvClients.BackgroundColor = Color.FromArgb(155,198,138);
            dgvClients.BorderStyle = BorderStyle.FixedSingle;
            dgvClients.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClients.DefaultCellStyle.SelectionBackColor = Color.Green;
            dgvClients.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvClients.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvClients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 16f, FontStyle.Regular);
            dgvClients.EnableHeadersVisualStyles = false;
           
        }

        // -----------------------------
        // LOAD CLIENTS FROM DB
        // -----------------------------
        void LoadClients()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT
                            clientID        AS [ID],
                            clientName      AS [Name],
                            clientSurname   AS [Surname],
                            clientType      AS [Type],
                            emailAddress    AS [Email],
                            phoneNumber     AS [Phone],
                            status          AS [Status],
                            dateAdded       AS [Date Added]
                        FROM Client
                        ORDER BY clientName";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    clientTable = new DataTable();
                    da.Fill(clientTable);
                    dgvClients.DataSource = clientTable;
                    dgvClients.Columns["ID"].Visible = false;
                }
                ColourRows();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading clients:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading clients:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // -----------------------------
        // COLOUR ROWS BY STATUS
        // -----------------------------
        void ColourRows()
        {
            foreach (DataGridViewRow row in dgvClients.Rows)
            {
                //if (row.IsNewRow) continue;

                string status = row.Cells["Status"].Value?.ToString();
                string type = row.Cells["Type"].Value?.ToString();

                // Base colour by client type
                switch (type)
                {
                    case "Residential":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // AliceBlue
                        break;
                    case "Commercial":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240); // FloralWhite
                        break;
                    case "Government":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240); // Honeydew
                        break;
                    default:
                        row.DefaultCellStyle.BackColor = Color.White;
                        break;
                }

                // Override: archived clients go gray
                if (status == "Archived")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220);
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
            dgvClients.Refresh();
        }

        private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                // Guard: filtered view may have fewer rows than clientTable
                DataView dv = (dgvClients.DataSource as DataTable).DefaultView;
                if (e.RowIndex >= dv.Count) return;

                DataGridViewRow row = dgvClients.Rows[e.RowIndex];

                selectedClientID = Convert.ToInt32(dv[e.RowIndex]["ID"]);

                lblDetailID.Text = "ID: " + row.Cells["ID"]?.Value?.ToString();
                lblDetailName.Text = "Name: " + row.Cells["Name"].Value?.ToString();
                lblDetailSurname.Text = "Surname: " + row.Cells["Surname"].Value?.ToString();
                lblDetailType.Text = "Type: " + row.Cells["Type"].Value?.ToString();
                lblDetailEmail.Text = "Email: " + row.Cells["Email"].Value?.ToString();
                lblDetailPhone.Text = "Phone: " + row.Cells["Phone"].Value?.ToString();
                lblDetailStatus.Text = "Status: " + row.Cells["Status"].Value?.ToString();
                lblDetailDate.Text = "Date Added: " +
                    Convert.ToDateTime(row.Cells["Date Added"].Value).ToString("dd MMM yyyy");

                string status = row.Cells["Status"].Value?.ToString();
                lblDetailStatus.ForeColor = status == "Active" ? Color.LightGreen : Color.Gray;

                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnActivate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading selected client:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ApplyFilter()
        {
            try
            {
                string searchText = txtSearchV.Text.Trim().Replace("'", "''");
                string typeFilter = cboFilterType.SelectedItem?.ToString();
                string statusFilter = cboFilterStatus.SelectedItem?.ToString();
                string column = GetColumnName(cboSearchColumn.SelectedItem?.ToString());

                string filter = "";

                if (!string.IsNullOrWhiteSpace(searchText) && column != null)
                {
                    if (column == "Date Added")
                        filter = $"CONVERT([Date Added], 'System.String') LIKE '%{searchText}%'";
                    else
                        filter = $"[{column}] LIKE '%{searchText}%'";
                }

                if (typeFilter != "All Types")
                {
                    string typeClause = $"[Type] = '{typeFilter}'";
                    filter = string.IsNullOrEmpty(filter) ? typeClause : filter + " AND " + typeClause;
                }

                if (statusFilter != "All Statuses")
                {
                    string statusClause = $"[Status] = '{statusFilter}'";
                    filter = string.IsNullOrEmpty(filter) ? statusClause : filter + " AND " + statusClause;
                }

                (dgvClients.DataSource as DataTable).DefaultView.RowFilter = filter;
                ColourRows();
            }
            catch (Exception ex)
            {
                // Invalid filter syntax — silently clear rather than crash
                try { (dgvClients.DataSource as DataTable).DefaultView.RowFilter = ""; } catch { }
                // Uncomment below to surface the error during development:
                // MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        string GetColumnName(string uiName)
        {
            switch (uiName)
            {
                case "Name": return "Name";
                case "Surname": return "Surname";
                case "Email": return "Email";
                case "Phone": return "Phone";
                case "Type": return "Type";
                case "Status": return "Status";
                case "Date Added": return "Date Added";
                default: return null;
            }
        }

        // -----------------------------
        // CLEAR FILTERS
        // -----------------------------
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            try
            {
               
                txtSearchV.Text = "";
               /// cboFilterType.SelectedIndex = -1;
               // cboFilterStatus.SelectedIndex = -1;
                (dgvClients.DataSource as DataTable).DefaultView.RowFilter = "";
                ColourRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing filters:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboSearchColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSearchColumn.SelectedIndex==6)
            {
               //change size of font
               label10.Font = new Font(label10.Font.FontFamily, 12, FontStyle.Bold);

                label10.Text = "Enter date client was added:\ndd-mm-yyyy";
            }
            else { 
                label10.Text = "Enter client " + cboSearchColumn.SelectedItem.ToString() + " :";
            }
               

        }

        private void txtSearchV_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cboFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedClientID == -1)
            {
                MessageBox.Show("Please select a client first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, selectedClientID);
                loadingClient = true;
                tabControl1.SelectedIndex = 1;
                ResetOriginalValues();
                loadingClient = false;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading client for editing:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            tabControl1.SelectedIndex = 0;
            LoadClients();

        }
        void ClearDetailsPanel()
        {
            lblDetailID.Text = "ID: —";
            lblDetailName.Text = "Name: —";
            lblDetailSurname.Text = "Surname: —";
            lblDetailType.Text = "Type: —";
            lblDetailEmail.Text = "Email: —";
            lblDetailPhone.Text = "Phone: —";
            lblDetailStatus.Text = "Status: —";
            lblDetailDate.Text = "Date Added: —";
            lblDetailStatus.ForeColor = Color.Black;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void disableInput()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;

            btnUpdate.Enabled = false;
            btnUpdate.ForeColor = Color.Gray;


        }

        private void enableInput()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            btnUpdate.Enabled = true;
            btnUpdate.ForeColor = Color.White;
        }

        private void userTip_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            ColourRows();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (selectedClientID == -1)
            {
                MessageBox.Show("Please select a client to archive.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent archiving a client who is already archived
            string currentStatus = lblDetailStatus.Text.Replace("Status: ", "").Trim();
            if (currentStatus == "Archived")
            {
                MessageBox.Show("This client is already archived.", "Already Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string clientName = lblDetailName.Text.Replace("Name: ", "").Trim() + " " +
                                lblDetailSurname.Text.Replace("Surname: ", "").Trim();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to archive client '{clientName}'?\n\n" +
                "They will no longer appear as active but will not be permanently deleted.",
                "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "UPDATE Client SET status = 'Archived' WHERE clientID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedClientID);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 0)
                        {
                            MessageBox.Show("No client was updated. They may have already been removed.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                MessageBox.Show($"'{clientName}' has been archived successfully.",
                    "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearDetailsPanel();
                selectedClientID = -1;
                LoadClients();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while archiving client:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while archiving client:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /////validation////
        /// <summary>
        /// Validates a South African phone number.
        /// Accepts 10-digit numbers starting with 0, or +27 international format.
        /// Strips spaces, dashes, and brackets before checking.
        /// </summary>
        bool IsPhoneValid(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            // Strip formatting characters
            string cleaned = phone.Trim()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");

            // +27XXXXXXXXX (12 chars) or +270XXXXXXXXX (13 chars — rare but handle it)
            if (cleaned.StartsWith("+27"))
            {
                string local = cleaned.Substring(3);
                return local.Length >= 9 && local.Length <= 10 && local.All(char.IsDigit);
            }

            // 0XXXXXXXXX (10 digits)
            if (cleaned.StartsWith("0"))
            {
                return cleaned.Length == 10 && cleaned.All(char.IsDigit);
            }

            return false;
        }

        /// <summary>
        /// Returns true if the email is already in use by a DIFFERENT client.
        /// Pass excludeClientID = -1 when adding (no client to exclude).
        /// Pass the current clientID when updating.
        /// </summary>
        bool IsEmailTaken(string email, int excludeClientID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = excludeClientID == -1
                        ? "SELECT COUNT(*) FROM Client WHERE emailAddress = @email"
                        : "SELECT COUNT(*) FROM Client WHERE emailAddress = @email AND clientID <> @id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        if (excludeClientID != -1)
                            cmd.Parameters.AddWithValue("@id", excludeClientID);

                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch
            {
                // If the check fails, let the DB constraint handle it
                return false;
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (selectedClientID == -1)
            {
                MessageBox.Show("Please select a client to make active again.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent activating a client who is already activated
            string currentStatus = lblDetailStatus.Text.Replace("Status: ", "").Trim();
            if (currentStatus == "Active")
            {
                MessageBox.Show("This client is already active.", "Already Active", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string clientName = lblDetailName.Text.Replace("Name: ", "").Trim() + " " +
                                lblDetailSurname.Text.Replace("Surname: ", "").Trim();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to activate client '{clientName}'?\n\n" +
                "They will appear as active and not archived anymore.",
                "Confirm Activate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "UPDATE Client SET status = 'Active' WHERE clientID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedClientID);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows == 0)
                        {
                            MessageBox.Show("No client was updated. They may have already been removed.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                MessageBox.Show($"'{clientName}' has been activated successfully.",
                    "Activated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearDetailsPanel();
                selectedClientID = -1;
                LoadClients();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while activating client:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while activating client:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadClients();
        }
    }
 }
