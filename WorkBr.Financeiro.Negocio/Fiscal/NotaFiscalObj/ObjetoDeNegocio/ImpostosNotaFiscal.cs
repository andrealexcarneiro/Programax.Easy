using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class ImpostosNotaFiscal
    {
        public virtual IcmsNotaFiscal Icms { get; set; }

        public virtual IcmsInterestadualNotaFiscal IcmsInterestadual { get; set; }

        public virtual FCP Fcp { get; set; }

        public virtual IpiNotaFiscal Ipi { get; set; }

        public virtual PisNotaFiscal Pis { get; set; }

        public virtual CofinsNotaFiscal Cofins { get; set; }

        public virtual IINotaFiscal II { get; set; }

        public virtual TributosDevolvidosNotaFiscal TributosDevolvidos { get; set; }

        public virtual double TotalTributacaoEstadual { get; set; }

        public virtual double TotalTributacaoFederal { get; set; }

        public virtual double TotalTributacao { get; set; }
    }
}
