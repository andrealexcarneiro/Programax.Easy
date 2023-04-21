using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ
{
    public class ItemMovimentacaoCaixaMap : MapeamentoBase<ItemMovimentacaoCaixa>
    {
        public ItemMovimentacaoCaixaMap()
        {
            Table("MOVIMENTACOESCAIXASITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEMCAIXA_ID");

            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.DataHora).Column("ITEMCAIXA_DATA_HORA");
            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.EstahEstornado).Column("ITEMCAIXA_ESTAH_ESTORNADO");
            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.HistoricoMovimentacoes).Column("ITEMCAIXA_HISTORICO_MOVIMENTACOES");
            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.TipoMovimentacao).Column("ITEMCAIXA_TIPO_MOVIMENTACAO").CustomType<EnumTipoMovimentacaoCaixa>();
            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.Valor).Column("ITEMCAIXA_VALOR");
            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.OrigemMovimentacaoCaixa).Column("ITEMCAIXA_ORIGEM_MOVIMENTACAO").CustomType<EnumOrigemMovimentacaoCaixa>();
            Map(itemMovimentacaoCaixa => itemMovimentacaoCaixa.NumeroDocumentoOrigem).Column("ITEMCAIXA_NUMERO_DOCUMENTO_ORIGEM");

            References(itemMovimentacaoCaixa => itemMovimentacaoCaixa.FormaPagamento).Column("ITEMCAIXA_FORMA_PAGAMENTO_ID").Not.LazyLoad();
            References(itemMovimentacaoCaixa => itemMovimentacaoCaixa.MovimentacaoCaixa).Column("ITEMCAIXA_MOVIMENTACAO_CAIXA");
            References(itemMovimentacaoCaixa => itemMovimentacaoCaixa.Parceiro).Column("ITEMCAIXA_PARCEIRO");
            References(itemMovimentacaoCaixa => itemMovimentacaoCaixa.CategoriaFinaceira).Column("ITEMCAIXA_CATEGORIA");
        }
    }
}
