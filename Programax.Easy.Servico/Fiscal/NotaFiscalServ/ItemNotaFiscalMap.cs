using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Servico.Fiscal.NotaFiscalServ
{
    public class ItemNotaFiscalMap : MapeamentoBase<ItemNotaFiscal>
    {
        public ItemNotaFiscalMap()
        {
            Table("NOTAS_FISCAIS_ITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEM_ID");

            MapeieDadosItem();
            MapeieImpostosItem();
        }

        private void MapeieDadosItem()
        {
            Map(item => item.Cfop).Column("ITEM_CFOP");
            Map(item => item.CodigoBarrasProduto).Column("ITEM_COD_BARRAS_PROD");
            Map(item => item.CodigoGtinProduto).Column("ITEM_GTIN");
            Map(item => item.Ncm).Column("ITEM_NCM");
            Map(item => item.Cest).Column("ITEM_CEST");
            Map(item => item.NomeProduto).Column("ITEM_NOME_PRODUTO");
            Map(item => item.Quantidade).Column("ITEM_QUANTIDADE");
            Map(item => item.UnidadeProduto).Column("ITEM_UNIDADE_PRODUTO");
            Map(item => item.ValorDesconto).Column("ITEM_VALOR_DESCONTO");
            Map(item => item.ValorFrete).Column("ITEM_VALOR_FRETE");
            Map(item => item.Seguro).Column("ITEM_VALOR_SEGURO");
            Map(item => item.OutrasDespesas).Column("ITEM_VALOR_OUTRAS_DESPESAS");
            Map(item => item.ValorTotal).Column("ITEM_VALOR_TOTAL");
            Map(item => item.ValorUnitario).Column("ITEM_VALOR_UNITARIO");

            References(produto => produto.Produto).Column("ITEM_PRODUTO_ID").Not.LazyLoad();
            References(produto => produto.NotaFiscal).Column("ITEM_NOTA_FISCAL_ID");
        }

        private void MapeieImpostosItem()
        {
            Component(item => item.Impostos, impostos =>
         {            
             impostos.Component(imp => imp.Pis, pis =>
             {
                 pis.Map(i => i.CstPis).Column("ITEM_PIS_CST").CustomType<EnumCstPis>();
                 pis.Map(i => i.BaseDeCalculo).Column("ITEM_PIS_BASE_CALCULO");
                 pis.Map(i => i.BaseDeCalculoST).Column("ITEM_PIS_BASE_CALCULO_ST");
                 pis.Map(i => i.QuantidadeVendida).Column("ITEM_PIS_QUANTIDADE_VENDIDA");
                 pis.Map(i => i.QuantidadeVendidaST).Column("ITEM_PIS_QUANTIDADE_VENDIDA_ST");
                 pis.Map(i => i.AliquotaPercentual).Column("ITEM_PIS_ALIQUOTA_PERCENTUAL");
                 pis.Map(i => i.AliquotaPercentualST).Column("ITEM_PIS_ALIQUOTA_PERCENTUAL_ST");
                 pis.Map(i => i.AliquotaReais).Column("ITEM_PIS_ALIQUOTA_REAIS");
                 pis.Map(i => i.AliquotaReaisST).Column("ITEM_PIS_ALIQUOTA_REAIS_ST");
                 pis.Map(i => i.ValorPis).Column("ITEM_PIS_VALOR");
                 pis.Map(i => i.ValorPisST).Column("ITEM_PIS_VALOR_ST");
             });

             impostos.Component(imp => imp.Cofins, cofins =>
             {
                 cofins.Map(i => i.CstCofins).Column("ITEM_COFINS_CST").CustomType<EnumCstCofins>();
                 cofins.Map(i => i.BaseDeCalculo).Column("ITEM_COFINS_BASE_CALCULO");
                 cofins.Map(i => i.BaseDeCalculoST).Column("ITEM_COFINS_BASE_CALCULO_ST");
                 cofins.Map(i => i.QuantidadeVendida).Column("ITEM_COFINS_QUANTIDADE_VENDIDA");
                 cofins.Map(i => i.QuantidadeVendidaST).Column("ITEM_COFINS_QUANTIDADE_VENDIDA_ST");
                 cofins.Map(i => i.AliquotaPercentual).Column("ITEM_COFINS_ALIQUOTA_PERCENTUAL");
                 cofins.Map(i => i.AliquotaPercentualST).Column("ITEM_COFINS_ALIQUOTA_PERCENTUAL_ST");
                 cofins.Map(i => i.AliquotaReais).Column("ITEM_COFINS_ALIQUOTA_REAIS");
                 cofins.Map(i => i.AliquotaReaisST).Column("ITEM_COFINS_ALIQUOTA_REAIS_ST");
                 cofins.Map(i => i.ValorCofins).Column("ITEM_COFINS_VALOR");
                 cofins.Map(i => i.ValorCofinsST).Column("ITEM_COFINS_VALOR_ST");
             });

             impostos.Component(imp => imp.Ipi, ipi =>
             {
                 ipi.Map(i => i.AliquotaIpi).Column("ITEM_IPI_ALIQUOTA");
                 ipi.Map(i => i.BaseDeCalculo).Column("ITEM_IPI_BASE_CALCULO");
                 ipi.Map(i => i.ValorIpi).Column("ITEM_IPI_VALOR");
                 ipi.Map(i => i.CstIpi).Column("ITEM_IPI_CST").CustomType<EnumCstIpi>();
                 ipi.Map(i => i.CodigoEnquadramento).Column("ITEM_IPI_COD_ENQUADRAMENTO");
             });

             impostos.Component(imp => imp.IcmsInterestadual, icmsInter =>
             {
                 icmsInter.Map(i => i.AliquotaFCP).Column("ITEM_ICMSINTERESTADUAL_ALIQUOTA_FCP");
                 icmsInter.Map(i => i.AliquotaInterestadual).Column("ITEM_ICMSINTERESTADUAL_ALIQUOTA_INTERESTADUAL");
                 icmsInter.Map(i => i.AliquotaInterna).Column("ITEM_ICMSINTERESTADUAL_ALIQUOTA_INTERNA");
                 icmsInter.Map(i => i.BaseDeCalculo).Column("ITEM_ICMSINTERESTADUAL_BASE_CALCULO");
                 icmsInter.Map(i => i.PercentualProvisorioPartilha).Column("ITEM_ICMSINTERESTADUAL_PERCENT_PROVISORIO");
                 icmsInter.Map(i => i.ValorFCP).Column("ITEM_ICMSINTERESTADUAL_VALOR_FCP");
                 icmsInter.Map(i => i.ValorIcmsDestino).Column("ITEM_ICMSINTERESTADUAL_VALOR_ICMS_DESTINO");
                 icmsInter.Map(i => i.ValorIcmsOrigem).Column("ITEM_ICMSINTERESTADUAL_VALOR_ICMS_ORIGEM");
             });

             impostos.Component(imp => imp.Icms, icms =>
             {
                 icms.Map(i => i.AliquotaIcms).Column("ITEM_ICMS_ALIQUOTA");
                 icms.Map(i => i.AliquotaIva).Column("ITEM_ICMS_ALIQUOTA_MVA");
                 icms.Map(i => i.AliquotaReducaoIcms).Column("ITEM_ICMS_ALIQUOTA_REDUCAO");
                 icms.Map(i => i.AliquotaReducaoIcmsSubstituicaoTributaria).Column("ITEM_ICMS_ALIQUOTA_REDUCAO_ST");
                 icms.Map(i => i.AliquotaSubstituicaoTributaria).Column("ITEM_ICMS_ALIQUOTA_ST");
                 icms.Map(i => i.AliquotaDbSt).Column("ITEM_ICMS_ALIQUOTA_DB_ST");
                 icms.Map(i => i.AliquotaCrSt).Column("ITEM_ICMS_ALIQUOTA_CR_ST");
                 icms.Map(i => i.BaseCalculoIcms).Column("ITEM_ICMS_BASE_ICMS");
                 icms.Map(i => i.BaseIcmsSubstituicaoTributaria).Column("ITEM_ICMS_BASE_ICMS_ST");
                 icms.Map(i => i.PercentualMargemValorAdicST).Column("ITEM_MARGEM_VALOR_ADIC_ICMS_ST");
                 icms.Map(i => i.CstCsosn).Column("ITEM_ICMS_CSTCSOSN").CustomType<EnumCstCsosn>();
                 icms.Map(i => i.ModBC).Column("ITEM_ICMS_MODBC").CustomType<EnumModBC>();
                 icms.Map(i => i.ModBCST).Column("ITEM_ICMS_MODBCST").CustomType<EnumModBCST>();
                 icms.Map(i => i.MotivoDesoneracaoProduto).Column("ITEM_ICMS_MOTIVO_DESONERACAO").CustomType<EnumMotivoDesoneracaoProduto>();
                 icms.Map(i => i.Origem).Column("ITEM_ICMS_ORIGEM").CustomType<EnumOrigem>();
                 icms.Map(i => i.ValorDesoneracaoProduto).Column("ITEM_ICMS_VALOR_DESONERACAO");
                 icms.Map(i => i.ValorIcms).Column("ITEM_ICMS_VALOR_ICMS");
                 icms.Map(i => i.ValorSubstituicaoTributaria).Column("ITEM_ICMS_VALOR_ST");

                 icms.Map(i => i.AliquotaSimplesNacional).Column("ITEM_ICMS_ALIQUOTA_SIMPLES_NACIONAL");
                 icms.Map(i => i.ValorIcmsSimplesNacional).Column("ITEM_ICMS_VALOR_ICMS_SIMPLES_NACIONAL");
             });

             impostos.Map(total => total.TotalTributacao).Column("ITEM_TRIBUTACAO_TOTAL");
             impostos.Map(total => total.TotalTributacaoEstadual).Column("ITEM_TRIBUTACAO_ESTADUAL");
             impostos.Map(total => total.TotalTributacaoFederal).Column("ITEM_TRIBUTACAO_FEDERAL");
         });
        }
    }
}
