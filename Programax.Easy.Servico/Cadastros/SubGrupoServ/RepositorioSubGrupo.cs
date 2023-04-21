using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.SubGrupoServ
{
    public class RepositorioSubGrupo : RepositorioBase<SubGrupo>, IRepositorioSubGrupo
    {
        public RepositorioSubGrupo(ISession sessao)
            : base(sessao)
        {

        }

        public List<SubGrupo> ConsulteLista(string descricao, Grupo grupo, string status)
        {
            Expression<Func<SubGrupo, bool>> expressaoParaConsulta = subGrupo => subGrupo.Descricao.IsLike("%" + descricao + "%");

            if (grupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(subGrupo => subGrupo.Grupo.Id == grupo.Id);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(subGrupo => subGrupo.Status == status);
            }

            return _session.QueryOver<SubGrupo>().Where(expressaoParaConsulta).List().ToList();
        }


        public List<SubGrupo> ConsulteListaAtiva(Grupo grupo)
        {
            grupo = grupo ?? new Grupo();

            return _session.QueryOver<SubGrupo>().Where(subGrupo => subGrupo.Grupo.Id == grupo.Id && subGrupo.Status == "A").List().ToList();
        }
    }
}
