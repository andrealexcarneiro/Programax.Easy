using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.GrupoTributacaoFederalServ
{
    public class TributacaoFederalIpiMap : MapeamentoBase<IpiNotaFiscal>
    {
        public TributacaoFederalIpiMap()
        {
            Table("TRIBUTACOESFEDERALIPI");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TRIBFEDIPI_ID");

            Map(grupoTrib => grupoTrib.BaseDeCalculo).Column("TRIBFEDIPI_BASE_DE_CALCULO");           
            Map(grupoTrib => grupoTrib.AliquotaIpi).Column("TRIBFEDIPI_ALIQUOTA_IPI");                        
            Map(grupoTrib => grupoTrib.ValorIpi).Column("TRIBFEDIPI_VALOR_IPI");        
            Map(grupoTrib => grupoTrib.CstIpi).Column("TRIBFEDIPI_CST_IPI").CustomType<EnumCstIpi>();
            Map(grupoTrib => grupoTrib.EstadoDestino).Column("TRIBFEDIPI_ESTADO_DESTINO");            
            Map(grupoTrib => grupoTrib.TipoCliente).Column("TRIBFEDIPI_TIPO_CLIENTE").CustomType<EnumTipoCliente>();
            Map(grupoTrib => grupoTrib.TipoSaida).Column("TRIBFEDIPI_TIPO_SAIDA").CustomType<EnumTipoSaidaTributacaoIcms>();
            
            References(grupoTrib => grupoTrib.GrupoTributacaoFederal).Column("TRIBFEDIPI_GRPTRIBFED_IPI_ID");
        }
    }
}
