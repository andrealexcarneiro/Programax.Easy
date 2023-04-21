using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CestObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;


namespace Programax.Easy.Servico.Fiscal.CestServ
{
    public class ConvercorCest : ConversorDeObjetoBasico<Cest>, IConversorDeObjeto<Cest>
    {
        public Cest CopieObjetoParaPersistencia(Cest objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCest>();

            var cestDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Cest();

            CopieTodasAsPropriedades(objetoDeNegocio, cestDaBase);

            return cestDaBase;
        }

    }
}
