using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M2GiantGroupSystem
{
    public static class ThemeManager
    
      
    {
        // ── Global State ──────────────────────────────────────────────
        public static bool IsDarkMode { get; private set; } = false;
        public static string FontName { get; private set; } = "Segoe UI";
        public static float FontSizeOffset { get; private set; } = 0f;

        // ── Event (all forms listen to this) ──────────────────────────
        public static event Action ThemeChanged;

        // ── Colours ───────────────────────────────────────────────────
        // Light: Seashell background + green controls
        // Dark:  Deep forest green theme

        public static Color BackColor => IsDarkMode ? Color.FromArgb(18, 28, 18) : Color.SeaShell;
        public static Color ForeColor => IsDarkMode ? Color.FromArgb(220, 240, 220) : Color.Black;
        public static Color PanelColor => IsDarkMode ? Color.FromArgb(30, 44, 30) : Color.FromArgb(198, 224, 180);
        public static Color GridBack => IsDarkMode ? Color.FromArgb(38, 54, 38) : Color.White;
        public static Color GridHeader => IsDarkMode ? Color.FromArgb(45, 90, 45) : Color.FromArgb(84, 130, 53);
        public static Color ButtonColor => IsDarkMode ? Color.FromArgb(45, 90, 45) : Color.FromArgb(84, 130, 53);

        // ── Setters (each one fires the event) ────────────────────────
        public static void SetDarkMode(bool dark)
        {
            IsDarkMode = dark;
            ThemeChanged?.Invoke();
        }

        public static void SetFont(string fontName)
        {
            FontName = fontName;
            ThemeChanged?.Invoke();
        }

        public static void SetFontSizeOffset(float offset)
        {
            FontSizeOffset = offset;
            ThemeChanged?.Invoke();
        }

        // ── Font Helper ───────────────────────────────────────────────
        public static Font GetFont(float baseSize = 9f, FontStyle style = FontStyle.Regular)
        {
            float size = Math.Max(6f, baseSize + FontSizeOffset);
            return new Font(FontName, size, style);
        }

        // ── Main Apply Method (called by every form) ──────────────────
        public static void ApplyTheme(Form form)
        {
            form.BackColor = BackColor;
            form.ForeColor = ForeColor;
            ApplyToControls(form.Controls, GetFont());
        }

        // ── Loops through every control on the form ───────────────────
        private static void ApplyToControls(Control.ControlCollection controls, Font font)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is PictureBox)
                    continue;
                ctrl.ForeColor = ForeColor;
                ctrl.Font = font;

                if (ctrl is Button btn)
                {
                    btn.BackColor = ButtonColor;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = IsDarkMode
                        ? Color.FromArgb(60, 110, 60)
                        : Color.FromArgb(60, 100, 35);
                }
                else if (ctrl is DataGridView dgv)
                {
                    ApplyDataGridTheme(dgv, font);
                    continue;
                }
                else if (ctrl is Panel || ctrl is GroupBox || ctrl is TabPage || ctrl is TabControl)
                {
                    ctrl.BackColor = PanelColor;
                }
                else if (ctrl is TextBox || ctrl is ComboBox || ctrl is DateTimePicker || ctrl is NumericUpDown)
                {
                    ctrl.BackColor = IsDarkMode ? Color.FromArgb(38, 54, 38) : Color.White;
                    ctrl.ForeColor = ForeColor;
                }
                else if (ctrl is Label || ctrl is CheckBox || ctrl is RadioButton)
                {
                    ctrl.BackColor = Color.Transparent;
                }
                else
                {
                    ctrl.BackColor = BackColor;
                }

                if (ctrl.HasChildren)
                    ApplyToControls(ctrl.Controls, font);
            }
        }

        // ── DataGridView Specific Styling ─────────────────────────────
        private static void ApplyDataGridTheme(DataGridView dgv, Font font)
        {
            dgv.BackgroundColor = GridBack;
            dgv.DefaultCellStyle.BackColor = GridBack;
            dgv.DefaultCellStyle.ForeColor = ForeColor;
            dgv.DefaultCellStyle.Font = font;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = GetFont(9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = IsDarkMode
                ? Color.FromArgb(60, 90, 60)
                : Color.FromArgb(180, 210, 160);

            if (dgv.HasChildren)
                ApplyToControls(dgv.Controls, font);
        }
    }
    

    }


