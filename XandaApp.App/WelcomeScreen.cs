using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace XandaApp.App
{
    public partial class WelcomeScreen : Form
    {
        private Timer timer1;
        private readonly ILogger<Main> _logger;

        public WelcomeScreen(ILogger<Main> logger)
        {
            InitializeComponent();
            _logger = logger;
            label1.Parent = pictureBox1;
        }

        private void WelcomeScreen_Load(object sender, EventArgs e)
        {
            timer1 = new Timer
            {
                Interval = 10000
            };

            timer1.Start();
            timer1.Tick += new EventHandler(ScreenTimerEvent);
        }

        public void ScreenTimerEvent(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            Hide();
            var main = new Main(_logger);
            main.Show();
            Close();
        }
    }
}
