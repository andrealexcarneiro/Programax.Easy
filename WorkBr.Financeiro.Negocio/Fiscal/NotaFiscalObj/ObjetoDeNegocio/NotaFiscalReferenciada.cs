using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class NotaFiscalReferenciada : ObjetoDeNegocioBase
    {
        public virtual EnumTipoNotaReferenciada TipoNotaReferenciada { get; set; }

        public virtual string ChaveDeAcesso { get; set; }

        public virtual int? CodigoUF { get; set; }

        public virtual string AnoMesEmissao { get; set; }

        public virtual EnumTipoPessoa TipoPessoa { get; set; }

        public virtual string CnpjEmitente { get; set; }

        public virtual string SerieNota { get; set; }

        public virtual string NumeroNota { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual string CTe { get; set; }

        public virtual string NumeroEcf { get; set; }

        public virtual string Coo { get; set; }

        public virtual EnumModeloNotaFiscalReferenciada? ModeloDocumento { get; set; }

        public virtual NotaFiscal NotaFiscal { get; set; }
    }
}
