using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Fiscal.CestObj.ObjetoDeNegocio
{
    public class ValidacaoCest:ValidacaoBase<Cest>
    {

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }
        #endregion

        #region " REGRAS "

        private void AssineRegraDescricaoCestObrigatorio()
        {
            RuleFor(cest => cest.DescricaoCest)
                .Must(descricaoCest => !string.IsNullOrEmpty(descricaoCest))
                .WithMessage("Descrição Cest não foi informado.");
        }

        private void AssineRegraCodigoCestObrigatorio()
        {
            RuleFor(cest => cest.CodigoCest)
                .Must(codigoCest => !string.IsNullOrEmpty(codigoCest))
                .WithMessage("Código Cest não foi informado.");
        }
                
        #endregion

        #region " MÉTODOS AUXILIARES "
        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraDescricaoCestObrigatorio();
            AssineRegraCodigoCestObrigatorio();           
        }
        
        #endregion

    }
}
