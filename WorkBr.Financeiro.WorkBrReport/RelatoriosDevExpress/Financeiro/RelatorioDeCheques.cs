using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioDeCheques : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Cheque> _listaDeCheques;
        private List<RelatorioCheques> relatorioCheques;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioDeCheques(List<Cheque> ListaDeCheques)
        {
            InitializeComponent();
            _tituloRelatorio = "MOVIMENTO DE CHEQUES";
            _listaDeCheques = ListaDeCheques;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            PreenchaTotaisCheques();
            PreenchaListaItens();
            PreenchaDataSource();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaTotaisCheques()
        {
            double totalAbertoDepositado = 0;
            int qtdAbertoDepositado = 0;

            double totalRecebido = 0;
            int qtdRecebido = 0;

            double totalDevolvidoPrimeira = 0;
            int qtdDevolvidoPrimeira = 0;

            double totalDevolvidoSegunda = 0;
            int qtdDevolvidoSegunda = 0;

            double totalReapresentado = 0;
            int qtdReapresentado = 0;

            double totalCustodiadoFactoring = 0;
            int qtdCustodiadoFactoring = 0;

            double totalInativo = 0;
            int qtdInativo = 0;

            double total = 0;
            int qtdTotal = 0;

            double totalAVencer = 0;
            int qtdAVencer = 0;

            double totalVencido = 0;
            int qtdVencido = 0;

            foreach (var cheque in _listaDeCheques)
            {
                if (cheque.StatusCheque == EnumStatusCheque.ABERTODEPOSITADO)
                {
                    totalAbertoDepositado += cheque.ValorCheque;
                    qtdAbertoDepositado++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.CUSTODIADOFACTORING)
                {
                    totalCustodiadoFactoring += cheque.ValorCheque;
                    qtdCustodiadoFactoring++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.DEVOLVIDO1)
                {
                    totalDevolvidoPrimeira += cheque.ValorCheque;
                    qtdDevolvidoPrimeira++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.DEVOLVIDO2)
                {
                    totalDevolvidoSegunda += cheque.ValorCheque;
                    qtdDevolvidoSegunda++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.INATIVO)
                {
                    totalInativo += cheque.ValorCheque;
                    qtdInativo++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.REAPRESENTADO)
                {
                    totalReapresentado += cheque.ValorCheque;
                    qtdReapresentado++;
                }
                else if (cheque.StatusCheque == EnumStatusCheque.RECEBIDO)
                {
                    totalRecebido += cheque.ValorCheque;
                    qtdRecebido++;
                }

                total += cheque.ValorCheque;
                qtdTotal++;

                if (cheque.StatusCheque != EnumStatusCheque.RECEBIDO && cheque.StatusCheque != EnumStatusCheque.INATIVO)
                {
                    if (cheque.DataVencimento >= DateTime.Now.Date.Date)
                    {
                        totalAVencer += cheque.ValorCheque;
                        qtdAVencer++;
                    }
                    else
                    {
                        totalVencido += cheque.ValorCheque;
                        qtdVencido++;
                    }
                }
            }

            lblTotalAbertoDepositado.Text = totalAbertoDepositado.ToString("#,###,##0.00") + "(" + qtdAbertoDepositado + ")";
            lblTotalRecebido.Text = totalRecebido.ToString("#,###,##0.00") + "(" + qtdRecebido + ")";
            lblTotalDevolvidaPrimeira.Text = totalDevolvidoPrimeira.ToString("#,###,##0.00") + "(" + qtdDevolvidoPrimeira + ")";
            lblTotalDevolvidaSegunda.Text = totalDevolvidoSegunda.ToString("#,###,##0.00") + "(" + qtdDevolvidoSegunda + ")";
            lblTotalReapresentado.Text = totalReapresentado.ToString("#,###,##0.00") + "(" + qtdReapresentado + ")";
            lblTotalCustodiadoFactoring.Text = totalCustodiadoFactoring.ToString("#,###,##0.00") + "(" + qtdCustodiadoFactoring + ")";
            lblTotalInativo.Text = totalInativo.ToString("#,###,##0.00") + "(" + qtdInativo + ")";
            lblTotal.Text = total.ToString("#,###,##0.00") + "(" + qtdTotal + ")";

            lblTotalAVencer.Text = totalAVencer.ToString("#,###,##0.00") + "(" + qtdAVencer + ")";
            lblTotalVencido.Text = totalVencido.ToString("#,###,##0.00") + "(" + qtdVencido + ")";
        }


        private void PreenchaListaItens()
        {
            List<RelatorioCheques> listaCheques = new List<RelatorioCheques>();

            foreach (var cheque in _listaDeCheques)
            {
                RelatorioCheques ItemCheque = new RelatorioCheques();

                ItemCheque.Id = cheque.Id;
                ItemCheque.Cliente = cheque.Pessoa != null ? cheque.Pessoa.Id + " - " + cheque.Pessoa.DadosGerais.Razao : string.Empty;
                ItemCheque.Agencia = cheque.Agencia;
                ItemCheque.Banco = cheque.Banco != null ? cheque.Banco.Descricao : string.Empty;
                ItemCheque.Conta = cheque.Conta;

                ItemCheque.DataEmissao = cheque.DataEmissao != null ? cheque.DataEmissao.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;
                ItemCheque.DataRecebimento = cheque.DataRecebimento != null ? cheque.DataRecebimento.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;
                ItemCheque.DataVencimento = cheque.DataVencimento != null ? cheque.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;
                ItemCheque.NumeroCheque = cheque.NumeroCheque;
                ItemCheque.Status = cheque.StatusCheque.Value.Descricao();

                ItemCheque.Valor = cheque.ValorCheque.ToString("0.00");
                ItemCheque.DataVencimento = cheque.DataVencimento != null ? cheque.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;

                listaCheques.Add(ItemCheque);
            }

            relatorioCheques = listaCheques;
        }

        private void PreenchaDataSource()
        {
            ConteudoRelatorio.DataSource = relatorioCheques;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class RelatorioCheques
        {
            public int Id { get; set; }

            public string Banco { get; set; }

            public string Agencia { get; set; }

            public string Conta { get; set; }

            public string NumeroCheque { get; set; }

            public string Valor { get; set; }

            public string Cliente { get; set; }

            public string DataEmissao { get; set; }

            public string DataVencimento { get; set; }

            public string DataRecebimento { get; set; }

            public string Status { get; set; }
        }

        #endregion
    }
}


