namespace SimpleScreenBright
{
    partial class SimpleScreenBright
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleScreenBright));
            this.monitorBrightnessTabControl = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.redetectMonitorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // monitorBrightnessTabControl
            // 
            this.monitorBrightnessTabControl.Location = new System.Drawing.Point(23, 51);
            this.monitorBrightnessTabControl.Name = "monitorBrightnessTabControl";
            this.monitorBrightnessTabControl.SelectedIndex = 0;
            this.monitorBrightnessTabControl.Size = new System.Drawing.Size(395, 171);
            this.monitorBrightnessTabControl.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(440, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveProfileToolStripMenuItem,
            this.loadProfileToolStripMenuItem,
            this.redetectMonitorsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveProfileToolStripMenuItem
            // 
            this.saveProfileToolStripMenuItem.Name = "saveProfileToolStripMenuItem";
            this.saveProfileToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.saveProfileToolStripMenuItem.Text = "&Save Profile";
            this.saveProfileToolStripMenuItem.Click += new System.EventHandler(this.saveProfileToolStripMenuItem_Click);
            // 
            // loadProfileToolStripMenuItem
            // 
            this.loadProfileToolStripMenuItem.Name = "loadProfileToolStripMenuItem";
            this.loadProfileToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.loadProfileToolStripMenuItem.Text = "&Load Profile";
            this.loadProfileToolStripMenuItem.Click += new System.EventHandler(this.loadProfileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // redetectMonitorsToolStripMenuItem
            // 
            this.redetectMonitorsToolStripMenuItem.Name = "redetectMonitorsToolStripMenuItem";
            this.redetectMonitorsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.redetectMonitorsToolStripMenuItem.Text = "&Redetect Monitor(s)";
            this.redetectMonitorsToolStripMenuItem.Click += new System.EventHandler(this.redetectMonitorsToolStripMenuItem_Click);
            // 
            // SimpleScreenBright
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(440, 246);
            this.Controls.Add(this.monitorBrightnessTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(448, 280);
            this.Name = "SimpleScreenBright";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Screen Brightness Adjuster";
            this.Load += new System.EventHandler(this.SimpleScreenBright_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl monitorBrightnessTabControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem redetectMonitorsToolStripMenuItem;
    }
}

