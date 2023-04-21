using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Report.RelatoriosDevExpress.Financeiro;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Telas.Financeiro.MovimentacoesCaixa;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormVisualizarMovimentacaoCaixa : FormularioBase
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoCaixa _movimentacaoCaixa;
        private List<ItemMovimentacaoCaixa> _listaItensMovimentacoes;

        #endregion

        #region " CONSTRUTOR "

        public FormVisualizarMovimentacaoCaixa()
        {
            InitializeComponent();

            _listaItensMovimentacoes = new List<ItemMovimentacaoCaixa>();

            PesquiseEPreenchaCaixaAberto();

            this.ActiveControl = gcItens;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            ImprimaRecibo();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormVisualizarMovimentacaoCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                ImprimaRecibo();
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PesquiseEPreenchaCaixaAberto()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();

            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa != null)
            {
                ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                _movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

                _listaItensMovimentacoes = _movimentacaoCaixa.ListaItensCaixa.ToList();

                PreenchaGrid();
                PreenchaResumo();
            }
        }

        private void PreenchaGrid()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa(false, false);

            _listaItensMovimentacoes = _listaItensMovimentacoes.OrderBy(x => x.DataHora).ToList();

            List<ItemMovimentacaoCaixaGrid> listaItemMovimentacaoCaixaGrid = new List<ItemMovimentacaoCaixaGrid>();

            double saldo = 0;

            foreach (var item in _listaItensMovimentacoes)
            {
                ItemMovimentacaoCaixaGrid itemMovimentacaoGrid = new ItemMovimentacaoCaixaGrid();

                itemMovimentacaoGrid.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                itemMovimentacaoGrid.FormaPagamento = item.FormaPagamento.Descricao;
                itemMovimentacaoGrid.HistoricoMovimentacao = item.HistoricoMovimentacoes;
                itemMovimentacaoGrid.Id = item.Id;

                if (item.Parceiro != null)
                {
                    var pessoa = servicoPessoa.Consulte(item.Parceiro.Id);

                    itemMovimentacaoGrid.Parceiro = pessoa != null ? pessoa.Id + " - " + pessoa.DadosGerais.Razao : string.Empty;
                }

                itemMovimentacaoGrid.EstahEstornado = item.EstahEstornado;
                itemMovimentacaoGrid.OrigemMovimentacao = item.OrigemMovimentacaoCaixa.Descricao();

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                {
                    itemMovimentacaoGrid.Entrada = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo += item.Valor;
                    }
                }
                else
                {
                    itemMovimentacaoGrid.Saida = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo -= item.Valor;
                    }
                }

                itemMovimentacaoGrid.Saldo = saldo.ToString("#,###,##0.00");

                listaItemMovimentacaoCaixaGrid.Add(itemMovimentacaoGrid);
            }

            gcItens.DataSource = listaItemMovimentacaoCaixaGrid;
            gcItens.RefreshDataSource();


            if (_listaItensMovimentacoes.Count > 0)
            {
                btnRecibo.Visible = true;
            }
            else
            {
                btnRecibo.Visible = false;
            }
        }

        private void PreenchaResumo()
        {
            double dinheiroEntrada = 0;
            double cartaoDebitoEntrada = 0;
            double cartaoCreditoEntrada = 0;
            double chequeEntrada = 0;
            double carteiraEntrada = 0;

            double dinheiroSaida = 0;
            double chequeSaida = 0;

            double dinheiroSaldoFinal = 0;
            double chequeSaldoFinal = 0;

            foreach (var item in _listaItensMovimentacoes)
            {
                if (item.EstahEstornado)
                {
                    continue;
                }

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        dinheiroEntrada += item.Valor;
                        dinheiroSaldoFinal += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        cartaoDebitoEntrada += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO)
                    {
                        cartaoCreditoEntrada += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeEntrada += item.Valor;
                        chequeSaldoFinal += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                    {
                        carteiraEntrada += item.Valor;
                    }
                }
                else
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        dinheiroSaida += item.Valor;
                        dinheiroSaldoFinal -= item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        chequeSaida += item.Valor;
                        chequeSaldoFinal -= item.Valor;
                    }
                }
            }

            txtDinheiroSaldoInicial.Text = _movimentacaoCaixa.SaldoInicial.ToDouble() == 0 ? _movimentacaoCaixa.SaldoInicialDinheiro.ToString("0.00") : _movimentacaoCaixa.SaldoInicial.ToString("0.00");
            txtChequeSaldoInicial.Text = _movimentacaoCaixa.SaldoInicialCheque.ToString("0.00");
            txtDinheiroEntrada.Text = dinheiroEntrada.ToString("0.00");
            txtCartaoDebitoEntrada.Text = cartaoDebitoEntrada.ToString("0.00");
            txtCartaoCreditoEntrada.Text = cartaoCreditoEntrada.ToString("0.00");
            txtChequeEntrada.Text = chequeEntrada.ToString("0.00");
            txtCarteiraEntrada.Text = carteiraEntrada.ToString("0.00");

            txtDinheiroSaida.Text = dinheiroSaida.ToString("0.00");
            txtChequeSaida.Text = chequeSaida.ToString("0.00");

            txtDinheiroSaldoFinal.Text = dinheiroSaldoFinal.ToString("0.00");
            txtChequeSaldoFinal.Text = chequeSaldoFinal.ToString("0.00");
        }

        private void ImprimaRecibo()
        {
            int idItemMovimentacao = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

            var itemMovimentacao = _listaItensMovimentacoes.FirstOrDefault(item => item.Id == idItemMovimentacao);

            if (itemMovimentacao.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
            {
                MessageBox.Show("Só é possível emitir o recibo para movimentações de saída.");
                return;
            }

            int? idParceiro = null;

            if (itemMovimentacao.Parceiro == null)
            {
                FormSelecaoClienteReciboMovimentacaoCaixa formSelecaoClienteReciboMovimentacaoCaixa = new FormSelecaoClienteReciboMovimentacaoCaixa();
                var resultado = formSelecaoClienteReciboMovimentacaoCaixa.SelecioneParceiro();

                if (resultado == DialogResult.Cancel)
                {
                    return;
                }

                if (formSelecaoClienteReciboMovimentacaoCaixa.Parceiro != null)
                {
                    idParceiro = formSelecaoClienteReciboMovimentacaoCaixa.Parceiro.Id;
                }
                else
                {
                    MessageBox.Show("É necessário informar um parceiro para emitir o recibo.");
                    return;
                }
            }

            RelatorioReciboCaixa relatorio = new RelatorioReciboCaixa(idItemMovimentacao, idParceiro);
            TratamentosDeTela.ExibirRelatorio(relatorio);
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemMovimentacaoCaixaGrid
        {
            public int Id { get; set; }

            public bool EstahEstornado { get; set; }

            public string DataHora { get; set; }

            public string Parceiro { get; set; }

            public string HistoricoMovimentacao { get; set; }

            public string FormaPagamento { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }

            public string Saldo { get; set; }

            public string OrigemMovimentacao { get; set; }
        }

        #endregion
    }
}
