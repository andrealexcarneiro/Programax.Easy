using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.Repositorio
{
    public interface IRepositorioParametros : IRepositorioBase<Parametros>
    {
        Parametros ConsulteParametros();
    }
}
