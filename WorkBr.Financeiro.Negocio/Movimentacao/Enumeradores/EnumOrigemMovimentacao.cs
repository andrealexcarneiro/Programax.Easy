using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Movimentacao.Enumeradores
{
    public enum EnumOrigemMovimentacao
    {
        [Description("Venda")]
        VENDA,

        [Description("Entrada de Mercadoria")]
        ENTRADADEMERCADORIA,

        [Description("Correção de Estoque")]
        CORRECAODEESTOQUE,

        [Description("Inventário")]
        INVENTARIO,

        [Description("Troca de Mercadoria")]
        TROCADEMERCADORIA,

        [Description("Estorno de Saída")]
        ESTORNOSAIDA
    }
}
