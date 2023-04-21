using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumFormatoImpressaoDanfe
    {
        [Description("SEM GERAÇÃO DE DANFE")]
        SEMGERACAODANFE = 0,

        [Description("DANFE NORMAL, RETRATO")]
        DANFENORMALRETRATO = 1,

        [Description("DANFE NORMAL, PAISAGEM")]
        DANFENORMALPAISAGEM = 2,

        [Description("DANFE SIMPLIFICADO")]
        DANFESIMPLIFICADO = 3,

        [Description("DANFE NFC-E")]
        DANFENFCE = 4,

        [Description("DANFE NFC-E EM MENSAGEM ELETRÔNICA")]
        DANFENFCEEMMENSAGEMELETRONICA = 5
    }
}
