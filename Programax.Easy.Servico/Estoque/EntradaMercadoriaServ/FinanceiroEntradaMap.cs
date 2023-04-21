using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Estoque.EntradaMercadoriaServ
{
    public class FinanceiroEntradaMap : MapeamentoBase<FinanceiroEntrada>
    {
        public FinanceiroEntradaMap()
        {
            Table("ENTRADASFINANCEIRO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ENTFINAN_ID");

            Map(financeiro => financeiro.DataVencimento).Column("ENTFINAN_DATA_VENCIMENTO");
            Map(financeiro => financeiro.NumeroDocumento).Column("ENTFINAN_NR_DOCUMENTO");
            Map(financeiro => financeiro.Parcela).Column("ENTFINAN_PARCELA");
            Map(financeiro => financeiro.ValorDuplicata).Column("ENTFINAN_VALOR_DUPLICATA");

            References(financeiro => financeiro.EntradaMercadoria).Column("ENTFINAN_ENTRADA_ID");
            References(financeiro => financeiro.FormaPagamento).Column("ENTFINAN_FORMA_PAGAMENTO_ID");
            References(financeiro => financeiro.ContaPagar).Column("ENTFINAN_CONTA_PAGAR_RECEBER_ID");
        }
    }
}
