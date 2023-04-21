using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.GruposCategoriasServ
{
    public class SubGrupoCategoriaMap:MapeamentoBase<SubGrupoCategoria>
    {
        public SubGrupoCategoriaMap()
            : base()
        {
            Table("SUBGRUPOSCATEGORIAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("SUBGRUPOCAT_ID");

            Map(grupo => grupo.Ativo).Column("SUBGRUPOCAT_STATUS");
            Map(grupo => grupo.DataCadastro).Column("SUBGRUPOCAT_DATA_CADASTRO");
            Map(grupo => grupo.Descricao).Column("SUBGRUPOCAT_DESCRICAO");

            References(grupo => grupo.Grupo).Column("SUB_GRUPO_ID").LazyLoad();
        }
    }
}
