using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programax.Easy.Negocio.Financeiro.CategoriaDreObj.ObjetoDeNeocio
{
    [Serializable]
    public class CategoriaDre  : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }

        public virtual EnumTipoCategoria TipoCategoria { get; set; }

        public virtual SubGrupoCategoria SubGrupoCategoria { get; set; }
    }
}
