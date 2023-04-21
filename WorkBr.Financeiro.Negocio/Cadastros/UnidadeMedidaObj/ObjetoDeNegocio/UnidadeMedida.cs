using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.UnidadeMedidaObj.ObjetoDeNegocio
{
    [Serializable]
    public class UnidadeMedida : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string Abreviacao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
