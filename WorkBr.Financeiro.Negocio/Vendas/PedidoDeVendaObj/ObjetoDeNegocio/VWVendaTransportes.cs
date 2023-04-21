using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    public class VWVendaTransportes: ObjetoDeNegocioBase
    {
        public virtual int ClienteId { get; set; }

        public virtual string ClienteNome { get; set; }

        public virtual int IndicadorId { get; set; }

        public virtual string IndicadorNome { get; set; }

        public virtual int AtendenteId { get; set; }

        public virtual string AtendenteNome { get; set; }

        public virtual int VendedorId { get; set; }

        public virtual string VendedorNome { get; set; }

        public virtual int SupervisorId { get; set; }

        public virtual string SupervisorNome { get; set; }

        public virtual EnumTipoPedidoDeVenda TipoPedidoVenda { get; set; }

        public virtual EnumStatusPedidoDeVenda Status { get; set; }

        public virtual DateTime DataEntrega { get; set; }
        
        public virtual string FormaPagamentoNome { get; set; }

        public virtual string CondicaoPagamentoNome { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual bool VendaJahExportadaPdvEcf { get; set; }
    }
}
