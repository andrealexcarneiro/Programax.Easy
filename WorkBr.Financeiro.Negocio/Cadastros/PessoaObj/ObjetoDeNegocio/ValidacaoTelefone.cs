using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio
{
    public class ValidacaoTelefone : ValidacaoBase<Telefone>
    {
        public override void ValideInclusao()
        {
            AssineRegrasComunsAAtulizacaoEInclusao();
        }

        public override void ValideAtualizacao()
        {
            AssineRegrasComunsAAtulizacaoEInclusao();
        }

        private void AssineRegrasComunsAAtulizacaoEInclusao()
        {
            AssineRegraTipoTelefoneObrigatorio();
            AssineRegraDDDTelefoneObrigatorio();
            AssineRegraNumeroTelefoneObrigatorio();
        }

        private void AssineRegraTipoTelefoneObrigatorio()
        {
            RuleFor(Telefone => Telefone.TipoTelefone)
                .NotNull()
                .WithMessage("O tipo do telefone não foi informado.");
        }

        private void AssineRegraDDDTelefoneObrigatorio()
        {
            RuleFor(Telefone => Telefone.Ddd)
                .NotNull()
                .WithMessage("DDD Não foi informado.");
        }

        private void AssineRegraNumeroTelefoneObrigatorio()
        {
            RuleFor(Telefone => Telefone.Numero)
                .Must(numero => !string.IsNullOrEmpty(numero))
                .WithMessage("Numéro do telefone não foi informado.");
        }
    }
}
