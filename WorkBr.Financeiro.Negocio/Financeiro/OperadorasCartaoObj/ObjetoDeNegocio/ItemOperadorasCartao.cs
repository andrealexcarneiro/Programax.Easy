using System;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemOperadorasCartao : ObjetoDeNegocioBase
    {
        public virtual CondicaoPagamento CondicaoPagamento { get; set; }
        
        public virtual int CobrarApartirDaParcela { get; set; }

        public virtual double Taxa { get; set; }

        public virtual int OperadorasId {get;set; }
    }
}
