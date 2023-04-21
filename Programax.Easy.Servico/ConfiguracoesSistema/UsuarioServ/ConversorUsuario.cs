using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Servico.CopiadoresDeObjetosParaPerisistencia;

namespace Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ
{
    public class ConversorUsuario : ConversorDeObjetoBasico<Usuario>, IConversorDeObjeto<Usuario>
    {
        public bool AtualizarSenha { get; set; }

        public ConversorUsuario()
        {
            AtualizarSenha = true;
        }

        public Usuario CopieObjetoParaPersistencia(Usuario objetoDeNegocio)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioUsuario>();

            var usuarioDaBase = repositorio.Consulte(objetoDeNegocio.Id) ?? new Usuario();

            string senhaDaBase = usuarioDaBase.Senha;

            CopieTodasAsPropriedades(objetoDeNegocio, usuarioDaBase);

            if (!AtualizarSenha)
            {
                usuarioDaBase.Senha = senhaDaBase;
            }

            return usuarioDaBase;
        }
    }
}
