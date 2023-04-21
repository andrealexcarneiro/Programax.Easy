using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumTipoMovimentacaoCaixa
    {
        [Description("ENTRADA - ACRÉSCIMO")]
        ENTRADAACRESCIMO,

        [Description("SAÍDA - SANGRIA")]
        SAIDASANGRIA,

        [Description("SAÍDA - PAGTO DESPESAS")]
        SAIDADESPESAS,

        [Description("SAÍDA - TROCO")]
        SAIDATROCO,

        [Description("SAÍDA - OUTRAS SAÍDAS")]
        OUTRASSAIDAS,

    }
}
