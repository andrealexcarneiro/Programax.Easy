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
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TransferenciaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;

namespace Programax.Easy.Servico.Cadastros.TransferenciaServ
{
    public class RepositorioTransferencia : RepositorioBase<Transferencia>, IRepositorioTransferencia
    {
        public RepositorioTransferencia(ISession sessao)
            : base(sessao)
        {

        }

        public bool ConsulteProdutoEstahEmInventario(Produto produto)
        {
            ItemTransferencia item = null;

            return _session.QueryOver<Transferencia>()
                //.Left.JoinAlias(inventario => inventario.ListaDeItens, () => item)
                //.Where(inventario => inventario.Status == EnumStatusInventario.ABERTO && item.Produto.Id == produto.Id)
                .RowCount() > 0;
        }

        public List<Transferencia> ConsulteLista(DateTime? dataInicio, EnumStatusInventario? statusInventario)
        {
            Expression<Func<Transferencia, bool>> expressaoParaConsulta = inventario => inventario.Id > 0;

            if (dataInicio != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.DataInicio >= dataInicio);
            }

            if (statusInventario != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(inventario => inventario.Status == statusInventario);
            }


            return _session.QueryOver<Transferencia>()
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
