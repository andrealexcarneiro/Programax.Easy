using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio
{
    [Serializable]
    public class Crediario : ObjetoDeNegocioBase
    {
        public virtual Pessoa Pessoa { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual CondicaoPagamento CondicaoPagamento { get; set; }

        public virtual TabelaPreco TabelaPreco { get; set; }

        public virtual EnumStatusCrediario StatusAnaliseCredito { get; set; }

        public virtual double ValorLimiteCredito { get; set; }

        public virtual bool PodeAlterarMultaEJuros { get; set; }

        public virtual DateTime DataUltimaAlteracao { get; set; }
        public virtual DateTime DataValidade { get; set; }

        public virtual Pessoa UsuarioUltimaAlteracao { get; set; }
    }
}
