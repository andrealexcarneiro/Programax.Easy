using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWAPagarAnual : ObjetoDeNegocioBase
    {
        public virtual decimal ValorParcelaPagar { get; set; }
        public virtual int MesVencimento { get; set; } 
    }
}
