using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ
{
    public class ConversorItemMovimentacaoBanco : ConversorDeObjetoBasico<ItemMovimentacaoBanco>, IConversorDeObjeto<ItemMovimentacaoBanco>
    {
        public ItemMovimentacaoBanco CopieObjetoParaPersistencia(ItemMovimentacaoBanco objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoBanco>();

            var itemMovimentacaoBancoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ItemMovimentacaoBanco();

            CopieTodasAsPropriedades(objetoDeNegocio, itemMovimentacaoBancoBase);

            return itemMovimentacaoBancoBase;
        }
    }
}
