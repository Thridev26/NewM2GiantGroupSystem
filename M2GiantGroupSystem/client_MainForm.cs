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
    }
}
