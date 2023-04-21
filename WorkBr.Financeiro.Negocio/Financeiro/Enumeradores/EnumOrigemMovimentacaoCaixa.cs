using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumOrigemMovimentacaoCaixa
    {
        [Description("DIRETO NO CAIXA")]
        DIRETONOCAIXA,

        [Description("CHEQUE")]
        CHEQUE,

        [Description("CONTAS A PAGAR")]
        CONTAPAGAR,

        [Description("CONTAS A RECEBER")]
        CONTARECEBER,

        [Description("PEDIDO DE VENDAS")]
        PEDIDODEVENDA,

        [Description("TROCA PEDIDO DE VENDAS")]
        TROCAPEDIDODEVENDA
    }
}
