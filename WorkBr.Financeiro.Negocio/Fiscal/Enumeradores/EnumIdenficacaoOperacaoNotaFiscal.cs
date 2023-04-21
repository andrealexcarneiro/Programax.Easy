using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumIdenficacaoOperacaoNotaFiscal
    {
        [Description("OPERAÇÃO INTERNA")]
        OPERACAOINTERNA = 1,

        [Description("OPERAÇÃO INTERESTADUAL")]
        OPERACAOINTERESTADUAL = 2,

        [Description("OPERAÇÃO COM EXTERIOR")]
        OPERACAOCOMEXTERIOR = 3
    }
}
