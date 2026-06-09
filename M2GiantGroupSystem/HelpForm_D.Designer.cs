namespace M2GiantGroupSystem
{
    partial class HelpForm_D
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm_D));
            this.btnThemeToggle = new System.Windows.Forms.Button();
            this.trkFontSize = new System.Windows.Forms.TrackBar();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThemeToggle
            // 
            this.btnThemeToggle.Location = new System.Drawing.Point(61, 43);
            this.btnThemeToggle.Name = "btnThemeToggle";
            this.btnThemeToggle.Size = new System.Drawing.Size(144, 39);
            this.btnThemeToggle.TabIndex = 0;
            this.btnThemeToggle.Text = "🌙  Dark Mode";
            this.btnThemeToggle.UseVisualStyleBackColor = true;
            this.btnThemeToggle.Click += new System.EventHandler(this.btnThemeToggle_Click);
            // 
            // trkFontSize
            // 
            this.trkFontSize.Location = new System.Drawing.Point(46, 125);
            this.trkFontSize.Name = "trkFontSize";
            this.trkFontSize.Size = new System.Drawing.Size(104, 56);
            this.trkFontSize.TabIndex = 1;
            this.trkFontSize.Scroll += new System.EventHandler(this.trkFontSize_Scroll);
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(58, 184);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(75, 16);
            this.lblFontSize.TabIndex = 2;
            this.lblFontSize.Text = "Font Size: 0";
            this.lblFontSize.Click += new System.EventHandler(this.lblFontSize_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Font Size ";
            // 
            // HelpForm_D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFontSize);
            this.Controls.Add(this.trkFontSize);
            this.Controls.Add(this.btnThemeToggle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpForm_D";
            this.Text = "Help ";
            this.Load += new System.EventHandler(this.HelpForm_D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThemeToggle;
        private System.Windows.Forms.TrackBar trkFontSize;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.Label label1;
    }
}