using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio
{
    public class ValidacaoFormaPagamento : ValidacaoBase<FormaPagamento>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(formaPagamento => formaPagamento.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada.");
        }

        #endregion
    }
}
