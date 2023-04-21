using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumCstCsosn
    {
        [Description("00 - Tributada Integralmente")]
        NORMAL00 = 0,

        [Description("10 - Tributada e com cobrança do ICMS por Substituição Tributária")]
        NORMAL10 = 10,

        [Description("20 - Com redução de base de cálculo")]
        NORMAL20 = 20,

        [Description("30 - Isenta ou Não Tributada e com cobrança de ICMS por Substituição Tributária")]
        NORMAL30 = 30,

        [Description("40 - Isenta")]
        NORMAL40 = 40,

        [Description("41 - Não Tributada")]
        NORMAL41 = 41,

        [Description("50 - Suspensão")]
        NORMAL50 = 50,

        [Description("51 - Diferimento")]
        NORMAL51 = 51,

        [Description("60 - ICMS cobrado anteriormente por Substituição Tributária")]
        NORMAL60 = 60,

        [Description("70 - Com redução de base de cálculo e cobrança de ICMS por Substituição Tributária")]
        NORMAL70 = 70,

        [Description("90 - Outras")]
        NORMAL90 = 90,

        [Description("101 - Simples - Tributada pelo Simples Nacional com Permissão de Crédito")]
        SIMPLES101 = 101,

        [Description("102 - Simples - Tributada pelo Simples Nacional sem Permissao de Crédito")]
        SIMPLES102 = 102,

        [Description("103 - Simples - Isenção do ICMS no Simples Nacional para Faixa de Receita Bruta")]
        SIMPLES103 = 103,

        [Description("201 - Simples - Tributada pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por ST")]
        SIMPLES201 = 201,

        [Description("202 - Simples - Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por ST")]
        SIMPLES202 = 202,

        [Description("203 - Simples - Isenção do ICMS do Simples Nacional para Faixa de Receita Bruta e com cobrança do ICMS por ST")]
        SIMPLES203 = 203,

        [Description("300 - Simples - Imune")]
        SIMPLES300 = 300,

        [Description("400 - Simples - Não tributada pelo Simples Nacional")]
        SIMPLES400 = 400,

        [Description("500 - Simples - ICMS cobrado anteriormente por Substituição Tributária (Substituído) ou por antecipação")]
        SIMPLES500 = 500,

        [Description("900 - Simples - Outras")]
        SIMPLES900 = 900
    }
}
