using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumStatusCheque
    {
        [Description("ABERTO/DEPOSITADO")]
        ABERTODEPOSITADO,

        [Description("RECEBIDO")]
        RECEBIDO,

        [Description("DEVOLVIDO 1ª")]
        DEVOLVIDO1,

        [Description("DEVOLVIDO 2ª")]
        DEVOLVIDO2,

        [Description("REAPRESENTADO")]
        REAPRESENTADO,

        [Description("CUSTODIADO/FACTORING")]
        CUSTODIADOFACTORING,

        [Description("INATIVO")]
        INATIVO
    }
}
