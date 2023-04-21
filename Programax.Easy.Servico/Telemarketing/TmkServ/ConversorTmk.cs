using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Telemarketing.TmkServ
{
    public class ConversorTmk : ConversorDeObjetoBasico<Tmk>, IConversorDeObjeto<Tmk>
    {
        public Tmk CopieObjetoParaPersistencia(Tmk objetoDeNegocio)
        {
            var objetoDeNegocioCopiado = objetoDeNegocio.CloneCompleto();

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTmk>();

            var roteiroBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Tmk();

            CopieTodasAsPropriedades(objetoDeNegocioCopiado, roteiroBase);

            return roteiroBase;
        }
    }
}
