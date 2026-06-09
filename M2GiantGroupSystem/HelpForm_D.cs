using M2GiantGroupSystem;
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
    public partial class HelpForm_D : Form
    {
        public HelpForm_D()
        {
            InitializeComponent();
            ThemeManager.ThemeChanged += ApplyTheme;
        }
        private void HelpForm_D_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            trkFontSize.Value = (int)ThemeManager.FontSizeOffset;
            lblFontSize.Text = $"Font Size: {trkFontSize.Value:+#;-#;0}";
            UpdateToggleLabel();
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            ThemeManager.SetDarkMode(!ThemeManager.IsDarkMode);
            UpdateToggleLabel();
        }

        private void trkFontSize_Scroll(object sender, EventArgs e)
        {
            ThemeManager.SetFontSizeOffset(trkFontSize.Value);
            lblFontSize.Text = $"Font Size: {trkFontSize.Value:+#;-#;0}";
        }

        private void UpdateToggleLabel()
        {
            btnThemeToggle.Text = ThemeManager.IsDarkMode ? "☀  Light Mode" : "🌙  Dark Mode";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.ThemeChanged -= ApplyTheme;
            base.OnFormClosed(e);
        }

        private void ApplyTheme()
        {
            if (ThemeManager.IsDarkMode)
                ThemeManager.ApplyTheme(this);
        }

        private void lblFontSize_Click(object sender, EventArgs e)
        {

        }
    }
}



