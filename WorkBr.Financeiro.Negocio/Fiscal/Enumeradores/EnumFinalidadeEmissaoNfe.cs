using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumFinalidadeEmissaoNfe
    {
        [Description("NF-E NORMAL")]
        NFENORMAL = 1,

        [Description("NF-E COMPLEMENTAR")]
        NFECOMPLEMENTAR = 2,

        [Description("NF-E DE AJUSTE")]
        NFEAJUSTE = 3,

        [Description("DEVOLUÇÃO DE MERCADORIA")]
        NFEDEVOLUCAO = 4
    }
}
