using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio
{
    [Serializable]
    public class Cor: ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
