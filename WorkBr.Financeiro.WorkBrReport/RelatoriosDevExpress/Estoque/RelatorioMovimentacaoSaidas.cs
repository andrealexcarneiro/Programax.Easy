using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Programax.Easy.Report.RelatoriosDevExpress.Estoque
{
    public partial class RelatorioMovimentacaoSaidas : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private int? _produtoId;
        private DateTime? _dataInicialMovimentacao;
        private DateTime? _dataFinalMovimentacao;
        List<ItensRelatorio> _listaRelatorio = new List<ItensRelatorio>();

        #endregion

        #region " CONSTRUTOR "

        public RelatorioMovimentacaoSaidas(int? produtoId, DateTime? dataInicialMovimentacao, DateTime? dataFinalMovimentacao)
        {
            InitializeComponent();

            _produtoId = produtoId;
            _dataInicialMovimentacao = dataInicialMovimentacao;
            _dataFinalMovimentacao = dataFinalMovimentacao;

            _tituloRelatorio = "Movimentações Saída de Itens Vendidos e Baixados";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            var listaPedidos = new ServicoPedidoDeVenda().ConsulteLista(_dataInicialMovimentacao, _dataFinalMovimentacao, null, null, null, Negocio.Vendas.Enumeradores.EnumTipoPedidoDeVenda.PEDIDOVENDA, Negocio.Vendas.Enumeradores.EnumStatusPedidoDeVenda.FATURADO);

            if (listaPedidos != null)
            {
                foreach (var itemPedido in listaPedidos)
                {
                    ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

                    var listaMovimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(itemPedido.Id);

                    var pedidoAtualizado = new ServicoPedidoDeVenda().ConsulteJoinComItens(itemPedido.Id);

                    foreach (var itemProduto in pedidoAtualizado.ListaItens)
                    {
                        var produtoPesquisa = _produtoId == null ? itemProduto.Produto.Id : _produtoId;

                        ItensRelatorio itensRelatorio = new ItensRelatorio();

                        double quantidadeVendida = pedidoAtualizado.ListaItens.Where(x => x.Produto.Id == produtoPesquisa).Sum(x => x.Quantidade);
                        double quantidadeSaida = listaMovimentacao.Where(x => x.Produto.Id == produtoPesquisa).Sum(x => x.Quantidade);
                        double diferencaQuantidade = quantidadeVendida - quantidadeSaida;

                        itensRelatorio.Data = itemPedido.DataElaboracao.ToString("dd/MM/yyyy HH:mm");
                        itensRelatorio.CodigoItem = itemProduto.Produto.Id.ToString();
                        itensRelatorio.Itens = itemProduto.Produto.DadosGerais.Descricao;
                        itensRelatorio.QuantidadeVendida = quantidadeVendida.ToString("0.0000");
                        itensRelatorio.Saida = quantidadeSaida.ToString("0.0000");
                        itensRelatorio.Diferenca = diferencaQuantidade.ToString("0.0000");
                        itensRelatorio.Estoque = itemProduto.Produto.FormacaoPreco.Estoque.ToString("0.0000");
                        itensRelatorio.NumeroPedido = itemPedido.Id.ToString();

                        if (diferencaQuantidade > 0)
                        {
                            if (_produtoId != null)
                            {
                                if (_produtoId == itemProduto.Produto.Id)
                                    _listaRelatorio.Add(itensRelatorio);
                            }
                            else
                                _listaRelatorio.Add(itensRelatorio);
                        }
                    }
                }
            }
            ConteudoRelatorio.DataSource = _listaRelatorio;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class ItensRelatorio
        {
            public string Data { get; set; }

            public string CodigoItem { get; set; }

            public string Itens { get; set; }
            
            public string QuantidadeVendida { get; set; }

            public string Diferenca { get; set; }

            public string Estoque { get; set; }
            
            public string Saida { get; set; }  
            
            public string NumeroPedido { get; set; }
        }
        #endregion
    }
}
