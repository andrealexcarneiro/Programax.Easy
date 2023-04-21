using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioMovimentacaoCaixa : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private int _idMovimentacaoCaixa;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioMovimentacaoCaixa(int idMovimentacaoCaixa)
        {
            InitializeComponent();

            _tituloRelatorio = "MOVIMENTO DE CAIXA";
            _idMovimentacaoCaixa = idMovimentacaoCaixa;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();
            var movimentacaoCaixa = servicoMovimentacaoCaixa.Consulte(_idMovimentacaoCaixa);
            MovimentacaoCaixaRelatorio movimentacaoCaixaRelatorio = new MovimentacaoCaixaRelatorio();

            PreenchaInformacoesMovimentacaoECaixa(movimentacaoCaixa, movimentacaoCaixaRelatorio);
            PreenchaTotaisMovimentacao(movimentacaoCaixa, movimentacaoCaixaRelatorio);
            PreenchaListaItens(movimentacaoCaixa, movimentacaoCaixaRelatorio);

            PreenchaDataSource(movimentacaoCaixaRelatorio);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaInformacoesMovimentacaoECaixa(MovimentacaoCaixa movimentacaoCaixa, MovimentacaoCaixaRelatorio movimentacaoCaixaRelatorio)
        {
            movimentacaoCaixaRelatorio.DataMovimentacao = movimentacaoCaixa.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy");
            movimentacaoCaixaRelatorio.NomeCaixa = movimentacaoCaixa.Caixa.Id + " - " + movimentacaoCaixa.Caixa.NomeCaixa;
            movimentacaoCaixaRelatorio.OperadorCaixa = movimentacaoCaixa.Caixa.Funcionario.DadosGerais.Razao;
        }

        private void PreenchaTotaisMovimentacao(MovimentacaoCaixa movimentacaoCaixa, MovimentacaoCaixaRelatorio movimentacaoCaixaRelatorio)
        {
            double entradaDinheiro = 0;
            double entradaCartaoDebito = 0;
            double entradaCartaoCredito = 0;
            double entradaCheque = 0;
            double entradaCarteira = 0;
            double saidaDinheiro = 0;
            double saidaCheque = 0;
            double saldoDinheiro = 0;
            double saldoCheque = 0;

            foreach (var item in movimentacaoCaixa.ListaItensCaixa)
            {
                if (item.EstahEstornado)
                {
                    continue;
                }

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        entradaDinheiro += item.Valor;
                        saldoDinheiro += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        entradaCartaoDebito += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO)
                    {
                        entradaCartaoCredito += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        entradaCheque += item.Valor;
                        saldoCheque += item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO)
                    {
                        entradaCarteira += item.Valor;
                    }
                }
                else
                {
                    if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO)
                    {
                        saidaDinheiro += item.Valor;
                        saldoDinheiro -= item.Valor;
                    }
                    else if (item.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE)
                    {
                        saidaCheque += item.Valor;
                        saldoCheque -= item.Valor;
                    }
                }
            }

            movimentacaoCaixaRelatorio.SaldoInicialDinheiro = movimentacaoCaixa.SaldoInicial.ToDouble() == 0? movimentacaoCaixa.SaldoInicialDinheiro.ToString("#,###,##0.00"): movimentacaoCaixa.SaldoInicial.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.SaldoInicialCheque = movimentacaoCaixa.SaldoInicialCheque.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.EntradaDinheiro = entradaDinheiro.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.EntradaCartaoDebito = entradaCartaoDebito.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.EntradaCartaoCredito = entradaCartaoCredito.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.EntradaCheque = entradaCheque.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.EntradaCarteira = entradaCarteira.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.SaidaDinheiro = saidaDinheiro.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.SaidaCheque = saidaCheque.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.SaldoFinalDinheiro = saldoDinheiro.ToString("#,###,##0.00");
            movimentacaoCaixaRelatorio.SaldoFinalCheque = saldoCheque.ToString("#,###,##0.00");
        }

        private void PreenchaListaItens(MovimentacaoCaixa movimentacaoCaixa, MovimentacaoCaixaRelatorio movimentacaoCaixaRelatorio)
        {
            double saldo = 0;

            foreach (var item in movimentacaoCaixa.ListaItensCaixa)
            {
                ItemMovimentacaoCaixaRelatorio itemMovimentacaoCaixaRelatorio = new ItemMovimentacaoCaixaRelatorio();

                if (item.Parceiro != null)
                {
                    itemMovimentacaoCaixaRelatorio.CodigoParceiro = item.Parceiro.Id.ToString();
                    itemMovimentacaoCaixaRelatorio.NomeParceiro = item.Parceiro.DadosGerais.NomeFantasia;
                }

                itemMovimentacaoCaixaRelatorio.DataHora = item.DataHora.ToString("dd/MM/yyyy HH:mm");
                itemMovimentacaoCaixaRelatorio.FormaPagamento = item.FormaPagamento.TipoFormaPagamento.Descricao();
                itemMovimentacaoCaixaRelatorio.HistoricoMovimentacao = item.EstahEstornado ? "(ESTORNO) " + item.HistoricoMovimentacoes : item.HistoricoMovimentacoes;

                if (item.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO)
                {
                    itemMovimentacaoCaixaRelatorio.Entrada = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo += item.Valor;
                    }
                }
                else
                {
                    itemMovimentacaoCaixaRelatorio.Saida = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo -= item.Valor;
                    }
                }

                itemMovimentacaoCaixaRelatorio.EstahEstornado = item.EstahEstornado;

                itemMovimentacaoCaixaRelatorio.Saldo = saldo.ToString("#,###,##0.00");

                movimentacaoCaixaRelatorio.ListaItensMovimentacaoCaixa.Add(itemMovimentacaoCaixaRelatorio);
            }
        }

        private void PreenchaDataSource(MovimentacaoCaixaRelatorio movimentacaoCaixaRelatorio)
        {
            List<MovimentacaoCaixaRelatorio> listaMovimentacoes = new List<MovimentacaoCaixaRelatorio>();
            listaMovimentacoes.Add(movimentacaoCaixaRelatorio);

            ConteudoRelatorio.DataSource = listaMovimentacoes;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class MovimentacaoCaixaRelatorio
        {
            public MovimentacaoCaixaRelatorio()
            {
                ListaItensMovimentacaoCaixa = new List<ItemMovimentacaoCaixaRelatorio>();
            }

            public string NomeCaixa { get; set; }

            public string OperadorCaixa { get; set; }

            public string DataMovimentacao { get; set; }

            public string SaldoInicialDinheiro { get; set; }
            
            public string SaldoInicialCheque { get; set; }

            public string EntradaDinheiro { get; set; }

            public string EntradaCartaoDebito { get; set; }

            public string EntradaCartaoCredito { get; set; }

            public string EntradaCheque { get; set; }

            public string EntradaCarteira { get; set; }

            public string SaidaDinheiro { get; set; }

            public string SaidaCheque { get; set; }

            public string SaldoFinalDinheiro { get; set; }

            public string SaldoFinalCheque { get; set; }

            public List<ItemMovimentacaoCaixaRelatorio> ListaItensMovimentacaoCaixa { get; set; }
        }

        public class ItemMovimentacaoCaixaRelatorio
        {
            public string DataHora { get; set; }

            public string CodigoParceiro { get; set; }

            public string NomeParceiro { get; set; }

            public string HistoricoMovimentacao { get; set; }

            public string FormaPagamento { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }

            public string Saldo { get; set; }

            public bool EstahEstornado { get; set; }
        }

        #endregion
    }
}
