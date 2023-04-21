using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumFiltroSituacao
    {
        [Description("TODOS OS SALDOS")]
        TODOSOSSALDOS,

        [Description("SALDO ZERADO")]
        SALDOZERADO,

        [Description("SALDO POSITIVO")]
        SALDOPOSITIVO,

        [Description("SALDO NEGATIVO")]
        SALDONEGATIVO
    }
}
