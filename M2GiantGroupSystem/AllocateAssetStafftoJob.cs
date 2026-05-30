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
    public partial class AllocateAssetStafftoJob : Form
    {
        public AllocateAssetStafftoJob()
        {
            InitializeComponent();
        }

        private void jobBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.jobBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.groupWst1DataSet);

        }

        private void AllocateAssetStafftoJob_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'groupWst1DataSet.JobStaffAssignment' table. You can move, or remove it, as needed.
            this.jobStaffAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobStaffAssignment);
            // TODO: This line of code loads data into the 'groupWst1DataSet.Staff' table. You can move, or remove it, as needed.
            this.staffTableAdapter.Fill(this.groupWst1DataSet.Staff);
            // TODO: This line of code loads data into the 'groupWst1DataSet.JobAssetAssignment' table. You can move, or remove it, as needed.
            this.jobAssetAssignmentTableAdapter.Fill(this.groupWst1DataSet.JobAssetAssignment);
            // TODO: This line of code loads data into the 'groupWst1DataSet.HiredAsset' table. You can move, or remove it, as needed.
            this.hiredAssetTableAdapter.Fill(this.groupWst1DataSet.HiredAsset);
            // TODO: This line of code loads data into the 'groupWst1DataSet.OwnedAsset' table. You can move, or remove it, as needed.
            this.ownedAssetTableAdapter.Fill(this.groupWst1DataSet.OwnedAsset);
            // Create an instance of the adapter containing your new query
            GroupWst1DataSetTableAdapters.QueriesTableAdapter queries =
                new GroupWst1DataSetTableAdapters.QueriesTableAdapter();

            // Fill your DataGridView with the results
            dataGridView1.DataSource = queries.GetDataByInProgress();

        }
    }
}
