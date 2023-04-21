using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.InventarioObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.MarcaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.InventarioServ
{
    public class RepositorioInventario : RepositorioBase<Inventario>, IRepositorioInventario
    {
        public RepositorioInventario(ISession sessao)
            : base(sessao)
        {

        }

        public bool ConsulteProdutoEstahEmInventario(Produto produto)
        {
            ItemInventario item = null;

            return _session.QueryOver<Inventario>()
                .Left.JoinAlias(inventario => inventario.ListaDeItens, () => item)
                .Where(inventario => inventario.Status == EnumStatusInventario.ABERTO && item.Produto.Id == produto.Id)
                .RowCount() > 0;
        }

        public List<Inventario> ConsulteLista(DateTime? dataInicio, EnumStatusInventario? statusInventario, int? contagem, Marca marca, Categoria categoria, Grupo grupo, SubGrupo subGrupo)
        {
            Expression<Func<Inventario, bool>> expressaoParaConsulta = inventario => inventario.Id > 0;

            if (dataInicio != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.DataInicio >= dataInicio);
            }

            if (statusInventario != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.Status == statusInventario);
            }

            if (contagem != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.ContagemAtual == contagem);
            }

            if (marca != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.Marca.Id == marca.Id);
            }

            if (categoria != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.Categoria.Id == categoria.Id);
            }

            if (grupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.Grupo.Id == grupo.Id);
            }

            if (subGrupo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.SubGrupo.Id == subGrupo.Id);
            }

            return _session.QueryOver<Inventario>()
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
