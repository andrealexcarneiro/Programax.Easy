using System.Collections.Generic;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    public class ConversorPedidoDeVenda : ConversorDeObjetoBasico<PedidoDeVenda>, IConversorDeObjeto<PedidoDeVenda>
    {
        public PedidoDeVenda CopieObjetoParaPersistencia(PedidoDeVenda objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPedidoDeVenda>();

            var pedidoDeVendaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new PedidoDeVenda();

            if (objetoDeNegocio == pedidoDeVendaDaBase)
            {
                return pedidoDeVendaDaBase;
            }

            var listaDeItens = CopieListaItens(objetoDeNegocio, pedidoDeVendaDaBase);
            var listaDeParcelas = CopieListaParcelas(objetoDeNegocio, pedidoDeVendaDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, pedidoDeVendaDaBase);

            pedidoDeVendaDaBase.ListaItens = listaDeItens;
            pedidoDeVendaDaBase.ListaParcelasPedidoDeVenda = listaDeParcelas;

            return pedidoDeVendaDaBase;
        }

        private IList<ItemPedidoDeVenda> CopieListaItens(PedidoDeVenda objetoDeNegocio, PedidoDeVenda pedidoDeVendaDaBase)
        {
            var listaItens = pedidoDeVendaDaBase.ListaItens;

            listaItens.Clear();

            foreach (var item in objetoDeNegocio.ListaItens)
            {
                var itemCopiado = new ItemPedidoDeVenda();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.PedidoDeVenda = pedidoDeVendaDaBase;
                listaItens.Add(itemCopiado);
            }

            return listaItens;
        }

        private IList<ParcelaPedidoDeVenda> CopieListaParcelas(PedidoDeVenda objetoDeNegocio, PedidoDeVenda pedidoDeVendaDaBase)
        {
            var listaParcelas = pedidoDeVendaDaBase.ListaParcelasPedidoDeVenda;

            listaParcelas.Clear();

            foreach (var item in objetoDeNegocio.ListaParcelasPedidoDeVenda)
            {
                var itemCopiado = new ParcelaPedidoDeVenda();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.PedidoDeVenda = pedidoDeVendaDaBase;
                listaParcelas.Add(itemCopiado);
            }

            return listaParcelas;
        }
    }
}
