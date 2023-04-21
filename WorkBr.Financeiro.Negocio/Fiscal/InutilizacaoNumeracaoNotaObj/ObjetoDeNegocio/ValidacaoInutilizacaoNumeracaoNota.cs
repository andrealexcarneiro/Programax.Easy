using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Negocio.Fiscal.InutilizacaoNumeracaoNotaObj.ObjetoDeNegocio
{
    public class ValidacaoInutilizacaoNumeracaoNota : ValidacaoBase<InutilizacaoNumeracaoNota>
    {
        public override void ValideInclusao()
        {
            AssineRegraNumeroInicialObrigatorio();
            AssineRegraNumeroFinalObrigatorio();
            AssineRegraNumeroFinalMaiorOuIgualNumeroInicial();
            AssineRegraAnoObrigatorio();
            AssineRegraSerieObrigatoria();
            AssineRegraJustificativaObrigatoria();
            AssineRegraJustificativaTamanhoMinimo();
        }

        private void AssineRegraNumeroInicialObrigatorio()
        {
            RuleFor(inutilizacao => inutilizacao.NumeroInicial)
                .Must(numeroInicial => numeroInicial > 0)
                .WithMessage("Número inicial não informado.");
        }

        private void AssineRegraNumeroFinalObrigatorio()
        {
            RuleFor(inutilizacao => inutilizacao.NumeroFinal)
                .Must(numeroFinal => numeroFinal > 0)
                .WithMessage("Número final não informado.");
        }

        private void AssineRegraNumeroFinalMaiorOuIgualNumeroInicial()
        {
            RuleFor(inutilizacao => inutilizacao.NumeroFinal)
                .Must(numeroFinal => numeroFinal >= ObjetoValidado.NumeroInicial)
                .WithMessage("Número final tem que ser maior ou igual ao número inicial")
                .When(inutilizacao => inutilizacao.NumeroFinal > 0 && inutilizacao.NumeroInicial > 0);
        }

        private void AssineRegraAnoObrigatorio()
        {
            RuleFor(inutilizacao => inutilizacao.Ano)
                .Must(ano => !string.IsNullOrEmpty(ano) && ano.ToInt() > 0)
                .WithMessage("Ano não informado.");
        }

        private void AssineRegraSerieObrigatoria()
        {
            RuleFor(inutilizacao => inutilizacao.Serie)
                .Must(serie => serie > 0)
                .WithMessage("Série não informada.");
        }

        private void AssineRegraJustificativaObrigatoria()
        {
            RuleFor(inutilizacao => inutilizacao.Justificativa)
                .Must(justificativa => !string.IsNullOrEmpty( justificativa))
                .WithMessage("Justificativa não informada.");
        }

        private void AssineRegraJustificativaTamanhoMinimo()
        {
            RuleFor(inutilizacao => inutilizacao.Justificativa)
                .Must(justificativa => justificativa.Length >= 15)
                .WithMessage("Justificativa precisa de no mínimo 15 caracteres.")
                .When(inutilizacao => !string.IsNullOrEmpty(inutilizacao.Justificativa));
        }
    }
}
