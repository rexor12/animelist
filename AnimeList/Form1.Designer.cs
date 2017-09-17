namespace AnimeList
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.downloadListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadAnimePlanetButton = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadMyAnimeListButton = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.layout = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadListToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // downloadListToolStripMenuItem
            // 
            this.downloadListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadAnimePlanetButton,
            this.downloadMyAnimeListButton});
            this.downloadListToolStripMenuItem.Name = "downloadListToolStripMenuItem";
            this.downloadListToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.downloadListToolStripMenuItem.Text = "Download list";
            // 
            // downloadAnimePlanetButton
            // 
            this.downloadAnimePlanetButton.Name = "downloadAnimePlanetButton";
            this.downloadAnimePlanetButton.Size = new System.Drawing.Size(147, 22);
            this.downloadAnimePlanetButton.Text = "Anime-Planet";
            this.downloadAnimePlanetButton.Click += new System.EventHandler(this.downloadAnimePlanetButton_Click);
            // 
            // downloadMyAnimeListButton
            // 
            this.downloadMyAnimeListButton.Name = "downloadMyAnimeListButton";
            this.downloadMyAnimeListButton.Size = new System.Drawing.Size(147, 22);
            this.downloadMyAnimeListButton.Text = "MyAnimeList";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 659);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // layout
            // 
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(1264, 681);
            this.layout.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.layout);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Rexor AnimeList";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem downloadListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadAnimePlanetButton;
        private System.Windows.Forms.ToolStripMenuItem downloadMyAnimeListButton;
        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

