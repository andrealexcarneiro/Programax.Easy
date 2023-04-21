using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ
{
    public class ParcelaCondicaoPagamentoMap : MapeamentoBase<ParcelaCondicaoPagamento>
    {
        public ParcelaCondicaoPagamentoMap()
        {
            Table("PAGAMENTOCONDICAOPARCELAS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("PARC_ID");

            Map(parcela => parcela.NumeroParcela).Column("PARC_NUMERO_PARCELA");
            Map(parcela => parcela.Dias).Column("PARC_DIAS");
            Map(parcela => parcela.PercentualRateio).Column("PARC_PERCENTUAL_RATEIO");
            Map(parcela => parcela.PercentualAcrescimo).Column("PARC_PERCENTUAL_ACRESCIMO");

            References(parcela => parcela.CondicaoPagamento).Column("PARC_COPAG_ID");
        }
    }
}
