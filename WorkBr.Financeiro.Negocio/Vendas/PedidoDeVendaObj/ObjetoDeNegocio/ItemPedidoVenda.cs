using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemPedidoDeVenda : ObjetoDeNegocioBase
    {
        public virtual Produto Produto { get; set; }

        public virtual double ValorUnitario { get; set; }

        public virtual double Quantidade { get; set; }

        public virtual double QuantidadeDevolvida { get; set; }

        public virtual double DescontoUnitario { get; set; }

        public virtual double TotalDesconto { get; set; }

        public virtual double ValorFrete { get; set; }

        public virtual double? ValorSeguro { get; set; }

        public virtual double? ValorOutrasDespesas { get; set; }

        public virtual double? ValorIpi { get; set; }

        public virtual double? ValorIcmsST { get; set; }

        public virtual double? ValorIcms { get; set; }

        public virtual double ValorTotal { get; set; }

        public virtual bool DescontoEhPercentual { get; set; }

        public virtual bool ItemEstahInconsistente { get; set; }
        public virtual double itemReserva { get; set; }

        public virtual PedidoDeVenda PedidoDeVenda { get; set; }

        public virtual TributacaoIcms TributacaoIcms { get; set; }

        public virtual IpiNotaFiscal Ipi { get; set; }

        public virtual PisNotaFiscal Pis { get; set; }

        public virtual CofinsNotaFiscal Cofins {get; set;}

        public virtual string Observacao { get; set; }
    }
}
