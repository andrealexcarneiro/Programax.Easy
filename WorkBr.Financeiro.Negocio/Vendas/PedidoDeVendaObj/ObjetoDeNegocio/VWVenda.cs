using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    public class VWVenda: ObjetoDeNegocioBase
    {
        public virtual int ClienteId { get; set; }

        public virtual string ClienteNome { get; set; }

        public virtual string ClienteCpfCnpj { get; set; }

        public virtual EnumTipoPessoa TipoCliente { get; set; }

        public virtual string Cidade { get; set; }

        public virtual string UF { get; set; }
        public virtual string Bairro { get; set; }

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

        public virtual DateTime DataElaboracao { get; set; }

        public virtual DateTime? DataFechamento { get; set; }

        public virtual string FormaPagamentoNome { get; set; }

        public virtual string CondicaoPagamentoNome { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual double ComissaoIndicador { get; set; }

        public virtual double ComissaoAtendente { get; set; }

        public virtual double ComissaoVendedor { get; set; }
        
        public virtual double ComissaoSupervisor { get; set; }

        public virtual bool VendaJahExportadaPdvEcf { get; set; }

        public virtual bool PedidoEstahPago { get; set; }
    }
}
