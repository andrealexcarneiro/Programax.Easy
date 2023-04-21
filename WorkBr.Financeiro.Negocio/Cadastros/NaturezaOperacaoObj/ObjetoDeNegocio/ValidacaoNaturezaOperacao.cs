using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.NaturezaOperacaoObj.ObjetoDeNegocio
{
    public class ValidacaoNaturezaOperacao : ValidacaoBase<NaturezaOperacao>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ValidacaoNaturezaOperacaoCfop _validacaoNaturezaOperacaoCfop;

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoNaturezaOperacao()
        {
            _validacaoNaturezaOperacaoCfop = new ValidacaoNaturezaOperacaoCfop();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAAtualizacaoEInclusao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAAtualizacaoEInclusao();
        }

        public override FluentValidation.Results.ValidationResult Validate(NaturezaOperacao instance)
        {
            _validacaoNaturezaOperacaoCfop.NaturezaOperacao = instance;

            return base.Validate(instance);
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(naturezaOperacao => naturezaOperacao.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição não informada!");
        }

        private void AssineRegraListaCfops()
        {
            _validacaoNaturezaOperacaoCfop.ValideInclusao();

            RuleFor(naturezaOperacao => naturezaOperacao.ListaCfops).SetValidator(_validacaoNaturezaOperacaoCfop);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAAtualizacaoEInclusao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraListaCfops();
        }

        #endregion
    }
}
