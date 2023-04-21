using FluentValidation;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio
{
    public class ValidacaoCaixa : ValidacaoBase<Caixa>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraFuncionarioObrigatorio()
        {
            RuleFor(caixa => caixa.Funcionario)
                .NotNull()
                .WithMessage("Funcionário não foi informado.");
        }

        private void AssineRegraNomeCaixaObrigatorio()
        {
            RuleFor(caixa => caixa.NomeCaixa)
                .Must(nomeCaixa => !string.IsNullOrEmpty(nomeCaixa))
                .WithMessage("Nome Caixa não foi informado.");
        }

        private void AssineRegraPerfilCaixaObrigatorio()
        {
            RuleFor(caixa => caixa.PerfilCaixa)
                .NotNull()
                .WithMessage("Perfil Caixa não informado.");
        }

        private void AssineRegraNomeUnico()
        {
            RuleFor(caixa => caixa.NomeCaixa)
                .Must(nomeCaixa => NomeEhUnico())
                .WithMessage("Nome Caixa já existente.")
                .When(caixa => !string.IsNullOrEmpty(caixa.NomeCaixa));
        }

        private void AssineRegraFuncionarioUnico()
        {
            RuleFor(caixa => caixa.NomeCaixa)
                .Must(nomeCaixa => FuncionarioEhUnico())
                .WithMessage("Este funcionário já contém um caixa.")
                .When(caixa => caixa.Funcionario != null);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraFuncionarioObrigatorio();
            AssineRegraNomeCaixaObrigatorio();
            AssineRegraPerfilCaixaObrigatorio();
            AssineRegraNomeUnico();
            AssineRegraFuncionarioUnico();
        }

        private bool NomeEhUnico()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCaixa>();

            var caixa = repositorio.ConsultePeloNomeCaixa(ObjetoValidado.NomeCaixa);

            return caixa == null || caixa.Id == ObjetoValidado.Id;
        }

        private bool FuncionarioEhUnico()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCaixa>();

            var caixa = repositorio.ConsultePeloFuncionario(ObjetoValidado.Funcionario);

            return caixa == null || caixa.Id == ObjetoValidado.Id;
        }

        #endregion
    }
}
