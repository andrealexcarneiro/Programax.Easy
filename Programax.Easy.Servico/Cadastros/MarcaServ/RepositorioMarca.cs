using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.Repositorio;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Cadastros.MarcaServ
{
    public class RepositorioMarca : RepositorioBase<Marca>, IRepositorioMarca
    {
        public RepositorioMarca(ISession sessao)
            : base(sessao)
        {

        }

        public List<Marca> ConsulteLista(int? idMarca, string descricao, string status)
        {
            Expression<Func<Marca, bool>> expressaoParaConsulta = marca => marca.Descricao.IsLike("%" + descricao + "%");

            if (!string.IsNullOrEmpty(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(marca => marca.Ativo == status);
            }

            if (idMarca != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(ncm => ncm.Id == idMarca.Value);
            }

            return _session.QueryOver<Marca>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<Marca> ConsulteListaAtiva()
        {
            return _session.QueryOver<Marca>().Where(marca => marca.Ativo == "A").List().ToList();
        }
    }
}
