using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.Repositorio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Negocio.Vendas.TrocaPedidoDeVendaObj.ObjetoDeNegocio
{
    public class ValidacaoTrocaPedidoDeVenda : ValidacaoBase<TrocaPedidoDeVenda>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ValidacaoItemPedidoTrocaPedidoDeVenda _validacaoItemPedidoTrocaPedidoDeVenda;

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoTrocaPedidoDeVenda()
        {
            _validacaoItemPedidoTrocaPedidoDeVenda = new ValidacaoItemPedidoTrocaPedidoDeVenda();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override FluentValidation.Results.ValidationResult Validate(TrocaPedidoDeVenda instance)
        {
            _validacaoItemPedidoTrocaPedidoDeVenda.TrocaPedidoDeVenda = instance;
            _validacaoItemPedidoTrocaPedidoDeVenda.ValideInclusao();

            return base.Validate(instance);
        }

        public override void ValideInclusao()
        {
            AssineRegraPedidoObrigatorio();
            AssineRegraPrecisaDePeloMenosUmProdutoDevolvido();
            AssineRegraMotivoTrocaObrigatoria();
            AssineRegraItensPedido();
        }

        public override void ValideAtualizacao()
        {
            AssineRegraPedidoObrigatorio();
            AssineRegraPrecisaDePeloMenosUmProdutoDevolvido();
            AssineRegraMotivoTrocaObrigatoria();
            AssineRegraTrocaFaturadaCanceladaOuRecusadaNaoPodeHaverAlteracao();
            AssineRegraItensPedido();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraPedidoObrigatorio()
        {
            RuleFor(troca => troca.PedidoDeVenda)
               .Must(pedidoDeVenda => pedidoDeVenda != null && pedidoDeVenda.Id > 0)
               .WithMessage("Pedido de Venda não informado.");
        }

        private void AssineRegraPrecisaDePeloMenosUmProdutoDevolvido()
        {
            RuleFor(troca => troca.ListaItensPedido)
               .Must(listaItensPedido => ExistePeloMenosUmProdutoDevolvido())
               .WithMessage("É necessário informar ao menos 1 produto a ser devolvido.");
        }

        private void AssineRegraTrocaFaturadaCanceladaOuRecusadaNaoPodeHaverAlteracao()
        {
            RuleFor(troca => troca.PedidoDeVenda)
               .Must(pedidoDeVenda => !EstaTrocaEstahFaturadaCanceladaOuRecusada())
               .WithMessage("Essa Troca já foi finalizada, não há como efetuar alterações");
        }

        private void AssineRegraMotivoTrocaObrigatoria()
        {
            RuleFor(troca => troca.MotivoTroca)
               .Must(motivoTroca => motivoTroca != null)
               .WithMessage("Motivo da Troca não informada.");
        }

        private void AssineRegraItensPedido()
        {
            RuleFor(troca => troca.ListaItensPedido).SetValidator(_validacaoItemPedidoTrocaPedidoDeVenda);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private bool EstaTrocaEstahFaturadaCanceladaOuRecusada()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioTrocaPedidoDeVenda>();

            var trocaBase = repositorio.ConsulteSemSessao(ObjetoValidado.Id);

            return trocaBase.Status == EnumStatusTrocaPedidoDeVenda.CACNCELADO ||
                      trocaBase.Status == EnumStatusTrocaPedidoDeVenda.FATURADO ||
                      trocaBase.Status == EnumStatusTrocaPedidoDeVenda.RECUSADO;
        }

        private bool ExistePeloMenosUmProdutoDevolvido()
        {
            return ObjetoValidado.ListaItensPedido.ToList().Exists(item => item.QuantidadeTrocar > 0);
        }

        #endregion
    }
}
