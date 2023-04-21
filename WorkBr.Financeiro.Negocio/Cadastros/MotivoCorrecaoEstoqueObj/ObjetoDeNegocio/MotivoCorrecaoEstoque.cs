using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.MotivoCorrecaoEstoqueObj.ObjetoDeNegocio
{
    [Serializable]
    public class MotivoCorrecaoEstoque: ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
