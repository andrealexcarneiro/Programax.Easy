using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores
{
    public enum EnumTipoCartaoCreditoEDebito
    {
        [Description("TEF DIAL (REDECARD, VISA, AMEX)")]
        TEFDIALREDECARDVISAAMEX,

        [Description("TEF DISC (TECBAN)")]
        TEFDISCTECBAN,

        [Description("TEF SOROCRED")]
        TEFSOROCRED,

        [Description("TEF GOODCARD (GET)")]
        TEFGOODCARDGET,

        [Description("TEF ULTRACARD")]
        TEFULTRACARD,

        [Description("TEF HIPERTEF")]
        TEFHIPERTEF,

        [Description("TEF CLISITEF")]
        TEFCLISITEF,

        [Description("INFORMAR PAGAMENTO COM POS")]
        INFORMARPAGAMENTOCOMPOS
    }
}
