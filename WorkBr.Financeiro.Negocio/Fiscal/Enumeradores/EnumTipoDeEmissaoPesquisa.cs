using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumTipoDeEmissaoPesquisa
    {
        [Description("NORMAL")]
        NORMAL,

        [Description("CONTIGENCIA")]
        CONTIGENCIA
    }
}
