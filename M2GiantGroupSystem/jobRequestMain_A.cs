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
        //view job requests--------------------------------------------------------------------------------------------------------
         int selectedIndex;
        public void runQuery2(DateTime d, string s1, string s2)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT 
    jr.jobRequestID,
    jr.siteAddress,
    jr.status,
    jr.dateRecieved,

    c.clientName,
    c.clientSurname,
    c.emailAddress,

    jt.jobTypeName

   

FROM JobRequest jr

INNER JOIN Client c 
    ON jr.clientID = c.clientID

LEFT JOIN RequestItem ri 
    ON jr.jobRequestID = ri.jobRequestID

LEFT JOIN JobType jt 
    ON ri.jobTypeID = jt.jobTypeID
WHERE 
    (jt.jobTypeName =" + "'" + s1 + "')"
    + "OR ( jr.dateRecieved=" + "'" + d + "')"
+ "OR (jr.status=" + "'" + s2 + "')";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvJoin.DataSource = dt;
            }
        }




        //add details to request items--------------------------------------------------------------------------------------------------------
        //global variable to store jobRequestID

        string jobName;
        int jobTypeID;
        int requestItemID;




        //capturing job requests--------------------------------------------------------------------------------------------------------

        int numberOfResults = 0;
        string value;
        int clientID;
        int jobRequestID;
        void loadClientDataIntoTextboxes()
        {
            if (numberOfResults == 1)
            {
                clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, this.groupWst1DataSet1.Client[0].clientID);
                return;
            }
            if (lbSearchResults.SelectedIndex > -1)
            {
                string selectedItem = lbSearchResults.SelectedItem.ToString();
                string[] parts = selectedItem.Split(':');
                int id = int.Parse(parts[0]);
                clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, id);

            }
        }

        void loadListBox(int i)
        {

            clientID = this.groupWst1DataSet1.Client[i].clientID;
            lbSearchResults.Items.Add(clientID + ":" + value);
        }
        //

        //update job request--------------------------------------------------------------------------------------------------------
        public void runQuery(TextBox t, DataGridView dgv)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT jobRequestID,Client.clientName, Client.clientSurname, Client.emailAddress, JobRequest.dateRecieved, JobRequest.siteAddress, JobRequest.siteEvaluationDate " +

                 " FROM Client INNER JOIN JobRequest ON Client.clientID = JobRequest.clientID " +
                     " WHERE clientName LIKE " + "'%" + t.Text + "%'" +
                     "OR clientSurname LIKE  " + "'%" + t.Text + "%'" +
                     "OR siteAddress LIKE  " + "'%" + t.Text + "%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv.DataSource = dt;
               
            }
        }


        private void jobRequestMain_A_Load(object sender, EventArgs e)
        {
            //set tab control index to the one passed in the constructor
            tabControl1.SelectedIndex = tabIndex;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            //edit job request----------------------------------------------------------------------------------------------
            this.jobRequestTableAdapter1.Fill(this.groupWst1DataSet1.JobRequest);

         

            //whichever row is selected in the datagridview will be highlighted in green and the entire row will be
            //highlighted 
            dgv_clientJoinJobRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_clientJoinJobRequest.DefaultCellStyle.SelectionBackColor = Color.Green;
            runQuery(textBox3, dgvJoinPictures);

            dgvJoinPictures.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJoinPictures.DefaultCellStyle.SelectionBackColor = Color.Green;
            runQuery(textBox3, dgv_clientJoinJobRequest);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Green;
            runQuery(textBox2, dataGridView1);

            //adding details-------------------------------------------------------------------------------------------------
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;


            //view job requests--------------------------------------------------------------------------------------------------------
            runQuery2(DateTime.Now, "", "");

            label3.Text = "Select site\nevaluation date:";


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnDisplayMap_A_Click(object sender, EventArgs e)
        {
            // 1. Open the map form as a clean modal popup window
            using (MapPopupForm mapWindow = new MapPopupForm())
            {
                // 2. Display the map window. If the user drops a pin, it returns OK and closes automatically
                if (mapWindow.ShowDialog() == DialogResult.OK)
                {
                    // 3. Instantly fill your main form text boxes with the captured coordinates!
                    // Change these to match your exact textbox names if they are different (e.g. txtLat)
                    tbLat_A.Text = mapWindow.SelectedLatitude.ToString("F6");
                    tbLong_A.Text = mapWindow.SelectedLongitude.ToString("F6");

                    // 4. Show a friendly notification
                    MessageBox.Show("Location coordinates successfully captured from the map pin!",
                                    "Capture Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
            int index = cmbCriteria_A.SelectedIndex;
            switch (index)
            {
                case 0:
                    lbSearchResults.Items.Clear();
                    clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", "");
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

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (tbAddress_A.Text != "" && cmbRequestSource_A.SelectedIndex != -1 && cmbUrgencyLevel_A.SelectedIndex != -1)

            {

                jobRequestID = Convert.ToInt32(jobRequestTableAdapter1.InsertQuery(clientID, tbAddress_A.Text, cmbRequestSource_A.SelectedItem.ToString(), cmbUrgencyLevel_A.SelectedItem.ToString()));

                // MessageBox.Show("Inquiry saved successfully! Job Request ID: " + jobRequestID);


                foreach (var item in clbItems.CheckedItems)
                {

                    string itemString = item.ToString();

                    this.jobTypeTableAdapter1.FillByName(this.groupWst1DataSet1.JobType, itemString);

                    int jobTypeID = Convert.ToInt32(this.groupWst1DataSet1.JobType.Rows[0]["JobTypeID"]);




                    requestItemTableAdapter1.InsertQuery(jobRequestID, jobTypeID);
                    this.requestItemTableAdapter1.Fill(this.groupWst1DataSet1.RequestItem);


                }
                MessageBox.Show("Inquiry with requested items saved successfully! ");
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Site address, request source and urgency level) before saving the inquiry.");
            }
        }

        

        private void btnMap_Click(object sender, EventArgs e)
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
            jobRequestID = Convert.ToInt32(dgv_clientJoinJobRequest.Rows[e.RowIndex].Cells["jobRequestID"].Value);
            lbl_ID.Text = "Selected Job Request ID: " + jobRequestID.ToString();
            jobRequestTableAdapter1.FillByID(this.groupWst1DataSet1.JobRequest, jobRequestID);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime selectedDate = dateTimePicker1.Value;
            if (selectedDate < currentDate)
            {
                MessageBox.Show("Please select a date that is not in the past.");
                return;
            }
            try
            {

                jobRequestTableAdapter1.UpdateQuery(tbSiteAddress.Text, decimal.Parse(tbLong.Text), decimal.Parse(tbLat.Text),
                    cmbRS.SelectedItem.ToString(), cmbUL.SelectedItem.ToString(),
                    cmbStatus.SelectedItem.ToString(), dateTimePicker1.Value.ToString(), jobRequestID);

                MessageBox.Show("Changes updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (e.RowIndex < 0)
            {
                return;
            }//if the user clicks the header row index will be -1 and program will crash if we try to access the cell

            label17.Text = "Selected Job Request ID: " + dataGridView1.Rows[e.RowIndex].Cells["jobRequestID"].Value.ToString();

            flowLayoutPanel1.Controls.Clear();
            ///clear the flowlayoutpanel before adding new controls for the selected job request

            jobRequestID = Convert.ToInt32(
                dataGridView1.Rows[e.RowIndex]
                .Cells["jobRequestID"].Value
            );

            requestItemTableAdapter1.FillByJobRequestID(
                groupWst1DataSet1.RequestItem,
                jobRequestID
            );

            foreach (DataRow row in groupWst1DataSet1.RequestItem.Rows)
            {
                requestItemID = Convert.ToInt32(row["requestItemID"]);
                jobTypeID = Convert.ToInt32(row["jobTypeID"]);

                jobDetailTableAdapter1.FillByJobTypeID(
                    groupWst1DataSet1.JobDetail,
                    jobTypeID
                );

                //TITLE LABEL FOR JOB TYPE
                jobTypeTableAdapter1.FillByID(groupWst1DataSet1.JobType, jobTypeID);
                jobName = groupWst1DataSet1.JobType.Rows[0]["jobTypeName"].ToString();

                Label lbl_title = new Label();
                lbl_title.Text = "Job Name: " + jobName;
                lbl_title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl_title.AutoSize = true;
                lbl_title.Margin = new Padding(10, 15, 10, 5);

                flowLayoutPanel1.Controls.Add(lbl_title);

                //need to dynamically add labels and textboxes to the flowlayoutpanel to display job details for this jobtypeID
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

                    // check if a value already exists for this jobDetailID + requestItemID
                    string existingValue = GetExistingDetailValue(
                        Convert.ToInt32(jobDetailRow["jobDetailID"]),
                        requestItemID
                    );

                    if (existingValue != null)
                        tb_jobDetailName.Text = existingValue;

                    flowLayoutPanel1.Controls.Add(lbl_jobDetailName);
                    flowLayoutPanel1.Controls.Add(tb_jobDetailName);
                }

                //VISIBLE SEPARATOR
                Panel separator = new Panel();

                separator.BorderStyle = BorderStyle.Fixed3D;
                separator.Width = flowLayoutPanel1.Width - 30;
                separator.Height = 2;
                separator.Margin = new Padding(5, 15, 5, 15);

                flowLayoutPanel1.Controls.Add(separator);
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
      "Are you sure you want to save these changes?",
      "Confirm Change",
      MessageBoxButtons.YesNo,
      MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                MessageBox.Show("No changes were saved.");
                return;
            }

            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is TextBox tb)
                {
                    if (string.IsNullOrWhiteSpace(tb.Text)) continue;

                    dynamic data = tb.Tag;
                    int jobDetailID = data.jobDetailID;
                    int reqItemID = data.requestItemID;
                    string detailValue = tb.Text;

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        // check if record already exists
                        SqlCommand checkCmd = new SqlCommand(@"
                    SELECT COUNT(*) FROM ItemDetail 
                    WHERE jobDetailID = @jobDetailID 
                    AND requestItemID = @requestItemID", conn);

                        checkCmd.Parameters.AddWithValue("@jobDetailID", jobDetailID);
                        checkCmd.Parameters.AddWithValue("@requestItemID", reqItemID);
                        conn.Open();

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // UPDATE existing
                            SqlCommand updateCmd = new SqlCommand(@"
                        UPDATE ItemDetail 
                        SET detailValue = @detailValue
                        WHERE jobDetailID = @jobDetailID 
                        AND requestItemID = @requestItemID", conn);

                            updateCmd.Parameters.AddWithValue("@detailValue", detailValue);
                            updateCmd.Parameters.AddWithValue("@jobDetailID", jobDetailID);
                            updateCmd.Parameters.AddWithValue("@requestItemID", reqItemID);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // INSERT new
                            SqlCommand insertCmd = new SqlCommand(@"
                        INSERT INTO ItemDetail (detailValue, jobDetailID, requestItemID)
                        VALUES (@detailValue, @jobDetailID, @requestItemID)", conn);

                            insertCmd.Parameters.AddWithValue("@detailValue", detailValue);
                            insertCmd.Parameters.AddWithValue("@jobDetailID", jobDetailID);
                            insertCmd.Parameters.AddWithValue("@requestItemID", reqItemID);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            MessageBox.Show("Successfully saved!");
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

                default:

                    break;

            }
        }

        private void btnFilter_A_Click(object sender, EventArgs e)
        {
            int selectedIndex = cmbFilter_A.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    runQuery2(DateTime.Now, "", cmbStatus_A.SelectedItem.ToString());

                    break;
                case 1:
                    runQuery2(DateTime.Now, cmbJobType.SelectedItem.ToString(), "");

                    break;
                case 2:
                    runQuery2(dateTimePicker1.Value, "", "");

                    break;

                default:
                    MessageBox.Show("Please select a filter option.");
                    break;

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
            // 1. Open the map form as a clean modal popup window
            using (MapPopupForm mapWindow = new MapPopupForm())
            {
                // 2. Display the map window. If the user drops a pin, it returns OK and closes automatically
                if (mapWindow.ShowDialog() == DialogResult.OK)
                {
                    // 3. Instantly fill your main form text boxes with the captured coordinates!
                    // Change these to match your exact textbox names if they are different (e.g. txtLat)
                    tbLat_A.Text = mapWindow.SelectedLatitude.ToString("F6");
                    tbLong_A.Text = mapWindow.SelectedLongitude.ToString("F6");

                    // 4. Show a friendly notification
                    MessageBox.Show("Location coordinates successfully captured from the map pin!",
                                    "Capture Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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
            jobRequestID = Convert.ToInt32(dgvJoinPictures.Rows[e.RowIndex].Cells["jobRequestID"].Value);
            label11.Text= "Job requestID selected: " +jobRequestID.ToString();
        }

        private void dgvJoinPictures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            ofd.Title = "Select a Site Photo";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedPhotoPath = ofd.FileName;
                pbPreview.Image = Image.FromFile(selectedPhotoPath);
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // 1.Check a job request is selected
            if (jobRequestID == 0)
            {
                MessageBox.Show("Please select a Job Request from the table before uploading a photo.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check a photo has been browsed
            if (selectedPhotoPath == null)
            {
                MessageBox.Show("Please browse and select a photo first.",
                    "No Photo Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Check the selected file still exists on disk
            if (!File.Exists(selectedPhotoPath))
            {
                MessageBox.Show("The selected photo file no longer exists. Please browse and select it again.",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                selectedPhotoPath = null;
                pbPreview.Image = Image.FromFile(defaultImagePath);
                return;
            }

            // 4. Check a radio button is selected
            if (!rbBefore.Checked && !rbAfter.Checked)
            {
                MessageBox.Show("Please select whether this is a BEFORE or AFTER photo.",
                    "Photo Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Confirm before uploading
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

                string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

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

                // Reset controls
                selectedPhotoPath = null;
                pbPreview.Image = Image.FromFile(defaultImagePath);
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                rbBefore.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading photo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private string GetExistingDetailValue(int jobDetailID, int requestItemID)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
