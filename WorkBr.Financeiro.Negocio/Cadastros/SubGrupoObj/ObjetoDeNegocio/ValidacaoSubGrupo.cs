using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;

namespace Programax.Easy.Negocio.Cadastros.SubGrupoObj.ObjetoDeNegocio
{
    public class ValidacaoSubGrupo : ValidacaoBase<SubGrupo>
    {
        public override void ValideInclusao()
        {
            AssineRegraGrupoObrigatorio();
            AssineRegraDescricaoObrigatoria();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraGrupoObrigatorio();
            AssineRegraDescricaoObrigatoria();
        }

        private void AssineRegraGrupoObrigatorio()
        {
            RuleFor(grupo => grupo.Grupo)
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
