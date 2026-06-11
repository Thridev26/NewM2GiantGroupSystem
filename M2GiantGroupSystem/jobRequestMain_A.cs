using iText.StyledXmlParser.Jsoup.Select;
using M2GiantGroupSystem.GroupWst1DataSetTableAdapters;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Design;

namespace M2GiantGroupSystem
{
    public partial class jobRequestMain_A : Form
    {
        int tabIndex;
        private string selectedPhotoPath = null;
        private string defaultImagePath = @"C:\Users\ashmi\source\repos\NewM2GiantGroupSystem\M2GiantGroupSystem\images1\no image available icon.jpg";
        
        public jobRequestMain_A(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }
        // ─────────────────────────────────────────────
        // SHARED VARIABLES
        // ─────────────────────────────────────────────
        int selectedIndex;
        int numberOfResults = 0;
        string value;
        int clientID;
        int jobRequestID;
        string jobName;
        int jobTypeID;
        int requestItemID;
        string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";


        //view job requests--------------------------------------------------------------------------------------------------------

       

        private void jobRequestMain_A_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = tabIndex;
                tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
                tabControl1.DrawItem += tabControl1_DrawItem;
                tabControl1.ItemSize = new Size(300, 30);
                tabControl1.SizeMode = TabSizeMode.Fixed;

                this.jobRequestTableAdapter1.Fill(this.groupWst1DataSet1.JobRequest);

                dgv_clientJoinJobRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv_clientJoinJobRequest.DefaultCellStyle.SelectionBackColor = Color.Green;

                runQuery(textBox3, dgvJoinPictures);

                dgvJoinPictures.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvJoinPictures.DefaultCellStyle.SelectionBackColor = Color.Green;
                runQuery(textBox3, dgv_clientJoinJobRequest);

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Green;
                runQuery(textBox2, dataGridView1);

                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                flowLayoutPanel1.WrapContents = false;
                flowLayoutPanel1.AutoScroll = true;

                runQuery2(DateTime.Now, "", "");

                dgvJoin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvJoin.DefaultCellStyle.SelectionBackColor = Color.Green;

                label3.Text = "Select site\nevaluation date:";
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading the form:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading the form:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // ─────────────────────────────────────────────
        // VIEW / FILTER TAB — runQuery2 (parameterised)
        // ─────────────────────────────────────────────
        public void runQuery2(DateTime d, string s1, string s2)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT 
                            jr.jobRequestID,
                            jr.siteAddress,
                            jr.status,
                            jr.dateRecieved,
                            c.clientName,
                            c.clientSurname,
                            c.emailAddress,
                            jt.jobTypeName
                        FROM JobRequest jr
                        INNER JOIN Client c ON jr.clientID = c.clientID
                        LEFT JOIN RequestItem ri ON jr.jobRequestID = ri.jobRequestID
                        LEFT JOIN JobType jt ON ri.jobTypeID = jt.jobTypeID
                        WHERE 
                            (@s1 = '' OR jt.jobTypeName = @s1)
                            OR (@s2 = '' OR jr.status = @s2)
                            OR (@useDate = 0 OR CAST(jr.dateRecieved AS DATE) = CAST(@d AS DATE))";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@s1", s1 ?? "");
                        cmd.Parameters.AddWithValue("@s2", s2 ?? "");
                        cmd.Parameters.AddWithValue("@d", d.Date);
                        // Only apply the date filter when a specific date is actually being filtered on
                        cmd.Parameters.AddWithValue("@useDate", (s1 == "" && s2 == "") ? 0 : 0);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvJoin.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading job requests:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading job requests:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void runQuery(TextBox t, DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT jobRequestID, Client.clientName, Client.clientSurname,
                               Client.emailAddress, JobRequest.dateRecieved,
                               JobRequest.siteAddress, JobRequest.siteEvaluationDate
                        FROM Client
                        INNER JOIN JobRequest ON Client.clientID = JobRequest.clientID
                        WHERE clientName    LIKE @search
                           OR clientSurname LIKE @search
                           OR siteAddress   LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + t.Text + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
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

        // ─────────────────────────────────────────────
        // CLIENT SEARCH HELPERS
        // ─────────────────────────────────────────────
        void loadClientDataIntoTextboxes()
        {
            try
            {
                if (numberOfResults == 1)
                {
                    clientTableAdapter1.FillByID(
                        this.groupWst1DataSet1.Client,
                        this.groupWst1DataSet1.Client[0].clientID);
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
                    clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, id);
                }
                 if (numberOfResults==0)
                {
                    lbSearchResults.Items.Add("Client not found!");
                    return;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading client:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading client:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void loadListBox(int i)
        {

            clientID = this.groupWst1DataSet1.Client[i].clientID;
            lbSearchResults.Items.Add(clientID + ":" + value);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnDisplayMap_A_Click(object sender, EventArgs e)
        {
            OpenMap(tbLat_A, tbLong_A);
        }

        private void cmbCriteria_A_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbCriteria_A.SelectedIndex;
            switch (index)
            {
                case 0:
                    lblSearchBy_A.Text = "Search by Client Name";
                    break;
                case 1:
                    lblSearchBy_A.Text = "Search by Client Surname";
                    break;
                case 2:
                    lblSearchBy_A.Text = "Search by Client Email";
                    break;
                case 3:
                    lblSearchBy_A.Text = "Search by Client Phone";
                    break;

                default:
                    lblSearchBy_A.Text = "Search by...";
                    break;

            }
        }

        private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClientDataIntoTextboxes();
        }

        private void tbSearchValue_A_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cmbCriteria_A.SelectedIndex;

                if (index == -1)
                {
                    MessageBox.Show("Please select a search criteria first.",
                        "No Criteria Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                switch (index)
                {
                    case 0:
                        lbSearchResults.Items.Clear();
                        clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", "");
                        numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                        for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", ""); i++)
                        {
                            value = this.groupWst1DataSet1.Client[i].clientName+ " "+ this.groupWst1DataSet1.Client[i].clientSurname; 
                            loadListBox(i);
                        }
                        break;
                    case 1:
                        lbSearchResults.Items.Clear();
                        clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", tbSearchValue_A.Text, "", "");
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
                }

                loadClientDataIntoTextboxes();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error during client search:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during client search:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            // — Validation —
            if (clientID == 0)
            {
                MessageBox.Show("Please search for and select a client before saving.",
                    "No Client Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbAddress_A.Text))
            {
                MessageBox.Show("Site address is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbAddress_A.Focus();
                return;
            }
            if (tbAddress_A.Text.Trim().Length > 100)
            {
                MessageBox.Show("Site address cannot exceed 100 characters.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbAddress_A.Focus();
                return;
            }
            if (cmbRequestSource_A.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a request source.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRequestSource_A.Focus();
                return;
            }
            if (cmbUrgencyLevel_A.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an urgency level.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUrgencyLevel_A.Focus();
                return;
            }
            if (clbItems.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one job type from the list.",
                    "No Items Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                clbItems.Focus();
                return;
            }

            try
            {
                jobRequestID = Convert.ToInt32(jobRequestTableAdapter1.InsertQuery(
                    clientID,
                    tbAddress_A.Text.Trim(),
                    cmbRequestSource_A.SelectedItem.ToString(),
                    cmbUrgencyLevel_A.SelectedItem.ToString()));

                foreach (var item in clbItems.CheckedItems)
                {
                    string itemString = item.ToString();

                    this.jobTypeTableAdapter1.FillByName(this.groupWst1DataSet1.JobType, itemString);

                    if (this.groupWst1DataSet1.JobType.Rows.Count == 0)
                    {
                        MessageBox.Show($"Job type '{itemString}' could not be found in the database. Skipping.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    int jtID = Convert.ToInt32(this.groupWst1DataSet1.JobType.Rows[0]["JobTypeID"]);
                    requestItemTableAdapter1.InsertQuery(jobRequestID, jtID);
                    this.requestItemTableAdapter1.Fill(this.groupWst1DataSet1.RequestItem);
                }

                MessageBox.Show("Inquiry with requested items saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                tbAddress_A.Text = "";
                tbLat_A.Text = "";
                tbLong_A.Text = "";
                cmbRequestSource_A.SelectedIndex = -1;
                cmbUrgencyLevel_A.SelectedIndex = -1;
                for (int i = 0; i < clbItems.Items.Count; i++)
                    clbItems.SetItemChecked(i, false);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while saving job request:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while saving job request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnMap_Click(object sender, EventArgs e)
        {
            OpenMap(tbLat, tbLong);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }



        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           runQuery(textBox1, dgv_clientJoinJobRequest);
        }

        private void dgv_clientJoinJobRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgv_clientJoinJobRequest.Rows[e.RowIndex].Cells["jobRequestID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job Request ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobRequestID = Convert.ToInt32(cell.Value);
                lbl_ID.Text = "Selected Job Request ID: " + jobRequestID.ToString();

                jobRequestTableAdapter1.FillByID(this.groupWst1DataSet1.JobRequest, jobRequestID);

                if (groupWst1DataSet1.JobRequest.Rows.Count == 0) return;

                DataRow r = groupWst1DataSet1.JobRequest.Rows[0];

                tbSiteAddress.Text = r["siteAddress"].ToString();
                tbLat.Text = r.IsNull("latitude") ? "" : Convert.ToDecimal(r["latitude"]).ToString("F6");
                tbLong.Text = r.IsNull("longitude") ? "" : Convert.ToDecimal(r["longitude"]).ToString("F6");

                // Safely set combos by matching the stored value
                cmbRS.SelectedItem = cmbRS.Items.Cast<object>()
                                            .FirstOrDefault(i => i.ToString() == r["requestSource"].ToString());
                cmbUL.SelectedItem = cmbUL.Items.Cast<object>()
                                            .FirstOrDefault(i => i.ToString() == r["urgencyLevel"].ToString());
                cmbStatus.SelectedItem = cmbStatus.Items.Cast<object>()
                                            .FirstOrDefault(i => i.ToString() == r["status"].ToString());

                if (!r.IsNull("siteEvaluationDate"))
                    dateTimePicker1.Value = Convert.ToDateTime(r["siteEvaluationDate"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting job request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // — Validation —
           
            if (jobRequestID == 0)
            {
                MessageBox.Show("Please select a job request from the table before saving changes.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbSiteAddress.Text))
            {
                MessageBox.Show("Site address is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbSiteAddress.Focus();
                return;
            }
            if (tbSiteAddress.Text.Trim().Length > 100)
            {
                MessageBox.Show("Site address cannot exceed 100 characters.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbSiteAddress.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLat.Text) || string.IsNullOrWhiteSpace(tbLong.Text))
            {
                MessageBox.Show("Latitude and longitude are required. Please use the map pin to capture coordinates.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(tbLat.Text, out decimal lat))
            {
                MessageBox.Show("Latitude contains an invalid value. Please use the map pin to recapture coordinates.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbLat.Focus();
                return;
            }
            if (!decimal.TryParse(tbLong.Text, out decimal lng))
            {
                MessageBox.Show("Longitude contains an invalid value. Please use the map pin to recapture coordinates.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbLong.Focus();
                return;
            }
            // Sanity check — coordinates must be in a plausible range for South Africa
            if (lat < -35 || lat > -22 || lng < 16 || lng > 33)
            {
                MessageBox.Show("The coordinates appear to be outside South Africa. Please re-pin the location on the map.",
                    "Invalid Coordinates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbRS.SelectedItem == null)
            {
                MessageBox.Show("Please select a request source.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRS.Focus();
                return;
            }
            if (cmbUL.SelectedItem == null)
            {
                MessageBox.Show("Please select an urgency level.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUL.Focus();
                return;
            }
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return;
            }

            DateTime selectedDate = dateTimePicker1.Value;
            if (selectedDate.Date < DateTime.Now.Date)
            {
                MessageBox.Show("The site evaluation date cannot be in the past.",
                    "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                jobRequestTableAdapter1.UpdateQuery(
                    tbSiteAddress.Text.Trim(),
                    lng,
                    lat,
                    cmbRS.SelectedItem.ToString(),
                    cmbUL.SelectedItem.ToString(),
                    cmbStatus.SelectedItem.ToString(),
                    selectedDate.ToString(),
                    jobRequestID);

                MessageBox.Show("Changes updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while updating job request:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while updating job request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMap_Click_1(object sender, EventArgs e)
        {
            // 1. Open the map form as a clean modal popup window
            using (MapPopupForm mapWindow = new MapPopupForm())
            {
                // 2. Display the map window. If the user drops a pin, it returns OK and closes automatically
                if (mapWindow.ShowDialog() == DialogResult.OK)
                {
                    // 3. Instantly fill your main form text boxes with the captured coordinates!
                    // Change these to match your exact textbox names if they are different (e.g. txtLat)
                    tbLat.Text = mapWindow.SelectedLatitude.ToString("F6");
                    tbLong.Text = mapWindow.SelectedLongitude.ToString("F6");

                    // 4. Show a friendly notification
                    MessageBox.Show("Location coordinates successfully captured from the map pin!",
                                    "Capture Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            runQuery(textBox2,dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dataGridView1.Rows[e.RowIndex].Cells["jobRequestID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job Request ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                label17.Text = "Selected Job Request ID: " + cell.Value.ToString();
                flowLayoutPanel1.Controls.Clear();

                jobRequestID = Convert.ToInt32(cell.Value);

                requestItemTableAdapter1.FillByJobRequestID(groupWst1DataSet1.RequestItem, jobRequestID);

                if (groupWst1DataSet1.RequestItem.Rows.Count == 0)
                {
                    Label lbl_empty = new Label();
                    lbl_empty.Text = "No request items found for this job request.";
                    lbl_empty.AutoSize = true;
                    lbl_empty.Margin = new Padding(10, 15, 10, 5);
                    flowLayoutPanel1.Controls.Add(lbl_empty);
                    return;
                }

                foreach (DataRow row in groupWst1DataSet1.RequestItem.Rows)
                {
                    requestItemID = Convert.ToInt32(row["requestItemID"]);
                    jobTypeID = Convert.ToInt32(row["jobTypeID"]);

                    jobDetailTableAdapter1.FillByJobTypeID(groupWst1DataSet1.JobDetail, jobTypeID);
                    jobTypeTableAdapter1.FillByID(groupWst1DataSet1.JobType, jobTypeID);

                    if (groupWst1DataSet1.JobType.Rows.Count == 0) continue;

                    jobName = groupWst1DataSet1.JobType.Rows[0]["jobTypeName"].ToString();

                    Label lbl_title = new Label();
                    lbl_title.Text = "Job Name: " + jobName;
                    lbl_title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    lbl_title.AutoSize = true;
                    lbl_title.Margin = new Padding(10, 15, 10, 5);
                    flowLayoutPanel1.Controls.Add(lbl_title);

                    foreach (DataRow jobDetailRow in groupWst1DataSet1.JobDetail.Rows)
                    {
                        Label lbl_jobDetailName = new Label();
                        lbl_jobDetailName.Text = jobDetailRow["detailName"].ToString();
                        lbl_jobDetailName.AutoSize = false;
                        lbl_jobDetailName.Width = 120;
                        lbl_jobDetailName.Margin = new Padding(10, 10, 5, 0);

                        TextBox tb_jobDetailName = new TextBox();
                        tb_jobDetailName.Margin = new Padding(5, 5, 20, 10);
                        tb_jobDetailName.Width = 300;
                        tb_jobDetailName.Name = "tb_jobDetailName" + jobDetailRow["jobDetailID"].ToString();
                        tb_jobDetailName.Tag = new
                        {
                            jobDetailID = Convert.ToInt32(jobDetailRow["jobDetailID"]),
                            requestItemID = requestItemID
                        };

                        string existingValue = GetExistingDetailValue(
                            Convert.ToInt32(jobDetailRow["jobDetailID"]),
                            requestItemID);

                        if (existingValue != null)
                            tb_jobDetailName.Text = existingValue;

                        flowLayoutPanel1.Controls.Add(lbl_jobDetailName);
                        flowLayoutPanel1.Controls.Add(tb_jobDetailName);
                    }

                    Panel separator = new Panel();
                    separator.BorderStyle = BorderStyle.Fixed3D;
                    separator.Width = flowLayoutPanel1.Width - 30;
                    separator.Height = 2;
                    separator.Margin = new Padding(5, 15, 5, 15);
                    flowLayoutPanel1.Controls.Add(separator);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading job details:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading job details:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (jobRequestID ==0)
            {
                MessageBox.Show("Please select a job request from the table before saving details.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check at least one textbox in the panel has a value
            bool hasAnyValue = flowLayoutPanel1.Controls
                .OfType<TextBox>()
                .Any(tb => !string.IsNullOrWhiteSpace(tb.Text));

            if (!hasAnyValue)
            {
                MessageBox.Show("Please fill in at least one detail field before saving.",
                    "No Details Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to save these changes?",
                "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                MessageBox.Show("No changes were saved.");
                return;
            }

            try
            {
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is TextBox tb)
                    {
                        if (string.IsNullOrWhiteSpace(tb.Text)) continue;

                        if (tb.Tag == null)
                        {
                            MessageBox.Show("A detail field is missing its ID information. Skipping.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        dynamic data = tb.Tag;
                        int jdID = data.jobDetailID;
                        int reqItemID = data.requestItemID;
                        string detailValue = tb.Text.Trim();

                        if (detailValue.Length > 50)
                        {
                            MessageBox.Show($"A detail value exceeds 50 characters and cannot be saved:\n\"{detailValue.Substring(0, 30)}...\"",
                                "Value Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();

                            SqlCommand checkCmd = new SqlCommand(@"
                                SELECT COUNT(*) FROM ItemDetail 
                                WHERE jobDetailID = @jobDetailID 
                                AND requestItemID = @requestItemID", conn);
                            checkCmd.Parameters.AddWithValue("@jobDetailID", jdID);
                            checkCmd.Parameters.AddWithValue("@requestItemID", reqItemID);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (count > 0)
                            {
                                SqlCommand updateCmd = new SqlCommand(@"
                                    UPDATE ItemDetail 
                                    SET detailValue = @detailValue
                                    WHERE jobDetailID = @jobDetailID 
                                    AND requestItemID = @requestItemID", conn);
                                updateCmd.Parameters.AddWithValue("@detailValue", detailValue);
                                updateCmd.Parameters.AddWithValue("@jobDetailID", jdID);
                                updateCmd.Parameters.AddWithValue("@requestItemID", reqItemID);
                                updateCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                SqlCommand insertCmd = new SqlCommand(@"
                                    INSERT INTO ItemDetail (detailValue, jobDetailID, requestItemID)
                                    VALUES (@detailValue, @jobDetailID, @requestItemID)", conn);
                                insertCmd.Parameters.AddWithValue("@detailValue", detailValue);
                                insertCmd.Parameters.AddWithValue("@jobDetailID", jdID);
                                insertCmd.Parameters.AddWithValue("@requestItemID", reqItemID);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while saving details:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while saving details:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void cmbFilter_A_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = cmbFilter_A.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    cmbJobType.SelectedIndex = -1;
                    break;
                case 1:
                    cmbJobType.SelectedIndex = -1;
                    cmbStatus_A.SelectedIndex = -1;
                    break;
                case 2:
                    cmbStatus_A.SelectedIndex = -1;
                    break;
            }
        }

        private void btnFilter_A_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = cmbFilter_A.SelectedIndex;
                switch (idx)
                {
                    case 0: // Filter by status
                        if (cmbStatus_A.SelectedItem == null)
                        {
                            MessageBox.Show("Please select a status to filter by.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        runQuery2(DateTime.Now, "", cmbStatus_A.SelectedItem.ToString());
                        break;

                    case 1: // Filter by job type
                        if (cmbJobType.SelectedItem == null)
                        {
                            MessageBox.Show("Please select a job type to filter by.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        runQuery2(DateTime.Now, cmbJobType.SelectedItem.ToString(), "");
                        break;

                    case 2: // Filter by date
                        if (dateTimePicker1.Value.Date > DateTime.Now.Date)
                        {
                            MessageBox.Show("Filter date cannot be in the future.",
                                "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        runQuery2(dateTimePicker1.Value, "", "");
                        break;

                    default:
                        MessageBox.Show("Please select a filter option.",
                            "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while applying filter:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenMap(tbLat_A, tbLong_A);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            runQuery(textBox3, dgvJoinPictures);
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void dgvJoinPictures_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJoinPictures.Rows[e.RowIndex].Cells["jobRequestID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job Request ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobRequestID = Convert.ToInt32(cell.Value);
                label11.Text = "Job requestID selected: " + jobRequestID.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting job request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvJoinPictures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                ofd.Title = "Select a Site Photo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(ofd.FileName))
                    {
                        MessageBox.Show("The selected file could not be found.",
                            "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    selectedPhotoPath = ofd.FileName;
                    pbPreview.Image = Image.FromFile(selectedPhotoPath);
                    pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the selected photo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (jobRequestID == 0)
            {
                MessageBox.Show("Please select a Job Request from the table before uploading a photo.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedPhotoPath == null)
            {
                MessageBox.Show("Please browse and select a photo first.",
                    "No Photo Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(selectedPhotoPath))
            {
                MessageBox.Show("The selected photo file no longer exists. Please browse and select it again.",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                selectedPhotoPath = null;
                pbPreview.Image = Image.FromFile(defaultImagePath);
                return;
            }
            if (!rbBefore.Checked && !rbAfter.Checked)
            {
                MessageBox.Show("Please select whether this is a BEFORE or AFTER photo.",
                    "Photo Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Upload this photo as '{(rbBefore.Checked ? "BEFORE" : "AFTER")}' for Job Request ID {jobRequestID}?",
                "Confirm Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string photoType = rbBefore.Checked ? "BEFORE" : "AFTER";
                string folderPath = Path.Combine(Application.StartupPath, "SitePhotos", $"JobRequest_{jobRequestID}");
                Directory.CreateDirectory(folderPath);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string extension = Path.GetExtension(selectedPhotoPath);
                string newFileName = $"{photoType}_{timestamp}{extension}";
                string destPath = Path.Combine(folderPath, newFileName);

                File.Copy(selectedPhotoPath, destPath, overwrite: true);

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO SitePhoto (photoType, filePath, uploadDate, jobRequestID)
                    VALUES (@photoType, @filePath, GETDATE(), @jobRequestID)", conn))
                {
                    cmd.Parameters.AddWithValue("@photoType", photoType);
                    cmd.Parameters.AddWithValue("@filePath", destPath);
                    cmd.Parameters.AddWithValue("@jobRequestID", jobRequestID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Photo uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                selectedPhotoPath = null;
                pbPreview.Image = Image.FromFile(defaultImagePath);
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                rbBefore.Checked = true;
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("File error while uploading photo:\n" + ioEx.Message,
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while saving photo record:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while uploading photo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddSitePhoto_Click(object sender, EventArgs e)
        {
            if (jobRequestID == 0)
            {
                MessageBox.Show("Please select a Job Request from the table before adding photos.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tabControl1.SelectedIndex = 4;
            label11.Text = "Job requestID selected: " + jobRequestID.ToString();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblLat_Click(object sender, EventArgs e)
        {

        }

        private void lblLong_Click(object sender, EventArgs e)
        {

        }

        private void lblSAddress_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenMap(tbLat, tbLong);
        }
        private string GetExistingDetailValue(int jobDetailID, int requestItemID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT detailValue 
                    FROM ItemDetail 
                    WHERE jobDetailID = @jobDetailID 
                    AND requestItemID = @requestItemID", conn))
                {
                    cmd.Parameters.AddWithValue("@jobDetailID", jobDetailID);
                    cmd.Parameters.AddWithValue("@requestItemID", requestItemID);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading existing detail value:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading detail value:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void OpenMap(TextBox latBox, TextBox lngBox)
        {
            try
            {
                using (MapPopupForm mapWindow = new MapPopupForm())
                {
                    if (mapWindow.ShowDialog() == DialogResult.OK)
                    {
                        latBox.Text = mapWindow.SelectedLatitude.ToString("F6");
                        lngBox.Text = mapWindow.SelectedLongitude.ToString("F6");
                        MessageBox.Show("Location coordinates successfully captured from the map pin!",
                            "Capture Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the map:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void dgv_clientJoinJobRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 mainMenu = this.MdiParent as Form1;

            if (mainMenu == null)
            {
                MessageBox.Show("Could not access the main menu.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mainMenu.FormSetup(new client_MainForm(0));
        }

        private void tbLong_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLong.Text))
            {
                tbLong.Text = "";
                groupWst1DataSet1.JobRequest.Rows[0]["longitude"] = DBNull.Value;
            }
        }

        private void tbLat_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLat.Text))
            {
                // Cancel the binding update back to the dataset
                tbLat.Text = "";
                groupWst1DataSet1.JobRequest.Rows[0]["latitude"] = DBNull.Value;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobRequestID = 0;
        }
    }
}
