using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.Repositorio;

namespace Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ
{
    public class ConversorTrocaPedidoDeVenda : ConversorDeObjetoBasico<TrocaPedidoDeVenda>, IConversorDeObjeto<TrocaPedidoDeVenda>
    {
        public TrocaPedidoDeVenda CopieObjetoParaPersistencia(TrocaPedidoDeVenda objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTrocaPedidoDeVenda>();

            var trocaPedidoDeVendaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new TrocaPedidoDeVenda();

            if (objetoDeNegocio == trocaPedidoDeVendaDaBase)
            {
                return trocaPedidoDeVendaDaBase;
            }

            var listaDeItens = CopieListaItens(objetoDeNegocio, trocaPedidoDeVendaDaBase);
            var listaDeItensPedido = CopieListaItensPedido(objetoDeNegocio, trocaPedidoDeVendaDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, trocaPedidoDeVendaDaBase);

            trocaPedidoDeVendaDaBase.ListaItens = listaDeItens;
            trocaPedidoDeVendaDaBase.ListaItensPedido = listaDeItensPedido;

            return trocaPedidoDeVendaDaBase;
        }

        private IList<ItemTrocaPedidoDeVenda> CopieListaItens(TrocaPedidoDeVenda objetoDeNegocio, TrocaPedidoDeVenda trocaPedidoDeVendaDaBase)
        {
            var listaItens = trocaPedidoDeVendaDaBase.ListaItens;

            listaItens.Clear();

            foreach (var item in objetoDeNegocio.ListaItens)
            {
                var itemCopiado = new ItemTrocaPedidoDeVenda();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.TrocaPedidoDeVenda = trocaPedidoDeVendaDaBase;
                listaItens.Add(itemCopiado);
            }

            return listaItens;
        }

        private IList<ItemPedidoTrocaPedidoDeVenda> CopieListaItensPedido(TrocaPedidoDeVenda objetoDeNegocio, TrocaPedidoDeVenda trocaPedidoDeVendaDaBase)
        {
            var listaItensPedido = trocaPedidoDeVendaDaBase.ListaItensPedido;

            listaItensPedido.Clear();

            foreach (var item in objetoDeNegocio.ListaItensPedido)
            {
                var itemCopiado = new ItemPedidoTrocaPedidoDeVenda();

                CopieTodasAsPropriedades(item, itemCopiado);

                itemCopiado.Id = 0;
                itemCopiado.TrocaPedidoDeVenda = trocaPedidoDeVendaDaBase;
                listaItensPedido.Add(itemCopiado);
            }

            return listaItensPedido;
        }
    }
}
