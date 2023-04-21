using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;

namespace Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.Repositorio
{
    public interface IRepositorioMovimentacao : IRepositorioBase<MovimentacaoBase>
    {
        List<VwMovimentacaoProduto> ConsulteVwMovimentacoesProdutos(int? produtoId, DateTime? dataInicial, DateTime? dataFinal, Enumeradores.EnumTipoMovimentacao? tipoMovimentacao, Enumeradores.EnumOrigemMovimentacao? origemMovimentacao);

        List<VwMovimentacaoSaidaItens> ConsulteVwMovimentacoesSaidaItens(int? produtoId, DateTime? dataInicial, DateTime? dataFinal);

        List<ItemMovimentacao> ConsulteListaItensSaidaPorPedido(int numeroPedido);
        List<ItemMovimentacao> ConsulteListaItensSaidaPorPedidoEItem(int numeroPedido, int Item);
        List<BaixaItens> ConsulteListaItensBaixaPorPedido(int numeroPedido);
    }
}
