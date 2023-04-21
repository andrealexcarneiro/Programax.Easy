using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class VWAPagarMensalMap : MapeamentoBase<VWAPagarMensal>
    {
        public VWAPagarMensalMap()
        {
            Table("VW_A_PAGAR_MENSAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(campo => campo.ValorParcelaPagar).Column("VALORPARCELAPAGAR");
            Map(campo => campo.Dia).Column("DIA");
        }
    }
}
