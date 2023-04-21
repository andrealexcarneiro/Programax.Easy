using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using System;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class EnderecoPessoa : ObjetoDeNegocioBase
    {
        public virtual string Complemento { get; set; }

        public virtual string Numero { get; set; }

        public virtual EnumTipoEndereco? TipoEndereco { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual string CEP { get; set; }

        public virtual string Rua { get; set; }

        public virtual string Bairro { get; set; }

        public virtual Cidade Cidade { get; set; }
    }
}
