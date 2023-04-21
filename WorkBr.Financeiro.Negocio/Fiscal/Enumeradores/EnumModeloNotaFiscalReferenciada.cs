using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumModeloNotaFiscalReferenciada
    {
        [Description("01 - NF")]
        MODELO01,

        [Description("2B - CUPOM FISCAL EMITIDO POR MÁQUINA REGISTRADORA (NÃO ECF);")]
        MODELO2B,

        [Description("2C - CUPOM FISCAL PDV")]
        MODELO2C,

        [Description("2D - CUPOM FISCAL (EMITIDO POR ECF)")]
        MODELO2D,

        [Description("04 - NF DE PRODUTOR")]
        MODELO04
    }
}
