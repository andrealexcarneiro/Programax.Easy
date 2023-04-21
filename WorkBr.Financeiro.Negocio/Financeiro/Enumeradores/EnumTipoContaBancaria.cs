using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumTipoContaBancaria
    {
        [Description("Conta Corrente")]
        CONTACORRENTE,

        [Description("Conta Poupança")]
        CONTAPOUPANCA,

        [Description("Conta Investimento")]
        CONTAINVESTIMENTO
    }
}
