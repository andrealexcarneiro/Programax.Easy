using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumTipoInventario
    {
        [Description("CÍCLICO (ROTATIVO)")]
        CICLICO,

        [Description("PERMANENTE (CLÁSSICO)")]
        PERMANENTE
    }
}
