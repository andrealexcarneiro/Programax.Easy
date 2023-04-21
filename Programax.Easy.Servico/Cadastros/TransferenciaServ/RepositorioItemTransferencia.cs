using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    public class RepositorioItemTransferencia : RepositorioBase<ItemTransferencia>, IRepositorioItemTransferencia
    {
        public RepositorioItemTransferencia(ISession sessao)
            : base(sessao)
        {

        }
        public List<ItemTransferencia> ConsulteLista(int Id)
        {
            Expression<Func<ItemTransferencia, bool>> expressaoParaConsulta = itemTransferencia => itemTransferencia.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemTransferencia => itemTransferencia.SubEstoque == Id && itemTransferencia.QuantidadeEstoque != 0);

            return _session.QueryOver<ItemTransferencia>()
                .TransformUsing(Transformers.DistinctRootEntity)
                
                .Where(expressaoParaConsulta).List().ToList();
                
        }

        public List<ItemTransferencia> ConsulteProduto(int Id)
        {
            Expression<Func<ItemTransferencia, bool>> expressaoParaConsulta = itemTransferencia => itemTransferencia.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemTransferencia => itemTransferencia.produto == Id);

            return _session.QueryOver<ItemTransferencia>()
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(expressaoParaConsulta).List().ToList();
        }
    }
    
}
