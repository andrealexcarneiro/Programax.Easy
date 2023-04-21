using System.ComponentModel;


namespace Programax.Easy.Negocio.Vendas.Enumeradores
{
    public enum EnumStatusRoteiro
    {
        [Description("ROTEIRIZADO")]
        EMROTA,

        [Description("REALIZADO")]
        CONCLUIDO,

        [Description("INCONCLUSO")]
        INCONCLUSO,

        [Description("AGENDADO")]
        EMAGENDA
    }
}
