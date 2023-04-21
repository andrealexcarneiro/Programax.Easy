using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumTipoMovimentacaoBanco
    {
        [Description("ENTRADA - RECEITA")]
        ENTRADA,

        [Description("SAÍDA - DESPESA")]
        SAIDA,
        
        [Description("TODAS")]
        TODAS
    }
}
