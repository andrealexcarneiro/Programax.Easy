using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio
{
    public class ValidacaoItemEntrada : ValidacaoBase<ItemEntrada>
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

        #region " REGRAS DE OBRIGATORIEDADE "

        private void AssineRegraProdutoObrigatorio()
        {
            RuleFor(item => item.Produto)
                .NotNull()
                .WithMessage("Produto não informado.");
        }

        private void AssineRegraUnidadeObrigatoria()
        {
            RuleFor(item => item.Unidade)
                .NotNull()
                .WithMessage("Unidade não informada.");
        }

        private void AssineRegraUnidadeOrigemObrigatorio()
        {
            RuleFor(item => item.Origem)
                .NotNull()
                .WithMessage("Origem não informada.");
        }

        private void AssineRegraCstCsosnObrigatorio()
        {
            RuleFor(item => item.CstCsosn)
                .NotNull()
                .WithMessage("CST / CSOSN não informado.");
        }

        private void AssineRegraCfopObrigatorio()
        {
            RuleFor(item => item.Cfop)
                .NotNull()
                .WithMessage("CFOP não informado.");
        }

        private void AssineRegraValorUnitarioObrigatorio()
        {
            RuleFor(item => item.ValorUnitario)
                .NotNull()
                .WithMessage("Valor unitário não informado.");
        }

        private void AssineRegraQuantidadeBrutaObrigatoria()
        {
            RuleFor(item => item.QuantidadeBruta)
                .NotNull()
                .WithMessage("Quantidade não informada.");
        }

        private void AssineRegraQuantidadeObrigatoria()
        {
            RuleFor(item => item.Quantidade)
                .NotNull()
                .WithMessage("Quantidade Estocar não informada.");
        }

        private void AssineRegraQuantidadeMaiorQueZero()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Item com código {0} - {1} quantidade estocar tem que ser maior que 0(zero).");

            RuleFor(item => item.Quantidade)
                .Must((item, quantidade) =>
                    {
                        mensagemComposta.ListaDeParametros.Add(item.Produto.Id);
                        mensagemComposta.ListaDeParametros.Add(item.Produto.DadosGerais.Descricao);

                        return quantidade > 0;
                    })
                .WithMessage(mensagemComposta)
                .When(item => item.Quantidade != null && item.Produto != null);
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAoInserirEAtualizar()
        {
            AssineRegraProdutoObrigatorio();
            AssineRegraUnidadeObrigatoria();
            AssineRegraUnidadeOrigemObrigatorio();
            AssineRegraCstCsosnObrigatorio();
            AssineRegraCfopObrigatorio();

            AssineRegraValorUnitarioObrigatorio();

            AssineRegraQuantidadeBrutaObrigatoria();
            AssineRegraQuantidadeObrigatoria();
            AssineRegraQuantidadeMaiorQueZero();
        }

        #endregion
    }
}
