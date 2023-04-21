using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioCondicaoPagamento : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "
        
        private DateTime _dataInicialPeriodo;
        private DateTime _dataFinalPeriodo;
        
        #endregion

        #region " CONSTRUTOR "

        public RelatorioCondicaoPagamento(DateTime dataInicialPeriodo, DateTime dataFinalPeriodo)
        {
            InitializeComponent();
            
            _dataInicialPeriodo = dataInicialPeriodo;
            _dataFinalPeriodo = dataFinalPeriodo;            
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            var listaDePedidos = servicoPedidoDeVenda.ConsulteListaVWVendas(Negocio.Vendas.Enumeradores.EnumPesquisaPorFuncao.VENDEDOR, new List<Negocio.Cadastros.PessoaObj.ObjetoDeNegocio.Pessoa>(),
                                                                                    new List<Negocio.Cadastros.PessoaObj.ObjetoDeNegocio.Pessoa>(),
                                                                                    false,
                                                                                    _dataInicialPeriodo,
                                                                                    _dataFinalPeriodo,
                                                                                    false,
                                                                                    false,
                                                                                    false,
                                                                                    false,
                                                                                    false,
                                                                                    false,
                                                                                    true,
                                                                                    true,
                                                                                    false,
                                                                                    false,
                                                                                    Negocio.Vendas.Enumeradores.EnumOrdenacaoPesquisaVwVendas.CODIGO);


            //var listaDePedidos = servicoPedidoDeVenda.ConsulteLista(_dataInicialPeriodo, _dataFinalPeriodo, null, null, null, Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.PEDIDOVENDA,null);

            IEnumerable<IGrouping<string, VWVenda>> query = from pedidos in listaDePedidos group pedidos 
                                                         by pedidos.CondicaoPagamentoNome;
            //where (pedidos.StatusPedidoVenda == Negocio.Vendas.Enumeradores.EnumStatusPedidoDeVenda.FATURADO ||
            //       pedidos.StatusPedidoVenda == Negocio.Vendas.Enumeradores.EnumStatusPedidoDeVenda.EMITIDONFE) 
            //CondicaoPagamento.Id;

            List<CondicaoDePagamentoRelatorio> listaCondicaoPagtoRelatorio = new List<CondicaoDePagamentoRelatorio>();

            int cont = 1;

            foreach (var item in query)
            {
                CondicaoDePagamentoRelatorio ItemcondicaoPagamentoRelatorio = new CondicaoDePagamentoRelatorio();
                
                ItemcondicaoPagamentoRelatorio.Id = cont++;

                ItemcondicaoPagamentoRelatorio.CondicaoDePagamentoDescrição = item.Key;//listaDePedidos.FirstOrDefault(x=>x.CondicaoPagamento.Id == item.Key).CondicaoPagamento.Descricao;

                ItemcondicaoPagamentoRelatorio.ValorTotal = item.Sum(x => x.ValorTotal).ToString("0.00");

                ItemcondicaoPagamentoRelatorio.ContarCondicaoPagamento = item.Count().ToString();

                listaCondicaoPagtoRelatorio.Add(ItemcondicaoPagamentoRelatorio);
            }

            ConteudoRelatorio.DataSource = listaCondicaoPagtoRelatorio;

            txtTotalRegistros.Text = listaCondicaoPagtoRelatorio.Count.ToString();           
            txtTotal.Text = listaCondicaoPagtoRelatorio.Sum(x => x.ValorTotal.ToDouble()).ToString("R$ #,###,##0.00");
            
            txtPeriodo.Text = _dataInicialPeriodo.ToString("dd/MM/yyyy") + " à " + _dataFinalPeriodo.ToString("dd/MM/yyyy");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "
                

        #endregion

        #region " CLASSES AUXILIARES "

        public class CondicaoDePagamentoRelatorio
        {
            public int Id { get; set; }

            public string CondicaoDePagamentoDescrição { get; set; }

            public string ContarCondicaoPagamento { get; set; }
           
            public string ValorTotal { get; set; }
        }

        #endregion
    }
}
