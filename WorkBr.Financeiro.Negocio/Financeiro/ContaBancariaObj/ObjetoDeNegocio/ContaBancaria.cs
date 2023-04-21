using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ContaBancaria:ObjetoDeNegocioBase
    {
        public virtual Agencia Agencia { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual EnumTipoContaBancaria? TipoContaBancaria { get; set; }

        public virtual string NumeroConta { get; set; }

        public virtual PlanoDeContas PlanoDeContas { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
