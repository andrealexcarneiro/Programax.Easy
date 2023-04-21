using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio
{
    public interface IRepositorioRoteirizacao:IRepositorioBase<Roteirizacao>
    {   
        List<Roteirizacao> ConsulteLista(Pessoa pessoa,               
                                                                  EnumStatusRoteiro? statusRoteiro,                                                                  
                                                                  EnumDataFiltrarRoteiro? tipoDataFiltrar,
                                                                  DateTime? dataInicialPeriodo,
                                                                  DateTime? dataFinalPeriodo);
        
        List<Roteirizacao> ConsulteListaCodigoRoteiro(int? numeroRoteiro);

        //Roteiro ConsultePeloNumeroPedidoParceiroEDataElaboracao(int PedidoId,
        //                                                                                                                            Pessoa parceiro, 
        //                                                                                                                            DateTime dataElaboracao);

        //Roteiro ConsultePorPedido(int idPedido);
    }
}
