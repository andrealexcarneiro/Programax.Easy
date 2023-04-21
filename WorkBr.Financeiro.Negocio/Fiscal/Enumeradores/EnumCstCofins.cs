using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumCstCofins
    {
        [Description("COFINS 01 - Operação Tributável - Base de Cálculo = Valor da Operação Alíquota Normal (Cumulativo / Não Cumulativo)")]
        cofins01,

        [Description("COFINS 02 - Operação Tributável - Base de Cálculo = Valor da Operação(Alíquota Diferenciada)")]
        cofins02,

        [Description("COFINS 03 - Operação Tributável - Base de Cálculo = Quantidade Vendida x Alíquota por Unidade de Produto")]
        cofins03,

        [Description("COFINS 04 - Operação Tributável - Tributação Monofásica - (Alíquota Zero)")]
        cofins04,

        [Description("COFINS 05 - Operação Tributável (ST)")]
        cofins05,

        [Description("COFINS 06 - Operação Tributável - Alíquota Zero")]
        cofins06,

        [Description("COFINS 07 - Operação Isenta da Contribuição")]
        cofins07,

        [Description("COFINS 08 - Operação sem Incidência da Contribuição")]
        cofins08,

        [Description("COFINS 09 - Operação com Suspensão da Contribuição")]
        cofins09,

        [Description("COFINS 49 - Outras Operações de Saída")]
        cofins49,

        [Description("COFINS 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno")]
        cofins50,

        [Description("COFINS 51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno")]
        cofins51,

        [Description("COFINS 52 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação")]
        cofins52,

        [Description("COFINS 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno")]
        cofins53,

        [Description("COFINS 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação")]
        cofins54,

        [Description("COFINS 55 - Operação com Direito a Crédito - Vinculada a Receitas Não-Tributadas no Mercado Interno e de Exportação")]
        cofins55,

        [Description("COFINS 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno, e de Exportação")]
        cofins56,

        [Description("COFINS 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada  no Mercado Interno")]
        cofins60,

        [Description("COFINS 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não-Tributada no Mercado Interno")]
        cofins61,

        [Description("COFINS 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação")]
        cofins62,

        [Description("COFINS 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não-Tributadas no Mercado Interno")]
        cofins63,

        [Description("COFINS 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação")]
        cofins64,

        [Description("COFINS 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não - Tributadas no Mercado Interno e de Exportação")]
        cofins65,

        [Description("COFINS 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação")]
        cofins66,

        [Description("COFINS 67 - Crédito Presumido - Outras Operações")]
        cofins67,

        [Description("COFINS 70 - Operação de Aquisição sem Direito a Crédito")]
        cofins70,

        [Description("COFINS 71 - Operação de Aquisição com Isenção")]
        cofins71,

        [Description("COFINS 72 - Operação de Aquisição com Suspensão")]
        cofins72,

        [Description("COFINS 73 - Operação de Aquisição a Alíquota Zero")]
        cofins73,

        [Description("COFINS 74 - Operação de Aquisição sem Incidência da Contribuição")]
        cofins74,

        [Description("COFINS 75 - Operação de Aquisição por Substituição Tributária")]
        cofins75,

        [Description("COFINS 98 - Outras Operações de Entrada")]
        cofins98,

        [Description("COFINS 99 - Outras Operações")]
        cofins99
    }
}
