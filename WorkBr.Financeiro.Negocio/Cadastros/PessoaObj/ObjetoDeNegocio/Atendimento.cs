using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Atendimento : ObjetoDeNegocioBase
    {
        public virtual Pessoa Supervisor { get; set; }

        public virtual Pessoa Atendente { get; set; }

        public virtual Pessoa Vendedor { get; set; }

        public virtual Pessoa Indicador { get; set; }

        public virtual TabelaPreco TabelaDePreco { get; set; }

        public virtual CondicaoPagamento CondicaoDePagamento { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual OrigemCliente OrigemCliente { get; set; }

        public virtual string Observacoes { get; set; }
    }
}
