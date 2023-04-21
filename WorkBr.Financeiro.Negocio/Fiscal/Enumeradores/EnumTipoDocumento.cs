using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumTipoDocumento
    {
        [Description("PEDIDO DE VENDAS")]
        PEDIDODEVENDAS,

        [Description("DEVOLUÇÃO")]
        DEVOLUCAO,

        [Description("OUTRAS SAÍDAS")]
        OUTRASSAIDAS
    }
}
