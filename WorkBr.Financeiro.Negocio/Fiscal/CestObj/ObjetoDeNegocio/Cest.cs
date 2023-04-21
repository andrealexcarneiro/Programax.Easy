using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio
{
    [Serializable]
    public class Cest : ObjetoDeNegocioBase
    {
        public virtual string CodigoCest { get; set;}

        public virtual string DescricaoCest { get; set; }

        public virtual string CodigoNcm { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }

}

