using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class IINotaFiscal
    {
        public virtual double BaseCalculoImpostoImportacao { get; set; }

        public virtual double ValorDespesasAduaneiras { get; set; }

        public virtual double ValorImpostoImportacao { get; set; }

        public virtual double ValorImpostoSobreOperacoesFinanceiras { get; set; }
    }
}
