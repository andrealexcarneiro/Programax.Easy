using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Gerais
{
    public class TipoMensagem
    {
        public virtual bool PrimeiraResposta { get; set; }
        public virtual bool SegundaResposta { get; set; }
        public virtual bool TerceiraResposta { get; set; }
        public virtual string PrimeiroConteudo { get; set; }
        public virtual string SegundoConteudo { get; set; }
    }
}
