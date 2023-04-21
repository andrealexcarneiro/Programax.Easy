using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumPerfilCaixa
    {
        [Description("DIRETORIA")]
        DIRETORIA = 1,

        [Description("GERENTE GERAL")]
        GERENTEGERAL = 2,

        [Description("GERENTE DE CAIXA")]
        GERENTECAIXA = 3,

        [Description("CAIXA")]
        CAIXA = 4
    }
}
