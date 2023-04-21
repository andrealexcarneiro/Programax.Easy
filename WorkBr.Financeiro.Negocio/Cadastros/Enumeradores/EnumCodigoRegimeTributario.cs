using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumCodigoRegimeTributario
    {
        [Description("SIMPLES NACIONAL")]
        SIMPLESNACIONAL = 1,

        [Description("SIMPLES NACIONAL - EXCESSO DE SUBLIMITE DE RECEITA BRUTA")]
        SIMPLESNACIONALEXCESSOSUBLIMITERECEITABRUTA = 2,

        [Description("REGIME NORMAL")]
        REGIMENORMAL = 3
    }
}
