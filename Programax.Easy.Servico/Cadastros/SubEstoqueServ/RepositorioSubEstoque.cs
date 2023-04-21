using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubEstoqueObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.SubEstoqueServ
{
    public class RepositorioSubEstoque : RepositorioBase<SubEstoque>, IRepositorioSubEstoque
    {
        public RepositorioSubEstoque(ISession sessao)
            : base(sessao)
        {

        }

        public List<SubEstoque> ConsulteLista(int? id, string descricao, string status)
        {
            Expression<Func<SubEstoque, bool>> expressaoParaConsulta = subestoque => subestoque.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(subestoque => subestoque.Ativo == status);
            }

            if (id != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(ncm => ncm.Id == id.Value);
            }

            return _session.QueryOver<SubEstoque>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<SubEstoque> ConsulteListaAtiva()
        {
            return _session.QueryOver<SubEstoque>().Where(subestoque => subestoque.Ativo == "A").List().ToList();
        }
    }
}
