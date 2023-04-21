using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.Repositorio;
using System;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Financeiro.ConciliacaoBancariaServ
{
    public class RepositorioConciliacaoBancaria : RepositorioBase<ConciliacaoBancaria>, IRepositorioConciliacaoBancaria
    {
        public RepositorioConciliacaoBancaria(ISession sessao)
            : base(sessao)
        {

        }

        public List<vw_fluxo_caixa> ConsulteFluxoCaixa(DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo)
        {
            Expression<Func<vw_fluxo_caixa, bool>> expressaoParaConsulta = concilicao => concilicao.Id2 > 0;

            if (dataInicialPeriodo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(fluxo => fluxo.DATAREALIZADO.Date >= dataInicialPeriodo.GetValueOrDefault().Date);
            }

            if (dataFinalPeriodo != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(fluxo => fluxo.DATAREALIZADO.Date <= dataFinalPeriodo.GetValueOrDefault().Date);
            }

            return _session.QueryOver<vw_fluxo_caixa>()
                               .Where(expressaoParaConsulta).List().ToList();

        }

        public List<ConciliacaoBancaria> ConsulteLista(EnumOrigemMovimentacaoBanco? Status, EnumDataFiltrarConciliacaoBancaria?
                                                       DataFiltrar, DateTime? dataInicialPeriodo, DateTime? dataFinalPeriodo, int? movimentacaoId)
        {
            Expression<Func<ConciliacaoBancaria, bool>> expressaoParaConsulta = concilicao => concilicao.Id > 0;

            if (Status != null && Status != EnumOrigemMovimentacaoBanco.TODOS)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(concilicao => concilicao.StatusConciliacao == Status);
            }
            
            if (DataFiltrar != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (DataFiltrar == EnumDataFiltrarConciliacaoBancaria.DATADOLANCTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(concilicao => concilicao.DataLancto >= dataInicialPeriodo.GetValueOrDefault());
                    }

                    if (DataFiltrar == EnumDataFiltrarConciliacaoBancaria.DATAVENCIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(concilicao => concilicao.DataVencimento >= dataInicialPeriodo.GetValueOrDefault());
                    }
                }

                if (dataFinalPeriodo != null)
                {
                    if (DataFiltrar == EnumDataFiltrarConciliacaoBancaria.DATADOLANCTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(concilicao => concilicao.DataLancto <= dataFinalPeriodo.GetValueOrDefault());
                    }

                    if (DataFiltrar == EnumDataFiltrarConciliacaoBancaria.DATAVENCIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(concilicao => concilicao.DataVencimento <= dataFinalPeriodo.GetValueOrDefault());
                    }
                }
            }

            if(movimentacaoId !=null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(concilicao => concilicao.MovimentacaoBanco.Id == movimentacaoId);
            }

            return _session.QueryOver<ConciliacaoBancaria>()
                 .TransformUsing(Transformers.DistinctRootEntity)
                 .Where(expressaoParaConsulta).List().ToList();
        }
    }
}
