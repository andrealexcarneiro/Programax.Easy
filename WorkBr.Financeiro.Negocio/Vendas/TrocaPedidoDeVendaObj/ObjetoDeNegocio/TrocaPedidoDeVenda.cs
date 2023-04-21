using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class TrocaPedidoDeVenda : ObjetoDeNegocioBase
    {
        public TrocaPedidoDeVenda()
        {
            ListaItens = new List<ItemTrocaPedidoDeVenda>();
            ListaItensPedido = new List<ItemPedidoTrocaPedidoDeVenda>();
        }

        public virtual PedidoDeVenda PedidoDeVenda { get; set; }

        public virtual EnumStatusTrocaPedidoDeVenda Status { get; set; }

        public virtual EnumTipoMovimentacaoFinanceiraTrocaPedidoDeVenda TipoMovimentacaoFinanceira { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual DateTime DataVencimento { get; set; }

        public virtual DateTime DataElaboracao { get; set; }

        public virtual DateTime? DataFechamento { get; set; }

        public virtual double ValorTotalTroca { get; set; }

        public virtual MotivoTrocaPedidoDeVenda MotivoTroca { get; set; }

        public virtual Pessoa UsuarioRealizouTroca { get; set; }

        public virtual IList<ItemTrocaPedidoDeVenda> ListaItens { get; set; }

        public virtual IList<ItemPedidoTrocaPedidoDeVenda> ListaItensPedido { get; set; }
    }
}
