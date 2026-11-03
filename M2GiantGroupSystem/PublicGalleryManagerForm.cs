using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    // Self-contained MDI form; no Designer or .csproj replacement.
    // Only managers with levels 4 and 5 can publish. Backend SQL accounts should also be restricted.
    public class PublicGalleryManagerForm : Form
    {
        readonly ComboBox jobs = new ComboBox();
        readonly ComboBox before = new ComboBox();
        readonly ComboBox after = new ComboBox();
        readonly TextBox title = new TextBox();
        readonly TextBox category = new TextBox();
        readonly TextBox description = new TextBox();
        readonly NumericUpDown order = new NumericUpDown();
        readonly CheckBox publish = new CheckBox();
        readonly DataGridView list = new DataGridView();
        readonly PictureBox beforePreview = new PictureBox();
        readonly PictureBox afterPreview = new PictureBox();
        readonly Label status = new Label();
        byte[] beforeOverride, afterOverride;
        string beforeOverrideMime, afterOverrideMime;
        int currentGalleryID;
        string Conn { get { return ConfigurationManager.ConnectionStrings["GroupWst1ConnString"].ConnectionString; } }

        public PublicGalleryManagerForm()
        {
            if (UserSession.StaffID <= 0 ||
                (UserSession.AccessLevel != 4 &&
                 UserSession.AccessLevel != 5 &&
                 UserSession.AccessLevel != 6))
            {
                throw new UnauthorizedAccessException(
                    "Only authorised managers can manage the public gallery.");
            }

            Text = "Public Gallery | Curate Projects";
            BackColor = Color.FromArgb(242, 247, 244);
            AutoScroll = true;
            Padding = new Padding(16);

            // ======================================================
            // MAIN TWO-COLUMN LAYOUT
            // ======================================================

            var outer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                AutoScroll = true
            };

            outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));

            Controls.Add(outer);

            var left = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8)
            };

            var right = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            outer.Controls.Add(left, 0, 0);
            outer.Controls.Add(right, 1, 0);

            // ======================================================
            // LEFT SIDE - GALLERY PROJECT DETAILS
            // ======================================================

            var fields = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                Padding = new Padding(8)
            };

            fields.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            left.Controls.Add(fields);

            AddHeading(fields, "CURATE THE PUBLIC GALLERY", 18);

            AddHeading(
                fields,
                "Only completed jobs are selectable. Choose one BEFORE " +
                "and one AFTER photo from records, or browse local image files.",
                9);

            // Completed job
            AddField(fields, "Completed job", jobs);

            jobs.DropDownStyle = ComboBoxStyle.DropDownList;

            jobs.SelectedIndexChanged += (s, e) =>
                LoadJobPhotos();

            // Project title
            AddField(fields, "Project title (public)", title);
            title.MaxLength = 100;

            // Category
            AddField(fields, "Category / service", category);
            category.MaxLength = 50;

            // Description
            AddField(
                fields,
                "Short description (do not include client details)",
                description);

            description.MaxLength = 350;
            description.Multiline = true;
            description.Height = 65;

            // BEFORE photo
            AddField(fields, "BEFORE photo from job records", before);

            before.DropDownStyle = ComboBoxStyle.DropDownList;

            before.SelectedIndexChanged += (s, e) =>
                PreviewSelected(before, beforePreview, true);

            // AFTER photo
            AddField(fields, "AFTER photo from job records", after);

            after.DropDownStyle = ComboBoxStyle.DropDownList;

            after.SelectedIndexChanged += (s, e) =>
                PreviewSelected(after, afterPreview, false);

            // ======================================================
            // PHOTO PREVIEWS
            // ======================================================

            var photos = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = true,
                Dock = DockStyle.Top
            };

            var beforeBox = PhotoBox(
                beforePreview,
                "Before preview");

            var afterBox = PhotoBox(
                afterPreview,
                "After preview");

            photos.Controls.Add(beforeBox);
            photos.Controls.Add(afterBox);

            fields.Controls.Add(photos);

            // Browse buttons
            var browseRow = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = true
            };

            Button browseBefore = ActionButton(
                "Browse BEFORE",
                Color.FromArgb(45, 106, 79));

            Button browseAfter = ActionButton(
                "Browse AFTER",
                Color.FromArgb(45, 106, 79));

            browseBefore.Click += (s, e) =>
                BrowsePhoto(true);

            browseAfter.Click += (s, e) =>
                BrowsePhoto(false);

            browseRow.Controls.Add(browseBefore);
            browseRow.Controls.Add(browseAfter);

            fields.Controls.Add(browseRow);

            // Display order
            AddField(
                fields,
                "Display order (smaller appears first)",
                order);

            order.Maximum = 100000;
            order.Value = 100;

            // Publish option
            publish.Text =
                "Publish immediately to public gallery";

            publish.AutoSize = true;
            publish.Checked = false;

            fields.Controls.Add(publish);

            // Save and New Entry buttons
            var buttons = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = true
            };

            Button save = ActionButton(
                "Save gallery project",
                Color.FromArgb(27, 67, 50));

            save.Click += SaveGallery;

            Button clear = ActionButton(
                "New entry",
                Color.FromArgb(90, 111, 100));

            clear.Click += (s, e) => ClearSelection();

            buttons.Controls.Add(save);
            buttons.Controls.Add(clear);

            fields.Controls.Add(buttons);

            status.AutoSize = true;
            status.ForeColor = Color.FromArgb(45, 106, 79);

            fields.Controls.Add(status);

            // ======================================================
            // RIGHT SIDE - GALLERY ENTRIES
            // ======================================================

            var galleryLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(8)
            };

            galleryLayout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            galleryLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 70F));

            galleryLayout.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            galleryLayout.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 60F));

            // ======================================================
            // HEADING AND SELECTED ENTRY LABEL
            // ======================================================

            var headingPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Label galleryHeading = new Label
            {
                Text = "GALLERY ENTRIES",
                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold),

                ForeColor = Color.FromArgb(27, 67, 50),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            Label selectedLabel = new Label
            {
                Text = "No gallery entry selected",
                Font = new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Regular),

                ForeColor = Color.FromArgb(90, 111, 100),
                AutoSize = true,
                Location = new Point(2, 40)
            };

            headingPanel.Controls.Add(galleryHeading);
            headingPanel.Controls.Add(selectedLabel);

            galleryLayout.Controls.Add(headingPanel, 0, 0);

            // ======================================================
            // DATAGRIDVIEW CONFIGURATION
            // ======================================================

            list.Dock = DockStyle.Fill;
            list.Visible = true;
            list.ReadOnly = true;
            list.AutoGenerateColumns = true;

            list.AllowUserToAddRows = false;
            list.AllowUserToDeleteRows = false;
            list.AllowUserToResizeRows = false;

            list.MultiSelect = false;

            // Select the whole row
            list.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            list.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            list.BackgroundColor = Color.White;
            list.ForeColor = Color.Black;
            list.GridColor = Color.LightGray;

            list.BorderStyle =
                BorderStyle.FixedSingle;

            list.RowHeadersVisible = false;
            list.RowTemplate.Height = 38;

            // ======================================================
            // COLUMN HEADER STYLING
            // ======================================================

            list.EnableHeadersVisualStyles = false;
            list.ColumnHeadersVisible = true;

            list.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode
                    .DisableResizing;

            list.ColumnHeadersHeight = 42;

            list.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(27, 67, 50);

            list.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            list.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Bold);

            // ======================================================
            // NORMAL ROW STYLING
            // ======================================================

            list.DefaultCellStyle.BackColor =
                Color.White;

            list.DefaultCellStyle.ForeColor =
                Color.Black;

            list.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Regular);

            list.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(242, 247, 244);

            // ======================================================
            // SELECTED ROW STYLING
            // ======================================================

            // Giant Group dark green highlight
            Color selectedGreen =
                Color.FromArgb(45, 106, 79);

            list.DefaultCellStyle.SelectionBackColor =
                selectedGreen;

            list.DefaultCellStyle.SelectionForeColor =
                Color.White;

            // Apply same selection colours to alternating rows
            list.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                selectedGreen;

            list.AlternatingRowsDefaultCellStyle.SelectionForeColor =
                Color.White;

            // Prevent selection from becoming invisible
            // when focus moves to the action buttons
            

            // Clear automatic initial selection after binding
            list.DataBindingComplete += (s, e) =>
            {
                list.ClearSelection();
                list.CurrentCell = null;
                selectedLabel.Text = "No gallery entry selected";
            };

            // ======================================================
            // SELECTION CHANGED EVENT
            // ======================================================

            list.SelectionChanged += (s, e) =>
            {
                if (list.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow =
                        list.SelectedRows[0];

                    if (list.Columns.Contains("Title"))
                    {
                        string projectName =
                            Convert.ToString(
                                selectedRow.Cells["Title"].Value);

                        selectedLabel.Text =
                            "Selected: " + projectName;

                        selectedLabel.ForeColor =
                            Color.FromArgb(27, 67, 50);
                    }
                }
                else
                {
                    selectedLabel.Text =
                        "No gallery entry selected";

                    selectedLabel.ForeColor =
                        Color.FromArgb(90, 111, 100);
                }
            };

            // Double-click to edit
            list.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    EditSelected();
                }
            };

            galleryLayout.Controls.Add(list, 0, 1);

            // ======================================================
            // RIGHT SIDE ACTION BUTTONS
            // ======================================================

            FlowLayoutPanel rightButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White
            };

            Button edit = ActionButton(
                "Edit selected",
                Color.FromArgb(45, 106, 79));

            edit.Click += (s, e) =>
                EditSelected();

            Button toggle = ActionButton(
                "Publish / unpublish",
                Color.FromArgb(27, 67, 50));

            toggle.Click += (s, e) =>
                ToggleSelected();

            Button refresh = ActionButton(
                "Refresh",
                Color.FromArgb(90, 111, 100));

            refresh.Click += (s, e) =>
                LoadGallery();

            rightButtons.Controls.Add(edit);
            rightButtons.Controls.Add(toggle);
            rightButtons.Controls.Add(refresh);

            galleryLayout.Controls.Add(rightButtons, 0, 2);

            right.Controls.Add(galleryLayout);

            // ======================================================
            // FORM LOAD
            // ======================================================

            Load += (s, e) =>
            {
                LoadJobs();
                LoadGallery();
            };
        }

        static void AddHeading(TableLayoutPanel p,string t,float size)
        {
            p.Controls.Add(new Label { Text=t,AutoSize=true,MaximumSize=new Size(540,0),Font=new Font("Segoe UI",size,size>12?FontStyle.Bold:FontStyle.Regular),ForeColor=Color.FromArgb(27,67,50),Margin=new Padding(3,6,3,12) });
        }
        static void AddHeadingTo(Panel p,string t,float size)
        {
            p.Controls.Add(new Label { Text=t,Height=42,Dock=DockStyle.Top,Font=new Font("Segoe UI",size,FontStyle.Bold),ForeColor=Color.FromArgb(27,67,50) });
        }
        static void AddField(TableLayoutPanel p,string label,Control input)
        {
            p.Controls.Add(new Label { Text=label,AutoSize=true,Font=new Font("Segoe UI",9,FontStyle.Bold),Margin=new Padding(3,8,3,2) });
            input.Width=400; input.Margin=new Padding(3,2,3,8); p.Controls.Add(input);
        }
        static Panel PhotoBox(PictureBox img,string label)
        {
            Panel p = new Panel { Width=192,Height=155,Margin=new Padding(4) };
            img.Location=new Point(0,22); img.Size=new Size(185,124); img.SizeMode=PictureBoxSizeMode.Zoom; img.BackColor=Color.FromArgb(225,240,232); img.BorderStyle=BorderStyle.FixedSingle;
            p.Controls.Add(new Label { Text=label,Location=new Point(0,0),AutoSize=true }); p.Controls.Add(img); return p;
        }
        static Button ActionButton(string text,Color color)
        {
            return new Button { Text=text,AutoSize=true,MinimumSize=new Size(105,36),BackColor=color,ForeColor=Color.White,FlatStyle=FlatStyle.Flat,Margin=new Padding(4) };
        }
        void LoadJobs()
        {
            var dt = new DataTable();
            using(var cn=new SqlConnection(Conn))
            using(var da=new SqlDataAdapter(@"SELECT j.jobID, 'Job #' + CONVERT(varchar(15), j.jobID) + ' | ' + jr.siteAddress AS DisplayName
                FROM dbo.Job j JOIN dbo.Quote q ON q.QuoteID=j.quoteID JOIN dbo.JobRequest jr ON jr.jobRequestID=q.jobRequestID
                WHERE j.jobStatus='Completed' ORDER BY j.jobID DESC",cn)) { da.Fill(dt); }
            jobs.DataSource=dt; jobs.DisplayMember="DisplayName"; jobs.ValueMember="jobID";
        }
        void LoadJobPhotos()
        {
            beforeOverride=afterOverride=null;beforeOverrideMime=afterOverrideMime=null;
            ShowImage(beforePreview,null);ShowImage(afterPreview,null);
            if (jobs.SelectedValue==null || !(jobs.SelectedValue is int)) return;
            int jobID=(int)jobs.SelectedValue;
            var dt=new DataTable();
            using(var cn=new SqlConnection(Conn))
            using(var cmd=new SqlCommand(@"
              SELECT sp.photoID, 'Photo #' + CONVERT(varchar(15),sp.photoID) + ' | ' + sp.photoType + ' | ' + CONVERT(varchar(10),sp.uploadDate,120) AS DisplayName, sp.photoType
              FROM dbo.SitePhoto sp
              JOIN dbo.Job j ON j.jobID=@jobID JOIN dbo.Quote q ON q.QuoteID=j.quoteID
              WHERE sp.jobRequestID=q.jobRequestID AND (sp.jobID IS NULL OR sp.jobID=@jobID)
              ORDER BY sp.photoID DESC",cn))
            using(var da=new SqlDataAdapter(cmd)) {cmd.Parameters.Add("@jobID",SqlDbType.Int).Value=jobID;da.Fill(dt);}
            // Select from actual types to avoid accidentally swapping BEFORE/AFTER.
            var b=dt.Clone();var a=dt.Clone();
            foreach(DataRow row in dt.Rows) {if(Convert.ToString(row["photoType"])=="BEFORE") b.ImportRow(row);if(Convert.ToString(row["photoType"])=="AFTER") a.ImportRow(row);}
            before.DataSource=b;before.DisplayMember="DisplayName";before.ValueMember="photoID";
            after.DataSource=a;after.DisplayMember="DisplayName";after.ValueMember="photoID";
            if (b.Rows.Count==0)status.Text="No BEFORE photos found; browse a local image instead.";
            if (a.Rows.Count==0)status.Text="No AFTER photos found; browse a local image instead.";
        }
        void PreviewSelected(ComboBox selection,PictureBox target,bool isBefore)
        {
            if (isBefore && beforeOverride!=null || !isBefore && afterOverride!=null) return;
            if (!(selection.SelectedValue is int)) {ShowImage(target,null);return;}
            try { var p=GetExistingPhoto((int)selection.SelectedValue);ShowImage(target,p.Item1); }
            catch(Exception ex) {status.Text="Photo cannot be previewed: "+ex.Message;ShowImage(target,null);}
        }
        // Returns byte array and validated MIME. Copies bytes from SQL-stored or locally available desktop files.
        Tuple<byte[],string> GetExistingPhoto(int photoID)
        {
            using(var cn=new SqlConnection(Conn))
            using(var cmd=new SqlCommand(@"SELECT sp.filePath,sc.ImageData,sc.MimeType FROM dbo.SitePhoto sp
                      LEFT JOIN dbo.SitePhotoContent sc ON sc.photoID=sp.photoID WHERE sp.photoID=@id",cn))
            {
                cmd.Parameters.Add("@id",SqlDbType.Int).Value=photoID; cn.Open();
                using(var r=cmd.ExecuteReader())
                {
                    if(!r.Read())throw new InvalidOperationException("Photo not found");
                    if(!r.IsDBNull(1)) return ValidImage((byte[])r[1]);
                    string path=r.GetString(0);
                    if(!File.Exists(path))throw new FileNotFoundException("Photo path is unavailable on this computer",path);
                    return ValidImage(File.ReadAllBytes(path));
                }
            }
        }
        // Restrict size and verify actual binary signature, not just extension.
        static Tuple<byte[],string> ValidImage(byte[] bytes)
        {
            if(bytes==null||bytes.Length==0||bytes.Length>8*1024*1024)throw new InvalidOperationException("Photo must be 1 byte to 8 MB");
            bool jpg=bytes.Length>3&&bytes[0]==0xFF&&bytes[1]==0xD8&&bytes[2]==0xFF;
            bool png=bytes.Length>8&&bytes[0]==137&&bytes[1]==80&&bytes[2]==78&&bytes[3]==71&&bytes[4]==13&&bytes[5]==10&&bytes[6]==26&&bytes[7]==10;
            if(!jpg&&!png)throw new InvalidOperationException("Only JPEG or PNG images are supported");
            using(var ms=new MemoryStream(bytes)) using(var img=Image.FromStream(ms,true,true)) {if(img.Width>10000||img.Height>10000)throw new InvalidOperationException("Photo dimensions are too large");}
            return Tuple.Create(bytes,jpg?"image/jpeg":"image/png");
        }
        void BrowsePhoto(bool isBefore)
        {
            using(var fd=new OpenFileDialog {Filter="JPEG or PNG|*.jpg;*.jpeg;*.png",Title=isBefore?"Select BEFORE photo":"Select AFTER photo"})
            {
                if(fd.ShowDialog(this)!=DialogResult.OK)return;
                try
                {
                    var photo=ValidImage(File.ReadAllBytes(fd.FileName));
                    if(isBefore){beforeOverride=photo.Item1;beforeOverrideMime=photo.Item2;ShowImage(beforePreview,photo.Item1);}
                    else{afterOverride=photo.Item1;afterOverrideMime=photo.Item2;ShowImage(afterPreview,photo.Item1);}
                    status.Text="Selected "+(isBefore?"BEFORE":"AFTER")+" photo from file.";
                }
                catch(Exception ex){MessageBox.Show(ex.Message,"Invalid image");}
            }
        }
        static void ShowImage(PictureBox box,byte[] bytes)
        {
            var old=box.Image;box.Image=null; if(old!=null)old.Dispose();
            if(bytes==null)return;
            using(var ms=new MemoryStream(bytes))using(var image=Image.FromStream(ms,true,true))box.Image=new Bitmap(image);
        }

        void LoadGallery()
        {
            try
            {
                DataTable dt = new DataTable();
                string databaseName = "";

                using (SqlConnection cn = new SqlConnection(Conn))
                {
                    cn.Open();
                    databaseName = cn.Database;

                    string sql = @"
                SELECT
                    GalleryID,
                    JobID,
                    Title,
                    Category,
                    IsPublished,
                    DisplayOrder,
                    CreatedAtUtc
                FROM dbo.PublicJobGallery
                ORDER BY DisplayOrder, GalleryID DESC";

                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                list.AutoGenerateColumns = true;
                list.DataSource = null;
                list.Columns.Clear();
                list.DataSource = dt;

                list.Visible = true;
                list.BackgroundColor = Color.White;
                list.ForeColor = Color.Black;
                list.EnableHeadersVisualStyles = false;
                list.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                list.ColumnHeadersDefaultCellStyle.BackColor =
                    Color.LightGray;

                list.Refresh();

                /*MessageBox.Show(
                    "Connected database: " + databaseName +
                    "\nGallery entries found: " + dt.Rows.Count,
                    "Gallery Diagnostics",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading gallery entries:\n" + ex.Message,
                    "Gallery Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        void SaveGallery(object sender,EventArgs e)
        {
            if(!(jobs.SelectedValue is int)){MessageBox.Show("Choose a completed job.");return;}
            if(string.IsNullOrWhiteSpace(title.Text)||string.IsNullOrWhiteSpace(category.Text)){MessageBox.Show("Enter a public title and category.");return;}
            try
            {
                var b=beforeOverride==null?GetExistingPhoto(SelectedPhotoID(before)):ValidImage(beforeOverride);
                var a=afterOverride==null?GetExistingPhoto(SelectedPhotoID(after)):ValidImage(afterOverride);
                using(var cn=new SqlConnection(Conn))
                using(var cmd=new SqlCommand(@"
                   IF NOT EXISTS(SELECT 1 FROM dbo.Job WHERE jobID=@job AND jobStatus='Completed') THROW 50001,'Only completed jobs can be published',1;
                   IF @id=0
                   INSERT dbo.PublicJobGallery(JobID,Title,Category,Description,BeforeMime,BeforeImage,AfterMime,AfterImage,IsPublished,DisplayOrder)
                   VALUES(@job,@title,@cat,@descr,@bm,@bi,@am,@ai,@pub,@ord);
                   ELSE
                   UPDATE dbo.PublicJobGallery SET JobID=@job,Title=@title,Category=@cat,Description=@descr,
                       BeforeMime=@bm,BeforeImage=@bi,AfterMime=@am,AfterImage=@ai,IsPublished=@pub,DisplayOrder=@ord WHERE GalleryID=@id;",cn))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=currentGalleryID;
                    cmd.Parameters.Add("@job",SqlDbType.Int).Value=(int)jobs.SelectedValue;
                    cmd.Parameters.Add("@title",SqlDbType.NVarChar,100).Value=title.Text.Trim();
                    cmd.Parameters.Add("@cat",SqlDbType.NVarChar,50).Value=category.Text.Trim();
                    cmd.Parameters.Add("@descr",SqlDbType.NVarChar,350).Value=string.IsNullOrWhiteSpace(description.Text)?(object)DBNull.Value:description.Text.Trim();
                    cmd.Parameters.Add("@bm",SqlDbType.VarChar,20).Value=b.Item2;
                    cmd.Parameters.Add("@bi",SqlDbType.VarBinary,-1).Value=b.Item1;
                    cmd.Parameters.Add("@am",SqlDbType.VarChar,20).Value=a.Item2;
                    cmd.Parameters.Add("@ai",SqlDbType.VarBinary,-1).Value=a.Item1;
                    cmd.Parameters.Add("@pub",SqlDbType.Bit).Value=publish.Checked;
                    cmd.Parameters.Add("@ord",SqlDbType.Int).Value=(int)order.Value;
                    cn.Open(); cmd.ExecuteNonQuery();
                }
                bool wasPublished = publish.Checked; LoadGallery(); ClearSelection();status.Text="Saved. "+(wasPublished?"Published":"Unpublished draft")+" gallery project.";
            }
            catch(Exception ex){MessageBox.Show("Unable to save gallery project: "+ex.Message,"Gallery Error");}
        }
        static int SelectedPhotoID(ComboBox box)
        {
            if(!(box.SelectedValue is int))throw new InvalidOperationException("Choose or browse both a BEFORE and an AFTER photo.");
            return (int)box.SelectedValue;
        }
        int SelectedGalleryID()
        {
            if(list.CurrentRow==null||list.CurrentRow.Cells["GalleryID"].Value==null)return 0;
            return Convert.ToInt32(list.CurrentRow.Cells["GalleryID"].Value);
        }
        void EditSelected()
        {
            int id=SelectedGalleryID();if(id==0)return;
            try
            {
                using(var cn=new SqlConnection(Conn))
                using(var cmd=new SqlCommand(@"SELECT JobID,Title,Category,Description,BeforeImage,AfterImage,IsPublished,DisplayOrder FROM dbo.PublicJobGallery WHERE GalleryID=@id",cn))
                {
                    cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cn.Open();
                    using(var r=cmd.ExecuteReader())
                    {
                        if(!r.Read())return;
                        jobs.SelectedValue=r.GetInt32(0);
                        title.Text=r.GetString(1);category.Text=r.GetString(2);description.Text=r.IsDBNull(3)?"":r.GetString(3);
                        beforeOverride=(byte[])r[4];afterOverride=(byte[])r[5];
                        var b=ValidImage(beforeOverride);var a=ValidImage(afterOverride);
                        beforeOverrideMime=b.Item2;afterOverrideMime=a.Item2;
                        ShowImage(beforePreview,beforeOverride);ShowImage(afterPreview,afterOverride);
                        publish.Checked=r.GetBoolean(6);order.Value=Math.Max(order.Minimum,Math.Min(order.Maximum,r.GetInt32(7)));
                        currentGalleryID=id;status.Text="Editing gallery entry #"+id+". Save to apply changes.";
                    }
                }
            }
            catch(Exception ex){MessageBox.Show(ex.Message,"Gallery Error");}
        }
        void ToggleSelected()
        {
            int id=SelectedGalleryID();if(id==0){MessageBox.Show("Select a gallery row first.");return;}
            try
            {
                using(var cn=new SqlConnection(Conn))
                using(var cmd=new SqlCommand(@"UPDATE dbo.PublicJobGallery SET IsPublished=CASE WHEN IsPublished=1 THEN 0 ELSE 1 END WHERE GalleryID=@id",cn))
                {cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;cn.Open();cmd.ExecuteNonQuery();}
                LoadGallery();status.Text="Publication status changed for gallery #"+id;
            }
            catch(Exception ex){MessageBox.Show(ex.Message,"Gallery Error");}
        }
        void ClearSelection()
        {
            currentGalleryID=0;title.Clear();category.Clear();description.Clear();
            publish.Checked=false;order.Value=100;beforeOverride=afterOverride=null;beforeOverrideMime=afterOverrideMime=null;
            ShowImage(beforePreview,null);ShowImage(afterPreview,null); LoadJobPhotos();status.Text="New gallery entry.";
        }
    }
}
