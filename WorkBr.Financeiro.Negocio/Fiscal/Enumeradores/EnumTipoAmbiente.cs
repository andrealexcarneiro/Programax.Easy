using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumTipoAmbiente
    {
        [Description("PRODUÇÃO")]
        PRODUCAO = 1,

        [Description("HOMOLOGAÇÃO")]
        HOMOLOGACAO = 2
    }
}
