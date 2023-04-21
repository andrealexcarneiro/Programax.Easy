using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.VendedorObj.ObjetoDeNegocio
{
    [Serializable]
    public class VendedorAntigo : ObjetoDeNegocioBase
    {
        public virtual string Nome { get; set; }

        public virtual string Ativo { get; set; }
    }
}
