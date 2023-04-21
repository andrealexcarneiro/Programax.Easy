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
    public class RepositorioSubGrupoCategoria : RepositorioBase<SubGrupoCategoria>, IRepositorioSubGrupoCategoria
    {
        public RepositorioSubGrupoCategoria(ISession sessao)
            : base(sessao)
        {

        }

        public List<SubGrupoCategoria> ConsulteLista(int? idGrupo, string descricao, string status)
        {
            Expression<Func<SubGrupoCategoria, bool>> expressaoParaConsulta = grupo => grupo.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(grupo => grupo.Ativo == status);
            }
            
            if (idGrupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(grupo => grupo.Grupo.Id == idGrupo);
            }

            return _session.QueryOver<SubGrupoCategoria>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<SubGrupoCategoria> ConsulteListaAtiva()
        {
            return _session.QueryOver<SubGrupoCategoria>().Where(grupo => grupo.Ativo == "A").List().ToList();
        }
    }
}
