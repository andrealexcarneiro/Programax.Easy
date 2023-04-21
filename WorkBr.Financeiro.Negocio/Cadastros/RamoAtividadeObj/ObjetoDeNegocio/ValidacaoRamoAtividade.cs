using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.RamoAtividadeObj.ObjetoDeNegocio
{
    public class ValidacaoRamoAtividade : ValidacaoBase<RamoAtividade>
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
            RuleFor(ramoAtividade => ramoAtividade.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada.");
        }
    }
}
