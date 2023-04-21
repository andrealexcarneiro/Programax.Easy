using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using System.Collections.Generic;
using System;
using System.Linq;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.CategoriaServ
{
    public class RepositorioCategoria : RepositorioBase<CategoriaFinanceira>, IRepositorioCategoria
    {
        public RepositorioCategoria(ISession sessao)
            : base(sessao)
        {

        }

        public List<CategoriaFinanceira> ConsulteLista(string descricao, SubGrupoCategoria grupoCategoria, string status, EnumTipoCategoria? tipoCategoria = null)
        {
            Expression<Func<CategoriaFinanceira, bool>> expressaoParaConsulta = cat => cat.Descricao.IsLike("%" + descricao + "%");
            
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

            return _session.QueryOver<CategoriaFinanceira>().Where(expressaoParaConsulta).List().ToList();
        }

        public List<CategoriaFinanceira> ConsulteListaAtivos(SubGrupoCategoria subGrupoCategoria)
        {
            subGrupoCategoria = subGrupoCategoria ?? new SubGrupoCategoria();

            return _session.QueryOver<CategoriaFinanceira>().Where(cat => cat.SubGrupoCategoria.Id == subGrupoCategoria.Id && cat.Status == "A").List().ToList();
        }

        public List<CategoriaFinanceira> ConsulteListaAtivos()
        {
            return _session.QueryOver<CategoriaFinanceira>().Where(cat => cat.Status == "A").List().ToList();
        }
    }
}
