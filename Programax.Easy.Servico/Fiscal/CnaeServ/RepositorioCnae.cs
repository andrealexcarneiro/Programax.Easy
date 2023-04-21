using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CnaeObj.Repositorio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Fiscal.CnaeServ
{
    public class RepositorioCnae : RepositorioBase<Cnae>, IRepositorioCnae
    {
        public RepositorioCnae(ISession sessao)
            : base(sessao)
        {

        }

        public Cnae ConsultePeloCodigo(string codigo)
        {
            return _session.QueryOver<Cnae>().Where(cnae => cnae.Codigo == codigo).SingleOrDefault();
        }


        public List<Cnae> ConsulteLista(string codigoCnae, string descricao, EnumAtividadeCnae? atividade, string status)
        {
            Expression<Func<Cnae, bool>> expressaoParaConsulta = cnae => cnae.Codigo.IsLike("%" + codigoCnae + "%");

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(cnae => cnae.Descricao.IsLike("%" + descricao + "%"));

            if (atividade != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cnae => cnae.Atividade == atividade);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cnae => cnae.Status == status);
            }

            return _session.QueryOver<Cnae>().Where(expressaoParaConsulta).List().ToList();
        }
    }
}
