using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ContaPagarReceber : ObjetoDeNegocioBase
    {
        public ContaPagarReceber()
        {
            ListaHistoricoAlteracoesVencimento = new List<HistoricoAlteracaoVencimento>();
            ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();
        }

        public virtual DateTime DataEmissao { get; set; }

        public virtual DateTime? DataVencimento { get; set; }

        public virtual DateTime? DataPagamento { get; set; }

        public virtual EnumTipoOperacaoContasPagarReceber TipoOperacao { get; set; }

        public virtual EnumStatusContaPagarReceber Status { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Pessoa Usuario { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual PlanoDeContas PlanoDeContas { get; set; }

        public virtual BancoParaMovimento BancoParaMovimento { get; set; }

        public virtual CategoriaFinanceira CategoriaFinanceira { get; set; }

        public virtual OperadorasCartao OperadorasCartao { get; set; }

        public virtual string Historico { get; set; }

        public virtual double ValorParcela { get; set; }

        public virtual bool EstahAlterandoParcela { get; set; }

        public virtual double Multa { get; set; }

        public virtual double Juros { get; set; }

        public virtual double Desconto { get; set; }

        public virtual bool ehCalculoDeJurosMultaManual { get; set; }

        public virtual bool ehCalculoMultaAutomatica {get;set;}

        public virtual double ValorTotal
        {
            get
            {   
                return CalculoValorTotalContaPagarReceber.CalculeValorTotalContaPagarReceber (this, ehCalculoDeJurosMultaManual, ehCalculoMultaAutomatica);                
            }           
        }

        public virtual double ValorPago { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual EnumOrigemDocumento? OrigemDocumento { get; set; } //0 - Parcelas geradas pelo pedido de vendas e 1 - Direto do contas a receber

        public virtual bool MultaEhPercentual { get; set; }

        public virtual bool JurosEhPercentual { get; set; }

        public virtual IList<ContaPagarReceberPagamento> ListaContasPagarReceberParcial { get; set; }

        public virtual IList<HistoricoAlteracaoVencimento> ListaHistoricoAlteracoesVencimento { get; set; }

        public virtual int? ChequeId {get;set;}

        public virtual bool EhConciliacao { get; set; }

        public virtual int CondicaoPgtoId { get; set; }

        public virtual bool EhRecebimento { get; set; }

        public virtual DateTime DataPedidoElaboracao { get; set; }
    }
}
