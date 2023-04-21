using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.FormaPagamentoServ
{
    public class CondicaoDePagamentoDaFormaMap : MapeamentoBase<CondicaoDePagamentoDaForma>
    {
        public CondicaoDePagamentoDaFormaMap()
        {
            Table("CONDICOESDEPAGAMENTODAFORMA");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CONDPAG_ID");

            References(condicao => condicao.CondicaoPagamento).Column("CONDPAG_CONDICAO_PAGAMENTO_ID").Not.LazyLoad().Fetch.Join();
            References(condicao => condicao.FormaPagamento).Column("CONDPAG_FORMA_PAGAMENTO_ID");
        }
    }
}
