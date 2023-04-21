using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.GruposDreObj.ObjetoDeNegocio
{
    [Serializable]
    public class GrupoDre : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }
        public virtual DateTime DataCadastro { get; set; }
        public virtual string Ativo { get; set; }
    }
}
