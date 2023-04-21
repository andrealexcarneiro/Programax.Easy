using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Fiscal.NotasFiscais
{
    public partial class userControlPainelNcmAliquotaInterestadual : UserControl
    {
        public string CodigoNcm
        {
            get
            {
                return lblCodigoNcm.Text;
            }
            set
            {
                lblCodigoNcm.Text = value;
            }
        }

        public double FCP
        {
            get
            {
                return txtFCP.Text.ToDouble();
            }
            set
            {
                txtFCP.Text = value.ToString("0.00");
            }
        }

        public double AliquotaInterna
        {
            get
            {
                return txtAliquotaInterna.Text.ToDouble();
            }
            set
            {
                txtAliquotaInterna.Text = value.ToString("0.00");
            }
        }

        public userControlPainelNcmAliquotaInterestadual()
        {
            InitializeComponent();

            txtFCP.Focus();
        }
    }
}
