using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.TamanhoObj.ObjetoDeNegocio
{
    public class ValidacaoTamanho : ValidacaoBase<Tamanho>
    {
        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(tamanho => tamanho.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
