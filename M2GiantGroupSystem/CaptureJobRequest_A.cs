//using M2GiantGroupSystem.GroupWst1DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_Design;

namespace M2GiantGroupSystem
{
    public partial class CaptureJobRequest_A : Form
    {
        public CaptureJobRequest_A()
        {
            InitializeComponent();
        }
        int numberOfResults = 0;
        string value;
        int clientID;
        int jobRequestID;
        void loadClientDataIntoTextboxes()
        {
            if (numberOfResults==1 )
            {
                clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, this.groupWst1DataSet1.Client[0].clientID);
                return;
            }
            if (lbSearchResults.SelectedIndex>-1)
            {
                string selectedItem = lbSearchResults.SelectedItem.ToString();
                string[] parts = selectedItem.Split(':');
                int id = int.Parse(parts[0]);
                clientTableAdapter1.FillByID(this.groupWst1DataSet1.Client, id);

            }
        }

        void loadListBox(int i)
        {
           
            clientID = this.groupWst1DataSet1.Client[i].clientID;
            lbSearchResults.Items.Add(clientID + ":" + value);
        }

        private void lblFindClient_A_Click(object sender, EventArgs e)
        {

        }

        private void lblSearchBy_A_Click(object sender, EventArgs e)
        {

        }

        private void cmbCriteria_A_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbCriteria_A.SelectedIndex;
            switch (index)
            {
                case 0:
                    lblSearchBy_A.Text = "Search by Client Name";
                    break;
                case 1:
                    lblSearchBy_A.Text = "Search by Client Surname";
                    break;
                case 2:
                    lblSearchBy_A.Text = "Search by Client Email";
                    break;
                case 3:
                    lblSearchBy_A.Text = "Search by Client Phone";
                    break;

                default:
                    lblSearchBy_A.Text = "Search by...";
                    break;

            }
        }

        private void tbSearchValue_A_TextChanged(object sender, EventArgs e)
        {
             int index = cmbCriteria_A.SelectedIndex;
            switch (index)
            {
                case 0:
                    lbSearchResults.Items.Clear();
                    clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text,"", "", "");
                    numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                    //for all rows found add name to listbox
                    for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, tbSearchValue_A.Text, "", "", ""); i++)
                    {
                        value = this.groupWst1DataSet1.Client[i].clientName;
                        loadListBox(i);
                    }

                    break;
                case 1:
                    lbSearchResults.Items.Clear();
                    clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", tbSearchValue_A.Text, "", "");
                    numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                    for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", tbSearchValue_A.Text, "", ""); i++)
                    {
                        value = this.groupWst1DataSet1.Client[i].clientSurname;
                        loadListBox(i);
                    }
                    break;
                case 2:
                    lbSearchResults.Items.Clear();
                    clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", tbSearchValue_A.Text, "");
                    numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                    for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", tbSearchValue_A.Text, ""); i++)
                    {
                        value = this.groupWst1DataSet1.Client[i].emailAddress;
                        loadListBox(i);
                    }
                    break;
                case 3:
                    lbSearchResults.Items.Clear();

                    clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", "", tbSearchValue_A.Text);
                    numberOfResults = this.groupWst1DataSet1.Client.Rows.Count;
                    for (int i = 0; i < clientTableAdapter1.FillByNameSurnameEmailPhone(this.groupWst1DataSet1.Client, "", "", "", tbSearchValue_A.Text); i++)
                    {
                        value = this.groupWst1DataSet1.Client[i].phoneNumber;
                        loadListBox(i);
                    }
                    break;

                default:
                    
                    break;

            }//switch

            loadClientDataIntoTextboxes();

        }//text changed

        private void CaptureJobRequest_A_Load(object sender, EventArgs e)
        {
            //make form maximised
            this.WindowState = FormWindowState.Maximized;
            lbSearchResults.Items.Clear();
        }

        private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClientDataIntoTextboxes();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (tbAddress_A.Text != "" && cmbRequestSource_A.SelectedIndex != -1 && cmbUrgencyLevel_A.SelectedIndex != -1)

            {

                jobRequestID= Convert.ToInt32(jobRequestTableAdapter1.InsertQuery(clientID, tbAddress_A.Text, cmbRequestSource_A.SelectedItem.ToString(), cmbUrgencyLevel_A.SelectedItem.ToString()));
               
                // MessageBox.Show("Inquiry saved successfully! Job Request ID: " + jobRequestID);


                foreach (var item in clbItems.CheckedItems)
                {

                    string itemString = item.ToString();

                    this.jobTypeTableAdapter1.FillByName(this.groupWst1DataSet1.JobType, itemString);

                    int jobTypeID = Convert.ToInt32(this.groupWst1DataSet1.JobType.Rows[0]["JobTypeID"]);




                    requestItemTableAdapter1.InsertQuery(jobRequestID, jobTypeID);
                    this.requestItemTableAdapter1.Fill(this.groupWst1DataSet1.RequestItem);


                }
                MessageBox.Show("Inquiry with requested items saved successfully! ");
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields (Site address, request source and urgency level) before saving the inquiry.");
            }
        }

        private void btnDisplayMap_A_Click(object sender, EventArgs e)
        {
            // 1. Open the map form as a clean modal popup window
            using (MapPopupForm mapWindow = new MapPopupForm())
            {
                // 2. Display the map window. If the user drops a pin, it returns OK and closes automatically
                if (mapWindow.ShowDialog() == DialogResult.OK)
                {
                    // 3. Instantly fill your main form text boxes with the captured coordinates!
                    // Change these to match your exact textbox names if they are different (e.g. txtLat)
                    tbLat_A.Text = mapWindow.SelectedLatitude.ToString("F6");
                    tbLong_A.Text = mapWindow.SelectedLongitude.ToString("F6");

                    // 4. Show a friendly notification
                    MessageBox.Show("Location coordinates successfully captured from the map pin!",
                                    "Capture Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
