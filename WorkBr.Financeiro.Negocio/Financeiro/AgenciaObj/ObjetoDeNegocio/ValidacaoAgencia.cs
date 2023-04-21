using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using System.Collections;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.AgenciaObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.AgenciaObj.ObjetoDeNegocio
{
    public class ValidacaoAgencia : ValidacaoBase<Agencia>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            ValidacoesComunsAoCadastroEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            ValidacoesComunsAoCadastroEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraBancoObrigatorio()
        {
            RuleFor(agencia => agencia.Banco)
                .NotNull()
                .WithMessage("Banco não informado.");
        }

        private void AssineRegraNomeAgenciaObrigatorio()
        {
            RuleFor(agencia => agencia.NomeAgencia)
                .Must(nomeAgencia => !string.IsNullOrWhiteSpace(nomeAgencia))
                .WithMessage("Nome agência não informada.");
        }

        private void AssineRegraNumeroAgenciaObrigatorio()
        {
            RuleFor(agencia => agencia.NumeroAgencia)
                .Must(numeroAgencia => !string.IsNullOrWhiteSpace(numeroAgencia))
                .WithMessage("Número agência não informada.");
        }

        private void AssineRegraAgenciaNaoExistente()
        {
            RuleFor(agencia => agencia.NumeroAgencia)
                .Must(numeroAgencia => NaoExisteAgenciaComEsteNumeroEBanco())
                .WithMessage("Já existe uma agência com este número e banco cadastrados.")
                .When(agencia => agencia.Banco != null && !string.IsNullOrWhiteSpace(agencia.NumeroAgencia));
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ValidacoesComunsAoCadastroEAtualizacao()
        {
            AssineRegraBancoObrigatorio();
            AssineRegraNomeAgenciaObrigatorio();
            AssineRegraNumeroAgenciaObrigatorio();
            AssineRegraAgenciaNaoExistente();
        }

        private bool NaoExisteAgenciaComEsteNumeroEBanco()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioAgencia>();

            var agenciaBanco = repositorio.Consulte(ObjetoValidado.Banco.Id, ObjetoValidado.NumeroAgencia);

            return agenciaBanco == null || agenciaBanco.Id == ObjetoValidado.Id;
        }

        #endregion
    }
}
