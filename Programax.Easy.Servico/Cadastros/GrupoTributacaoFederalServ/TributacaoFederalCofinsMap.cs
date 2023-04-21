using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ
{
    public class TributacaoFederalCofinsMap : MapeamentoBase<CofinsNotaFiscal>
    {
        public TributacaoFederalCofinsMap()
        {
            Table("TRIBUTACOESFEDERALCOFINS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TRIBFEDCOFINS_ID");

            Map(grupoTrib => grupoTrib.BaseDeCalculo).Column("TRIBFEDCOFINS_BASE_DE_CALCULO");
            Map(grupoTrib => grupoTrib.BaseDeCalculoST).Column("TRIBFEDCOFINS_BASE_DE_CALCULO_ST");
            Map(grupoTrib => grupoTrib.QuantidadeVendida).Column("TRIBFEDCOFINS_QUANTIDADE_VENDIDA");
            Map(grupoTrib => grupoTrib.QuantidadeVendidaST).Column("TRIBFEDCOFINS_QUANTIDADE_VENDIDA_ST");
            Map(grupoTrib => grupoTrib.AliquotaPercentual).Column("TRIBFEDCOFINS_ALIQUOTA_PERCENTUAL");
            Map(grupoTrib => grupoTrib.AliquotaPercentualST).Column("TRIBFEDCOFINS_ALIQUOTA_PERCENTUAL_ST");
            Map(grupoTrib => grupoTrib.AliquotaReais).Column("TRIBFEDCOFINS_ALIQUOTA_REAIS");
            Map(grupoTrib => grupoTrib.AliquotaReaisST).Column("TRIBFEDCOFINS_ALIQUOTA_REAIS_ST");                        
            Map(grupoTrib => grupoTrib.ValorCofins).Column("TRIBFEDCOFINS_VALOR_COFINS");
            Map(grupoTrib => grupoTrib.ValorCofinsST).Column("TRIBFEDCOFINS_VALOR_COFINS_ST");
            Map(grupoTrib => grupoTrib.CstCofins).Column("TRIBFEDCOFINS_CST_COFINS").CustomType<EnumCstCofins>();
            Map(grupoTrib => grupoTrib.EstadoDestino).Column("TRIBFEDCOFINS_ESTADO_DESTINO");            
            Map(grupoTrib => grupoTrib.TipoCliente).Column("TRIBFEDCOFINS_TIPO_CLIENTE").CustomType<EnumTipoCliente>();
            Map(grupoTrib => grupoTrib.TipoSaida).Column("TRIBFEDCOFINS_TIPO_SAIDA").CustomType<EnumTipoSaidaTributacaoIcms>();
            
            References(grupoTrib => grupoTrib.GrupoTributacaoFederal).Column("TRIBFEDCOFINS_GRPTRIBFED_COFINS_ID");
        }
    }
}
