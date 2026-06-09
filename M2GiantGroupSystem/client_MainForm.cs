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
            disableInput();
            userTip.Text = "Updating will be disabled until a \n " +
                           "client is selected from the results \n" +
                           "or only one client is found.";

            userTip1.Text = "Select a criteria  \nbefore entering\n a value.";

            //view clients

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
            //---------------------------------------------------
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

            loadingClient = false;
            formLoaded = true;
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
            DialogResult result = MessageBox.Show(
            "Are you sure you want to add this client?",
            "Confirm Add",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
              );

            if (result == DialogResult.Yes)
            {
                clientTableAdapter1.InsertQuery(
                cmb_type.SelectedItem.ToString(),
                tb_email.Text,
                cmb_status.SelectedItem.ToString(),
                tb_name.Text,
                tb_surname.Text,
                tb_phone.Text
            );

                MessageBox.Show("Client added successfully!");
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
            DialogResult result = MessageBox.Show(
             "Are you sure you want to update client details for " + tb_name.Text + " ?",
             "Confirm Update",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Question
               );
            if (result == DialogResult.Yes)
            {
                clientTableAdapter1.UpdateQuery(
                textBox4.Text,
                textBox3.Text,
                comboBox1.SelectedItem.ToString(),
                  textBox2.Text,
                comboBox2.SelectedItem.ToString(),
                textBox1.Text,
             clientID
            );
                MessageBox.Show("Client updated successfully!");
            }
            else
            {
                MessageBox.Show("Client was not updated.");

            }
        }


        private void tbSearchValue_A_TextChanged(object sender, EventArgs e)
        {

            int index = cmbCriteria_A.SelectedIndex;
            switch (index)
            {
                case 0:
                    lbSearchResults.Items.Clear();
                    clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", "");
                    ResetOriginalValues();
                    numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                    //for all rows found add name to listbox
                    for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", ""); i++)
                    {
                        value = this.groupWst1DataSet1.Client[i].clientName;
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
                        value = this.groupWst1DataSet1.Client[i].clientSurname;
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

            }//switch

            loadClientDataIntoTextboxes();
        }
        void loadClientDataIntoTextboxes()
        {
            loadingClient = true;

            if (numberOfResults == 1)
            {
                clientTableAdapter1.FillByID(
                    this.groupWst1DataSet1.Client,
                    this.groupWst1DataSet1.Client[0].clientID);

                ResetOriginalValues();

                loadingClient = false;
                enableInput();
                return;
            }

            if (lbSearchResults.SelectedIndex > -1)
            {
                string selectedItem = lbSearchResults.SelectedItem.ToString();
                string[] parts = selectedItem.Split(':');
                int id = int.Parse(parts[0]);

                clientTableAdapter1.FillByID(
                    this.groupWst1DataSet1.Client,
                    id);
                enableInput();

                ResetOriginalValues();
            }

            loadingClient = false;
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
                //make textbox green to show its enabled
                tbSearchValue_A.Focus();
                tbSearchValue_A.BackColor = Color.FromArgb(155, 198, 138);

             

            }
            lblSearchBy_A.Text = "Enter: " + cmbCriteria_A.SelectedItem.ToString();
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

                // Hide the ID column from view but keep it in the table
                dgvClients.Columns["ID"].Visible = false;

                
            }
            ColourRows();

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
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvClients.Rows[e.RowIndex];

            selectedClientID = Convert.ToInt32(clientTable.Rows[e.RowIndex]["ID"]);

            lblDetailID.Text = "ID: " + row.Cells["ID"]?.Value?.ToString();
            lblDetailName.Text = "Name: " + row.Cells["Name"].Value?.ToString();
            lblDetailSurname.Text = "Surname: " + row.Cells["Surname"].Value?.ToString();
            lblDetailType.Text = "Type: " + row.Cells["Type"].Value?.ToString();
            lblDetailEmail.Text = "Email: " + row.Cells["Email"].Value?.ToString();
            lblDetailPhone.Text = "Phone: " + row.Cells["Phone"].Value?.ToString();
            lblDetailStatus.Text = "Status: " + row.Cells["Status"].Value?.ToString();
            lblDetailDate.Text = "Date Added: " + Convert.ToDateTime(row.Cells["Date Added"].Value).ToString("dd MMM yyyy");

            // Colour the status label
            string status = row.Cells["Status"].Value?.ToString();
            lblDetailStatus.ForeColor = status == "Active" ? Color.LightGreen : Color.Gray;

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        void ApplyFilter()
        {
            string searchText = txtSearchV.Text.Trim().Replace("'", "''");
            string typeFilter = cboFilterType.SelectedItem?.ToString();
            string statusFilter = cboFilterStatus.SelectedItem?.ToString();
            string column = GetColumnName(cboSearchColumn.SelectedItem?.ToString());

            string filter = "";

            // Text search
            if (!string.IsNullOrWhiteSpace(searchText) && column != null)
            {
                if (column == "Date Added")
                    filter = $"CONVERT([Date Added], 'System.String') LIKE '%{searchText}%'";
                else
                    filter = $"[{column}] LIKE '%{searchText}%'";
            }

            // Type filter
            if (typeFilter != "All Types")
            {
                string typeClause = $"[Type] = '{typeFilter}'";
                filter = string.IsNullOrEmpty(filter) ? typeClause : filter + " AND " + typeClause;
            }

            // Status filter
            if (statusFilter != "All Statuses")
            {
                string statusClause = $"[Status] = '{statusFilter}'";
                filter = string.IsNullOrEmpty(filter) ? statusClause : filter + " AND " + statusClause;
            }

            try
            {
                (dgvClients.DataSource as DataTable).DefaultView.RowFilter = filter;
                ColourRows();
            }
            catch { }
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
            txtSearchV.Text = "";
            cboFilterType.SelectedIndex = 0;
            cboFilterStatus.SelectedIndex = 0;
            (dgvClients.DataSource as DataTable).DefaultView.RowFilter = "";
            ColourRows();
        }

        private void cboSearchColumn_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                MessageBox.Show("Please select a client first.");
                return;
            }
            clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, selectedClientID);
            loadingClient = true;

            tabControl1.SelectedIndex = 1;

            // Load selected client's data here if needed

            ResetOriginalValues();

            loadingClient = false;
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

        }
    }
 }
