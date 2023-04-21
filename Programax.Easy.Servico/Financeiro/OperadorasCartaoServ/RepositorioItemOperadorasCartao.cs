using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Financeiro.OperadorasCartaoServ
{
    public class RepositorioItemOperadorasCartao : RepositorioBase<ItemOperadorasCartao>, IRepositorioItemOperadorasCartao
    {
        public RepositorioItemOperadorasCartao(ISession sessao)
            : base(sessao)
        {

        }
        public List<ItemOperadorasCartao> ConsulteLista(int idOperadora)
        {
            Expression<Func<ItemOperadorasCartao, bool>> expressaoParaConsulta = itemoperadora => itemoperadora.OperadorasId==idOperadora;


            return _session.QueryOver<ItemOperadorasCartao>()
                                       .TransformUsing(Transformers.DistinctRootEntity)
                                       .Where(expressaoParaConsulta).List().ToList();
            
        }
    }
}
