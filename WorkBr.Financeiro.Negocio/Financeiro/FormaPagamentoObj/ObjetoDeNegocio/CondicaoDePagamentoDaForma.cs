using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class CondicaoDePagamentoDaForma : ObjetoDeNegocioBase
    {
        public virtual CondicaoPagamento CondicaoPagamento { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }
    }
}
