using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Fiscal.CartaCorrecaoObj.ObjetoDeNegocio
{
    public class ValidacaoCartaCorrecao : ValidacaoBase<CartaCorrecao>
    {
        public override void ValideInclusao()
        {
            AssineRegraTamanhoMinimoCorrecao();
            AssineRegraTamanhoMaximoCorrecao();
        }

        private void AssineRegraTamanhoMinimoCorrecao()
        {
            RuleFor(carta => carta.Correcao)
                .Must(corecao => corecao.Length >= 15)
                .WithMessage("A correção deverá ter no mínimo 15 caracteres.");
        }

        private void AssineRegraTamanhoMaximoCorrecao()
        {
            RuleFor(carta => carta.Correcao)
                .Must(corecao => corecao.Length <= 1000)
                .WithMessage("A correção poderá ter no máximo 1.000 caracteres.");
        }
    }
}
