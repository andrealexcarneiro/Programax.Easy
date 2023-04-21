using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.CategoriaServ
{
    public class CategoriaMap : MapeamentoBase<Categoria>
    {
        public CategoriaMap()
        {
            Table("PRODUTOSLINHAS");

            Id(linha => linha.Id).Column("LINHA_ID");

            Map(linha => linha.Descricao).Column("LINHA_DESCRICAO");
            Map(linha => linha.DataCadastro).Column("LINHA_DATA_CADASTRO");
            Map(linha => linha.Status).Column("LINHA_STATUS");
        }
    }
}
