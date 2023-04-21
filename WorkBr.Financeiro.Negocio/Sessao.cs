using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.PermissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.GrupoAcessoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio
{
    public static class Sessao
    {
        public static Pessoa PessoaLogada { get; set; }

        public static List<Permissao> ListaDePermissoes { get; set; }

        public static GrupoAcesso GrupoAcesso { get; set; }
    }
}
