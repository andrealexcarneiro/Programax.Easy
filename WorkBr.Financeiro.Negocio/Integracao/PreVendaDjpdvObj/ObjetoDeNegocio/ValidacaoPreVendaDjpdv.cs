using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using FluentValidation;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Negocio.Integracao.PreVendaDjpdvObj.ObjetoDeNegocio
{
    public class ValidacaoPreVendaDjpdv : ValidacaoBase<PreVendaDjpdv>
    {
        #region " VARIÁVEIS PRIVADAS "

        private PedidoDeVenda _pedidoDeVenda;

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraPreVendaNaoContemItensSemNcm();
            AssineRegraPreVendaContemItensSemValor();
            AssineRegraPedidoDeVendaTemQueEstarFaturado();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraPreVendaNaoContemItensSemNcm();
            AssineRegraPreVendaContemItensSemValor();
            AssineRegraPedidoDeVendaTemQueEstarFaturado();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraPreVendaNaoContemItensSemNcm()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Os seguinte produtos não possuem NCM:{0}");

            RuleFor(preVenda => preVenda.PedidoDeVendaId)
                .Must(pedidDeVendaId => !PreVendaContemItensSemNcm(mensagemComposta))
                .WithMessage(mensagemComposta);
        }

        private void AssineRegraPreVendaContemItensSemValor()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Os seguinte produtos não possuem Valor Unitário:{0}");

            RuleFor(preVenda => preVenda.PedidoDeVendaId)
                .Must(pedidDeVendaId => !PreVendaContemItensItensSemValorUnitario(mensagemComposta))
                .WithMessage(mensagemComposta);
        }

        private void AssineRegraPedidoDeVendaTemQueEstarFaturado()
        {
            RuleFor(preVenda => preVenda.PedidoDeVendaId)
                .Must(preVendaId => PreVendaEstahFaturada())
                .WithMessage("Pedido de venda tem que estar faturado.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool PreVendaContemItensSemNcm(MensagemComposta mensagemComposta)
        {
            StringBuilder produtosSemNcm = new StringBuilder();

            var pedidoDeVenda = RetornePedidoDeVenda();

            bool existeProdutoSemNcm = false;

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if (item.Produto.ContabilFiscal == null || item.Produto.ContabilFiscal.Ncm == null)
                {
                    produtosSemNcm.Append("\r\n");
                    produtosSemNcm.Append(item.Produto.Id);
                    produtosSemNcm.Append(" - ");
                    produtosSemNcm.Append(item.Produto.DadosGerais.Descricao);

                    existeProdutoSemNcm = true;
                }
            }

            mensagemComposta.ListaDeParametros.Add(produtosSemNcm);

            return existeProdutoSemNcm;
        }

        private bool PreVendaContemItensItensSemValorUnitario(MensagemComposta mensagemComposta)
        {
            StringBuilder produtosSemValorUnitario = new StringBuilder();

            var pedidoDeVenda = RetornePedidoDeVenda();

            bool existeProdutoSemValorUnitario = false;

            foreach (var item in pedidoDeVenda.ListaItens)
            {
                if (item.ValorUnitario == 0)
                {
                    produtosSemValorUnitario.Append("\r\n");
                    produtosSemValorUnitario.Append(item.Produto.Id);
                    produtosSemValorUnitario.Append(" - ");
                    produtosSemValorUnitario.Append(item.Produto.DadosGerais.Descricao);

                    existeProdutoSemValorUnitario = true;
                }
            }

            mensagemComposta.ListaDeParametros.Add(produtosSemValorUnitario);

            return existeProdutoSemValorUnitario;
        }

        private bool PreVendaEstahFaturada()
        {
            var pedidoDeVenda = RetornePedidoDeVenda();

            return pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO;
        }

        private PedidoDeVenda RetornePedidoDeVenda()
        {
            if (_pedidoDeVenda == null)
            {
                var repositorio = FabricaDeRepositorios.Crie<IRepositorioPedidoDeVenda>();
                _pedidoDeVenda = repositorio.Consulte(ObjetoValidado.PedidoDeVendaId);
            }

            return _pedidoDeVenda;
        }

        #endregion
    }
}
