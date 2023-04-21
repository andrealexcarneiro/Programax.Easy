using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class FormaPagamentoNotaFiscal : ObjetoDeNegocioBase
    {
        public virtual double ValorPagamento { get; set; }

        public virtual EnumFormaPagamentoNfce FormaPagamentoNfce { get; set; }

        public virtual CartaoFormaPagamentoNotaFiscal Cartao { get; set; }        

        public virtual NotaFiscal NotaFiscal { get; set; }
    }
}
