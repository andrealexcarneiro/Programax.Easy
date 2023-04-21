using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumModBCST
    {
        [Description("PREÇO TABELADO OU MÁXIMO SUGERIDO")]
        PRECOTABELADOOUMAXIMOSUGERIDO = 0,

        [Description("LISTA NEGATIVA (VALOR)")]
        LISTANEGATIVAVALOR = 1,

        [Description("LISTA POSITIVA (VALOR)")]
        LISTAPOSITIVAVALOR = 2,

        [Description("LISTA NEUTRA (VALOR)")]
        LISTANEUTRAVALOR = 3,

        [Description("MARGEM VALOR AGREGADO (%)")]
        MARGEMVALORAGREGADOPERCENTUAL = 4,

        [Description("PAUTA (VALOR) (V2.0)")]
        PAUTAVALORV20 = 5
    }
}
