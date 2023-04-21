using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumSituacaoTributariaProduto
    {
        [Description("ISENTO")]
        ISENTO,

        [Description("NÃO TIRBUTADO")]
        NAOTRIBUTADO,

        [Description("SUBSTITUIÇÃO TRIBUTÁRIA")]
        SUBSTITUICAOTRIBUTARIA,

        [Description("TRIBUTADA PELO ICMS")]
        TRIBUTADAPELOICMS,

        [Description("TRIBUTADA PELO ISSQN")]
        TRIBUTADAPELOISSQN,

        [Description("ISENTO ISSQN")]
        ISENTOISSQN
    }
}
