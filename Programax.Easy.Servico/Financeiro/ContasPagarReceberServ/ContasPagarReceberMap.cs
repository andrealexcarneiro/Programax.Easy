using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class ContasPagarReceberMap : MapeamentoBase<ContaPagarReceber>
    {
        public ContasPagarReceberMap()
        {
            Table("CONTASPAGARRECEBER");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CPR_ID");

            Map(condicao => condicao.Historico).Column("CPR_HISTORICO");
            Map(condicao => condicao.Status).Column("CPR_STATUS").CustomType<EnumStatusContaPagarReceber>();
            Map(condicao => condicao.TipoOperacao).Column("CPR_TIPO_OPERACAO").CustomType<EnumTipoOperacaoContasPagarReceber>();

            Map(condicao => condicao.DataEmissao).Column("CPR_DATA_EMISSAO");
            Map(condicao => condicao.DataVencimento).Column("CPR_DATA_VENCIMENTO");
            Map(condicao => condicao.DataPagamento).Column("CPR_DATA_PAGAMENTO");

            Map(condicao => condicao.ValorParcela).Column("CPR_VALOR_PARCELA");
            Map(condicao => condicao.Multa).Column("CPR_MULTA");
            Map(condicao => condicao.Juros).Column("CPR_JUROS");
            Map(condicao => condicao.Desconto).Column("CPR_DESCONTO");

            Map(condicao => condicao.ValorPago).Column("CPR_VALOR_PAGO");

            Map(condicao => condicao.NumeroDocumento).Column("CPR_NUMERO_DOCUMENTO");
            Map(condicao => condicao.OrigemDocumento).Column("CPR_ORIGEM_DOCUMENTO").CustomType<EnumOrigemDocumento>();
            Map(condicao => condicao.ChequeId).Column("CPR_CHEQUE_ID");

            Map(condicao => condicao.MultaEhPercentual).Column("CPR_MULTA_EH_PERCENTUAL");
            Map(condicao => condicao.JurosEhPercentual).Column("CPR_JUROS_EH_PERCENTUAL");
            Map(condicao => condicao.ehCalculoDeJurosMultaManual).Column("CPR_CALCULO_JUROS_MULTAS_MANUAL");
            Map(condicao => condicao.CondicaoPgtoId).Column("CPR_ID_CONDICAO_PGTO");

            References(condicao => condicao.FormaPagamento).Column("CPR_FORPAG_ID").Not.LazyLoad().Fetch.Join();
            References(condicao => condicao.Pessoa).Column("CPR_PES_ID").Not.LazyLoad().Fetch.Join();
            References(condicao => condicao.Usuario).Column("CPR_PES_USUARIO_ID");
            References(condicao => condicao.PlanoDeContas).Column("CPR_PLC_ID").Not.LazyLoad();//.Not.LazyLoad().Fetch.Join(); --> Correção mapeamento ao salvar no contas a pagar receber com plano de contas

            References(condicao => condicao.BancoParaMovimento).Column("CPR_ID_BANCO_MOV");
            References(condicao => condicao.CategoriaFinanceira).Column("CPR_ID_CATEGORIA");
            References(condicao => condicao.OperadorasCartao).Column("CPR_ID_OPERADORASCARTAO");

            HasMany(condicao => condicao.ListaContasPagarReceberParcial).KeyColumn("CPRPARCIAL_CONTAS_PAGAR_RECEBER_ID").AsBag().Table("CONTASPAGARRECEBERPARCIAL").Not.LazyLoad();
            HasMany(condicao => condicao.ListaHistoricoAlteracoesVencimento).KeyColumn("CPRV_CONTA_PAGAR_RECEBER").Cascade.AllDeleteOrphan().Inverse().AsBag().Table("CONTASPAGARRECEBERVENCIMENTOS").Not.LazyLoad();
      
        }
    }
}
