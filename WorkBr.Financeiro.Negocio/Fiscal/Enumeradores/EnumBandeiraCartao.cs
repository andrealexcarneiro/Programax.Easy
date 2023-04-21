using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumBandeiraCartao
    {
        VISA = 1,

        MASTERCARD = 2,

        [Description("AMERICAN EXPRESS")]
        AMERICANEXPRESS = 3,

        SOROCRED = 4,

        OUTROS = 99
    }
}
