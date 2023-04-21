using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class CartaoFormaPagamentoNotaFiscal
    {
        public virtual string CnpjCredenciadoraCartao { get; set; }

        public virtual EnumTipoIntegracaoPagamento TipoIntegracaoPagamento { get; set; }

        public virtual EnumBandeiraCartao? BandeiraCartao { get; set; }

        public virtual string CodigoAutorizacaoOperacaoCartaoCreditoDebito { get; set; }
    }
}
