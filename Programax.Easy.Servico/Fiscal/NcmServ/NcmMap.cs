using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Fiscal.NcmServ
{
    public class NcmMap : MapeamentoBase<Ncm>
    {
        public NcmMap()
        {
            Table("CLASSIFICACAOFISCAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CFISC_ID");

            Map(ncm => ncm.Descricao).Column("CFISC_DESCRICAO");
            Map(ncm => ncm.CodigoNcm).Column("CFISC_NCM");
            Map(ncm => ncm.Cest).Column("CFISC_CEST");
            Map(ncm => ncm.Status).Column("CFISC_STATUS");
            Map(ncm => ncm.DataCadastro).Column("CFISC_DATA_REG");
            Map(ncm => ncm.ImpostoIbptFederalNacional).Column("CFISC_IMPOSTO_IBPT_FEDERAL_NACIONAL");
            Map(ncm => ncm.ImpostoIbptFederalImportados).Column("CFISC_IMPOSTO_IBPT_FEDERAL_IMPORTADOS");
            Map(ncm => ncm.ImpostoIbptEstadual).Column("CFISC_IMPOSTO_IBPT_ESTADUAL");
            Map(ncm => ncm.ImpostoIbptMunicipal).Column("CFISC_IMPOSTO_IBPT_MUNICIPAL");
            Map(ncm => ncm.DataValidadeIbpt).Column("CFISC_DATA_VALIDADE_IBPT");
            Map(ncm => ncm.ChaveTabelaIbpt).Column("CFISC_CHAVE_TABELA_IBPT");
        }
    }
}
