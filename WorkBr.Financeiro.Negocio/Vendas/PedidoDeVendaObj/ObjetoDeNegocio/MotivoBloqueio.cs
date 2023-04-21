using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    [Serializable]
    public class MotivoBloqueio
    {
        public virtual bool DescontoAcima { get; set; }

        public virtual bool SemSaldoCredito { get; set; }

        public virtual bool ItemSemEstoque { get; set; }

        public virtual bool ClienteBloqueado { get; set; }

        public virtual bool FinanceiroEmAberto { get; set; }
    }
}
