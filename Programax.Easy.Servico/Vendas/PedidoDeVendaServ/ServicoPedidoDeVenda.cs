using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Easy.Negocio.Gerais;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    [Funcionalidade(EnumFuncionalidade.PEDIDODEVENDAS)]
    public class ServicoPedidoDeVenda : ServicoAkilSmallBusiness<PedidoDeVenda, ValidacaoPedidoDeVenda, ConversorPedidoDeVenda>
    {
        #region " VARIÁVEIS PRIVADAS "

        IRepositorioPedidoDeVenda _repositorioPedidoDeVenda;

        #endregion

        #region " CONSTRUTOR "

        public ServicoPedidoDeVenda()
        {
            RetorneRepositorio();
        }

        public ServicoPedidoDeVenda(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<PedidoDeVenda> RetorneRepositorio()
        {
            if (_repositorioPedidoDeVenda == null)
            {
                _repositorioPedidoDeVenda = FabricaDeRepositorios.Crie<IRepositorioPedidoDeVenda>();
            }

            return _repositorioPedidoDeVenda;
        }

        public void AtualizePedidoJaReservadoComNovosDados(PedidoDeVenda pedidoDeVenda)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {   
                var pedidoBase = _repositorioPedidoDeVenda.Consulte(pedidoDeVenda.Id);
                
                if (pedidoBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.RESERVADO ||
                    pedidoBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO)
                {
                    pedidoDeVenda.DataFechamento = DateTime.Now;

                    try
                    {
                        ValidacaoPedidoDeVenda validacaoPedidoDeVenda = new ValidacaoPedidoDeVenda();
                        validacaoPedidoDeVenda.ValidePedidoParaReserva();
                        validacaoPedidoDeVenda.Valide(pedidoDeVenda).AssegureSucesso();

                        FechePedidoDeVenda(pedidoDeVenda);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("ListaParcelasPedidoDeVenda"))
                            throw ex;
                        // Pedido precisa passsar pela liberação.
                        EnviePedidoDeVendaParaLiberacao(pedidoDeVenda);
                    }
                }
                else
                {
                    Atualize(pedidoDeVenda);
                }

                ValidacaoPedidoDeVenda validacaoPedidoDeVendaVerificaStatus = new ValidacaoPedidoDeVenda();
               // 'Andre
                //validacaoPedidoDeVendaVerificaStatus.ValidePedidoJaFaturadoOuEmitidoNfeNaoPodeSerAtualizado();
                validacaoPedidoDeVendaVerificaStatus.Valide(pedidoDeVenda).AssegureSucesso();

                scope.Complete();
            }
        }

        public override int Cadastre(PedidoDeVenda objetoDeNegocio)
        {
            PreenchaFormasPagamentosDaBase(objetoDeNegocio);

            return base.Cadastre(objetoDeNegocio);
        }

        public override void Atualize(PedidoDeVenda objetoDeNegocio)
        {
            //Andre
            PreenchaFormasPagamentosDaBase(objetoDeNegocio);

            base.Atualize(objetoDeNegocio);
        }

        #endregion

        #region " CONSULTAS "

        public PedidoDeVenda ConsulteJoinComItens(int pedidoId)
        {
            return _repositorioPedidoDeVenda.ConsulteJoinComItens(pedidoId);
        }

        public PedidoDeVenda ConsultePedidoFaturadoOuEmitidoNfe(int pedidoId, bool EhReservado = false)
        {
            return _repositorioPedidoDeVenda.ConsultePedidoFaturadoOuEmitidoNfe(pedidoId, EhReservado);
        }

        public PedidoDeVenda ConsulteMaiorVenda(Pessoa cliente)
        {
            return _repositorioPedidoDeVenda.ConsulteMaiorVenda(cliente);
        }

        public PedidoDeVenda ConsultePedidoParaRoteiro(int idPedido)
        {
            return _repositorioPedidoDeVenda.ConsultePedidoParaRoteiro(idPedido);
        }

        public List<PedidoDeVenda> ConsulteLista(DateTime? dataInicial,
                                                                      DateTime? dataFinal,
                                                                      Pessoa atendente,
                                                                      Pessoa vendedor,
                                                                      Pessoa cliente,
                                                                      EnumTipoPedidoDeVenda? tipoPedidoDeVenda,
                                                                      EnumStatusPedidoDeVenda? statusPedidoDeVenda, 
                                                                      Pessoa usuario=null, bool EReservado = false)
        {
            return _repositorioPedidoDeVenda.ConsulteLista(dataInicial, dataFinal, atendente, vendedor, cliente, tipoPedidoDeVenda, statusPedidoDeVenda, usuario, EReservado);
        }

        public List<PedidoDeVenda> ConsulteListaParaRoteiro(DateTime? dataInicial, DateTime? dataFinal)
        {
            return _repositorioPedidoDeVenda.ConsulteListaParaRoteiro(dataInicial, dataFinal);
        }

        #endregion

        #region " CONSULTAS VIEWS "

        public List<VWVenda> ConsulteListaVWVendas(EnumPesquisaPorFuncao pesquisaPorFuncao,
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
                                                                            bool statusNaoPago,
                                                                            bool statusNotaSemRecebimento,
                                                                            EnumOrdenacaoPesquisaVwVendas ordenacao
                                                                            )
        {
            return _repositorioPedidoDeVenda.ConsulteListaVWVenda(pesquisaPorFuncao,
                                                                                               parceiroPesquisa,
                                                                                               parceiroPesquisaII,
                                                                                               periodoFaturamento,
                                                                                               dataInicialPeriodo,
                                                                                               dataFinalPeriodo,
                                                                                               statusAberto,
                                                                                               statusOrcamento,
                                                                                               statusCancelado,
                                                                                               statusEmLiberacao,
                                                                                               statusRecusado,
                                                                                               statusReservado,
                                                                                               statusFaturado,
                                                                                               statusEmitidoNFe,
                                                                                               statusNaoPago,
                                                                                               statusNotaSemRecebimento,
                                                                                               ordenacao
                                                                                               );
        }
        public List<CustoFinanceiro> ConsulteListaCustoFinanceiro(List<Pessoa> parceiroPesquisa,
                                                                           DateTime dataInicialPeriodo,
                                                                           DateTime dataFinalPeriodo)
                                                                           
        {
            return _repositorioPedidoDeVenda.ConsulteListaCustoFinanceiro(parceiroPesquisa,
                                                                                               dataInicialPeriodo,
                                                                                               dataFinalPeriodo
                                                                                               );
        }

        public List<PedidoDeVenda> ConsulteListaPedidosPagosERoteiroConcluido(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                                                            List<Pessoa> parceiroPesquisa,
                                                                            bool periodoFaturamento,
                                                                            DateTime dataInicialPeriodo,
                                                                            DateTime dataFinalPeriodo)
        {
            return _repositorioPedidoDeVenda.ConsulteListaPedidosPagosERoteiroConcluido(pesquisaPorFuncao,
                                                                                                parceiroPesquisa,
                                                                                               periodoFaturamento,
                                                                                               dataInicialPeriodo,
                                                                                               dataFinalPeriodo);
        }
       
        public List<VWVendaTransportes> ConsulteListaVWVendasTransportes(EnumPesquisaPorFuncao pesquisaPorFuncao,
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
                                                                            EnumOrdenacaoPesquisaVwVendas ordenacao)
        {
            return _repositorioPedidoDeVenda.ConsulteListaVWVendaTransportes(pesquisaPorFuncao,
                                                                                               parceiroPesquisa,
                                                                                               periodoFaturamento,
                                                                                               dataInicialPeriodo,
                                                                                               dataFinalPeriodo,
                                                                                               statusAberto,
                                                                                               statusOrcamento,
                                                                                               statusCancelado,
                                                                                               statusEmLiberacao,
                                                                                               statusRecusado,
                                                                                               statusReservado,
                                                                                               statusFaturado,
                                                                                               statusEmitidoNFe,
                                                                                               ordenacao);
        }


        public List<VWVenda> ConsulteListaVWVendasPorCliente(Pessoa cliente,
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
                                                                                               string formaPagamento = null)
        {
            return _repositorioPedidoDeVenda.ConsulteListaVWVendasPorCliente(cliente,
                                                                                                                    tipoPessoaFisicaOuJuridica,
                                                                                                                    tipoPedidoDeVenda,
                                                                                                                    periodoFaturamento,
                                                                                                                    dataInicialPeriodo,
                                                                                                                    dataFinalPeriodo,
                                                                                                                    statusAberto,
                                                                                                                    statusOrcamento,
                                                                                                                    statusCancelado,
                                                                                                                    statusEmLiberacao,
                                                                                                                    statusRecusado,
                                                                                                                    statusReservado,
                                                                                                                    statusFaturado,
                                                                                                                    statusEmitidoNFe,                                                                                                                                                                                                                                     
                                                                                                                    ordenacao,
                                                                                                                    formaPagamento);
        }

        public List<VWVenda> ConsulteListaVWVendas(int? numeroDocumento, bool periodoFaturamento, DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool? vendaJahExportada)
        {
            return _repositorioPedidoDeVenda.ConsulteListaVWVendas(numeroDocumento, periodoFaturamento, dataInicial, dataFinal, statusPedidoDeVenda, vendaJahExportada);
        }
        public List<VWVenda> ConsulteTotalVWVendas(DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool statusEmitidoNFe)
        {
            return _repositorioPedidoDeVenda.ConsulteTotalVWVendas( dataInicial, dataFinal, statusPedidoDeVenda, statusEmitidoNFe);
        }
        public List<VWVenda> ConsulteTotalVWVendasII(DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool statusEmitidoNFe)
        {
            return _repositorioPedidoDeVenda.ConsulteTotalVWVendasII(dataInicial, dataFinal, statusPedidoDeVenda, statusEmitidoNFe);
        }
        public List<VWVenda> ConsulteVWVendasPag(DateTime dataInicial, DateTime dataFinal, string Condicao)
        {
            return _repositorioPedidoDeVenda.ConsulteVWVendasPag(dataInicial, dataFinal, Condicao);
        }

        #endregion

        #region " OPERAÇÕES COM PEDIDO DE VENDA "

        public void FechePedidoDeVenda(PedidoDeVenda pedidoDeVenda)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                ServicoParametros servicoParamentros = new ServicoParametros();

                var parametros = servicoParamentros.ConsulteParametros();

                //Se o parâmetro "Trabalhar Com Estoque Reservado" estiver marcado vai entrar aqui
                //e reservar o estoque. Mas se o parâmetro "Reserve Estoque Ao Faturar Pedido" estiver
                //marcado a reserva será no momento em que faturar o pedido, caso o parâmetro: "Baixar Estoque
                //Na Saída" estiver marcado, senão a baixa será direta faturamento do pedido.
                if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    if (!parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido)
                   // ReserveEstoque(pedidoDeVenda);

                InsiraNaturezaOperacaoParaVenda(pedidoDeVenda);

                pedidoDeVenda.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;
                pedidoDeVenda.DataFechamento = DateTime.Now.Date;

                if (pedidoDeVenda.Id == 0)
                {
                    this.Cadastre(pedidoDeVenda);
                }
                else
                {
                    this.Atualize(pedidoDeVenda);
                }

                scope.Complete();
            }
        }

        public void EnviePedidoDeVendaParaLiberacao(PedidoDeVenda pedidoDeVenda)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
                ServicoContasReceber servicoContasReceber = new ServicoContasReceber(false, false);

                var analiseCredito = servicoAnaliseCredito.Consulte(pedidoDeVenda.Cliente.Id);

                ServicoParametros servicoParamentros = new ServicoParametros();

                var parametros = servicoParamentros.ConsulteParametros();

                //Se o parâmetro "Trabalhar Com Estoque Reservado" estiver marcado vai entrar aqui
                //e reservar o estoque. Mas se o parâmetro "Reserve Estoque Ao Faturar Pedido" estiver
                //marcado a reserva será no momento em que faturar o pedido, caso o parâmetro: "Baixar Estoque
                //Na Saída" estiver marcado, senão a baixa será direta faturamento do pedido.
                if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    if (!parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido)
                        ReserveEstoque(pedidoDeVenda);

                InsiraNaturezaOperacaoParaVenda(pedidoDeVenda);

                pedidoDeVenda.StatusPedidoVenda = EnumStatusPedidoDeVenda.EMLIBERACAO;

                double saldoDisponivelMenosValoresPedido = pedidoDeVenda.SaldoDisponivel - pedidoDeVenda.ListaParcelasPedidoDeVenda
                                                                                                                                          .Sum(x => x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
                                                                                                                                                          x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                                                                                                                                                          x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO ||
                                                                                                                                                          x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ||
                                                                                                                                                          x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA ? x.Valor : 0);

                pedidoDeVenda.MotivoBloqueio = new MotivoBloqueio();
                pedidoDeVenda.MotivoBloqueio.SemSaldoCredito = saldoDisponivelMenosValoresPedido <= 0;
                pedidoDeVenda.MotivoBloqueio.DescontoAcima = pedidoDeVenda.ListaItens.Any(item => item.ItemEstahInconsistente);
                pedidoDeVenda.MotivoBloqueio.FinanceiroEmAberto = servicoContasReceber.PossuiTituloAtrasado(pedidoDeVenda.Cliente.Id);
                //Andre
                if (pedidoDeVenda.MotivoBloqueio.FinanceiroEmAberto == true)
                {
                    //pedidoDeVenda.MotivoBloqueio.ClienteBloqueado = analiseCredito.StatusAnaliseCredito == EnumStatusCrediario.LIBERACAOGERENTE;
                }
               

                if (!pedidoDeVenda.MotivoBloqueio.DescontoAcima)
                {
                    pedidoDeVenda.MotivoBloqueio.DescontoAcima = !CalculosPedidoDeVenda.DescontoEstahIgualOuAbaixoDoPermitido(pedidoDeVenda);
                }

                if (pedidoDeVenda.Id == 0)
                {
                    this.Cadastre(pedidoDeVenda);
                }
                else
                {
                    this.Atualize(pedidoDeVenda);
                }

                scope.Complete();
            }
        }

        public void CancelePedidoDeVenda(int pedidoDeVendaId)
        {
            var pedidoDeVenda = Consulte(pedidoDeVendaId);


            CanceleOuRecusePedidoDeVenda(pedidoDeVenda, EnumStatusPedidoDeVenda.CANCELADO);
        }

        public void CanceleOuRecusePedidoDeVenda(PedidoDeVenda pedidoDeVenda, EnumStatusPedidoDeVenda statusPedidoDeVenda)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO || pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.RESERVADO)
                {   
                    ServicoParametros servicoParametros = new ServicoParametros();
                    var parametros = servicoParametros.ConsulteParametros();

                    pedidoDeVenda = Consulte(pedidoDeVenda.Id); 

                    if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)                        
                        RemovaReservaEstoque(pedidoDeVenda);                    
                }
                else if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO || pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE)
                {
                    ValidacaoPedidoDeVenda validacaoPedidoDeVenda = new ValidacaoPedidoDeVenda();
                    validacaoPedidoDeVenda.ValidePedidoParaCancelamentoQuandoEstiverFaturadoOuEmitidoNfe();
                    validacaoPedidoDeVenda.Valide(pedidoDeVenda).AssegureSucesso();

                    ServicoParametros servicoParametros = new ServicoParametros();
                    var parametros = servicoParametros.ConsulteParametros();

                    pedidoDeVenda = Consulte(pedidoDeVenda.Id);

                    //Quando estiver marcado em Parametros > "Permite Baixar estoque na saída" deverá dar entrada ou baixa manualmente no estoque.
                    //if (!parametros.ParametrosVenda.PermiteBaixarEstoqueNaSaida)
                        EstorneProdutosParaEstoque(pedidoDeVenda);

                        InativeContasAReceberDoPedido(pedidoDeVenda);

                        GereSaidaDeCaixaDoPedido(pedidoDeVenda);
                    
                }

                pedidoDeVenda.StatusPedidoVenda = statusPedidoDeVenda;

                if (pedidoDeVenda.Id == 0)
                {
                    this.Cadastre(pedidoDeVenda);
                }
                else
                {
                    this.Atualize(pedidoDeVenda);
                }

                scope.Complete();
            }
        }

        public void VoltePedidoDeVendaParaReservado(int pedidoDeVendaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var pedido = Consulte(pedidoDeVendaId);

                if (pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO)                    
                {
                    ValidePedidoParaEstorno(pedido);

                    pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;

                    ServicoParametros servicoParametros = new ServicoParametros();

                    var parametros = servicoParametros.ConsulteParametros();

                    if (parametros.ParametrosVenda.PermiteBaixarEstoqueNaSaida)
                    {
                        if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                        {
                            pedido = Consulte(pedido.Id);
                            pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;

                            var itemReserva = VerificaEstoqueReservadoNegativo(pedido);

                            if (itemReserva.PrimeiraResposta)
                                throw new Exception("Não é possível estornar o pedido: " + pedido.Id + " pois o(s) item(s): " + itemReserva.PrimeiroConteudo + "foi(ram) baixado(s), portanto, não tem estoque reservado");
                            else
                            {
                                Atualize(pedido);
                                RemovaReservaEstoque(pedido);
                            }
                        }
                        else
                        {
                            pedido = Consulte(pedido.Id); 
                            pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;
                            Atualize(pedido); 
                        }

                        Atualize(pedido);
                        scope.Complete();
                    }
                    else
                    {
                        if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                        {
                            var itemReserva = VerificaEstoqueReservadoNegativo(pedido);

                            if (itemReserva.PrimeiraResposta)
                                throw new Exception("Não é possível estornar o pedido: " + pedido.Id + " pois o(s) item(s): " + itemReserva.PrimeiroConteudo + "foi(ram) baixado(s), portanto, não tem estoque reservado");
                            else
                            {
                                pedido = Consulte(pedido.Id);
                                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;
                                Atualize(pedido);
                                RemovaReservaEstoque(pedido); 
                            }
                        }
                        else
                        {   
                            pedido = Consulte(pedido.Id);
                            pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;
                            Atualize(pedido);
                            AtualizeDiretoNoEstoque(pedido, false);                                                        
                        }

                        Atualize(pedido);
                        scope.Complete();
                    }                    
                }                
            }
        }
        
        public void RemoveReservaListaDeItens(PedidoDeVenda pedidoVenda)
        {
            var repositorioProdutos = FabricaDeRepositorios.Crie<IRepositorioProduto>();
            List<Produto> listaProdutosBaixados = new List<Produto>();

            foreach (var item in pedidoVenda.ListaItens)
            {
                var produto = repositorioProdutos.Consulte(item.Produto.Id);

                produto.FormacaoPreco.EstoqueReservado -= item.Quantidade;

                listaProdutosBaixados.Add(produto);
            }

            if (listaProdutosBaixados.Count > 0)
            {
                repositorioProdutos.AtualizeLista(listaProdutosBaixados);
            }
        }

        public void VoltePedidoNFRejeitadoParaReservado(int pedidoDeVendaId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var pedido = Consulte(pedidoDeVendaId);

                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.FATURADO;

                ValidePedidoParaEstorno(pedido);

                pedido.StatusPedidoVenda = EnumStatusPedidoDeVenda.RESERVADO;

                ServicoParametros servicoParametros = new ServicoParametros();

                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.PermiteBaixarEstoqueNaSaida)
                {
                    if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    {
                        var itemReserva = VerificaEstoqueReservadoNegativo(pedido);

                        if (itemReserva.PrimeiraResposta)
                            throw new Exception("Não é possível estornar o pedido: " + pedido.Id + " pois o(s) item(s): " + itemReserva.PrimeiroConteudo + "foi(ram) baixado(s), portanto, não tem estoque reservado");
                        else
                            RemovaReservaEstoque(pedido);
                    }
                    else
                        AtualizeDiretoNoEstoque(pedido);
                }
                else
                {
                    if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    {
                        var itemReserva = VerificaEstoqueReservadoNegativo(pedido);

                        if (itemReserva.PrimeiraResposta)
                            throw new Exception("Não é possível estornar o pedido: " + pedido.Id + " pois o(s) item(s): " + itemReserva.PrimeiroConteudo + "foi(ram) baixado(s), portanto, não tem estoque reservado");
                        else
                            RemovaReservaEstoque(pedido);
                    }
                    else
                        AtualizeDiretoNoEstoque(pedido);                    
                }

                Atualize(pedido);

                scope.Complete();
            }
        }

        private void AtualizeDiretoNoEstoque(PedidoDeVenda pedido, bool validarEstoqueNegativo=true)
        {   
            var itemEstoque = VerificaEstoqueNegativo(pedido);

            if (itemEstoque.SegundaResposta && validarEstoqueNegativo)
                throw new Exception("Não é possível estornar o pedido: " + pedido.Id + " pois o(s) item(s): " + itemEstoque.PrimeiroConteudo + " não tem estoque");
            else
                EstorneProdutosParaEstoque(pedido);
            //ReserveEstoque(pedido);
        }
        #endregion

        #region " CÁLCULOS "

        public static double CalculePrecoUnitarioProduto(TabelaPreco tabelaPreco, Produto produto, bool pesquisarTabelaPreco = true, bool pesquisarProduto = true)
        {
            if (pesquisarTabelaPreco)
            {
                ServicoTabelaPreco servicoTabelaPreco = new ServicoTabelaPreco(false, false);
                tabelaPreco = servicoTabelaPreco.Consulte(tabelaPreco.Id);
            }

            if (pesquisarProduto)
            {
                ServicoProduto servicoProduto = new ServicoProduto(false, false);
                produto = servicoProduto.Consulte(produto.Id);
            }

            return CalculosPedidoDeVenda.CalculePrecoUnitarioProduto(tabelaPreco, produto);
        }

        #endregion

        #region " VALIDAÇÕES "

        public void ValidePedidoDeVenda(PedidoDeVenda pedidoDeVenda)
        {
            PreenchaFormasPagamentosDaBase(pedidoDeVenda);

            ValidacaoPedidoDeVenda validacaoPedidoDeVenda = new ValidacaoPedidoDeVenda();

            if (pedidoDeVenda.Id == 0)
            {
                validacaoPedidoDeVenda.ValideInclusao();
            }
            else
            {
                validacaoPedidoDeVenda.ValideAtualizacao();
            }

            validacaoPedidoDeVenda.Valide(pedidoDeVenda).AssegureSucesso();
        }

        public void ValidePedidoParaReserva(PedidoDeVenda pedidoDeVenda)
        {
            PreenchaFormasPagamentosDaBase(pedidoDeVenda);

            ValidacaoPedidoDeVenda validacaoPedidoDeVenda = new ValidacaoPedidoDeVenda();
            validacaoPedidoDeVenda.ValidePedidoParaReserva();
            validacaoPedidoDeVenda.Valide(pedidoDeVenda).AssegureSucesso();
        }

        public void ValideItemPedidoVenda(ItemPedidoDeVenda itemPedidoVenda)
        {
            ValidacaoItemPedidoDeVenda validacaoItemPedidoDeVenda = new ValidacaoItemPedidoDeVenda();

            validacaoItemPedidoDeVenda.ValideInclusao();

            validacaoItemPedidoDeVenda.Valide(itemPedidoVenda).AssegureSucesso();
        }

        public void ValideItemPedidoVendaLiberacao(ItemPedidoDeVenda itemPedidoVenda)
        {
            ValidacaoItemPedidoDeVenda validacaoItemPedidoDeVenda = new ValidacaoItemPedidoDeVenda();

            validacaoItemPedidoDeVenda.ValideItemLiberacao();

            validacaoItemPedidoDeVenda.Valide(itemPedidoVenda).AssegureSucesso();
        }

        public bool VerifiqueItemQuantidadeEstoqueNegativo(double quantidade, Produto produto, bool reserva)
        {
            ServicoParametros servicoParametros = new ServicoParametros();
            var parametros = servicoParametros.ConsulteParametros();
            double quantidadesubestoque = 0;

            if (produto == null) return false;

            ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
            var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(produto.Id);

            if (ItemTransferencia != null)
            {
                foreach (var itemproduto in ItemTransferencia)
                {
                    quantidadesubestoque += itemproduto.QuantidadeEstoque;
                }

            }

            if (reserva == true)
            {
                
                if (produto.FormacaoPreco.Estoque  - quantidadesubestoque - produto.FormacaoPreco.EstoqueReservado - quantidade < 0 && parametros.ParametrosVenda.NaoAceitarEstoqueNegativo)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (produto.FormacaoPreco.Estoque - quantidadesubestoque - quantidade < 0 && parametros.ParametrosVenda.NaoAceitarEstoqueNegativo)
                {
                    return true;
                }
            }
            
            return false;
        }

        public TipoMensagem VerificaEstoqueNegativo(PedidoDeVenda pedidoDeVenda)
        {
            TipoMensagem tipoMensagem = new TipoMensagem();

            tipoMensagem.PrimeiraResposta = false;
            tipoMensagem.SegundaResposta = false;
            tipoMensagem.PrimeiroConteudo = string.Empty;
            tipoMensagem.SegundoConteudo = string.Empty;

            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if (item != null && item.Produto != null)
                {
                    var produto = repositorioProduto.Consulte(item.Produto.Id);

                    var quantidadeTotalItem = pedidoDeVenda.ListaItens.Sum(x => x.Produto.Id == item.Produto.Id? x.Quantidade:0);

                    if ((produto.FormacaoPreco.Estoque - produto.FormacaoPreco.EstoqueReservado -  quantidadeTotalItem) < 0)
                        tipoMensagem.PrimeiroConteudo += item.Produto.Id + "; ";

                    if (produto.Principal.QuantidadeMinima != null)
                        if (produto.FormacaoPreco.Estoque - item.Quantidade <= produto.Principal.QuantidadeMinima)
                            tipoMensagem.SegundoConteudo += item.Produto.Id + "; ";
                }
            }

            if (tipoMensagem.PrimeiroConteudo != string.Empty)
            {
                ServicoParametros servicoParametros = new ServicoParametros();
                var parametros = servicoParametros.ConsulteParametros();

                if(!parametros.ParametrosVenda.NaoAceitarEstoqueNegativo)
                    tipoMensagem.PrimeiraResposta = true;
                else
                    tipoMensagem.SegundaResposta = true;
            }

            if (tipoMensagem.SegundoConteudo != string.Empty)
                tipoMensagem.TerceiraResposta = true;
            
            return tipoMensagem;
        }

        public TipoMensagem VerificaEstoqueReservadoNegativo(PedidoDeVenda pedidoDeVenda)
        {
            TipoMensagem tipoMensagem = new TipoMensagem();

            tipoMensagem.PrimeiraResposta = false;
            tipoMensagem.SegundaResposta = false;
            tipoMensagem.PrimeiroConteudo = string.Empty;
           
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if (item != null && item.Produto != null)
                {
                    var produto = repositorioProduto.Consulte(item.Produto.Id);

                    var quantidadeTotalItem = pedidoDeVenda.ListaItens.Sum(x => x.Produto.Id == item.Produto.Id ? x.Quantidade : 0);

                    //if ((produto.FormacaoPreco.EstoqueReservado - quantidadeTotalItem) < 0)
                    //    tipoMensagem.PrimeiroConteudo += item.Produto.Id + "; ";
                }
            }

            if (tipoMensagem.PrimeiroConteudo != string.Empty)
            {
                ServicoParametros servicoParametros = new ServicoParametros();
                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.NaoAceitarEstoqueNegativo)
                    tipoMensagem.PrimeiraResposta = true;              
            }
            
            return tipoMensagem;
        }

        #endregion

        #region " MÉTODOS PRIVADOS E PROTEGIDOS "

        private void ReserveEstoque(PedidoDeVenda pedidoDeVenda)
        {
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();
            
            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if (item != null && item.Produto != null)
                {
                    var produto = repositorioProduto.Consulte(item.Produto.Id);

                    produto.FormacaoPreco.EstoqueReservado += item.Quantidade;

                    repositorioProduto.Atualize(produto);
                }
            }
        }

        public void RemovaReservaEstoque(PedidoDeVenda pedidoDeVenda)
        {   
            IRepositorioProduto repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();
                
            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if (item != null && item.Produto != null)
                {
                    var produto = repositorioProduto.Consulte(item.Produto.Id);

                    produto.FormacaoPreco.EstoqueReservado -= item.Quantidade;
                    if (produto.FormacaoPreco.EstoqueReservado <= 0)
                    {
                        produto.FormacaoPreco.EstoqueReservado = 0;
                    }

                    repositorioProduto.Atualize(produto);
                }                   
            }
        }
        
        public void InativeContasAReceberDoPedido(PedidoDeVenda pedidoDeVenda)
        {
            ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber(false, false);

            foreach (var parcela in pedidoDeVenda.ListaParcelasPedidoDeVenda)
            {
                if (parcela.ContaPagarReceber != null)
                {
                    var contapagarReceber = servicoContasPagarReceber.Consulte(parcela.ContaPagarReceber.Id);

                    if (contapagarReceber.Status != Negocio.Financeiro.Enumeradores.EnumStatusContaPagarReceber.INATIVO)
                    {
                        servicoContasPagarReceber.InativeContaPagarReceber(contapagarReceber, false);
                    }
                }
            }
        }

        private void GereSaidaDeCaixaDoPedido(PedidoDeVenda pedidoDeVenda)
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa(false, false);
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa(false, false);
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa(false, false);
            var listaItensMovimentacoesCaixa = servicoItemMovimentacaoCaixa.ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoCaixa.PEDIDODEVENDA, pedidoDeVenda.Id);

            foreach (var itemMovimentacao in listaItensMovimentacoesCaixa)
            {
                ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();
                itemMovimentacaoCaixa.DataHora = DateTime.Now;
                itemMovimentacaoCaixa.FormaPagamento = itemMovimentacao.FormaPagamento;
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "Pedido Cancelado: nº " + pedidoDeVenda.Id;
                itemMovimentacaoCaixa.ItemDeEntrada = false;
                itemMovimentacaoCaixa.NumeroDocumentoOrigem = pedidoDeVenda.Id;
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.PEDIDODEVENDA;
                itemMovimentacaoCaixa.Parceiro = itemMovimentacao.Parceiro;
                itemMovimentacaoCaixa.TipoMovimentacao = EnumTipoMovimentacaoCaixa.SAIDASANGRIA;
                itemMovimentacaoCaixa.Valor = itemMovimentacao.Valor;

                itemMovimentacaoCaixa.MovimentacaoCaixa = movimentacaoCaixa;

                servicoItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);
            }
        }

        private void InsiraNaturezaOperacaoParaVenda(PedidoDeVenda pedidoVenda)
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
            ServicoNaturezaOperacao servicoNaturezaOperacao = new ServicoNaturezaOperacao(false, false);

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            if (!pedidoVenda.EnderecoPedidoDeVenda.ClienteResideExterior)
            {
                if (pedidoVenda.EnderecoPedidoDeVenda.Cidade.Estado.UF == empresa.DadosEmpresa.Endereco.Cidade.Estado.UF)
                {
                    pedidoVenda.NaturezaOperacao = servicoNaturezaOperacao.ConsulteNaturezaOperacaoPorCfop("5102");
                }
                else
                {
                    pedidoVenda.NaturezaOperacao = servicoNaturezaOperacao.ConsulteNaturezaOperacaoPorCfop("6102");
                }
            }
            else
                pedidoVenda.NaturezaOperacao = servicoNaturezaOperacao.ConsulteNaturezaOperacaoPorCfop("7100");
        }

        public void EstorneProdutosParaEstoque(PedidoDeVenda pedidoVenda)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = pedidoVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.VENDA;

            movimentacaoBase.Observacoes = "Estornado Nr. Pedido: " + pedidoVenda.Id;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;
            
            foreach (var item in pedidoVenda.ListaItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.Quantidade;
                itemMovimentacao.PedidoVenda_Id = pedidoVenda.Id;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        public void EstorneItemDeEstoque(PedidoDeVenda pedidoVenda, ItemPedidoDeVenda item)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            //Vai atualizar para estornar a saída desta entrada
            var listaMov = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(pedidoVenda.Id);

            //foreach (var itemMov in listaMov)
            //{
            //    if (itemMov.TipoMovimentacao == EnumTipoMovimentacao.SAIDA && itemMov.Produto.Id == item.Produto.Id)
            //    {
            //        var movbase = servicoMovimentacao.Consulte(itemMov.MovimentacaoBase.Id);

            //        foreach (var iteMovBase in movbase.ListaDeItens)
            //        {
            //            if(iteMovBase.Id == itemMov.Id)
            //            {
            //                iteMovBase.EstahEstornado = 1;                            
            //            }
            //        }

            //        servicoMovimentacao.Atualize(movbase);
            //    }
            //}

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = pedidoVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.ESTORNOSAIDA;

            movimentacaoBase.Observacoes = "Estorno individual do item: " + item.Produto.Id + " do Pedido Nr: " + pedidoVenda.Id; 
                
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;

            ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

            itemMovimentacao.Produto = item.Produto;
            itemMovimentacao.Quantidade = item.Quantidade;
            itemMovimentacao.PedidoVenda_Id = pedidoVenda.Id;

            movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            
            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        private void ValidePedidoParaEstorno(PedidoDeVenda pedidoVenda)
        {
            ValidacaoPedidoDeVenda validacaoPedidoDeVenda = new ValidacaoPedidoDeVenda();
            validacaoPedidoDeVenda.ValidePedidoEstorno();
            validacaoPedidoDeVenda.Valide(pedidoVenda).AssegureSucesso();
        }

        public void ValidePedidoParaEstornoAoCancelarRecebimentos(PedidoDeVenda pedidoVenda)
        {
            ValidacaoPedidoDeVenda validacaoPedidoDeVenda = new ValidacaoPedidoDeVenda();
            validacaoPedidoDeVenda.ValidePedidoAoCancelarRecebimento();
            validacaoPedidoDeVenda.Valide(pedidoVenda).AssegureSucesso();
        }

        private void PreenchaFormasPagamentosDaBase(PedidoDeVenda pedidoVenda)
        {
            ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento(false, false);
            var listaFormasPagamento = servicoFormaPagamento.ConsulteListaAtivos();
        }

        #endregion
    }
}
