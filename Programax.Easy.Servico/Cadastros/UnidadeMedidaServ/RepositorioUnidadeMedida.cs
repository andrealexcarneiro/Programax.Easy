using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.UnidadeMedidaServ
{
    public class RepositorioUnidadeMedida : RepositorioBase<UnidadeMedida>, IRepositorioUnidadeMedida
    {
        public RepositorioUnidadeMedida(ISession sessao)
            : base(sessao)
        {
        }

        public List<UnidadeMedida> ConsulteLista(string descricao, string abreviacao, string status)
        {
            Expression<Func<UnidadeMedida, bool>> expressaoParaConsulta = unidadeMedida => unidadeMedida.Descricao.IsLike("%" + descricao + "%") &&
                                                                                                                                           unidadeMedida.Abreviacao.IsLike("%" + abreviacao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(unidadeMedida => unidadeMedida.Status == status);
            }

            return _session.QueryOver<UnidadeMedida>().Where(expressaoParaConsulta).List<UnidadeMedida>().ToList();
        }

        public List<UnidadeMedida> ConsulteListaAtiva()
        {
            return _session.QueryOver<UnidadeMedida>().Where(unidade => unidade.Status == "A").List<UnidadeMedida>().ToList();
        }

        public UnidadeMedida ConsultePelaAbreviacao(string abreviacao, int idDesconsiderar)
        {
            return _session.QueryOver<UnidadeMedida>().Where(unidade => unidade.Abreviacao == abreviacao && unidade.Id != idDesconsiderar).Take(1).SingleOrDefault();
        }
    }
}
