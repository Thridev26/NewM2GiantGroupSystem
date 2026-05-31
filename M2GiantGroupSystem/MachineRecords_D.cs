using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace M2GiantGroupSystem
{
    public partial class MachineRecords_D : Form
    {
        public MachineRecords_D()
        {
            InitializeComponent();
        }

        private void MachineRecords_D_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = TabIndex;
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            tabControl1.ItemSize = new Size(300, 30);
            tabControl1.SizeMode = TabSizeMode.Fixed;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                //    textBox2.Text = row.Cells[0].Value?.ToString() ?? "";
                //    textBox3.Text = row.Cells[1].Value?.ToString() ?? "";
                //    comboBox3.Text = row.Cells[2].Value?.ToString() ?? "";

                //    dateTimePicker2.Value = Convert.ToDateTime(row.Cells[3].Value);

                //    comboBox2.Text = row.Cells[4].Value?.ToString() ?? "";

                //    dtServicedt.Value = Convert.ToDateTime(row.Cells[5].Value);

                //    comboBox1.Text = row.Cells[6].Value?.ToString() ?? "";
                //}
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
            //string status = row.Cells[6].Value?.ToString() ?? "";

            //switch (status)
            //{
            //    case "Available":
            //        row.Cells[6].Style.BackColor = Color.LightGreen;
            //        row.Cells[6].Style.ForeColor = Color.Black;
            //        break;
            //    case "In Use":
            //        row.Cells[6].Style.BackColor = Color.LightCoral;
            //        row.Cells[6].Style.ForeColor = Color.Black;
            //        break;
            //    case "Under Maintenance":
            //        row.Cells[6].Style.BackColor = Color.LightYellow;
            //        row.Cells[6].Style.ForeColor = Color.Black;
            //        break;
            //    default:
            //        row.Cells[6].Style.BackColor = Color.White;
            //        row.Cells[6].Style.ForeColor = Color.Black;
            //        break;

        }
    } }
}

