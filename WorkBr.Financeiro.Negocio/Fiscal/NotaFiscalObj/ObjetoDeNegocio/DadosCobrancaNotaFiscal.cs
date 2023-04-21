using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio
{
    [Serializable]
    public class DadosCobrancaNotaFiscal
    {
        public DadosCobrancaNotaFiscal()
        {
            ListaDuplicatas = new List<DuplicataNotaFiscal>();
            ListaDeParcelasVendas = new List<ParcelaPedidoDeVenda>();
        }

        public virtual FaturaNotaFiscal FaturaNotaFiscal { get; set; }

        public virtual EnumCondicaoVistaPrazo CondicaoVistaPrazo {get;set;}

        public virtual EnumFormaPagamentoNfce FormaPagamentoNF { get; set; }

        public virtual double TotalDePagamento { get; set; }

        public List<ParcelaPedidoDeVenda> ListaDeParcelasVendas { get; set; }

        public IList<DuplicataNotaFiscal> ListaDuplicatas { get; set; }
    }
}
