using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using System;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ
{
    public class RepositorioItemMovimentacaoCaixa : RepositorioBase<ItemMovimentacaoCaixa>, IRepositorioItemMovimentacaoCaixa
    {
        public RepositorioItemMovimentacaoCaixa(ISession sessao)
            : base(sessao)
        {

        }

        public List<ItemMovimentacaoCaixa> ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoCaixa origemMovimentacaoCaixa, int? numeroDocumentoOrigem)
        {
            return _session.QueryOver<ItemMovimentacaoCaixa>()
                                        .Where(item => item.OrigemMovimentacaoCaixa == origemMovimentacaoCaixa &&
                                                                item.NumeroDocumentoOrigem == numeroDocumentoOrigem &&
                                                                item.EstahEstornado == false).List().ToList();
        }

        public List<ItemMovimentacaoCaixa> ConsulteSaldoItensCaixaMovimento(int movimentoId, int categoriaId)
        {
            return _session.QueryOver<ItemMovimentacaoCaixa>()
                                        .Where(item => item.MovimentacaoCaixa.Id == movimentoId &&
                                                                item.CategoriaFinaceira.Id == categoriaId &&
                                                                item.EstahEstornado==false).List().ToList();
        }

        public List<ItemMovimentacaoCaixa> ConsulteListaPorCategoriasEPagamentos(int categoriaId, int formaPagamentoId, 
                                                                                DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo, Pessoa pessoa)
        {

            Expression<Func<ItemMovimentacaoCaixa, bool>> expressaoParaConsulta = item => item.CategoriaFinaceira.Id == categoriaId &&
                                                                item.CategoriaFinaceira.Id != 24 &&
                                                                item.FormaPagamento.Id == formaPagamentoId &&
                                                                item.EstahEstornado == false &&
                                                                (item.DataHora >= dataInicialPeriodo.GetValueOrDefault() &&
                                                                item.DataHora <= dataFinalPeriodo.GetValueOrDefault().AddHours(23).AddMinutes(59));

            if (pessoa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(item => item.Parceiro.Id == pessoa.Id);
            }

            return _session.QueryOver<ItemMovimentacaoCaixa>().Where(expressaoParaConsulta).List().ToList();

        }
    }
}
