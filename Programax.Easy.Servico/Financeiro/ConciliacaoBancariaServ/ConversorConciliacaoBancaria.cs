using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Financeiro.ConciliacaoBancariaServ
{
    public class ConversorConciliacaoBancaria : ConversorDeObjetoBasico<ConciliacaoBancaria>, IConversorDeObjeto<ConciliacaoBancaria>
    {
        public ConciliacaoBancaria CopieObjetoParaPersistencia(ConciliacaoBancaria objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioConciliacaoBancaria>();

            var conciliacaoBancaria = repositorio.Consulte(objetoDeNegocio.Id) ?? new ConciliacaoBancaria();

            CopieTodasAsPropriedades(objetoDeNegocio, conciliacaoBancaria);

            return conciliacaoBancaria;
        }
    }
}
