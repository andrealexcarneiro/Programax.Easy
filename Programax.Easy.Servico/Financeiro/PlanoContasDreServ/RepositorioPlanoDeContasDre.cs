using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Financeiro.PlanoContasDreServ
{
    public class RepositorioPlanoDeContasDre : RepositorioBase<PlanoContaDre>, IRepositorioPlanoDeContasDre
    {
        public RepositorioPlanoDeContasDre(ISession sessao)
            : base(sessao)
        {

        }
        //OrderBy(planoDeContasDre => planoDeContasDre.NumeroPlanoDeContas).Asc().List().ToList();
        //OrderBy(planoDeContasDre => planoDeContasDre.NumeroPlanoDeContas).Asc().ThenBy(planoDeContasDre => planoDeContasDre.Descricao).Asc().ThenBy(planoDeContasDre => planoDeContasDre.Grau).Asc().List().ToList();
        public override List<PlanoContaDre> ConsulteLista()
        {
            return _session.QueryOver<PlanoContaDre>().OrderBy(planoDeContasDre => planoDeContasDre.NumeroPlanoDeContas).Asc().List().ToList();

        }

        public List<PlanoContaDre> ConsulteLista(string numeroPlanoContas,
                                                                     string descricao,
                                                                     string status,
                                                                     EnumNaturezaPlanoContas? naturezaPlanoContas,
                                                                     EnumTipoPlanoContas? tipoPlanoContas,
                                                                     string numeroPlanoContasContador, string Grau)
        {
            Expression<Func<PlanoContaDre, bool>> expressaoParaConsulta = planoDeContas => planoDeContas.Descricao.IsLike("%" + descricao + "%");

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

            return _session.QueryOver<PlanoContaDre>().Where(expressaoParaConsulta)
                                        .OrderBy(planoDeContas => planoDeContas.NumeroPlanoDeContas).Asc()
                                        .List().ToList();
        }

        public PlanoContaDre ConsultePlanoDeContasPeloNumeroDiferenteDoIdInformado(int idPlanoDeContas, string numeroPlanoDeContas)
        {
            return _session.QueryOver<PlanoContaDre>()
                                        .Where(planoDeContas => planoDeContas.Id != idPlanoDeContas &&
                                                                               planoDeContas.NumeroPlanoDeContas == numeroPlanoDeContas)
                                        .SingleOrDefault();
        }

        public PlanoContaDre ConsultePlanoDeContasPeloNumero(string numeroPlanoContas)
        {
            return _session.QueryOver<PlanoContaDre>().Where(planoDeContas => planoDeContas.NumeroPlanoDeContas == numeroPlanoContas).Take(1).SingleOrDefault();
        }

        public PlanoContaDre ConsultePlanoDeContasAtivoPeloNumero(string numeroPlanoContas)
        {
            return _session.QueryOver<PlanoContaDre>().Where(planoDeContas => planoDeContas.Status == "A" &&
                                                                                                                      planoDeContas.NumeroPlanoDeContas == numeroPlanoContas)
                                                                                                                      .Take(1).SingleOrDefault();
        }
    }
}
