using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{       
    public enum EnumStatusInventario
    {
        [Description("EM ABERTO")]
        ABERTO,

        [Description("CONSOLIDADO")]
        CONSOLIDADO
    }
}
