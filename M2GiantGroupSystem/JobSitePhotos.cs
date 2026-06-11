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
        string connStr = "Data Source=146.230.177.46;Initial Catalog=GroupWst1;Persist Security Info=True;User ID=GroupWst1;Password=dtf39;Encrypt=True;TrustServerCertificate=True";

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
        private void JobSitePhotos_Load(object sender, EventArgs e)
        {
            try
            {
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

                pbLargeView.Image = LoadDefaultImage();
                pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;
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
                        SELECT Job.jobID, Client.clientName, Client.clientSurname,
                               Client.emailAddress, Job.startDate, Job.endDate,
                               Job.jobStatus, JobRequest.siteAddress
                        FROM Job
                        INNER JOIN Quote      ON Job.quoteID            = Quote.QuoteID
                        INNER JOIN JobRequest ON Quote.jobRequestID     = JobRequest.jobRequestID
                        INNER JOIN Client     ON JobRequest.clientID    = Client.clientID
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
        private void dgvJobs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJobs.Rows[e.RowIndex].Cells["jobID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobID = Convert.ToInt32(cell.Value);
                lblSelectedJob.Text = "Job ID selected: " + jobID.ToString();
                selectedPhotoID = 0;
                button1.Enabled = false;
                button2.Enabled = false;
                LoadPhotos(jobID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting job:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // kept for designer compatibility — same logic
        private void dgvJobs_CellClick_1(object sender, DataGridViewCellEventArgs e)
            => dgvJobs_CellClick(sender, e);

        // ─────────────────────────────────────────────
        // LOAD PHOTO THUMBNAILS
        // ─────────────────────────────────────────────
        private void LoadPhotos(int jobID)
        {
            try
            {
                flpThumbnails.Controls.Clear();
                pbLargeView.Image = LoadDefaultImage();
                pbLargeView.SizeMode = PictureBoxSizeMode.Zoom;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT photoID, filePath, photoType FROM SitePhoto WHERE jobID = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", jobID);
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
                                    // File listed in DB but unreadable — skip thumbnail
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
                                lbl_empty.Text = "No photos found for this job.";
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
        // DELETE PHOTO (button2)
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

                MessageBox.Show("Photo deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                selectedPhotoID = 0;
                pbLargeView.Image = LoadDefaultImage();
                button1.Enabled = false;
                button2.Enabled = false;
                LoadPhotos(jobID);
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
        // EDIT PHOTO TYPE (button1 — view tab)
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
                        LoadPhotos(jobID);
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

        // kept for designer compatibility
        private void button1_Click_1(object sender, EventArgs e)
            => button1_Click(sender, e);

        // ─────────────────────────────────────────────
        // ADD TAB — grid click (fetches jobRequestID)
        // ─────────────────────────────────────────────
        private void dgvJobsAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvJobsAdd.Rows[e.RowIndex].Cells["jobID"];
                if (cell.Value == null || cell.Value == DBNull.Value)
                {
                    MessageBox.Show("Selected row has no Job ID.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                jobID = Convert.ToInt32(cell.Value);
                jobRequestID = 0;
                label6.Text = "Job ID selected: " + jobID.ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT Quote.jobRequestID
                    FROM Job
                    INNER JOIN Quote ON Job.quoteID = Quote.QuoteID
                    WHERE Job.jobID = @jobID", conn))
                {
                    cmd.Parameters.AddWithValue("@jobID", jobID);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Could not find a Job Request linked to this job. Upload will be unavailable.",
                            "Missing Job Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    jobRequestID = Convert.ToInt32(result);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while selecting job:\n" + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while selecting job:\n" + ex.Message,
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
            if (jobID == 0)
            {
                MessageBox.Show("Please select a Job from the table before uploading a photo.",
                    "No Job Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (jobRequestID == 0)
            {
                MessageBox.Show("Could not determine the Job Request for this job. Please reselect the job.",
                    "Missing Job Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!rbBefore.Checked && !rbAfter.Checked)
            {
                MessageBox.Show("Please select whether this is a BEFORE or AFTER photo.",
                    "Photo Type Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            TabPage page = tabControl1.TabPages[e.Index];
            Rectangle tabRect = tabControl1.GetTabRect(e.Index);
            Font tabFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Color backColor = e.Index == tabControl1.SelectedIndex ? Color.DarkGreen : Color.Honeydew;
            Color textColor = e.Index == tabControl1.SelectedIndex ? Color.White : Color.Black;

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
        private void textBox1_TextChanged(object sender, EventArgs e) => runQuery(textBox1, dgvJobs);
        private void textBox2_TextChanged(object sender, EventArgs e) => runQuery(textBox2, dgvJobsAdd);

        // ─────────────────────────────────────────────
        // STUB HANDLERS
        // ─────────────────────────────────────────────
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void flpThumbnails_Click(object sender, EventArgs e) { }
    }
}