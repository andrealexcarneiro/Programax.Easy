using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.VendedorObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Telefone : ObjetoDeNegocioBase
    {
        public virtual Pessoa Pessoa { get; set; }

        public virtual EnumTipoTelefone? TipoTelefone { get; set; }

        public virtual int? Ddd { get; set; }

        public virtual string Numero { get; set; }

        public virtual string Observacao { get; set; }
    }
}
