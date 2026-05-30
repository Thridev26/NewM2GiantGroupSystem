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
    public partial class EditJobRequest_A : Form
    {
        int jobRequestID;
        public EditJobRequest_A()
        {
            InitializeComponent();
        }
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

        private void EditJobRequest_A_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.jobRequestTableAdapter1.Fill(this.groupWst1DataSet1.JobRequest);

            runQuery();

            //whichever row is selected in the datagridview will be highlighted in green and the entire row will be
            //highlighted 
            dgv_clientJoinJobRequest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_clientJoinJobRequest.DefaultCellStyle.SelectionBackColor = Color.Green;
        }

        private void tbSearchValue_A_TextChanged(object sender, EventArgs e)
        {
            runQuery();
        }

        private void cmbRS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_clientJoinJobRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            jobRequestID = Convert.ToInt32(dgv_clientJoinJobRequest.Rows[e.RowIndex].Cells["jobRequestID"].Value);
           lbl_ID.Text = "Selected Job Request ID: " + jobRequestID.ToString();
            jobRequestTableAdapter1.FillByID(this.groupWst1DataSet1.JobRequest, jobRequestID);
        }

        private void btnSave_Click(object sender, EventArgs e)
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
    }
}
