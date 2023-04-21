using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.OrigemClienteObj.ObjetoDeNegocio
{
    public class ValidacaoOrigemCliente : ValidacaoBase<OrigemCliente>
    {
        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatorio();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatorio();
        }

        private void AssineRegraDescricaoObrigatorio()
        {
            RuleFor(origemCliente => origemCliente.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada.");
        }
    }
}
