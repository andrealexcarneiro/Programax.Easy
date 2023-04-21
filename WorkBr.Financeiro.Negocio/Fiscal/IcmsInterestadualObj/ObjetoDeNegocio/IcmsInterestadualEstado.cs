using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio
{
    [Serializable]
    public class IcmsInterestadualEstado:ObjetoDeNegocioBase
    {
        public virtual string UF { get; set; }

        public virtual double FCP { get; set; }

        public virtual double AliquotaInterna { get; set; }

        public virtual IcmsInterestadual IcmsInterestadual { get; set; }
    }
}
