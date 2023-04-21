using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio
{
    public class ValidacaoPlanoContasDre : ValidacaoBase<PlanoContaDre>
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

        private void AssineRegraNumeroPlanoDeContasDreObrigatorio()
        {
            RuleFor(planoDeContasDre => planoDeContasDre.NumeroPlanoDeContas)
                .Must(numeroPlanoDeContas => !string.IsNullOrEmpty(numeroPlanoDeContas))
                .WithMessage("Número do plano de contas não informado.");
        }

        private void AssineRegraNumeroPlanoDeContasEhUnico()
        {
            //RuleFor(planoDeContas => planoDeContas.NumeroPlanoDeContas)
            //    .Must((planoDeContasDre, numeroPlanoDeContas) => NumeroPlanoDeContasEhUnico(planoDeContasDre))
            //    .WithMessage("Este número de plano de contas já está sendo utilizado.")
            //    .When(planoDeContas => !string.IsNullOrEmpty(planoDeContas.NumeroPlanoDeContas));
        }

        private void AssineRegraDescricaoPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContasDRe => planoDeContasDRe.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição do plano de contas não informado.");
        }

        private void AssineRegraNaturezaPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContasDRe => planoDeContasDRe.NaturezaPlanoContas)
                .NotNull()
                .WithMessage("Natureza do plano de contas não informado.");
        }

        private void AssineRegraTipoPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContasDre => planoDeContasDre.TipoPlanoContas)
                .NotNull()
                .WithMessage("Tipo do plano de contas não informado.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool NumeroPlanoDeContasEhUnico(PlanoContaDre planoDeContasDre)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPlanoDeContasDre>();

            var planoDeContasDaBase = repositorio.ConsultePlanoDeContasPeloNumeroDiferenteDoIdInformado(planoDeContasDre.Id, planoDeContasDre.NumeroPlanoDeContas);

            return planoDeContasDaBase == null;
        }

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraNumeroPlanoDeContasDreObrigatorio();
            AssineRegraNumeroPlanoDeContasEhUnico();

            AssineRegraDescricaoPlanoDeContasObrigatorio();

            AssineRegraNaturezaPlanoDeContasObrigatorio();
            AssineRegraTipoPlanoDeContasObrigatorio();
        }

        #endregion
    }
}
