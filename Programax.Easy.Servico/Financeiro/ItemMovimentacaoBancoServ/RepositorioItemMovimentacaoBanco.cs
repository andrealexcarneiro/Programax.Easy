using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoBancoObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using System;
using System.Linq.Expressions;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.MovimentacaoBancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using NHibernate.Transform;

namespace Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ
{
    public class RepositorioItemMovimentacaoBanco : RepositorioBase<ItemMovimentacaoBanco>, IRepositorioItemMovimentacaoBanco
    {
        public RepositorioItemMovimentacaoBanco(ISession sessao)
            : base(sessao)
        {

        }

        public List<ItemMovimentacaoBanco> ConsulteListaItens(MovimentacaoBanco Movimento, DateTime? DataInicialPeriodo, DateTime? DataFinalPeriodo, EnumOrigemMovimentacaoBanco? Origem,
                                                              string Descricao, EnumTipoMovimentacaoBanco? Tipo, string NumeroDoc, Pessoa Parceiro,
                                                              CategoriaFinanceira Categoria, List<int> IdsMovimento)
        {
            Expression<Func<ItemMovimentacaoBanco, bool>> expressaoParaConsulta = itemBanco => itemBanco.Id >= 0;

            if (Movimento != null && IdsMovimento.Count == 0)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.MovimentacaoBanco.Id == Movimento.Id);
            }

            if (DataInicialPeriodo != null && DataInicialPeriodo.Value != DateTime.Parse("01/01/0001 00:00:00"))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.DataHoraLancamento >= DataInicialPeriodo.Value.Date);
            }

            if (DataFinalPeriodo != null && DataFinalPeriodo.Value != DateTime.Parse("01/01/0001 00:00:00"))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.DataHoraLancamento <= DataFinalPeriodo.Value.AddHours(23).AddMinutes(59));
            }

            if (Origem != EnumOrigemMovimentacaoBanco.TODOS)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.OrigemMovimentacaoBanco == Origem);
            }

            if (Descricao != string.Empty)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.DescricaoDaMovimentacao.IsLike("%" + Descricao + "%"));
            }

            if (Tipo != EnumTipoMovimentacaoBanco.TODAS)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.TipoMovimentacao == Tipo);
            }

            if (NumeroDoc != string.Empty)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.NumeroDocumentoOrigem.IsLike("%" + NumeroDoc + "%"));
            }

            if (Parceiro != null && Parceiro.Id != 0)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.Parceiro.Id == Parceiro.Id);
            }

            if (Categoria != null && Categoria.Id != 0)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(itemBanco => itemBanco.Categoria.Id == Categoria.Id);
            }

            Expression<Func<ItemMovimentacaoBanco, bool>> expressaoParaConsultaBancos = null;

            if(IdsMovimento != null)
                if (IdsMovimento.Count > 0)
                {
                    for (int i = 0; i < IdsMovimento.Count; i++)
                    {
                        int idPesquisa = IdsMovimento[i];

                        if (i == 0)
                            expressaoParaConsultaBancos = expressaoParaConsultaBancos.AndAlso(mov => mov.MovimentacaoBanco.Id == idPesquisa);
                        else
                            expressaoParaConsultaBancos = expressaoParaConsultaBancos.OrElse(mov => mov.MovimentacaoBanco.Id == idPesquisa);
                    }
                }

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaBancos);

            return _session.QueryOver<ItemMovimentacaoBanco>().Where(expressaoParaConsulta).List().OrderBy(item => item.DataHoraLancamento).ToList();
        }

        public List<ItemMovimentacaoBanco> ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoBanco origemMovimentacaoBanco, string numeroDocumentoOrigem)
        {
            return _session.QueryOver<ItemMovimentacaoBanco>()
                                        .Where(item => item.OrigemMovimentacaoBanco == origemMovimentacaoBanco &&
                                                                item.NumeroDocumentoOrigem == numeroDocumentoOrigem &&
                                                                item.EstahEstornado == false).List().ToList();
        }

        //Para calcular o Saldo Inicial no Relatório de Fluxo de Caixa
        public double ConsulteSaldoItensBancoMovimento(int MovimentoId, int CategoriaId, DateTime dataInicial, DateTime dataFinal)
        {
            //return _session.QueryOver<ItemMovimentacaoBanco>()
            //                           .Where(item => item.MovimentacaoBanco.Id == MovimentoId &&
            //                                                   item.Categoria.Id == CategoriaId).List().ToList();

            string sql = "SELECT sum(itembanco_valor) as Valor FROM movimentacoesbancositens " +
                            "WHERE itembanco_Categoria_id = " + CategoriaId +
                            " AND itembanco_movimentacao_banco = " + MovimentoId +
                            " AND ( itembanco_data_hora >= " + "'" + dataInicial.ToString("yyyy-MM-dd 00:00:00") + "'" +
                            " AND itembanco_data_hora <= " + "'" + dataFinal.ToString("yyyy-MM-dd 23:59:00") + "'" + ")";

            var query = _session.CreateSQLQuery(sql);

            query.AddScalar("Valor", NHibernateUtil.Double);
            
            //query.SetResultTransformer(Transformers.AliasToBean(typeof(Double)));

            var valor = query.List();

            return valor[0].ToDouble();

        }
        
        public List<ItemMovimentacaoBanco> consultelistaPagarReceber(int IdContaPagarReceber)
        {
            return _session.QueryOver<ItemMovimentacaoBanco>()
                                      .Where(item => item.ContaPagarReceber.Id == IdContaPagarReceber).List().ToList();
        }

        public ItemMovimentacaoBanco ConsulteItemConciliadoImportado(int IdItemConciliadoImportado)
        {
            return _session.QueryOver<ItemMovimentacaoBanco>()
                                      .Where(item => item.ConciliacaoImportacaoId == IdItemConciliadoImportado).SingleOrDefault();
        }

        public void ExcluaParcialOrigemPagarReceber(ContaPagarReceber Objeto, bool EhDentroManutencao=true, bool EhConciliacao=false, double ValorPagoEstornar = 0)
        {
            Expression<Func<ItemMovimentacaoBanco, bool>> expressaoParaConsulta = itemMov => itemMov.Id >= 0;

            if (Objeto != null)
            {
                var lista = consultelistaPagarReceber(Objeto.Id);

                if (lista != null && lista.Count() > 0)
                {
                    foreach (var itemLista in lista)
                    {
                        if (EhDentroManutencao)
                        {
                            if (ValorPagoEstornar == 0)
                            {
                                if (itemLista.ContaPagarReceber.Id == Objeto.Id && itemLista.Valor == Objeto.ValorPago)
                                    Exclua(itemLista);
                            }
                            else if (itemLista.ContaPagarReceber.Id == Objeto.Id && itemLista.Valor == ValorPagoEstornar)
                                    Exclua(itemLista);

                            //Neste caso, se houver taxa de administração deste mesmo registro será excluído também... 
                            if (itemLista.ContaPagarReceber.Id == Objeto.Id && itemLista.TipoMovimentacao == EnumTipoMovimentacaoBanco.SAIDA &&
                                itemLista.OrigemMovimentacaoBanco==EnumOrigemMovimentacaoBanco.CONTARECEBER && itemLista.Valor == Objeto.ValorPago)
                                Exclua(itemLista);
                        }
                        else
                        {
                            if(!EhConciliacao)
                            {
                                if (itemLista.ContaPagarReceber.Id == Objeto.Id && itemLista.Valor == Objeto.ValorTotal)
                                    Exclua(itemLista);
                            }                                
                            else
                            {
                                if (itemLista.ContaPagarReceber.Id == Objeto.Id)
                                    Exclua(itemLista);
                            }
                                
                            //Neste caso, se houver taxa de administração deste mesmo registro será excluído também... 
                            if (itemLista.ContaPagarReceber.Id == Objeto.Id && itemLista.TipoMovimentacao == EnumTipoMovimentacaoBanco.SAIDA &&
                                itemLista.OrigemMovimentacaoBanco == EnumOrigemMovimentacaoBanco.CONTARECEBER)
                                Exclua(itemLista);
                        }
                    }                   
                }
                
            }
        }


    }
}
