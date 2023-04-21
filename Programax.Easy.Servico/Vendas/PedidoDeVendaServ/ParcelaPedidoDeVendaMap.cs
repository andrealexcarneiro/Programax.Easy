using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    public class ParcelaPedidoDeVendaMap : MapeamentoBase<ParcelaPedidoDeVenda>
    {
        public ParcelaPedidoDeVendaMap()
        {
            Table("PEDIDOSVENDASPARCELAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PARCELA_ID");

            Map(parcela => parcela.DataVencimento).Column("PARCELA_DATA_VENCIMENTO");
            Map(parcela => parcela.NumeroDocumento).Column("PARCELA_NUMERO_DOCUMENTO");
            Map(parcela => parcela.Parcela).Column("PARCELA_NUMERO_PARCELA");
            Map(parcela => parcela.Valor).Column("PARCELA_VALOR");

            References(parcela => parcela.CondicaoPagamento).Column("PARCELA_CONDICAO_PAGAMENTO_ID").Not.LazyLoad().Fetch.Select();
            References(parcela => parcela.FormaPagamento).Column("PARCELA_FORMA_PAGAMENTO_ID").Not.LazyLoad().Fetch.Join();
            References(parcela => parcela.PedidoDeVenda).Column("PARCELA_PEDIDO_VENDA_ID");
            References(parcela => parcela.ContaPagarReceber).Column("PARCELA_CONTA_PAGAR_RECEBER_ID"); 
            References(parcela => parcela.Operadoras).Column("PARCELA_OPERADORA_ID").Not.LazyLoad().Fetch.Join();
        }
    }
}
