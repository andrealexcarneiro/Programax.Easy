using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Vendas.Enumeradores
{
    public enum EnumStatusTrocaPedidoDeVenda
    {
        ABERTO,

        CACNCELADO,

        RECUSADO,

        RESERVADO,

        FATURADO,

        [Description("EM LIBERAÇÃO")]
        EMLIBERACAO
    }
}
