using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumIndicacaoPresenca
    {
        [Description("NÃO SE APLICA (POR EXEMPLO, PARA A NOTA FISCAL COMPLEMENTAR OU DE AJUSTE)")]
        NAOSEAPLICA = 0,
        
        [Description("OPERAÇÃO PRESENCIAL")]
        OPERACAOPRESENCIAL = 1,

        [Description("OPERAÇÃO NÃO PRESENCIAL, PELA INTERNET")]
        OPERACAONAOPRESENCIALPELAINTERNET = 2,

        [Description("OPERAÇÃO NÃO PRESENCIAL, TELEATENDIMENTO")]
        OPERACAONAOPRESENCIALTELEATENDIMENTO = 3,

        [Description("NFC-e em operação com entrega em domicílio")]
        NFCEEMOPERACAOCOMENTREGAEMDOMICILIO = 4,

        [Description("OPERAÇÃO NÃO PRESENCIAL, OUTROS")]
        OPERACAONAOPRESENCAOOUTROS = 9
    }
}
