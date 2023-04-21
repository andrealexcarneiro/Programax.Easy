using System.ComponentModel;

namespace Programax.Easy.Negocio.Movimentacao.Enumeradores
{
    public enum EnumTipoMovimentacao
    {
        [Description("ENTRADA")]
        ENTRADA,

        [Description("SAÍDA")]
        SAIDA,

        [Description("AMBOS")]
        AMBOS
    }
}
