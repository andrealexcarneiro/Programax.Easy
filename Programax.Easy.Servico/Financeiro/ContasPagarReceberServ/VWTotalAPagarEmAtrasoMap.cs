using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class VWTotalAPagarEmAtrasoMap : MapeamentoBase<VWTotalAPagarEmAtraso>
    {
        public VWTotalAPagarEmAtrasoMap()
        {
            Table("VW_TOTAL_A_PAGAR_ATRASO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(item => item.TotalAPagarEmAtraso).Column("TOTALAPAGARATRASO");
        }
    }
}
