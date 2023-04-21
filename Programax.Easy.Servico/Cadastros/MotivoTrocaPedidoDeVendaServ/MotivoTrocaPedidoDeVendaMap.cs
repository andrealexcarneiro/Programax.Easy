using Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Cadastros.MotivoTrocaPedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Cadastros.MotivoTrocaPedidoDeVendaServ
{
    public class MotivoTrocaPedidoDeVendaMap : MapeamentoBase<MotivoTrocaPedidoDeVenda>
    {
        public MotivoTrocaPedidoDeVendaMap()
        {
            Table("MOTIVOSTROCAPEDIDOVENDA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("MOTIVO_ID");

            Map(motivo => motivo.Descricao).Column("MOTIVO_DESCRICAO");
            Map(motivo => motivo.Status).Column("MOTIVO_STATUS");
            Map(motivo => motivo.DataCadastro).Column("MOTIVO_DATA_CADASTRO");
        }
    }
}
