using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Vendas.Enumeradores
{
    public enum EnumTipoPedidoDeVenda
    {
        [Description("ORÇAMENTO")]
        ORCAMENTO,

        [Description("PEDIDO DE VENDA")]
        PEDIDOVENDA,

        [Description("CONSIGNADO")]
        CONSIGNADO,

        [Description("PERMUTA")]
        PERMUTA
    }
}
