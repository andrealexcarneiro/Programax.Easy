using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.CategoriaServ
{
    public class RepositorioCategoria : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoria(ISession sessao)
            : base(sessao)
        {
        }

        public List<Categoria> ConsulteLista(string descricao, string status)
        {
            return _session.QueryOver<Categoria>().Where(categoria => categoria.Descricao.IsLike("%" + descricao + "%") &&
                                                                                                     categoria.Status == status).List<Categoria>().ToList();
        }


        public List<Categoria> ConsulteListaAtiva()
        {
            return _session.QueryOver<Categoria>().Where(categoria => categoria.Status == "A").List<Categoria>().ToList();
        }
    }
}
