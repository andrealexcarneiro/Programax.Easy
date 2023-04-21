using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ
{
    public class ConversorItemMovimentacaoCaixa : ConversorDeObjetoBasico<ItemMovimentacaoCaixa>, IConversorDeObjeto<ItemMovimentacaoCaixa>
    {
        public ItemMovimentacaoCaixa CopieObjetoParaPersistencia(ItemMovimentacaoCaixa objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoCaixa>();

            var itemMovimentacaoCaixaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new ItemMovimentacaoCaixa();

            CopieTodasAsPropriedades(objetoDeNegocio, itemMovimentacaoCaixaBase);

            return itemMovimentacaoCaixaBase;
        }
    }
}
