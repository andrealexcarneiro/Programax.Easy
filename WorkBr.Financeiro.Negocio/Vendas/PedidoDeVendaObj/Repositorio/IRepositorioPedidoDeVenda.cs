using System;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;


namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio
{
    public interface IRepositorioPedidoDeVenda : IRepositorioBase<PedidoDeVenda>
    {
        PedidoDeVenda ConsulteMaiorVenda(Pessoa cliente);

        PedidoDeVenda ConsultePedidoParaRoteiro(int idPedido);
        
        List<PedidoDeVenda> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, Pessoa atendente, Pessoa vendedor);

        List<PedidoDeVenda> ConsulteListaParaRoteiro(DateTime? dataInicial, DateTime? dataFinal);

        List<PedidoDeVenda> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, Pessoa atendente, Pessoa vendedor, Pessoa cliente, 
                                          Enumeradores.EnumTipoPedidoDeVenda? tipoPedidoDeVenda, Enumeradores.EnumStatusPedidoDeVenda? statusPedidoDeVenda, 
                                          Pessoa usuario = null, bool EReservado = false);

        List<VWVenda> ConsulteListaVWVenda(EnumPesquisaPorFuncao pesquisaPorFuncao,
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
                                                                  );
        List<CustoFinanceiro> ConsulteListaCustoFinanceiro(
                                                                   List<Pessoa> parceiroPesquisa,
                                                                   DateTime dataInicialPeriodo,
                                                                   DateTime dataFinalPeriodo
                                                                   );



        List<PedidoDeVenda> ConsulteListaPedidosPagosERoteiroConcluido(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                                                            List<Pessoa> parceiroPesquisa,
                                                                            bool periodoFaturamento,
                                                                            DateTime dataInicialPeriodo,
                                                                            DateTime dataFinalPeriodo);
      
        List<VWVendaTransportes> ConsulteListaVWVendaTransportes(EnumPesquisaPorFuncao pesquisaPorFuncao,
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
                                                                  EnumOrdenacaoPesquisaVwVendas ordenacao);


        List<VWVenda> ConsulteListaVWVendasPorCliente(Pessoa cliente,
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
                                                                                     string formaPagamento = null);

        PedidoDeVenda ConsultePedidoFaturadoOuEmitidoNfe(int pedidoId, bool EhReservado = false);

        List<VWVenda> ConsulteListaVWVendas(int? numeroDocumento, bool periodoFaturamento, DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool? vendaJahExportada);
        List<VWVenda> ConsulteTotalVWVendas(DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool statusEmitidoNFe);
        List<VWVenda> ConsulteTotalVWVendasII(DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool statusEmitidoNFe);
        List<VWVenda> ConsulteVWVendasPag(DateTime dataInicial, DateTime dataFinal, string Condicao);

        PedidoDeVenda ConsulteJoinComItens(int pedidoId);
    }
}
