using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ
{
    public class ConversorParametros : ConversorDeObjetoBasico<Parametros>, IConversorDeObjeto<Parametros>
    {
        public Parametros CopieObjetoParaPersistencia(Parametros objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioParametros>();

            var parametrosDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Parametros();

            //objetoDeNegocio.HabilitarRecursosEspeciais = parametrosDaBase.HabilitarRecursosEspeciais;

            CopieTodasAsPropriedades(objetoDeNegocio, parametrosDaBase);

            return parametrosDaBase;
        }
    }
}
