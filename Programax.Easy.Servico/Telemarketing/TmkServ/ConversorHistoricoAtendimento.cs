using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Telemarketing
{
    public class ConversorHistoricoAtendimento : ConversorDeObjetoBasico<HistoricoAtendimento>, IConversorDeObjeto<HistoricoAtendimento>
    {
        public HistoricoAtendimento CopieObjetoParaPersistencia(HistoricoAtendimento objetoDeNegocio)
        {
            var objetoDeNegocioCopiado = objetoDeNegocio.CloneCompleto();

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioHistoricoAtendimento>();

            var roteiroBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new HistoricoAtendimento();

            CopieTodasAsPropriedades(objetoDeNegocioCopiado, roteiroBase);

            return roteiroBase;
        }
    }
}
