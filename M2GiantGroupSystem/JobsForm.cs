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
    public partial class JobsForm : Form
    {
        int tabIndex;

        public JobsForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }

        private void JobsForm_Load(object sender, EventArgs e)
        {
            // Set the active tab index passed via constructor
            tabControl1.SelectedIndex = tabIndex;

            // CRITICAL: Tells WinForms that YOU will custom draw the headers
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;

            // Subscribes the tab control to the custom paint event method below
            tabControl1.DrawItem += tabControl1_DrawItem;

            // Configures a standard uniform size for all the tab headers
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Get the specific TabPage being rendered and its boundary rectangle
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);

            // Configure the font style
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // Base color for inactive tabs (Muted Mint/White mix)
            Color backColor = Color.Honeydew;

            // Highlight color ONLY for the actively selected tab (Solid Light Green)
            if (e.Index == tabControl1.SelectedIndex)
            {
                backColor = Color.LightGreen;
            }

            // Set text color
            Color textColor = Color.Black;

            // 1. Paint the background rectangle
            using (Brush b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, tabRect);
            }

            // 2. Paint the custom border outline around the tab
            using (Pen p = new Pen(Color.DarkGreen, 1))
            {
                e.Graphics.DrawRectangle(p, tabRect);
            }

            // 3. Paint the Tab text precisely centered horizontally and vertically
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