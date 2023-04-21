using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ
{
    public class ContaPagarReceberPagamentoMap : MapeamentoBase<ContaPagarReceberPagamento>
    {
        public ContaPagarReceberPagamentoMap()
        {
            Table("CONTASPAGARRECEBERPARCIAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CPRPARCIAL_ID");

            Map(condicao => condicao.DataPagamento).Column("CPRPARCIAL_DATA_PAGAMENTO");
            Map(condicao => condicao.Valor).Column("CPRPARCIAL_VALOR");
            Map(condicao => condicao.Observacoes).Column("CPRPARCIAL_OBSERVACOES");
            Map(condicao => condicao.EstahEstornado).Column("CPRPARCIAL_ESTAH_ESTORNADO");

            References(condicao => condicao.FormaPagamento).Column("CPRPARCIAL_FORMA_PAGAMENTO_ID");
            References(condicao => condicao.Responsavel).Column("CPRPARCIAL_RESPONSAVEL_ID").Not.LazyLoad();
            References(condicao => condicao.ContaPagarReceber).Column("CPRPARCIAL_CONTAS_PAGAR_RECEBER_ID");
        }
    }
}
