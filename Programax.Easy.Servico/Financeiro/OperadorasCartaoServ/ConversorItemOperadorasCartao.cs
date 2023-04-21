using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.OperadorasCartaoServ
{
    public class ConversorItemOperadorasCartao : ConversorDeObjetoBasico<ItemOperadorasCartao>, IConversorDeObjeto<ItemOperadorasCartao>
    {
        public ItemOperadorasCartao CopieObjetoParaPersistencia(ItemOperadorasCartao objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemOperadorasCartao>();

            var ItemBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ItemOperadorasCartao();

            CopieTodasAsPropriedades(objetoDeNegocio, ItemBase);

            return ItemBase;
        }
    }
}
