using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class InformacoesDocumentoOrigemNotaFiscal
    {
        public virtual DateTime DataElaboracao { get; set; }

        public virtual int DocumentoId { get; set; }

        public virtual EnumTipoDocumento Origem { get; set; }

        public virtual int UsuarioId { get; set; }

        public virtual string UsurioNome { get; set; }
    }
}
