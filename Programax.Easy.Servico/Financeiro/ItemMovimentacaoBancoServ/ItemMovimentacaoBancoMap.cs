using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ
{
    public class ItemMovimentacaoBancoMap : MapeamentoBase<ItemMovimentacaoBanco>
    {
        public ItemMovimentacaoBancoMap()
        {
            Table("MOVIMENTACOESBANCOSITENS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ITEMBANCO_ID");

            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.DataHoraLancamento).Column("ITEMBANCO_DATA_HORA");
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.DataAtualizacao).Column("ITEMBANCO_DATA_ATUALIZACAO");
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.EstahEstornado).Column("ITEMBANCO_ESTAH_ESTORNADO");
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.DescricaoDaMovimentacao).Column("ITEMBANCO_DESCRICAO_MOVIMENTACAO");
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.TipoMovimentacao).Column("ITEMBANCO_TIPO_MOVIMENTACAO").CustomType<EnumTipoMovimentacaoBanco>();
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.Valor).Column("ITEMBANCO_VALOR");
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.OrigemMovimentacaoBanco).Column("ITEMBANCO_ORIGEM_MOVIMENTACAO").CustomType<EnumOrigemMovimentacaoBanco>();
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.NumeroDocumentoOrigem).Column("ITEMBANCO_NUMERO_DOCUMENTO_ORIGEM");
            Map(ItemMovimentacaoBanco => ItemMovimentacaoBanco.ConciliacaoImportacaoId).Column("ITEMBANCO_CONCILIACAO_IMPORTACAO_ID");

            References(ItemMovimentacaoBanco => ItemMovimentacaoBanco.Categoria).Column("ITEMBANCO_CATEGORIA_ID").Not.LazyLoad();
            References(ItemMovimentacaoBanco => ItemMovimentacaoBanco.MovimentacaoBanco).Column("ITEMBANCO_MOVIMENTACAO_BANCO").Not.LazyLoad();
            References(ItemMovimentacaoBanco => ItemMovimentacaoBanco.Parceiro).Column("ITEMBANCO_PARCEIRO").Not.LazyLoad();
            References(ItemMovimentacaoBanco => ItemMovimentacaoBanco.UsuarioAtualizacao).Column("ITEMBANCO_USUARIO_ATUALIZACAO_ID");
            References(ItemMovimentacaoBanco => ItemMovimentacaoBanco.ContaPagarReceber).Column("ITEMBANCO_ID_CONTAS_PAGARRECEBER").Not.LazyLoad();
        }
    }
}
