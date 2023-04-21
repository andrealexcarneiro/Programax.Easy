using System;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using System.Transactions;
using System.Collections.Generic;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ
{
    [Funcionalidade(EnumFuncionalidade.MOVIMENTACAOBANCO)]
    public class ServicoItemMovimentacaoBanco : ServicoAkilSmallBusiness<ItemMovimentacaoBanco, ValidacaoItemMovimentacaoBanco, ConversorItemMovimentacaoBanco>
    {
        IRepositorioItemMovimentacaoBanco _repositorioItemMovimentacaoBanco;

        public ServicoItemMovimentacaoBanco()
        {
            RetorneRepositorio();
        }

        public ServicoItemMovimentacaoBanco(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<ItemMovimentacaoBanco> RetorneRepositorio()
        {
            if (_repositorioItemMovimentacaoBanco == null)
            {
                _repositorioItemMovimentacaoBanco = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoBanco>();
            }

            return _repositorioItemMovimentacaoBanco;
        }

        public override int Cadastre(ItemMovimentacaoBanco objetoDeNegocio)
        {
            return base.Cadastre(objetoDeNegocio);
        }
       
        public void EstorneItemMovimentacaoBanco(ItemMovimentacaoBanco itemMovimentacaoBanco)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                //Primeiro caso: Qualquer item que não é pedido de vendas, e não tem documento de origem.

                itemMovimentacaoBanco.EstahEstornado = true;

                List<ItemMovimentacaoBanco> listaItensMovimentacaoBanco = null;

                if (itemMovimentacaoBanco.NumeroDocumentoOrigem != null)
                {
                    listaItensMovimentacaoBanco = ConsulteListaItensPorNumeroDocumentoOrigemAtivos(itemMovimentacaoBanco.OrigemMovimentacaoBanco, itemMovimentacaoBanco.NumeroDocumentoOrigem);

                    foreach (var item in listaItensMovimentacaoBanco)
                    {
                        item.EstahEstornado = true;

                        Atualize(item);
                    }
                }
                else
                {
                    Atualize(itemMovimentacaoBanco);
                }

                //Segundo caso: Item gerado por um pedido de vendas, ou seja, tem documento de origem.

                if (itemMovimentacaoBanco.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.PEDIDODEVENDA && itemMovimentacaoBanco.NumeroDocumentoOrigem != string.Empty)
                {
                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                    
                    //Se o Pedido tiver NF rejeitada, será estornado!         
                    
                    ServicoNotaFiscal NotaFiscal = new ServicoNotaFiscal();
                    var _NotaFiscal = NotaFiscal.RetorneNotaSoPorPedido(itemMovimentacaoBanco.NumeroDocumentoOrigem.ToInt(),true);

                    if (_NotaFiscal.InformacoesGeraisNotaFiscal.Status == Negocio.Fiscal.Enumeradores.EnumStatusNotaFiscal.REJEITADA)
                    {
                        servicoPedidoDeVenda.VoltePedidoNFRejeitadoParaReservado(itemMovimentacaoBanco.NumeroDocumentoOrigem.ToInt());

                        if (itemMovimentacaoBanco.NumeroDocumentoOrigem != string.Empty)
                        {
                            listaItensMovimentacaoBanco = ConsulteListaItensPorNumeroDocumentoOrigemAtivos(itemMovimentacaoBanco.OrigemMovimentacaoBanco, itemMovimentacaoBanco.NumeroDocumentoOrigem);

                            foreach (var item in listaItensMovimentacaoBanco)
                            {
                                item.EstahEstornado = true;

                                Atualize(item);
                            }
                        }
                        else
                        {
                            Atualize(itemMovimentacaoBanco);
                        }
                    }
                    else
                    {
                        //Se o pedido estiver com nota emitida, e não é rejeitada, não será possível estornar!

                        var statusPedido = servicoPedidoDeVenda.Consulte(itemMovimentacaoBanco.NumeroDocumentoOrigem.ToInt());

                        if (statusPedido != null)
                            if (statusPedido.StatusPedidoVenda == Negocio.Vendas.Enumeradores.EnumStatusPedidoDeVenda.EMITIDONFE)
                            {
                                throw new Exception("Não é possível estornar o pedido: " + statusPedido.Id + ". Pois foi emitido a nota fiscal! Para estornar, cancele a nota fiscal");
                            }

                        servicoPedidoDeVenda.VoltePedidoDeVendaParaReservado(itemMovimentacaoBanco.NumeroDocumentoOrigem.ToInt());

                        if (itemMovimentacaoBanco.NumeroDocumentoOrigem != string.Empty)
                        {
                            listaItensMovimentacaoBanco = ConsulteListaItensPorNumeroDocumentoOrigemAtivos(itemMovimentacaoBanco.OrigemMovimentacaoBanco, itemMovimentacaoBanco.NumeroDocumentoOrigem);

                            foreach (var item in listaItensMovimentacaoBanco)
                            {
                                item.EstahEstornado = true;

                                Atualize(item);
                            }
                        }
                        else
                        {
                            Atualize(itemMovimentacaoBanco);
                        }                        
                    }

                }
                //Terceiro caso: item de origem contas a pagar receber, e não tem documento de origem.

                else if ((itemMovimentacaoBanco.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.CONTAPAGAR ||
                            itemMovimentacaoBanco.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.CONTARECEBER) &&
                            itemMovimentacaoBanco.NumeroDocumentoOrigem != null)
                {
                    //ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();
                    //var pagamento = servicoContasPagarReceberPagamento.Consulte(itemMovimentacaoBanco.ContaPagarReceber.Id);
                                        
                    //if (!pagamento.EstahEstornado)
                    //{
                    //    servicoContasPagarReceberPagamento.EstorneRegistro(pagamento);
                    //}
                }

                scope.Complete();
            }
        }

        public List<ItemMovimentacaoBanco> ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoBanco origemMovimentacaoBanco, string numeroDocumentoOrigem)
        {
            return _repositorioItemMovimentacaoBanco.ConsulteListaItensPorNumeroDocumentoOrigemAtivos(origemMovimentacaoBanco, numeroDocumentoOrigem);
        }

        public List<ItemMovimentacaoBanco> ConsulteListaItens(MovimentacaoBanco Movimento, DateTime? DataInicialPeriodo, DateTime? DataFinalPeriodo, EnumOrigemMovimentacaoBanco? Origem, string Descricao, 
                                                                EnumTipoMovimentacaoBanco? Tipo, string NumeroDoc, Pessoa Parceiro, CategoriaFinanceira Categoria, List<int> IdsMovimento)
        {
            return _repositorioItemMovimentacaoBanco.ConsulteListaItens(Movimento, DataInicialPeriodo, DataFinalPeriodo, Origem, Descricao, Tipo, NumeroDoc, Parceiro, Categoria, IdsMovimento);
        }

        public double ConsulteSaldoItensBancoMovimento(int MovimentoId, int CategoriaId, DateTime dataInicial, DateTime dataFinal)
        {
            return _repositorioItemMovimentacaoBanco.ConsulteSaldoItensBancoMovimento(MovimentoId, CategoriaId, dataInicial, dataFinal);
        }

        public void InsiraMovimentacaoBancaria(ContaPagarReceber contasPagarReceber, bool EhPagamentoParcial,
                                               EnumTipoOperacaoContasPagarReceber tipoOperacao, DateTime DataPagamento,
                                               CategoriaFinanceira categoria, BancoParaMovimento Banco, double valorPago,
                                               Pessoa usuario, bool ehTaxaAdm = false)
        {
            ItemMovimentacaoBanco itemMovimentacaoBanco = new ItemMovimentacaoBanco();

            itemMovimentacaoBanco.EstahEstornado = false;
            itemMovimentacaoBanco.Categoria = categoria;
            itemMovimentacaoBanco.Parceiro = contasPagarReceber.Pessoa;

            string descHistorico = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? "Recebto." : "Pagto.";

            itemMovimentacaoBanco.DescricaoDaMovimentacao = EhPagamentoParcial ? descHistorico + " Parcial. " + contasPagarReceber.Historico :
                                                            descHistorico + " Quitado. " + contasPagarReceber.Historico;

            itemMovimentacaoBanco.DataHoraLancamento = DataPagamento;

            itemMovimentacaoBanco.MovimentacaoBanco = new ServicoMovimentacaoBanco().ConsulteBancoAberto(new BancoParaMovimento { Id = Banco.Id });

            itemMovimentacaoBanco.TipoMovimentacao = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ?
                                                     EnumTipoMovimentacaoBanco.ENTRADA : EnumTipoMovimentacaoBanco.SAIDA;

            itemMovimentacaoBanco.OrigemMovimentacaoBanco = tipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ?
                                                            EnumOrigemMovimentacaoBanco.CONTARECEBER : EnumOrigemMovimentacaoBanco.CONTAPAGAR;

            //Caso a movimentacão for a taxa de administração
            if (ehTaxaAdm)
            {
                itemMovimentacaoBanco.OrigemMovimentacaoBanco = EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL;
                //contasPagarReceber.TipoOperacao != tipoOperacao ? EnumOrigemMovimentacaoBanco.CONTARECEBER : itemMovimentacaoBanco.OrigemMovimentacaoBanco;            
            }

            itemMovimentacaoBanco.Valor = valorPago;
            itemMovimentacaoBanco.NumeroDocumentoOrigem = contasPagarReceber.NumeroDocumento;
            itemMovimentacaoBanco.ContaPagarReceber = contasPagarReceber;

            //Usuário e Data de atualização / cadastro
            itemMovimentacaoBanco.DataAtualizacao = DateTime.Now;
            itemMovimentacaoBanco.UsuarioAtualizacao = new Pessoa { Id = usuario.Id };

            Cadastre(itemMovimentacaoBanco);
        }

        public string retorneNumeroParcela(string numeroDocumento)
        {
            var PrimeiroSplit = numeroDocumento.Split('-');

            var SegundoSplit = PrimeiroSplit[1].Split('/');

            int tam = 0;
            foreach (var item in SegundoSplit)
            {
                tam++;
            }

            if (tam > 1)
                return SegundoSplit[0];
            else
                return PrimeiroSplit[0];
           
        }

        public void CalculeDespesasCartoes(ContaPagarReceber contasPagarReceber, bool EhPagamentoParcial,
                                           EnumTipoOperacaoContasPagarReceber tipoOperacao, DateTime DataPagamento,
                                           BancoParaMovimento Banco, OperadorasCartao operadorasCartao,
                                           double valorPago, Pessoa usuario)
        {
            if (operadorasCartao == null || operadorasCartao.Id == 0) return;

            double valorAPagarSobreATaxaAdmCartao = 0;

            var cartao = new ServicoOperadorasCartao().Consulte(operadorasCartao.Id);
                       
            var listaItemOperadoras = new ServicoItemOperadorasCartao().ConsulteLista(cartao.Id);

            var parcela = retorneNumeroParcela(contasPagarReceber.NumeroDocumento);

            if(cartao.CategoriaDeDespesa != null)
            {
                if(listaItemOperadoras.Count == 0) // Caso não tenha itens de operadoras com condição de pagamento
                {
                    if (parcela.ToInt() >= cartao.CobrarTaxaApartirDaParcela)
                    {
                        if (cartao.TaxaAdministracao > 0)
                        {
                            valorAPagarSobreATaxaAdmCartao = (valorPago * cartao.TaxaAdministracao) / 100;

                            contasPagarReceber.Historico = "Taxa de Administração de cartão. Ref. ao doc:" + contasPagarReceber.NumeroDocumento;

                            InsiraMovimentacaoBancaria(contasPagarReceber, false, EnumTipoOperacaoContasPagarReceber.PAGAR,
                                                        DataPagamento, cartao.CategoriaDeDespesa, Banco, valorAPagarSobreATaxaAdmCartao, usuario, true);
                        }
                    }
                }
                else
                {//Vai fazer o cálculo da taxa conforme a condição de pagamento
                    var itemOperadora = listaItemOperadoras.Find(x => x.CondicaoPagamento.Id == contasPagarReceber.CondicaoPgtoId);

                    if (itemOperadora == null) return;

                    if (parcela.ToInt() >= itemOperadora.CobrarApartirDaParcela)
                    {
                        valorAPagarSobreATaxaAdmCartao = (valorPago * itemOperadora.Taxa) / 100;

                        contasPagarReceber.Historico = "Taxa de Administração de cartão. Ref. ao doc:" + contasPagarReceber.NumeroDocumento;

                        InsiraMovimentacaoBancaria(contasPagarReceber, false, EnumTipoOperacaoContasPagarReceber.PAGAR,
                                                    DataPagamento, cartao.CategoriaDeDespesa, Banco, valorAPagarSobreATaxaAdmCartao, usuario, true);
                    }

                }
            }
        }

        public void ExcluaParcialOrigemPagarReceber(ContaPagarReceber Objeto, bool EhDentroManutencao = true, bool EhConciliacao = false, double ValorPagoEstornar=0)
        {
            _repositorioItemMovimentacaoBanco.ExcluaParcialOrigemPagarReceber(Objeto, EhDentroManutencao, EhConciliacao, ValorPagoEstornar);
        }

        public ItemMovimentacaoBanco ConsulteItemConciliadoImportado(int IdItemConciliadoImportado)
        {
            return _repositorioItemMovimentacaoBanco.ConsulteItemConciliadoImportado(IdItemConciliadoImportado);
        }

        public void EstornarContasPagarReceber(ItemMovimentacaoBanco itemMov)
        {
            if (itemMov.ContaPagarReceber == null) return;

            var contaPagarReceberCompleto = new ServicoContasPagarReceber().Consulte(itemMov.ContaPagarReceber.Id);

            if (contaPagarReceberCompleto == null) return;

            if (contaPagarReceberCompleto.ListaContasPagarReceberParcial != null && contaPagarReceberCompleto.ListaContasPagarReceberParcial.Count > 0)
            {
                foreach (var item in contaPagarReceberCompleto.ListaContasPagarReceberParcial)
                {
                    if (item.Valor == itemMov.Valor && item.EstahEstornado == false)
                    {
                        new ServicoContasPagarReceberPagamento().EstorneRegistro(item);
                        return;
                    }
                }
            }

            if (contaPagarReceberCompleto.Status == EnumStatusContaPagarReceber.QUITADO || contaPagarReceberCompleto.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                if (contaPagarReceberCompleto.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
                    new ServicoContasReceber().EstornarContaPagarReceber(contaPagarReceberCompleto);
                else
                    new ServicoContasPagar().EstornarContaPagarReceber(contaPagarReceberCompleto);
        }
    }
}
