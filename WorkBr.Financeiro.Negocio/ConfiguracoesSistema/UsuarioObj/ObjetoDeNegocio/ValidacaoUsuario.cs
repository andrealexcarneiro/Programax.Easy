using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.LicencaDeUsoObj.Repositorio;

namespace Programax.Easy.Negocio.ConfiguracoesSistema.UsuarioObj.ObjetoDeNegocio
{
    public class ValidacaoUsuario : ValidacaoBase<Usuario>
    {
        #region " CONSTANTES "

        private const int TAMANHO_MINIMO_SENHA = 5;

        #endregion

        #region " PROPRIEDADES "

        public bool AtualizarSenha { get; set; }

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoUsuario()
        {
            AtualizarSenha = true;
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsEntreInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsEntreInclusaoEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraLoginUnico()
        {
            RuleFor(usuario => usuario.Login)
                .Must((usuario, login) => NaoExisteUsuarioComEsteLogin(usuario))
                .WithMessage("Este login já está sendo utilizado por outro usuário.")
                .When(usuario => !string.IsNullOrEmpty(usuario.Login));
        }

        private void AssineRegraPessoaObrigatoria()
        {
            RuleFor(usuario => usuario.Id)
                .Must(id => id > 0)
                .WithMessage("Parceiro não informado.");
        }

        private void AssineRegraQuantidadeDeUsuariosMenorOuIgualAoContratado()
        {
            RuleFor(usuario => usuario.Id)
                .Must(id => QuantidadeDeUsuariosEstahIgualOuAbaixoDoPermitido())
                .WithMessage("Quantidade de usuários ativos está acima do permitido.")
                .When(usuario => usuario.Ativo);
        }

        public void AssineRegraLoginObrigatorio()
        {
            RuleFor(usuario => usuario.Login)
                .Must(login => !string.IsNullOrEmpty(login))
                .WithMessage("Login não informado.");
        }

        public void AssineRegraSenhaTamanhoMinimo()
        {
            RuleFor(usuario => usuario.Senha)
            .Must(senha => !string.IsNullOrEmpty(senha) && senha.Length >= TAMANHO_MINIMO_SENHA)
            .WithMessage("A senha deve conter no mínimo 5 caracteres.")
            .When(usuario => AtualizarSenha);
        }

        public void AssineRegraGrupoAcessoObrigatorio()
        {
            RuleFor(usuario => usuario.GrupoAcesso)
                .NotNull()
                .WithMessage("Grupo de acesso não informado.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsEntreInclusaoEAtualizacao()
        {
            AssineRegraLoginUnico();
            AssineRegraPessoaObrigatoria();
            AssineRegraLoginObrigatorio();
            AssineRegraSenhaTamanhoMinimo();
            AssineRegraGrupoAcessoObrigatorio();
            AssineRegraQuantidadeDeUsuariosMenorOuIgualAoContratado();
        }

        private bool NaoExisteUsuarioComEsteLogin(Usuario usuario)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioUsuario>();

            var usuarioBanco = repositorio.ConsulteUsuario(usuario.Login);

            if (usuarioBanco != null && usuarioBanco.Id != usuario.Id)
            {
                return false;
            }

            return true;
        }

        private bool QuantidadeDeUsuariosEstahIgualOuAbaixoDoPermitido()
        {
            var repositorioUsuario = FabricaDeRepositorios.Crie<IRepositorioUsuario>();
            var usuarioDaBase = repositorioUsuario.ConsulteSemSessao(ObjetoValidado.Id);

            if (usuarioDaBase != null && usuarioDaBase.Ativo)
            {
                return true;
            }

            int quantidadeDeUsuariosAtivos = repositorioUsuario.ConsulteQuantidadeDeUsuariosAtivos();

            var repositorioLicencaDeUso = FabricaDeRepositorios.Crie<IRepositorioLicencaDeUso>();
            var licencaDeUso = repositorioLicencaDeUso.ConsulteUltimaLicencaDeUso();

            if (licencaDeUso == null)
            {
                return false;
            }

            return licencaDeUso.QuantidadeUsuariosContratados > quantidadeDeUsuariosAtivos;
        }

        #endregion
    }
}
