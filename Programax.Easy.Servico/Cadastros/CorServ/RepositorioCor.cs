using NHibernate;
using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CorObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using System.Linq.Expressions;
using System;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.CorServ
{
    public class RepositorioCor : RepositorioBase<Cor>, IRepositorioCor
    {
        public RepositorioCor(ISession sessao)
            : base(sessao)
        {

        }

        public List<Cor> ConsulteLista(string descricao, string status)
        {
            Expression<Func<Cor, bool>> expressaoParaConsulta = cor => cor.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cor => cor.Status == status);
            }

            return _session.QueryOver<Cor>().Where(expressaoParaConsulta).List<Cor>().ToList();
        }

        public List<Cor> ConsulteListaAtiva()
        {
            return _session.QueryOver<Cor>().Where(cor => cor.Status == "A").List().ToList();
        }
    }
}
