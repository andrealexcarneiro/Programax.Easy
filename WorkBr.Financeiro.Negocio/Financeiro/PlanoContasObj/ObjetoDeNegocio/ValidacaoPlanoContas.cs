using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio
{
    public class ValidacaoPlanoContas : ValidacaoBase<PlanoDeContas>
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

        private void AssineRegraNumeroPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContas => planoDeContas.NumeroPlanoDeContas)
                .Must(numeroPlanoDeContas => !string.IsNullOrEmpty(numeroPlanoDeContas))
                .WithMessage("Número do plano de contas não informado.");
        }

        private void AssineRegraNumeroPlanoDeContasEhUnico()
        {
            RuleFor(planoDeContas => planoDeContas.NumeroPlanoDeContas)
                .Must((planoDeContas, numeroPlanoDeContas) => NumeroPlanoDeContasEhUnico(planoDeContas))
                .WithMessage("Este número de plano de contas já está sendo utilizado.")
                .When(planoDeContas => !string.IsNullOrEmpty(planoDeContas.NumeroPlanoDeContas));
        }

        private void AssineRegraDescricaoPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContas => planoDeContas.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição do plano de contas não informado.");
        }

        private void AssineRegraNaturezaPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContas => planoDeContas.NaturezaPlanoContas)
                .NotNull()
                .WithMessage("Natureza do plano de contas não informado.");
        }

        private void AssineRegraTipoPlanoDeContasObrigatorio()
        {
            RuleFor(planoDeContas => planoDeContas.TipoPlanoContas)
                .NotNull()
                .WithMessage("Tipo do plano de contas não informado.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool NumeroPlanoDeContasEhUnico(PlanoDeContas planoDeContas)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPlanoDeContas>();

            var planoDeContasDaBase = repositorio.ConsultePlanoDeContasPeloNumeroDiferenteDoIdInformado(planoDeContas.Id, planoDeContas.NumeroPlanoDeContas);

            return planoDeContasDaBase == null;
        }

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraNumeroPlanoDeContasObrigatorio();
            AssineRegraNumeroPlanoDeContasEhUnico();

            AssineRegraDescricaoPlanoDeContasObrigatorio();

            AssineRegraNaturezaPlanoDeContasObrigatorio();
            AssineRegraTipoPlanoDeContasObrigatorio();
        }

        #endregion
    }
}
