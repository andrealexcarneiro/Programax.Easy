using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumTipoNotaReferenciada
    {
        [Description("NF-E OU NFCE")]
        NFEOUNFCE,

        [Description("NOTA FISCAL MODELO 1/1A")]
        NOTAFISCAL1A,

        [Description("NOTA FISCAL DE PRODUTOR RURAL")]
        NOTAFISCALPRODUTORRURAL,

        [Description("CUPOM FISCAL")]
        CUPOMFISCAL
    }
}
