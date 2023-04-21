using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.GrupoObj.ObjetoDeNegocio
{
    public class ValidacaoGrupo : ValidacaoBase<Grupo>
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
            RuleFor(grupo => grupo.Categoria)
                .NotNull()
                .WithMessage("Linha não informada");
        }

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(grupo => grupo.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada");
        }
    }
}
