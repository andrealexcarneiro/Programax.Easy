using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.ContaBancariaObj.ObjetoDeNegocio
{
    public class ValidacaoContaBancaria : ValidacaoBase<ContaBancaria>
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

        private void AssineRegraAgenciaObrigatoria()
        {
            RuleFor(contaBancaria => contaBancaria.Agencia)
                .NotNull()
                .WithMessage("Agência não informada.");
        }

        private void AssineRegraTipoContaObrigatoria()
        {
            RuleFor(contaBancaria => contaBancaria.TipoContaBancaria)
                .NotNull()
                .WithMessage("Tipo de Conta Bancária não informada.");
        }

        private void AssineRegraNumeroContaObrigatoria()
        {
            RuleFor(contaBancaria => contaBancaria.NumeroConta)
                .Must(numeroConta => NumeroContaFoiInformado())
                .WithMessage("Número Conta não informada.");
        }

        private void AssineRegraDigitoContaObrigatoria()
        {
            RuleFor(contaBancaria => contaBancaria.NumeroConta)
                .Must(numeroConta => DigitoContaFoiInformado())
                .WithMessage("É necessário informar o número da conta seguido com um hífen e o dígito da mesma.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraAgenciaObrigatoria();
            AssineRegraTipoContaObrigatoria();
            AssineRegraNumeroContaObrigatoria();
            AssineRegraDigitoContaObrigatoria();
        }

        private bool NumeroContaFoiInformado()
        {
            var numeroEDigito = ObjetoValidado.NumeroConta.Split('-');

            if (numeroEDigito.Length == 0)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(numeroEDigito[0]);
        }

        private bool DigitoContaFoiInformado()
        {
            var numeroEDigito = ObjetoValidado.NumeroConta.Split('-');

            if (numeroEDigito.Length <= 1)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(numeroEDigito[1]);
        }

        #endregion
    }
}
