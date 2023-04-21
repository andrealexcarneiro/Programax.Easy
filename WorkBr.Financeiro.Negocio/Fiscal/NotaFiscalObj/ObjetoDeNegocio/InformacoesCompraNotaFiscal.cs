using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class InformacoesCompraNotaFiscal
    {
        public virtual string NotaEmpenho { get; set; }

        public virtual string Pedido { get; set; }

        public virtual string Contrato { get; set; }
    }
}
