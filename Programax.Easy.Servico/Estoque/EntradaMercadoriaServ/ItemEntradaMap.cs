using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class ItemEntradaMap : MapeamentoBase<ItemEntrada>
    {
        public ItemEntradaMap()
        {
            Table("ENTRADASITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEMENT_ID");

            Map(item => item.Quantidade).Column("ITEMENT_QUANTIDADE");
            Map(item => item.QuantidadeBruta).Column("ITEMENT_QUANTIDADE_BRUTA");

            Map(item => item.Origem).Column("ITEMENT_ORIGEM").CustomType<EnumOrigem>();
            Map(item => item.CstCsosn).Column("ITEMENT_CST_CSOSN").CustomType<EnumCstCsosn>();

            Map(item => item.ValorUnitario).Column("ITEMENT_VALOR_UNITARIO");
            Map(item => item.PercentualDesconto).Column("ITEMENT_PERCENTUAL_DESCONTO");
            Map(item => item.ValorDesconto).Column("ITEMENT_VALOR_DESCONTO");
            Map(item => item.OutrasDespesas).Column("ITEMENT_OUTRAS_DESPESAS");
            Map(item => item.ValorFrete).Column("ITEMENT_VALOR_FRETE");
            Map(item => item.ValorTotal).Column("ITEMENT_VALOR_TOTAL");

            Map(item => item.PercentualIpi).Column("ITEMENT_PERCENTUAL_IPI");
            Map(item => item.ValorIpi).Column("ITEMENT_VALOR_IPI");

            Map(item => item.BaseIcms).Column("ITEMENT_BASE_ICMS");
            Map(item => item.PercentualReducao).Column("ITEMENT_PERCENTUAL_REDUCAO");
            Map(item => item.PercentualIcms).Column("ITEMENT_PERCENTUAL_ICMS");
            Map(item => item.ValorIcms).Column("ITEMENT_VALOR_ICMS");

            Map(item => item.ValorDesoneracaoProduto).Column("ITEMENT_VALOR_ICMS_DESONERACAO");
            Map(item => item.MotivoDesoneracaoProduto).Column("ITEMENT_MOTIVO_DESONERACAO").CustomType<EnumMotivoDesoneracaoProduto>();

            Map(item => item.BaseIcmsSt).Column("ITEMENT_BASE_ICMS_ST");
            Map(item => item.PercentualIva).Column("ITEMENT_PERCENTUAL_IVA");
            Map(item => item.AliquotaST).Column("ITEMENT_ALIQUOTA_ICMS_ST");
            Map(item => item.ValorIcmsSt).Column("ITEMENT_VALOR_ICMS_ST");

            References(item => item.Produto).Column("ITEMENT_PROD_ID");
            References(item => item.Unidade).Column("ITEMENT_UND_ID");
            References(item => item.Cfop).Column("ITEMENT_CFOP_ID");
            References(item => item.Ncm).Column("ITEMENT_NCM_ID");
            References(item => item.EntradaMercadoria).Column("ITEMENT_ENTRADA_ID");
        }
    }
}
