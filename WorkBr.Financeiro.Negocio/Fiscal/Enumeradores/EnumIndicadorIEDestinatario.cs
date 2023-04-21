using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumIndicadorIEDestinatario
    {
        [Description("CONTRIBUINTE ICMS PAGAMENTO À VISTA")]
        CONTRIBUINTEICMS = 1,

        [Description("ISENTO")]
        ISENTO = 2,

        [Description("NÃO CONTRIBUINTE")]
        NAOCONTRIBUINTE = 9
    }
}
