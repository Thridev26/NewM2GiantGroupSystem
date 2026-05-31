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

namespace M2GiantGroupSystem
{
    public partial class Add_Client_A : Form
    {
        public Add_Client_A()
        {
            InitializeComponent();
        }

        private void Add_Client_A_Load(object sender, EventArgs e)
        {
            cmb_status.SelectedIndex = 0;
        }

        

        

        private void btn_addClient_Click_1(object sender, EventArgs e)
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
