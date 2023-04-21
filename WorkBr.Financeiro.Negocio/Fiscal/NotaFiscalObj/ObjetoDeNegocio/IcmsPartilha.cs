using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class IcmsInterestadualNotaFiscal
    {
        public virtual double BaseDeCalculo { get; set; }

        public virtual double AliquotaFCP { get; set; }

        public virtual double AliquotaInterna { get; set; }

        public virtual double AliquotaInterestadual { get; set; }

        public virtual double PercentualProvisorioPartilha { get; set; }

        public virtual double ValorFCP { get; set; }

        public virtual double ValorIcmsDestino { get; set; }

        public virtual double ValorIcmsOrigem { get; set; }        
    }
}
