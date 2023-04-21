using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.Repositorio
{
    public interface IRepositorioItemMovimentacaoBanco : IRepositorioBase<ItemMovimentacaoBanco>
    {
        List<ItemMovimentacaoBanco> consultelistaPagarReceber(int IdContaPagarReceber);

        List<ItemMovimentacaoBanco> ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoBanco origemMovimentacaoBanco, string numeroDocumentoOrigem);

        List<ItemMovimentacaoBanco> ConsulteListaItens(MovimentacaoBanco Movimento, DateTime? DataInicialPeriodo, DateTime? DataFinalPeriodo, EnumOrigemMovimentacaoBanco? Origem, string Descricao,
                EnumTipoMovimentacaoBanco? Tipo, string NumeroDoc, Pessoa Parceiro, CategoriaFinanceira Categoria, List<int> IdsMovimento);

        void ExcluaParcialOrigemPagarReceber(ContaPagarReceber Objeto, bool EhDentroManutencao=true, bool EhConciliacao = false, double ValorPagoEstornar=0);

        ItemMovimentacaoBanco ConsulteItemConciliadoImportado(int IdItemConciliadoImportado);

        double ConsulteSaldoItensBancoMovimento(int MovimentoId, int CategoriaId, DateTime dataInicial, DateTime dataFinal);
    }
}
