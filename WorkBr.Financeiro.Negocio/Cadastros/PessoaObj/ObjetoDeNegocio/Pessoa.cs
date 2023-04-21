using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PaisObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Pessoa : ObjetoDeNegocioBase
    {
        public Pessoa()
        {
            EmpresaPessoa = new EmpresaPessoa();
            DadosPessoais = new DadosPessoais();
            ListaDeEnderecos = new List<EnderecoPessoa>();
            ListaDeTelefones = new List<Telefone>();
            DadosGerais = new DadosGerais();
            Funcionario = new Funcionario();
            Vendedor = new Vendedor();
            Atendimento = new Atendimento();
        }

        public virtual DadosGerais DadosGerais { get; set; }

        public virtual Atendimento Atendimento { get; set; }

        public virtual EmpresaPessoa EmpresaPessoa { get; set; }

        public virtual DadosPessoais DadosPessoais { get; set; }

        public virtual IList<Telefone> ListaDeTelefones { get; set; }

        public virtual IList<EnderecoPessoa> ListaDeEnderecos { get; set; }

        public virtual IList<Comissao> ListaDeComissoes { get; set; }

        public virtual Funcionario Funcionario { get; set; }

        public virtual Vendedor Vendedor { get; set; }
    }
}
