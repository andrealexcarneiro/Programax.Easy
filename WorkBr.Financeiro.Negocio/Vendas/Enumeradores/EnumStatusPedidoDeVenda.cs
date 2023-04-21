using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Vendas.Enumeradores
{
    public enum EnumStatusPedidoDeVenda
    {
        [Description("ABERTO")]
        ABERTO,

        [Description("RESERVADO")]
        RESERVADO,

        [Description("PAGO")]
        FATURADO,

        [Description("CANCELADO")]
        CANCELADO,

        [Description("ORÇAMENTO")]
        ORCAMENTO,

        [Description("EM LIBERAÇÃO")]
        EMLIBERACAO,

        [Description("RECUSADO")]
        RECUSADO,

        [Description("EMITIDO NFE")]
        EMITIDONFE
    }
}
