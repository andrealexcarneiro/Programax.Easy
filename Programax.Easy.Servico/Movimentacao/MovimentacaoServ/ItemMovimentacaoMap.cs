using FluentNHibernate.Mapping;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Movimentacao.MovimentacaoServ
{
    public class ItemMovimentacaoMap : MapeamentoBase<ItemMovimentacao>
    {
        public ItemMovimentacaoMap()
        {
            Table("MOVIMENTACOESITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEMMOV_ID");

            Map(item => item.Quantidade).Column("ITEMMOV_QUANTIDADE");

            Map(item => item.TipoMovimentacao).Column("ITEMMOV_TIPO_MOVIMENTACAO").CustomType<GenericEnumMapper<EnumTipoMovimentacao>>();

            Map(item => item.PedidoVenda_Id).Column("ITEMMOV_PEDIDO_ID");

            Map(item => item.EstahEstornado).Column("ITEMMOV_ESTAH_ESTORNADO");
                       
            References(item => item.Produto).Column("ITEMMOV_PROD_ID");

            References(item => item.MovimentacaoBase).Column("ITEMMOV_MOV_ID");        
        }
    }
}
