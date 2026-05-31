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
using UI_Design;

namespace M2GiantGroupSystem
{
    public partial class jobRequestMain_A : Form
    {
        int tabIndex;
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
                string sql = @"SELECT Client.clientName, Client.clientSurname, Client.emailAddress, JobRequest.dateRecieved, JobRequest.siteAddress, JobRequest.siteEvaluationDate,jobRequestID " +

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

           // runQuery();

            //whichever row is selected in the datagridview will be highlighted in green and the entire row will be
            //highlighted 
            dgv_clientJoinJobRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_clientJoinJobRequest.DefaultCellStyle.SelectionBackColor = Color.Green;

            //adding details-------------------------------------------------------------------------------------------------
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;


            //view job requests--------------------------------------------------------------------------------------------------------
            runQuery2(DateTime.Now, "", "");


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

                    lbl_jobDetailName.Text =
                        jobDetailRow["detailName"].ToString();

                    lbl_jobDetailName.AutoSize = false;
                    lbl_jobDetailName.Width = 120;

                    lbl_jobDetailName.Margin = new Padding(10, 10, 5, 0);


                    TextBox tb_jobDetailName = new TextBox();

                    tb_jobDetailName.Margin = new Padding(5, 5, 20, 10);
                    tb_jobDetailName.Width = 300;

                    tb_jobDetailName.Name =
                        "tb_jobDetailName" +
                        jobDetailRow["jobDetailID"].ToString();

                    //STORE IMPORTANT IDS INSIDE THE TEXTBOX
                    tb_jobDetailName.Tag = new
                    {
                        jobDetailID = Convert.ToInt32(jobDetailRow["jobDetailID"]),
                        requestItemID = requestItemID
                    };


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
                   MessageBoxIcon.Question
                     );

            if (result == DialogResult.Yes)
            {
                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox tb = (TextBox)control;

                        if (string.IsNullOrWhiteSpace(tb.Text))
                        {
                            continue;
                        }

                        dynamic data = tb.Tag;

                        int jobDetailID = data.jobDetailID;
                        int requestItemID = data.requestItemID;

                        string detailValue = tb.Text;

                        itemDetailTableAdapter1.InsertQuery(
                           detailValue,
                           jobDetailID,
                           requestItemID
                           );



                    }
                }//for each
                MessageBox.Show("Successfully saved!");
            } //if
            else
            {
                MessageBox.Show("No changes were saved.");
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
    }
}
