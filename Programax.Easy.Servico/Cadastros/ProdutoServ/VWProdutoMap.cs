using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    public class VWProdutoMap: MapeamentoBase<VWProduto>
    {
        public VWProdutoMap()
        {
            Table("VW_PRODUTOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VWPROD_ID");

            Map(vwProduto => vwProduto.CodigoBarras).Column("VWPROD_CODBARRAS");
            Map(vwProduto => vwProduto.Descricao).Column("VWPROD_DESCRICAO");
            Map(vwProduto => vwProduto.CodigoFiscal).Column("VWPROD_CFISC_ID");
            Map(vwProduto => vwProduto.Estoque).Column("VWPROD_ESTOQUE");
            Map(vwProduto => vwProduto.EstoqueReservado).Column("VWPROD_RESERVADO");
            Map(vwProduto => vwProduto.QtdMinima).Column("VWPROD_QTDMINIMA");
            Map(vwProduto => vwProduto.ValorVenda).Column("VWPROD_VALORVENDA");
            Map(vwProduto => vwProduto.ValorCompra).Column("VWPROD_VALOR_COMPRA");

            Map(vwProduto => vwProduto.PrecoCompra).Column("VWPROD_PRECO_COMPRA_ITEM");

            Map(vwProduto => vwProduto.CategoriaId).Column("VWPROD_CATEGORIA_ID");
            Map(vwProduto => vwProduto.CategoriaDescricao).Column("VWPROD_CATEGORIA_DESCRICAO");
            Map(vwProduto => vwProduto.GrupoId).Column("VWPROD_GRUPO_ID");
            Map(vwProduto => vwProduto.GrupoDescricao).Column("VWPROD_GRUPO_DESCRICAO");
            Map(vwProduto => vwProduto.SubGrupoId).Column("VWPROD_SUBGRUPO_ID");
            Map(vwProduto => vwProduto.SubGrupoDescricao).Column("VWPROD_SUBGRUPO_DESCRICAO");
            Map(vwProduto => vwProduto.MarcaId).Column("VWPROD_MARCA_ID");
            Map(vwProduto => vwProduto.MarcaDescricao).Column("VWPROD_MARCA_DESCRICAO");
            Map(vwProduto => vwProduto.FabricanteId).Column("VWPROD_FABRICANTE_ID");
            Map(vwProduto => vwProduto.FabricanteDescricao).Column("VWPROD_FABRICANTE_DESCRICAO");
            Map(vwProduto => vwProduto.TamanhoId).Column("VWPROD_TAMANHO_ID");
            Map(vwProduto => vwProduto.TamanhoDescricao).Column("VWPROD_TAMANHO_DESCRICAO");
            Map(vwProduto => vwProduto.UnidadeId).Column("VWPROD_UNIDADE_ID");
            Map(vwProduto => vwProduto.UnidadeDescricao).Column("VWPROD_UNIDADE_DESCRICAO");
            Map(vwProduto => vwProduto.DDV).Column("VWPROD_DDV");
        }
    }
}
