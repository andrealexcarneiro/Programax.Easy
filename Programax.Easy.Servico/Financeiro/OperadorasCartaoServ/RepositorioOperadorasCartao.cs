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
    public class RepositorioOperadorasCartao : RepositorioBase<OperadorasCartao>, IRepositorioOperadorasCartao
    {
        public RepositorioOperadorasCartao(ISession sessao)
            : base(sessao)
        {

        }
        public List<OperadorasCartao> ConsulteLista(string descricao, string status)
        {
            Expression<Func<OperadorasCartao, bool>> expressaoParaConsulta = operadora => operadora.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(operadora => operadora.Status == status);
            }


            return _session.QueryOver<OperadorasCartao>()
                                       .TransformUsing(Transformers.DistinctRootEntity)
                                       .Where(expressaoParaConsulta).List().ToList();

            //return _session.QueryOver<OperadorasCartao>().Where(expressaoParaConsulta).List().ToList();
        }

        public OperadorasCartao ConsulteOperadorasPeloIdInformado(int idOperadora)
        {
            return _session.QueryOver<OperadorasCartao>()
                                        .Where(operdadora => operdadora.Id == idOperadora).SingleOrDefault();
        }
    }
}
