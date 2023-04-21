using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormObservacoesVendaRapida : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private string _observacoesAtualizada;
        private string _observacoesAntiga;

        #endregion

        #region " CONSTRUTOR "

        public FormObservacoesVendaRapida()
        {
            InitializeComponent();
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public string EditeObservacoes(string observacoes)
        {
            _observacoesAtualizada = observacoes;
            _observacoesAntiga = observacoes;

            txtObservacoesVendaRapida.Text = observacoes;

            this.AbrirTelaModal();

            return _observacoesAtualizada;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            _observacoesAtualizada = txtObservacoesVendaRapida.Text;

            this.Close();
        }

        #endregion
    }
}
