using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Grupo : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
