using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumEstadoCivil
    {
        [Description("SOLTEIRO(A)")]
        SOLTEIRO,

        [Description("CASADO(A)")]
        CASADO,

        [Description("DIVORCIADO(A)")]
        DIVORCIADO,

        [Description("VIUVO(A)")]
        VIUVO,

        [Description("SEPARAÇÃO JUDICIAL")]
        SEPARACAOJUDICIAL,

        [Description("AMASIADO(A)")]
        AMASIADO
    }
}
