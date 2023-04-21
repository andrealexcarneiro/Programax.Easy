using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Fiscal.IcmsInterestadualServ
{
    public class IcmsInterestadualMap : MapeamentoBase<IcmsInterestadual>
    {
        public IcmsInterestadualMap()
        {
            Table("ICMSINTERESTADUAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ICMS_ID");

            References(condicao => condicao.Ncm).Column("ICMS_NCM_ID").Not.LazyLoad();
            HasMany(condicao => condicao.ListaIcmsInterestadualEstado).KeyColumn("ICMS_ICMS_INTERESTAUDALID").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("ICMSINTERESTADUALESTADO");
        }
    }
}
