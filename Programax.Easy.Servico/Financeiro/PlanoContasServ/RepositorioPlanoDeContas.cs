using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Financeiro.PlanoContasServ
{
    public class RepositorioPlanoDeContas : RepositorioBase<PlanoDeContas>, IRepositorioPlanoDeContas
    {
        public RepositorioPlanoDeContas(ISession sessao)
            : base(sessao)
        {

        }

        public override List<PlanoDeContas> ConsulteLista()
        {
            return _session.QueryOver<PlanoDeContas>().OrderBy(planoDeContas => planoDeContas.NumeroPlanoDeContas).Asc().List().ToList();
        }

        public List<PlanoDeContas> ConsulteLista(string numeroPlanoContas,
                                                                     string descricao,
                                                                     string status,
                                                                     EnumNaturezaPlanoContas? naturezaPlanoContas,
                                                                     EnumTipoPlanoContas? tipoPlanoContas,
                                                                     string numeroPlanoContasContador)
        {
            Expression<Func<PlanoDeContas, bool>> expressaoParaConsulta = planoDeContas => planoDeContas.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(numeroPlanoContas))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(planoDeContas => planoDeContas.NumeroPlanoDeContas.IsLike("%" + numeroPlanoContas + "%"));
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(planoDeContas => planoDeContas.Status == status);
            }

            if (naturezaPlanoContas != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(planoDeContas => planoDeContas.NaturezaPlanoContas == naturezaPlanoContas);
            }

            if (tipoPlanoContas != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(planoDeContas => planoDeContas.TipoPlanoContas == tipoPlanoContas);
            }

            if (!string.IsNullOrWhiteSpace(numeroPlanoContasContador))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(planoDeContas => planoDeContas.NumeroPlanoContasContador.IsLike("%" + numeroPlanoContasContador + "%"));
            }

            return _session.QueryOver<PlanoDeContas>().Where(expressaoParaConsulta)
                                        .OrderBy(planoDeContas => planoDeContas.NumeroPlanoDeContas).Asc()
                                        .List().ToList();
        }

        public PlanoDeContas ConsultePlanoDeContasPeloNumeroDiferenteDoIdInformado(int idPlanoDeContas, string numeroPlanoDeContas)
        {
            return _session.QueryOver<PlanoDeContas>()
                                        .Where(planoDeContas => planoDeContas.Id != idPlanoDeContas &&
                                                                               planoDeContas.NumeroPlanoDeContas == numeroPlanoDeContas)
                                        .SingleOrDefault();
        }

        public PlanoDeContas ConsultePlanoDeContasPeloNumero(string numeroPlanoContas)
        {
            return _session.QueryOver<PlanoDeContas>().Where(planoDeContas => planoDeContas.NumeroPlanoDeContas == numeroPlanoContas).Take(1).SingleOrDefault();
        }

        public PlanoDeContas ConsultePlanoDeContasAtivoPeloNumero(string numeroPlanoContas)
        {
            return _session.QueryOver<PlanoDeContas>().Where(planoDeContas => planoDeContas.Status == "A" &&
                                                                                                                      planoDeContas.NumeroPlanoDeContas == numeroPlanoContas)
                                                                                                                      .Take(1).SingleOrDefault();
        }
    }
}
