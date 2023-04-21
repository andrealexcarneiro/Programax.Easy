using Programax.Easy.Negocio.Movimentacao.AjusteFiscalObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Movimentacao.AjusteFiscalServ
{
    public class AjusteFiscalMap : MapeamentoBase<AjusteFiscal>
    {
        public AjusteFiscalMap()
        {
            Not.LazyLoad();

            Table("AJUSTESFISCAIS");

            Id(x => x.Id).Column("AJUFISC_ID").GeneratedBy.Native();

            Map(ajuste => ajuste.Codigo).Column("AJUFISC_CODIGO");
            Map(ajuste => ajuste.Descricao).Column("AJUFISC_DESCRICAO");
            Map(ajuste => ajuste.DataInicio).Column("AJUFISC_DATA_INICIO");
            Map(ajuste => ajuste.DataFim).Column("AJUFISC_DATA_FIM");
        }

    }
}
