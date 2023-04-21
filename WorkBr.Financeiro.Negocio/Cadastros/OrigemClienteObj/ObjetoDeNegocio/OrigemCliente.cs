using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio
{
    [Serializable]
    public class OrigemCliente : ObjetoDeNegocioBase
    {
        public virtual string Descricao { get; set; }

        public virtual string Status { get; set; }

        public virtual DateTime DataCadastro { get; set; }
    }
}
