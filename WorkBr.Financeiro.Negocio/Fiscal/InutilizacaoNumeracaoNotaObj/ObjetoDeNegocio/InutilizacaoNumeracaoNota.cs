using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio
{
    public class InutilizacaoNumeracaoNota : ObjetoDeNegocioBase
    {
        public virtual string Ano { get; set; }

        public virtual int NumeroInicial { get; set; }

        public virtual int NumeroFinal { get; set; }

        public virtual int Serie { get; set; }

        public virtual EnumModeloNotaFiscal ModeloNotaFiscal { get; set; }

        public virtual string Justificativa { get; set; }

        public virtual string Protocolo { get; set; }
    }
}
