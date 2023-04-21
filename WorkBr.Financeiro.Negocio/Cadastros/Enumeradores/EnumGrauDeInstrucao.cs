using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Cadastros.Enumeradores
{
    public enum EnumGrauDeInstrucao
    {
        [Description("ANALFABETO")]
        ANALFABETO = 1,

        [Description("ATÉ O 5º ANO INCOMPLETO DO ENSINO FUNDAMENTAL")]
        ATEQUINTOENSINOFUNDAMENTALINCOMPLETO = 2,

        [Description("5º ANO COMPLETO DO ENSINO FUNDAMENTAL")]
        QUINTOANOCOMPLETODOENSINOFUNDAMENTAL = 3,

        [Description("DO 6º AO 9º ANO DO ENSINO FUNDAMENTAL INCOMPLETO")]
        DOSEXTOAONONOANOENSINOFUNDAMENTALINCOMPLETO = 4,

        [Description("ENSINO FUNDAMENTAL COMPLETO")]
        ENSINOFUNDAMENTALCOMPLETO = 5,

        [Description(" ENSINO MÉDIO INCOMPLETO")]
        ENSINOMEDICOINCOMPLETO = 6,

        [Description(" ENSINO MÉDIO COMPLETO")]
        ENSINOMEDIOCOMPLETO = 7,

        [Description("EDUCAÇÃO SUPERIOR INCOMPLETA")]
        EDUCACAOSUPERIORINCOMPLETA = 8,

        [Description("EDUCAÇÃO SUPERIOR COMPLETA")]
        EDUCACAOSUPERIORCOMPLETA = 9,

        [Description("MESTRADO COMPLETO")]
        MESTRADOCOMPLETO = 10,

        [Description("DOUTORADO COMPLETO")]
        DOUTORADOCOMPLETO = 11,
    }
}
