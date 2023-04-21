using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumOrigemMovimentacaoBanco
    {
        [Description("TRANSFERÊNCIA ENTRE BANCOS")]
        TRANSFERENCIAENTREBANCOS,

        [Description("CHEQUE")]
        CHEQUE,

        [Description("CONTAS A PAGAR")]
        CONTAPAGAR,

        [Description("CONTAS A RECEBER")]
        CONTARECEBER,

        [Description("PEDIDO DE VENDAS")]
        PEDIDODEVENDA,

        [Description("TROCA PEDIDO DE VENDAS")]
        TROCAPEDIDODEVENDA,

        [Description("INFOR. DIRETA")]
        INFORMACAOMANUAL,

        [Description("IMPORTADO")]
        IMPORTADO,

        [Description("CONCILIADO")]
        CONCILIADO,

        [Description("IGNORADO")]
        IGNORADO,

        [Description("NENHUM")]
        NENHUM,

        [Description("TODOS")]
        TODOS
    }
}
