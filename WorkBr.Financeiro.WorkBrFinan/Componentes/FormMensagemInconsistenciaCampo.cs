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
    public partial class FormMensagemInconsistenciaCampo : Form
    {
        private List<string> _inconsistencias;

        public FormMensagemInconsistenciaCampo(List<string> inconsistencias)
        {
            InitializeComponent();

            _inconsistencias = inconsistencias;

            MonteInconsistencias();

            this.Opacity = 0.95;
        }

        private void MonteInconsistencias()
        {
            StringBuilder stringBuilderInconsistencias = new StringBuilder();

            foreach (var item in _inconsistencias)
            {
                stringBuilderInconsistencias.AppendLine(item);
            }

            lblNaturezaOperacao.Text = stringBuilderInconsistencias.ToString();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
