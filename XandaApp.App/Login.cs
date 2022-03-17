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
    public partial class Login : Form
    {
        private readonly ILogger<Main> _logger;

        public Login(ILogger<Main> logger)
        {
            InitializeComponent();
            _logger = logger;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Do login checks first before this
            this.Hide();
            var welcomeScreen = new WelcomeScreen(_logger);
            welcomeScreen.Show();
        }
    }
}
