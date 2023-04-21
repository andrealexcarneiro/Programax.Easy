using Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Fiscal.CnaeServ
{
    public class CnaeMap : MapeamentoBase<Cnae>
    {
        public CnaeMap()
        {
            Table("CNAE");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CNAE_ID");

            Map(condicao => condicao.Codigo).Column("CNAE_CODIGO");
            Map(condicao => condicao.Atividade).Column("CNAE_ATIVIDADE").CustomType<EnumAtividadeCnae>();

            Map(condicao => condicao.DataCadastro).Column("CNAE_DATA_REG");
            Map(condicao => condicao.Descricao).Column("CNAE_DESCRICAO");
            Map(condicao => condicao.Status).Column("CNAE_STATUS");
        }
    }
}
