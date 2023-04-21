using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumOrigemConciliacaoBancaria
    {
        [Description("A PAGAR")]
        APAGAR,

        [Description("A RECEBER")]
        ARECEBER,

        [Description("MOVIMENTACAO_CR")]
        MOVIMENTACAOBANCARIACR,

        [Description("MOVIMENTACAO_DB")]
        MOVIMENTACAOBANCARIADB,

        [Description("EXT. BANCÁRIO_CR")]
        EXTRATOBANCARIOCR,

        [Description("EXT. BANCÁRIO_DB")]
        EXTRATOBANCARIODB,
    }
}
