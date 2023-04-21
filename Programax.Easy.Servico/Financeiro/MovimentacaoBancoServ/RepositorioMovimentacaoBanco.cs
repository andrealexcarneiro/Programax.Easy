using System;
using System.Collections.Generic;
using System.Linq;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.Repositorio;
using NHibernate;
using NHibernate.Transform;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ
{
    public class RepositorioMovimentacaoBanco : RepositorioBase<MovimentacaoBanco>, IRepositorioMovimentacaoBanco
    {
        public RepositorioMovimentacaoBanco(ISession sessao)
            : base(sessao)
        {

        }

        public MovimentacaoBanco ConsulteBancoAberto(BancoParaMovimento banco)
        {
            return _session.QueryOver<MovimentacaoBanco>()
                             .TransformUsing(Transformers.DistinctRootEntity)
                             .Where(movimentacaoBanco => movimentacaoBanco.Status == EnumStatusMovimentacaoCaixa.ABERTO &&
                                                                           movimentacaoBanco.Banco.Id ==banco.Id )
                             .Take(1).SingleOrDefault();
        }
               

        public List<MovimentacaoBanco> ConsulteLista(BancoParaMovimento banco,
                                                                            EnumDataFiltrarMovimentacaoCaixa? dataFiltrarMovimentacaoBanco, 
                                                                            DateTime? dataInicial,
                                                                            DateTime? dataFinal, 
                                                                            EnumStatusMovimentacaoCaixa? statusMovimentacao)
        {
            Expression<Func<MovimentacaoBanco, bool>> expressaoParaConsulta = movimentacaoBanco => movimentacaoBanco.Id > 0;

            if (banco != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoBanco => movimentacaoBanco.Banco.Id == banco.Id);
            }

            if (dataFiltrarMovimentacaoBanco != null)
            { 
                if (dataFiltrarMovimentacaoBanco == EnumDataFiltrarMovimentacaoCaixa.DATAABERTURA)
                {
                    if (dataInicial != null)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoBanco => movimentacaoBanco.DataHoraAbertura >= dataInicial.GetValueOrDefault());
                    }

                    if (dataFinal != null)
                    {
                        dataFinal = dataFinal.Value.AddHours(23).AddMinutes(59);

                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoBanco => movimentacaoBanco.DataHoraAbertura <= dataFinal.GetValueOrDefault());
                    }
                }
                else
                {
                    if (dataInicial != null)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoBanco => movimentacaoBanco.DataHoraFechamento >= dataInicial.GetValueOrDefault());
                    }

                    if (dataFinal != null)
                    {
                        dataFinal = dataFinal.Value.AddHours(23).AddMinutes(59);

                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoBanco => movimentacaoBanco.DataHoraFechamento <= dataFinal.GetValueOrDefault());
                    }
                }
            }

            if (statusMovimentacao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(movimentacaoBanco => movimentacaoBanco.Status == statusMovimentacao);
            }
                        
            return _session.QueryOver<MovimentacaoBanco>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta)                                        
                                        .List<MovimentacaoBanco>().ToList();
        }

        //Para calcular o Saldo Inicial no Relatório de Fluxo de Caixa
        public List<int> ConsulteRegistrosDeMovimentoDoBanco(int bancoId, DateTime dataInicial, DateTime dataFinal)
        {
            string sql = "SELECT movbanco_id as MovimentoId FROM movimentacoesbanco " +
                            "WHERE movbanco_banco = " + bancoId +
                            " AND ( movbanco_data_hora_abertura >= " + "'" + dataInicial.ToString("yyyy-MM-dd 00:00:00") + "'" +
                            " AND movbanco_data_hora_abertura <= " + "'" + dataFinal.ToString("yyyy-MM-dd 23:59:00") + "'" + ")";

            var query = _session.CreateSQLQuery(sql);

            query.AddScalar("MovimentoId", NHibernateUtil.Int32);

            //query.SetResultTransformer(Transformers.AliasToBean(typeof(int)));

            var listaIdsMovimento = query.List<int>().ToList();

            return listaIdsMovimento;

        }

        public ItemMovimentacaoBanco ConsulteMovimentacaoNumeroItemBanco(DateTime? dataInicial, DateTime? dataFinal, int pedido, int categoria)
        {
            dataFinal = dataFinal.Value.AddHours(23).AddMinutes(59);
            
            return _session.QueryOver<ItemMovimentacaoBanco>()
                             .TransformUsing(Transformers.DistinctRootEntity)
                             .Where(movimentacaoItemBanco => movimentacaoItemBanco.DataHoraLancamento >= dataInicial &&
                                                             movimentacaoItemBanco.DataHoraLancamento <= dataFinal &&
                                                             movimentacaoItemBanco.NumeroDocumentoOrigem == pedido.ToString() &&
                                                             movimentacaoItemBanco.Categoria.Id == categoria &&
                                                             movimentacaoItemBanco.TipoMovimentacao == EnumTipoMovimentacaoBanco.ENTRADA &&
                                                             movimentacaoItemBanco.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.PEDIDODEVENDA
                                                             )
                             .Take(1).SingleOrDefault();
        }
    }
}
