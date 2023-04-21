using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.IcmsInterestadualObj.ObjetoDeNegocio
{
    [Serializable]
    public class IcmsInterestadual : ObjetoDeNegocioBase
    {
        public IcmsInterestadual()
        {
            ListaIcmsInterestadualEstado = new List<IcmsInterestadualEstado>();
        }

        public virtual Ncm Ncm { get; set; }

        public virtual IList<IcmsInterestadualEstado> ListaIcmsInterestadualEstado { get; set; }
    }
}
