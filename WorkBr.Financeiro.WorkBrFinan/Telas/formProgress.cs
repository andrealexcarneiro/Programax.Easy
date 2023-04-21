using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Programax.Easy.View.Telas
{
    public partial class FormProgress : Form
    {
        public FormProgress(bool AtivarLabel = false)
        {
            InitializeComponent();
            lblDownload.Visible = AtivarLabel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ProgressBar.Increment(1);
        }
    }
}
