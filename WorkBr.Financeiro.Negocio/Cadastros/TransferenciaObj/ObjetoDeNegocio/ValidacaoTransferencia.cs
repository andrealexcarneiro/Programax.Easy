using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.TransferenciaObj.ObjetoDeNegocio
{
    public class ValidacaoTransferencia : ValidacaoBase<Transferencia>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ValidacaoItemTransferencia _validacaoItemTransferencia;

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoTransferencia()
        {
            _validacaoItemTransferencia = new ValidacaoItemTransferencia(); 
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override FluentValidation.Results.ValidationResult Validate(Transferencia instance)
        {
            _validacaoItemTransferencia.Transferencia = instance;

            return base.Validate(instance);
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraTipoInventarioObrigatorio()
        {
            RuleFor(transferencia => transferencia.TipoInventario)
                .NotNull()
                .WithMessage("Tipo do inventário não informado.");
        }

        private void AssineRegraModalidadeInventarioObrigatorio()
        {
            RuleFor(inventario => inventario.Modalidade)
                .NotNull()
                .WithMessage("Modalidade do inventario não informado.");
        }

        //private void AssineRegraEhNecessarioTerAoMenosUmItem()
        //{
        //    RuleFor(inventario => inventario.ListaDeItens)
        //        .Must(listaDeItens => listaDeItens.Count > 0)
        //        .WithMessage("Não foi encontrado nenhum item.");
        //}

        //private void AssineRegrasItens()
        //{
        //    _validacaoItemTransferencia.ValideInclusao();

        //    RuleFor(entrada => entrada.ListaDeItens).SetValidator(_validacaoItemTransferencia);
        //}

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
           // AssineRegraEhNecessarioTerAoMenosUmItem();
           // AssineRegrasItens();
            AssineRegraTipoInventarioObrigatorio();
            AssineRegraModalidadeInventarioObrigatorio();
        }

        #endregion
    }
}
