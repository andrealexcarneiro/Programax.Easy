using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace WorkBr.Financeiro.WorkBrFinanc.Negocio
{
    public partial class XtraForm2 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm2()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            iniciar_UserControl(backstageViewTabItem1,(new uCtrlConsulta()));
        }

        private void iniciar_UserControl(DevExpress.XtraBars.Ribbon.BackstageViewTabItem pControl, DevExpress.XtraEditors.XtraUserControl pUserControl)
        {
            pUserControl.Dock = DockStyle.Fill;
            pUserControl.AutoSize = true;
            pUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            pControl.ContentControl.Controls.Add(pUserControl);
            pUserControl.Show();


        }

    }
}