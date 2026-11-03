using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    // Add this existing item to the WinForms project in Visual Studio, then add a
    // Notifications menu item for access levels 5 and 4 that opens this form.
    public partial class CancellationNotificationsForm : Form
    {
        private readonly DataGridView grid = new DataGridView();
        private readonly Label unreadLabel = new Label();
        private readonly Button refresh = new Button();
        private readonly Button markRead = new Button();
        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["GroupWst1ConnString"].ConnectionString; }
        }

        public CancellationNotificationsForm()
        {
            if (UserSession.StaffID <= 0 ||
                (UserSession.AccessLevel != 4 &&
                 UserSession.AccessLevel != 5 && UserSession.AccessLevel != 6))
            {
                throw new UnauthorizedAccessException(
                    "Notifications are only for systems and operations managers.");
            }

            Text = "Cancellation Notifications";
            BackColor = Color.White;
            Padding = new Padding(15);
            Width = 1000;
            Height = 600;

            // Header panel
            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.FromArgb(30, 41, 59),
                Padding = new Padding(20)
            };

            Label title = new Label
            {
                Text = "Cancellation Notifications",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 12)
            };

            Label subtitle = new Label
            {
                Text = "Manage cancelled client job requests",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(22, 60)
            };

            header.Controls.Add(title);
            header.Controls.Add(subtitle);

            // Actions panel
            Panel actions = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            unreadLabel.Text = "Unread cancellations: 0";
            unreadLabel.Font = new Font(
                "Segoe UI", 11, FontStyle.Bold);
            unreadLabel.ForeColor = Color.Firebrick;
            unreadLabel.AutoSize = true;
            unreadLabel.Location = new Point(20, 25);

            refresh.Text = "Refresh";
            refresh.Size = new Size(110, 38);
            refresh.BackColor = Color.FromArgb(37, 99, 235);
            refresh.ForeColor = Color.White;
            refresh.FlatStyle = FlatStyle.Flat;
            refresh.FlatAppearance.BorderSize = 0;
            refresh.Click += (s, e) => LoadNotifications();

            markRead.Text = "Mark Selected as Read";
            markRead.Size = new Size(190, 38);
            markRead.BackColor = Color.FromArgb(22, 163, 74);
            markRead.ForeColor = Color.White;
            markRead.FlatStyle = FlatStyle.Flat;
            markRead.FlatAppearance.BorderSize = 0;
            markRead.Click += MarkSelectedRead;

            // Keep buttons on the right when resizing
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                Width = 325,
                Height = 55,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            buttonPanel.Controls.Add(refresh);
            buttonPanel.Controls.Add(markRead);

            actions.Controls.Add(buttonPanel);
            actions.Controls.Add(unreadLabel);

            // DataGridView styling
            grid.Dock = DockStyle.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersHeight = 45;
            grid.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(51, 65, 85);
            grid.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            grid.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);
            grid.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            grid.RowTemplate.Height = 40;

            grid.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);

            // Add controls in docking order.
            // Fill first, then top panels.
            Controls.Add(grid);
            Controls.Add(actions);
            Controls.Add(header);

            // Adjust layout for DPI and font scaling.
            AutoScaleMode = AutoScaleMode.Font;

            Load += (s, e) => LoadNotifications();
        }

        private void LoadNotifications()
        {
            const string sql = @"SELECT n.NotificationID, n.JobRequestID AS [Request #],
                 c.clientName + ' ' + c.clientSurname AS [Client], jr.siteAddress AS [Site],
                 n.CancelledAtUtc AS [Cancelled (UTC)],
                 CASE WHEN r.NotificationID IS NULL THEN 'Unread' ELSE 'Read' END AS [Status]
                 FROM dbo.RequestCancellationNotification n
                 JOIN dbo.Client c ON c.clientID=n.ClientID
                 JOIN dbo.JobRequest jr ON jr.jobRequestID=n.JobRequestID
                 LEFT JOIN dbo.RequestCancellationRead r ON r.NotificationID=n.NotificationID AND r.StaffID=@StaffID
                 ORDER BY n.CancelledAtUtc DESC;";
            try
            {
                using(var cn=new SqlConnection(ConnectionString))
                using(var cmd=new SqlCommand(sql,cn))
                using(var da=new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.Add("@StaffID",SqlDbType.Int).Value=UserSession.StaffID;
                    var dt=new DataTable(); da.Fill(dt); grid.DataSource=dt;
                    if(grid.Columns["NotificationID"]!=null) grid.Columns["NotificationID"].Visible=false;
                    int unread=0; foreach(DataRow row in dt.Rows) if(Convert.ToString(row["Status"])=="Unread") unread++;
                    unreadLabel.Text="Unread cancellations: "+unread;
                }
            }
            catch(Exception ex) { MessageBox.Show("Unable to load cancellation notifications: " + ex.Message); }
        }
        private void MarkSelectedRead(object sender,EventArgs e)
        {
            if(grid.CurrentRow==null) return;
            int id=Convert.ToInt32(grid.CurrentRow.Cells["NotificationID"].Value);
            const string sql=@"INSERT INTO dbo.RequestCancellationRead(NotificationID,StaffID)
              SELECT @ID,@StaffID WHERE EXISTS(SELECT 1 FROM dbo.RequestCancellationNotification WHERE NotificationID=@ID)
              AND NOT EXISTS(SELECT 1 FROM dbo.RequestCancellationRead WHERE NotificationID=@ID AND StaffID=@StaffID);";
            try
            {
                using(var cn=new SqlConnection(ConnectionString))
                using(var cmd=new SqlCommand(sql,cn))
                {
                    cmd.Parameters.Add("@ID",SqlDbType.Int).Value=id;
                    cmd.Parameters.Add("@StaffID",SqlDbType.Int).Value=UserSession.StaffID;
                    cn.Open(); cmd.ExecuteNonQuery();
                }
                LoadNotifications();
            }
            catch(Exception ex) { MessageBox.Show("Unable to mark notification as read: " + ex.Message); }
        }
    }
}
