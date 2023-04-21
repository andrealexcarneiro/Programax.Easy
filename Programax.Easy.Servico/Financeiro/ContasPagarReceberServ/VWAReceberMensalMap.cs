using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class VWAReceberMensalMap : MapeamentoBase<VWAReceberMensal>
    {
        public VWAReceberMensalMap()
        {
            Table("VW_A_RECEBER_MENSAL");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(campo => campo.ValorParcelaReceber).Column("VALORPARCELARECEBER");
            Map(campo => campo.Dia).Column("DIA");
        }
    }
}
