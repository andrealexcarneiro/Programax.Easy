using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ
{
    public class RepositorioCondicaoPagamento : RepositorioBase<CondicaoPagamento>, IRepositorioCondicaoPagamento
    {
        public RepositorioCondicaoPagamento(ISession sessao)
            : base(sessao)
        {

        }

        public List<CondicaoPagamento> ConsulteListaDeCondicoesPagamentoAtivas()
        {
            return _session.QueryOver<CondicaoPagamento>().Where(condicaoPagamento => condicaoPagamento.Status == "A").List().ToList();
        }

        public List<CondicaoPagamento> ConsulteLista(string descricao, string status)
        {
            Expression<Func<CondicaoPagamento, bool>> expressaoParaConsulta = condicaoPagamento => condicaoPagamento.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(condicaoPagamento => condicaoPagamento.Status == status);
            }

            return _session.QueryOver<CondicaoPagamento>().Where(expressaoParaConsulta).TransformUsing(Transformers.DistinctRootEntity).List().ToList();
        }

        public CondicaoPagamento ConsulteCondicaoPagamentoAVistaPadrao()
        {
            return _session.QueryOver<CondicaoPagamento>().Where(cond => cond.CondicaoPadraoAVista == true).Take(1).SingleOrDefault();
        }
    }
}
