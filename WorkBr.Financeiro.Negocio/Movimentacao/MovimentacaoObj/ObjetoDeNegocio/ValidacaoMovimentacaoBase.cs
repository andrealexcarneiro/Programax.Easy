using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio
{
    public class ValidacaoMovimentacaoBase<TObjeto> : ValidacaoBase<TObjeto>
        where TObjeto : MovimentacaoBase
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAoInserirEAtualizar();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAoInserirEAtualizar();
        }

        #endregion

        #region " REGRAS "

        protected void AssineRegraEhNecessarioPeloMenos1Produto()
        {
            RuleFor(movimentacao => movimentacao.ListaDeItens)
                .Must(listaDeItens => listaDeItens != null && listaDeItens.Count > 0)
                .WithMessage("É necessário inserir ao menos 1 item.");
        }

        protected void AssineRegrasItens()
        {
            ValidacaoItemMovimentacao validacaoItemMovimentacao = new ValidacaoItemMovimentacao();

            validacaoItemMovimentacao.ValideInclusao();

            RuleFor(entrada => entrada.ListaDeItens).SetValidator(validacaoItemMovimentacao);
        }

        private void AssineRegraMotivoObrigatorio()
        {
            RuleFor(correcao => correcao.Motivo)
                .NotNull()
                .WithMessage("Motivo da correção é obrigatório")
                .When(movimentacao => movimentacao.OrigemMovimentacao == EnumOrigemMovimentacao.CORRECAODEESTOQUE);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        protected virtual void RegrasComunsAoInserirEAtualizar()
        {
            AssineRegraEhNecessarioPeloMenos1Produto();
            AssineRegrasItens();
            AssineRegraMotivoObrigatorio();
        }

        #endregion
    }
}
