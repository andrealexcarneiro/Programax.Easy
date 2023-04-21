using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.ProdutoServ
{
    public class VWProdutoPdvMap: MapeamentoBase<VWProdutoPdv>
    {
        public VWProdutoPdvMap()
        {
            Table("VW_PRODUTOS_PDV");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("VWPROD_ID");

            Map(vwProduto => vwProduto.CodigoDeBarras).Column("VWPROD_CODBARRAS");
            Map(vwProduto => vwProduto.Descricao).Column("VWPROD_DESCRICAO");
            Map(vwProduto => vwProduto.EhPromocao).Column("VWPROD_EH_PROMOCAO");
            Map(vwProduto => vwProduto.Estoque).Column("VWPROD_ESTOQUE");
            Map(vwProduto => vwProduto.EstoqueReservado).Column("VWPROD_ESTOQUE_RESERVADO");
            Map(vwProduto => vwProduto.ValorPromocao).Column("VWPROD_VALOR_PROMOCAO");
            Map(vwProduto => vwProduto.ValorVenda).Column("VWPROD_VALORVENDA");
            Map(vwProduto => vwProduto.Foto).Column("VWPROD_FOTO");
        }
    }
}
