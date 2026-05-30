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
    public partial class ViewJobRequests : Form
    {
        public ViewJobRequests()
        {
            InitializeComponent();
        }
        int selectedIndex;
        public void runQuery(DateTime d, string s1, string s2)
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
    (jt.jobTypeName =" + "'" + s1+ "')"  
    +"OR ( jr.dateRecieved=" + "'" + d + "')"
+"OR (jr.status=" + "'" + s2+ "')";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvJoin.DataSource = dt;
            }
        }

        private void ViewJobRequests_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            runQuery(DateTime.Now, "", "");

        }

        private void btnFilter_A_Click(object sender, EventArgs e)
        {
            int selectedIndex = cmbFilter_A.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    runQuery(DateTime.Now, "", cmbStatus_A.SelectedItem.ToString());
             
                    break;
                case 1:
                    runQuery(DateTime.Now, cmbJobType.SelectedItem.ToString(), "");
          
                    break;
                case 2:
                    runQuery(dateTimePicker1.Value, "", "");
                  
                    break;
                
                default:
                    MessageBox.Show("Please select a filter option.");
                    break;

            }
           
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
    }
}
