using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.CidadeServ
{
    public class RepositorioCidade : RepositorioBase<Cidade>, IRepositorioCidade
    {
        public RepositorioCidade(ISession sessao)
            : base(sessao)
        {
            
        }

        public List<Cidade> ConsulteListaAtiva(string uf)
        {
            return _session.QueryOver<Cidade>().Where(cidade => cidade.Estado != null && cidade.Estado.UF == uf&& cidade.Status == "A")
                .List().ToList().OrderBy(cidade => cidade.Descricao).ToList();
        }

        public List<Cidade> ConsulteListaCidades(string descricao, string uf, string status)
        {
            Expression<Func<Cidade, bool>> expressaoParaConsulta = cidade => cidade.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(uf))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cidade => cidade.Estado.UF == uf);
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cidade => cidade.Status == status);
            }

            return _session.QueryOver<Cidade>().Where(expressaoParaConsulta).List().ToList();
        }

        public Cidade ConsultePeloCodigoIbge(string codigoIbge)
        {
            return _session.QueryOver<Cidade>().Where(cidade => cidade.CodigoIbge == codigoIbge).Take(1).SingleOrDefault();
        }

        public Cidade ConsultePeloCodigoIbgeAtivo(string codigoIbge)
        {
            return _session.QueryOver<Cidade>().Where(cidade => cidade.Status == "A" && cidade.CodigoIbge == codigoIbge).Take(1).SingleOrDefault();
        }
    }
}
