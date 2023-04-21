using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Programax.Easy.Negocio.Financeiro.Enumeradores
{
    public enum EnumTipoFormaPagamento
    {
        [Description("OUTROS")]
        OUTROS,

        [Description("DINHEIRO")]
        DINHEIRO,

        [Description("BOLETO BANCÁRIO")]
        BOLETOBANCARIO,

        [Description("DEPÓSITO BANCÁRIO")]
        DEPOSITOBANCARIO,

        [Description("CHEQUE")]
        CHEQUE,

        [Description("DUPLICATA")]
        DUPLICATA,

        [Description("CREDIÁRIO PRÓPRIO")]
        CREDIARIOPROPRIO,

        [Description("CARTÃO CRÉDITO")]
        CARTAOCREDITO,

        [Description("CARTÃO DÉBITO")]
        CARTAODEBITO,

        [Description("CRÉDITO INTERNO")]
        CREDITOINTERNO,
        
        [Description("PIX")]
        PIX,

        [Description("DEBITO EM CONTA")]
        DEBITOEMCONTA,

        [Description("CASHBACK")]
        CASHBACK
    }
}
