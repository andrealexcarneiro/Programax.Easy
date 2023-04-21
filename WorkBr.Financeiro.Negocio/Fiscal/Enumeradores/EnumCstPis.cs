using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumCstPis
    {
        [Description("PIS 01 - Operação Tributável - Base de Cálculo = Valor da Operação Alíquota Normal (Cumulativo / Não Cumulativo)")]
        pis01,

        [Description("PIS 02 - Operação Tributável - Base de Cálculo = Valor da Operação (Alíquota Diferenciada)")]
        pis02,

        [Description("PIS 03 - Operação Tributável - Base de Cálculo = Quantidade Vendida x Alíquota por Unidade de Produto")]
        pis03,

        [Description("PIS 04 - Operação Tributável - Tributação Monofásica - (Alíquota Zero)")]
        pis04,

        [Description("PIS 05 - Operação Tributável (ST)")]
        pis05,

        [Description("PIS 06 - Operação Tributável - Alíquota Zero")]
        pis06,

        [Description("PIS 07 - Operação Isenta da Contribuição")]
        pis07,

        [Description("PIS 08 - Operação sem Incidência da Contribuição")]
        pis08,

        [Description("PIS 09 - Operação com Suspensão da Contribuição")]
        pis09,

        [Description("PIS 49 - Outras Operações de Saída")]
        pis49,

        [Description("PIS 50 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Tributada no Mercado Interno")]
        pis50,

        [Description("PIS 51 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita Não Tributada no Mercado Interno")]
        pis51,

        [Description("PIS 52 - Operação com Direito a Crédito - Vinculada Exclusivamente a Receita de Exportação")]
        pis52,

        [Description("PIS 53 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno")]
        pis53,

        [Description("PIS 54 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas no Mercado Interno e de Exportação")]
        pis54,

        [Description("PIS 55 - Operação com Direito a Crédito - Vinculada a Receitas Não - Tributadas no Mercado Interno e de Exportação")]
        pis55,

        [Description("PIS 56 - Operação com Direito a Crédito - Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação")]
        pis56,

        [Description("PIS 60 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Tributada no Mercado Interno")]
        pis60,

        [Description("PIS 61 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita Não - Tributada no Mercado Interno")]
        pis61,

        [Description("PIS 62 - Crédito Presumido - Operação de Aquisição Vinculada Exclusivamente a Receita de Exportação")]
        pis62,

        [Description("PIS 63 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno")]
        pis63,

        [Description("PIS 64 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas no Mercado Interno e de Exportação")]
        pis64,

        [Description("PIS 65 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Não - Tributadas no Mercado Interno e de Exportação")]
        pis65,

        [Description("PIS 66 - Crédito Presumido - Operação de Aquisição Vinculada a Receitas Tributadas e Não - Tributadas no Mercado Interno, e de Exportação")]
        pis66,

        [Description("PIS 67 - Crédito Presumido - Outras Operações")]
        pis67,

        [Description("PIS 70 - Operação de Aquisição sem Direito a Crédito")]
        pis70,

        [Description("PIS 71 - Operação de Aquisição com Isenção")]
        pis71,

        [Description("PIS 72 - Operação de Aquisição com Suspensão")]
        pis72,

        [Description("PIS 73 - Operação de Aquisição a Alíquota Zero")]
        pis73,

        [Description("PIS 74 - Operação de Aquisição sem Incidência da Contribuição")]
        pis74,

        [Description("PIS 75 - Operação de Aquisição por Substituição Tributária")]
        pis75,

        [Description("PIS 98 - Outras Operações de Entrada")]
        pis98,

        [Description("PIS 99 - Outras Operações")]
        pis99
    }
}
