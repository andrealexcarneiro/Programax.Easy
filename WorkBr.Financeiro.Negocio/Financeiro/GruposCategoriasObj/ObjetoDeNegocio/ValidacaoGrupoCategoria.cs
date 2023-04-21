using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio
{
    public class ValidacaoGrupoCategoria : ValidacaoBase<GrupoCategoria>
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
            RuleFor(grupo => grupo.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
