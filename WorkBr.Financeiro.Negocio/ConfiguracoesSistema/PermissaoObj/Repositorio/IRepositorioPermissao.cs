using System.Collections.Generic;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.Repositorio
{
    public interface IRepositorioPermissao : IRepositorioBase<Permissao>
    {
        List<Permissao> ConsulteLista(int idGrupoAcesso);
    }
}
