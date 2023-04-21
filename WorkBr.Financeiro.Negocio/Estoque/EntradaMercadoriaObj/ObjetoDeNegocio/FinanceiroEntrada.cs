using System;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio
{
    [Serializable]
    public class FinanceiroEntrada : ObjetoDeNegocioBase
    {
        public virtual string Parcela { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual DateTime DataVencimento { get; set; }

        public virtual double ValorDuplicata { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual EntradaMercadoria EntradaMercadoria { get; set; }

        public virtual ContaPagarReceber ContaPagar { get; set; }
    }
}
