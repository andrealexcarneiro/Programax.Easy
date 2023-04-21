using FluentValidation;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    public class ValidacaoFornecedorProduto : ValidacaoBase<FornecedorProduto>
    {
        #region " PROPRIEDADES "

        public Produto Produto { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraFornecedorECodigoSaoUnicos();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraFornecedorECodigoSaoUnicos();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraFornecedorECodigoSaoUnicos()
        {
            RuleFor(produto => produto.CodigoProduto)
                .Must(codigoProduto => CodigoFornecedorEhUnico())
                .WithMessage("O código do fornecedor já está sendo utilizado por outro produto.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool CodigoFornecedorEhUnico()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var produto = repositorio.ConsulteProdutoPeloCodigoFornecedorExcetoParaOProduto(ObjetoValidado.CodigoProduto, ObjetoValidado.Fornecedor, Produto.Id);

            return produto == null || produto.Id == Produto.Id;
        }

        #endregion
    }
}
