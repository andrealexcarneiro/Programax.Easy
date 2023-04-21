using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Estoque.Enumeradores;

namespace Programax.Easy.Servico.Estoque.EntradaMercadoriaServ
{
    public class EntradaMercadoriaMap : MapeamentoBase<EntradaMercadoria>
    {
        public EntradaMercadoriaMap()
        {
            Table("ENTRADAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ENTRADA_ID");

            Map(entrada => entrada.ModeloDocumentoFiscal).Column("ENTRADA_MODELO_DOCUMENTO_FISCAL").CustomType<EnumModeloDocumentoFiscal>();
            Map(entrada => entrada.TipoFrete).Column("ENTRADA_TIPO_FRETE").CustomType<EnumTipoFrete>();
            Map(entrada => entrada.StatusEntrada).Column("ENTRADA_STATUS").CustomType<EnumStatusEntrada>();

            Map(entrada => entrada.NaturezaOperacaoNota).Column("ENTRADA_NATUREZA_OPERACAO_NOTA");

            Map(entrada => entrada.CondicaoPagamentoEntrada).Column("ENTRADA_CONDICAO_PAGAMENTO").CustomType<EnumCondicaoPagamentoNota>();
            Map(entrada => entrada.NumeroConhecimentoFrete).Column("ENTRADA_NUMERO_CONHECIMENTO_FRETE");
            Map(entrada => entrada.ValorFrete).Column("ENTRADA_VALOR_FRETE");
            Map(entrada => entrada.BaseIcms).Column("ENTRADA_BASE_ICMS");
            Map(entrada => entrada.AliquotaIcms).Column("ENTRADA_ALIQUOTA_ICMS");
            Map(entrada => entrada.ValorIcms).Column("ENTRADA_VALOR_ICMS");
            Map(entrada => entrada.ValorIcmsDesoneracao).Column("ENTRADA_VALOR_ICMS_DESONERACAO");
            Map(entrada => entrada.BaseIcmsSt).Column("ENTRADA_BASE_ICMS_ST");
            Map(entrada => entrada.ValorIcmsSt).Column("ENTRADA_VALOR_ICMS_ST");
            Map(entrada => entrada.ValorIpi).Column("ENTRADA_VALOR_IPI");
            Map(entrada => entrada.ValorDesconto).Column("ENTRADA_VALOR_DESCONTO");
            Map(entrada => entrada.ValorOutrasDespesas).Column("ENTRADA_OUTRAS_DESPESAS");
            Map(entrada => entrada.ValorTotalNota).Column("ENTRADA_VALOR_TOTAL_NOTA");

            Map(entrada => entrada.NumeroNota).Column("ENTRADA_NUMERO_NOTA");
            Map(entrada => entrada.ChaveDeAcesso).Column("ENTRADA_CHAVE_ACESSO");
            Map(entrada => entrada.Serie).Column("ENTRADA_SERIE_NOTA");
            Map(entrada => entrada.DataEmissao).Column("ENTRADA_DATA_EMISSAO");
            Map(entrada => entrada.DataMovimentacao).Column("ENTRADA_DATA_MOVIMENTACAO");

            Map(entrada => entrada.DataCadastro).Column("ENTRADA_DATA_CADASTRO");

            Map(entrada => entrada.Observacoes).Column("ENTRADA_OBSERVACOES");

            Map(entrada => entrada.AtualizaPrecoCusto).Column("ENTRADA_ATUALIZA_PRECO_CUSTO");

            Map(entrada => entrada.Tipo).Column("ENTRADA_TIPO");

            References(entrada => entrada.NaturezaOperacao).Column("ENTRADA_NATUREZA_OPERACAO_ID");
            References(entrada => entrada.AjusteFiscal).Column("ENTRADA_AJUSFISC_ID");
            References(entrada => entrada.Transportadora).Column("ENTRADA_TRANSPORTADORA_ID");
            References(entrada => entrada.UsuarioCadastro).Column("ENTRADA_USUARIO_ID");
            References(entrada => entrada.Fornecedor).Column("ENTRADA_FORNECEDOR_ID");

            HasMany(entrada => entrada.ListaDeItens).KeyColumn("ITEMENT_ENTRADA_ID").Cascade.AllDeleteOrphan().AsBag();
            HasMany(entrada => entrada.ListaFinanceiroEntrada).KeyColumn("ENTFINAN_ENTRADA_ID").Cascade.AllDeleteOrphan().AsBag();
        }
    }
}