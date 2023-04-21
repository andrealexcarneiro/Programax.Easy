using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using System.Linq;

namespace Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.ObjetoDeNegocio
{
    public class ValidacaoGrupoTributacaoIcms : ValidacaoBase<GrupoTributacaoIcms>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraPossuiPeloMenosUmaTributacao();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraPossuiPeloMenosUmaTributacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(grupoTributacaoIcms => grupoTributacaoIcms.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição não informada");
        }

        private void AssineRegraPossuiPeloMenosUmaTributacao()
        {
            RuleFor(grupoTributacaoIcms => grupoTributacaoIcms.ListaTributacoesIcms)
                .Must(listaTributacoesIcms => listaTributacoesIcms.Count > 0)
                .WithMessage("É necessário ter pelo menos uma tributação adicionada");
        }

        #endregion
    }
}
