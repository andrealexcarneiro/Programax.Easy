using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumTipoCfop
    {
        [Description("ENTRADA E SAÍDA")]
        ENTRADAESAIDA,

        [Description("ENTRADA")]
        ENTRADA,
        
        [Description("SAÍDA")]
        SAIDA
    }
}
