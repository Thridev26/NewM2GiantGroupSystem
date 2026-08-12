using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class reportsMainForm : Form
    {
        public reportsMainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 mainMenu = (Form1)Application.OpenForms["Form1"];
            if (mainMenu != null)
            {
                mainMenu.FormSetup(new WeeklyExpensesReportForm());
            } //only works if main menu is open, but main menu should always be open when this form is open so shouldnt be an issue
            else
            {
                MessageBox.Show("Main menu not found. Please open the main menu before adding a new client.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 mainMenu = (Form1)Application.OpenForms["Form1"];
            if (mainMenu != null)
            {
                mainMenu.FormSetup(new incomeReportForm());
            } //only works if main menu is open, but main menu should always be open when this form is open so shouldnt be an issue
            else
            {
                MessageBox.Show("Main menu not found. Please open the main menu before adding a new client.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form1 mainMenu = (Form1)Application.OpenForms["Form1"];
            if (mainMenu != null)
            {
                mainMenu.FormSetup(new JobTypeReportForm());
            } //only works if main menu is open, but main menu should always be open when this form is open so shouldnt be an issue
            else
            {
                MessageBox.Show("Main menu not found.");
            }
        }
    }
}
