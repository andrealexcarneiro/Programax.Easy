using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio;
using Programax.Easy.Negocio.Financeiro.CategoriaDreObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.CategoriaDreServ
{
    public class RepositorioCategoriaDre : RepositorioBase<CategoriaDre>, IRepositorioCategoriaDre
    {
        public RepositorioCategoriaDre(ISession sessao)
           : base(sessao)
        {

        }

        public List<CategoriaDre> ConsulteLista(string descricao, SubGrupoCategoria grupoCategoria, string status, EnumTipoCategoria? tipoCategoria = null)
        {
            Expression<Func<CategoriaDre, bool>> expressaoParaConsulta = cat => cat.Descricao.IsLike("%" + descricao + "%");

            if (tipoCategoria != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cat => cat.TipoCategoria == tipoCategoria);
            }

            if (grupoCategoria != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cat => cat.SubGrupoCategoria.Id == grupoCategoria.Id);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cat => cat.Status == status);
            }

            return _session.QueryOver<CategoriaDre>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<CategoriaDre> ConsulteListaAtivos(SubGrupoCategoria subGrupoCategoria)
        {
            subGrupoCategoria = subGrupoCategoria ?? new SubGrupoCategoria();

            return _session.QueryOver<CategoriaDre>().Where(cat => cat.SubGrupoCategoria.Id == subGrupoCategoria.Id && cat.Status == "A").List().ToList();
        }

        public List<CategoriaDre> ConsulteListaAtivos()
        {
            return _session.QueryOver<CategoriaDre>().Where(cat => cat.Status == "A").List().ToList();
        }
    }
}
