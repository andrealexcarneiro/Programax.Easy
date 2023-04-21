using System;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio
{
    [Serializable]
    public class ParametrosFinanceiro
    {
        public virtual double PercentualDespesasFixas { get; set; }

        public virtual double PercentualDespesasVariaveis { get; set; }

        public virtual double PercentualImpostos { get; set; }

        public virtual double PercentualOutrasDespesas { get; set; }

        public virtual double PercentualFrete { get; set; }

        public virtual double PercentualComissoes { get; set; }

        public virtual double PercentualLucro { get; set; }

        public virtual double ValoPadraoCreditoInicial { get; set; }

        public virtual double MultaContasReceber { get; set; }

        public virtual double JurosContasReceber { get; set; }

        public virtual bool QuestionarSeDesejaEmitirNotaAoReceberPedido { get; set; }

        public virtual bool QuestionarSeDesejaExportarVendaParaPdv { get; set; }

        public virtual bool IgnorarCreditoInicial { get; set; }

        public virtual bool HabilitarResumoFinanceiro { get; set; }

        //Conciliação Bancária
        public virtual bool HabilitarConciliacaoBancaria { get; set; }
        public virtual bool ImportacaoAutomaticaExtrato { get; set; }
        public virtual int DiasAntes { get; set; }
        public virtual int DiasDepois { get; set; }

        public virtual bool AbrirResumoFinanceiroAoIniciarAkil { get; set; }

        public virtual string ObservacoesCarnePagamento { get; set; }

    }
}
