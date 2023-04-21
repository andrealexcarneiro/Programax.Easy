using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Programax.Easy.View.Componentes
{
    public partial class FormFundoModal : Form
    {
        private Form _formulario;

        public FormFundoModal()
        {
            InitializeComponent();

            this.Opacity = 0.7;
        }

        public void AbrirTela(Form form)
        {
            _formulario = form;

            this.ShowDialog();
        }

        public void AbrirTela(Form form, bool fundoModalCompleto)
        {
            _formulario = form;

            if (fundoModalCompleto)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }

            this.ShowDialog();
        }

        private void FormFundoModal_Load(object sender, EventArgs e)
        {
            _formulario.ShowDialog();

            this.Close();
        }
    }
}
