using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumTipoEndereco
    {
        [Description("PRINCIPAL")]
        PRINCIPAL,

        [Description("RESIDENCIAL")]
        RESIDENCIAL,

        [Description("COBRANÇA")]
        COBRANCA,

        [Description("ENTREGA")]
        ENTREGA,

        [Description("ESCRITÓRIO")]
        ESCRITORIO
    }
}
