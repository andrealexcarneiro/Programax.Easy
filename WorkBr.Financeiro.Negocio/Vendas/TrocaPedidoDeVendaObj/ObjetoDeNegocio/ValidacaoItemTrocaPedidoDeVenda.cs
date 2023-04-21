using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using FluentValidation;

namespace Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio
{
    public class ValidacaoItemTrocaPedidoDeVenda : ValidacaoBase<ItemTrocaPedidoDeVenda>
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
            RuleFor(item => item.Desconto)
                .Must(desconto => DescontoNaoEstahAcimaDoPermitido())
                .WithMessage("Desconto acima do permitido.")
                .When(item => item.TrocaPedidoDeVenda != null &&
                                       item.TrocaPedidoDeVenda.PedidoDeVenda != null &&
                                       item.TrocaPedidoDeVenda.PedidoDeVenda.TabelaPreco != null &&
                                       item.TrocaPedidoDeVenda.PedidoDeVenda.Usuario != null);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraQuantidadeMaiorQueZero();
        }

        private bool DescontoNaoEstahAcimaDoPermitido()
        {
            var repositorioComissao = FabricaDeRepositorios.Crie<IRepositorioComissao>();
            var repositorioTabelaPreco = FabricaDeRepositorios.Crie<IRepositorioTabelaPreco>();
            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();

            var tabelaPreco = repositorioTabelaPreco.Consulte(ObjetoValidado.TrocaPedidoDeVenda.PedidoDeVenda.TabelaPreco.Id);
            var produto = repositorioProduto.Consulte(ObjetoValidado.Produto.Id);

            var valorUnitario = CalculosPedidoDeVenda.CalculePrecoUnitarioProduto(tabelaPreco, produto);

            var valorTotalSemDesconto = valorUnitario * ObjetoValidado.Quantidade;

            var listaComissoes = repositorioComissao.ConsultePorPessoaETabelaPreco(ObjetoValidado.TrocaPedidoDeVenda.PedidoDeVenda.Usuario, tabelaPreco);

            if (listaComissoes.Count == 0)
            {
                return Math.Round(ObjetoValidado.ValorTotal, 2) >= Math.Round(valorTotalSemDesconto, 2);
            }

            Comissao comissaoMaiorDesconto = listaComissoes.FirstOrDefault();

            for (int i = 1; i < listaComissoes.Count; i++)
            {
                var comissao = listaComissoes[i];

                var valorPrimeiroDesconto = CalculosPedidoDeVenda.ApliqueDescontoComissao(valorTotalSemDesconto, comissaoMaiorDesconto);
                var valorSegundoDesconto = CalculosPedidoDeVenda.ApliqueDescontoComissao(valorTotalSemDesconto, comissao);

                if (valorSegundoDesconto > valorPrimeiroDesconto)
                {
                    comissaoMaiorDesconto = comissao;
                }
            }

            if (comissaoMaiorDesconto.DescontoMaximoEhPercentual)
            {
                if (ObjetoValidado.DescontoEhPercentual && ObjetoValidado.Desconto > comissaoMaiorDesconto.DescontoMaximo)
                {
                    return false;
                }

                var percentualDescontoAplicado = Math.Round(100 - ((ObjetoValidado.ValorTotal / (double)valorTotalSemDesconto) * 100), 5);

                if (percentualDescontoAplicado > 0 && produto.FormacaoPreco.EhPromocao.GetValueOrDefault())
                {
                    return false;
                }

                return comissaoMaiorDesconto.DescontoMaximo >= percentualDescontoAplicado;
            }
            else
            {
                var valorDescontoAplicado = valorTotalSemDesconto - ObjetoValidado.ValorTotal;

                if (valorDescontoAplicado > 0 && produto.FormacaoPreco.EhPromocao.GetValueOrDefault())
                {
                    return false;
                }

                return comissaoMaiorDesconto.DescontoMaximo >= valorDescontoAplicado;
            }
        }

        #endregion
    }
}
