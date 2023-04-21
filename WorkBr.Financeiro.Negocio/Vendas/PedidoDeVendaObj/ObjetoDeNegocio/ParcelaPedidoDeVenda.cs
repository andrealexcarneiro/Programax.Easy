using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ParcelaPedidoDeVenda : ObjetoDeNegocioBase
    {
        public virtual string Parcela { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual CondicaoPagamento CondicaoPagamento { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual PedidoDeVenda PedidoDeVenda { get; set; }

        public virtual DateTime DataVencimento { get; set; }

        public virtual double Valor { get; set; }

        public virtual ContaPagarReceber ContaPagarReceber { get; set; }

        public virtual OperadorasCartao Operadoras { get; set; }        
    }
}
