using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class CartaCorrecao : ObjetoDeNegocioBase
    {
        public virtual int SequenciaEvento { get; set; }

        public virtual string Correcao { get; set; }

        public virtual DateTime DataHoraEmissao { get; set; }

        public virtual NotaFiscal NotaFiscal { get; set; }

        public virtual string NumeroProtocolo { get; set; }
    }
}
