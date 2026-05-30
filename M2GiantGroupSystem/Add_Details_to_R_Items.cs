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

namespace M2GiantGroupSystem
{
    public partial class Add_Details_to_R_Items : Form
    {
        public Add_Details_to_R_Items()
        {
            InitializeComponent();
        }
        //global variable to store jobRequestID
        int jobRequestID;
        string jobName;
        int jobTypeID;
        int requestItemID;
        public void runQuery()
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT Client.clientName, Client.clientSurname, Client.emailAddress, JobRequest.dateRecieved, JobRequest.siteAddress, JobRequest.siteEvaluationDate,jobRequestID " +

                 " FROM Client INNER JOIN JobRequest ON Client.clientID = JobRequest.clientID " +
                     " WHERE clientName LIKE " + "'%" + tbSearchValue_A.Text + "%'" +
                     "OR clientSurname LIKE  " + "'%" + tbSearchValue_A.Text + "%'" +
                     "OR siteAddress LIKE  " + "'%" + tbSearchValue_A.Text + "%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv_clientJoinJobRequest.DataSource = dt;
            }
        }

        private void Add_Details_to_R_Items_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;
            this.WindowState = FormWindowState.Maximized;
            runQuery();
           }

        private void tbSearchValue_A_TextChanged(object sender, EventArgs e)
        {
            runQuery();
        }

        private void dgv_clientJoinJobRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0)
            {
                return;
            }//if the user clicks the header row index will be -1 and program will crash if we try to access the cell


            flowLayoutPanel1.Controls.Clear();
            ///clear the flowlayoutpanel before adding new controls for the selected job request

            jobRequestID = Convert.ToInt32(
                dgv_clientJoinJobRequest.Rows[e.RowIndex]
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgv_clientJoinJobRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
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
    }
}
