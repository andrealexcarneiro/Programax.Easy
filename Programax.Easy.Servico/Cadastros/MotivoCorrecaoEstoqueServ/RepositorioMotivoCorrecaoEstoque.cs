using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Infraestrutura.Negocio.Utils;
using System.Linq.Expressions;
using System;

namespace Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ
{
    public class RepositorioMotivoCorrecaoEstoque: RepositorioBase<MotivoCorrecaoEstoque>, IRepositorioMotivoCorrecaoEstoque
    {
        public RepositorioMotivoCorrecaoEstoque(ISession sessao)
            : base(sessao)
        {

        }

        public List<MotivoCorrecaoEstoque> ConsulteLista(string descricao, string status)
        {
            Expression<Func<MotivoCorrecaoEstoque, bool>> expressaoParaConsulta = motivo => motivo.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(motivo => motivo.Status == status);
            }

            return _session.QueryOver<MotivoCorrecaoEstoque>().Where(expressaoParaConsulta).List().ToList();
        }
    }
}
