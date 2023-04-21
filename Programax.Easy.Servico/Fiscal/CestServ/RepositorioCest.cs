using System.Linq.Expressions;
using NHibernate;
using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Linq;
using Programax.Easy.Negocio.Fiscal.CestObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Fiscal.CestServ
{
    public class RepositorioCest: RepositorioBase<Cest>, IRepositorioCest
    {
        public RepositorioCest (ISession sessao)
            : base(sessao)
        {
        }

        public List<Cest> ConsulteListaCest(string codigoCest, string descricao, string status)
        {
            Expression<Func<Cest, bool>> expressaoParaConsulta = cest => cest.DescricaoCest.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(codigoCest))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cest=>cest.CodigoCest.IsLike("%" + codigoCest + "%"));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cest =>cest.Status == status);
            }

            return _session.QueryOver<Cest>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Cest> ConsulteListaDeCodigosCest(List<string> listaCodigosCest)
        {
            return _session.QueryOver<Cest>().Where(cest =>cest.CodigoCest.IsIn(listaCodigosCest)).List().ToList();
        }

        public Cest ConsultePeloCodigoCest(string codigoCest)
        {
            return _session.QueryOver<Cest>().Where(cest => cest.CodigoCest == codigoCest).Take(1).SingleOrDefault();
        }
    }
}
