using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using Programax.Easy.Servico.Cadastros.CaixaServ;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormFecharCaixaPdv : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoCaixa _movimentacaoCaixa;

        #endregion

        #region " CONSTRUTOR "

        public FormFecharCaixaPdv()
        {
            InitializeComponent();

            PesquiseCaixa();

            this.ActiveControl = rdbSaldoCorreto;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            FecheCaixa();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbSaldoCorreto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSaldoCorreto.Checked)
            {
                txtDiferenca.Text = string.Empty;
                txtDiferenca.Properties.ReadOnly = true;
            }
            else
            {
                txtDiferenca.Properties.ReadOnly = false;
            }
        }

        private void FormFecharCaixaPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                FecheCaixa();
            }
        }

        #endregion

        #region " MÉTODOS PÚBLICOS "

        private void PesquiseCaixa()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                MessageBoxAkil.Show("Usuário logado não contém caixa");

                return;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa(false, false);
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            PreenchaDadosMovimentacaoCaixa(movimentacaoCaixa);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaDadosMovimentacaoCaixa(MovimentacaoCaixa movimentacaoCaixa)
        {
            if (movimentacaoCaixa == null)
            {
                return;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
            var movimentacao = servicoMovimentacaoCaixa.Consulte(movimentacaoCaixa.Id);

            _movimentacaoCaixa = movimentacao;

            _movimentacaoCaixa.SaldoFinalDinheiro = movimentacaoCaixa.SaldoFinalDinheiro;
            _movimentacaoCaixa.SaldoFinalCheque = movimentacaoCaixa.SaldoFinalCheque;

            txtIdCaixa.Text = _movimentacaoCaixa.Caixa.Id.ToString();
            txtNomeCaixa.Text = _movimentacaoCaixa.Caixa.NomeCaixa;
            txtNrRegistroCaixa.Text = _movimentacaoCaixa.Id.ToString();

            txtDataHoraFechamento.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtUsuarioFechamento.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;

            double totalCheque = 0;
            double totalCartaoDebito = 0;
            double totalCartaoCredito = 0;
            double totalDinheiro = 0;

            foreach (var item in _movimentacaoCaixa.ListaItensCaixa)
            {
                if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                {
                    if (!item.EstahEstornado)
                        if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                        {
                            totalDinheiro += item.Valor;
                        }
                        else
                        {
                            totalDinheiro -= item.Valor;
                        }
                }
                else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                {
                    if (!item.EstahEstornado)
                        if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                        {
                            totalCheque += item.Valor;
                        }
                        else
                        {
                            totalCheque -= item.Valor;
                        }
                }
                else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO)
                {
                    if (!item.EstahEstornado)
                        if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                        {
                            totalCartaoDebito += item.Valor;
                        }
                        else
                        {
                            totalCartaoDebito -= item.Valor;
                        }
                }
                else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO)
                {
                    if (!item.EstahEstornado)
                        if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                        {
                            totalCartaoCredito += item.Valor;
                        }
                        else
                        {
                            totalCartaoCredito -= item.Valor;
                        }
                }
            }

            txtTotalCartaoCredito.Text = totalCartaoCredito.ToString("#,###,##0.00");
            txtTotalCartaoDebito.Text = totalCartaoDebito.ToString("#,###,##0.00");
            txtTotalCheque.Text = totalCheque.ToString("#,###,##0.00");
            txtTotalDinheiro.Text = totalDinheiro.ToString("#,###,##0.00");
        }

        private void FecheCaixa()
        {
            Action actionMovimentacao = () =>
            {
                Caixa caixa = new Caixa { Id = txtIdCaixa.Text.ToInt() };

                var movimentacaoCaixaClone = _movimentacaoCaixa.CloneCompleto();

                movimentacaoCaixaClone.Caixa = caixa;
                movimentacaoCaixaClone.ObservacoesFechamento = txtObs.Text;
                movimentacaoCaixaClone.Status = EnumStatusMovimentacaoCaixa.FECHADO;
                movimentacaoCaixaClone.UsuarioFechamento = Sessao.PessoaLogada;
                movimentacaoCaixaClone.DataHoraFechamento = txtDataHoraFechamento.Text.ToDate();

                movimentacaoCaixaClone.ResultadoCaixa = (EnumResultadoCaixa)pnlResultadoCaixa.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
                movimentacaoCaixaClone.DiferencaSaldoFinalCaixa = txtDiferenca.Text.ToDouble();

                movimentacaoCaixaClone.SaldoFinalDinheiro = txtTotalDinheiro.Text.ToDouble();
                movimentacaoCaixaClone.SaldoFinalCheque = txtTotalCheque.Text.ToDouble();
                
                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                servicoMovimentacaoCaixa.Atualize(movimentacaoCaixaClone);

                _movimentacaoCaixa = servicoMovimentacaoCaixa.Consulte(movimentacaoCaixaClone.Id);

                if (MessageBox.Show("Deseja emitir o relatório de movimentação de caixa?", "Emitir relatório de movimentação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    RelatorioMovimentacaoCaixa relatorioMovimentacaoCaixa = new RelatorioMovimentacaoCaixa(_movimentacaoCaixa.Id);
                    TratamentosDeTela.ExibirRelatorio(relatorioMovimentacaoCaixa);
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionMovimentacao, this, fecharFormAoConcluirOperacao: true, mensagemDeSucesso: "O Caixa foi fechado com sucesso!");
        }

        #endregion
    }
}
