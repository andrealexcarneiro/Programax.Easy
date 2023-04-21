using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormCadastroContasReceber : FormCadastroContasPagarReceber
    {
        public FormCadastroContasReceber()
        {
            InitializeComponent();

            _tipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;

            ServicoParametros servicoParametros = new ServicoParametros();
            var parametros = servicoParametros.ConsulteParametros();

            if (parametros.ParametrosFinanceiro != null)
            {
                txtMulta.Text = parametros.ParametrosFinanceiro.MultaContasReceber.ToString("0.00");
                txtJuros.Text = parametros.ParametrosFinanceiro.JurosContasReceber.ToString("0.00");
            }
        }

        protected override ServicoContasPagarReceber RetorneServicoContasPagarOuReceber()
        {
            return new ServicoContasReceber();
        }

        protected override void PreenchaPessoa(Negocio.Cadastros.PessoaObj.ObjetoDeNegocio.Pessoa pessoa, bool exibirMensagemDeNaoEncontrado = false)
        {
            base.PreenchaPessoa(pessoa, exibirMensagemDeNaoEncontrado);

            if (pessoa != null)
            {
                BloquearOuDesbloquearAlteracaoMultaEJuros();
            }
        }

        private void BloquearOuDesbloquearAlteracaoMultaEJuros()
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
            else
            {
                pnlJurosEhPercentual.Enabled = true;
                pnlMultaEhPercentual.Enabled = true;

                txtMulta.Properties.ReadOnly = false;
                txtJuros.Properties.ReadOnly = false;
            }
        }
    }
}
