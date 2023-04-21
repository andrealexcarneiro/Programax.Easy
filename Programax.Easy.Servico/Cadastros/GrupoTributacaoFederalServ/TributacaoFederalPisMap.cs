using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ
{
    public class TributacaoFederalPisMap : MapeamentoBase<PisNotaFiscal>
    {
        public TributacaoFederalPisMap()
        {
            Table("TRIBUTACOESFEDERALPIS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TRIBFEDPIS_ID");

            Map(grupoTrib => grupoTrib.BaseDeCalculo).Column("TRIBFEDPIS_BASE_DE_CALCULO");
            Map(grupoTrib => grupoTrib.BaseDeCalculoST).Column("TRIBFEDPIS_BASE_DE_CALCULO_ST");
            Map(grupoTrib => grupoTrib.QuantidadeVendida).Column("TRIBFEDPIS_QUANTIDADE_VENDIDA");
            Map(grupoTrib => grupoTrib.QuantidadeVendidaST).Column("TRIBFEDPIS_QUANTIDADE_VENDIDA_ST");
            Map(grupoTrib => grupoTrib.AliquotaPercentual).Column("TRIBFEDPIS_ALIQUOTA_PERCENTUAL");
            Map(grupoTrib => grupoTrib.AliquotaPercentualST).Column("TRIBFEDPIS_ALIQUOTA_PERCENTUAL_ST");
            Map(grupoTrib => grupoTrib.AliquotaReais).Column("TRIBFEDPIS_ALIQUOTA_REAIS");
            Map(grupoTrib => grupoTrib.AliquotaReaisST).Column("TRIBFEDPIS_ALIQUOTA_REAIS_ST");                        
            Map(grupoTrib => grupoTrib.ValorPis).Column("TRIBFEDPIS_VALOR_PIS");
            Map(grupoTrib => grupoTrib.ValorPisST).Column("TRIBFEDPIS_VALOR_PIS_ST");
            Map(grupoTrib => grupoTrib.CstPis).Column("TRIBFEDPIS_CST_PIS").CustomType<EnumCstPis>();
            Map(grupoTrib => grupoTrib.EstadoDestino).Column("TRIBFEDPIS_ESTADO_DESTINO");            
            Map(grupoTrib => grupoTrib.TipoCliente).Column("TRIBFEDPIS_TIPO_CLIENTE").CustomType<EnumTipoCliente>();
            Map(grupoTrib => grupoTrib.TipoSaida).Column("TRIBFEDPIS_TIPO_SAIDA").CustomType<EnumTipoSaidaTributacaoIcms>();
            
            References(grupoTrib => grupoTrib.GrupoTributacaoFederal).Column("TRIBFEDPIS_GRPTRIBFED_PIS_ID");
        }
    }
}
