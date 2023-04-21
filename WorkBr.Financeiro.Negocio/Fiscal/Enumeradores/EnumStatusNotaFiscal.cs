using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumStatusNotaFiscal
    {
        [Description("DISPONÍVEL")]
        DISPONIVEL,

        PROCESSANDO,
        
        REJEITADA,
        
        DENEGADA,
        
        [Description("PROBLEMA DE TRANSMISSÃO")]
        PROBLEMATRANSMISSAO,
        
        CANCELADA,

        AUTORIZADA
    }
}
