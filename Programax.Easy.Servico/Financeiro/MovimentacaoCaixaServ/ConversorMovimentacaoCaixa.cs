using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;


namespace Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ
{
    public class ConversorMovimentacaoCaixa : ConversorDeObjetoBasico<MovimentacaoCaixa>, IConversorDeObjeto<MovimentacaoCaixa>
    {
        public MovimentacaoCaixa CopieObjetoParaPersistencia(MovimentacaoCaixa objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();

            var formaPagamentoBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new MovimentacaoCaixa();

            CopieTodasAsPropriedades(objetoDeNegocio, formaPagamentoBase);

            return formaPagamentoBase;
        }
    }
}
