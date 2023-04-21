using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class BancoParaMovimento : ObjetoDeNegocioBase
    {
        public virtual string NomeBanco { get; set; }       

        public virtual DateTime DataCadastro { get; set; }

        public virtual ContaBancaria ContaBancaria {get;set; }

        public virtual string Status { get; set; }

        public virtual bool TornarPadrao { get; set; }
    }
}
