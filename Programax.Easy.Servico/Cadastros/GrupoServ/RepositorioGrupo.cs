using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.GrupoServ
{
    public class RepositorioGrupo : RepositorioBase<Grupo>, IRepositorioGrupo
    {
        public RepositorioGrupo(ISession sessao)
            : base(sessao)
        {

        }

        public List<Grupo> ConsulteLista(string descricao, Categoria categoria, string status)
        {
            Expression<Func<Grupo, bool>> expressaoParaConsulta = grupo => grupo.Descricao.IsLike("%" + descricao + "%");

            if (categoria != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(grupo => grupo.Categoria.Id == categoria.Id);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(grupo => grupo.Status == status);
            }

            return _session.QueryOver<Grupo>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Grupo> ConsulteListaAtivos(Categoria categoria)
        {
            categoria = categoria ?? new Categoria();

            return _session.QueryOver<Grupo>().Where(grupo => grupo.Categoria.Id == categoria.Id && grupo.Status == "A").List().ToList();
        }

        public List<Grupo> ConsulteListaAtivos()
        {
            return _session.QueryOver<Grupo>().Where(grupo => grupo.Status == "A").List().ToList();
        }
    }
}
