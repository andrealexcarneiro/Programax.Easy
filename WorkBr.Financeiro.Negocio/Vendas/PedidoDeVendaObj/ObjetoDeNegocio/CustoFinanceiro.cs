using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;


namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    public class CustoFinanceiro : ObjetoDeNegocioBase
    {
        public virtual Int64 VendedorId { get; set; }

        public virtual string VendedorNome { get; set; }

        public virtual DateTime DataVenda { get; set; }
        public virtual Decimal ValorVendido { get; set; }

    }
}
