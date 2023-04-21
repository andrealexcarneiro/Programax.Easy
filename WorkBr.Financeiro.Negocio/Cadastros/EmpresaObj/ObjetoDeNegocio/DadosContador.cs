using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio
{
    [Serializable]
    public class DadosContador
    {
        public virtual string Escritorio { get; set; }

        public virtual string Cnpj { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Crc { get; set; }

        public virtual string CpfContador { get; set; }

        public virtual string Telefone { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Celular { get; set; }

        public virtual EnderecoEmpresaComEmail Endereco { get; set; }
    }
}
