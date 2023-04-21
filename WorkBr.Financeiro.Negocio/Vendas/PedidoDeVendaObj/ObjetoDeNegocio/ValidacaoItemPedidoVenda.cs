using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    public class ValidacaoItemPedidoDeVenda : ValidacaoBase<ItemPedidoDeVenda>
    {
        #region " VARIÁVEIS PRIVADAS "

        private TabelaPreco _tabelaPreco;
        private List<Comissao> _listaComissoes;

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

        #endregion

        #region " MÉTODOS PÚBLICOS "

        public void ValideItemLiberacao()
        {
            AssineRegraDescontoMaiorQueOPermitido();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraQuantidadeMaiorQueZero()
        {
            RuleFor(item => item.Quantidade)
                .Must(quantidade => quantidade > 0)
                .WithMessage("Quantidade tem que ser maior que zero.");
        }

        private void AssineRegraDescontoMaiorQueOPermitido()
        {
            RuleFor(item => item.TotalDesconto)
                .Must(desconto => DescontoNaoEstahAcimaDoPermitido())
                .WithMessage("Desconto acima do permitido.")
                .When(item => item.PedidoDeVenda != null && item.PedidoDeVenda.TabelaPreco != null && item.PedidoDeVenda.Usuario != null);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraQuantidadeMaiorQueZero();
        }

        private bool DescontoNaoEstahAcimaDoPermitido()
        {
            #region " INSTANCIA REPOSITÓRIOS "

            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            #endregion

            #region " PESQUISA DE TABELA DE PREÇO E PRODUTO "

            var tabelaPreco = RetorneTabelaPreco();
            var produto = repositorioProduto.Consulte(ObjetoValidado.Produto.Id);

            #endregion

            #region " VALORES UNITARIO, TOTAL SEM DESCONTO E PERCENTUAL DESCONTO APLICADO "

            var valorUnitarioBase = CalculosPedidoDeVenda.CalculePrecoUnitarioProduto(_tabelaPreco, produto);
            var valorTotalSemDescontoBase = valorUnitarioBase * ObjetoValidado.Quantidade;
            var valorTotalProduto = ObjetoValidado.Quantidade * ObjetoValidado.ValorUnitario - ObjetoValidado.TotalDesconto;

            #endregion

            #region " VERIFICA SE DESCONTO NÃO É MAIOR QUE DO PRODUTO "

            if (produto.FormacaoPreco != null &&
                produto.FormacaoPreco.PercentualDescontoMaximo != null)
            {
                double valorDescontoMaximoPorProduto = Math.Round(valorTotalSemDescontoBase * (produto.FormacaoPreco.PercentualDescontoMaximo.Value / (double)100), 2);

                if (ObjetoValidado.DescontoEhPercentual)
                {
                    double TotalDescontoPercentual = valorTotalSemDescontoBase * (ObjetoValidado.DescontoUnitario / (double)100);

                    if (TotalDescontoPercentual > valorDescontoMaximoPorProduto)
                    {
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    if (ObjetoValidado.DescontoUnitario > valorDescontoMaximoPorProduto)
                    {
                        return false;
                    }
                    else
                        return true;
                }
            } 

            #endregion

            #region " PESQUISA TODAS AS COMISSÕES COM A TABELA DE PREÇO EM QUESTÃO PARA O USUARIO DA VENDA "

            var listaComissoes = RetorneListaComissoes();

            #endregion

            #region " CASO NÃO TENHA NENHUMA COMISSÃO PARA A TABELA DE PREÇO "

            if (listaComissoes.Count == 0)
            {
                return Math.Round(valorTotalProduto, 2) >= Math.Round(valorTotalSemDescontoBase, 2);
            }

            #endregion

            #region " VERIFICA QUAL O MAIOR DESCONTO "

            Comissao comissaoMaiorDesconto = listaComissoes.FirstOrDefault();

            for (int i = 1; i < listaComissoes.Count; i++)
            {
                var comissao = listaComissoes[i];

                var valorPrimeiroDesconto = CalculosPedidoDeVenda.ApliqueDescontoComissao(valorTotalSemDescontoBase, comissaoMaiorDesconto);
                var valorSegundoDesconto = CalculosPedidoDeVenda.ApliqueDescontoComissao(valorTotalSemDescontoBase, comissao);

                if (valorSegundoDesconto > valorPrimeiroDesconto)
                {
                    comissaoMaiorDesconto = comissao;
                }
            }

            #endregion

            #region " QUANDO DESCONTO DA COMISSÃO É POR PERCENTUAL "

            if (comissaoMaiorDesconto.DescontoMaximoEhPercentual)
            {
                var valorMaximoTotalProduto = valorTotalSemDescontoBase - Math.Round(valorTotalSemDescontoBase * comissaoMaiorDesconto.DescontoMaximo / (double)100, 2);

                if (valorTotalProduto > valorMaximoTotalProduto && produto.FormacaoPreco.EhPromocao.GetValueOrDefault())
                {
                    return false;
                }

                return valorTotalProduto >= valorMaximoTotalProduto;
            }

            #endregion

            #region " QUANDO DESCONTO DA COMISSÃO É POR VALOR "

            else
            {
                if (ObjetoValidado.TotalDesconto > 0 && produto.FormacaoPreco.EhPromocao.GetValueOrDefault())
                {
                    return false;
                }

                return comissaoMaiorDesconto.DescontoMaximo >= ObjetoValidado.TotalDesconto;
            }
            #endregion
        }

        private TabelaPreco RetorneTabelaPreco()
        {
            if (_tabelaPreco == null)
            {
                var repositorioTabelaPreco = FabricaDeRepositorios.Crie<IRepositorioTabelaPreco>();
                _tabelaPreco = repositorioTabelaPreco.Consulte(ObjetoValidado.PedidoDeVenda.TabelaPreco.Id);
            }

            return _tabelaPreco;
        }

        private List<Comissao> RetorneListaComissoes()
        {
            if (_listaComissoes == null)
            {
                var repositorioComissao = FabricaDeRepositorios.Crie<IRepositorioComissao>();
                _listaComissoes = repositorioComissao.ConsultePorPessoaETabelaPreco(ObjetoValidado.PedidoDeVenda.Usuario, _tabelaPreco);
            }

            return _listaComissoes;
        }

        #endregion
    }
}
