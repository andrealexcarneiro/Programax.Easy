using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    public class PedidoDeVendaMap : MapeamentoBase<PedidoDeVenda>
    {
        public PedidoDeVendaMap()
        {
            Table("PEDIDOSVENDAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PEDIDO_ID");

            Map(pedido => pedido.AReceberAberto).Column("PEDIDO_RECEBER_ABERTO");
            Map(pedido => pedido.DataElaboracao).Column("PEDIDO_DATA_ELABORACAO");
            Map(pedido => pedido.DataPrevisaoEntrega).Column("PEDIDO_DATA_PREVISAO_ENTREGA");
            Map(pedido => pedido.DataMontagem).Column("PEDIDO_DATA_MONTAGEM");
            Map(pedido => pedido.DataDesmontagem).Column("PEDIDO_DATA_DESMONTAGEM");
            Map(pedido => pedido.Desconto).Column("PEDIDO_DESCONTO");
            Map(pedido => pedido.DescontoEhPercentual).Column("PEDIDO_DESCONTO_EH_PERCENTUAL");
            Map(pedido => pedido.LimiteDeCredito).Column("PEDIDO_LIMITE_CREDITO");
            Map(pedido => pedido.MaiorCompra).Column("PEDIDO_MAIOR_COMPRA");
            Map(pedido => pedido.ObservacoesGeraisVenda).Column("PEDIDO_OBS_GERAL_VENDA");
            Map(pedido => pedido.ObservacoesNotaFiscal).Column("PEDIDO_OBS_NOTA_FISCAL");
            Map(pedido => pedido.SaldoDisponivel).Column("PEDIDO_SALDO_DISPONIVEL");
            Map(pedido => pedido.StatusPedidoVenda).Column("PEDIDO_STATUS").CustomType<EnumStatusPedidoDeVenda>();
            Map(pedido => pedido.TipoFrete).Column("PEDIDO_TIPO_FRETE").CustomType<EnumTipoFrete>();
            Map(pedido => pedido.TipoPedidoVenda).Column("PEDIDO_TIPO_PEDIDO_VENDA").CustomType<EnumTipoPedidoDeVenda>(); ;
            Map(pedido => pedido.ValorFrete).Column("PEDIDO_VALOR_FRETE");
            Map(pedido => pedido.Volume).Column("PEDIDO_VOLUME");
            Map(pedido => pedido.ValorTotal).Column("PEDIDO_VALOR_TOTAL");
            Map(pedido => pedido.DataFechamento).Column("PEDIDO_DATA_FECHAMENTO");
            Map(pedido => pedido.PedidoDoPdv).Column("PEDIDO_DO_PDV");
            Map(pedido => pedido.PedidoExportadoParaPdvEcf).Column("PEDIDO_JAH_EXPORTADO_PDV_ECF");
            Map(pedido => pedido.ValorIcmsST).Column("PEDIDO_VALOR_ICMS_ST");

            Map(pedido => pedido.EstahPago).Column("PEDIDO_ESTAH_PAGO");
            Map(pedido => pedido.Carteira).Column("PEDIDO_CARTEIRA");

            Map(pedido => pedido.TipoCliente).Column("PEDIDO_TIPO_CLIENTE").CustomType<EnumTipoCliente>();

            Map(pedido => pedido.StatusRoteiro).Column("PEDIDO_STATUS_ROTEIRO").CustomType<EnumStatusRoteiro>();

            References(pedido => pedido.Atendente).Column("PEDIDO_ATENDENTE_ID");
            References(pedido => pedido.Cliente).Column("PEDIDO_CLIENTE_ID").Not.LazyLoad().Fetch.Select(); ;
            References(pedido => pedido.CondicaoPagamento).Column("PEDIDO_COND_PAGAMENTO_ID").Not.LazyLoad().Fetch.Select();
            References(pedido => pedido.FormaPagamento).Column("PEDIDO_FORMA_PAGAMENTO_ID").Not.LazyLoad().Fetch.Join();
            References(pedido => pedido.Indicador).Column("PEDIDO_INDICADOR_ID");
            References(pedido => pedido.Vendedor).Column("PEDIDO_VENDEDOR_ID").Not.LazyLoad().Fetch.Select();
            References(pedido => pedido.Transportadora).Column("PEDIDO_TRANSPORTADORA_ID");
            References(pedido => pedido.Usuario).Column("PEDIDO_USUARIO_ID");
            References(pedido => pedido.Supervisor).Column("PEDIDO_SUPERVISOR_ID");
            References(pedido => pedido.TabelaPreco).Column("PEDIDO_TABELA_PRECO_ID").Not.LazyLoad().Fetch.Join();
            References(pedido => pedido.NaturezaOperacao).Column("PEDIDO_NATUREZA_OPERACAO_ID").Not.LazyLoad().Fetch.Join();

            References(pedido => pedido.NotaFiscal).Column("PEDIDO_NOTA_FISCAL_ID");

            HasMany(pedido => pedido.ListaItens).KeyColumn("PEDITEM_PEDIDO_ID").Cascade.AllDeleteOrphan().Inverse().AsBag().OrderBy("PEDITEM_ID");
            HasMany(pedido => pedido.ListaParcelasPedidoDeVenda).KeyColumn("PARCELA_PEDIDO_VENDA_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();

            Component(pedido => pedido.EnderecoPedidoDeVenda, enderecoPedidoDeVenda =>
            {
                enderecoPedidoDeVenda.Map(produto => produto.Complemento).Column("PEDIDO_COMPLEMENTO_ENDERECO").Length(2147483647).LazyLoad();
                enderecoPedidoDeVenda.Map(produto => produto.Numero).Column("PEDIDO_NUMERO_ENDERECO");
                enderecoPedidoDeVenda.Map(produto => produto.TipoEndereco).Column("PEDIDO_TIPO_ENDERECO").CustomType<EnumTipoEndereco>();

                enderecoPedidoDeVenda.Map(produto => produto.CEP).Column("PEDIDO_CEP_ENDERECO");
                enderecoPedidoDeVenda.Map(produto => produto.Rua).Column("PEDIDO_RUA_ENDERECO");
                enderecoPedidoDeVenda.Map(produto => produto.Bairro).Column("PEDIDO_BAIRRO_ENDERECO");
                enderecoPedidoDeVenda.References(produto => produto.Cidade).Column("PEDIDO_CIDADE_ID_ENDERECO").Not.LazyLoad().Fetch.Join();
            });

            Component(pedido => pedido.MotivoBloqueio, motivoBloqueioPedidoDeVenda =>
            {
                motivoBloqueioPedidoDeVenda.Map(motivo => motivo.ClienteBloqueado).Column("PEDIDO_MOTIVO_CLIENTE_BLOQUEADO");
                motivoBloqueioPedidoDeVenda.Map(motivo => motivo.DescontoAcima).Column("PEDIDO_MOTIVO_DESCONTO_ACIMA");
                motivoBloqueioPedidoDeVenda.Map(motivo => motivo.FinanceiroEmAberto).Column("PEDIDO_MOTIVO_FINANCEIRO_ABERTO");
                motivoBloqueioPedidoDeVenda.Map(motivo => motivo.ItemSemEstoque).Column("PEDIDO_MOTIVO_ITEM_SEM_ESTOQUE");
                motivoBloqueioPedidoDeVenda.Map(motivo => motivo.SemSaldoCredito).Column("PEDIDO_MOTIVO_SEM_SALDO_CREDITO");
            });
        }
    }
}
