using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class DuplicataNotaFiscal : ObjetoDeNegocioBase
    {
        public virtual string Parcela { get; set; }

        public virtual string NumeroDuplicata { get; set; }

        public virtual DateTime DataVencimento { get; set; }

        public virtual double ValorDuplicata { get; set; }

        public virtual EnumTipoFormaPagamento FormaPagamento {get;set;}

        public virtual int CondicaoPagamento { get; set; }

        public virtual NotaFiscal NotaFiscal { get; set; }
    }
}
