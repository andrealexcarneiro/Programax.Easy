using Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Fiscal.IcmsInterestadualServ
{
    public class IcmsInterestadualEstadoMap : MapeamentoBase<IcmsInterestadualEstado>
    {
        public IcmsInterestadualEstadoMap()
        {
            Table("ICMSINTERESTADUALESTADO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ICMS_ID");

            Map(icms => icms.AliquotaInterna).Column("ICMS_ALIQUOTA_INTERNA");
            Map(icms => icms.FCP).Column("ICM_FCP");
            Map(icms => icms.UF).Column("ICMS_UF");

            References(icms => icms.IcmsInterestadual).Column("ICMS_ICMS_INTERESTAUDALID");
        }
    }
}
