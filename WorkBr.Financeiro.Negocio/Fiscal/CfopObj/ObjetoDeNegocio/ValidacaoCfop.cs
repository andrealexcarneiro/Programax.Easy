using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Fiscal.CfopObj.ObjetoDeNegocio
{
    public class ValidacaoCfop : ValidacaoBase<Cfop>
    {
        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraCodigoObrigatorio();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraCodigoObrigatorio();
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(cfop => cfop.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }

        private void AssineRegraCodigoObrigatorio()
        {
            RuleFor(cfop => cfop.Codigo)
                .Must(codigo => !string.IsNullOrEmpty(codigo))
                .WithMessage("Código não informado");
        }
    }
}
