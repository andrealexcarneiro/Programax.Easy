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
    public partial class RelatorioVendas : RelatorioBasePaisagem
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

        private bool _comissaoServico;

        private bool _statusNaoPago;
        private bool _statusNotaSemRecebimento;
        private bool _consolidado;
        private string _nomeColaborador;
        private Marca _marca;
        private Fabricante _fabricante;
        private SubGrupo _subgrupo;
        private Categoria _categoria;
        private Grupo _grupo;
        private string _nomevendedorConsolidado = "";
        private int contador = 0;
        private double vlAcumulado = 0;

        #endregion

        #region " CONSTRUTOR "

        public RelatorioVendas(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                         List<Pessoa> parceiroPesquisa,
                                         List<Pessoa> parceiroPesquisaII,
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
                                         bool recebidos, bool statusNaoPago, bool statusNotaSemRecebimento,
                                         EnumOrdenacaoPesquisaVwVendas ordenacao,
                                         Marca marca, Fabricante fabricante,
                                         SubGrupo subgrupo, Categoria categoria,
                                         Grupo grupo, bool comissaoServico = false,
                                         bool consolidado = false )
        {
            InitializeComponent();

            _pesquisaPorFuncao = pesquisaPorFuncao;
            _parceiroPesquisa = parceiroPesquisa;
            _parceiroPesquisaII = parceiroPesquisaII;
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

            _comissaoServico = comissaoServico;

            _statusNaoPago = statusNaoPago;
            _statusNotaSemRecebimento = statusNotaSemRecebimento;

            _marca = marca;
            _fabricante = fabricante;
            _subgrupo = subgrupo;
            _categoria = categoria;
            _grupo = grupo;

            _ordenacao = ordenacao;
            _consolidado = consolidado;
            

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
            //Vai calcular as comissões de serviço
            if (_comissaoServico)
            {
                CarregaRelatorioVendasComissoesServicoPorItem();
                return;
            }

            //Vai fazer a busca por grupo e carregar as comissões por item
            if (_marca != null || _fabricante != null || _categoria != null)
            {
                CarregueDadosRelatorioPorGrupo();
                return;
            }
            if (_comissaoPorItem)
            {
                CarregueDadosRelatorioPorItens();
                return;
            }
            double totalComissao = 0;
            double totalComissaoRecebido = 0;
            if (_consolidado == true)
            {
                _ordenacao = EnumOrdenacaoPesquisaVwVendas.VENDEDOR;
            }

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
                                                                                                                               _statusNaoPago,
                                                                                                                               _statusNotaSemRecebimento,
                                                                                                                               _ordenacao
                                                                                                                               );

            List<VendasRelatorio> listaVendasRelatorio = new List<VendasRelatorio>();

            xrTableCell6.Text = "";
            xrLabel3.Visible = true;
            xrLabel15.Visible = false;
            xrLabel9.Visible = true;
            xrLabel17.Visible = false;
            xrLabel10.Visible = true;
            xrLabel18.Visible = false;
            xrLabel14.Visible = true;
            xrTableCell17.Visible = true;
            xrTableCell23.Visible = true;
            xrTableCell24.Visible = true;
            xrLabel5.Visible = true;
            xrLabel12.Visible = true;
            xrLabel7.Visible = true;

            if (_consolidado == false)
            {


                foreach (var venda in listaVWVenda)
                {
                    VendasRelatorio vendaRelatorio = new VendasRelatorio();

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

                    else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADORVENDEDOR)
                    {
                        vendaRelatorio.NomeColaborador = venda.IndicadorNome + " - " + venda.VendedorNome;

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
                        vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    }
                    //*** Fim comissão recebidos

                    vendaRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                    vendaRelatorio.Situacao = venda.Status.Descricao();
                    vendaRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                    if (venda.Status.Descricao() != "RESERVADO" && venda.Status.Descricao() != "CANCELADO")
                    {
                        vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    }

                    vendaRelatorio.ValorPedido = venda.ValorTotal;

                    listaVendasRelatorio.Add(vendaRelatorio);
                }

            }
            else
            {

                
                foreach (var venda in listaVWVenda)
                {
                    VendasRelatorio vendaRelatorio = new VendasRelatorio();

                    if (_nomevendedorConsolidado.ToString() != venda.VendedorNome)
                    {
                       //if(_nomevendedorConsolidado == "")
                        //{
                            _nomevendedorConsolidado = venda.VendedorNome;
                        //}
                        foreach (var venda2 in listaVWVenda)
                        {
                            if (_nomevendedorConsolidado.ToString() == venda2.VendedorNome)
                            {
                                contador += 1;
                                vlAcumulado += venda2.ValorTotal;
                                //_nomevendedorConsolidado = venda2.VendedorNome;
                            }
                        }
                        vendaRelatorio.NomeColaborador = venda.VendedorNome;
                        vendaRelatorio.IdContador = contador;
                        vendaRelatorio.ValorAcumulado = vlAcumulado;
                        listaVendasRelatorio.Add(vendaRelatorio);
                        contador = 0;
                        vlAcumulado = 0;
                    }
                    _nomevendedorConsolidado = venda.VendedorNome;

                }
                xrLabel15.Visible = true;
                xrLabel3.Visible = false;
                xrTableCell19.Visible = false;
                xrLabel9.Visible = false;
                xrLabel17.Visible = true;
                xrLabel10.Visible = false;
                xrLabel18.Visible = true;
                xrLabel14.Visible = false;
                xrTableCell17.Visible = false;
                xrTableCell23.Visible = false;
                xrTableCell24.Visible = false;
                xrLabel5.Visible = false;
                xrLabel12.Visible = false;
                xrLabel7.Visible = false;

                
            }
            ConteudoRelatorio.DataSource = listaVendasRelatorio;
            txtTotalPedidos.Text = listaVWVenda.Count.ToString();
            txtSomaPedidos.Text = listaVWVenda.Sum(x => x.ValorTotal).ToString("R$ #,###,##0.00");
            txtTotalComissao.Text = !_recebidos ? totalComissao.ToString("R$ #,###,##0.00") : totalComissaoRecebido.ToString("R$ #,###,##0.00");
        }

        #endregion

        # region "Emitir Relatório Por Grupo"

        private void CarregueDadosRelatorioPorGrupo()
        {
            double totalComissao = 0;
            double totalComissaoRecebido = 0;
            double totalSomaGeral = 0;
            double totalQuantidadeGeral = 0;
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
                                                                                                                               _statusNaoPago,
                                                                                                                               _statusNotaSemRecebimento,
                                                                                                                               _ordenacao);

            List<VendasRelatorio> listaVendasRelatorio = new List<VendasRelatorio>();
            xrTableCell6.Text = "";
            xrLabel3.Visible = true;
            xrLabel15.Visible = false;
            xrLabel9.Visible = true;
            xrLabel17.Visible = false;
            xrLabel10.Visible = true;
            xrLabel18.Visible = false;
            xrLabel14.Visible = true;
            xrTableCell17.Visible = true;
            xrTableCell23.Visible = true;
            xrTableCell24.Visible = true;
            xrLabel5.Visible = true;
            xrLabel12.Visible = true;
            xrLabel7.Visible = true;

            foreach (var venda in listaVWVenda)
            {
                VendasRelatorio vendaRelatorio = new VendasRelatorio();

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
                    vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                }
                //*** Fim comissão recebidos
               
                var listaVWItensPedido = servicoPedidoDeVenda.ConsulteJoinComItens(venda.Id);

                foreach (var itens in listaVWItensPedido.ListaItens)
                {
                    itensRel item = new itensRel();

                    ServicoProduto servicoProduto = new ServicoProduto();
                   
                    var listaProdutos = servicoProduto.ConsulteListasProdutosAtivosII(itens.Produto.Id,
                                                                      _marca, _fabricante, _categoria,
                                                                       _grupo, _subgrupo);




                    //var EstahNosItens = itens.PedidoDeVenda.ListaItens.Where(x => (x.Produto.Principal.Marca != null && x.Produto.Principal.Marca.Id == (_marca != null? _marca.Id:0)) ||
                    //                                            (x.Produto.Principal.Fabricante != null && x.Produto.Principal.Fabricante.Id == (_fabricante != null?_fabricante.Id:0)) ||
                    //                                             (x.Produto.Principal.Categoria != null && x.Produto.Principal.Categoria.Id == (_categoria != null? _categoria.Id:0)) ||
                    //                                             (x.Produto.Principal.Grupo != null && x.Produto.Principal.Grupo.Id == (_grupo != null? _grupo.Id:0)) ||
                    //                                             (x.Produto.Principal.SubGrupo != null && x.Produto.Principal.SubGrupo.Id == (_subgrupo != null? _subgrupo.Id:0)));





                    if (listaProdutos.Count > 0)
                    {
                        item.Id = itens.Produto.Id;
                        item.Descricao = itens.Produto.DadosGerais.Descricao;
                        item.Quantidade = itens.Quantidade;
                        item.ValorTotalItem = itens.ValorTotal;

                        if (_comissaoPorItem)
                            item.ComissaoItem = itens.Produto.FormacaoPreco.PercentualComissoesVenda == null || itens.Produto.FormacaoPreco.PercentualComissoesVenda == 0 ?
                                                            (ValorComissaoPedido / venda.ValorTotal) * itens.ValorTotal :
                                                            (itens.Produto.FormacaoPreco.PercentualComissoesVenda.ToDouble() / (100)) * itens.ValorTotal;
                        else
                            item.ComissaoItem = (ValorComissaoPedido / venda.ValorTotal) * itens.ValorTotal;

                        string calculoComissao = item.ComissaoItem.ToString("0.00000");

                        totalComissao += calculoComissao.ToDouble();

                        totalSomaGeral += item.ValorTotalItem;
                        totalQuantidadeGeral += item.Quantidade;
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
                    //if (venda.Status.ToInt() != 1)
                    //{
                        vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    //}
                     
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

            //Dar ou não visibilidade
            //Labels
            xrLabel2.Visible = false; xrLabel10.Visible = false;
            xrLabel12.Visible = true; xrLabel7.Visible = false; xrLabel11.Visible = false; xrLabel9.Visible = false;

            //Txts
            txtQuantidadeItens.Visible = true; txtSomaQuantidadeItens.Visible = true; txtSomaTotalItens.Visible = true;
            txtTotalPedidos.Visible = false; txtSomaPedidos.Visible = false; txtSomaPedidos.Visible = true;
            txtSomaGeralItens.Visible = true; txtGeralItens.Visible = true;

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

        #region "Emitir Relatório de Comissão Por Item"

        private void CarregueDadosRelatorioPorItens()
        {
            double totalComissao = 0;
            double totalComissaoRecebido = 0;
            double totalSomaGeral = 0;
            double totalQuantidadeGeral = 0;
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
                                                                                                                               _statusNaoPago,
                                                                                                                               _statusNotaSemRecebimento,
                                                                                                                               _ordenacao);

            List<VendasRelatorio> listaVendasRelatorio = new List<VendasRelatorio>();
            xrTableCell6.Text = "";
            xrLabel3.Visible = true;
            xrLabel15.Visible = false;
            xrLabel9.Visible = true;
            xrLabel17.Visible = false;
            xrLabel10.Visible = true;
            xrLabel18.Visible = false;
            xrLabel14.Visible = true;
            xrTableCell17.Visible = true;
            xrTableCell23.Visible = true;
            xrTableCell24.Visible = true;
            xrLabel5.Visible = true;
            xrLabel12.Visible = true;
            xrLabel7.Visible = true;

            foreach (var venda in listaVWVenda)
            {
                VendasRelatorio vendaRelatorio = new VendasRelatorio();

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
                    itensRel item = new itensRel();

                    item.Id = itens.Produto.Id;
                    item.Descricao = itens.Produto.DadosGerais.Descricao;
                    item.Quantidade = itens.Quantidade;
                    item.ValorTotalItem = itens.ValorTotal;

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
                    if (venda.Status.ToInt() != 1)
                    {
                        vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                    }
                        
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

            //Dar ou não visibilidade
            //Labels
            xrLabel2.Visible = false; xrLabel10.Visible = false;
            xrLabel12.Visible = true; xrLabel7.Visible = false; xrLabel11.Visible = false; xrLabel9.Visible = false;

            //Txts
            txtQuantidadeItens.Visible = true; txtSomaQuantidadeItens.Visible = true; txtSomaTotalItens.Visible = true;
            txtTotalPedidos.Visible = false; txtSomaPedidos.Visible = false; txtSomaPedidos.Visible = true;
            txtSomaGeralItens.Visible = true; txtGeralItens.Visible = true;

            //Tables
            xrTableCell23.Visible = false; xrTableCell24.Visible = false;
        }

        #endregion

        #region "Emitir relatório de comissões de serviço por item"

        private void CarregaRelatorioVendasComissoesServicoPorItem()
        {
            double totalComissao = 0;
            double totalComissaoRecebido = 0;

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            ServicoRoteiro servicoRoteiro =new ServicoRoteiro();

            List<PedidoDeVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteListaPedidosPagosERoteiroConcluido(_pesquisaPorFuncao,
                                                                                                                               _parceiroPesquisa,
                                                                                                                               _periodoFaturamento,
                                                                                                                               _dataInicialPeriodo,
                                                                                                                               _dataFinalPeriodo);

            List<VendasRelatorio> listaVendasRelatorio = new List<VendasRelatorio>();

            
            xrTableCell6.Text = "";
            xrLabel3.Visible = true;
            xrLabel15.Visible = false;
            xrLabel9.Visible = true;
            xrLabel17.Visible = false;
            xrLabel10.Visible = true;
            xrLabel18.Visible = false;
            xrLabel14.Visible = true;
            xrTableCell17.Visible = true;
            xrTableCell23.Visible = true;
            xrTableCell24.Visible = true;
            xrLabel5.Visible = true;
            xrLabel12.Visible = true;
            xrLabel7.Visible = true;


            foreach (var venda in listaVWVenda)
            {
                VendasRelatorio vendaRelatorio = new VendasRelatorio();

                vendaRelatorio.Id = venda.Id.ToString();

                vendaRelatorio.ClienteId = venda.Cliente.Id.ToString();
                vendaRelatorio.NomeCliente = venda.Cliente.DadosGerais.Razao;

                if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    vendaRelatorio.NomeColaborador = venda.Indicador.DadosGerais.Razao;

                    var agenda = servicoRoteiro.ConsultePorPedido(venda.Id);

                    var comissao = retorneComissaoPorPedido(venda, agenda);

                    vendaRelatorio.Comissao = comissao;
                    totalComissao += comissao;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    vendaRelatorio.NomeColaborador = venda.Atendente.DadosGerais.Razao;

                    var agenda = servicoRoteiro.ConsultePorPedido(venda.Id);

                    var comissao = retorneComissaoPorPedido(venda, agenda);

                    vendaRelatorio.Comissao = comissao;
                    totalComissao += comissao;
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    vendaRelatorio.NomeColaborador = venda.Vendedor.DadosGerais.Razao;

                    var agenda = servicoRoteiro.ConsultePorPedido(venda.Id);

                    var comissao = retorneComissaoPorPedido(venda, agenda);

                    vendaRelatorio.Comissao = comissao;
                    totalComissao += comissao;
                    
                }
                else if (_pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    vendaRelatorio.NomeColaborador = venda.Supervisor.DadosGerais.Razao;

                    var agenda = servicoRoteiro.ConsultePorPedido(venda.Id);

                    var comissao = retorneComissaoPorPedido(venda, agenda);

                    vendaRelatorio.Comissao = comissao;
                    totalComissao += comissao;
                }

                vendaRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                vendaRelatorio.Situacao = venda.StatusPedidoVenda.Descricao();
                vendaRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                if (venda.StatusPedidoVenda.ToInt() != 1)
                {
                    vendaRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;
                }
                    
                vendaRelatorio.ValorPedido = venda.ValorTotal;

                listaVendasRelatorio.Add(vendaRelatorio);
            }

            ConteudoRelatorio.DataSource = listaVendasRelatorio;

            txtTotalPedidos.Text = listaVWVenda.Count.ToString();
            txtSomaPedidos.Text = listaVWVenda.Sum(x => x.ValorTotal).ToString("R$ #,###,##0.00");
            txtTotalComissao.Text = !_recebidos ? totalComissao.ToString("R$ #,###,##0.00") : totalComissaoRecebido.ToString("R$ #,###,##0.00");
        }

        private double retorneComissaoPorPedido(PedidoDeVenda pedido, Roteiro agenda)
        {
            double comissao = 0;
            double quantidade = 0;
            double reducao = 0;

            switch (agenda.TipoServico)
            {
                case EnumTipoServico.ENTREGAEINSTALACAO:

                    var quantidadeEntrega = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorEntrega.ToDouble() > 0).Sum(x => x.Quantidade);
                    var quantidadeInstalacao = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorInstalacao.ToDouble() > 0).Sum(x => x.Quantidade);

                    var comissaoEntrega = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorEntrega.ToDouble()) * quantidadeEntrega;

                    var comissaoInstalacao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorInstalacao.ToDouble()) * quantidadeInstalacao;


                    if (quantidadeEntrega == 2)
                    {
                        reducao = (20 / (100).ToDouble() * comissaoEntrega);

                        comissaoEntrega = comissaoEntrega - reducao;
                    }                       
                    else if (quantidadeEntrega >=3)
                    {
                        reducao = (30 / (100).ToDouble() * comissaoEntrega);

                        comissaoEntrega = comissaoEntrega - reducao;
                    }

                    if (quantidadeInstalacao == 2)
                    {
                        reducao = (20/(100).ToDouble() * comissaoInstalacao);

                        comissaoInstalacao = comissaoInstalacao - reducao;
                    }
                    else if (quantidadeInstalacao >= 3)
                    {
                        reducao = (30 / (100).ToDouble() * comissaoInstalacao);

                        comissaoInstalacao = comissaoInstalacao - (comissaoInstalacao * (30 / 100));
                    }

                    comissao = comissaoEntrega + comissaoInstalacao;

                    return comissao;

                case EnumTipoServico.SOENTREGA:

                    quantidade = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorEntrega.ToDouble() > 0).Sum(x => x.Quantidade);

                    comissao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorEntrega.ToDouble()) * quantidade;

                    if (quantidade == 2)
                    {
                        reducao = (20 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }                        
                    else if (quantidade >= 3)
                    {
                        reducao = (30 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }   

                    return comissao;

                case EnumTipoServico.ENTREGAAPOSHORARIO:

                    quantidade = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorEntregaAposHorario.ToDouble() > 0).Sum(x => x.Quantidade);

                    comissao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorEntregaAposHorario.ToDouble()) * quantidade;

                    if (quantidade == 2)
                    {
                        reducao = (20 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }
                    else if (quantidade >= 3)
                    {
                        reducao = (30 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }

                    return comissao;

                case EnumTipoServico.SOINSTALACAO:

                    quantidade = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorInstalacao.ToDouble() > 0).Sum(x => x.Quantidade);

                    comissao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorInstalacao.ToDouble()) * quantidade;

                    if (quantidade == 2)
                    {
                        reducao = (20 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }
                    else if (quantidade >= 3)
                    {
                        reducao = (30 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }

                    return comissao;

                case EnumTipoServico.INSTALACAOAPOSHORARIO:

                    quantidade = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorInstalacaoAposHorario.ToDouble() > 0).Sum(x => x.Quantidade);

                    comissao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorInstalacaoAposHorario.ToDouble()) * quantidade;

                    if (quantidade == 2)
                    {
                        reducao = (20 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }
                    else if (quantidade >= 3)
                    {
                        reducao = (30 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }

                    return comissao;

                case EnumTipoServico.INSTALACAOEMOUTRASCIDADES:

                    quantidade = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorInstalacaoOutrasCidades.ToDouble() > 0).Sum(x => x.Quantidade);

                    comissao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorInstalacaoOutrasCidades.ToDouble()) * quantidade;

                    if (quantidade == 2)
                    {
                        reducao = (20 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }
                    else if (quantidade >= 3)
                    {
                        reducao = (30 / (100).ToDouble() * comissao);
                        comissao = comissao - reducao;
                    }

                    return comissao;

                case EnumTipoServico.DESLOCAMENTOEGARANTIA:

                    quantidade = pedido.ListaItens.Where(x => x.Produto.FormacaoPreco.ValorDeslocamentoEGarantia.ToDouble() > 0).Sum(x => x.Quantidade);

                    comissao = pedido.ListaItens.Sum(x => x.Produto.FormacaoPreco.ValorDeslocamentoEGarantia.ToDouble()) * quantidade;

                    return comissao;
            }
            
            return 0;
        }

        #endregion

        #region "Métodos Auxiliares"

        public void CalculeComissaoApenasVendasRecebidos(int vendaId,double vendaValorTotal, ref double totalComissaoRecebido,ref VendasRelatorio vendaRelatorio)
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

        public class VendasRelatorio
        {
            public VendasRelatorio()
            {
                itensRel = new List<itensRel>();
            }

            public string Id { get; set; }
            public int IdContador { get; set; }

            public string ClienteId { get; set; }

            public string NomeCliente { get; set; }

            public string NomeColaborador { get; set; }

            public string TipoPedido { get; set; }

            public string Situacao { get; set; }

            public string DataEmissao { get; set; }

            public string DataFaturamento { get; set; }

            public double ValorPedido { get; set; }
            public double ValorAcumulado { get; set; }

            public double Comissao { get; set; }

            public List<itensRel> itensRel { get; set; }
        }

        public class itensRel
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
            public double ComissaoItem { get; set; }
            public double ValorTotalItem { get; set; }
            public double Quantidade { get; set; }
        }

        #endregion
    }
}
