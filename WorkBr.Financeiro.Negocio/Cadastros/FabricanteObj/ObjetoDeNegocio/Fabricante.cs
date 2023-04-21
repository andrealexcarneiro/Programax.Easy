using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.FabricanteObj.ObjetoDeNegocio
{
    [Serializable]
    public class Fabricante : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Ativo { get; set; }
    }
}
