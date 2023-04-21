using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.Repositorio;
using NHibernate;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Criterion;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    public class RepositorioGrupoCategoria : RepositorioBase<GrupoCategoria>, IRepositorioGrupoCategoria
    {
        public RepositorioGrupoCategoria(ISession sessao)
            : base(sessao)
        {

        }

        public List<GrupoCategoria> ConsulteLista(int? idGrupo, string descricao, string status)
        {
            Expression<Func<GrupoCategoria, bool>> expressaoParaConsulta = grupo => grupo.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(grupo => grupo.Ativo == status);
            }                       

            return _session.QueryOver<GrupoCategoria>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<GrupoCategoria> ConsulteListaAtiva()
        {
            return _session.QueryOver<GrupoCategoria>().Where(grupo => grupo.Ativo == "A").List().ToList();
        }
    }
}
