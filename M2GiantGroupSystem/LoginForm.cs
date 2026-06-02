using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace M2GiantGroupSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username;
            username = usernameBox.Text;
            MessageBox.Show("Welcome " + usernameBox.Text);
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //go through all forms in the application and close them
            foreach (Form form in Application.OpenForms)
            {
                form.Close();
            }
            Application.Exit();

        }

        private void checkPasswordBox_CheckedChanged(object sender, EventArgs e)
        {
            // If the checkbox is checked, hide the asterisks (show the text)
            if (checkPasswordBox.Checked)
            {
                passwordBox.UseSystemPasswordChar = false;
            }
            // If it's unchecked, bring the asterisks back
            else
            {
                passwordBox.UseSystemPasswordChar = true;
            }
        }

        //This method is for demonstration purposes only. It shows how to hash a password and update it in the database.
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    // 1. Generate the secure hash for an existing password
        //    string realPassword = "branch16"; // The password you want the user to have
        //    string secureHash = BCrypt.Net.BCrypt.HashPassword(realPassword);

        //    // 2. Update your database with this new, long, secure string
        //    // Replace '1' with the specific staffID you are updating
        //    string updateQuery = "UPDATE Staff SET passwordHash = @hash WHERE staffID = 5";

        //    using (SqlConnection conn = new SqlConnection("Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;Trust Server Certificate=True"))
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@hash", secureHash);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
