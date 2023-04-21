using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio
{
    public class ValidacaoItemPedidoTrocaPedidoDeVenda : ValidacaoBase<ItemPedidoTrocaPedidoDeVenda>
    {
        #region " VARIÁVEIS PRIVADAS "

        private PedidoDeVenda _pedidoDeVendaDaBase;

        #endregion

        #region " PROPRIEDADES "

        public TrocaPedidoDeVenda TrocaPedidoDeVenda { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraQuantidadeDevolvidaMenorOuIgualAQuantidadeDoProduto();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraQuantidadeDevolvidaMenorOuIgualAQuantidadeDoProduto();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraQuantidadeDevolvidaMenorOuIgualAQuantidadeDoProduto()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("O produto {0} - {1}, já contém {2} produtos devolvidos, o máximo que pode ser devolvido é {3}");

            RuleFor(itemPedidoTroca => itemPedidoTroca.QuantidadeTrocar)
                .Must(quantidadeTrocar => QuantidadeDevolvidaEhMenorOuIgualAQuantidadeDoProduto(mensagemComposta))
                .WithMessage(mensagemComposta);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool QuantidadeDevolvidaEhMenorOuIgualAQuantidadeDoProduto(MensagemComposta mensagemComposta)
        {
            if (ObjetoValidado.QuantidadeTrocar > 0)
            {
                var pedido = RetornePedidoDeVendaDaBase();

                var itemPedido = pedido.ListaItens.FirstOrDefault(itemDoPedido => itemDoPedido.Produto.Id == ObjetoValidado.Produto.Id &&
                                                                                                                   itemDoPedido.Quantidade == ObjetoValidado.Quantidade &&
                                                                                                                   itemDoPedido.ValorUnitario == ObjetoValidado.ValorUnitario &&
                                                                                                                   itemDoPedido.ValorTotal == ObjetoValidado.ValorTotal);

                mensagemComposta.ListaDeParametros.Add(itemPedido.Produto.Id.ToString());
                mensagemComposta.ListaDeParametros.Add(itemPedido.Produto.DadosGerais.Descricao);
                mensagemComposta.ListaDeParametros.Add(itemPedido.QuantidadeDevolvida.ToString());
                mensagemComposta.ListaDeParametros.Add((itemPedido.Quantidade - itemPedido.QuantidadeDevolvida).ToString());

                return itemPedido.QuantidadeDevolvida + ObjetoValidado.QuantidadeTrocar <= itemPedido.Quantidade;
            }

            return true;
        }

        private PedidoDeVenda RetornePedidoDeVendaDaBase()
        {
            if (_pedidoDeVendaDaBase == null)
            {
                var repositorioPedidoDeVenda = FabricaDeRepositorios.Crie<IRepositorioPedidoDeVenda>();

                _pedidoDeVendaDaBase = repositorioPedidoDeVenda.ConsulteSemSessao(TrocaPedidoDeVenda.PedidoDeVenda.Id);
            }

            return _pedidoDeVendaDaBase;
        }

        #endregion
    }
}
