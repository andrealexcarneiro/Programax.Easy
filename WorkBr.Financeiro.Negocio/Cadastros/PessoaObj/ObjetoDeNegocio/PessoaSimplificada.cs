using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class PessoaSimplificada
    {
        public virtual int Id { get; set; }

        public virtual string Razao { get; set; }
    }
}
