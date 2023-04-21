using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.InventarioObj.ObjetoDeNegocio
{
    public class ValidacaoInventario : ValidacaoBase<Inventario>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ValidacaoItemInventario _validacaoItemInventario;

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoInventario()
        {
            _validacaoItemInventario = new ValidacaoItemInventario(); 
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

        public override FluentValidation.Results.ValidationResult Validate(Inventario instance)
        {
            //_validacaoItemInventario.Inventario = instance;

            return base.Validate(instance);
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraTipoInventarioObrigatorio()
        {
            RuleFor(inventario => inventario.TipoInventario)
                .NotNull()
                .WithMessage("Tipo do inventário não informado.");
        }

        private void AssineRegraModalidadeInventarioObrigatorio()
        {
            RuleFor(inventario => inventario.Modalidade)
                .NotNull()
                .WithMessage("Modalidade do inventario não informado.");
        }

        private void AssineRegraEhNecessarioTerAoMenosUmItem()
        {
            RuleFor(inventario => inventario.ListaDeItens)
                .Must(listaDeItens => listaDeItens.Count > 0)
                .WithMessage("Não foi encontrado nenhum item.");
        }

        private void AssineRegrasItens()
        {
            _validacaoItemInventario.ValideInclusao();

            RuleFor(entrada => entrada.ListaDeItens).SetValidator(_validacaoItemInventario);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraEhNecessarioTerAoMenosUmItem();
            AssineRegrasItens();
            AssineRegraTipoInventarioObrigatorio();
            AssineRegraModalidadeInventarioObrigatorio();
        }

        #endregion
    }
}
