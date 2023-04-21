using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormManutencaoContaReceber : FormManutencaoContaPagarReceber
    {
        public FormManutencaoContaReceber()
        {
            InitializeComponent();
        }

        protected override ServicoContasPagarReceber RetorneServicoContaPagarOuReceber()
        {
            return new ServicoContasReceber();
        }

        protected override void EventoAposCarregarTitulo()
        {
            BloquearAlteracaoMultaEJuros();
        }

        private void BloquearAlteracaoMultaEJuros()
        {
            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
            var analiseCredito = servicoAnaliseCredito.Consulte(txtIdPessoa.Text.ToInt());

            if (analiseCredito == null) return;

            if (!analiseCredito.PodeAlterarMultaEJuros)
            {
                pnlJurosEhPercentual.Enabled = false;
                pnlMultaEhPercentual.Enabled = false;

                txtMulta.Properties.ReadOnly = true;
                txtJuros.Properties.ReadOnly = true;
            }
        }
    }
}
