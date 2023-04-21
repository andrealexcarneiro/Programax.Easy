using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.Repositorio
{
    public interface IRepositorioTrocaPedidoDeVenda : IRepositorioBase<TrocaPedidoDeVenda>
    {
        List<TrocaPedidoDeVenda> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, PedidoDeVendaObj.ObjetoDeNegocio.PedidoDeVenda pedidoDeVenda, Enumeradores.EnumStatusTrocaPedidoDeVenda? statusTrocaPedidoDeVenda);

        void AtualizeEstornoDeTroca(TrocaPedidoDeVenda troca);
    }   
}
