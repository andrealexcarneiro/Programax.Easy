using System;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio
{
    [Serializable]
    public class Banco : ObjetoDeNegocioBase
    {
        public virtual string Codigo { get; set; }

        public virtual string Descricao { get; set; }

        public virtual string Site { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual string Status { get; set; }
    }
}
