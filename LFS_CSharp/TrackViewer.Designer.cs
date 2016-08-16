namespace LFS_CSharp
{
    partial class TrackViewer
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
            this.btn_open_track = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_open_track
            // 
            this.btn_open_track.Location = new System.Drawing.Point(13, 13);
            this.btn_open_track.Name = "btn_open_track";
            this.btn_open_track.Size = new System.Drawing.Size(75, 23);
            this.btn_open_track.TabIndex = 0;
            this.btn_open_track.Text = "Open Track";
            this.btn_open_track.UseVisualStyleBackColor = true;
            this.btn_open_track.Click += new System.EventHandler(this.btn_open_track_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(667, 405);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // TrackViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 460);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_open_track);
            this.Name = "TrackViewer";
            this.Text = "TrackViewer";
<<<<<<< HEAD
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrackViewer_FormClosing);
=======
>>>>>>> 74f6ad01297260e38313f2d9b746c89faa42e76b
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TrackViewer_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_open_track;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}