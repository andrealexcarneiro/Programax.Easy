using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class VWAPagarTodosDiasDoAnoMap : MapeamentoBase<VWAPagarTodosDiasDoAno>
    {
        public VWAPagarTodosDiasDoAnoMap()
        {
            Table("VW_A_PAGAR_TODOS_DIAS_ANO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(campo => campo.ValorParcelaPagar).Column("VALORPARCELA");
            Map(campo => campo.Vencimento).Column("DATAVENCIMENTO");
        }
    }
}
