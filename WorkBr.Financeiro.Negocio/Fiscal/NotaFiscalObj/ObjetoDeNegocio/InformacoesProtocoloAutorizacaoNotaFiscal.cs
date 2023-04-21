using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class InformacoesProtocoloAutorizacaoNotaFiscal
    {
        public string VersaoNota { get; set; }

        public virtual int TipoAmbiente { get; set; }

        public virtual string VersaoAplicativo { get; set; }

        public virtual string ChaveNfe { get; set; }

        public virtual DateTime DataHoraRecibo { get; set; }

        public virtual string NumeroProtocolo { get; set; }

        public virtual string DigestValue { get; set; }

        public virtual int Status { get; set; }

        public virtual string Motivo { get; set; }

        public virtual string Id { get; set; }
    }
}
