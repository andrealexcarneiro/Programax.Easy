using Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Fiscal.CestServ
{
    public class CestMap:MapeamentoBase<Cest>
    {
        public CestMap()
        {
            Table("CEST");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(cest => cest.DescricaoCest).Column("DESCRICAO");
            Map(cest => cest.CodigoNcm).Column("NCM");
            Map(cest => cest.CodigoCest).Column("CEST");
            Map(cest => cest.Status).Column("STATUS");
            Map(cest => cest.DataCadastro).Column("DATACADASTRO");
        }
    }
}
