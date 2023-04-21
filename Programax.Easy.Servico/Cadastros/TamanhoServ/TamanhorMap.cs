using Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.TamanhoServ
{
    public class TamanhoMap : MapeamentoBase<Tamanho>
    {
        public TamanhoMap()
        {
            Table("TAMANHOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TAMA_ID");

            Map(tamanho => tamanho.Descricao).Column("TAMA_DESCRICAO");
            Map(tamanho => tamanho.Status).Column("TAMA_STATUS");
            Map(tamanho => tamanho.DataCadastro).Column("TAMA_DATA_CADASTRO");
        }
    }
}
