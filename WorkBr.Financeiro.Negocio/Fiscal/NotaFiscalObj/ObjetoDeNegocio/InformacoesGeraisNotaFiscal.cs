using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class InformacoesGeraisNotaFiscal
    {
        public virtual DateTime DataCadastro { get; set; }

        public virtual string ChaveDeAcesso { get; set; }

        public virtual EnumStatusNotaFiscal Status { get; set; }

        public virtual EnumTipoFrete TipoFrete { get; set; }

        public virtual int? TransportadoraId { get; set; } 

        public virtual int Volume { get; set; }

        public virtual string MensagemDeErro { get; set; }

        public virtual string MensagemDevolvida { get; set; }

        public virtual string Observacoes { get; set; }

        public virtual string ObservacoesFisco { get; set; }

        public virtual string NumeroReciboLote { get; set; }

        public virtual string Xml { get; set; }
    }
}
