using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumResultadoCaixa
    {
        [Description("SALDO CORRETO")]
        SALDOCORRETO,

        [Description("DIFERENÇA MENOR")]
        DIFERENCAMENOR,

        [Description("DIFERENÇA MAIOR")]
        DIFERENCAMAIOR
    }
}
