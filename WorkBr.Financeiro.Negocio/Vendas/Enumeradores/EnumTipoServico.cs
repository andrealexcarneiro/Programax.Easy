using System.ComponentModel;


namespace Programax.Easy.Negocio.Vendas.Enumeradores
{
    public enum EnumTipoServico
    {
        [Description("ENTREGA E INSTALAÇÃO")]
        ENTREGAEINSTALACAO,

        [Description("SÓ ENTREGA")]
        SOENTREGA,

        [Description("SÓ INSTALAÇÃO")]
        SOINSTALACAO,

        [Description("VISITA")]
        VISITA,

        [Description("ENTREGA APÓS HORÁRIO")]
        ENTREGAAPOSHORARIO,

        [Description("INSTALAÇÃO APÓS HORÁRIO")]
        INSTALACAOAPOSHORARIO,

        [Description("INSTALAÇÃO EM OUTRAS CIDADES")]
        INSTALACAOEMOUTRASCIDADES,

        [Description("DESLOCAMENTO E GARANTIA")]
        DESLOCAMENTOEGARANTIA,

        [Description("RECEBIMENTO")]
        RECEBIMENTO,
    }
}
