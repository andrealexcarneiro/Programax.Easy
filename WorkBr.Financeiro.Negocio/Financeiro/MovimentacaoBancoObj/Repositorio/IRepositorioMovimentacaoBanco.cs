using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.Repositorio
{
    public interface IRepositorioMovimentacaoBanco : IRepositorioBase<MovimentacaoBanco>
    {
        MovimentacaoBanco ConsulteBancoAberto(BancoParaMovimento banco);

        List<MovimentacaoBanco> ConsulteLista(BancoParaMovimento banco, EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoCaixa, DateTime? dataInicial, DateTime? dataFinal, EnumStatusMovimentacaoCaixa? statusMovimentacao);

        ItemMovimentacaoBanco ConsulteMovimentacaoNumeroItemBanco(DateTime? dataInicial, DateTime? dataFinal, int pedido, int formaPagamento);

        List<int> ConsulteRegistrosDeMovimentoDoBanco(int bancoId, DateTime dataInicial, DateTime dataFinal);
    }
}
