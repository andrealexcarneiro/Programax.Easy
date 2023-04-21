using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using NHibernate.Transform;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ
{
    public class RepositorioMovimentacaoCaixa : RepositorioBase<MovimentacaoCaixa>, IRepositorioMovimentacaoCaixa
    {
        public RepositorioMovimentacaoCaixa(ISession sessao)
            : base(sessao)
        {

        }

        public MovimentacaoCaixa ConsulteCaixaAberto(Caixa caixa)
        {
            return _session.QueryOver<MovimentacaoCaixa>()
                             .TransformUsing(Transformers.DistinctRootEntity)
                             .Where(movimentacaoCaixa => movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.ABERTO &&
                                                                           movimentacaoCaixa.Caixa.Id ==caixa.Id )
                             .Take(1).SingleOrDefault();
        }

        public VWSaldoAtualCaixa ConsulteSaldoAtualCaixa()
        {
            return _session.QueryOver<VWSaldoAtualCaixa>().Take(1).SingleOrDefault();
        }

        public List<MovimentacaoCaixa> ConsulteLista(Caixa caixa,
                                                                            EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoCaixa, 
                                                                            DateTime? dataInicial,
                                                                            DateTime? dataFinal, 
                                                                            EnumStatusMovimentacaoCaixa? statusMovimentacao)
        {
            Expression<Func<MovimentacaoCaixa, bool>> expressaoParaConsulta = movimentacaoCaixa => movimentacaoCaixa.Id > 0;

            if (caixa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoCaixa => movimentacaoCaixa.Caixa.Id == caixa.Id);
            }

            if (dataFiltrarMovimentacaoCaixa != null)
            { 
                if (dataFiltrarMovimentacaoCaixa == EnumDataFiltrarMovimentacaoCaixa.DATAABERTURA)
                {
                    if (dataInicial != null)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoCaixa => movimentacaoCaixa.DataHoraAbertura >= dataInicial.GetValueOrDefault());
                    }

                    if (dataFinal != null)
                    {
                        dataFinal = dataFinal.Value.AddHours(23).AddMinutes(59);

                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoCaixa => movimentacaoCaixa.DataHoraAbertura <= dataFinal.GetValueOrDefault());
                    }
                }
                else
                {
                    if (dataInicial != null)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoCaixa => movimentacaoCaixa.DataHoraFechamento >= dataInicial.GetValueOrDefault());
                    }

                    if (dataFinal != null)
                    {
                        dataFinal = dataFinal.Value.AddHours(23).AddMinutes(59);

                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoCaixa => movimentacaoCaixa.DataHoraFechamento <= dataFinal.GetValueOrDefault());
                    }
                }
            }

            if (statusMovimentacao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoCaixa => movimentacaoCaixa.Status == statusMovimentacao);
            }
                        
            return _session.QueryOver<MovimentacaoCaixa>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta)                                        
                                        .List<MovimentacaoCaixa>().ToList();
        }

        public ItemMovimentacaoCaixa ConsulteMovimentacaoNumeroItemCaixa(DateTime? dataInicial, DateTime? dataFinal, int pedido, int formaPagamento)
        {
            dataFinal = dataFinal.Value.AddHours(23).AddMinutes(59);

            return _session.QueryOver<ItemMovimentacaoCaixa>()
                             .TransformUsing(Transformers.DistinctRootEntity)
                             .Where(movimentacaoItemCaixa => movimentacaoItemCaixa.DataHora >= dataInicial &&
                                                             movimentacaoItemCaixa.DataHora <= dataFinal &&
                                                             movimentacaoItemCaixa.NumeroDocumentoOrigem == pedido &&
                                                             movimentacaoItemCaixa.FormaPagamento.Id == formaPagamento &&
                                                             movimentacaoItemCaixa.TipoMovimentacao == EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO &&
                                                             movimentacaoItemCaixa.OrigemMovimentacaoCaixa == EnumOrigemMovimentacaoCaixa.PEDIDODEVENDA &&
                                                             movimentacaoItemCaixa.EstahEstornado == false 
                                                             )
                             .Take(1).SingleOrDefault();
        }
    }
}
