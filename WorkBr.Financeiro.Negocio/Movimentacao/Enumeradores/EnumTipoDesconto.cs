using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Movimentacao.Enumeradores
{
    public enum EnumTipoDesconto
    {
        [Description("Valor")]
        Valor = 0,

        [Description("Percentual")]
        Percentual = 1
    }
}
