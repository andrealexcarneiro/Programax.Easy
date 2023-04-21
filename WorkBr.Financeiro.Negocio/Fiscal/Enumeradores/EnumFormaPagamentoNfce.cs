using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Fiscal.Enumeradores
{
    public enum EnumFormaPagamentoNfce
    {
        [Description("DINHEIRO")]
        DINHEIRO = 1,

        [Description("CHEQUE")]
        CHEQUE = 2,

        [Description("CARTÃO CRÉDITO")]
        CARTAOCREDITO = 3,

        [Description("CARTÃO DÉBITO")]
        CARTAODEBITO = 4,

        [Description("CREDIÁRIO PRÓPRIO-LOJA")]
        CREDITOLOJA = 5,

        [Description("DUPLICATA")]
        DUPLICATA = 9,
        
        [Description("VALE ALIMENTAÇÃO")]
        VALEALIMENTACAO = 10,

        [Description("VALE REFEIÇÃO")]
        VALEREFEICAO = 11,

        [Description("VALE PRESENTE")]
        VALEPRESENTE = 12,

        [Description("VALE COMBUSTÍVEL")]
        VALECOMBUSTIVEL = 13,

        [Description("BOLETO BANCÁRIO")]
        BOLETOBANCARIO = 15,

        [Description("SEM PAGAMENTO")]
        SEMPAGAMENTO = 90,

        [Description("OUTROS")]
        OUTROS = 99,

        [Description("PIX")]
        PIX = 17,
    }
}
