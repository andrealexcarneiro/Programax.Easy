using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ParcelaCondicaoPagamento: ObjetoDeNegocioBase
    {
        public virtual int NumeroParcela { get; set; }
                 
        public virtual int Dias { get; set; }
                 
        public virtual double PercentualRateio { get; set; }
                 
        public virtual double PercentualAcrescimo { get; set; }

        public virtual CondicaoPagamento CondicaoPagamento { get; set; }
    }
}
