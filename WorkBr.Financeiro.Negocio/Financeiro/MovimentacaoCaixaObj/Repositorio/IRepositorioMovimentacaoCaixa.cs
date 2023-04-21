using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio
{
    public interface IRepositorioMovimentacaoCaixa : IRepositorioBase<MovimentacaoCaixa>
    {
        MovimentacaoCaixa ConsulteCaixaAberto(Caixa caixa);

        VWSaldoAtualCaixa ConsulteSaldoAtualCaixa();

        List<MovimentacaoCaixa> ConsulteLista(Caixa caixa, EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoCaixa, DateTime? dataInicial, DateTime? dataFinal, EnumStatusMovimentacaoCaixa? statusMovimentacao);

        ItemMovimentacaoCaixa ConsulteMovimentacaoNumeroItemCaixa(DateTime? dataInicial, DateTime? dataFinal, int pedido, int formaPagamento);
    }
}
