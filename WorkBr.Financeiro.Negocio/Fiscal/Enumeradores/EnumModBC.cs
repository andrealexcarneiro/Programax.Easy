using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumModBC
    {
        [Description("MARGEM VALOR AGREGADO (%)")]
        MARGEMVALORAGREGADO = 0,

        [Description("PAUTA (VALOR)")]
        PAUTAVALOR = 1,

        [Description("PREÇO TABELADO MÁX. (VALOR)")]
        PRECOTABELADOMAXIMO = 2,

        [Description("VALOR DA OPERAÇÃO")]
        VALOROPERACAO = 3
    }
}
