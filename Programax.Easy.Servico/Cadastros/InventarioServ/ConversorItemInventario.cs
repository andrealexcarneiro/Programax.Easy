using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    public class ConversorItemInventario : ConversorDeObjetoBasico<ItemInventario>, IConversorDeObjeto<ItemInventario>
    {
        public ItemInventario CopieObjetoParaPersistencia(ItemInventario objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemInventario>();

            var itemInventarioDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ItemInventario();

            objetoDeNegocio.Inventario = itemInventarioDaBase.Inventario;

            CopieTodasAsPropriedades(objetoDeNegocio, itemInventarioDaBase);

            return itemInventarioDaBase;
        }
    }
}
