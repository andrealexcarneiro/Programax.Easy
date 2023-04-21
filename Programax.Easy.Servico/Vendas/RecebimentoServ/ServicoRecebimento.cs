using System;
using System.Collections.Generic;
using System.Transactions;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.RecebimentoObj.Repositorio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Linq;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Vendas.RecebimentoServ
{
    [Funcionalidade(EnumFuncionalidade.RECEBIMENTO)]
    public class ServicoRecebimento : ServicoAkilSmallBusiness<Recebimento, ValidacaoRecebimento, ConversorRecebimento>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioRecebimento _repositorioRecebimento;
        private List<int> _idCadastradosContaReceber;

        #endregion

        #region " CONSTRUTOR "

        public ServicoRecebimento()
        {
            RetorneRepositorio();
        }

        public ServicoRecebimento(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }


        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void FatureRecebimento(Recebimento recebimento, double cashback, double pix, double dinheiro, double cartaoDebito, double cartaoCredito, double cheque, List<Cheque> listaDeCheques=null)
        {
            if (recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
            {
                FaturePedidoDeVenda(recebimento, cashback,pix,dinheiro, cartaoDebito, cartaoCredito, cheque, listaDeCheques);
            }
            else if (recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.TROCAPEDIDODEVENDAS)
            {
                FatureTrocaPedidoDeVenda(recebimento, cashback, pix, dinheiro, cartaoDebito, cartaoCredito, cheque);
            }
        }

        public void CadastreEFataurePedidoDeVenda(PedidoDeVenda pedidoDeVenda)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { Timeout = new TimeSpan(0, 5, 0) }))
            {
                pedidoDeVenda.DataFechamento = DateTime.Now;
                pedidoDeVenda.StatusPedidoVenda = EnumStatusPedidoDeVenda.FATURADO;

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda(false, false);
                servicoPedidoDeVenda.FechePedidoDeVenda(pedidoDeVenda);

                var pedidoRecebimento = Consulte(pedidoDeVenda.Id);

                double totalCashBack = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CASHBACK ? parcela.Valor : 1), 2);
                double totalPix = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.PIX ? parcela.Valor : 0), 2);
                double totalDinheiro = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ? parcela.Valor : 0), 2);
                double totalCheque = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ? parcela.Valor : 0), 2);
                double totalCartaoDebito = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO ? parcela.Valor : 0), 2);
                double totalCartaoCredito = Math.Round(pedidoRecebimento.ListaParcelasRecebimento.Sum(parcela => parcela.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ? parcela.Valor : 0), 2);

                FaturePedidoDeVenda(pedidoRecebimento, totalCashBack, totalPix,totalDinheiro, totalCartaoDebito, totalCartaoCredito, totalCheque);

                scope.Complete();
            }
        }

        #endregion

        #region " FATURE PEDIDO DE VENDA "
        //Andre
        private void FaturePedidoDeVenda(Recebimento recebimento, double cashback, double pix,double dinheiro, double cartaoDebito, double cartaoCredito, double cheque, List<Cheque> listaDeCheques=null)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var repositorioPedidoVendas = FabricaDeRepositorios.Crie<IRepositorioPedidoDeVenda>();

                var pedidoVenda = repositorioPedidoVendas.Consulte(recebimento.Id);

                pedidoVenda.StatusPedidoVenda = EnumStatusPedidoDeVenda.FATURADO;

                //Vai determinar se o pedido está pago ou não.
                pedidoVenda.EstahPago = true;
                pedidoVenda.DataFechamento = DateTime.Now;

                //Caso o usuário marcar "Permite Baixar Estoque na Saída" não será possível
                //dar baixa (Saída) no estoque por aqui - Terá que ser Feito na tela de baixa de estoque
                ServicoParametros servicoParametros = new ServicoParametros(false);

                var parametros = servicoParametros.ConsulteParametros();

                if (parametros.ParametrosVenda.PermiteBaixarEstoqueNaSaida)
                {
                    if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    {
                        if (parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido)
                            ReserveEstoque(pedidoVenda);
                    }
                }
                else
                {
                    if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    {
                        if (parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido)
                            MovimenteEstoquePedidoVenda(pedidoVenda);
                        else
                            MovimenteEstoquePedidoVenda(pedidoVenda, true);
                    }
                    else
                        MovimenteEstoquePedidoVenda(pedidoVenda);
                }

                GereContasReceberPedidoVendaEInsiraNumeroDocumento(pedidoVenda, recebimento);

                //Vai gera o contas a receber conforme o cadastro de cheques
                if (listaDeCheques != null)
                {
                    if (listaDeCheques.Count != 0)
                    {
                        GereContasReceberParaCheques(listaDeCheques, pedidoVenda.FormaPagamento, recebimento);
                    }
                }

                GereEntradaNoCaixa(recebimento, cashback,pix, dinheiro, cartaoDebito, cartaoCredito, cheque);

                repositorioPedidoVendas.Atualize(pedidoVenda);

                scope.Complete();
            }

            //*****Se o recebimento da operadora do cartão for antecipado
            if (_idCadastradosContaReceber.Count > 0)
            {
                ServicoContasReceber servicoContasReceber = new ServicoContasReceber();

                foreach (var itemId in _idCadastradosContaReceber)
                {
                    var pagarReceber = servicoContasReceber.Consulte(itemId);

                    //Primeiro, vai quitar o contas a receber
                    pagarReceber.Status = EnumStatusContaPagarReceber.QUITADO;
                    pagarReceber.DataPagamento = DateTime.Now;
                    pagarReceber.ValorPago = pagarReceber.ValorTotal;

                    servicoContasReceber.Atualize(pagarReceber);

                    ServicoItemMovimentacaoBanco servicoMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

                    //Segundo, vai gerar a taxa no banco
                    servicoMovimentacaoBanco.CalculeDespesasCartoes(pagarReceber, false,
                                              EnumTipoOperacaoContasPagarReceber.PAGAR, pagarReceber.DataPagamento.Value,
                                              pagarReceber.OperadorasCartao.BancoParaMovimento,
                                              pagarReceber.OperadorasCartao,
                                              pagarReceber.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });

                    //No PDV não informa Banco. Então, vai o mesmo banco da operadora.
                    if (pagarReceber.BancoParaMovimento == null)
                        pagarReceber.BancoParaMovimento = pagarReceber.OperadorasCartao.BancoParaMovimento;

                    //Terceiro, vai gerar o pagamento no banco
                    servicoMovimentacaoBanco.InsiraMovimentacaoBancaria(pagarReceber, false, EnumTipoOperacaoContasPagarReceber.RECEBER,
                        pagarReceber.DataPagamento.Value, pagarReceber.OperadorasCartao.CategoriaDeDespesa, 
                        pagarReceber.BancoParaMovimento, pagarReceber.ValorPago, new Pessoa { Id = Sessao.PessoaLogada.Id });
                }
            }

        }

        private void MovimenteEstoquePedidoVenda(PedidoDeVenda pedidoVenda, bool removeApenasReserva = false)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = pedidoVenda.Cliente;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.VENDA;

            movimentacaoBase.Observacoes = "Nr. Pedido: " + pedidoVenda.Id;
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;

            var repositorioProdutos = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            List<Produto> listaProdutosBaixados = new List<Produto>();

            foreach (var item in pedidoVenda.ListaItens)
            {
                var produto = repositorioProdutos.Consulte(item.Produto.Id);
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                //item.Quantidade-> será a quantidade que será abatida no total
                itemMovimentacao.Quantidade = item.Quantidade;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);

                if(removeApenasReserva)
                    produto.FormacaoPreco.EstoqueReservado -= item.Quantidade;

                listaProdutosBaixados.Add(produto);
            }

            if (listaProdutosBaixados.Count > 0)
            {
                servicoMovimentacao.Cadastre(movimentacaoBase);

                repositorioProdutos.AtualizeLista(listaProdutosBaixados);
            }   
        }

        public void MovimenteEstoquePorItem(List<ItemPedidoDeVenda> ListaItens)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = DateTime.Now;
            movimentacaoBase.FornecedorOuCliente = null;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.VENDA;
            
            movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;

            var repositorioProdutos = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            List<Produto> listaProdutosBaixados = new List<Produto>();

            foreach (var item in ListaItens)
            {
                var produto = repositorioProdutos.Consulte(item.Produto.Id);

                ListeProdutosBaixadosEMovimente(produto, movimentacaoBase, item, listaProdutosBaixados);
            }

            if (listaProdutosBaixados.Count > 0)
            {
                servicoMovimentacao.Cadastre(movimentacaoBase);

                repositorioProdutos.AtualizeLista(listaProdutosBaixados);
            }
        }

        private void ListeProdutosBaixadosEMovimente(Produto produto, MovimentacaoBase movimentacaoBase, ItemPedidoDeVenda item, List<Produto> listaProdutosBaixados)
        {
            ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

            itemMovimentacao.Produto = item.Produto;
            itemMovimentacao.Quantidade = item.Quantidade;
            itemMovimentacao.PedidoVenda_Id = item.PedidoDeVenda != null? item.PedidoDeVenda.Id:0;

            movimentacaoBase.Observacoes = "Baixa na Saída de Mercadoria. Pedido N.: " + itemMovimentacao.PedidoVenda_Id
                + " Quem Pegou? " + " Data/Hora: " + DateTime.Now + item.Observacao;

            movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            
            ServicoParametros servicoParametros = new ServicoParametros(false);

            var parametros = servicoParametros.ConsulteParametros();

            if (parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {
                produto.FormacaoPreco.EstoqueReservado -= item.Quantidade;
            }
            
            listaProdutosBaixados.Add(produto);
        }

        private void GereContasReceberPedidoVendaEInsiraNumeroDocumento(PedidoDeVenda pedidoVenda, Recebimento recebimento)
        {
            ServicoContasReceber servicoContasReceber = new ServicoContasReceber(false, false);

            _idCadastradosContaReceber = new List<int>();

            foreach (var parcela in pedidoVenda.ListaParcelasPedidoDeVenda)
            {
                if (parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
                    (EnumTipoFormaPagamento)parcela.FormaPagamento.Id == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ||
                    (EnumTipoFormaPagamento)parcela.FormaPagamento.Id == EnumTipoFormaPagamento.DUPLICATA ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.PIX ||
                    parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CASHBACK ||
                    (parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO &&
                    parcela.DataVencimento > DateTime.Now.Date))
                {
                    ServicoParametros servicoParametros = new ServicoParametros(false);
                    var parametros = servicoParametros.ConsulteParametros();

                    double multa = 0;
                    double juros = 0;

                    if (parametros.ParametrosFinanceiro != null)
                    {
                        multa = parametros.ParametrosFinanceiro.MultaContasReceber;
                        juros = parametros.ParametrosFinanceiro.JurosContasReceber;
                    }

                    OperadorasCartao operadora = new OperadorasCartao();

                    if (parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                        parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        operadora = recebimento.ListaParcelasRecebimento.FirstOrDefault(x => x.NumeroDocumento == parcela.NumeroDocumento).OperadorasCartao;
                    }

                    //Se a operadora estiver com valor zero, o sistema irá acrescentar nulo para não ocorrer erros ao salvar
                    if (operadora != null)
                        if (operadora.Id == 0) operadora = null;

                    if (parcela.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO && parcela.DataVencimento <= DateTime.Now.Date)
                    {
                        if (parcela.CondicaoPagamento != null)
                            parcela.DataVencimento = parcela.CondicaoPagamento.ListaDeParcelas != null ? DateTime.Now.Date.AddDays(parcela.CondicaoPagamento.ListaDeParcelas.First().Dias) : parcela.DataVencimento;
                    }

                    var listaContasAReceber = servicoContasReceber.GereContasPagarReceber(pedidoVenda.Cliente,
                                                                                                                               DateTime.Now.Date,
                                                                                                                               parcela.DataVencimento,
                                                                                                                               pedidoVenda.Id + " - " + parcela.Parcela,
                                                                                                                               EnumOrigemDocumento.PEDIDODEVENDAS,
                                                                                                                               parcela.FormaPagamento,
                                                                                                                               null, recebimento.BancoParaMovimento,
                                                                                                                               recebimento.CategoriaFinanceira,
                                                                                                                               operadora,
                                                                                                                               "Nr. Pedido " + pedidoVenda.Id + " - parcela: " + parcela.Parcela,
                                                                                                                               Sessao.PessoaLogada,
                                                                                                                               EnumPeriodicidade.UNICA,
                                                                                                                               parcela.Valor,
                                                                                                                               multa,
                                                                                                                               juros,
                                                                                                                               true,
                                                                                                                               true,
                                                                                                                               1,
                                                                                                                               0);

                    listaContasAReceber[0].NumeroDocumento = listaContasAReceber[0].NumeroDocumento.Remove(0, 2);


                    parcela.NumeroDocumento = listaContasAReceber[0].NumeroDocumento;
                    parcela.ContaPagarReceber = listaContasAReceber[0];

                    //**Guarda o id da condição de pagto no contas a receber para gerar o valor a cobrar das operadoras***
                    listaContasAReceber[0].CondicaoPgtoId = parcela.CondicaoPagamento.Id;

                    listaContasAReceber[0].EhRecebimento = true;
                    listaContasAReceber[0].DataPedidoElaboracao = parcela.PedidoDeVenda.DataElaboracao;
                    servicoContasReceber.CadastreLista(listaContasAReceber);

                    //*****Se o recebimento da operadora do cartão for antecipado (Vai guardar os ids para serem processados)
                    if (parcela.Operadoras != null)
                    {
                        if (parcela.Operadoras.RecebimentoAntecipado)
                        {   
                            _idCadastradosContaReceber.Add(listaContasAReceber[0].Id);
                        }   
                    }
                }
            }
        }

        private void GereContasReceberParaCheques(List<Cheque> cheques, FormaPagamento formaPagamento, Recebimento recebimento)
        {
            ServicoContasReceber servicoContasReceber = new ServicoContasReceber(false, false);
            int numeroParcela = 1;

            foreach (var parcela in cheques)
            {   
                ServicoParametros servicoParametros = new ServicoParametros(false);
                var parametros = servicoParametros.ConsulteParametros();

                
                double multa = 0;
                double juros = 0;

                if (parametros.ParametrosFinanceiro != null)
                {
                    multa = parametros.ParametrosFinanceiro.MultaContasReceber;
                    juros = parametros.ParametrosFinanceiro.JurosContasReceber;
                }

                var listaContasAReceber = servicoContasReceber.GereContasPagarReceber(parcela.Pessoa,
                                                                                                                            DateTime.Now.Date,
                                                                                                                            parcela.DataVencimento.GetValueOrDefault(),
                                                                                                                            parcela.NumeroPedidoVenda + " - " + numeroParcela++,
                                                                                                                            EnumOrigemDocumento.PEDIDODEVENDAS,
                                                                                                                            new FormaPagamento {Id = formaPagamento.Id, TipoFormaPagamento = EnumTipoFormaPagamento.CHEQUE},
                                                                                                                            null, 
                                                                                                                            recebimento.BancoParaMovimento, 
                                                                                                                            recebimento.CategoriaFinanceira, 
                                                                                                                            null,
                                                                                                                            "Nr. Pedido " + parcela.NumeroPedidoVenda + " - parcela: " + numeroParcela,
                                                                                                                            Sessao.PessoaLogada,
                                                                                                                            EnumPeriodicidade.UNICA,
                                                                                                                            parcela.ValorCheque,
                                                                                                                            multa,
                                                                                                                            juros,
                                                                                                                            true,
                                                                                                                            true,
                                                                                                                            1,
                                                                                                                            0,
                                                                                                                            parcela.Id);

                listaContasAReceber[0].NumeroDocumento = listaContasAReceber[0].NumeroDocumento.Remove(0, 2);

                //parcela.NumeroDocumento = listaContasAReceber[0].NumeroDocumento;
                //parcela.ContaPagarReceber = listaContasAReceber[0];

                servicoContasReceber.CadastreLista(listaContasAReceber);
            }            
        }

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

        #endregion

        #region " FATURE TROCA PEDIDO DE VENDA "

        private void FatureTrocaPedidoDeVenda(Recebimento recebimento, double cashback, double pix, double dinheiro, double cartaoDebito, double cartaoCredito, double cheque)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                ServicoTrocaPedidoDeVenda servicoTrocaPedidoDeVenda = new ServicoTrocaPedidoDeVenda(false, false);
                var troca = servicoTrocaPedidoDeVenda.Consulte(recebimento.Id);

                servicoTrocaPedidoDeVenda.FatureTroca(troca);

                GereContasPagarReceberTroca(troca, recebimento);
                GereEntradaNoCaixa(recebimento, cashback, pix, dinheiro, cartaoDebito, cartaoCredito, cheque);

                scope.Complete();
            }
        }

        private void GereContasPagarReceberTroca(TrocaPedidoDeVenda trocaPedidoDeVenda, Recebimento recebimento)
        {
            ServicoContasPagarReceber servicoContasPagarReceber = null;

            if (trocaPedidoDeVenda.ValorTotalTroca >= 0)
            {
                servicoContasPagarReceber = new ServicoContasReceber(false, false);
            }
            else
            {
                servicoContasPagarReceber = new ServicoContasPagar(false, false);
            }

            if (trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
               trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
               trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ||
               trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA ||
               trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO)
            {
                ServicoParametros servicoParametros = new ServicoParametros(false);
                var parametros = servicoParametros.ConsulteParametros();

                double multa = 0;
                double juros = 0;

                if (parametros.ParametrosFinanceiro != null)
                {
                    multa = parametros.ParametrosFinanceiro.MultaContasReceber;
                    juros = parametros.ParametrosFinanceiro.JurosContasReceber;
                }

                OperadorasCartao operadora = new OperadorasCartao();

                if (trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                    trocaPedidoDeVenda.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO)
                {
                    operadora = recebimento.ListaParcelasRecebimento.FirstOrDefault(x => x.NumeroDocumento == trocaPedidoDeVenda.NumeroDocumento).OperadorasCartao;
                }

                var listaContasAReceber = servicoContasPagarReceber.GereContasPagarReceber(trocaPedidoDeVenda.PedidoDeVenda.Cliente,
                                                                                                                               DateTime.Now.Date,
                                                                                                                               trocaPedidoDeVenda.DataVencimento,
                                                                                                                               trocaPedidoDeVenda.NumeroDocumento,
                                                                                                                               EnumOrigemDocumento.PEDIDODEVENDAS,
                                                                                                                               trocaPedidoDeVenda.FormaPagamento,
                                                                                                                               null, 
                                                                                                                               recebimento.BancoParaMovimento, 
                                                                                                                               recebimento.CategoriaFinanceira,
                                                                                                                               operadora,
                                                                                                                               "Nr. Troca " + trocaPedidoDeVenda.Id,
                                                                                                                               Sessao.PessoaLogada,
                                                                                                                               EnumPeriodicidade.UNICA,
                                                                                                                               Math.Abs(trocaPedidoDeVenda.ValorTotalTroca),
                                                                                                                               multa,
                                                                                                                               juros,
                                                                                                                               true,
                                                                                                                               true,
                                                                                                                               1,
                                                                                                                               0);

                listaContasAReceber[0].NumeroDocumento = listaContasAReceber[0].NumeroDocumento.Remove(0, 2);

                servicoContasPagarReceber.CadastreLista(listaContasAReceber);
            }
        }

        #endregion

        #region " GERE MOVIMENTO CAIXA "

        private void GereEntradaNoCaixa(Recebimento recebimento, double cashback, double pix,double dinheiro, double cartaoDebito, double cartaoCredito, double cheque)
        {
            var repositorioCaixa = FabricaDeRepositorios.Crie<IRepositorioCaixa>();
            var repositorioMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();

            var caixa = repositorioCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);
            var movimentacaoCaixa = repositorioMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            CadastreItemMovimentacaoCaixa(recebimento, cashback, EnumTipoFormaPagamento.CASHBACK, movimentacaoCaixa);
            CadastreItemMovimentacaoCaixa(recebimento, dinheiro, EnumTipoFormaPagamento.DINHEIRO, movimentacaoCaixa);
            CadastreItemMovimentacaoCaixa(recebimento, pix, EnumTipoFormaPagamento.PIX, movimentacaoCaixa);
            CadastreItemMovimentacaoCaixa(recebimento, cartaoDebito, EnumTipoFormaPagamento.CARTAODEBITO, movimentacaoCaixa);
            CadastreItemMovimentacaoCaixa(recebimento, cartaoCredito, EnumTipoFormaPagamento.CARTAOCREDITO, movimentacaoCaixa);
            CadastreItemMovimentacaoCaixa(recebimento, cheque, EnumTipoFormaPagamento.CHEQUE, movimentacaoCaixa);
            
        }

        private void CadastreItemMovimentacaoCaixa(Recebimento recebimento,  double valor, EnumTipoFormaPagamento tipoFormaPagamento, MovimentacaoCaixa movimentacaoCaixa)
        {
            if (movimentacaoCaixa.Id == 0 || movimentacaoCaixa.Id == null)
            {
                return;
            }

            if (valor == 0)
            {
                return;
            }

            var repositorioItemMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoCaixa>();
            var repositorioFormaPagamento = FabricaDeRepositorios.Crie<IRepositorioFormaPagamento>();

            FormaPagamento formaPagamento = repositorioFormaPagamento.ConsultePeloTipo(tipoFormaPagamento);

            ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();
            itemMovimentacaoCaixa.DataHora = DateTime.Now;

            itemMovimentacaoCaixa.DataHora = DateTime.Now;
            itemMovimentacaoCaixa.FormaPagamento = formaPagamento;

            itemMovimentacaoCaixa.CategoriaFinaceira = recebimento.CategoriaFinanceira != null ? new CategoriaFinanceira { Id = recebimento.CategoriaFinanceira.Id } : null;

            itemMovimentacaoCaixa.MovimentacaoCaixa = movimentacaoCaixa;

            itemMovimentacaoCaixa.Parceiro = new Pessoa { Id = recebimento.ClienteId };
            if (tipoFormaPagamento != EnumTipoFormaPagamento.CASHBACK)
            {
                itemMovimentacaoCaixa.TipoMovimentacao = valor >= 0 ? EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO : EnumTipoMovimentacaoCaixa.SAIDASANGRIA;
            }
            else
            {
                itemMovimentacaoCaixa.TipoMovimentacao =  EnumTipoMovimentacaoCaixa.SAIDASANGRIA;
            }
            
            itemMovimentacaoCaixa.ItemDeEntrada = valor >= 0 ? true : false;
            itemMovimentacaoCaixa.Valor = Math.Abs(valor);
            itemMovimentacaoCaixa.NumeroDocumentoOrigem = recebimento.Id;

            if (recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.PEDIDODEVENDAS)
            {
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.PEDIDODEVENDA;
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "Nr. Pedido " + recebimento.Id;
            }
            else if (recebimento.TipoDocumento == EnumTipoDocumentoRecebimento.TROCAPEDIDODEVENDAS)
            {
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.TROCAPEDIDODEVENDA;
                itemMovimentacaoCaixa.HistoricoMovimentacoes = "Nr. Troca " + recebimento.Id;
            }

            repositorioItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Recebimento> RetorneRepositorio()
        {
            if (_repositorioRecebimento == null)
            {
                _repositorioRecebimento = FabricaDeRepositorios.Crie<IRepositorioRecebimento>();
            }

            return _repositorioRecebimento;
        }

        #endregion

        #region " CONSULTAS "

        public Recebimento Consulte(int id, EnumTipoDocumentoRecebimento tipoDocumentoRecebimento)
        {
            return _repositorioRecebimento.Consulte(id, tipoDocumentoRecebimento);
        }

        public RecebimentoNf ConsulteNf(int id, EnumTipoDocumentoRecebimento tipoDocumentoRecebimento)
        {
            return _repositorioRecebimento.ConsulteNf(id, tipoDocumentoRecebimento);
        }

        public List<Recebimento> ConsulteLista()
        {
            return _repositorioRecebimento.ConsulteLista();
        }

        public List<Recebimento> ConsulteListaPorDataElaboracao(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            return _repositorioRecebimento.ConsulteListaPorDataElaboracao(dataInicial, dataFinal, tipoDocumentoRecebimento);
        }

        public List<Recebimento> ConsulteListaPorDataFechamento(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            return _repositorioRecebimento.ConsulteListaPorDataFechamento(dataInicial, dataFinal, tipoDocumentoRecebimento);
        }

        public List<RecebimentoNf> ConsulteListaPorDataElaboracaoNf(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            return _repositorioRecebimento.ConsulteListaPorDataElaboracaoNf(dataInicial, dataFinal, tipoDocumentoRecebimento);
        }

        public List<RecebimentoNf> ConsulteListaPorDataFechamentoNf(DateTime dataInicial, DateTime dataFinal, EnumTipoDocumentoRecebimento? tipoDocumentoRecebimento)
        {
            return _repositorioRecebimento.ConsulteListaPorDataElaboracaoNf(dataInicial, dataFinal, tipoDocumentoRecebimento);
        }


        #endregion

    }
}
