using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.ObjetoDeNegocio
{
    public class LicencaDeUso:ObjetoDeNegocioBase
    {
        public virtual int IdDatabase { get; set; }

        public virtual string Contrato { get; set; }

        public virtual DateTime LiberadoAte { get; set; }

        public virtual int QuantidadeUsuariosContratados { get; set; }

        public virtual string ChaveLiberacao { get; set; }
    }
}
