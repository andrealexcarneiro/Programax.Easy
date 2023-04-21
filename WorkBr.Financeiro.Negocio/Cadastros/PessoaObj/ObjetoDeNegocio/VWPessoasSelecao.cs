using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    [Serializable]
    public class VWPessoasSelecao : ObjetoDeNegocioBase
    {
        public virtual int PessoaSelId { get; set; }
        public virtual string Razao { get; set; }
    }
}
