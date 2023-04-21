using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class AjusteFiscal: ObjetoDeNegocioBase
    {
        public virtual string Codigo { get; set; }

        public virtual string Descricao { get; set; }

        public virtual DateTime? DataInicio { get; set; }

        public virtual DateTime? DataFim { get; set; }
    }
}
