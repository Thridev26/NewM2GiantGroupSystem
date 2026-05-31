using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
