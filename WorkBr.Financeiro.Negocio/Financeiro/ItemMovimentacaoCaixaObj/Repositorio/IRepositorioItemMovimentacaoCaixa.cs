using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;

namespace Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio
{
    public interface IRepositorioItemMovimentacaoCaixa : IRepositorioBase<ItemMovimentacaoCaixa>
    {
        List<ItemMovimentacaoCaixa> ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoCaixa origemMovimentacaoCaixa, int? numeroDocumentoOrigem);

        List<ItemMovimentacaoCaixa> ConsulteSaldoItensCaixaMovimento(int movimentoId, int categoriaId);

        List<ItemMovimentacaoCaixa> ConsulteListaPorCategoriasEPagamentos(int categoriaId, int formaPagamentoId, DateTime?
                                                                            dataInicialPeriodo, DateTime? dataFinalPeriodo, Pessoa pessoa);

    }
}
