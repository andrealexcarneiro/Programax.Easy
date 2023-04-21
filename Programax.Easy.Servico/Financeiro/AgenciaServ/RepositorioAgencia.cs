using NHibernate;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using System.Linq.Expressions;
using System.Linq;
using System;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Criterion;

namespace Programax.Easy.Servico.Financeiro.AgenciaServ
{
    public class RepositorioAgencia : RepositorioBase<Agencia>, IRepositorioAgencia
    {
        public RepositorioAgencia(ISession sessao)
            : base(sessao)
        {

        }

        public List<Agencia> ConsulteLista(Banco banco, string nomeAgencia, string status)
        {
            Expression<Func<Agencia, bool>> expressaoParaConsulta = agencia => agencia.NomeAgencia.IsLike("%" + nomeAgencia + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(agencia => agencia.Status == status);
            }

            if (banco != null)
            {   
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(agencia => agencia.Banco.Id == banco.Id);
            }

            return _session.QueryOver<Agencia>().Where(expressaoParaConsulta).List().ToList();
        }

        public Agencia Consulte(int idBanco, string numeroAgencia)
        {
            return _session.QueryOver<Agencia>().Where(agencia => agencia.Banco.Id == idBanco && agencia.NumeroAgencia == numeroAgencia)
                                                                        .Take(1).SingleOrDefault();
        }
    }
}
