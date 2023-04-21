using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Estoque.Enumeradores
{
    public enum EnumCondicaoPagamentoNota
    {
        [Description("À VISTA")]
        AVISTA = 0,

        [Description("À PRAZO")]
        APRAZO = 1,

        [Description("OUTROS")]
        OUTROS = 2
    }
}
