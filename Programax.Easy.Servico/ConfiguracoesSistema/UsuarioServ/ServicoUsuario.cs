using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.Repositorio;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.PermissaoServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio;

namespace Programax.Easy.Servico.ConfiguracoesSistema.UsuarioServ
{
    [Funcionalidade(EnumFuncionalidade.USUARIO)]
    public class ServicoUsuario : ServicoAkilSmallBusiness<Usuario, ValidacaoUsuario, ConversorUsuario>
    {
        IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario()
        {
            RetorneRepositorio();
        }

        public override IRepositorioBase<Usuario> RetorneRepositorio()
        {
            if (_repositorioUsuario == null)
            {
                _repositorioUsuario = FabricaDeRepositorios.Crie<IRepositorioUsuario>();
            }

            return _repositorioUsuario;
        }

        public Usuario ConsulteUsuario(string login)
        {
            return _repositorioUsuario.ConsulteUsuario(login);
        }

        public bool AutenticarUsuario(string login, string senha)
        {
            var senhaMd5 = senha.GeraHashMD5();
            // para conectar somente o usuario- Andre 21/01/2022
           // var usuario = _repositorioUsuario.ConsulteUsuario(login);

            var usuario = _repositorioUsuario.ConsulteUsuario(login, senhaMd5);

            if (usuario != null)
            {
                usuario.GrupoAcesso.CarregueLazyLoad();

                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var pessoa = servicoPessoa.Consulte(usuario.Id);
                Sessao.PessoaLogada = pessoa;

                var grupoAcesso = usuario.GrupoAcesso != null ? usuario.GrupoAcesso.Id : 0;

                ServicoPermissao servicoPermissao = new ServicoPermissao();
                Sessao.ListaDePermissoes = servicoPermissao.ConsulteLista(grupoAcesso);
                Sessao.GrupoAcesso = usuario.GrupoAcesso;

                return true;
            }

            return false;
        }

        public int Cadastre(Usuario objetoDeNegocio, bool atualizarSenha)
        {
            var validacaoObjetoDeNegocio = new ValidacaoUsuario();

            validacaoObjetoDeNegocio.AtualizarSenha = atualizarSenha;

            validacaoObjetoDeNegocio.ValideInclusao();

            validacaoObjetoDeNegocio.Valide(objetoDeNegocio).AssegureSucesso();

            var conversorDeObjeto = new ConversorUsuario();

            conversorDeObjeto.AtualizarSenha = atualizarSenha;

            var objetoDeNegocioParaPersistencia = conversorDeObjeto.CopieObjetoParaPersistencia(objetoDeNegocio);

            _repositorioUsuario.Cadastre(objetoDeNegocioParaPersistencia, atualizarSenha);

            return objetoDeNegocio.Id;
        }

        public void Atualize(Usuario objetoDeNegocio, bool atualizarSenha)
        {
            var validacaoObjetoDeNegocio = new ValidacaoUsuario();

            validacaoObjetoDeNegocio.AtualizarSenha = atualizarSenha;

            validacaoObjetoDeNegocio.ValideAtualizacao();

            validacaoObjetoDeNegocio.Valide(objetoDeNegocio).AssegureSucesso();

            var conversorDeObjeto = new ConversorUsuario();

            conversorDeObjeto.AtualizarSenha = atualizarSenha;

            var objetoDeNegocioParaPersistencia = conversorDeObjeto.CopieObjetoParaPersistencia(objetoDeNegocio);

            _repositorioUsuario.Atualize(objetoDeNegocioParaPersistencia, atualizarSenha);
        }

        public int ConsulteQuantidadeDeUsuariosAtivos()
        {
            return _repositorioUsuario.ConsulteQuantidadeDeUsuariosAtivos();
        }
    }
}
