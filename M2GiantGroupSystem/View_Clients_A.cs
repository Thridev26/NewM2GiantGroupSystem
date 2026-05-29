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
    public partial class View_Clients_A : Form
    {
        public View_Clients_A()
        {
            InitializeComponent();
        }

        private void View_Clients_A_Load(object sender, EventArgs e)
        {
            clientTableAdapter1.Fill(this.groupWst1DataSet1.Client);
            
        }
    }
}
