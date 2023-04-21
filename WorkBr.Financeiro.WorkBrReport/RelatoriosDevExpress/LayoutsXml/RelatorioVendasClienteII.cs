using System;
using System.Collections.Generic;
using System.Linq;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Report.RelatoriosDevExpress.Vendas
{
    public partial class RelatorioVendasClientesII : RelatorioBasePaisagem
    {
        #region " VARIÁVEIS PRIVADAS "

        private Pessoa _cliente;        
        private EnumTipoPessoa? _tipoPessoaFisicaOuJuridica;
        private EnumTipoPedidoDeVenda? _tipoPedidoDeVenda;
        private List<FormaPagamento> _listaFormasPagamento;
        private List<ItemPedidoDeVenda> _listaItensPedidosVenda;
        private List<VendasClienteRelatorio> listaVendasRelatorio = new List<VendasClienteRelatorio>();        
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
        
        #endregion

        #region " CONSTRUTOR "

        public RelatorioVendasClientesII(Pessoa cliente,
                                                    EnumTipoPessoa? tipoPessoaFisicaOuJuridica,
                                                    EnumTipoPedidoDeVenda? tipoPedidoDeVenda,
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
                                                    EnumOrdenacaoPesquisaVwVendas ordenacao,
                                                    List<FormaPagamento> listaFormasPagamento,
                                                    Caixa caixa)
        {
            InitializeComponent();

            _cliente = cliente;
            _tipoPessoaFisicaOuJuridica = tipoPessoaFisicaOuJuridica;
            _tipoPedidoDeVenda = tipoPedidoDeVenda;
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
            
            _listaFormasPagamento = listaFormasPagamento;                
            
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

            _tituloRelatorio = "RELATÓRIO DE VENDAS POR CLIENTES";
        }

        #endregion
        public class PedidoDeVendaCliente
        {
          
        }
         
        #region "Métodos auxiliares"

        

        private void CarregarFormasPagamentoQuandoNãoSelecionado()
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

            List<FormaPagamento> listaFormaPagamento = new List<FormaPagamento>();

            _listaFormasPagamento = servicoFormaPagamento.ConsulteListaFormasDePagamentoAtivas();
        }
        private void PreenchaItensEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
                _listaItensPedidosVenda = pedidoDeVenda.ListaItens.ToList();
            }
            else
            {
                _listaItensPedidosVenda.Clear();
            }

            //PreenchaGridItens();
        }

        private void CarregaDadosStatusFaturadoEEmitidoNota()
        {   
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            PedidoDeVendaCliente pedidoDeVendaRelatorio = new PedidoDeVendaCliente();

            List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteListaVWVendasPorCliente(_cliente,
                                                                                                                                                _tipoPessoaFisicaOuJuridica,
                                                                                                                                           _tipoPedidoDeVenda,
                                                                                                                                                _periodoFaturamento,
                                                                                                                                                _dataInicialPeriodo,
                                                                                                                                                _dataFinalPeriodo,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                _statusFaturado,
                                                                                                                                                _statusEmitidoNFe,
                                                                                                                                                _ordenacao,
                                                                                                                                                null);


            double QuantidadeTotal = 0;
            double subTotal = 0;
            double totalDesconto = 0;
            double totalIcmsSTEIPI = 0;
            double pesoBrutoTotal = 0;
            foreach (var venda in listaVWVenda)
                {
                    for (int i = 0; i < _listaFormasPagamento.Count; i++)
                    {
                        VendasClienteRelatorio VendasClienteRelatorio = new VendasClienteRelatorio();
                      


                    var pedidoDeVendaItem = servicoPedidoDeVenda.Consulte(venda.Id);

                    VendasClienteRelatorio.Id = venda.Id.ToString();

                    pedidoDeVendaItem.ListaItens.CarregueLazyLoad();

                    
                    foreach (var item in pedidoDeVendaItem.ListaItens)
                    {
                        ItemVenda itemVenda = new ItemVenda();

                        itemVenda.CodigoProduto = item.Produto.Id;
                        itemVenda.DescricaoProduto = item.Produto.DadosGerais.Descricao;
                        itemVenda.Marca = item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                        itemVenda.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                        itemVenda.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Abreviacao : string.Empty;

                        itemVenda.Quantidade = item.Quantidade.ToString();
                        QuantidadeTotal += item.Quantidade;
                        itemVenda.ValorUnitario = item.ValorUnitario.ToString("#,###,##0.00");
                        itemVenda.Desconto = item.TotalDesconto.ToString("#,###,##0.00");
                        itemVenda.ValorTotal = (item.Quantidade * item.ValorUnitario - item.TotalDesconto).ToString("#,###,##0.00");

                        subTotal += item.ValorUnitario * item.Quantidade;
                        totalDesconto += item.TotalDesconto;
                        totalIcmsSTEIPI += item.ValorIcmsST.GetValueOrDefault();

                        //Calcula o peso bruto total, caso o produto não seja nulo
                        if (item.Produto.Principal.PesoBruto != null)
                            pesoBrutoTotal += (item.Produto.Principal.PesoBruto.ToDouble() * item.Quantidade.ToDouble());

                        VendasClienteRelatorio.ListaItensVendaCliente.Add(itemVenda);
                    }

                    VendasClienteRelatorio.ClienteId = venda.ClienteId.ToString();
                        VendasClienteRelatorio.NomeCliente = venda.ClienteNome;
                    //_listaItensPedidosVenda = servicoPedidoDeVenda..ConsulteListaFormasDePagamentoAtivas();
                    //VendasClienteRelatorio.CodProduto = pedidoDeVendaItem.ListaItens(venda.Id)
                        VendasClienteRelatorio.Cidade = venda.Cidade;
                        VendasClienteRelatorio.UF = venda.UF;

                        VendasClienteRelatorio.TipoCliente = venda.TipoCliente == EnumTipoPessoa.PESSOAFISICA ? "PF" : "PJ";

                        VendasClienteRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                        VendasClienteRelatorio.Situacao = venda.Status.Descricao();
                        VendasClienteRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                        VendasClienteRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;

                        VendasClienteRelatorio.FormaPagamento = venda.FormaPagamentoNome;
                        VendasClienteRelatorio.CondicaoPagamento = venda.CondicaoPagamentoNome;

                        VendasClienteRelatorio.ValorPedido = venda.ValorTotal.ToString("R$ #,###,##0.00");

                        VendasClienteRelatorio.ValorTotal = venda.ValorTotal;

                        ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa();

                        var movimentacaoItemCaixa = servicoMovimentacaoCaixa.ConsulteMovimentacaoNumeroItemCaixa(_dataInicialPeriodo, _dataFinalPeriodo, venda.Id, _listaFormasPagamento[i].Id);

                        if (movimentacaoItemCaixa != null)
                        {
                            VendasClienteRelatorio.ValorPedido = movimentacaoItemCaixa.Valor.ToString("0.00");
                            VendasClienteRelatorio.ValorTotal = movimentacaoItemCaixa.Valor;
                            VendasClienteRelatorio.FormaPagamento = movimentacaoItemCaixa.FormaPagamento.Descricao;
                            listaVendasRelatorio.Add(VendasClienteRelatorio);                            
                        }

                    if ((EnumTipoFormaPagamento)_listaFormasPagamento[i].Id == EnumTipoFormaPagamento.BOLETOBANCARIO || (EnumTipoFormaPagamento)_listaFormasPagamento[i].Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                        (EnumTipoFormaPagamento)_listaFormasPagamento[i].Id == EnumTipoFormaPagamento.DEPOSITOBANCARIO || (EnumTipoFormaPagamento)_listaFormasPagamento[i].Id == EnumTipoFormaPagamento.DUPLICATA ||
                        (EnumTipoFormaPagamento)_listaFormasPagamento[i].Id == EnumTipoFormaPagamento.CREDITOINTERNO)
                    {
                        if (venda.FormaPagamentoNome == _listaFormasPagamento[i].Descricao)
                            listaVendasRelatorio.Add(VendasClienteRelatorio);
                    }
                            

                }
                }           
        }
        
        private void CarregaDadosOutrosStatus()
        {
            for (int i = 0; i < _listaFormasPagamento.Count; i++)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteListaVWVendasPorCliente(_cliente,
                                                                                                                                                _tipoPessoaFisicaOuJuridica,
                                                                                                                                                _tipoPedidoDeVenda,
                                                                                                                                                _periodoFaturamento,
                                                                                                                                                _dataInicialPeriodo,
                                                                                                                                                _dataFinalPeriodo,
                                                                                                                                                _statusAberto,
                                                                                                                                                _statusOrcamento,
                                                                                                                                                _statusCancelado,
                                                                                                                                                _statusEmLiberacao,
                                                                                                                                                _statusRecusado,
                                                                                                                                                _statusReservado,
                                                                                                                                                false,
                                                                                                                                                false,   
                                                                                                                                                _ordenacao,
                                                                                                                                                _listaFormasPagamento[i].Descricao);



                foreach (var venda in listaVWVenda)
                {
                   
                    VendasClienteRelatorio VendasClienteRelatorio = new VendasClienteRelatorio();

                    VendasClienteRelatorio.Id = venda.Id.ToString();

                    VendasClienteRelatorio.ClienteId = venda.ClienteId.ToString();
                    VendasClienteRelatorio.NomeCliente = venda.ClienteNome;

                    VendasClienteRelatorio.Cidade = venda.Cidade;
                    VendasClienteRelatorio.UF = venda.UF;

                    VendasClienteRelatorio.TipoCliente = venda.TipoCliente == EnumTipoPessoa.PESSOAFISICA ? "PF" : "PJ";

                    VendasClienteRelatorio.TipoPedido = venda.TipoPedidoVenda.Descricao();
                    VendasClienteRelatorio.Situacao = venda.Status.Descricao();
                    VendasClienteRelatorio.DataEmissao = venda.DataElaboracao.ToString("dd/MM/yyyy");
                    VendasClienteRelatorio.DataFaturamento = venda.DataFechamento != null ? venda.DataFechamento.Value.ToString("dd/MM/yyyy") : string.Empty;

                    VendasClienteRelatorio.FormaPagamento = venda.FormaPagamentoNome;
                    VendasClienteRelatorio.CondicaoPagamento = venda.CondicaoPagamentoNome;

                    VendasClienteRelatorio.ValorPedido = venda.ValorTotal.ToString("R$ #,###,##0.00");

                    VendasClienteRelatorio.ValorTotal = venda.ValorTotal;

                    if (!listaVendasRelatorio.Exists(x=>x.Id.ToInt()== venda.Id))
                        listaVendasRelatorio.Add(VendasClienteRelatorio);
                }
            }
        }

        private void CarregarDadosRelatorioFormasPagamentoSelecionado()
        {  
            if(_statusFaturado || _statusEmitidoNFe)
                CarregaDadosStatusFaturadoEEmitidoNota();

            if (_statusAberto || _statusCancelado || _statusEmLiberacao || _statusOrcamento || _statusRecusado || _statusReservado)
            CarregaDadosOutrosStatus();

            ConteudoRelatorio.DataSource = listaVendasRelatorio;
            
            txtTotalPedidos.Text = listaVendasRelatorio.Count.ToString();
            txtSomaPedidos.Text = listaVendasRelatorio.Sum(x => x.ValorTotal).ToString("R$ #,###,##0.00");
        }
        
        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        protected override void CarregueDadosRelatorio()
        {
            if (_listaFormasPagamento.Count() == 0)
            {
                CarregarFormasPagamentoQuandoNãoSelecionado();
            }
            
            CarregarDadosRelatorioFormasPagamentoSelecionado();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        public class VendasClienteRelatorio
        {
            public VendasClienteRelatorio()
            {
                ListaItensVendaCliente = new List<ItemVenda>();
            }
            public List<ItemVenda> ListaItensVendaCliente { get; set; }
            public int IdPedido { get; set; }
            public string Id { get; set; }

            public string ClienteId { get; set; }

            public string NomeCliente { get; set; }

            public string TipoCliente { get; set; }

            public string Cidade { get; set; }

            public string UF { get; set; }

            public string FormaPagamento { get; set; }

            public string CondicaoPagamento { get; set; }

            public string TipoPedido { get; set; }

            public string Situacao { get; set; }

            public string DataEmissao { get; set; }

            public string DataFaturamento { get; set; }

            public string ValorPedido { get; set; }

            public double ValorTotal { get; set; }
           
            


        }
        public class ItemVenda
        {
            public int CodigoProduto { get; set; }

            public string DescricaoProduto { get; set; }

            public string Unidade { get; set; }

            public string Marca { get; set; }

            public string Cor { get; set; }

            public string Quantidade { get; set; }

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }
        }


        #endregion
    }
}
