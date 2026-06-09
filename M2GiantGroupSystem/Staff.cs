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
        public Staff()
        {
            InitializeComponent();
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupWst1DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.groupWst1DataSet.Staff);

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
    }
}
