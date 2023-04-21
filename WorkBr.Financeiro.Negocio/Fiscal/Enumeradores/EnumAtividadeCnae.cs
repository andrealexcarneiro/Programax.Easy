using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumAtividadeCnae
    {
        [Description("INDÚSTRIA OU EQUIPERADO A INDUSTRIAL")]
        INDUSTRIAOUEQUIPERADOAINDUSTRIAL = 0,

        [Description("PRESTADOR DE SERVIÇO")]
        PRESTADORDESERVICO = 1,

        [Description("ATIVIDADE DE COMÉRCIO")]
        ATIVIDADEDECOMERCIO = 2,

        [Description("ATIVIDADE FINANCEIRA")]
        ATIVIDADEFINANCEIRA = 3,

        [Description("ATIVIDADE IMOBILIÁRIA")]
        ATIVIDADEIMOBILIARIA = 4,

        [Description("OUTROS")]
        OUTROS = 9
    }
}
