using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ContaPagarReceberPesquisa : ObjetoDeNegocioBase
    {
        public virtual DateTime DataEmissao { get; set; }

        public virtual DateTime? DataVencimento { get; set; }

        public virtual DateTime? DataPagamento { get; set; }

        public virtual EnumTipoOperacaoContasPagarReceber TipoOperacao { get; set; }

        public virtual EnumStatusContaPagarReceber Status { get; set; }

        public virtual int PessoaId { get; set; }

        public virtual int UsuarioId { get; set; }

        public virtual int FormaPagamentoId { get; set; }

        public virtual int PlanoDeContasId { get; set; }

        public virtual int BancoParaMovimentoId { get; set; }

        public virtual int CategoriaFinanceiraId { get; set; }

        public virtual int OperadorasCartaoId { get; set; }

        public virtual string Historico { get; set; }

        public virtual double ValorParcela { get; set; }

        public virtual double Multa { get; set; }

        public virtual double Juros { get; set; }

        public virtual double Desconto { get; set; }

        public virtual bool ehCalculoDeJurosMultaManual { get; set; }

        public virtual bool ehCalculoMultaAutomatica {get;set;}

        public virtual double ValorPago { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual EnumOrigemDocumento OrigemDocumento { get; set; } //0 - Parcelas geradas pelo pedido de vendas e 1 - Direto do contas a receber

        public virtual bool MultaEhPercentual { get; set; }

        public virtual bool JurosEhPercentual { get; set; }

        public virtual int? ChequeId {get;set;}

        public virtual bool EhConciliacao { get; set; }

        public virtual int CondicaoPgtoId { get; set; }

        public virtual bool EhRecebimento { get; set; }

        public virtual DateTime DataPedidoElaboracao { get; set; }
    }
}
