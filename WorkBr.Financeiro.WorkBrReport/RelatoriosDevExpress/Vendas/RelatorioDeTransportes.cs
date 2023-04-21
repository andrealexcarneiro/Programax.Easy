using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
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
using DevExpress.XtraPrinting.Native;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioDeTransportes : RelatorioBaseDev
    {
        #region " VARIÁVEIS PRIVADAS "

        private EnumPesquisaPorFuncao _pesquisaPorFuncao;
        private Pessoa _parceiroPesquisa;
        private bool _periodoFaturamento;
        private DateTime _dataInicialPeriodo;
        private DateTime _dataFinalPeriodo;
        private EnumOrdenacaoPesquisaVwVendas _ordenacao;
        private bool _statusAberto;
        private bool _statusOrcamento;
        private bool _statusCancelado;
        private bool _statusEmLiberacao;
        private bool _statusRecusado;
        private bool _statusReservado;
        private bool _statusFaturado;
        private bool _statusEmitidoNFe;
        private bool _pesquisaPorPeriodo;
        private string _nomeColaborador;
        private Marca _marca;
        private Fabricante _fabricante;
        private SubGrupo _subgrupo;
        private Categoria _categoria;
        private Grupo _grupo;
        private List<RelatorioDeTransportes.ItemPedidoRelatorio> _listaPedidos;
        private DateTime _dataEntrega;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioDeTransportes(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                         Pessoa parceiroPesquisa,
                                         bool periodoFaturamento,
                                         DateTime dataInicialPeriodo,
                                         DateTime dataFinalPeriodo,
                                         bool statusAberto,
                                         bool statusOrcamento,
                                         bool statusCancelado,
                                         bool statusEmLiberacao,
                                         bool statusRecusado,
                                         bool statusReservado,
                                         bool statusFaturado,
                                         bool statusEmitidoNFe,
                                         bool pesquisaPorPeriodo,
                                         EnumOrdenacaoPesquisaVwVendas ordenacao,
                                         Marca marca, Fabricante fabricante,
                                         SubGrupo subgrupo, Categoria categoria,
                                         Grupo grupo, List<RelatorioDeTransportes.ItemPedidoRelatorio>listaPedidos)
        {
            InitializeComponent();

            _pesquisaPorFuncao = pesquisaPorFuncao;
            _parceiroPesquisa = parceiroPesquisa;
            _periodoFaturamento = periodoFaturamento;
            _dataInicialPeriodo = dataInicialPeriodo;
            _dataFinalPeriodo = dataFinalPeriodo;

            _statusAberto = statusAberto;
            _statusOrcamento = statusOrcamento;
            _statusCancelado = statusCancelado;
            _statusEmLiberacao = statusEmLiberacao;
            _statusRecusado = statusRecusado;
            _statusReservado = statusReservado;
            _statusFaturado = statusFaturado;
            _statusEmitidoNFe = statusEmitidoNFe;
            _pesquisaPorPeriodo = pesquisaPorPeriodo;
            _marca = marca;
            _fabricante = fabricante;
            _subgrupo = subgrupo;
            _categoria = categoria;
            _grupo = grupo;
            _listaPedidos = listaPedidos;

            _ordenacao = ordenacao;

            if (_periodoFaturamento)
            {
                lblPeriodo.Text = "Período Faturamento:";
            }
            else
            {
                lblPeriodo.Text = "Período Emissão:";
            }

            lblDataPeriodo.Text = dataInicialPeriodo.ToString("dd/MM/yyyy") + "  à " + dataFinalPeriodo.ToString("dd/MM/yyyy");

            _tituloRelatorio = "RELATÓRIO DE TRANSPORTES";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            if (_pesquisaPorPeriodo)
                CarregueDadosRelatorioPorGrupo();
            else
                CarregueDadosRelatorioPorPedido();
        }

        #endregion

        # region "Outros métodos"

        private void CarregueDadosRelatorioPorGrupo()
        {           
            bool temPedido;
            
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            List<VWVendaTransportes> listaVWVenda = servicoPedidoDeVenda.ConsulteListaVWVendasTransportes(_pesquisaPorFuncao,
                                                                                                                               _parceiroPesquisa,
                                                                                                                               _periodoFaturamento,
                                                                                                                               _dataInicialPeriodo,
                                                                                                                               _dataFinalPeriodo,
                                                                                                                               _statusAberto,
                                                                                                                               _statusOrcamento,
                                                                                                                               _statusCancelado,
                                                                                                                               _statusEmLiberacao,
                                                                                                                               _statusRecusado,
                                                                                                                               _statusReservado,
                                                                                                                               _statusFaturado,
                                                                                                                               _statusEmitidoNFe,
                                                                                                                               _ordenacao);

            List<GrupoDataEntrega> listaVendasRelatorio = new List<GrupoDataEntrega>();
            
            GrupoDataEntrega grupoDataEntregaitem = new GrupoDataEntrega();
            if (listaVWVenda.Count == 0)
                return;

            _dataEntrega = listaVWVenda.FirstOrDefault().DataEntrega;
            int totalRegistros = 0;

            foreach (var venda in listaVWVenda)
            {                
                PedidosClientes pedidosClientesitem = new PedidosClientes();
                
                temPedido = false;

                if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    _nomeColaborador = venda.IndicadorNome;                    
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    _nomeColaborador = venda.AtendenteNome;                    
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    _nomeColaborador = venda.VendedorNome;                    
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    _nomeColaborador = venda.SupervisorNome;                    
                }

                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(venda.Id);

                if (venda.DataEntrega != _dataEntrega)
                {
                    grupoDataEntregaitem.DataDaEntrega = _dataEntrega.ToString("dd/MM/yyyy");                                        
                    _dataEntrega = venda.DataEntrega;
                    
                    //Agrupa os itens repetidos e os ordena por código
                    grupoDataEntregaitem.itensRel = AgrupaItens(grupoDataEntregaitem.itensRel).ToList();

                    listaVendasRelatorio.Add(grupoDataEntregaitem);
                    grupoDataEntregaitem = new GrupoDataEntrega();                                        
                }
                
                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    itensRel item = new itensRel();
                    
                    ServicoProduto servicoProduto = new ServicoProduto();

                    var listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(itens.Produto.DadosGerais.Descricao,
                                                                      _marca, _fabricante, _categoria,
                                                                       _grupo, _subgrupo);
                    
                    if (listaProdutos.Count > 0)
                    {                           
                        item.CodigoDoItem = itens.Produto.Id;
                        item.Descricao = itens.Produto.DadosGerais.Descricao;
                        item.Quantidade = itens.Quantidade;
                        item.Peso = itens.Produto.Principal.PesoBruto.ToDouble() * itens.Quantidade;                                    
                        temPedido = true;

                        grupoDataEntregaitem.itensRel.Add(item);                      
                       
                    }

                }

                if (temPedido)
                {
                    pedidosClientesitem.NumeroDoPedido = venda.Id.ToString();

                    pedidosClientesitem.ClienteId = venda.ClienteId.ToString();
                    pedidosClientesitem.NomeCliente = venda.ClienteNome;
                    pedidosClientesitem.DataEntrega = venda.DataEntrega;
                    pedidosClientesitem.NomeColaborador = _nomeColaborador;
                    
                    _nomeColaborador = "";
                    totalRegistros += 1; 
                    grupoDataEntregaitem.pedidosClientes.Add(pedidosClientesitem);                    
                }                
                
            }
            
            if(listaVendasRelatorio.Count < totalRegistros)
            {
                //Agrupa os itens repetidos e os ordena por código
                grupoDataEntregaitem.itensRel = AgrupaItens(grupoDataEntregaitem.itensRel).ToList();

                grupoDataEntregaitem.DataDaEntrega = _dataEntrega.ToString("dd/MM/yyyy");                
                listaVendasRelatorio.Add(grupoDataEntregaitem);
            }            

            ConteudoRelatorio.DataSource = listaVendasRelatorio;
            
            if (_marca != null)
            {
                txtGruposBuscados.Visible = true;
                txtMarca.Visible = true;
                txtMarca.Text = _marca.Descricao;
            }

            if (_fabricante != null)
            {
                txtGruposBuscados.Visible = true;
                txtFabricante.Visible = true;
                txtFabricante.Text = _fabricante.Descricao;
            }

            if (_categoria != null)
            {
                txtGruposBuscados.Visible = true;
                txtCategoria.Visible = true;
                txtCategoria.Text = _categoria.Descricao;
            }

            if (_grupo != null)
            {
                txtGruposBuscados.Visible = true;
                txtGrupo.Visible = true;
                txtGrupo.Text = _grupo.Descricao;
            }

            if (_subgrupo != null)
            {
                txtGruposBuscados.Visible = true;
                txtSubgrupo.Visible = true;
                txtSubgrupo.Text = _subgrupo.Descricao;
            }
        }

        private void CarregueDadosRelatorioPorPedido()
        {
            bool temPedido;

            if (_listaPedidos.Count == 0)
                return;

            int totalRegistros = 0;

            List<GrupoDataEntrega> listaVendasRelatorio = new List<GrupoDataEntrega>();

            GrupoDataEntrega grupoDataEntregaitem = new GrupoDataEntrega();


            foreach (var pedido in _listaPedidos)
            {
                PedidosClientes pedidosClientesitem = new PedidosClientes();

                temPedido = false;

                if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    _nomeColaborador = pedido.IndicadorNome;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    _nomeColaborador = pedido.AtendenteNome;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    _nomeColaborador = pedido.VendedorNome;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    _nomeColaborador = pedido.SupervisorNome;
                }

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(pedido.NumeroDoPedido);

                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    itensRel item = new itensRel();

                    ServicoProduto servicoProduto = new ServicoProduto();

                    var listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(itens.Produto.DadosGerais.Descricao,
                                                                      _marca, _fabricante, _categoria,
                                                                       _grupo, _subgrupo);

                    if (listaProdutos.Count > 0)
                    {
                        item.CodigoDoItem = itens.Produto.Id;
                        item.Descricao = itens.Produto.DadosGerais.Descricao;
                        item.Quantidade = itens.Quantidade;
                        item.Peso = itens.Produto.Principal.PesoBruto.ToDouble() * itens.Quantidade;
                        temPedido = true;

                        grupoDataEntregaitem.itensRel.Add(item);

                    }

                }

                if (temPedido)
                {
                    pedidosClientesitem.NumeroDoPedido = pedido.NumeroDoPedido.ToString();

                    pedidosClientesitem.ClienteId = pedido.CodigoDoCliente.ToString();
                    pedidosClientesitem.NomeCliente = pedido.Cliente;
                    pedidosClientesitem.NomeColaborador = _nomeColaborador;
                    _nomeColaborador = "";
                    totalRegistros += 1;
                    grupoDataEntregaitem.pedidosClientes.Add(pedidosClientesitem);
                }

            }
            //Data da Entrega Fixa
            lblDataDaEntrega.Visible = true;
            lblDataDaEntrega.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //Tornar invisível, pois não são necessários para esta pesquisa
            lblPeriodo.Visible = false;
            lblDataPeriodo.Visible = false;
            lnhTotalGeralPorPeriodo.Visible = false;
            lblTotalGeralPorPeriodo.Visible = false;
            txtPesoTotalPeriodo.Visible = false;
            txtQuantidadeTotalPeriodo.Visible = false;

            //Agrupa os itens repetidos e os ordena por código
            grupoDataEntregaitem.itensRel = AgrupaItens(grupoDataEntregaitem.itensRel).ToList();

            listaVendasRelatorio.Add(grupoDataEntregaitem);
            

            ConteudoRelatorio.DataSource = listaVendasRelatorio;
        }
        
        public List<itensRel> AgrupaItens (List<itensRel> objetoAAgrupar) 
        {
            //Primeiramente ordena o vetor de itens por código do item
            objetoAAgrupar = objetoAAgrupar.OrderBy(Cod => Cod.CodigoDoItem).ToList();

            //Agrupa por código e soma peso e quantidade
            var listaItensAgrupado = (
                         from l in objetoAAgrupar
                         group l by l.CodigoDoItem
                         into g
                         select new
                         { CodigoDoItem = g.Key, Descricao = g.Select(d => d.Descricao).FirstOrDefault(), Peso = g.Sum(x => x.Peso), Quantidade = g.Sum(x => x.Quantidade) }).ToList();
                        
            //Limpa a lista de itens
            objetoAAgrupar = new List<itensRel>();

            
            foreach (var item in listaItensAgrupado)
            {
                itensRel itemLista = new itensRel();

                itemLista.CodigoDoItem = item.CodigoDoItem;
                itemLista.Descricao = item.Descricao;
                itemLista.Peso = item.Peso;
                itemLista.Quantidade = item.Quantidade;

                objetoAAgrupar.Add(itemLista);
            }     
            
            return objetoAAgrupar;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class itensRel
        {
            public int    CodigoDoItem { get; set; }
            public string Descricao { get; set; }            
            public double Peso { get; set; }
            public double Quantidade { get; set; }
        }

        public class PedidosClientes
        {
            public string NumeroDoPedido { get; set; }

            public string ClienteId { get; set; }

            public string NomeCliente { get; set; }

            public string NomeColaborador { get; set; }

            public DateTime DataEntrega { get; set; }

        }

        public class GrupoDataEntrega
        {
            public GrupoDataEntrega()
                {
                    itensRel = new List<itensRel>();
                    pedidosClientes = new List<PedidosClientes>();
                }

            public List<itensRel> itensRel { get; set; }
            public List<PedidosClientes> pedidosClientes { get; set; }
            
            public string DataDaEntrega { get; set; }            
        }

        public class ItemPedidoRelatorio
        {
            public string Item { get; set; }
            public int NumeroDoPedido { get; set; }
            public string Cliente { get; set; }
            public int    CodigoDoCliente { get; set; }
            public string IndicadorNome { get; set; }
            public string AtendenteNome { get; set; }
            public string VendedorNome { get; set; }
            public string SupervisorNome { get; set; }            
        }
        
        #endregion
    }
}
