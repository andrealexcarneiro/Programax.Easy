using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Fiscal.CnaeObj.ObjetoDeNegocio
{
    public class ValidacaoCnae : ValidacaoBase<Cnae>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraCodigoObrigatorio();
            AssineRegraDescricaoObrigatoria();
            AssineRegraAtividadeObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraCodigoObrigatorio();
            AssineRegraDescricaoObrigatoria();
            AssineRegraAtividadeObrigatoria();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraCodigoObrigatorio()
        {
            RuleFor(cnae => cnae.Codigo)
                .Must(codigo => !string.IsNullOrEmpty(codigo))
                .WithMessage("Código não informado.");
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(cnae => cnae.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição não informada.");
        }

        private void AssineRegraAtividadeObrigatoria()
        {
            RuleFor(cnae => cnae.Atividade)
                .NotNull()
                .WithMessage("Atividade não informada.");
        }

        #endregion
    }
}
