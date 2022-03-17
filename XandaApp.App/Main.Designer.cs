namespace XandaApp.App
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.xandaVideoPlayer = new LibVLCSharp.WinForms.VideoView();
            this.messagePanel = new System.Windows.Forms.Panel();
            this.messageLabel = new XandaApp.App.MarqueeLabel();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.picturePanel = new System.Windows.Forms.Panel();
            this.xandaPicture = new System.Windows.Forms.PictureBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.xandaSplitter = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.xandaVideoPlayer)).BeginInit();
            this.messagePanel.SuspendLayout();
            this.videoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.picturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xandaPicture)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xandaSplitter)).BeginInit();
            this.xandaSplitter.Panel1.SuspendLayout();
            this.xandaSplitter.Panel2.SuspendLayout();
            this.xandaSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // xandaVideoPlayer
            // 
            this.xandaVideoPlayer.BackColor = System.Drawing.Color.Black;
            this.xandaVideoPlayer.Location = new System.Drawing.Point(382, 102);
            this.xandaVideoPlayer.MediaPlayer = null;
            this.xandaVideoPlayer.Name = "xandaVideoPlayer";
            this.xandaVideoPlayer.Size = new System.Drawing.Size(255, 167);
            this.xandaVideoPlayer.TabIndex = 1;
            this.xandaVideoPlayer.Text = "videoView1";
            this.xandaVideoPlayer.Visible = false;
            // 
            // messagePanel
            // 
            this.messagePanel.Controls.Add(this.messageLabel);
            this.messagePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.messagePanel.Location = new System.Drawing.Point(0, 813);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(1474, 70);
            this.messagePanel.TabIndex = 9;
            this.messagePanel.Visible = false;
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.White;
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.messageLabel.Location = new System.Drawing.Point(0, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(1474, 70);
            this.messageLabel.TabIndex = 11;
            // 
            // videoPanel
            // 
            this.videoPanel.Controls.Add(this.webView21);
            this.videoPanel.Controls.Add(this.xandaVideoPlayer);
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(0, 0);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(676, 813);
            this.videoPanel.TabIndex = 10;
            // 
            // webView21
            // 
            this.webView21.BackColor = System.Drawing.Color.Black;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Location = new System.Drawing.Point(36, 222);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(308, 196);
            this.webView21.TabIndex = 7;
            this.webView21.Visible = false;
            this.webView21.ZoomFactor = 1D;
            // 
            // picturePanel
            // 
            this.picturePanel.Controls.Add(this.xandaPicture);
            this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picturePanel.Location = new System.Drawing.Point(0, 0);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(794, 813);
            this.picturePanel.TabIndex = 9;
            // 
            // xandaPicture
            // 
            this.xandaPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xandaPicture.Location = new System.Drawing.Point(0, 0);
            this.xandaPicture.Name = "xandaPicture";
            this.xandaPicture.Size = new System.Drawing.Size(794, 813);
            this.xandaPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xandaPicture.TabIndex = 3;
            this.xandaPicture.TabStop = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.xandaSplitter);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1474, 813);
            this.mainPanel.TabIndex = 10;
            // 
            // xandaSplitter
            // 
            this.xandaSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xandaSplitter.Location = new System.Drawing.Point(0, 0);
            this.xandaSplitter.Name = "xandaSplitter";
            // 
            // xandaSplitter.Panel1
            // 
            this.xandaSplitter.Panel1.AccessibleName = "picturePanel";
            this.xandaSplitter.Panel1.Controls.Add(this.picturePanel);
            // 
            // xandaSplitter.Panel2
            // 
            this.xandaSplitter.Panel2.AccessibleName = "videoPanel";
            this.xandaSplitter.Panel2.Controls.Add(this.videoPanel);
            this.xandaSplitter.Size = new System.Drawing.Size(1474, 813);
            this.xandaSplitter.SplitterDistance = 794;
            this.xandaSplitter.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 883);
            this.ControlBox = false;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.messagePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xandaVideoPlayer)).EndInit();
            this.messagePanel.ResumeLayout(false);
            this.videoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.picturePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xandaPicture)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.xandaSplitter.Panel1.ResumeLayout(false);
            this.xandaSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xandaSplitter)).EndInit();
            this.xandaSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private LibVLCSharp.WinForms.VideoView xandaVideoPlayer;
        private System.Windows.Forms.Panel messagePanel;
        public MarqueeLabel messageLabel;
        private System.Windows.Forms.Panel videoPanel;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Panel picturePanel;
        private System.Windows.Forms.PictureBox xandaPicture;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.SplitContainer xandaSplitter;
    }
}
