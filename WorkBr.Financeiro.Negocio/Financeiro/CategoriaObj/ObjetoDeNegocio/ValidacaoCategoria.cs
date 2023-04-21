using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio
{
    public class ValidacaoCategoria : ValidacaoBase<CategoriaFinanceira>
    {
        public override void ValideInclusao()
        {
            AssineRegraLinhaObrigatoria();
            AssineRegraDescricaoObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraLinhaObrigatoria();
            AssineRegraDescricaoObrigatoria();
        }

        private void AssineRegraLinhaObrigatoria()
        {
            RuleFor(categoria => categoria.SubGrupoCategoria)
                .NotNull()
                .WithMessage("Grupo não informado");
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(grupo => grupo.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
