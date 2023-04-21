using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio
{
    [Serializable]
    public class Pais: ObjetoDeNegocioBase
    {
        public virtual int CodigoPais { get; set; }

        public virtual string NomePais { get; set; }
    }
}
