using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    public class GrupoCategoriaMap:MapeamentoBase<GrupoCategoria>
    {
        public GrupoCategoriaMap()
            : base()
        {
            Table("GRUPOSCATEGORIAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("GRUPOCAT_ID");

            Map(grupo => grupo.Ativo).Column("GRUPOCAT_STATUS");
            Map(grupo => grupo.DataCadastro).Column("GRUPOCAT_DATA_CADASTRO");
            Map(grupo => grupo.Descricao).Column("GRUPOCAT_DESCRICAO");
        }
    }
}
