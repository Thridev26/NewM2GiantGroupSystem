using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace M2GiantGroupSystem
{
    public partial class JobSitePhotos : Form
    {
        int selectedTabIndex = 0;
        int jobRequestID = 0;
        private string selectedPhotoPath = null;
       
        public JobSitePhotos(int _selectedTabIndex)
        {
            InitializeComponent();
            this.selectedTabIndex = _selectedTabIndex;
        }
        int jobID = 0;
        private int selectedPhotoID = 0;
        private string defaultImagePath = @"C:\Users\ashmi\source\repos\NewM2GiantGroupSystem\M2GiantGroupSystem\images1\no image available icon.jpg";

        public void runQuery(TextBox t, DataGridView dgv)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT Job.jobID, Client.clientName, Client.clientSurname, 
                              Client.emailAddress, Job.startDate, Job.endDate, 
                              Job.jobStatus, JobRequest.siteAddress
                       FROM Job
                       INNER JOIN Quote ON Job.quoteID = Quote.QuoteID
                       INNER JOIN JobRequest ON Quote.jobRequestID = JobRequest.jobRequestID
                       INNER JOIN Client ON JobRequest.clientID = Client.clientID
                       WHERE clientName LIKE '%" + t.Text + @"%'
                       OR clientSurname LIKE '%" + t.Text + @"%'
                       OR siteAddress LIKE '%" + t.Text + @"%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void JobSitePhotos_Load(object sender, EventArgs e)
        {
            //set to maximize the form when it loads
          //  this.WindowState = FormWindowState.Maximized;
            //ask user if they want too manage job photos or request photos
            //   MessageBox.Show("Welcome to the Site Photos Manager! Please select a Job Request from the table to view and manage its associated site photos. You can upload BEFORE and AFTER photos for each Job Request.", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
           

            dgvJobs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobs.DefaultCellStyle.SelectionBackColor = Color.Green;

            dgvJobsAdd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobsAdd.DefaultCellStyle.SelectionBackColor = Color.Green;

            runQuery(textBox1, dgvJobs);

            runQuery(textBox2, dgvJobsAdd);

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

        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            jobID = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["jobID"].Value);
            lblSelectedJob.Text = "Job ID selected: " + jobID.ToString();
            selectedPhotoID = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            LoadPhotos(jobID);
        }
        private void LoadPhotos(int jobID)
        {
            flpThumbnails.Controls.Clear();
            pbLargeView.Image = Image.FromFile(defaultImagePath);
            pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;

            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT photoID, filePath, photoType FROM SitePhoto WHERE jobID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jobID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string path = reader["filePath"].ToString();
                    string type = reader["photoType"].ToString();
                    int photoID = Convert.ToInt32(reader["photoID"]);

                    if (!File.Exists(path)) continue;

                    Panel card = new Panel();
                    card.Width = 120;
                    card.Height = 140;
                    card.Margin = new Padding(6);
                    card.Cursor = Cursors.Hand;
                    card.Tag = path;

                    PictureBox pb = new PictureBox();
                    pb.Width = 110;
                    pb.Height = 110;
                    pb.Left = 5;
                    pb.Top = 5;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Image = Image.FromFile(path);
                    pb.Cursor = Cursors.Hand;
                    pb.Tag = photoID;
                    pb.Click += Thumbnail_Click;

                    Label lbl = new Label();
                    lbl.Text = type;
                    lbl.Left = 5;
                    lbl.Top = 118;
                    lbl.Width = 110;
                    lbl.Height = 18;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                    lbl.ForeColor = type == "BEFORE" ? Color.DarkBlue : Color.DarkGreen;

                    card.Controls.Add(pb);
                    card.Controls.Add(lbl);
                    flpThumbnails.Controls.Add(card);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void flpThumbnails_Click(object sender, EventArgs e)
        {
           
        }
        private void Thumbnail_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            selectedPhotoID = Convert.ToInt32(pb.Tag);

            string path = pb.Parent.Tag.ToString();
            pbLargeView.Image = Image.FromFile(path);
            pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;

            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedPhotoID == 0)
            {
                MessageBox.Show("Please click a photo first.", "No Photo Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this photo? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM SitePhoto WHERE photoID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", selectedPhotoID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Photo deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            selectedPhotoID = 0;
            pbLargeView.Image = Image.FromFile(defaultImagePath);
            button1.Enabled = false;
            button2.Enabled = false;
            LoadPhotos(jobID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedPhotoID == 0)
            {
                MessageBox.Show("Please click a photo first.", "No Photo Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this photo? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM SitePhoto WHERE photoID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", selectedPhotoID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Photo deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            selectedPhotoID = 0;
            pbLargeView.Image = Image.FromFile(defaultImagePath);
            button1.Enabled = false;
            button2.Enabled = false;
            LoadPhotos(jobID);
        }
        private string GetCurrentPhotoType(int photoID)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT photoType FROM SitePhoto WHERE photoID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", photoID);
                conn.Open();
                return cmd.ExecuteScalar()?.ToString() ?? "BEFORE";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            runQuery(textBox1, dgvJobs);
        }

        private void dgvJobs_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            jobID = Convert.ToInt32(dgvJobs.Rows[e.RowIndex].Cells["jobID"].Value);
            lblSelectedJob.Text = "Job ID selected: " + jobID.ToString();
            selectedPhotoID = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            LoadPhotos(jobID);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            runQuery(textBox2, dgvJobsAdd);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
           
                if (jobID == 0)
                {
                    MessageBox.Show("Please select a Job from the table before uploading a photo.",
                        "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedPhotoPath == null)
                {
                    MessageBox.Show("Please browse and select a photo first.",
                        "No Photo Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(selectedPhotoPath))
                {
                    MessageBox.Show("The selected photo file no longer exists. Please browse and select it again.",
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    selectedPhotoPath = null;
                    pbPreview.Image = Image.FromFile(defaultImagePath);
                    return;
                }

                if (!rbBefore.Checked && !rbAfter.Checked)
                {
                    MessageBox.Show("Please select whether this is a BEFORE or AFTER photo.",
                        "Photo Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // check jobRequestID was actually fetched
                if (jobRequestID == 0)
                {
                    MessageBox.Show("Could not determine the Job Request for this job. Please reselect the job.",
                        "Missing Job Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    $"Upload this photo as '{(rbBefore.Checked ? "BEFORE" : "AFTER")}' for Job ID {jobID}?",
                    "Confirm Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                try
                {
                    string photoType = rbBefore.Checked ? "BEFORE" : "AFTER";
                    string folderPath = Path.Combine(Application.StartupPath, "SitePhotos", $"Job_{jobID}");
                    Directory.CreateDirectory(folderPath);

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string extension = Path.GetExtension(selectedPhotoPath);
                    string newFileName = $"{photoType}_{timestamp}{extension}";
                    string destPath = Path.Combine(folderPath, newFileName);

                    File.Copy(selectedPhotoPath, destPath, overwrite: true);

                    string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

                    // conn was missing — added here
                    using (SqlConnection conn = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO SitePhoto (photoType, filePath, uploadDate, jobID, jobRequestID)
            VALUES (@photoType, @filePath, GETDATE(), @jobID, @jobRequestID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@photoType", photoType);
                        cmd.Parameters.AddWithValue("@filePath", destPath);
                        cmd.Parameters.AddWithValue("@jobID", jobID);
                        cmd.Parameters.AddWithValue("@jobRequestID", jobRequestID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Photo uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    selectedPhotoPath = null;
                    pbPreview.Image = Image.FromFile(defaultImagePath);
                    pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                    rbBefore.Checked = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading photo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        

        private void dgvJobsAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // was reading from dgvJobs — fixed to dgvJobsAdd
            jobID = Convert.ToInt32(dgvJobsAdd.Rows[e.RowIndex].Cells["jobID"].Value);
            label6.Text = "Job ID selected: " + jobID.ToString();
            jobRequestID = 0;

            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
        SELECT Quote.jobRequestID 
        FROM Job 
        INNER JOIN Quote ON Job.quoteID = Quote.QuoteID
        WHERE Job.jobID = @jobID", conn))
            {
                cmd.Parameters.AddWithValue("@jobID", jobID);
                conn.Open();
                jobRequestID = Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (selectedPhotoID == 0)
            {
                MessageBox.Show("Please click a photo first.", "No Photo Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string current = GetCurrentPhotoType(selectedPhotoID);

            Form editForm = new Form();
            editForm.Text = "Edit Photo Type";
            editForm.Width = 280;
            editForm.Height = 150;
            editForm.StartPosition = FormStartPosition.CenterParent;
            editForm.FormBorderStyle = FormBorderStyle.FixedDialog;

            ComboBox cmb = new ComboBox();
            cmb.Items.AddRange(new string[] { "BEFORE", "AFTER" });
            cmb.SelectedItem = current;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.Left = 20; cmb.Top = 20; cmb.Width = 220;

            Button btnSave = new Button();
            btnSave.Text = "Save";
            btnSave.Left = 20; btnSave.Top = 60;
            btnSave.Click += (s, ev) =>
            {
                string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE SitePhoto SET photoType = @type WHERE photoID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@type", cmb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@id", selectedPhotoID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Photo type updated.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                editForm.Close();
                LoadPhotos(jobID);
            };

            editForm.Controls.AddRange(new Control[] { cmb, btnSave });
            editForm.ShowDialog();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            ofd.Title = "Select a Site Photo";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedPhotoPath = ofd.FileName;
                pbPreview.Image = Image.FromFile(selectedPhotoPath);
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
