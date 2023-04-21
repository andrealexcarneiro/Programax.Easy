using Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.UnidadeMedidaServ
{
    public class UnidadeMedidaMap : MapeamentoBase<UnidadeMedida>
    {
        public UnidadeMedidaMap()
        {
            Table("UNIDADESMEDIDAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("UND_ID");

            Map(unidade=> unidade.Descricao).Column("UND_DESCRICAO");
            Map(unidade=> unidade.Abreviacao).Column("UND_ABREVIACAO");
            Map(unidade=> unidade.Status).Column("UND_STATUS");
            Map(unidade=> unidade.DataCadastro).Column("UND_DATA_CADASTRO");
        }
    }
}
