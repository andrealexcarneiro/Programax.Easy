using System;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using System.Text;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Cadastros.GrupoTributacaoIcmsObj.Repositorio;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{

    public class ValidacaoProduto : ValidacaoBase<Produto>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ValidacaoFornecedorProduto _validacaoFornecedorProduto;

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoProduto()
        {
            _validacaoFornecedorProduto = new ValidacaoFornecedorProduto();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override FluentValidation.Results.ValidationResult Validate(Produto instance)
        {
            _validacaoFornecedorProduto.ValideInclusao();
            _validacaoFornecedorProduto.Produto = instance;

            return base.Validate(instance);
        }

        public override void ValideInclusao()
        {
            ValidacoesComunsDeInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            ValidacoesComunsDeInclusaoEAtualizacao();
        }

        #endregion

        #region " REGRAS DE OBRIGATORIEDADE "

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(produto => produto.DadosGerais.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição não informada.");
        }

        private void AssineRegraUnidadeEhObrigatoria()
        {
            RuleFor(produto => produto.DadosGerais.Unidade)
                .NotNull()
                .WithMessage("Unidade não foi informada.");
        }

        #endregion

        #region " REGRAS DE NEGÓCIO "

        private void AssineRegraPrecoVendaMaiorPrecoCusto()
        {
            //RuleFor(produto => produto.FormacaoPreco.ValorVenda)
            //    .Must((produto, valorVenda) => valorVenda.Value >= produto.FormacaoPreco.PrecoDeCompra.Value)
            //    .WithMessage("Preço de venda menor que preço de Custo.")
            //    .When(produto => produto.FormacaoPreco != null && produto.FormacaoPreco.PrecoDeCompra != null && produto.ValorVenda != null);
        }

        private void AssineRegraCodigoDeBarrasEhUnico()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("O item {0} - {1} já está cadastrado com este código de barras.");

            RuleFor(produto => produto.DadosGerais.CodigoDeBarras)
                .Must((produto, codigoDeBarras) => CodigoDeBarrasEhUnico(produto, mensagemComposta))
                .WithMessage(mensagemComposta)
                .When(produto => produto.DadosGerais != null && !string.IsNullOrEmpty(produto.DadosGerais.CodigoDeBarras));
        }

        private void AssineRegrasFornecedoresProdutos()
        {
            RuleFor(produto => produto.ListaFornecedores).SetValidator(_validacaoFornecedorProduto);
        }

        private void AssineRegraNaturezaProdutoIgualNaturezaGrupoIcms()
        {
            RuleFor(produto => produto.ContabilFiscal.NaturezaProduto)
                .Must(naturezaProduto => NaturezaProdutoEhIgualNaturezaGrupoIcms())
                .WithMessage("Natureza do produto é diferente do grupo tributação icms")
                .When(produto => produto.ContabilFiscal.GrupoTributacaoIcms != null);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void ValidacoesComunsDeInclusaoEAtualizacao()
        {
            AssineRegraDescricaoObrigatoria();
            AssineRegraUnidadeEhObrigatoria();

            AssineRegraPrecoVendaMaiorPrecoCusto();

            AssineRegraCodigoDeBarrasEhUnico();

            AssineRegrasFornecedoresProdutos();
            AssineRegraNaturezaProdutoIgualNaturezaGrupoIcms();
        }

        private bool CodigoDeBarrasEhUnico(Produto produto, MensagemComposta mensagemComposta)
        {
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            Produto produtoDaBase = repositorioProduto.ConsultePeloCodigoDeBarras(produto.DadosGerais.CodigoDeBarras, produto.Id);

            if (produtoDaBase != null)
            {
                mensagemComposta.ListaDeParametros.Add(produtoDaBase.Id);
                mensagemComposta.ListaDeParametros.Add(produtoDaBase.DadosGerais.Descricao);
            }

            return produtoDaBase == null;
        }

        private bool NaturezaProdutoEhIgualNaturezaGrupoIcms()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioGrupoTributacaoIcms>();
            var grupoTributacaoIcms = repositorio.Consulte(ObjetoValidado.ContabilFiscal.GrupoTributacaoIcms.Id);

            return ObjetoValidado.ContabilFiscal.NaturezaProduto == grupoTributacaoIcms.NaturezaProduto;
        }

        #endregion
    }
}
