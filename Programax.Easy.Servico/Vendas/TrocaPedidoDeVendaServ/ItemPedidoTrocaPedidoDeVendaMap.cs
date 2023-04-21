using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ
{
    public class ItemPedidoTrocaPedidoDeVendaMap : MapeamentoBase<ItemPedidoTrocaPedidoDeVenda>
    {
        public ItemPedidoTrocaPedidoDeVendaMap()
        {
            Table("TROCA_PEDIDO_VENDA_ITENS_PEDIDO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEM_ID");

            Map(item => item.Desconto).Column("ITEM_DESCONTO");
            Map(item => item.Quantidade).Column("ITEM_QUANTIDADE");
            Map(item => item.QuantidadeTrocar).Column("ITEM_QUANTIDADE_TROCAR");
            Map(item => item.ValorTotal).Column("ITEM_VALOR_TOTAL");
            Map(item => item.ValorUnitario).Column("ITEM_VALOR_UNITARIO");

            References(item => item.TrocaPedidoDeVenda).Column("ITEM_TROCA_PEDIDO_ID");
            References(item => item.Produto).Column("ITEM_PRODUTO_ID");
        }
    }
}
