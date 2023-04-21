using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.TabelaPrecoServ
{
    public class RepositorioTabelaPreco : RepositorioBase<TabelaPreco>, IRepositorioTabelaPreco
    {
        public RepositorioTabelaPreco(ISession sessao)
            : base(sessao)
        {

        }

        public List<TabelaPreco> ConsulteListaTabelaPrecosAtivas()
        {
            return _session.QueryOver<TabelaPreco>().Where(tabelaPreco => tabelaPreco.Status == "A").List().ToList();
        }

        public List<TabelaPreco> ConsulteLista(string descricao, string status, DateTime? dataValidade = null)
        {
            Expression<Func<TabelaPreco, bool>> expressaoParaConsulta = tabelaPreco => tabelaPreco.Id > 0;

            if (!string.IsNullOrEmpty(descricao))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(tabelaPreco => tabelaPreco.NomeTabela.IsLike("%" + descricao + "%"));
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(tabelaPreco => tabelaPreco.Status != null && tabelaPreco.Status.ToUpper() == status.ToUpper());
            }

            if (dataValidade != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(tabelaPreco => tabelaPreco.DataDeValidade != null && tabelaPreco.DataDeValidade == dataValidade);
            }

            return _session.QueryOver<TabelaPreco>().Where(expressaoParaConsulta).List().ToList();
        }
    }
}
