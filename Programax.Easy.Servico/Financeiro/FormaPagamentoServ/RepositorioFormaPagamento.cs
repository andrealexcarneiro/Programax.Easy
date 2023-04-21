using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.FormaPagamentoServ
{
    public class RepositorioFormaPagamento : RepositorioBase<FormaPagamento>, IRepositorioFormaPagamento
    {
        public RepositorioFormaPagamento(ISession sessao)
            : base(sessao)
        {
            
        }

        public List<FormaPagamento> ConsulteListaFormasDePagamentoAtivas()
        {
            return _session.QueryOver<FormaPagamento>().Where(formaPagamento => formaPagamento.Status == "A").List().ToList();
        }


        public List<FormaPagamento> ConsulteLista(string descricao, string status)
        {
            Expression<Func<FormaPagamento, bool>> expressaoParaConsulta = formaPagamento => formaPagamento.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(formaPagamento => formaPagamento.Status == status);
            }

            return _session.QueryOver<FormaPagamento>().Where(expressaoParaConsulta).List().ToList();
        }


        public List<FormaPagamento> ConsulteListaAtivos()
        {
            return _session.QueryOver<FormaPagamento>().Where(formaPagamento => formaPagamento.Status == "A").List().ToList();
        }

        public FormaPagamento ConsultePeloTipo(EnumTipoFormaPagamento tipoFormaPagamento)
        {
            return _session.QueryOver<FormaPagamento>().Where(forma => forma.TipoFormaPagamento == tipoFormaPagamento).Take(1).SingleOrDefault();
        }
    }
}
