using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumCstIpi
    {
        [Description("00 - Entrada com Recuperação de Crédito")]
        IPI00 = 0,

        [Description("01 - Entrada Tributável com Alíquota Zero")]
        IPI01 = 1,

        [Description("02 - Entrada Isenta")]
        IPI02 = 2,

        [Description("03 - Entrada Não-Tributada")]
        IPI03 = 3,

        [Description("04 - Entrada Imune")]
        IPI04 = 4,

        [Description("05 - Entrada com Suspensão")]
        IPI05 = 5,

        [Description("49 - Outras Entradas")]
        IPI49 = 6,

        [Description("50 - Saída Tributada")]
        IPI50 = 7,       

        [Description("51 - Saída Tributável com Alíquota Zero")]
        IPI51= 8,

        [Description("52 - Saída Isenta")]
        IPI52 = 9,

        [Description("53 - Saída Não-Tributada")]
        IPI53 = 10,

        [Description("54 - Saída Imune")]
        IPI54 = 11,

        [Description("55 - Saída com Suspensão")]
        IPI55 = 12,

        [Description("99 - Outras Saídas")]
        IPI99 = 13

    }
}
