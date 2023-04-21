using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioVendasChoque : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private EnumPesquisaPorFuncao _pesquisaPorFuncao;
        private List<Pessoa> _parceiroPesquisa;
        private List<Pessoa> _parceiroPesquisaII;
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
        private bool _comissaoPorItem;
        private bool _comissaoCompartilhada;
        private bool _recebidos;
        private string _nomeColaborador;
        private Marca _marca;
        private Fabricante _fabricante;
        private SubGrupo _subgrupo;
        private Categoria _categoria;
        private Grupo _grupo;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioVendasChoque(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                         List<Pessoa> parceiroPesquisa,
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
                                         bool comissaoPorItem,
                                         bool comissaoCompartilhada,
                                         bool recebidos,
                                         EnumOrdenacaoPesquisaVwVendas ordenacao,
                                         Marca marca, Fabricante fabricante,
                                         SubGrupo subgrupo, Categoria categoria,
                                         Grupo grupo)
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
            _comissaoPorItem = comissaoPorItem;
            _comissaoCompartilhada = comissaoCompartilhada;
            _recebidos = recebidos;
            _marca = marca;
            _fabricante = fabricante;
            _subgrupo = subgrupo;
            _categoria = categoria;
            _grupo = grupo;

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

            _tituloRelatorio = "RELATÓRIO DE VENDAS - COMISSÕES";
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            //Vai fazer a busca por grupo e carregar as comissões por item
            if (_marca != null || _fabricante != null || _categoria != null)
            {
                CarregueDadosRelatorioPorGrupo();
                return;
            }

            if(_comissaoPorItem)
            {
                CarregueDadosRelatorioPorItens();
                return;

            }
                double totalComissao = 0;
                double totalComissaoRecebido = 0;
                double totalChoquePorPedido = 0;               
            
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteListaVWVendas(_pesquisaPorFuncao,
                                                                                                                               _parceiroPesquisa,
                                                                                                                               _parceiroPesquisaII,
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
                                                                                                                               false,
                                                                                                                               false,
                                                                                                                               _ordenacao);

            List<VendasRelatorioChoque> listaVendasRelatorio = new List<VendasRelatorioChoque>();

            xrTableCell6.Text = "";            
            
            foreach (var venda in listaVWVenda)
            {
                VendasRelatorioChoque vendaRelatorio = new VendasRelatorioChoque();

                vendaRelatorio.Id = venda.Id.ToString();
                
                vendaRelatorio.ClienteId = venda.ClienteId.ToString();
                vendaRelatorio.NomeCliente = venda.ClienteNome;

                if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    vendaRelatorio.NomeColaborador = venda.IndicadorNome;
                    vendaRelatorio.Comissao = venda.ComissaoIndicador;
                    totalComissao += venda.ComissaoIndicador;                        
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    vendaRelatorio.NomeColaborador = venda.AtendenteNome;
                    vendaRelatorio.Comissao = venda.ComissaoAtendente;
                    totalComissao += venda.ComissaoAtendente;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    vendaRelatorio.NomeColaborador = venda.VendedorNome;                        
                    
                    if (!_comissaoCompartilhada)
                    {
                        vendaRelatorio.Comissao = venda.ComissaoVendedor;
                        totalComissao += venda.ComissaoVendedor;
                    }
                    else
                    {
                        vendaRelatorio.Comissao = venda.ComissaoVendedor - venda.ComissaoAtendente;
                        totalComissao += venda.ComissaoVendedor - venda.ComissaoAtendente;
                    }
                    
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    vendaRelatorio.NomeColaborador = venda.SupervisorNome;
                    vendaRelatorio.Comissao = venda.ComissaoSupervisor;
                    totalComissao += venda.ComissaoSupervisor;
                }

                //***Comissão apenas dos recebidos, entra aqui...
                if (_recebidos)
                {
                    CalculeComissaoApenasVendasRecebidos(venda.Id, venda.ValorTotal, ref totalComissaoRecebido, ref vendaRelatorio);
                }
                //*** Fim comissão recebidos

                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(venda.Id);

                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    vendaRelatorio.ValorPedidoTabela += (itens.ValorUnitario * itens.Quantidade);                    
                }

                vendaRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                vendaRelatorio.Situacao = venda.Status.Descricao();
                vendaRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                vendaRelatorio.ValorPedido = venda.ValorTotal;
                vendaRelatorio.ChoquePorPedido = vendaRelatorio.ValorPedido - vendaRelatorio.ValorPedidoTabela;
                totalChoquePorPedido += vendaRelatorio.ChoquePorPedido;

                listaVendasRelatorio.Add(vendaRelatorio);
            }

            ConteudoRelatorio.DataSource = listaVendasRelatorio;

            txtTotalPedidos.Text = listaVWVenda.Count.ToString();
            txtSomaPedidos.Text = listaVWVenda.Sum(x => x.ValorTotal).ToString("R$ #,###,##0.00");
            txtTotalComissao.Text = !_recebidos? totalComissao.ToString("R$ #,###,##0.00"): totalComissaoRecebido.ToString("R$ #,###,##0.00");
            txtTotalChoquePedido.Text = totalChoquePorPedido.ToString("R$ #,###,##0.00");
        }

        #endregion

        # region "Emitir Relatório Por Grupo"

        private void CarregueDadosRelatorioPorGrupo()
        {
            double totalComissao = 0;
            double totalComissaoRecebido = 0;
            double totalSomaGeral = 0;
            double totalQuantidadeGeral = 0;
            double totalChoquePorItem = 0;            
            double ValorComissaoPedido = 0;            
            bool temPedido;

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteListaVWVendas(_pesquisaPorFuncao,
                                                                                                                               _parceiroPesquisa,
                                                                                                                               _parceiroPesquisaII,
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
                                                                                                                               false,
                                                                                                                               false,
                                                                                                                               _ordenacao);

            List<VendasRelatorioChoque> listaVendasRelatorio = new List<VendasRelatorioChoque>();

            foreach (var venda in listaVWVenda)
            {
                VendasRelatorioChoque vendaRelatorio = new VendasRelatorioChoque();

                temPedido = false;

                if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    _nomeColaborador = venda.IndicadorNome;
                    ValorComissaoPedido = venda.ComissaoIndicador;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    _nomeColaborador = venda.AtendenteNome;
                    ValorComissaoPedido = venda.ComissaoAtendente;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    _nomeColaborador = venda.VendedorNome;

                    if (!_comissaoCompartilhada)
                    {
                        ValorComissaoPedido = venda.ComissaoVendedor;
                    }
                    else
                    {
                        ValorComissaoPedido = venda.ComissaoVendedor - venda.ComissaoAtendente;
                    }
                    
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    _nomeColaborador = venda.SupervisorNome;
                    ValorComissaoPedido = venda.ComissaoSupervisor;
                }

                //***Comissão apenas dos recebidos, entra aqui...
                if (_recebidos)
                {
                    vendaRelatorio.Comissao = ValorComissaoPedido;
                    CalculeComissaoApenasVendasRecebidos(venda.Id, venda.ValorTotal, ref totalComissaoRecebido, ref vendaRelatorio);
                    ValorComissaoPedido = vendaRelatorio.Comissao;
                }
                //*** Fim comissão recebidos

                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(venda.Id);

                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    itensRelChoque item = new itensRelChoque();

                    ServicoProduto servicoProduto = new ServicoProduto();

                    var listaProdutos = servicoProduto.ConsulteListasProdutosAtivos(itens.Produto.DadosGerais.Descricao,
                                                                      _marca, _fabricante, _categoria,
                                                                       _grupo, _subgrupo);
                    if (listaProdutos.Count > 0)
                    {
                        item.Id = itens.Produto.Id;
                        item.Descricao = itens.Produto.DadosGerais.Descricao;
                        item.Quantidade = itens.Quantidade;
                        item.ValorTotalItem = itens.ValorTotal;
                        item.ValorTotalItemTabela = itens.ValorUnitario * itens.Quantidade;
                        item.ChoquePorItem = item.ValorTotalItem - item.ValorTotalItemTabela;

                        if (_comissaoPorItem)
                            item.ComissaoItem = itens.Produto.FormacaoPreco.PercentualComissoesVenda == null || itens.Produto.FormacaoPreco.PercentualComissoesVenda ==0? 
                                                            (ValorComissaoPedido / venda.ValorTotal) * itens.ValorTotal :
                                                            (itens.Produto.FormacaoPreco.PercentualComissoesVenda.ToDouble() / (100)) * itens.ValorTotal;
                        else
                            item.ComissaoItem = (ValorComissaoPedido / venda.ValorTotal) * itens.ValorTotal;

                        string calculoComissao = item.ComissaoItem.ToString("0.00000");

                        totalComissao += calculoComissao.ToDouble();
                        
                        totalSomaGeral += item.ValorTotalItem;
                        totalQuantidadeGeral += item.Quantidade;
                        totalChoquePorItem += item.ChoquePorItem;
                        temPedido = true;

                        vendaRelatorio.itensRel.Add(item);
                    }
                }

                if (temPedido)
                {
                    vendaRelatorio.Id = venda.Id.ToString();

                    vendaRelatorio.ClienteId = venda.ClienteId.ToString();
                    vendaRelatorio.NomeCliente = venda.ClienteNome;

                    vendaRelatorio.NomeColaborador = _nomeColaborador;
                    vendaRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                    vendaRelatorio.Situacao = venda.Status.Descricao();
                    vendaRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                    vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    ValorComissaoPedido = 0;
                    _nomeColaborador = "";

                    listaVendasRelatorio.Add(vendaRelatorio);
                }
            }

            ConteudoRelatorio.DataSource = listaVendasRelatorio;

            //Totalizar no final
            txtTotalPedidos.Text = listaVWVenda.Count.ToString();
            txtSomaPedidos.Text = totalSomaGeral.ToString("R$ #,###,##0.00");
            txtTotalComissao.Text = totalComissao.ToString("R$ #,###,##0.00");
            txtSomaGeralItens.Text = totalQuantidadeGeral.ToString("#,###,##0.00");
            txtTotalChoqueItem.Text = totalChoquePorItem.ToString("R$ #,###,##0.00");

            //Dar ou não visibilidade
            //Labels
            xrLabel2.Visible = false; xrLabel10.Visible = false;
            xrLabel12.Visible = true; xrLabel7.Visible = false; xrLabel11.Visible = false; xrLabel9.Visible = false;
            
            //Txts
            txtQuantidadeItens.Visible = true; txtSomaQuantidadeItens.Visible = true; txtSomaTotalItens.Visible = true;
            txtTotalPedidos.Visible = false; txtSomaPedidos.Visible = false; txtSomaPedidos.Visible = true;
            txtSomaGeralItens.Visible = true; txtGeralItens.Visible = true; txtTotalChoquePedido.Visible = false;
            txtTotalChoqueItem.Visible = true; txtAgrupoChoquePorItem.Visible = true; txtAgrupoChoquePorPedido.Visible = false;
            
            //Tables
            xrTableCell23.Visible = false; xrTableCell24.Visible = false; 

            if (_marca != null)
            {
                txtGruposBuscados.Visible = true;
                txtMarca.Visible = true;
                txtMarca.Text = _marca.Descricao;
            }

            if (_fabricante != null)
            {
                txtFabricante.Visible = true;
                txtFabricante.Text = _fabricante.Descricao;
            }

            if (_categoria != null)
            {
                txtCategoria.Visible = true;
                txtCategoria.Text = _categoria.Descricao;
            }

            if (_grupo != null)
            {
                txtGrupo.Visible = true;
                txtGrupo.Text = _grupo.Descricao;
            }

            if (_subgrupo != null)
            {
                txtSubgrupo.Visible = true;
                txtSubgrupo.Text = _subgrupo.Descricao;
            }
        }

        #endregion

#region "Emitir Relatório Comissao Por Itens"

        private void CarregueDadosRelatorioPorItens()
        {
            double totalComissao = 0;
            double totalComissaoRecebido = 0;
            double totalSomaGeral = 0;
            double totalQuantidadeGeral = 0;
            double totalChoquePorItem = 0;
            double ValorComissaoPedido = 0;
            bool temPedido;

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteListaVWVendas(_pesquisaPorFuncao,
                                                                                                                               _parceiroPesquisa,
                                                                                                                               _parceiroPesquisaII,
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
                                                                                                                               false,
                                                                                                                               false,
                                                                                                                               _ordenacao);

            List<VendasRelatorioChoque> listaVendasRelatorio = new List<VendasRelatorioChoque>();

            foreach (var venda in listaVWVenda)
            {
                VendasRelatorioChoque vendaRelatorio = new VendasRelatorioChoque();

                temPedido = false;

                if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    _nomeColaborador = venda.IndicadorNome;
                    ValorComissaoPedido = venda.ComissaoIndicador;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    _nomeColaborador = venda.AtendenteNome;
                    ValorComissaoPedido = venda.ComissaoAtendente;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    _nomeColaborador = venda.VendedorNome;

                    if (!_comissaoCompartilhada)
                    {
                        ValorComissaoPedido = venda.ComissaoVendedor;
                    }
                    else
                    {
                        ValorComissaoPedido = venda.ComissaoVendedor - venda.ComissaoAtendente;
                    }                   
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    _nomeColaborador = venda.SupervisorNome;
                    ValorComissaoPedido = venda.ComissaoSupervisor;
                }

                //***Comissão apenas dos recebidos, entra aqui...
                if (_recebidos)
                {
                    vendaRelatorio.Comissao = ValorComissaoPedido;
                    CalculeComissaoApenasVendasRecebidos(venda.Id, venda.ValorTotal, ref totalComissaoRecebido, ref vendaRelatorio);
                    ValorComissaoPedido = vendaRelatorio.Comissao;
                }
                //*** Fim comissão recebidos

                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(venda.Id);

                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    itensRelChoque item = new itensRelChoque();
                                        
                    item.Id = itens.Produto.Id;
                    item.Descricao = itens.Produto.DadosGerais.Descricao;
                    item.Quantidade = itens.Quantidade;
                    item.ValorTotalItem = itens.ValorTotal;
                    item.ValorTotalItemTabela = itens.ValorUnitario * itens.Quantidade;
                    item.ChoquePorItem = item.ValorTotalItem - item.ValorTotalItemTabela;

                    if (_comissaoPorItem)
                    {
                        if (itens.Produto.FormacaoPreco.PercentualComissoesVenda == null || itens.Produto.FormacaoPreco.PercentualComissoesVenda == 0)
                        {
                            item.ComissaoItem = (ValorComissaoPedido / venda.ValorTotal) * itens.ValorTotal;
                        }
                        else
                        {
                            item.ComissaoItem = (itens.Produto.FormacaoPreco.PercentualComissoesVenda.ToDouble() / (100)) * itens.ValorTotal;
                        }
                    }
                    else
                    {
                        item.ComissaoItem = (ValorComissaoPedido / venda.ValorTotal) * itens.ValorTotal;
                    }

                    string calculoComissao = item.ComissaoItem.ToString("0.00000");

                    totalComissao += calculoComissao.ToDouble();
                    
                    totalSomaGeral += item.ValorTotalItem;
                    totalQuantidadeGeral += item.Quantidade;
                    totalChoquePorItem += item.ChoquePorItem;
                    temPedido = true;

                    vendaRelatorio.itensRel.Add(item);                    
                }

                if (temPedido)
                {
                    vendaRelatorio.Id = venda.Id.ToString();

                    vendaRelatorio.ClienteId = venda.ClienteId.ToString();
                    vendaRelatorio.NomeCliente = venda.ClienteNome;

                    vendaRelatorio.NomeColaborador = _nomeColaborador;
                    vendaRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                    vendaRelatorio.Situacao = venda.Status.Descricao();
                    vendaRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                    vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    ValorComissaoPedido = 0;
                    _nomeColaborador = "";

                    listaVendasRelatorio.Add(vendaRelatorio);
                }
            }

            ConteudoRelatorio.DataSource = listaVendasRelatorio;

            //Totalizar no final
            txtTotalPedidos.Text = listaVWVenda.Count.ToString();
            txtSomaPedidos.Text = totalSomaGeral.ToString("R$ #,###,##0.00");
            txtTotalComissao.Text = totalComissao.ToString("R$ #,###,##0.00");
            txtSomaGeralItens.Text = totalQuantidadeGeral.ToString("#,###,##0.00");
            txtTotalChoqueItem.Text = totalChoquePorItem.ToString("R$ #,###,##0.00");

            //Dar ou não visibilidade
            //Labels
            xrLabel2.Visible = false; xrLabel10.Visible = false;
            xrLabel12.Visible = true; xrLabel7.Visible = false; xrLabel11.Visible = false; xrLabel9.Visible = false;

            //Txts
            txtQuantidadeItens.Visible = true; txtSomaQuantidadeItens.Visible = true; txtSomaTotalItens.Visible = true;
            txtTotalPedidos.Visible = false; txtSomaPedidos.Visible = false; txtSomaPedidos.Visible = true;
            txtSomaGeralItens.Visible = true; txtGeralItens.Visible = true; txtTotalChoquePedido.Visible = false;
            txtTotalChoqueItem.Visible = true; txtAgrupoChoquePorItem.Visible = true; txtAgrupoChoquePorPedido.Visible = false;

            //Tables
            xrTableCell23.Visible = false; xrTableCell24.Visible = false;
                        
        }

        #endregion

        #region "Métodos Auxiliares"

        public void CalculeComissaoApenasVendasRecebidos(int vendaId, double vendaValorTotal, ref double totalComissaoRecebido, ref VendasRelatorioChoque vendaRelatorio)
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber();

            var contasPagarReceber = servicoContasPagarReceber.ConsulteListaDeRecebimentoPorPedido(vendaId.ToString());

            if (!contasPagarReceber.Exists(x => x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO) &&
                !contasPagarReceber.Exists(x => x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO))
            {
                if (contasPagarReceber != null && contasPagarReceber.Count > 0)
                {
                    double valorRecebido = 0;

                    foreach (var item in contasPagarReceber)
                    {
                        if (item.ValorPago != 0)
                        {
                            valorRecebido += item.ValorPago;
                        }
                    }

                    if (valorRecebido != 0)
                    {
                        vendaRelatorio.Comissao = (vendaRelatorio.Comissao / vendaValorTotal) * valorRecebido;
                        totalComissaoRecebido += vendaRelatorio.Comissao;
                    }
                    else
                    {
                        vendaRelatorio.Comissao = 0;
                    }
                }
                else
                {
                    totalComissaoRecebido += vendaRelatorio.Comissao;
                }
            }
            else
            {
                totalComissaoRecebido += vendaRelatorio.Comissao;
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class VendasRelatorioChoque
        {
            public VendasRelatorioChoque()
            {
                itensRel = new List<itensRelChoque>();
            }

            public string Id { get; set; }

            public string ClienteId { get; set; }

            public string NomeCliente { get; set; }

            public string NomeColaborador { get; set; }

            public string TipoPedido { get; set; }

            public string Situacao { get; set; }

            public string DataEmissao { get; set; }

            public string DataFaturamento { get; set; }

            public double ValorPedido { get; set; }

            public double ValorPedidoTabela { get; set; }

            public double ChoquePorPedido { get; set; }

            public double Comissao { get; set; }

            public List<itensRelChoque> itensRel { get; set; }
        }

        public class itensRelChoque
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
            public double ComissaoItem { get; set; }
            public double ValorTotalItemTabela { get; set; }
            public double ChoquePorItem { get; set; }
            public double ValorTotalItem { get; set; }
            public double Quantidade { get; set; }
        }

        #endregion
    }
}
