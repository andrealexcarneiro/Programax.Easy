using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class FormaPagamento : ObjetoDeNegocioBase
    {
        public FormaPagamento()
        {
            ListaCondicoesPagamento = new List<CondicaoDePagamentoDaForma>();
        }

        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual bool DisponivelParaPdv { get; set; }

        public virtual bool DisponivelParaContasPagar { get; set; }

        public virtual bool DisponivelParaContasReceber { get; set; }

        public virtual bool DisponivelParaPedidoVenda { get; set; }

        public virtual EnumTipoFormaPagamento TipoFormaPagamento { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual IList<CondicaoDePagamentoDaForma> ListaCondicoesPagamento { get; set; }

        public virtual EnumFormaPagamentoNfce FormaPagamentoNfce { get; set; }
    }
}
