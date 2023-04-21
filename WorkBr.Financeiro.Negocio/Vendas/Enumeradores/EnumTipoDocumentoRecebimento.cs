using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Vendas.Enumeradores
{
    public enum EnumTipoDocumentoRecebimento
    {
        [Description("PEDIDO DE VENDAS")]
        PEDIDODEVENDAS,

        [Description("TROCA PEDIDO DE VENDAS")]
        TROCAPEDIDODEVENDAS
    }
}
