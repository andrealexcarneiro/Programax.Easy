using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using System.Linq;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    public class ConversorInventario : ConversorDeObjetoBasico<Inventario>, IConversorDeObjeto<Inventario>
    {
        public Inventario CopieObjetoParaPersistencia(Inventario objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioInventario>();

            var inventarioDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Inventario();

            var listaDeItens = CopieListaDeItens(objetoDeNegocio, inventarioDaBase);

            CopiePropriedadesEspecificas(objetoDeNegocio, inventarioDaBase);

            CopieTodasAsPropriedades(objetoDeNegocio, inventarioDaBase);

            inventarioDaBase.ListaDeItens = listaDeItens;

            return inventarioDaBase;
        }

        protected IList<ItemInventario> CopieListaDeItens(Inventario objetoDeNegocio, Inventario inventarioDaBase)
        {
            var listaDeItens = inventarioDaBase.ListaDeItens;

            foreach (var item in objetoDeNegocio.ListaDeItens)
            {
                ItemInventario itemInventario = null;

                if (item.Id > 0)
                {
                    itemInventario = listaDeItens.FirstOrDefault(itemDaLista => itemDaLista.Id == item.Id);
                }

                itemInventario = itemInventario ?? new ItemInventario();

                CopieTodasAsPropriedades(item, itemInventario);

                itemInventario.Inventario = inventarioDaBase;

                if (itemInventario.Id == 0)
                {
                    listaDeItens.Add(itemInventario);
                }
            }

            return listaDeItens;
        }

        private void CopiePropriedadesEspecificas(Inventario objetoDeNegocio, Inventario inventarioDaBase)
        {
            if (objetoDeNegocio.ContagemAtual == 0)
            {
                objetoDeNegocio.ContagemAtual = inventarioDaBase.ContagemAtual;
            }
        }
    }
}
