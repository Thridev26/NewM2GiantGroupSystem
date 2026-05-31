using M2GiantGroupSystem.GroupWst1DataSetTableAdapters;
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
    public partial class client_MainForm : Form
    {
        int tabIndex;
        public client_MainForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }

        private void client_MainForm_Load(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = tabIndex;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btn_addClient_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Are you sure you want to add this client?",
            "Confirm Add",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
              );

            if (result == DialogResult.Yes)
            {
                clientTableAdapter1.InsertQuery(
                cmb_type.SelectedItem.ToString(),
                tb_email.Text,
                cmb_status.SelectedItem.ToString(),
                tb_name.Text,
                tb_surname.Text,
                tb_phone.Text
            );

                MessageBox.Show("Client added successfully!");
            }
            else
            {
                MessageBox.Show("Client was not added.");
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            //Base colours (non-selected tabs)
            Color backColor = Color.Honeydew;


            //Highlight ONLY selected tab
            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            // Text always forest green (or you can change for selected if needed)
            Color textColor = Color.Black;

            // Fill background
            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            //BORDER 
            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            // Draw text
            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                tabFont,
                tabRect,
                textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
    }
}
