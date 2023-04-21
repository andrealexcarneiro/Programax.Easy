using System;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class RetencaoTributosNotaFiscal
    {
        public virtual double? ValorRetidoPis { get; set; }
                                     
        public virtual double? ValorRetidoCofins { get; set; }
                                     
        public virtual double? ValorRetidoCsll { get; set; }
                                     
        public virtual double? BaseCalculoIRRF { get; set; }
                                     
        public virtual double? BaseCalculoRetencaoPrevidenciaSocial { get; set; }
                                     
        public virtual double? ValorRetencaoPrevidenciaSocial { get; set; }
    }
}
