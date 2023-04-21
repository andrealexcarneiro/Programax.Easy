using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class TributosDevolvidosNotaFiscal
    {
        public double PercentualDaMercadoriaDevolvida { get; set; }

        public double ValorDoIpiDevolvido { get; set; }
    }
}
