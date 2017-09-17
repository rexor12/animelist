namespace AnimeList.Layouts
{
    partial class AnimeListLayout
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.animeGridView = new AnimeList.UserInterface.BetterGridView();
            this.SuspendLayout();
            // 
            // animeGridView
            // 
            this.animeGridView.AutoScroll = true;
            this.animeGridView.AutoScrollMinSize = new System.Drawing.Size(175, 0);
            this.animeGridView.ButtonHeight = 200F;
            this.animeGridView.ButtonMargin = 20F;
            this.animeGridView.ButtonWidth = 100F;
            this.animeGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.animeGridView.Location = new System.Drawing.Point(0, 0);
            this.animeGridView.Name = "animeGridView";
            this.animeGridView.Size = new System.Drawing.Size(210, 132);
            this.animeGridView.TabIndex = 0;
            // 
            // AnimeListLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.animeGridView);
            this.Name = "AnimeListLayout";
            this.Size = new System.Drawing.Size(210, 132);
            this.ResumeLayout(false);

        }

        #endregion

        private UserInterface.BetterGridView animeGridView;
    }
}
