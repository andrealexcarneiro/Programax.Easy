using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoFederalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.TributacaoIpiServ
{
    public class GrupoTributacaoFederalMap : MapeamentoBase<GrupoTributacaoFederal>
    {
        public GrupoTributacaoFederalMap()
        {
            Table("GRUPOTRIBUTACOESFEDERAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("GRPTRIBFED_ID");

            Map(grupoTrib => grupoTrib.Descricao).Column("GRPTRIBFED_DESCRICAO");
            Map(grupoTrib => grupoTrib.NaturezaProduto).Column("GRPTRIBFED_NATUREZA_OPERACAO").CustomType<EnumNaturezaProduto>();
            Map(grupoTrib => grupoTrib.RegimeTributario).Column("GRPTRIBFED_REGIME_TRIBUTARIO").CustomType<EnumCodigoRegimeTributario>();

            HasMany(grupoTribFed => grupoTribFed.ListaCofins).KeyColumn("TRIBFEDCOFINS_GRPTRIBFED_COFINS_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("TRIBUTACOESFEDERALCOFINS");
            HasMany(grupoTribFed => grupoTribFed.ListaPis).KeyColumn("TRIBFEDPIS_GRPTRIBFED_PIS_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("TRIBUTACOESFEDERALPIS");
            HasMany(grupoTribFed => grupoTribFed.ListaIpi).KeyColumn("TRIBFEDIPI_GRPTRIBFED_IPI_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("TRIBUTACOESFEDERALIPI");
        }
    }
}
