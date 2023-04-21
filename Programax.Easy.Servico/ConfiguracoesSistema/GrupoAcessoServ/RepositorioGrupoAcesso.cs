using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.ConfiguracoesSistema.GrupoAcessoServ
{
    public class RepositorioGrupoAcesso: RepositorioBase<GrupoAcesso>, IRepositorioGrupoAcesso
    {
        public RepositorioGrupoAcesso(ISession sesao)
            : base(sesao)
        {
        }

        public List<GrupoAcesso> ConsulteLista(string descricao)
        {
            return _session.QueryOver<GrupoAcesso>().Where(grupoAcesso => grupoAcesso.Descricao.IsLike("%" + descricao + "%")).List().ToList();
        }
    }
}
