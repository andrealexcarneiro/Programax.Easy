using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumTipoMovimentacaoNaturezaOperacao
    {
        [Description("ENTRADA")]
        ENTRADA,

        [Description("SAÍDA")]
        SAIDA
    }
}
