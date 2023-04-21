using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PautaIcmsObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.Cadastros.PautaIcmsServ
{
    public class ConversorPautaIcms : ConversorDeObjetoBasico<PautaIcms>, IConversorDeObjeto<PautaIcms>
    {
        public PautaIcms CopieObjetoParaPersistencia(PautaIcms objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPautaIcms>();

            var pautaIcmsBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new PautaIcms();

            CopieTodasAsPropriedades(objetoDeNegocio, pautaIcmsBase);

            return pautaIcmsBase;
        }
    }
}
