using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.CorObj.ObjetoDeNegocio
{
    public class ValidacaoCor : ValidacaoBase<Cor>
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
            RuleFor(cor => cor.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
