using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ
{
    public class ItemTrocaPedidoDeVendaMap : MapeamentoBase<ItemTrocaPedidoDeVenda>
    {
        public ItemTrocaPedidoDeVendaMap()
        {
            Table("TROCA_PEDIDO_VENDA_ITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEM_ID");

            Map(item => item.Desconto).Column("ITEM_DESCONTO");
            Map(item => item.DescontoEhPercentual).Column("ITEM_DESCONTO_EH_PERCENTUAL");
            Map(item => item.Quantidade).Column("ITEM_QUANTIDADE");
            Map(item => item.ValorTotal).Column("ITEM_VALOR_TOTAL");
            Map(item => item.ValorUnitario).Column("ITEM_VALOR_UNITARIO");
            Map(item => item.ItemEstahInconsistente).Column("ITEM_ESTAH_INCONSISTENTE");

            References(item => item.TrocaPedidoDeVenda).Column("ITEM_TROCA_PEDIDO_ID");
            References(item => item.Produto).Column("ITEM_PRODUTO_ID");
        }
    }
}
