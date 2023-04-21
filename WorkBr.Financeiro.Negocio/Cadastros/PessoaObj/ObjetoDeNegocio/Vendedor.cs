using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable()]
    public class Vendedor
    {
        public virtual bool EhVendedor { get; set; }

        public virtual bool EhSupervisor { get; set; }

        public virtual bool EhAtendente { get; set; }

        public virtual bool EhIndicador { get; set; }
    }
}
