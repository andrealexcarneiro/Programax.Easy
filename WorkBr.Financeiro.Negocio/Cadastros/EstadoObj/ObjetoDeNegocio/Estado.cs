using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Estado : ObjetoDeNegocioBase
    {
        private new string Id { get; set; }

        public virtual string UF { get; set; }

        public virtual string Nome { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual int CodigoEstado { get; set; }
    }
}
