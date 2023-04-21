using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System.Linq;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioContasPagarReceber : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoa;
        private EnumDataFiltrarContasPagarReceber? _dataFiltrar;
        private DateTime? _dataInicialPeriodo;
        private DateTime? _dataFinalPeriodo;
        private EnumStatusContaPagarReceber? _statusContaPagarReceber;
        private EnumTipoOperacaoContasPagarReceber _tipoOperacao;
        private EnumOrdenacaoPesquisaContasPagarReceber _ordenacaoPesquisaContasPagarReceber;
        private int? _categoriaId;
        

        #endregion

        #region " CONSTRUTOR "

        public RelatorioContasPagarReceber(Pessoa pessoa,
                                                            EnumDataFiltrarContasPagarReceber? dataFiltrar,
                                                            DateTime? dataInicialPeriodo,
                                                            DateTime? dataFinalPeriodo,
                                                            EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                            EnumTipoOperacaoContasPagarReceber tipoOperacao,
                                                            EnumOrdenacaoPesquisaContasPagarReceber ordenacaoPesquisaContasPagarReceber,
                                                            int? categoriaFinanceiraId)
        {
            InitializeComponent();

            _pessoa = pessoa;
            _dataFiltrar = dataFiltrar;
            _dataInicialPeriodo = dataInicialPeriodo;
            _dataFinalPeriodo = dataFinalPeriodo;
            _statusContaPagarReceber = statusContaPagarReceber;
            _tipoOperacao = tipoOperacao;
            _ordenacaoPesquisaContasPagarReceber = ordenacaoPesquisaContasPagarReceber;
            _categoriaId = categoriaFinanceiraId;

            if (tipoOperacao == EnumTipoOperacaoContasPagarReceber.PAGAR)
            {
                _tituloRelatorio = "RELATÓRIO DE CONTAS A PAGAR";
            }
            else
            {
                _tituloRelatorio = "RELATÓRIO DE CONTAS A RECEBER";
            }
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            bool registro = false;

            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var listaContasPagarReceber = servicoContasPagarReceber.ConsulteListaFazendoFetchComParceiroEEnderecos(_pessoa,
                                                                                                                                                                         _dataFiltrar,
                                                                                                                                                                         _dataInicialPeriodo,
                                                                                                                                                                         _dataFinalPeriodo,
                                                                                                                                                                         _statusContaPagarReceber,
                                                                                                                                                                         _tipoOperacao,
                                                                                                                                                                         _ordenacaoPesquisaContasPagarReceber,
                                                                                                                                                                         _categoriaId);

            List<ContasPagarReceberRelatorio> listaContasPagarReceberRelatorio = new List<ContasPagarReceberRelatorio>();

            foreach (var contaPagarReceber in listaContasPagarReceber)
            {
                ContasPagarReceberRelatorio contasPagarReceberRelatorio = new ContasPagarReceberRelatorio();

                contasPagarReceberRelatorio.Id = contaPagarReceber.Id;

                contasPagarReceberRelatorio.CodigoParceiro = contaPagarReceber.Pessoa.Id.ToString();
                contasPagarReceberRelatorio.NomeParceiro = contaPagarReceber.Pessoa.DadosGerais.Razao;

                contasPagarReceberRelatorio.NumeroDocumento = contaPagarReceber.NumeroDocumento;

                contasPagarReceberRelatorio.Categoria = contaPagarReceber.CategoriaFinanceira != null ? contaPagarReceber.CategoriaFinanceira.Descricao : string.Empty;

                contasPagarReceberRelatorio.Historico = contaPagarReceber.Historico;

                contasPagarReceberRelatorio.Situacao = contaPagarReceber.Status.Descricao();

                int diasAtraso = RetorneDiasDeAtraso(contaPagarReceber);

                contasPagarReceberRelatorio.Atraso = diasAtraso + " dias";
                contasPagarReceberRelatorio.DataVencimento = contaPagarReceber.DataVencimento != null ? contaPagarReceber.DataVencimento.Value.ToString("dd/MM/yyyy") : string.Empty;
                contasPagarReceberRelatorio.DataPagamento = contaPagarReceber.DataPagamento != null ? contaPagarReceber.DataPagamento.Value.ToString("dd/MM/yyyy") : string.Empty;

                contasPagarReceberRelatorio.DataEmissao = contaPagarReceber.DataEmissao != null ? contaPagarReceber.DataEmissao.ToString("dd/MM/yyyy") : string.Empty;

                contasPagarReceberRelatorio.Desconto = "R$ " + contaPagarReceber.Desconto.ToString("#,###,##0.00");
                contasPagarReceberRelatorio.MultaJuros = "R$ " + (contaPagarReceber.ValorTotal - contaPagarReceber.ValorParcela + contaPagarReceber.Desconto).ToString("#,###,##0.00");
                contasPagarReceberRelatorio.Valor = "R$ " + contaPagarReceber.ValorParcela.ToString("#,###,##0.00");
                contasPagarReceberRelatorio.ValorTotal = "R$ " + contaPagarReceber.ValorTotal.ToString("#,###,##0.00");
                contasPagarReceberRelatorio.ValorPago = "R$ " + contaPagarReceber.ValorPago.ToString("#,###,##0.00");

                listaContasPagarReceberRelatorio.Add(contasPagarReceberRelatorio);
                ConteudoRelatorio.DataSource = listaContasPagarReceberRelatorio;

                txtTotalRegistros.Text = listaContasPagarReceber.Count.ToString() ;
                txtTotalParcial.Text = listaContasPagarReceber.Sum(x => x.ValorParcela).ToString("R$ #,###,##0.00");
                txtTotal.Text = listaContasPagarReceber.Sum(x => x.ValorTotal).ToString("R$ #,###,##0.00");
                registro = true;
            }

            if (registro == false)
            {

                ServicoItemMovimentacaoCaixa servicoContasItensCaixa = new ServicoItemMovimentacaoCaixa();

                var listaContasCaixa = servicoContasItensCaixa.ConsulteListaPorCategoriasEPagamentos(_categoriaId.ToInt(), 1, _dataInicialPeriodo, _dataFinalPeriodo);

                List<ContasPagarReceberRelatorio> listaContasPagarReceberRelatorioCaixa = new List<ContasPagarReceberRelatorio>();

                foreach (var ContasCaixa in listaContasCaixa)
                {
                   ContasPagarReceberRelatorio contasPagarReceberRelatorio = new ContasPagarReceberRelatorio();

                    contasPagarReceberRelatorio.Id = ContasCaixa.Id;

                    contasPagarReceberRelatorio.CodigoParceiro = ContasCaixa.MovimentacaoCaixa.UsuarioAbertura.Id.ToString();
                    //contasPagarReceberRelatorio.NomeParceiro = ContasCaixa.MovimentacaoCaixa.DadosGerais.Razao;

                    contasPagarReceberRelatorio.NumeroDocumento = ContasCaixa.MovimentacaoCaixa.Id.ToString();

                    contasPagarReceberRelatorio.Categoria = ContasCaixa.CategoriaFinaceira != null ? ContasCaixa.CategoriaFinaceira.Descricao : string.Empty;

                    contasPagarReceberRelatorio.Historico = ContasCaixa.HistoricoMovimentacoes;

                    contasPagarReceberRelatorio.Situacao = "PAGO";

                    //int diasAtraso = RetorneDiasDeAtraso(ContasCaixa);

                    //contasPagarReceberRelatorio.Atraso = diasAtraso + " dias";
                    contasPagarReceberRelatorio.DataVencimento = ContasCaixa.DataHora != null ? ContasCaixa.DataHora.ToString("dd/MM/yyyy") : string.Empty;
                    contasPagarReceberRelatorio.DataPagamento = ContasCaixa.MovimentacaoCaixa.DataHoraFechamento != null ? ContasCaixa.MovimentacaoCaixa.DataHoraFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;

                    contasPagarReceberRelatorio.DataEmissao = ContasCaixa.DataHora != null ? ContasCaixa.DataHora.ToString("dd/MM/yyyy") : string.Empty;

                    contasPagarReceberRelatorio.Desconto = "R$ " + "0,00";
                    contasPagarReceberRelatorio.MultaJuros = "R$ " + ContasCaixa.Valor.ToString("#,###,##0.00");
                    contasPagarReceberRelatorio.Valor = "R$ " + ContasCaixa.Valor.ToString("#,###,##0.00");
                    contasPagarReceberRelatorio.ValorTotal = "R$ " + ContasCaixa.Valor.ToString("#,###,##0.00");
                    contasPagarReceberRelatorio.ValorPago = "R$ " + ContasCaixa.Valor.ToString("#,###,##0.00");

                    listaContasPagarReceberRelatorio.Add(contasPagarReceberRelatorio);

                }


                ConteudoRelatorio.DataSource = listaContasPagarReceberRelatorio;
            
                txtTotalRegistros.Text =  listaContasCaixa.Count.ToString();
                txtTotalParcial.Text = listaContasCaixa.Sum(x => x.Valor).ToString("R$ #,###,##0.00");
                txtTotal.Text = listaContasCaixa.Sum(x => x.Valor).ToString("R$ #,###,##0.00");
            }
        }
        

            #endregion

       

            #region " MÉTODOS AUXILIARES "

            private int RetorneDiasDeAtraso(ContaPagarReceber contaPagarReceber)
        {
            if (contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO)
            {
                return 0;
            }

            DateTime dataPagamento = DateTime.Now;

            if (contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO)
            {
                dataPagamento = contaPagarReceber.DataPagamento.Value;
            }

            var diferenca = dataPagamento - contaPagarReceber.DataVencimento.Value;

            return diferenca.TotalDays < 0 ? 0 : diferenca.TotalDays.ToInt();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class ContasPagarReceberRelatorio
        {
            public int Id { get; set; }

            public string CodigoParceiro { get; set; }

            public string NomeParceiro { get; set; }

            public string NumeroDocumento { get; set; }

            public string Categoria { get; set; }

            public string Historico { get; set; }

            public string DataEmissao { get; set; }

            public string DataVencimento { get; set; }

            public string DataPagamento { get; set; }

            public string Valor { get; set; }

            public string MultaJuros { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }

            public string ValorPago { get; set; }

            public string Atraso { get; set; }

            public string Situacao { get; set; }
        }

        #endregion
    }
   
}
