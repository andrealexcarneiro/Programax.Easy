using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate.Transform;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Vendas.RoteiroServ
{
    public class RepositorioHistoricoRoteiro : RepositorioBase<HistoricoRoteiro>, IRepositorioHistoricoRoteiro
    {
        public RepositorioHistoricoRoteiro(ISession sessao)
            : base(sessao)
        {
        }

        public List<HistoricoRoteiro> ConsulteLista(int idRoteiro)
        {
            Expression<Func<HistoricoRoteiro, bool>> expressaoParaConsulta = roteiro => roteiro.Roteirizacao.Id == idRoteiro;
                        
            return _session.QueryOver<HistoricoRoteiro>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }

    }
}
