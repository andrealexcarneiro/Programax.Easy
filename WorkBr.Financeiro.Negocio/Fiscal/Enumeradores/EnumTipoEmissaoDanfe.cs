using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumTipoEmissaoDanfe
    {
        [Description("Emissão normal (não em contingência)")]
        EMISSAONORMAL = 1,

        [Description("Contingência FS-IA, com impressão do DANFE em formulário de segurança")]
        CONTINGENCIAFSIA = 2,

        [Description("Contingência SCAN (Sistema de Contingência do Ambiente Nacional)")]
        CONTINGENCIASCAN = 3,

        [Description("Contingência DPEC (Declaração Prévia da Emissão em Contingência)")]
        CONTINGENCIADPEC = 4,

        [Description("Contingência FS-DA, com impressão do DANFE em formulário de segurança")]
        CONTINGENCIAFSDA = 5,

        [Description("Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)")]
        CONTINGENCIASVCAN = 6,

        [Description("Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)")]
        CONTINGENCIASVCRS = 7,

         [Description("Contingência offLine (SEFAZ Virtual de Contingência da NFCe)")]
        CONTINGENCIAOFFLINE = 9
    }
}
