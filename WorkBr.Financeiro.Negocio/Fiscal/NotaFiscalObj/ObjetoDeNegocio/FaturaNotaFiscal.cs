using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class FaturaNotaFiscal
    {
        public string NumeroFatura { get; set; }

        public double? ValorOriginalFatura { get; set; }

        public double? ValorDesconto { get; set; }

        public double? ValorLiquido { get; set; }
    }
}
