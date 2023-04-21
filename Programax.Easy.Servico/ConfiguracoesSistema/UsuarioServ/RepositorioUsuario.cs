using NHibernate;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Criterion;

namespace Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ
{
    public class RepositorioUsuario: RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(ISession sesao)
            : base(sesao)
        {
        }

        public Usuario ConsulteUsuario(string login)
        {
            return _session.QueryOver<Usuario>().Where(usuario => usuario.Login == login).SingleOrDefault();
        }

        public Usuario ConsulteUsuario(string login, string senha)
        {
            
            return _session.QueryOver<Usuario>().Where(usuario => usuario.Login == login && usuario.Senha == senha && usuario.Ativo).SingleOrDefault();
        }

        public void Cadastre(Usuario objeto, bool atualizarSenha)
        {
            if (atualizarSenha)
            {
                Cadastre(objeto);
            }
            else
            {
                base.Cadastre(objeto);
            }
        }

        public override void Cadastre(Usuario objeto)
        {
            objeto.Senha = objeto.Senha.GeraHashMD5();

            base.Cadastre(objeto);
        }

        public void Atualize(Usuario objeto, bool atualizarSenha)
        {
            if (atualizarSenha)
            {
                Atualize(objeto);
            }
            else
            {
                base.Atualize(objeto);
            }
        }

        public override void Atualize(Usuario objeto)
        {
            objeto.Senha = objeto.Senha.GeraHashMD5();

            base.Atualize(objeto);
        }

        public int ConsulteQuantidadeDeUsuariosAtivos()
        {
            return _session.QueryOver<Usuario>().Where(usuario => usuario.Ativo == true)
                                                                    .Select(Projections.RowCount())
                                                                    .FutureValue<int>()
                                                                    .Value;
        }
    }
}
