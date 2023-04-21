using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Endereco : ObjetoDeNegocioBase
    {
        public virtual string CEP { get; set; }

        public virtual string Rua { get; set; }

        public virtual string Bairro  { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }
    }
}
