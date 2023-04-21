using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWTotalAPagarEmAtraso: ObjetoDeNegocioBase
    {
        public virtual double TotalAPagarEmAtraso { get; set; }
    }
}
