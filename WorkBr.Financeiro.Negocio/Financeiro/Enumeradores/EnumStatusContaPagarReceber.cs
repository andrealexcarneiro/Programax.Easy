using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumStatusContaPagarReceber
    {
        ABERTO,

        [Description("ABERTO PRORROGADO")]
        ABERTOPRORROGADO,

        QUITADO,

        INATIVO,

        CANCELADO,

        [Description("ABERTO PROTESTADO")]
        ABERTOPROTESTADO,

        [Description("CONCILIADO/QUITADO")]
        CONCILIADOQUITADO,

        [Description("NÃO CONCILIADO")]
        NAOCONCILIADO
    }
}
