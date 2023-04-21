using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Caixa:ObjetoDeNegocioBase
    {
        public virtual string NomeCaixa { get; set; }

        public virtual Pessoa Funcionario { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual EnumPerfilCaixa? PerfilCaixa { get; set; }

        public virtual string Status { get; set; }
    }
}
