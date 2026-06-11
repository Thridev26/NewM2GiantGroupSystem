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
            ThemeManager.ThemeChanged += ApplyTheme;
            // Explicitly set initial state
            btnExitToMenu.Enabled = false;
        }

        private void ApplyPermissions()
        {
            int level = UserSession.AccessLevel; // The user Session object is global so this will work 

            // 1. If Owner (6), they already have full access.  
            // We just return early and don't change anything! 
            if (level >= 6) return;

            // 2. If we reach this point, the user is NOT an owner. 
            // Now we apply restrictions for everyone else. 
            switch (level)
            {
                case 5: // Admin: Some locks 
                   toolStripMenuItem4.Enabled = false;
                    toolStripMenuItem5.Enabled = false;
                    break;

                case 4: // Ops Manager: More locks 
                    toolStripMenuItem4.Enabled = false;
                    toolStripMenuItem5.Enabled = false;
                    clientsToolStripMenuItem.Enabled = false;
                    toolStripMenuItem9.Enabled = false;
                    toolStripMenuItem1.Enabled = false;
                    toolStripMenuItem2.Enabled = false;
                    addEditJobDetailsToolStripMenuItem.Enabled = false;
                    editJobProgressToolStripMenuItem.Enabled = false;
                    toolStripMenuItem4.Enabled = false;
                    toolStripMenuItem6.Enabled = false;
                    toolStripMenuItem8.Enabled = false;
                    break;

                default: // Level 3 and below: Complete lockdown – lock all controls if you feel they should 
                    toolStripMenuItem4.Enabled = false;
                    toolStripMenuItem5.Enabled = false;
                    clientsToolStripMenuItem.Enabled = false;
                    toolStripMenuItem9.Enabled = false;
                    toolStripMenuItem1.Enabled = false;
                    toolStripMenuItem2.Enabled = false;
                    addEditJobDetailsToolStripMenuItem.Enabled = false;
                    editJobProgressToolStripMenuItem.Enabled = false;
                    toolStripMenuItem4.Enabled = false;
                    toolStripMenuItem6.Enabled = false;
                    toolStripMenuItem8.Enabled = false;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            ApplyTheme();
            // Initially disable the Return to Menu button because no child forms are open
            btnExitToMenu.Enabled = false;
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

        private void recordMaintenanceLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new Maintenance(0));
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            FormSetup(new HelpForm_D());
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.ThemeChanged -= ApplyTheme;
            base.OnFormClosed(e);
        }
        private void ApplyTheme()
        {
            if (ThemeManager.IsDarkMode)
                ThemeManager.ApplyTheme(this);
        }

        private void siteEvaluationPhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new SitePhotosForm(0));
        }

        private void completedJobPhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new JobSitePhotos(0));
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            // This closes whatever child is currently active
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }

        private void Form1_MdiChildActivate(object sender, EventArgs e)
        {
            // Check if any child forms are currently active
            if (this.ActiveMdiChild == null)
            {
                // No children open, disable the button
                btnExitToMenu.Enabled = false;
            }
            else
            {
                // A child is open, enable the button
                btnExitToMenu.Enabled = true;
            }
        }

        private void trackPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetup(new paymentMain(0));
        }
    }
}
