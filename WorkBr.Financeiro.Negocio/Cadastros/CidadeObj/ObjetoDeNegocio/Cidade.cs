using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.EstadoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio
{
    [Serializable]
    public class Cidade : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string CodigoIbge { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
