using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class InformacoesComercioExteriorNotaFiscal
    {
        public virtual string UFEmbarque { get; set; }

        public virtual string DescricaoLocalEmbarque { get; set; }

        public virtual string DescricaoLocalDespacho { get; set; }
    }
}
