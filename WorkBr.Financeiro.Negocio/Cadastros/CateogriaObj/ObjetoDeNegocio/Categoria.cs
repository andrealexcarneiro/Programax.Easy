using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.CateogriaObj.ObjetoDeNegocio
{
    [Serializable]
    public class Categoria : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }
    }
}
