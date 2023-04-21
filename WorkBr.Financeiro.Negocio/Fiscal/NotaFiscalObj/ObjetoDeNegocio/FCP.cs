using System;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class FCP
    {
        public virtual double PercentualFCP { get; set; }

        public virtual double ValorFCP { get; set; }

        public virtual double ValorBaseFCP { get; set; }

        public virtual double ValorBCSTFCP { get; set; }

        public virtual double PercentualBCSTFCP { get; set; }

        public virtual double ValorFCPST { get; set; }

    }
}
