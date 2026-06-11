using M2GiantGroupSystem;
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
    public partial class HelpForm_D : Form
    {

        public HelpForm_D()
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
        }
        private int clientsSearchIndex = 0;
        private int assetSearchIndex = 0;
        private int jobSearchIndex = 0;
        private int jobRequestIndex = 0;
        private int reportsIndex = 0;
        private int scheduleIndex = 0;
        private int invoiceIndex = 0;
        private int quoteIndex = 0;
        private int staffIndex = 0;
        private int maintenenceIndex = 0;
        private int photosIndex = 0;

        private void HelpForm_D_Load(object sender, EventArgs e)
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(100, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            //set all to read only
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            richTextBox3.ReadOnly = true;
            richTextBox4.ReadOnly = true;
            richTextBox5.ReadOnly = true;
            richTextBox6.ReadOnly = true;
            richTextBox7.ReadOnly = true;
            richTextBox8.ReadOnly = true;
            richTextBox9.ReadOnly = true;
            richTextBox10.ReadOnly = true;

            clientsText.ReadOnly = true;

            //set all to the same font
            richTextBox1.Font = new Font("Segoe UI", 12,FontStyle.Bold);
            richTextBox2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox4.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox5.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox7.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox6.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox8.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox9.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            richTextBox10.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            clientsText.Font = new Font("Segoe UI", 12, FontStyle.Bold);


          richTextBox6.Text = "ASSETS TAB\r\n\r\nPurpose:\r\nThis tab is used to manage machine asset records. Users can add new owned assets, update existing owned asset details, delete owned asset records, add new hired assets, update existing hired asset details, and delete hired asset records.\r\n\r\n--------------------------------------------------\r\n\r\nADDING AN OWNED ASSET\r\n\r\n1. Open the \"Owned Assets\" tab.\r\n2. Navigate to the Add Asset panel.\r\n3. Enter the asset's:\r\n   • Serial Number\r\n   • Asset Type\r\n   • Purchase Date\r\n   • Current Condition\r\n   • Next Service Date\r\n   • Status\r\n4. Click the \"Add Asset\" button.\r\n5. Confirm the details when prompted.\r\n6. A success message will appear if the asset is added successfully.\r\n\r\nNote:\r\n• All fields are required.\r\n• Serial Number must be entered before submitting.\r\n\r\n--------------------------------------------------\r\n\r\nUPDATING AN OWNED ASSET\r\n\r\n1. Click a row in the Owned Asset table to select an asset.\r\n2. The asset's details will load automatically into the fields.\r\n3. Modify the required information.\r\n4. Click \"Update Asset\".\r\n5. Confirm the update when prompted.\r\n6. A success message will appear if the update is successful.\r\n\r\nNote:\r\n• All fields are required before updating.\r\n• You must select an asset from the table before editing.\r\n\r\n--------------------------------------------------\r\n\r\nDELETING AN OWNED ASSET\r\n\r\n1. Navigate to the Delete Asset panel.\r\n2. Begin typing a Serial Number in the search field.\r\n3. The asset's details will load automatically via AutoComplete.\r\n4. Review the loaded details to confirm the correct asset is selected.\r\n5. Click \"Delete Asset\".\r\n6. Confirm the deletion when prompted.\r\n7. A success message will appear if the asset is deleted successfully.\r\n\r\nNote:\r\n• An asset must be loaded before deletion is allowed.\r\n• Deletion is permanent and cannot be undone.\r\n\r\n--------------------------------------------------\r\n\r\nSEARCHING OWNED ASSETS\r\n\r\n1. Type into the Serial Number search field above the Owned Asset table.\r\n2. Matching records will automatically appear in the table.\r\n\r\n--------------------------------------------------\r\n\r\nVIEWING OWNED ASSETS\r\n\r\n1. Browse the Owned Asset table on the Owned Assets tab.\r\n2. Click a row to load the asset's full details into the fields.\r\n\r\nStatus Colour Coding:\r\n• Available — Light Green\r\n• In Use — Light Coral (Red)\r\n• Under Maintenance — Light Yellow\r\n\r\nService Date Warning:\r\n• Assets with a service date within 30 days are highlighted in Yellow.\r\n• A warning message will appear on form load listing all affected assets.\r\n\r\n--------------------------------------------------\r\n\r\nHIRED ASSET MODULE\r\n\r\n--------------------------------------------------\r\n\r\nADDING A HIRED ASSET\r\n\r\n1. Open the \"Hired Assets\" tab.\r\n2. Navigate to the Add Hired Asset panel.\r\n3. Enter the asset's:\r\n   • Supplier Name\r\n   • Hire Date\r\n   • Return Date\r\n   • Hire Cost\r\n   • Equipment Type\r\n   • Status\r\n4. Click the \"Add Asset\" button.\r\n5. Confirm the details when prompted.\r\n6. A success message will appear if the asset is added successfully.\r\n\r\nNote:\r\n• All fields are required.\r\n• Hire Cost must be a valid number.\r\n\r\n--------------------------------------------------\r\n\r\nUPDATING A HIRED ASSET\r\n\r\n1. Click a row in the Hired Asset table to select an asset.\r\n2. The asset's details will load automatically into the fields.\r\n3. Modify the required information.\r\n4. Click \"Update\".\r\n5. Confirm the update when prompted.\r\n6. A success message will appear if the update is successful.\r\n\r\nNote:\r\n• Supplier Name, Hire Cost, Equipment Type, and Status are all required.\r\n• Hire Cost must be a valid number.\r\n\r\n--------------------------------------------------\r\n\r\nDELETING A HIRED ASSET\r\n\r\n1. Navigate to the Delete Hired Asset panel.\r\n2. Begin typing a Supplier Name in the search field.\r\n3. The asset's details will load automatically via AutoComplete.\r\n4. Review the loaded details to confirm the correct asset is selected.\r\n5. Click \"Delete Asset\".\r\n6. Confirm the deletion when prompted.\r\n7. A success message will appear if the asset is deleted successfully.\r\n\r\nNote:\r\n• A Supplier Name must be entered and an asset loaded before deletion is allowed.\r\n• Deletion is permanent and cannot be undone.\r\n\r\n--------------------------------------------------\r\n\r\nVIEWING HIRED ASSETS\r\n\r\n1. Browse the Hired Asset table on the Hired Assets tab.\r\n2. Click a row to load the asset's full details into the fields.\r\n\r\nStatus Colour Coding:\r\n• Active — Light Green\r\n• Returned — Light Coral (Red)\r\n• Overdue — Orange\r\n• Damaged — Light Yellow\r\n\r\nReturn Date Warning:\r\n• Assets with a return date within 7 days are highlighted in Yellow.\r\n• A warning message will appear when switching to the Hired Assets tab listing all affected assets.\r\n\r\n--------------------------------------------------\r\n\r\nIMPORTANT NOTES\r\n\r\n• All required fields must be completed before adding, updating, or deleting a record.\r\n• Hire Cost must be entered as a valid number.\r\n• Changes are only saved after confirmation.\r\n• Deletions are permanent and cannot be reversed.\r\n• Asset details load automatically when selecting a record from the table or typing in a search field.\r\n• Service and return date warnings appear automatically to alert users of upcoming deadlines.\r\n";
          clientsText.Text = "CLIENTS TAB\r\n\r\nPurpose:\r\nThis tab is used to manage client information. Users can add new clients, update existing client details, search for clients, filter client records, archive clients, and reactivate archived clients.\r\n\r\n--------------------------------------------------\r\n\r\nADDING A CLIENT\r\n\r\n1. Open the \"Add Client\" tab.\r\n2. Enter the client's:\r\n   • First Name\r\n   • Surname\r\n   • Email Address\r\n   • Phone Number\r\n   • Client Type\r\n   • Status\r\n3. Click the \"Add Client\" button.\r\n4. Confirm the action when prompted.\r\n5. A success message will appear if the client is added successfully.\r\n\r\nNote:\r\n• Email addresses must be unique.\r\n• South African phone numbers only are accepted.\r\n• All fields are required.\r\n\r\n--------------------------------------------------\r\n\r\nUPDATING A CLIENT\r\n\r\n1. Open the \"Update Client\" tab.\r\n2. Select a search criterion (Name, Surname, Email, or Phone).\r\n3. Enter a search value.\r\n4. Select a client from the search results list.\r\n5. The client's details will load automatically.\r\n6. Modify the required information.\r\n7. Click \"Update\".\r\n8. Confirm the update when prompted.\r\n\r\nNote:\r\n• Update controls remain disabled until a client is selected.\r\n• Modified fields are highlighted to indicate changes.\r\n\r\n--------------------------------------------------\r\n\r\nVIEWING CLIENTS\r\n\r\n1. Open the \"View Clients\" tab.\r\n2. Browse the client list displayed in the table.\r\n3. Click a row to view detailed client information.\r\n4. The selected client's details will appear in the information panel.\r\n\r\nColour Coding:\r\n• Residential clients - Light Blue\r\n• Commercial clients - Light Cream\r\n• Government clients - Light Green\r\n• Archived clients - Grey\r\n\r\n--------------------------------------------------\r\n\r\nSEARCHING FOR CLIENTS\r\n\r\n1. Select a search category from the Search By dropdown.\r\n2. Enter a search value.\r\n3. Matching records will automatically appear in the table.\r\n\r\nAvailable Search Categories:\r\n• Name\r\n• Surname\r\n• Email\r\n• Phone Number\r\n• Client Type\r\n• Status\r\n• Date Added\r\n\r\n--------------------------------------------------\r\n\r\nFILTERING CLIENTS\r\n\r\n1. Select a Client Type filter.\r\n2. Select a Status filter.\r\n3. The client table will update automatically.\r\n4. Click \"Clear Filters\" to remove all filters.\r\n\r\n--------------------------------------------------\r\n\r\nARCHIVING A CLIENT\r\n\r\n1. Select a client from the client table.\r\n2. Click \"Archive Client\".\r\n3. Confirm the action.\r\n4. The client's status will change to Archived.\r\n\r\nNote:\r\n• Archived clients are not permanently deleted.\r\n• Archived clients can be reactivated later.\r\n\r\n--------------------------------------------------\r\n\r\nREACTIVATING A CLIENT\r\n\r\n1. Select an archived client.\r\n2. Click \"Activate Client\".\r\n3. Confirm the action.\r\n4. The client's status will change back to Active.\r\n\r\n--------------------------------------------------\r\n\r\nIMPORTANT NOTES\r\n\r\n• All required fields must be completed.\r\n• Email addresses must be unique.\r\n• Invalid phone numbers will be rejected.\r\n• Archived clients cannot be archived again.\r\n• Active clients cannot be activated again.\r\n• Changes are only saved after confirmation.";
            richTextBox1.Text = "hi";
            richTextBox2.Text= "hi";
            richTextBox3.Text = "hi";
            richTextBox4.Text = "hi";
            richTextBox5.Text = "hi";
            richTextBox7.Text = "hi";
            
            richTextBox8.Text = "hi";
            richTextBox9.Text = "hi";
            richTextBox10.Text = "hi";


        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.SetDarkMode(!ThemeManager.IsDarkMode);
            UpdateToggleLabel();
        }

        private void trkFontSize_Scroll(object sender, EventArgs e)
        {
           
        }

        private void UpdateToggleLabel()
        {
           
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

        private void lblFontSize_Click(object sender, EventArgs e)
        {

        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 11, FontStyle.Bold);

            Color backColor = Color.Honeydew;
            Color textColor = Color.Black;

            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.DarkGreen;
                textColor = Color.White;
            }

            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }

        private void HighlightAll(string searchText, RichTextBox rb)
        {
            rb.SelectAll();

            
            rb.SelectionBackColor = Color.FromArgb(251, 253, 223); ;
           

            if (string.IsNullOrWhiteSpace(searchText))
                return;

            int start = 0;

            while ((start = rb.Text.IndexOf(
                searchText,
                start,
                StringComparison.OrdinalIgnoreCase)) != -1)
            {
                rb.Select(start, searchText.Length);
                rb.SelectionBackColor = Color.Yellow;
                start += searchText.Length;
            }

            
        }

        private void seacrchClients_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(seacrchClients.Text, clientsText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(clientsText, seacrchClients.Text, ref clientsSearchIndex);
        }
        private void FindNextInRichTextBox(
    RichTextBox rtb,
    string searchText,
    ref int lastIndex)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return;

            int index = rtb.Find(searchText, lastIndex, RichTextBoxFinds.None);

            if (index == -1)
            {
                lastIndex = 0;
                index = rtb.Find(searchText, lastIndex, RichTextBoxFinds.None);
            }

            if (index != -1)
            {
                rtb.Select(index, searchText.Length);
                rtb.ScrollToCaret();
                rtb.Focus();

                lastIndex = index + searchText.Length;
            }
            else
            {
                MessageBox.Show("Text not found.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox1.Text, richTextBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox1, textBox1.Text, ref jobRequestIndex);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox2.Text, richTextBox2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox2, textBox2.Text, ref quoteIndex);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox3.Text, richTextBox3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox3, textBox3.Text, ref scheduleIndex);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox4.Text, richTextBox4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox4, textBox4.Text, ref jobSearchIndex);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox5.Text, richTextBox5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox5, textBox5.Text, ref invoiceIndex);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox6.Text, richTextBox6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox6, textBox6.Text, ref assetSearchIndex);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox7.Text, richTextBox7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox7, textBox7.Text, ref maintenenceIndex);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox8.Text, richTextBox8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox8, textBox8.Text, ref staffIndex);
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox9.Text, richTextBox9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox9, textBox9.Text, ref reportsIndex);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            HighlightAll(textBox10.Text, richTextBox10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FindNextInRichTextBox(richTextBox10, textBox10.Text, ref photosIndex);
        }

        private void seacrchClients_Enter(object sender, EventArgs e)
        {
            seacrchClients.Clear();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            textBox9.Clear();
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            textBox10.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set all textboxes back to Search...
            textBox1.Text = "Search...";
            textBox2.Text = "Search...";
            textBox3.Text = "Search...";
            textBox4.Text = "Search...";
            textBox5.Text = "Search...";
            textBox6.Text = "Search...";
            textBox7.Text = "Search...";
            textBox8.Text = "Search...";
            textBox9.Text = "Search...";
            textBox10.Text = "Search...";
            seacrchClients.Text = "Search...";
        }
    }
}



