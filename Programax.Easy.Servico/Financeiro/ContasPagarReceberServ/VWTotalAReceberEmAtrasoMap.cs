﻿using Programax.Infraestrutura.Servico.Mapeamentos;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class VWTotalAReceberEmAtrasoMap : MapeamentoBase<VWTotalAReceberEmAtraso>
    {
        public VWTotalAReceberEmAtrasoMap()
        {
            Table("VW_TOTAL_A_RECEBER_ATRASO");

            Id(x => x.Id).GeneratedBy.Native().UnsavedValue(0).Column("ID");
            Map(item => item.TotalAReceberEmAtraso).Column("TOTALARECEBERATRASO");
        }
    }
}
