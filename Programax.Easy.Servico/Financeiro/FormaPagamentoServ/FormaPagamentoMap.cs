using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Fiscal.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.FormaPagamentoServ
{
    public class FormaPagamentoMap : MapeamentoBase<FormaPagamento>
    {
        public FormaPagamentoMap()
        {
            Table("PAGAMENTOFORMA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("FORPAG_ID");

            Map(condicao => condicao.Descricao).Column("FORPAG_DESCRICAO");
            Map(condicao => condicao.Status).Column("FORPAG_STATUS");
            Map(condicao => condicao.DisponivelParaPdv).Column("FORPAG_ESTAH_DISPONIVEL_PDV");
            Map(condicao => condicao.DisponivelParaContasPagar).Column("FORPAG_ESTAH_DISPONIVEL_CONTAS_PAGAR");
            Map(condicao => condicao.DisponivelParaContasReceber).Column("FORPAG_ESTAH_DISPONIVEL_CONTAS_RECEBER");
            Map(condicao => condicao.DisponivelParaPedidoVenda).Column("FORPAG_ESTAH_DISPONIVEL_PEDIDO_VENDA");
            Map(condicao => condicao.DataCadastro).Column("FORPAG_DATA_CADASTRO");
            Map(condicao => condicao.TipoFormaPagamento).Column("FORPAG_TIPO_FORMA_PAGAMENTO").CustomType<EnumTipoFormaPagamento>().Not.LazyLoad();
            Map(condicao => condicao.FormaPagamentoNfce).Column("FORPAG_FORMA_PAGAMENTO_NFCE").CustomType<EnumFormaPagamentoNfce>();

            HasMany(condicao => condicao.ListaCondicoesPagamento).KeyColumn("CONDPAG_FORMA_PAGAMENTO_ID").Cascade.AllDeleteOrphan().Inverse().AsBag();
        }
    }
}
