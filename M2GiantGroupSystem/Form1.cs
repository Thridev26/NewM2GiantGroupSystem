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
            
            myForm.Dock = DockStyle.Fill;
          //  myForm.WindowState = FormWindowState.Maximized;
            myForm.Show();  // display the child window
            AppState.selectedIdCalendar = -8;
        }

        private void createQuoteToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            
            FormSetup(new Quotation(0));
            
        }

        private void allocateAssetsToJobsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FormSetup(new AllocateAssetStafftoJob(0));
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

        private void addEditAssetRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineRecords_D asset = new MachineRecords_D();
            asset.ShowDialog();
            // FormSetup(asset);
        }

        private void viewAllAllocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new AllocateAssetStafftoJob(1));
        }

        private void viewAllAllocationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Calendar calendarform = new Calendar();
            FormSetup(calendarform);
        }

        private void viewAllQuotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Quotation(1));
        }

        private void editQuotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Quotation(1));
        }

        private void svaeQuoteAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Quotation(1));
        }

        private void printQuoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Quotation(1));
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            FormSetup(new client_MainForm(2));
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            FormSetup(new reportsMainForm());
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void addEditJobDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new JobsForm(0));
        }

        private void viewJobProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new JobsForm(1));
        }

        private void editJobProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new JobsForm(2));
        }

        private void addEditStaffDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Staff(0));
        }

        private void addStaffDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Staff(1));
        }
    }
}
