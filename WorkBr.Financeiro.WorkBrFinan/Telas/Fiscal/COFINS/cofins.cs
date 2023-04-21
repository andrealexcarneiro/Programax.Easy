using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.View.Telas.Fiscal.COFINS
{
    class cofins
    {
        public enum CSTCOFINSII
        {
          
            [Description("01")]
            cofins01,

            [Description("RESERVADO")]
            RESERVADO,

            [Description("PAGO")]
            FATURADO,

            [Description("CANCELADO")]
            CANCELADO,

            [Description("ORÇAMENTO")]
            ORCAMENTO,

            [Description("EM LIBERAÇÃO")]
            EMLIBERACAO,

            [Description("RECUSADO")]
            RECUSADO,

            [Description("EMITIDO NFE")]
            EMITIDONFE
        }
    }
}
