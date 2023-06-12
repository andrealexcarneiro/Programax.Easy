using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio
{
    public interface IRepositorioRoteiro:IRepositorioBase<Roteiro>
    {
        Roteiro Consulte(int id, EnumPeriodo periodo);

        List<Roteiro> ConsulteLista(Pessoa pessoa,
                                                                  EnumPeriodo? periodo, 
                                                                  EnumStatusRoteiro? statusRoteiro,                                                                  
                                                                  EnumDataFiltrarRoteiro? tipoDataFiltrar,
                                                                  DateTime? dataInicialPeriodo,
                                                                  DateTime? dataFinalPeriodo, 
                                                                  int? idPedido,
                                                                  int? idRoteiro,
                                                                  bool buscarConcluidos = true);

        Roteiro ConsultePeloNumeroPedidoParceiroEDataElaboracao(int PedidoId,
                                                                                                                                    Pessoa parceiro, 
                                                                                                                                    DateTime dataElaboracao, 
                                                                                                                                    EnumPeriodo periodo);
        Roteiro ConsultePorPedido(int idPedido);

        List<Roteiro> ConsulteListaPorRoteirizacao(int roteirizacaoId);
    }
}
