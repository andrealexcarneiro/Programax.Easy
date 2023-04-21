using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Vendas.TrocaPedidoDeVendaServ
{
    public class TrocaPedidoDeVendaMap : MapeamentoBase<TrocaPedidoDeVenda>
    {
        public TrocaPedidoDeVendaMap()
        {
            Table("TROCA_PEDIDO_VENDA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("TROCA_ID");

            Map(troca => troca.Status).Column("TROCA_STATUS").CustomType<EnumStatusTrocaPedidoDeVenda>();
            Map(troca => troca.TipoMovimentacaoFinanceira).Column("TROCA_TIPO_MOVIMENTACAO_FINANCEIRA").CustomType<EnumTipoMovimentacaoFinanceiraTrocaPedidoDeVenda>();

            Map(troca => troca.NumeroDocumento).Column("TROCA_NUMERO_DOCUMENTO");
            Map(troca => troca.DataVencimento).Column("TROCA_DATA_VENCIMENTO");
            Map(troca => troca.DataFechamento).Column("TROCA_DATA_FECHAMENTO");
            Map(troca => troca.DataElaboracao).Column("TROCA_DATA_ELABORACAO");
            Map(troca => troca.ValorTotalTroca).Column("TROCA_VALOR_TOTAL");

            References(troca => troca.FormaPagamento).Column("TROCA_FORMA_PAGAMENTO_ID");
            References(troca => troca.PedidoDeVenda).Column("TROCA_PEDIDO_ID");
            References(troca => troca.MotivoTroca).Column("TROCA_MOTIVO_ID");
            References(troca => troca.UsuarioRealizouTroca).Column("TROCA_PESSOA_ID");
            
            HasMany(troca => troca.ListaItens).KeyColumn("ITEM_TROCA_PEDIDO_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
            HasMany(troca => troca.ListaItensPedido).KeyColumn("ITEM_TROCA_PEDIDO_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
        }
    }
}
