using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio;
using NHibernate;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Cadastros.NaturezaOperacaoServ
{
    public class RepositorioNaturezaOperacao : RepositorioBase<NaturezaOperacao>, IRepositorioNaturezaOperacao
    {
        public RepositorioNaturezaOperacao(ISession sessao)
            : base(sessao)
        {

        }

        public List<NaturezaOperacao> ConsulteListaAtiva()
        {
            return _session.QueryOver<NaturezaOperacao>().Where(naturezaOperacao => naturezaOperacao.Status == "A").List().ToList();
        }

        public List<NaturezaOperacao> ConsulteLista(int? id, string descricao, string status)
        {
            Expression<Func<NaturezaOperacao, bool>> expressaoParaConsulta = natureza => natureza.Descricao.IsLike("%" + descricao + "%");

            if (id != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(natureza => natureza.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(natureza => natureza.Status == status);
            }

            return _session.QueryOver<NaturezaOperacao>().Where(expressaoParaConsulta).List().ToList();
        }


        public NaturezaOperacao ConsulteNaturezaOperacaoPorCfop(string codigoCfop)
        {
            NaturezaOperacaoCfop naturezaOperacaoCfop = null;
            Cfop cfop = null;

            return _session.QueryOver<NaturezaOperacao>()
                .Left.JoinAlias(natureza => natureza.ListaCfops, () => naturezaOperacaoCfop)
                .Left.JoinAlias(natureza => naturezaOperacaoCfop.Cfop, () => cfop)
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(natureza => cfop.Codigo == codigoCfop)
                .Take(1).SingleOrDefault();
        }
    }
}
