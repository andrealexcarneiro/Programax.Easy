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
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.PessoaServ;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioMovimentacaoBanco : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private MovimentacaoBanco _movimentacaoBanco;
        private List<string> _bancos;
        private DateTime? _dataInicial;
        private DateTime? _dataFinal;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioMovimentacaoBanco(MovimentacaoBanco MovimentacaoBanco, List<string> Bancos, DateTime? dataInicial, DateTime? dataFinal)
        {
            InitializeComponent();

            _tituloRelatorio = "MOVIMENTO DE BANCO";
            _movimentacaoBanco = MovimentacaoBanco;

            _bancos = Bancos;
            _dataInicial = dataInicial;
            _dataFinal = dataFinal;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            //ServicoMovimentacaoBanco servicoMovimentacaoCaixa = new ServicoMovimentacaoBanco();
            var movimentacaoBanco = _movimentacaoBanco;

            MovimentacaoBancoRelatorio movimentacaoBancoRelatorio = new MovimentacaoBancoRelatorio();

            PreenchaInformacoesMovimentacaoEBanco(movimentacaoBanco, movimentacaoBancoRelatorio);
            PreenchaTotaisMovimentacao(movimentacaoBanco, movimentacaoBancoRelatorio);
            PreenchaListaItens(movimentacaoBanco, movimentacaoBancoRelatorio);

            PreenchaDataSource(movimentacaoBancoRelatorio);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void PreenchaInformacoesMovimentacaoEBanco(MovimentacaoBanco movimentacaoBanco, MovimentacaoBancoRelatorio movimentacaoBancoRelatorio)
        {
            movimentacaoBancoRelatorio.DataMovimentacao = movimentacaoBanco.DataHoraAbertura.GetValueOrDefault().ToString("dd/MM/yyyy");
            movimentacaoBancoRelatorio.NomeBanco = movimentacaoBanco.Banco.Id + " - " + movimentacaoBanco.Banco.NomeBanco;           
        }

        private void PreenchaTotaisMovimentacao(MovimentacaoBanco movimentacaoBanco, MovimentacaoBancoRelatorio movimentacaoBancoRelatorio)
        {
            double entradaDinheiro = 0;
            double saidaDinheiro = 0;
            double saldoDinheiro = 0;

            foreach (var item in movimentacaoBanco.ListaItensBanco)
            {
                if (item.EstahEstornado)
                {
                    continue;
                }

                if (item.TipoMovimentacao == EnumTipoMovimentacaoBanco.ENTRADA)
                {
                    entradaDinheiro += item.Valor;
                    saldoDinheiro += item.Valor;
                }
                else
                {
                    saidaDinheiro += item.Valor;
                    saldoDinheiro -= item.Valor;
                }
            }
            
            //movimentacaoBancoRelatorio.SaldoInicialDinheiro = movimentacaoBanco.SaldoInicial.ToDouble() == 0 ? movimentacaoBanco.SaldoInicial.ToString("#,###,##0.00") : movimentacaoBanco.SaldoInicial.ToString("#,###,##0.00");

            movimentacaoBancoRelatorio.EntradaDinheiro = entradaDinheiro.ToString("#,###,##0.00");

            movimentacaoBancoRelatorio.SaidaDinheiro = saidaDinheiro.ToString("#,###,##0.00");

            movimentacaoBancoRelatorio.SaldoFinalDinheiro = saldoDinheiro.ToString("#,###,##0.00");

            if (entradaDinheiro == 0 && movimentacaoBanco.SaldoInicial.ToDouble() != 0)
            {
                movimentacaoBancoRelatorio.SaldoFinalDinheiro = (movimentacaoBanco.SaldoInicial.ToDouble() - saidaDinheiro).ToString("#,###,##0.00");
            }
        }
          
        private void PreenchaListaItens(MovimentacaoBanco movimentacaoBanco, MovimentacaoBancoRelatorio movimentacaoBancoRelatorio)
        {
            double saldo = 0;

            foreach (var item in movimentacaoBanco.ListaItensBanco)
            {
                ItemMovimentacaoBancoRelatorio itemMovimentacaoBancoRelatorio = new ItemMovimentacaoBancoRelatorio();

                //item.Parceiro = new ServicoPessoa().Consulte(item.Parceiro.Id);

                if (item.Parceiro != null)
                {
                    itemMovimentacaoBancoRelatorio.CodigoParceiro = item.Parceiro.Id.ToString();
                    itemMovimentacaoBancoRelatorio.NomeParceiro = item.Parceiro.DadosGerais.NomeFantasia;
                }

                itemMovimentacaoBancoRelatorio.DataHora = item.DataHoraLancamento.ToString("dd/MM/yyyy HH:mm");
                itemMovimentacaoBancoRelatorio.Categoria = item.Categoria != null? item.Categoria.Descricao:string.Empty;
                itemMovimentacaoBancoRelatorio.DescricaoMovimentacao = item.EstahEstornado ? "(ESTORNO) " + item.DescricaoDaMovimentacao : item.DescricaoDaMovimentacao;
                itemMovimentacaoBancoRelatorio.Origem = item.OrigemMovimentacaoBanco.Descricao();
                itemMovimentacaoBancoRelatorio.NumeroDocumento = item.NumeroDocumentoOrigem;

                if (item.TipoMovimentacao == EnumTipoMovimentacaoBanco.ENTRADA)
                {
                    itemMovimentacaoBancoRelatorio.Entrada = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo += item.Valor;
                    }
                }
                else
                {
                    itemMovimentacaoBancoRelatorio.Saida = item.Valor.ToString("#,###,##0.00");

                    if (!item.EstahEstornado)
                    {
                        saldo -= item.Valor;
                    }
                }

                itemMovimentacaoBancoRelatorio.EstahEstornado = item.EstahEstornado;

                itemMovimentacaoBancoRelatorio.Saldo = saldo.ToString("#,###,##0.00");

                movimentacaoBancoRelatorio.ListaItensMovimentacaoBanco.Add(itemMovimentacaoBancoRelatorio);
            }
        }

        private void PreencheDadosDePesquisa()
        {
            if (_bancos.Count > 0)
            {
                lblBancos_1.Visible = false;
                lblBancos_2.Visible = true;
                foreach (var item in _bancos)
                {  
                    lblBancos_2.Text = lblBancos_2.Text + item + "/ ";
                }
            }

            if(_dataFinal != null && _dataInicial != null)
            {
                lblDatasIntervalos.Visible = true;
                lblIntervaloPesquisa.Visible = true;
                lblDatasIntervalos.Text = _dataInicial.Value.ToString("dd/MM/yyyy") + " a " + _dataFinal.Value.ToString("dd/MM/yyyy");
            }
        }

        private void PreenchaDataSource(MovimentacaoBancoRelatorio movimentacaoBancoRelatorio)
        {
            List<MovimentacaoBancoRelatorio> listaMovimentacoes = new List<MovimentacaoBancoRelatorio>();
            listaMovimentacoes.Add(movimentacaoBancoRelatorio);

            PreencheDadosDePesquisa();

            ConteudoRelatorio.DataSource = listaMovimentacoes;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class MovimentacaoBancoRelatorio
        {
            public MovimentacaoBancoRelatorio()
            {
                ListaItensMovimentacaoBanco = new List<ItemMovimentacaoBancoRelatorio>();
            }

            public string NomeBanco { get; set; }

            public string DataMovimentacao { get; set; }

            public string SaldoInicialDinheiro { get; set; }
            
            public string EntradaDinheiro { get; set; }

            public string SaidaDinheiro { get; set; }
                        
            public string SaldoFinalDinheiro { get; set; }
                        
            public List<ItemMovimentacaoBancoRelatorio> ListaItensMovimentacaoBanco { get; set; }
        }

        public class ItemMovimentacaoBancoRelatorio
        {
            public string DataHora { get; set; }

            public string CodigoParceiro { get; set; }

            public string NomeParceiro { get; set; }

            public string DescricaoMovimentacao { get; set; }

            public string Categoria { get; set; }

            public string Origem { get; set; }

            public string NumeroDocumento { get; set; }

            public string Entrada { get; set; }

            public string Saida { get; set; }

            public string Saldo { get; set; }

            public bool EstahEstornado { get; set; }
        }

        #endregion
    }
}
