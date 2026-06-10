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
            // Initially disable the Return to Menu button because no child forms are open
            toolStripMenuItem13.Enabled = false;
            // Explicitly start the clock
            timer1.Start();
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
           // asset.ShowDialog();
            FormSetup(asset);
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

        private void addASitePhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new jobRequestMain_A(4));
        }

        private void createAnInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new InvoiceReportForm(0));
        }

        public void CloseActiveChild()
        {
            try
            {
                // Because the button is disabled when no child is active, 
                // we no longer need the 'else' block or the 'MessageBox'.
                if (this.ActiveMdiChild != null)
                {
                    this.ActiveMdiChild.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while attempting to close the active form: {ex.Message}",
                                "Developer Exception Log",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            CloseActiveChild();
        }

        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            // If there is an active child, enable the button. If not (null), disable it.
            toolStripMenuItem13.Enabled = (this.ActiveMdiChild != null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // This runs every 1000 milliseconds (1 second)
            // We update the text of the status label to show the current time
            //lblClock.Text = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss");
        }
    }
}
