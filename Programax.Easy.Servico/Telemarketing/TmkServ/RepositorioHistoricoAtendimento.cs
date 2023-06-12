using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate.Transform;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.TeleMarketing.TeleMarketingObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Telemarketing.TmkServ
{
    public class RepositorioHistoricoAtendimento : RepositorioBase<HistoricoAtendimento>, IRepositorioHistoricoAtendimento
    {
        public RepositorioHistoricoAtendimento(ISession sessao)
            : base(sessao)
        {
        }

        public List<HistoricoAtendimento> ConsulteLista(int idPedido)
        {
            Expression<Func<HistoricoAtendimento, bool>> expressaoParaConsulta = pedido => pedido.Pedido.Id == idPedido;
                        
            return _session.QueryOver<HistoricoAtendimento>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }

        public List<HistoricoAtendimento> ConsulteListaCliente(int idCliente)
        {
            Expression<Func<HistoricoAtendimento, bool>> expressaoParaConsulta = pedido => pedido.codCliente == idCliente;

            return _session.QueryOver<HistoricoAtendimento>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
