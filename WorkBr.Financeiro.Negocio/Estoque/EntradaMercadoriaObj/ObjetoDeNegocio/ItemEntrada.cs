using System;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio
{
    [Serializable]
    public class ItemEntrada: ObjetoDeNegocioBase
    {
        public virtual Produto Produto { get; set; }

        public virtual double Quantidade { get; set; }

        public virtual double QuantidadeBruta { get; set; }

        public virtual UnidadeMedida Unidade { get; set; }

        public virtual EnumOrigem? Origem { get; set; }

        public virtual EnumCstCsosn? CstCsosn { get; set; }

        public virtual Cfop Cfop { get; set; }

        public virtual Ncm Ncm { get; set; }

        public virtual EnumMotivoDesoneracaoProduto? MotivoDesoneracaoProduto { get; set; }

        public virtual double? ValorDesoneracaoProduto { get; set; }

        public virtual double? ValorUnitario { get; set; }

        public virtual double? PercentualDesconto { get; set; }

        public virtual double? ValorDesconto { get; set; }

        public virtual double? OutrasDespesas { get; set; }

        public virtual double? ValorFrete { get; set; }

        public virtual double? ValorTotal { get; set; }

        public virtual double? PercentualIpi { get; set; }

        public virtual double? ValorIpi { get; set; }

        public virtual double? BaseIcms { get; set; }

        public virtual double? PercentualReducao { get; set; }

        public virtual double? PercentualIcms { get; set; }

        public virtual double? ValorIcms { get; set; }

        public virtual double? BaseIcmsSt { get; set; }

        public virtual double? PercentualIva { get; set; }

        public virtual double? AliquotaST { get; set; }

        public virtual double? ValorIcmsSt { get; set; }

        public virtual EntradaMercadoria EntradaMercadoria { get; set; }
    }
}
