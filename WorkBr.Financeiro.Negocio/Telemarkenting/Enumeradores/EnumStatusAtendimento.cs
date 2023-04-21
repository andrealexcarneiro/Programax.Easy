using System.ComponentModel;

namespace Programax.Easy.Negocio.TeleMarketing.Enumeradores
{
    public enum EnumStatusAtendimento
    {
        //[Description("NENHUM")]
        //DISPONIVEL,

        [Description("DISPONIVEL")]
        DISPONIVEL,

        [Description("AGENDADO")]
        AGENDADO,

        [Description("CONCLUIDO")]
        CONCLUIDO,

        [Description("EM ATENDIMENTO")]
        EMATENDIMENTO,

        [Description("CANCELADO")]
        CANCELADO,
    }
}
