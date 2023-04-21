using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.ConfiguracaoNfeObj.ObjetoDeNegocio
{
    [Serializable]
    public class ConfiguracaoNfe : ObjetoDeNegocioBase
    {
        public virtual double? AliquotaSimplesNacional { get; set; }

        public virtual EnumFormatoImpressaoDanfe FormatoImpressaoDanfe { get; set; }

        public virtual int NumeroNota { get; set; }

        public virtual int Serie { get; set; }

        public virtual EnumTipoAmbiente TipoAmbiente { get; set; }

        public virtual string NumeroSerieCertificado { get; set; }

        public virtual EnumModeloNotaFiscal PadraoModeloNF { get; set; }

        public virtual EnumModeloNotaFiscal ModeloNF { get; set; }
    }
}
