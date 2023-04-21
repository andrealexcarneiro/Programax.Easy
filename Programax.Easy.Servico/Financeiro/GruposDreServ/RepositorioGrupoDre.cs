using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposDreObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.GruposDreServ
{
    public class RepositorioGrupoDre : RepositorioBase<GrupoDre>, IRepositorioGrupoDre
    {
        public RepositorioGrupoDre(ISession sessao)
           : base(sessao)
        {

        }

        public List<GrupoDre> ConsulteLista(int? idGrupo, string descricao, string status)
        {
            Expression<Func<GrupoDre, bool>> expressaoParaConsulta = grupo => grupo.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(grupo => grupo.Ativo == status);
            }

            return _session.QueryOver<GrupoDre>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<GrupoDre> ConsulteListaAtiva()
        {
            return _session.QueryOver<GrupoDre>().Where(grupo => grupo.Ativo == "A").List().ToList();
        }


    }
}

