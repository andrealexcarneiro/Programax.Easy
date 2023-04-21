using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio
{
    [Serializable]
    public class Usuario : ObjetoDeNegocioBase
    {
        public virtual string Login { get; set; }

        public virtual string Senha { get; set; }

        public virtual GrupoAcesso GrupoAcesso { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual bool Ativo { get; set; }
    }
}
