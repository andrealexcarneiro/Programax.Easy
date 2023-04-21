using System;
using System.Linq;
using System.Collections.Generic;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioCustoFinanceirotodos : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private EnumPesquisaPorFuncao _pesquisaPorFuncao;
        private List<Pessoa> _parceiroPesquisa;
        private List<Pessoa> _parceiroPesquisaII;
        private bool _periodoFaturamento;
        private DateTime _dataInicialPeriodo;
        private DateTime _dataFinalPeriodo;
        private EnumOrdenacaoPesquisaVwVendas _ordenacao;
        private double _custo;
      

      


        #endregion

        #region " CONSTRUTOR "

        public RelatorioCustoFinanceirotodos(List<Pessoa> parceiroPesquisa,
                                         List<Pessoa> parceiroPesquisaII,
                                         DateTime dataInicialPeriodo,
                                         DateTime dataFinalPeriodo, double Custo

                                          )
        {
            InitializeComponent();


            _parceiroPesquisa = parceiroPesquisa;
            _parceiroPesquisaII = parceiroPesquisaII;
            _dataInicialPeriodo = dataInicialPeriodo;
            _dataFinalPeriodo = dataFinalPeriodo;
            _custo = Custo;

            if (_periodoFaturamento)
            {
                lblPeriodo.Text = "Período Faturamento:";
            }
            else
            {
                lblPeriodo.Text = "Período Emissão:";
            }

            lblDataPeriodo.Text = dataInicialPeriodo.ToString("dd/MM/yyyy") + "  à " + dataFinalPeriodo.ToString("dd/MM/yyyy");

            _tituloRelatorio = "RELATÓRIO DE CUSTO FINANCEIRO DE VENDAS POR VENDEDOR";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
        
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            List<CustoFinanceiro> listaCusto = servicoPedidoDeVenda.ConsulteListaCustoFinanceiro(_parceiroPesquisa, _dataInicialPeriodo,
                                                                                                                               _dataFinalPeriodo);
            VendasRelatorio vendaRelatorio = new VendasRelatorio();
            string DataIni = _dataInicialPeriodo.ToString("dd/MM/yyyy");
            string DataFim = _dataFinalPeriodo.ToString("dd/MM/yyyy");
            double _Percentual = 0;
            double _TotalValor = 0;
            double _TotalCusto = 0;
            double _TotalPercentual = 0;
           

            lblDataPeriodo.Text = DataIni + " à " + DataFim;

            List<VendasRelatorio> listaVendasRelatorio = new List<VendasRelatorio>();

            foreach (var itens in listaCusto)
            {
                itensRel item = new itensRel();
               
                item.Id = itens.VendedorId.ToInt();
                item.Nome = itens.VendedorNome;
                item.Data = itens.DataVenda.ToString("dd/MM/yyyy");
                item.Custo = _custo.ToString("#,###,##0.00");
                if (_custo.ToDouble() > itens.ValorVendido.ToDouble())
                {
                    _Percentual = 0;
                }
                else
                {
                    _Percentual = _custo.ToDouble() * 100 / itens.ValorVendido.ToDouble();
                }
                _TotalValor += itens.ValorVendido.ToDouble();
                _TotalCusto += _custo.ToDouble();
                _TotalPercentual += _Percentual.ToDouble();

                item.PCusto = _Percentual.ToString("#,###,##0.0");
                item.Valor = itens.ValorVendido.ToString("#,###,##0.00");
                

                vendaRelatorio.itensRel.Add(item);
             //   listaVendasRelatorio = new List<VendasRelatorio>();
               
               
            }
            listaVendasRelatorio.Add(vendaRelatorio);
            ConteudoRelatorio.DataSource = listaVendasRelatorio;

            lblTotalCusto.Text = _TotalCusto.ToString("#,###,##0.00");
            lblTotalValor.Text = _TotalValor.ToString("#,###,##0.00");
            lblTotalPercentual.Text = _TotalPercentual.ToString("#,###,##0.0");
            
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class VendasRelatorio
        {
            public VendasRelatorio()
            {
                itensRel = new List<itensRel>();
            }

            public string Id { get; set; }

            public int ClienteId { get; set; }

            public string NomeCliente { get; set; }

            public string NomeColaborador { get; set; }

            public string TipoPedido { get; set; }

            public string Situacao { get; set; }

            public string DataEmissao { get; set; }

            public string DataFaturamento { get; set; }

            public double ValorPedido { get; set; }

            public double Comissao { get; set; }

            public List<itensRel> itensRel { get; set; }
            public List<VendasRelatorio> vendasRelatorios { get; set; }
        }

        public class itensRel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Data { get; set; }
            public string Valor { get; set; }
            public string Custo { get; set; }
            public string PCusto { get; set; }

        }

        #endregion
    }
}

