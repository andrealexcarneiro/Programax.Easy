using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemPedidoTrocaPedidoDeVenda : ObjetoDeNegocioBase
    {
        public virtual Produto Produto { get; set; }

        public virtual double Quantidade { get; set; }

        public virtual double QuantidadeTrocar { get; set; }

        public virtual double ValorUnitario { get; set; }

        public virtual double Desconto { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual TrocaPedidoDeVenda TrocaPedidoDeVenda { get; set; }
    }
}
