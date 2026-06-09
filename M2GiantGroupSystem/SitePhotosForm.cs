using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class SitePhotosForm : Form

    {
        int tabIndex;
        private int selectedPhotoID = 0;
        public SitePhotosForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }
        private string selectedPhotoPath = null;
        private string defaultImagePath = @"C:\Users\ashmi\source\repos\NewM2GiantGroupSystem\M2GiantGroupSystem\images1\no image available icon.jpg";
        int jobRequestID = 0;
       
      
        public void runQuery(TextBox t, DataGridView dgv)
        {
            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT jobRequestID,Client.clientName, Client.clientSurname, Client.emailAddress, JobRequest.dateRecieved, JobRequest.siteAddress, JobRequest.siteEvaluationDate " +

                 " FROM Client INNER JOIN JobRequest ON Client.clientID = JobRequest.clientID " +
                     " WHERE clientName LIKE " + "'%" + t.Text + "%'" +
                     "OR clientSurname LIKE  " + "'%" + t.Text + "%'" +
                     "OR siteAddress LIKE  " + "'%" + t.Text + "%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);


                DataTable dt = new DataTable();
                da.Fill(dt);

                dgv.DataSource = dt;

            }
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // 1.Check a job request is selected
            if (jobRequestID == 0)
            {
                MessageBox.Show("Please select a Job Request from the table before uploading a photo.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check a photo has been browsed
            if (selectedPhotoPath == null)
            {
                MessageBox.Show("Please browse and select a photo first.",
                    "No Photo Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Check the selected file still exists on disk
            if (!File.Exists(selectedPhotoPath))
            {
                MessageBox.Show("The selected photo file no longer exists. Please browse and select it again.",
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                selectedPhotoPath = null;
                pbPreview.Image = Image.FromFile(defaultImagePath);
                return;
            }

            // 4. Check a radio button is selected
            if (!rbBefore.Checked && !rbAfter.Checked)
            {
                MessageBox.Show("Please select whether this is a BEFORE or AFTER photo.",
                    "Photo Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Confirm before uploading
            DialogResult confirm = MessageBox.Show(
                $"Upload this photo as '{(rbBefore.Checked ? "BEFORE" : "AFTER")}' for Job Request ID {jobRequestID}?",
                "Confirm Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string photoType = rbBefore.Checked ? "BEFORE" : "AFTER";
                string folderPath = Path.Combine(Application.StartupPath, "SitePhotos", $"JobRequest_{jobRequestID}");
                Directory.CreateDirectory(folderPath);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string extension = Path.GetExtension(selectedPhotoPath);
                string newFileName = $"{photoType}_{timestamp}{extension}";
                string destPath = Path.Combine(folderPath, newFileName);

                File.Copy(selectedPhotoPath, destPath, overwrite: true);

                string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
            INSERT INTO SitePhoto (photoType, filePath, uploadDate, jobRequestID)
            VALUES (@photoType, @filePath, GETDATE(), @jobRequestID)", conn))
                {
                    cmd.Parameters.AddWithValue("@photoType", photoType);
                    cmd.Parameters.AddWithValue("@filePath", destPath);
                    cmd.Parameters.AddWithValue("@jobRequestID", jobRequestID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Photo uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset controls
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

        private void SitePhotosForm_Load(object sender, EventArgs e)
        {
            //set to maximize the form when it loads
            this.WindowState = FormWindowState.Maximized;
            //ask user if they want too manage job photos or request photos
            //   MessageBox.Show("Welcome to the Site Photos Manager! Please select a Job Request from the table to view and manage its associated site photos. You can upload BEFORE and AFTER photos for each Job Request.", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
            runQuery(textBox1, dgvJoinPicturesView);

            dgvJoinPicturesView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJoinPicturesView.DefaultCellStyle.SelectionBackColor = Color.Green;

            runQuery(textBox3, dgvJoinPictures);

            dgvJoinPictures.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJoinPictures.DefaultCellStyle.SelectionBackColor = Color.Green;
            photosTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            photosTabControl.DrawItem += tabControl1_DrawItem;
            photosTabControl.ItemSize = new Size(300, 30);
            photosTabControl.SizeMode = TabSizeMode.Fixed;
        }

        private void dgvJoinPictures_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            jobRequestID = Convert.ToInt32(dgvJoinPictures.Rows[e.RowIndex].Cells["jobRequestID"].Value);
            label11.Text = "Job requestID selected: " + jobRequestID.ToString();
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = photosTabControl.TabPages[e.Index];
            Rectangle tabRect = photosTabControl.GetTabRect(e.Index);

            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);

            Color backColor = Color.Honeydew;
            Color textColor = Color.Black;

            if (e.Index == photosTabControl.SelectedIndex)
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

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            runQuery(textBox1, dgvJoinPicturesView);
        }

        private void dgvJoinPicturesView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            jobRequestID = Convert.ToInt32(dgvJoinPicturesView.Rows[e.RowIndex].Cells["jobRequestID"].Value);
            label3.Text = "Job requestID selected: " + jobRequestID.ToString();

            button1.Enabled = false;
            button2.Enabled = false;
            LoadPhotos(jobRequestID);
        }
        private void LoadPhotos(int jobRequestID)
        {
            flpThumbnails.Controls.Clear();
            pbLargeView.Image = Image.FromFile(defaultImagePath);  // add this
            pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;
      
         

            string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT photoID, filePath, photoType FROM SitePhoto WHERE jobRequestID = @id AND jobID IS NULL";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", jobRequestID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string path = reader["filePath"].ToString();
                    string type = reader["photoType"].ToString();
                    int photoID = Convert.ToInt32(reader["photoID"]);

                    if (!File.Exists(path)) continue;

                    // Outer panel = the card
                    Panel card = new Panel();
                    card.Width = 120;
                    card.Height = 140;
                    card.Margin = new Padding(6);
                    card.Cursor = Cursors.Hand;
                    card.Tag = path;

                    // Thumbnail PictureBox
                    PictureBox pb = new PictureBox();
                    pb.Width = 110;
                    pb.Height = 110;
                    pb.Left = 5;
                    pb.Top = 5;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Image = Image.FromFile(path);
                    pb.Cursor = Cursors.Hand;
                    pb.Tag = path;
                    pb.Click += Thumbnail_Click;

                    // BEFORE/AFTER label
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

                    pb.Tag = photoID;   // int
                    card.Tag = path;    // string
                }
            }
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

            button1.Enabled = true;  // edit
            button2.Enabled = true;  // delete
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedPhotoID == 0)
            {
                MessageBox.Show("Please click a photo first.", "No Photo Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ask user to pick BEFORE or AFTER
            string[] options = { "BEFORE", "AFTER" };
            string current = GetCurrentPhotoType(selectedPhotoID);

            Form editForm = new Form();
            editForm.Text = "Edit Photo Type";
            editForm.Width = 280;
            editForm.Height = 150;
            editForm.StartPosition = FormStartPosition.CenterParent;
            editForm.FormBorderStyle = FormBorderStyle.FixedDialog;

            ComboBox cmb = new ComboBox();
            cmb.Items.AddRange(options);
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
                LoadPhotos(jobRequestID); // refresh thumbnails
            };

            editForm.Controls.AddRange(new Control[] { cmb, btnSave });
            editForm.ShowDialog();
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
            pbLargeView.Image = null;
            LoadPhotos(jobRequestID); // refresh thumbnails
        }

        private void dgvJoinPictures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pbLargeView_Click(object sender, EventArgs e)
        {

        }

        private void rbBefore_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbAfter_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pbPreview_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
