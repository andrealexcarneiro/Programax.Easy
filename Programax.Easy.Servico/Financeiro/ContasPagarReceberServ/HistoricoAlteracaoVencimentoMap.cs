using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Servico.Mapeamentos;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class HistoricoAlteracaoVencimentoMap : MapeamentoBase<HistoricoAlteracaoVencimento>
    {
        public HistoricoAlteracaoVencimentoMap()
        {
            Table("CONTASPAGARRECEBERVENCIMENTOS");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("CPRV_ID");

            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.DataAlteracao).Column("CPRV_DATA_ALTERACAO");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.DataVencimento).Column("CPRV_DATA_VENCIMENTO");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.Desconto).Column("CPRV_DESCONTO");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.Juros).Column("CPRV_JUROS");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.Multa).Column("CPRV_MULTA");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.NumeroAlteracao).Column("CPRV_NUMERO_ALTERACAO");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.Observacoes).Column("CPRV_OBSERVACOES");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.Valor).Column("CPRV_VALOR");
            Map(historicoAlteracaoVencimento => historicoAlteracaoVencimento.ValorTotal).Column("CPRV_VALOR_TOTAL");

            References(historicoAlteracaoVencimento => historicoAlteracaoVencimento.ContaPagarReceber).Column("CPRV_CONTA_PAGAR_RECEBER");

            References(historicoAlteracaoVencimento => historicoAlteracaoVencimento.Usuario).Column("CPRV_USUARIO").Not.LazyLoad();
        }
    }
}
