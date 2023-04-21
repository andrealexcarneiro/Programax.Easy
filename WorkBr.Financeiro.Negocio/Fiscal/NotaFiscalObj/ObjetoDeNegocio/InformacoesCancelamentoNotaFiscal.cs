using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class InformacoesCancelamentoNotaFiscal
    {
        public virtual string JustificativaCancelamento { get; set; }

        public virtual string ProtocoloCancelamento { get; set; }
    }
}
