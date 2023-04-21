using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWEnderecosPessoas:ObjetoDeNegocioBase
    {
        public virtual int PessoaId { get; set; }

        public virtual string Bairro { get; set; }

        public virtual int CidadeId { get; set; }

        public virtual string CidadeNome { get; set; }

        public virtual string EstadoUF { get; set; }

        public virtual string EstadoNome { get; set; }
    }
}
