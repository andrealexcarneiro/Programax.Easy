using System;
using Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio
{
    [Serializable]
    public class SubGrupo:ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }

        public virtual Grupo Grupo { get; set; }
    }
}
