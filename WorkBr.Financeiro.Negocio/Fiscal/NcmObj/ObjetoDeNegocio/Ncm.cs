using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.NcmObj.ObjetoDeNegocio
{
    [Serializable]
    public class Ncm : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string CodigoNcm { get; set; }

        public virtual string Cest { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual double ImpostoIbptFederalNacional { get; set; }

        public virtual double ImpostoIbptFederalImportados { get; set; }

        public virtual double ImpostoIbptEstadual { get; set; }

        public virtual double ImpostoIbptMunicipal { get; set; }

        public virtual DateTime DataValidadeIbpt { get; set; }

        public virtual string ChaveTabelaIbpt { get; set; }
    }
}
