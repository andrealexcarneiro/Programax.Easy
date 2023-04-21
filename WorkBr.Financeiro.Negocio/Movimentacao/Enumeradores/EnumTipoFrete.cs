using System.ComponentModel;

namespace Programax.Easy.Negocio.Movimentacao.Enumeradores
{
    public enum EnumTipoFrete
    {
        [Description("POR CONTA DO EMITENTE")]
        PORCONTADOEMITENTE = 0,

        [Description("POR CONTA DO DESTINATARIO / REMETENTE")]
        PORCONTADODESTINATARIOREMETENTE = 1,

        [Description("POR CONTA DE TERCEIROS")]
        PORCONTADETERCEIROS = 2,

        [Description("SEM COBRANCA DE FRETE")]
        SEMCOBRANCADEFRETE = 9
    }
}
