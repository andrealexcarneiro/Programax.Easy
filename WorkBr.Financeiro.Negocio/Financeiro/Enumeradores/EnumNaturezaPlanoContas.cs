using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumNaturezaPlanoContas
    {
        [Description("RECEITA")]
        RECEITA,

        [Description("DESPESA")]
        DESPESA,

        [Description("TRANSFERÊNCIA")]
        TRANSFERENCIA
    }
}
