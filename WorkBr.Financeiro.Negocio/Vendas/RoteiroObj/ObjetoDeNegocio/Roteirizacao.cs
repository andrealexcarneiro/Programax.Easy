using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio
{
    [Serializable]
    public class Roteirizacao : ObjetoDeNegocioBase
    {
        public virtual DateTime DataCriacao { get; set; }

        public virtual Pessoa PessoaFuncionario { get; set; }

        public virtual DateTime? DataConclusao { get; set; }

        public virtual Pessoa Usuario { get; set; }

        public virtual EnumStatusRoteiro Status { get; set; }   
    }
}
