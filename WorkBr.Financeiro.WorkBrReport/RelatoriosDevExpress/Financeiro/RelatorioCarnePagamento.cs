using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Utils;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Financeiro
{
    public partial class RelatorioCarnePagamento : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _pessoa;
        private EnumDataFiltrarContasPagarReceber? _dataFiltrar;
        private DateTime? _dataInicialPeriodo;
        private DateTime? _dataFinalPeriodo;
        private EnumStatusContaPagarReceber? _statusContaPagarReceber;
        private EnumTipoOperacaoContasPagarReceber _tipoOperacao;
        private EnumOrdenacaoPesquisaContasPagarReceber _ordenacaoPesquisaContasPagarReceber;
        private List<ContaPagarReceber> _listaClientesPromissoria = new List<ContaPagarReceber>();
        private int? _numeroPedido;
       
        #endregion

        #region " CONSTRUTOR "

        public RelatorioCarnePagamento(List<ContaPagarReceber>listaClientesPromissoria, int? NumeroPedido)
        {
            InitializeComponent();
            
            _listaClientesPromissoria = listaClientesPromissoria;

            _numeroPedido = NumeroPedido;

            CarregueDadosEmpresa();
            
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            if (_numeroPedido != null)
                geraCarneApenasUmClienteSelecionadoETodasParcelas();
            else
                geraCarneVariosClientesEParcelasSelecionados();


        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void geraCarneVariosClientesEParcelasSelecionados()
        {
            List<ContasPagarReceberRelatorio> listaContasPagarReceberRelatorio = new List<ContasPagarReceberRelatorio>();

            for (int i = 0; i < _listaClientesPromissoria.Count; i++)
            {
                ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

                //_pessoa = _listaClientesPromissoria[i].Pessoa;

                var contaPagarReceber = servicoContasPagarReceber.Consulte(_listaClientesPromissoria[i].Id);

                ContasPagarReceberRelatorio contasPagarReceberRelatorio = new ContasPagarReceberRelatorio();

                contasPagarReceberRelatorio.NomeParceiro = contaPagarReceber.Pessoa.DadosGerais.Razao;

                contasPagarReceberRelatorio.CnpjCpfParceiro = contaPagarReceber.Pessoa.DadosGerais.CpfCnpj;

                contasPagarReceberRelatorio.TelefoneParceiro = contaPagarReceber.Pessoa.ListaDeTelefones.Count > 0 ? contaPagarReceber.Pessoa.ListaDeTelefones[0].Numero : null;

                var NumeroDoc = _listaClientesPromissoria[i].NumeroDocumento.Split('-');

                contasPagarReceberRelatorio.NumeroDocumento = NumeroDoc.Count() == 2? NumeroDoc[0] : _listaClientesPromissoria[i].NumeroDocumento;

                contasPagarReceberRelatorio.Parcela = NumeroDoc.Count() == 2? NumeroDoc[1] : _listaClientesPromissoria[i].NumeroDocumento;
                
                //**** Demonstrações*****
                if (NumeroDoc.Count() == 2)
                {
                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                    var numeroPedido = NumeroDoc[0].Trim();
                    var pedido = servicoPedido.Consulte(numeroPedido.ToInt());

                    if (pedido != null)
                    {
                        contasPagarReceberRelatorio.TotalValorParcelas = pedido.ValorTotal.ToString("0.00");
                        int cont = 1;

                        foreach (var item in pedido.ListaItens)
                        {
                            contasPagarReceberRelatorio.Demonstracoes += item.Quantidade + " X " + item.Produto.DadosGerais.Descricao;

                            if (pedido.ListaItens.Count > cont)
                            contasPagarReceberRelatorio.Demonstracoes += "*";
                            cont++;
                        }
                    }

                }
                //***Fim Demonstrações

                //****Instruções*****
                ServicoParametros servicoParamentros = new ServicoParametros();

                var parametros = servicoParamentros.ConsulteParametros();

                contasPagarReceberRelatorio.Instrucoes = parametros.ParametrosFinanceiro.ObservacoesCarnePagamento;

                //**Fim Instrucoes

                contasPagarReceberRelatorio.DataVencimento = _listaClientesPromissoria[i].DataVencimento != null ? _listaClientesPromissoria[i].DataVencimento.Value.ToString("dd/MM/yyyy") : string.Empty;

                contasPagarReceberRelatorio.DataEmissao = DateTime.Now.ToString("dd/MM/yyyy");

                contasPagarReceberRelatorio.Valor = "R$ " + _listaClientesPromissoria[i].ValorParcela.ToString("#,###,##0.00");
                
                listaContasPagarReceberRelatorio.Add(contasPagarReceberRelatorio);
                
            }
            ConteudoRelatorio.DataSource = listaContasPagarReceberRelatorio;
        }

        private void geraCarneApenasUmClienteSelecionadoETodasParcelas()
        {
            for (int i = 0; i < _listaClientesPromissoria.Count; i++)
            {
                ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

                _pessoa = _listaClientesPromissoria[i].Pessoa;
                _dataFiltrar = EnumDataFiltrarContasPagarReceber.EMISSAO;
                _dataInicialPeriodo = _listaClientesPromissoria[i].DataEmissao;
                _dataFinalPeriodo = _listaClientesPromissoria[i].DataEmissao;
                _statusContaPagarReceber = EnumStatusContaPagarReceber.ABERTO;
                _tipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;
                _ordenacaoPesquisaContasPagarReceber = EnumOrdenacaoPesquisaContasPagarReceber.NOMEPARCEIRO;


                var listaContasPagarReceber = servicoContasPagarReceber.ConsulteListaFazendoFetchComParceiroEEnderecos(_pessoa,
                                                                                                                       _dataFiltrar,
                                                                                                                       _dataInicialPeriodo,
                                                                                                                        _dataFinalPeriodo,
                                                                                                                        _statusContaPagarReceber,
                                                                                                                       _tipoOperacao,
                                                                                                                       _ordenacaoPesquisaContasPagarReceber);

                List<ContasPagarReceberRelatorio> listaContasPagarReceberRelatorio = new List<ContasPagarReceberRelatorio>();

                int numContas=0;
                foreach (var contaPagarReceber in listaContasPagarReceber)
                {
                    var numeroDoc = contaPagarReceber.NumeroDocumento.Split('-');

                    if (_numeroPedido == numeroDoc[0].Trim().ToInt())
                    {
                        ContasPagarReceberRelatorio contasPagarReceberRelatorio = new ContasPagarReceberRelatorio();

                        contasPagarReceberRelatorio.NomeParceiro = contaPagarReceber.Pessoa.DadosGerais.Razao;

                        contasPagarReceberRelatorio.CnpjCpfParceiro = contaPagarReceber.Pessoa.DadosGerais.CpfCnpj;

                        contasPagarReceberRelatorio.NumeroDocumento = _numeroPedido.ToString();

                        var Parcelas = listaContasPagarReceber[numContas].NumeroDocumento.Split('-');

                        contasPagarReceberRelatorio.Parcela = Parcelas[1].ToString();

                        numContas++;

                        //**** Demonstrações*****
                        ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                        var pedido = servicoPedido.Consulte(_numeroPedido.ToInt());

                        if (pedido != null)
                        {
                            contasPagarReceberRelatorio.TotalValorParcelas = pedido.ValorTotal.ToString("0.00");
                            int cont = 1;

                            foreach (var item in pedido.ListaItens)
                            {
                                contasPagarReceberRelatorio.Demonstracoes += item.Quantidade + " X " + item.Produto.DadosGerais.Descricao;

                                if (pedido.ListaItens.Count > cont)
                                    contasPagarReceberRelatorio.Demonstracoes += "*";
                                cont++;
                            }
                        }

                        //** Fim Demonstracoes

                        //****Instruções*****
                        ServicoParametros servicoParamentros = new ServicoParametros();

                        var parametros = servicoParamentros.ConsulteParametros();

                        contasPagarReceberRelatorio.Instrucoes = parametros.ParametrosFinanceiro.ObservacoesCarnePagamento;

                        //**Fim Instrucoes

                        contasPagarReceberRelatorio.DataVencimento = contaPagarReceber.DataVencimento != null ? contaPagarReceber.DataVencimento.Value.ToString("dd/MM/yyyy") : string.Empty;

                        contasPagarReceberRelatorio.DataEmissao = DateTime.Now.ToString("dd/MM/yyyy");

                        contasPagarReceberRelatorio.Valor = "R$ " + contaPagarReceber.ValorParcela.ToString("#,###,##0.00");

                        listaContasPagarReceberRelatorio.Add(contasPagarReceberRelatorio);
                    }
                }
                ConteudoRelatorio.DataSource = listaContasPagarReceberRelatorio;
            }
        }

        private void CarregueDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            txtNomeEmpresa.Text = empresa.DadosEmpresa.RazaoSocial;

            txtNomeEmpresaSacado.Text = empresa.DadosEmpresa.RazaoSocial;
            
            txtCNPJEmpresa.Text = empresa.DadosEmpresa.Cnpj;           
        }
        
        #endregion

        #region " CLASSES AUXILIARES "

        public class ContasPagarReceberRelatorio
        {           
            public string NomeParceiro { get; set; }

            public string CnpjCpfParceiro { get; set; }

            public string TelefoneParceiro { get; set; }
            
            public string NumeroDocumento { get; set; }

            public string Parcela { get; set; }
            
            public string DataVencimento { get; set; }

            public string DataEmissao { get; set; }
            
            public string Valor { get; set; }

            public string TotalValorParcelas { get; set; }

            public string Instrucoes { get; set; }

            public string Demonstracoes { get; set; }
        }

        #endregion
    }
}
