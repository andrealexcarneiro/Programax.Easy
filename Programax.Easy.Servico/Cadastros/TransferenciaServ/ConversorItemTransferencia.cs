using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    public class ConversorItemTransferencia : ConversorDeObjetoBasico<ItemTransferencia>, IConversorDeObjeto<ItemTransferencia>
    {
        public ItemTransferencia CopieObjetoParaPersistencia(ItemTransferencia objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemTransferencia>();

            var itemTransferenciaDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ItemTransferencia();

            //objetoDeNegocio.Transferencia = itemTransferenciaDaBase.Transferencia;

            CopieTodasAsPropriedades(objetoDeNegocio, itemTransferenciaDaBase);

            return itemTransferenciaDaBase;
        }
    }
}
