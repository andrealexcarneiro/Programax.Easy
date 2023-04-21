using Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.CorServ
{
    public class CorMap : MapeamentoBase<Cor>
    {
        public CorMap()
        {
            Table("CORES");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("COR_ID");

            Map(cor => cor.Descricao).Column("COR_DESCRICAO");
            Map(cor => cor.DataCadastro).Column("COR_DATA_CADASTRO");
            Map(cor => cor.Status).Column("COR_STATUS");
        }
    }
}
