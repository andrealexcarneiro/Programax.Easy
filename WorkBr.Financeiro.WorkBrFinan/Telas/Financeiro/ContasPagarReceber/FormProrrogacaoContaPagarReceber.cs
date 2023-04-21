using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormProrrogacaoContaPagarReceber : FormularioPadrao
    {
        private ContaPagarReceber _contaPagarReceber;
        private EnumTipoFormaPagamento _tipoFormaPagamentoOrigem;

        public FormProrrogacaoContaPagarReceber()
        {
            InitializeComponent();

            this.ActiveControl = txtDataVencimento;
        }

        public void EditarContaPagarReceber(ContaPagarReceber contaPagarReceber)
        {
            _contaPagarReceber = contaPagarReceber;
            
            _tipoFormaPagamentoOrigem = _contaPagarReceber.FormaPagamento.TipoFormaPagamento;

            PreenchaCamposContaPagarReceber();

            this.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void PreenchaCamposContaPagarReceber()
        {
            txtId.Text = _contaPagarReceber.Id.ToString();

            txtIdPessoa.Text = _contaPagarReceber.Pessoa.Id.ToString();
            txtNomePessoa.Text = _contaPagarReceber.Pessoa.DadosGerais.Razao;

            txtDataEmissao.Text = _contaPagarReceber.DataEmissao.ToString("dd/MM/yyyy");
            txtDataVencimento.DateTime = _contaPagarReceber.DataVencimento.GetValueOrDefault();

            txtNumeroDocumento.Text = _contaPagarReceber.NumeroDocumento;
            txtStatus.Text = _contaPagarReceber.Status.Descricao();

            txtUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

            txtValorParcela.Text = _contaPagarReceber.ValorParcela.ToString("#,###,##0.00");
            txtMulta.Text = _contaPagarReceber.Multa.ToString("#,###,##0.00");
            txtJuros.Text = _contaPagarReceber.Juros.ToString("#,###,##0.00");
            txtDesconto.Text = _contaPagarReceber.Desconto.ToString("#,###,##0.00");
            txtValorTotal.Text = _contaPagarReceber.ValorTotal.ToString("#,###,##0.00");

            if (_contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO || _contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO)
            {
                btnSalvar.Visible = false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Action actionAtualizeContaPagarReceber = () =>
            {
                ServicoContasPagarReceber servicoContasPagarReceber = RetorneServicoContaPagarOuReceber();

                var contaPagarReceber = servicoContasPagarReceber.Consulte(_contaPagarReceber.Id);

                contaPagarReceber.Status = EnumStatusContaPagarReceber.ABERTOPRORROGADO;
                contaPagarReceber.DataVencimento = txtDataVencimento.Text.ToDate();

                servicoContasPagarReceber.Atualize(contaPagarReceber, txtObs.Text);

                if (_tipoFormaPagamentoOrigem == EnumTipoFormaPagamento.CHEQUE)
                {  
                    servicoContasPagarReceber.AtualizarChequesContaPagarReceber(contaPagarReceber);
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionAtualizeContaPagarReceber, this, fecharFormAoConcluirOperacao: true);
        }

        protected virtual ServicoContasPagarReceber RetorneServicoContaPagarOuReceber()
        {
            return null;
        }

        private void txtDataVencimento_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDataVencimento.Text))
            {
                _contaPagarReceber.DataVencimento = txtDataVencimento.Text.ToDate();

                var valorTotal = CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber(_contaPagarReceber);

                txtValorTotal.Text = valorTotal.ToString("#,###,##0.00");
            }
        }
    }
}
