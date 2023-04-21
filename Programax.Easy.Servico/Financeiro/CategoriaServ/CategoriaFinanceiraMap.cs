using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.CategoriaServ
{
    public class CategoriaFinanceiraMap : MapeamentoBase<CategoriaFinanceira>
    {
        public CategoriaFinanceiraMap()
        {
            Table("CATEGORIAS");

            Id(categoria => categoria.Id).Column("CAT_ID");

            Map(categoria => categoria.Descricao).Column("CAT_DESCRICAO");
            Map(categoria => categoria.DataCadastro).Column("CAT_DATA_CADASTRO");
            Map(categoria => categoria.Status).Column("CAT_STATUS");
            Map(categoria => categoria.Mostrar).Column("CAT_MOSTRAR");
            Map(categoria => categoria.MostrarDRE).Column("CAT_MOSTRARDRE");

            Map(categoria => categoria.TipoCategoria).Column("CAT_TIPO_CATEGORIA").CustomType<EnumTipoCategoria>();

            References(categoria => categoria.SubGrupoCategoria).Column("CAT_SUBGRUPO_ID").LazyLoad();
        }
    }
}
