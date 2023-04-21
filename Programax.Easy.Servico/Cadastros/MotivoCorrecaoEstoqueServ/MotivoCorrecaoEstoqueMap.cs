using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Cadastros.MotivoCorrecaoEstoqueServ
{
    public class MotivoCorrecaoEstoqueMap : MapeamentoBase<MotivoCorrecaoEstoque>
    {
        public MotivoCorrecaoEstoqueMap()
        {
            Table("MOTIVOSCORRECAOESTOQUE");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("MOTIVO_ID");

            Map(motivo => motivo.Descricao).Column("MOTIVO_DESCRICAO");
            Map(motivo => motivo.Status).Column("MOTIVO_STATUS");
            Map(motivo => motivo.DataCadastro).Column("MOTIVO_DATA_CADASTRO");
        }
    }
}
