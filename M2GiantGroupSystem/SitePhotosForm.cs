using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public partial class SitePhotosForm : Form
    {
        int tabIndex;
        private int selectedPhotoID = 0;
        private string selectedPhotoPath = null;
        private string defaultImagePath = @"C:\Users\ashmi\source\repos\NewM2GiantGroupSystem\M2GiantGroupSystem\images1\no image available icon.jpg";
        int jobRequestID = 0;

        string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

        public SitePhotosForm(int tab_index)
        {
            InitializeComponent();
            tabIndex = tab_index;
        }

        // ─────────────────────────────────────────────
        // HELPER — safe default image load
        // ─────────────────────────────────────────────
        private Image LoadDefaultImage()
        {
            if (File.Exists(defaultImagePath))
                return Image.FromFile(defaultImagePath);
            return null;
        }

        // ─────────────────────────────────────────────
        // FORM LOAD
        // ─────────────────────────────────────────────
        private void SitePhotosForm_Load(object sender, EventArgs e)
        {
            try
            {
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

                pbLargeView.Image = LoadDefaultImage();
                pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;

                button1.Enabled = false;
                button2.Enabled = false;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading the form:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading the form:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // SEARCH QUERY — parameterised
        // ─────────────────────────────────────────────
        public void runQuery(TextBox t, DataGridView dgv)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"
                        SELECT jobRequestID, Client.clientName, Client.clientSurname,
                               Client.emailAddress, JobRequest.dateRecieved,
                               JobRequest.siteAddress, JobRequest.siteEvaluationDate
                        FROM Client
                        INNER JOIN JobRequest ON Client.clientID = JobRequest.clientID
                        WHERE clientName   LIKE @search
                           OR clientSurname LIKE @search
                           OR siteAddress   LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + t.Text + "%");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error during search:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during search:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // VIEW TAB — grid click
        // ─────────────────────────────────────────────
        private void dgvJoinPicturesView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJoinPicturesView.Rows[e.RowIndex].Cells["jobRequestID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job Request ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobRequestID = Convert.ToInt32(cell.Value);
                label3.Text = "Job requestID selected: " + jobRequestID.ToString();

                button1.Enabled = false;
                button2.Enabled = false;
                selectedPhotoID = 0;

                LoadPhotos(jobRequestID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting job request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // UPLOAD TAB — grid click
        // ─────────────────────────────────────────────
        private void dgvJoinPictures_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJoinPictures.Rows[e.RowIndex].Cells["jobRequestID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job Request ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobRequestID = Convert.ToInt32(cell.Value);
                label11.Text = "Job requestID selected: " + jobRequestID.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting job request:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // LOAD PHOTO THUMBNAILS
        // ─────────────────────────────────────────────
        private void LoadPhotos(int jobRequestID)
        {
            try
            {
                flpThumbnails.Controls.Clear();
                pbLargeView.Image = LoadDefaultImage();
                pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT photoID, filePath, photoType FROM SitePhoto WHERE jobRequestID = @id AND jobID IS NULL";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", jobRequestID);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool anyLoaded = false;

                            while (reader.Read())
                            {
                                string path = reader["filePath"].ToString();
                                string type = reader["photoType"].ToString();
                                int photoID = Convert.ToInt32(reader["photoID"]);

                                if (!File.Exists(path)) continue;

                                anyLoaded = true;

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
                                pb.Cursor = Cursors.Hand;
                                pb.Tag = photoID;
                                pb.Click += Thumbnail_Click;

                                try
                                {
                                    pb.Image = Image.FromFile(path);
                                }
                                catch
                                {
                                    // File exists but is unreadable — skip this thumbnail
                                    continue;
                                }

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

                            if (!anyLoaded)
                            {
                                Label lbl_empty = new Label();
                                lbl_empty.Text = "No photos found for this job request.";
                                lbl_empty.AutoSize = true;
                                lbl_empty.Margin = new Padding(10);
                                lbl_empty.ForeColor = Color.Gray;
                                flpThumbnails.Controls.Add(lbl_empty);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading photos:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while loading photos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // THUMBNAIL CLICK
        // ─────────────────────────────────────────────
        private void Thumbnail_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox pb = (PictureBox)sender;
                selectedPhotoID = Convert.ToInt32(pb.Tag);

                string path = pb.Parent?.Tag?.ToString();
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    MessageBox.Show("The photo file could not be found on disk.",
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pbLargeView.Image = Image.FromFile(path);
                pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;

                button1.Enabled = true;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading photo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // EDIT PHOTO TYPE
        // ─────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedPhotoID == 0)
            {
                MessageBox.Show("Please click a photo first.",
                    "No Photo Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
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
                    if (cmb.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a photo type.",
                            "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
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
                        LoadPhotos(jobRequestID);
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Database error while updating photo type:\n" + sqlEx.Message,
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected error while updating photo type:\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                editForm.Controls.AddRange(new Control[] { cmb, btnSave });
                editForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening edit dialog:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // DELETE PHOTO
        // ─────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedPhotoID == 0)
            {
                MessageBox.Show("Please click a photo first.",
                    "No Photo Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this photo? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM SitePhoto WHERE photoID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", selectedPhotoID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        MessageBox.Show("Photo could not be found in the database. It may have already been deleted.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Photo deleted.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                selectedPhotoID = 0;
                pbLargeView.Image = LoadDefaultImage();
                button1.Enabled = false;
                button2.Enabled = false;
                LoadPhotos(jobRequestID);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while deleting photo:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while deleting photo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // BROWSE PHOTO
        // ─────────────────────────────────────────────
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                ofd.Title = "Select a Site Photo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(ofd.FileName))
                    {
                        MessageBox.Show("The selected file could not be found.",
                            "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    selectedPhotoPath = ofd.FileName;
                    pbPreview.Image = Image.FromFile(selectedPhotoPath);
                    pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the selected photo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // UPLOAD PHOTO
        // ─────────────────────────────────────────────
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (jobRequestID == 0)
            {
                MessageBox.Show("Please select a Job Request from the table before uploading a photo.",
                    "No Job Request Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                pbPreview.Image = LoadDefaultImage();
                return;
            }
           

            DialogResult confirm = MessageBox.Show(
           $"Upload this photo as 'BEFORE' for Job Request ID {jobRequestID}?",
                "Confirm Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                string photoType = "BEFORE";
                string folderPath = Path.Combine(Application.StartupPath, "SitePhotos", $"JobRequest_{jobRequestID}");
                Directory.CreateDirectory(folderPath);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string extension = Path.GetExtension(selectedPhotoPath);
                string newFileName = $"{photoType}_{timestamp}{extension}";
                string destPath = Path.Combine(folderPath, newFileName);

                File.Copy(selectedPhotoPath, destPath, overwrite: true);

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

                MessageBox.Show("Photo uploaded successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                selectedPhotoPath = null;
                pbPreview.Image = LoadDefaultImage();
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                rbBefore.Checked = true;
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("File error while uploading photo:\n" + ioEx.Message,
                    "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while saving photo record:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while uploading photo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        // HELPER — get current photo type
        // ─────────────────────────────────────────────
        private string GetCurrentPhotoType(int photoID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT photoType FROM SitePhoto WHERE photoID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", photoID);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Could not find the photo record in the database.",
                            "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return "BEFORE";
                    }

                    return result.ToString();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while fetching photo type:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "BEFORE";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while fetching photo type:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "BEFORE";
            }
        }

        // ─────────────────────────────────────────────
        // TAB DRAW
        // ─────────────────────────────────────────────
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = photosTabControl.TabPages[e.Index];
            Rectangle tabRect = photosTabControl.GetTabRect(e.Index);
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Color backColor = e.Index == photosTabControl.SelectedIndex ? Color.DarkGreen : Color.Honeydew;
            Color textColor = e.Index == photosTabControl.SelectedIndex ? Color.White : Color.Black;

            using (Brush b = new SolidBrush(backColor))
                e.Graphics.FillRectangle(b, tabRect);
            using (Pen p = new Pen(Color.DarkGreen, 1))
                e.Graphics.DrawRectangle(p, tabRect);

            TextRenderer.DrawText(e.Graphics, page.Text, tabFont, tabRect, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ─────────────────────────────────────────────
        // TEXT CHANGED
        // ─────────────────────────────────────────────
        private void textBox1_TextChanged(object sender, EventArgs e)
            => runQuery(textBox1, dgvJoinPicturesView);

        // ─────────────────────────────────────────────
        // STUB HANDLERS
        // ─────────────────────────────────────────────
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void flpThumbnails_Click(object sender, EventArgs e) { }
        private void dgvJoinPictures_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void pbLargeView_Click(object sender, EventArgs e) { }
        private void rbBefore_CheckedChanged(object sender, EventArgs e) { }
        private void rbAfter_CheckedChanged(object sender, EventArgs e) { }
        private void pbPreview_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            runQuery(textBox3, dgvJoinPictures);
        }
    }
}