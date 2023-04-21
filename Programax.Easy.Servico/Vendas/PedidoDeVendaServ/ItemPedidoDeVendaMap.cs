using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    public class ItemPedidoDeVendaMap : MapeamentoBase<ItemPedidoDeVenda>
    {
        public ItemPedidoDeVendaMap()
        {
            Table("PEDIDOSVENDASITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PEDITEM_ID");

            Map(item => item.DescontoUnitario).Column("PEDITEM_DESCONTO_UNITARIO");
            Map(item => item.DescontoEhPercentual).Column("PEDITEM_DESCONTO_EH_PERCENTUAL");
            Map(item => item.TotalDesconto).Column("PEDITEM_TOTAL_DESCONTO");
            Map(item => item.Quantidade).Column("PEDITEM_QUANTIDADE");
            Map(item => item.QuantidadeDevolvida).Column("PEDITEM_QUANTIDADE_DEVOLVIDA");
            Map(item => item.ValorTotal).Column("PEDITEM_VALOR_TOTAL");
            Map(item => item.ValorUnitario).Column("PEDITEM_VALOR_UNITARIO");
            Map(item => item.ValorFrete).Column("PEDITEM_VALOR_FRETE");
            Map(item => item.ValorIcmsST).Column("PEDITEM_VALOR_ICMS_ST");
            Map(item => item.ValorIpi).Column("PEDITEM_VALOR_IPI");
            Map(item => item.ItemEstahInconsistente).Column("PEDITEM_ESTAH_INCONSISTENTE");

            References(item => item.PedidoDeVenda).Column("PEDITEM_PEDIDO_ID");
            References(item => item.Produto).Column("PEDITEM_PRODUTO_ID").Not.LazyLoad().Fetch.Join();
        }
    }
}
