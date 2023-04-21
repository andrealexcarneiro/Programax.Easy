using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;

namespace Programax.Easy.View.Telas.Financeiro.ContasPagarReceber
{
    public partial class FormInserirPagamentoParcial : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private ContaPagarReceber _contaPagarReceber;
        private DialogResult _resultado;
        private List<FormaPagamento> _listaFormasPagamentos;
        private ServicoContasPagarReceber _servicoContaPagarReceber;
        private EnumTipoFormaPagamento _tipoFormaPagamentoOrigem;

        #endregion

        #region " CONSTRUTOR "

        public FormInserirPagamentoParcial(EnumTipoFormaPagamento tipoFormaPagamentoOrigem)
        {
            InitializeComponent();

            PreenchaCboFormaPagamento();

            txtDataPagamento.DateTime = DateTime.Now;

            _tipoFormaPagamentoOrigem = tipoFormaPagamentoOrigem;

            this.ActiveControl = cboFormaPagamento;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Action actionSalvar = () =>
            {
                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();

                string mensagemSucesso = string.Empty;

                ContaPagarReceberPagamento contaPagarReceberPagamento = new ContaPagarReceberPagamento();
                contaPagarReceberPagamento.DataPagamento = txtDataPagamento.Text.ToDate();
                contaPagarReceberPagamento.FormaPagamento = _listaFormasPagamentos.FirstOrDefault(x => x.Id == cboFormaPagamento.EditValue.ToInt());
                contaPagarReceberPagamento.Observacoes = txtObservacoes.Text;
                contaPagarReceberPagamento.Valor = txtValor.Text.ToDouble();
                contaPagarReceberPagamento.Responsavel = Sessao.PessoaLogada;
                contaPagarReceberPagamento.ContaPagarReceber = _contaPagarReceber;
                

                servicoContasPagarReceberPagamento.ValideContaPagarReceberPagamento(contaPagarReceberPagamento);

                var contaPagarReceberClonado = _contaPagarReceber.CloneCompleto();

                contaPagarReceberClonado.ValorPago = contaPagarReceberClonado.ListaContasPagarReceberParcial.Sum(x => !x.EstahEstornado ? x.Valor : 0) +
                                                                             contaPagarReceberPagamento.Valor;
                
                if (contaPagarReceberClonado.ValorPago.ToString("0.00") == contaPagarReceberClonado.ValorTotal.ToString("0.00"))
                {
                    if (MessageBox.Show("O Valor à Pagar é igual ao Valor à Receber, portanto esse título será baixado.\n\nDeseja continuar?", "Baixar Título", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }

                    contaPagarReceberClonado.DataPagamento = txtDataPagamento.Text.ToDate();
                  
                    _servicoContaPagarReceber.BaixeContaPagarReceber(contaPagarReceberClonado, contaPagarReceberPagamento);

                    if  (_tipoFormaPagamentoOrigem == EnumTipoFormaPagamento.CHEQUE)
                    {
                        _servicoContaPagarReceber.AtualizarChequesContaPagarReceber(contaPagarReceberClonado);
                    }

                    mensagemSucesso = "Título quitado com sucesso.";
                }
                else
                {
                    if (MessageBox.Show("Será criado um recebimento parcial.\n\nDeseja Continuar?", "Recebimento Parcial", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }

                    _servicoContaPagarReceber.Atualize(contaPagarReceberClonado, contaPagarReceberPagamento);

                    mensagemSucesso = "Pagamento parcial inserido com sucesso.";
                }

                MessageBox.Show(mensagemSucesso, "Cadastro Salvo", MessageBoxButtons.OK);

                _resultado = DialogResult.OK;

                this.Close();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, exibirMensagemDeSucesso: false);
        }

        private void txtDataPagamento_EditValueChanged(object sender, EventArgs e)
        {
            CalculeValorAReceber();
        }

        private void txtObservacoes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public DialogResult InserirPagamentoParcial(ContaPagarReceber contaPagarReceber, ServicoContasPagarReceber servicoContaPagarReceber)
        {
            _servicoContaPagarReceber = servicoContaPagarReceber;
            _contaPagarReceber = contaPagarReceber;

            txtValorTitulo.Text = _contaPagarReceber.ValorParcela.ToString("#,##0.00");
            txtDataVencimento.Text = _contaPagarReceber.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy");

            CalculeValorAReceber();

            this.AbrirTelaModal();

            return _resultado;
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaCboFormaPagamento()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            var listaFormasPagamentos = servicoFormaPagamento.ConsulteListaAtivos();

            _listaFormasPagamentos = listaFormasPagamentos.CloneCompleto();

            cboFormaPagamento.Properties.DataSource = listaFormasPagamentos;
            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";

            cboFormaPagamento.EditValue = 1;
        }

        private void CalculeValorAReceber()
        {
            if (_contaPagarReceber == null)
            {
                return;
            }

            var contaPagarReceberClonada = _contaPagarReceber.CloneCompleto();

            contaPagarReceberClonada.DataPagamento = txtDataPagamento.Text.ToDate();
            contaPagarReceberClonada.Status = EnumStatusContaPagarReceber.QUITADO;

            var valorTotal = CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber(contaPagarReceberClonada);

            double valorReceber = valorTotal - _contaPagarReceber.ListaContasPagarReceberParcial.Sum(x => !x.EstahEstornado ? x.Valor : 0);

            txtValorAReceber.Text = valorReceber.ToString("#,##0.00");
        }

        #endregion
    }
}
