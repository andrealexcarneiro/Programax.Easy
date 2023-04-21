using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ
{
    public class RepositorioMotivoTrocaPedidoDeVenda : RepositorioBase<MotivoTrocaPedidoDeVenda>, IRepositorioMotivoTrocaPedidoDeVenda
    {
        public RepositorioMotivoTrocaPedidoDeVenda(ISession sessao)
            : base(sessao)
        {

        }

        public List<MotivoTrocaPedidoDeVenda> ConsulteLista(string descricao, string status)
        {
            Expression<Func<MotivoTrocaPedidoDeVenda, bool>> expressaoParaConsulta = motivo => motivo.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(motivo => motivo.Status == status);
            }

            return _session.QueryOver<MotivoTrocaPedidoDeVenda>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<MotivoTrocaPedidoDeVenda> ConsulteListaAtiva()
        {
            return _session.QueryOver<MotivoTrocaPedidoDeVenda>().Where(motivo => motivo.Status == "A").List().ToList();
        }
    }
}
