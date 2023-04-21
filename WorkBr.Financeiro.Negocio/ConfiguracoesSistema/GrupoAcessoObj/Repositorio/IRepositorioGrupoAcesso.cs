using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.Repositorio
{
    public interface IRepositorioGrupoAcesso : IRepositorioBase<GrupoAcesso>
    {
        List<GrupoAcesso> ConsulteLista(string descricao);
    }
}
