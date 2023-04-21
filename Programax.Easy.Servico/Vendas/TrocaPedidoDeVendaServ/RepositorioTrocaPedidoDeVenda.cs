using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.Repositorio;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using System.Linq.Expressions;
using NHibernate.Transform;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ
{
    public class RepositorioTrocaPedidoDeVenda : RepositorioBase<TrocaPedidoDeVenda>, IRepositorioTrocaPedidoDeVenda
    {
        public RepositorioTrocaPedidoDeVenda(ISession sessao)
            : base(sessao)
        {

        }

        public List<TrocaPedidoDeVenda> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, PedidoDeVenda pedidoDeVenda, EnumStatusTrocaPedidoDeVenda? statusTrocaPedidoDeVenda)
        {
            Expression<Func<TrocaPedidoDeVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(troca => troca.DataElaboracao >= dataInicial.Value);
            }

            if (dataFinal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(troca => troca.DataElaboracao <= dataFinal.Value);
            }

            if (statusTrocaPedidoDeVenda != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(troca => troca.Status == statusTrocaPedidoDeVenda.Value);
            }

            if (pedidoDeVenda != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(troca => troca.PedidoDeVenda.Id == pedidoDeVenda.Id);
            }

            return _session.QueryOver<TrocaPedidoDeVenda>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta).List().ToList();
        }

        public void AtualizeEstornoDeTroca(TrocaPedidoDeVenda troca)
        {
            _session.Update(troca);    
        }
    }
}
