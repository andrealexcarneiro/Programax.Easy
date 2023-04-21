using System;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Transactions;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ
{
    [Funcionalidade(EnumFuncionalidade.MOVIMENTACAOCAIXA)]
    public class ServicoItemMovimentacaoCaixa : ServicoAkilSmallBusiness<ItemMovimentacaoCaixa, ValidacaoItemMovimentacaoCaixa, ConversorItemMovimentacaoCaixa>
    {
        IRepositorioItemMovimentacaoCaixa _repositorioItemMovimentacaoCaixa;

        public ServicoItemMovimentacaoCaixa()
        {
            RetorneRepositorio();
        }

        public ServicoItemMovimentacaoCaixa(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ItemMovimentacaoCaixa> RetorneRepositorio()
        {
            if (_repositorioItemMovimentacaoCaixa == null)
            {
                _repositorioItemMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoCaixa>();
            }

            return _repositorioItemMovimentacaoCaixa;
        }

        public override int Cadastre(ItemMovimentacaoCaixa objetoDeNegocio)
        {
            objetoDeNegocio.DataHora = DateTime.Now;

            return base.Cadastre(objetoDeNegocio);
        }

        public void EstorneItemMovimentacaoCaixa(ItemMovimentacaoCaixa itemMovimentacaoCaixa)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                //Primeiro caso: Qualquer item que não é pedido de vendas, e não tem documento de origem.

                itemMovimentacaoCaixa.EstahEstornado = true;

                List<ItemMovimentacaoCaixa> listaItensMovimentacaoCaixa = null;

                if (itemMovimentacaoCaixa.NumeroDocumentoOrigem != null)
                {
                    listaItensMovimentacaoCaixa = ConsulteListaItensPorNumeroDocumentoOrigemAtivos(itemMovimentacaoCaixa.OrigemMovimentacaoCaixa, itemMovimentacaoCaixa.NumeroDocumentoOrigem);

                    foreach (var item in listaItensMovimentacaoCaixa)
                    {
                        item.EstahEstornado = true;

                        Atualize(item);
                    }
                }
                else
                {
                    Atualize(itemMovimentacaoCaixa);
                }

                //Segundo caso: Item gerado por um pedido de vendas, ou seja, tem documento de origem.

                if (itemMovimentacaoCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.PEDIDODEVENDA && itemMovimentacaoCaixa.NumeroDocumentoOrigem != null)
                {
                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                    
                    //Se o Pedido tiver NF rejeitada, será estornado!         
                    
                    ServicoNotaFiscal NotaFiscal = new ServicoNotaFiscal();
                    var _NotaFiscal = NotaFiscal.RetorneNotaSoPorPedido(itemMovimentacaoCaixa.NumeroDocumentoOrigem.GetValueOrDefault(),true);

                    if (_NotaFiscal.InformacoesGeraisNotaFiscal.Status == Negocio.Fiscal.Enumeradores.EnumStatusNotaFiscal.REJEITADA)
                    {
                        servicoPedidoDeVenda.VoltePedidoNFRejeitadoParaReservado(itemMovimentacaoCaixa.NumeroDocumentoOrigem.GetValueOrDefault());

                        if (itemMovimentacaoCaixa.NumeroDocumentoOrigem != null)
                        {
                            listaItensMovimentacaoCaixa = ConsulteListaItensPorNumeroDocumentoOrigemAtivos(itemMovimentacaoCaixa.OrigemMovimentacaoCaixa, itemMovimentacaoCaixa.NumeroDocumentoOrigem);

                            foreach (var item in listaItensMovimentacaoCaixa)
                            {
                                item.EstahEstornado = true;

                                Atualize(item);
                            }
                        }
                        else
                        {
                            Atualize(itemMovimentacaoCaixa);
                        }
                    }
                    else
                    {
                        //Se o pedido estiver com nota emitida, e não é rejeitada, não será possível estornar!

                        var statusPedido = servicoPedidoDeVenda.Consulte(itemMovimentacaoCaixa.NumeroDocumentoOrigem.Value);

                        if (statusPedido != null)
                            if (statusPedido.StatusPedidoVenda == Negocio.Vendas.Enumeradores.EnumStatusPedidoDeVenda.EMITIDONFE)
                            {
                                throw new Exception("Não é possível estornar o pedido: " + statusPedido.Id + ". Pois foi emitido a nota fiscal! Para estornar, cancele a nota fiscal");
                            }

                        servicoPedidoDeVenda.VoltePedidoDeVendaParaReservado(itemMovimentacaoCaixa.NumeroDocumentoOrigem.GetValueOrDefault());
                        
                        if (itemMovimentacaoCaixa.NumeroDocumentoOrigem != null)
                        {
                            listaItensMovimentacaoCaixa = ConsulteListaItensPorNumeroDocumentoOrigemAtivos(itemMovimentacaoCaixa.OrigemMovimentacaoCaixa, itemMovimentacaoCaixa.NumeroDocumentoOrigem);

                            foreach (var item in listaItensMovimentacaoCaixa)
                            {
                                item.EstahEstornado = true;

                                Atualize(item);
                            }
                        }
                        else
                        {
                            Atualize(itemMovimentacaoCaixa);
                        }                        
                    }

                }
                //Terceiro caso: item de origem contas a pagar receber, e não tem documento de origem.

                else if ((itemMovimentacaoCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.CONTAPAGAR ||
                            itemMovimentacaoCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.CONTARECEBER) &&
                            itemMovimentacaoCaixa.NumeroDocumentoOrigem != null)
                {
                    ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();
                    var pagamento = servicoContasPagarReceberPagamento.Consulte(itemMovimentacaoCaixa.NumeroDocumentoOrigem.GetValueOrDefault());

                    if (!pagamento.EstahEstornado)
                    {
                        servicoContasPagarReceberPagamento.EstorneRegistro(pagamento);
                    }
                }
                
                //Quarto caso: Troca de pedido de vendas, vai voltar para reservado
                if(itemMovimentacaoCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.TROCAPEDIDODEVENDA)
                {
                    ServicoTrocaPedidoDeVenda servicoTroca = new ServicoTrocaPedidoDeVenda();

                    var troca = servicoTroca.Consulte(itemMovimentacaoCaixa.NumeroDocumentoOrigem.Value);

                    if (troca != null)
                    {
                        servicoTroca.VolteTrocaDeVendaParaReservado(troca.Id);

                        Atualize(itemMovimentacaoCaixa);
                    }                        
                }
                   
                scope.Complete();
            }
        }

        public List<ItemMovimentacaoCaixa> ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoCaixa origemMovimentacaoCaixa, int? numeroDocumentoOrigem)
        {
            return _repositorioItemMovimentacaoCaixa.ConsulteListaItensPorNumeroDocumentoOrigemAtivos(origemMovimentacaoCaixa, numeroDocumentoOrigem);
        }

        public List<ItemMovimentacaoCaixa> ConsulteListaPorCategoriasEPagamentos(int categoriaId, int formaPagamentoId, DateTime? dataInicialPeriodo, 
                                                                                  DateTime? dataFinalPeriodo, Pessoa pessoa=null)
        {
            return _repositorioItemMovimentacaoCaixa.ConsulteListaPorCategoriasEPagamentos(categoriaId, formaPagamentoId, dataInicialPeriodo, dataFinalPeriodo, pessoa);
        }

        public List<ItemMovimentacaoCaixa> ConsulteSaldoItensCaixaMovimento(int movimentoId, int categoriaId)
        {
            return _repositorioItemMovimentacaoCaixa.ConsulteSaldoItensCaixaMovimento(movimentoId, categoriaId);
        }
    }
}
