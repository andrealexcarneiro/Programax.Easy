using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class TotaisNotaFiscal
    {
        public virtual double BaseCalculoIcms { get; set; }
                  
        public virtual double BaseCalculoIcmsST { get; set; }
                  
        public virtual double Cofins { get; set; }
                  
        public virtual double Desconto { get; set; }
                  
        public virtual double Frete { get; set; }
                  
        public virtual double Icms { get; set; }
                  
        public virtual double IcmsDesoneracao { get; set; }
                  
        public virtual double ImpostoDeImportacao { get; set; }
                  
        public virtual double Ipi { get; set; }
                  
        public virtual double ValorNotaFiscal { get; set; }
                  
        public virtual double OutrosValores { get; set; }
                  
        public virtual double Pis { get; set; }
                  
        public virtual double Produtos { get; set; }
                  
        public virtual double ValorSeguro { get; set; }
                  
        public virtual double ValorSubstituicaoTributaria { get; set; }

        public virtual double? ValorFCP { get; set; }

        public virtual double? TotalFCPNf { get; set; }

        public virtual double? ValorFCPSt { get; set; }

        public virtual double? ValorFCPSTRet { get; set; }

        public virtual double? ValorInterestadualDestino { get; set; }

        public virtual double? ValorInterestadualOrigem { get; set; }
                  
        public virtual double TotalTributacao { get; set; }

        public virtual double TotalTributacaoFederal { get; set; }

        public virtual double TotalTributacaoEstadual { get; set; }

        public virtual RetencaoTributosNotaFiscal RetencaoTributosNotaFiscal { get; set; }
    }
}
