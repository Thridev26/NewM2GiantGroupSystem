using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void FormSetup(Form myForm)
        {
            // if a childform exists, close it 
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            // Turn off menu strip merging to stop the ugly layout artifacts at the top
            this.MainMenuStrip = null;

            // set the parent form of the child window 
            myForm.MdiParent = this;

            myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.WindowState = FormWindowState.Maximized;
            myForm.Dock = DockStyle.Fill;

            myForm.Show();  // display the child window
        }

        private void createQuoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 2. Instantiate your child object form
            Quotation quotation = new Quotation();
            // 3. Route it through your dynamic MDI layout manager
            FormSetup(quotation);
        }

        private void allocateAssetsToJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllocateAssetStafftoJob allocateAssetStafftoJob = new AllocateAssetStafftoJob();
            FormSetup(allocateAssetStafftoJob);
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new client_MainForm(0));
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
           //close this form and all it's childre
            this.Close();

            //show the login form again
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void updateClientTSItem_Click(object sender, EventArgs e)
        {
            FormSetup(new client_MainForm(1));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void captureJobRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new jobRequestMain_A(0));
        }

        private void updateJobRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new jobRequestMain_A(1));
        }

        private void captureDetailsForRequestedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new jobRequestMain_A(2));
        }

        private void viewJobRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new jobRequestMain_A(3));
        }
    }
}
