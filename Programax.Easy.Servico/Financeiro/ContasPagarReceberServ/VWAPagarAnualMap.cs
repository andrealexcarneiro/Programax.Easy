using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class VWAPagarAnualMap : MapeamentoBase<VWAPagarAnual>
    {
        public VWAPagarAnualMap()
        {
            Table("VW_A_PAGAR_ANUAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(campo => campo.ValorParcelaPagar).Column("VALORPARCELA");
            Map(campo => campo.MesVencimento).Column("MESVENCIMENTO");
        }
    }
}
