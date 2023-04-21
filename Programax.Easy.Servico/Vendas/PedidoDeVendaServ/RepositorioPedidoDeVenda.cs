using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Cadastros.Enumeradores;

namespace Programax.Easy.Servico.Vendas.PedidoDeVendaServ
{
    public class RepositorioPedidoDeVenda : RepositorioBase<PedidoDeVenda>, IRepositorioPedidoDeVenda
    {
        public RepositorioPedidoDeVenda(ISession sessao)
            : base(sessao)
        {

        }

        public PedidoDeVenda ConsulteMaiorVenda(Pessoa cliente)
        {
            return _session.QueryOver<PedidoDeVenda>().Where(pedido => pedido.Cliente.Id == cliente.Id &&
                                                                                                           pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO)
                                                                                                           .OrderBy(x => x.ValorTotal).Desc.Take(1).SingleOrDefault();
        }

        public PedidoDeVenda ConsultePedidoParaRoteiro(int idPedido)
        {
            return _session.QueryOver<PedidoDeVenda>().Where(pedido => pedido.Id == idPedido && pedido.StatusRoteiro == null)
                                                       .Take(1).SingleOrDefault();
        }

        public List<PedidoDeVenda> ConsulteLista(DateTime? dataInicial, DateTime? dataFinal, Pessoa atendente, Pessoa vendedor)
        {
            Expression<Func<PedidoDeVenda, bool>> expressaoParaConsulta = pedido => pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO;

            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.DataElaboracao >= dataInicial.Value);
            }

            if (dataFinal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.DataElaboracao <= dataFinal.Value);
            }

            if (atendente != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.Atendente.Id == atendente.Id);
            }

            if (vendedor != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.Vendedor.Id == vendedor.Id);
            }
           
            return _session.QueryOver<PedidoDeVenda>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta).List().ToList();
        }

        public List<PedidoDeVenda> ConsulteListaParaRoteiro(DateTime? dataInicial, DateTime? dataFinal)
        {
            Expression<Func<PedidoDeVenda, bool>> expressaoParaConsulta = pedido => pedido.StatusRoteiro == null;

            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.DataElaboracao >= dataInicial.Value);
            }

            if (dataFinal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.DataElaboracao <= dataFinal.Value);
            }

            return _session.QueryOver<PedidoDeVenda>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta).List().ToList();
        }

        public List<PedidoDeVenda> ConsulteLista(DateTime? dataInicial,
                                                                      DateTime? dataFinal,
                                                                      Pessoa atendente,
                                                                      Pessoa vendedor,
                                                                      Pessoa cliente,
                                                                      EnumTipoPedidoDeVenda? tipoPedidoDeVenda,
                                                                      EnumStatusPedidoDeVenda? statusPedidoDeVenda, 
                                                                      Pessoa usuario = null, bool EReservado=false)
        {
            Expression<Func<PedidoDeVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            if (dataInicial != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.DataElaboracao >= dataInicial.Value);
            }

            if (dataFinal != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.DataElaboracao <= dataFinal.Value);
            }

            if (atendente != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.Atendente.Id == atendente.Id);
            }

            if (vendedor != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.Vendedor.Id == vendedor.Id);
            }

            if (cliente != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.Cliente.Id == cliente.Id);
            }

            if (tipoPedidoDeVenda != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.TipoPedidoVenda == tipoPedidoDeVenda.Value);
            }

            if (statusPedidoDeVenda != null)
            {
                if (!EReservado)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.StatusPedidoVenda == statusPedidoDeVenda.Value);
                }
                else
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.StatusPedidoVenda == statusPedidoDeVenda.Value ||
                    pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.RESERVADO);
                }
            }

            if (usuario != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(pedido => pedido.Vendedor.Id == usuario.Id);
            }

            return _session.QueryOver<PedidoDeVenda>()
                                        .TransformUsing(Transformers.DistinctRootEntity)
                                        .Where(expressaoParaConsulta).List().ToList();
        }
        
        public List<VWVenda> ConsulteListaVWVenda(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                                                           List<Pessoa> parceiroPesquisa,
                                                                           List<Pessoa> parceiroPesquisaII,
                                                                           bool periodoFaturamento,
                                                                           DateTime dataInicialPeriodo ,
                                                                           DateTime dataFinalPeriodo,
                                                                           bool statusAberto,
                                                                           bool statusOrcamento,
                                                                           bool statusCancelado,
                                                                           bool statusEmLiberacao,
                                                                           bool statusRecusado,
                                                                           bool statusReservado,
                                                                           bool statusFaturado,
                                                                           bool statusEmitidoNFe,
                                                                           bool statusNaoPago,
                                                                           bool statusSemRecebimento,
                                                                           EnumOrdenacaoPesquisaVwVendas ordenacao
                                                                           )
        {
            Expression<Func<VWVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            //string sqlWhere = "VENDA_ID > " + "'" + 0 + "'";

            if (periodoFaturamento)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataFechamento >= dataInicialPeriodo &&
                vwvenda.DataFechamento <= dataFinalPeriodo.AddHours(23).AddMinutes(59));

                //sqlWhere = sqlWhere + " AND VENDA_DATA_FECHAMENTO >= " + "'" + dataInicialPeriodo.ToString("yyyy-MM-dd 00:00:00") + "'" 
                //                    + " AND VENDA_DATA_FECHAMENTO <= " + "'" + dataFinalPeriodo.ToString("yyyy-MM-dd 00:00:00") + "'";

            }
            else
            {
               
               

                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataElaboracao  >= dataInicialPeriodo &&
                vwvenda.DataElaboracao <=   dataFinalPeriodo.AddHours(23).AddMinutes(59));

                //sqlWhere = sqlWhere + " AND VENDA_DATA_ELABORACAO >= " + "'" + dataInicialPeriodo.ToString("yyyy-MM-dd 00:00:00") + "'"
                //                    + " AND VENDA_DATA_ELABORACAO <= " + "'" + dataFinalPeriodo.ToString("yyyy-MM-dd 00:00:00") + "'";

            }

            Expression<Func<VWVenda, bool>> expressaoParaConsultaParceiro = null;

            string sqlWhereParceiro = "AND (";

            if (parceiroPesquisaII.Count != 0)
            {
                for (int j = 0; j < parceiroPesquisaII.Count; j++)
                {
                    int idPesquisaII = parceiroPesquisaII[j].Id;
                     if (pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADORVENDEDOR)
                    {
                        if (idPesquisaII == 0)
                        {
                            if (j == 0)
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.IndicadorNome == null);
                            }
                            else
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.IndicadorNome == null);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {

                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.IndicadorId == idPesquisaII);
                            }
                            else
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.IndicadorId == idPesquisaII);
                            }
                        }
                    }

                }
            }
            
            sqlWhereParceiro = "AND (";
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaParceiro);

            Expression<Func<VWVenda, bool>> expressaoParaConsultaStatus = null;

            if (parceiroPesquisa.Count != 0)
                {
                    for (int i = 0; i < parceiroPesquisa.Count; i++)
                    {
                        int idPesquisa = parceiroPesquisa[i].Id;

                        if (pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                        {
                            if (i == 0)
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.IndicadorId == idPesquisa);

                                //sqlWhereParceiro = sqlWhereParceiro + " VENDA_INDICADOR_ID = " + "'" + idPesquisa + "'";

                            }
                            else
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.IndicadorId == idPesquisa);

                                //sqlWhereParceiro = sqlWhereParceiro + " OR VENDA_INDICADOR_ID = " + "'" + idPesquisa + "'";
                            }
                        }
                        else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                        {
                            if (i == 0)
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.AtendenteId == idPesquisa);

                                //sqlWhereParceiro = sqlWhereParceiro + " VENDA_ATENDENTE_ID = " + "'" + idPesquisa + "'";
                            }
                            else
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.AtendenteId == idPesquisa);

                                //sqlWhereParceiro = sqlWhereParceiro + " OR VENDA_ATENDENTE_ID = " + "'" + idPesquisa + "'";
                            }
                        }
                        else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                        {
                            if (idPesquisa == 0)
                            {
                                if (i == 0)
                                {
                                    expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.VendedorNome == null);

                                    //sqlWhereParceiro = sqlWhereParceiro + " VENDA_VENDEDOR_NOME is null";
                                }
                                else
                                {
                                    expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.VendedorNome == null);

                                    //sqlWhereParceiro = sqlWhereParceiro + " OR VENDA_VENDEDOR_NOME is null";
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.VendedorId == idPesquisa);

                                    //sqlWhereParceiro = sqlWhereParceiro + " VENDA_VENDEDOR_ID = " + "'" + idPesquisa + "'";
                                }
                                else
                                {
                                    expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.VendedorId == idPesquisa);

                                    //sqlWhereParceiro = sqlWhereParceiro + " OR VENDA_VENDEDOR_ID = " + "'" + idPesquisa + "'";
                                }
                            }
                        }


                        else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADORVENDEDOR)
                        {
                            if (idPesquisa == 0)
                            {
                                if (i == 0)
                                {
                                    expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.VendedorNome == null);

                                    //sqlWhereParceiro = sqlWhereParceiro + " VENDA_VENDEDOR_NOME is null";
                                }
                                else
                                {
                                    expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.VendedorNome == null);

                                    //sqlWhereParceiro = sqlWhereParceiro + " OR VENDA_VENDEDOR_NOME is null";
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {

                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.VendedorId == idPesquisa);
                                                                                                                        

                                    //sqlWhereParceiro = sqlWhereParceiro + " VENDA_VENDEDOR_ID = " + "'" + idPesquisa + "'";
                                }
                                else
                                {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.VendedorId == idPesquisa);
                                                                                                       

                                    //sqlWhereParceiro = sqlWhereParceiro + " OR VENDA_VENDEDOR_ID = " + "'" + idPesquisa + "'";
                                }
                            }
                        }


                        else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                        {
                            if (i == 0)
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.SupervisorId == idPesquisa);

                                //sqlWhereParceiro = sqlWhereParceiro + " SUPERVISOR_ID = " + "'" + idPesquisa + "'";
                            }
                            else
                            {
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.SupervisorId == idPesquisa);

                                //sqlWhereParceiro = sqlWhereParceiro + " OR SUPERVISOR_ID = " + "'" + idPesquisa + "'";
                            }
                        }
                    }
                }

                else
                {
                    //sqlWhereParceiro = "";
                }
              
            //if(sqlWhereParceiro != String.Empty)
            //sqlWhereParceiro = sqlWhereParceiro + ")" ;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaParceiro);

            //Expression<Func<VWVenda, bool>> expressaoParaConsultaStatus = null;

            //string sqlWhereStatus = " AND (";

            if (statusAberto)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.ABERTO);

                //sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.ABERTO.GetHashCode() + "'";
            }
            if (statusOrcamento)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.ORCAMENTO);

                //if(statusAberto)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.ORCAMENTO.GetHashCode() + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.ORCAMENTO.GetHashCode() + "'";
            }
            if (statusCancelado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.CANCELADO);

                //if (statusAberto || statusOrcamento)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.CANCELADO.GetHashCode() + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.CANCELADO.GetHashCode() + "'";
            }
            if (statusEmLiberacao)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.EMLIBERACAO);

                //if (statusAberto || statusOrcamento || statusCancelado)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.EMLIBERACAO.GetHashCode() + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.EMLIBERACAO.GetHashCode() + "'";
            }
            if (statusRecusado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.RECUSADO);

                //if (statusAberto || statusOrcamento || statusCancelado || statusEmLiberacao)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.RECUSADO.GetHashCode() + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.RECUSADO.GetHashCode() + "'";
            }
            if (statusReservado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.RESERVADO);

                //if (statusAberto || statusOrcamento || statusCancelado || statusEmLiberacao || statusRecusado)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.RESERVADO.GetHashCode() + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.RESERVADO.GetHashCode() + "'";
            }
            if (statusFaturado)
            {
                if (!statusSemRecebimento)
                {
                    expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.FATURADO);

                    //if (statusAberto || statusOrcamento || statusCancelado || statusEmLiberacao || statusRecusado || statusReservado)
                    //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.FATURADO.GetHashCode() + "'";
                    //else
                    //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.FATURADO.GetHashCode() + "'";
                }
                else
                {
                    expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.PedidoEstahPago == true
                    && vwvenda.Status != EnumStatusPedidoDeVenda.CANCELADO);

                    //if (statusAberto || statusOrcamento || statusCancelado || statusEmLiberacao || statusRecusado || statusReservado)
                    //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS != " + "'" + EnumStatusPedidoDeVenda.CANCELADO.GetHashCode() + "'" + " AND VENDA_ESTAH_PAGO = " + "'" + 1 + "'";
                    //else
                    //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS != " + "'" + EnumStatusPedidoDeVenda.CANCELADO.GetHashCode() + "'" + " AND VENDA_ESTAH_PAGO = " + "'" + 1 + "'";
                }                   
            }
            if (statusEmitidoNFe)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.EMITIDONFE);

                //if (statusAberto || statusOrcamento || statusCancelado || statusEmLiberacao || statusRecusado || statusReservado || statusFaturado)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.EMITIDONFE.GetHashCode() + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS = " + "'" + EnumStatusPedidoDeVenda.EMITIDONFE.GetHashCode() + "'";
            }
            if (statusNaoPago)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.PedidoEstahPago == false
                 && vwvenda.Status != EnumStatusPedidoDeVenda.CANCELADO);

                //if (statusAberto || statusOrcamento || statusCancelado || statusEmLiberacao || statusRecusado || statusReservado || statusFaturado || statusEmitidoNFe)
                //    sqlWhereStatus = sqlWhereStatus + " OR VENDA_STATUS != " + "'" + EnumStatusPedidoDeVenda.CANCELADO.GetHashCode() + "'" + " AND VENDA_ESTAH_PAGO = " + "'" + 0 + "'";
                //else
                //    sqlWhereStatus = sqlWhereStatus + " VENDA_STATUS != " + "'" + EnumStatusPedidoDeVenda.CANCELADO.GetHashCode() + "'" + " AND VENDA_ESTAH_PAGO = " + "'" + 0 + "'";
            }
            
             
            

            //sqlWhereStatus = sqlWhereStatus + ")";

            //if (!statusAberto && !statusOrcamento && !statusCancelado && !statusEmLiberacao && !statusRecusado && !statusReservado && !statusFaturado && !statusEmitidoNFe && !statusNaoPago)
            //sqlWhereStatus = "";

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaStatus);

            var query = _session.QueryOver<VWVenda>().Where(expressaoParaConsulta);

            //string ORDERBY = "";

            if (ordenacao == EnumOrdenacaoPesquisaVwVendas.CODIGO)
            {
                query.OrderBy(x => x.Id).Asc();

                //ORDERBY = ORDERBY + " ORDER BY VENDA_ID ASC";
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.DATAEMISSAO)
            {
                query.OrderBy(x => x.DataElaboracao).Asc();

                //ORDERBY = ORDERBY + " ORDER BY VENDA_DATA_ELABORACAO ASC";
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.VALORTOTAL)
            {
                query.OrderBy(x => x.ValorTotal).Asc();

                //ORDERBY = ORDERBY + " ORDER BY VENDA_VALOR_TOTAL ASC";
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.VENDEDOR)
            {
                query.OrderBy(x => x.VendedorNome).Asc();

                //ORDERBY = ORDERBY + " ORDER BY VENDA_VALOR_TOTAL ASC";
            }

            //var sql = " SELECT VENDA_ID AS Id, VENDA_CLIENTE_ID AS ClienteId, VENDA_CLIENTE_NOME AS ClienteNome, VENDA_CLIENTE_CPF_CNPJ AS ClienteCpfCnpj, " +
            //   "VENDA_TIPO_CLIENTE AS TipoCliente, VENDA_CIDADE AS Cidade, VENDA_UF AS UF, VENDA_INDICADOR_ID AS IndicadorId, " +
            //   "VENDA_INDICADOR_NOME AS  IndicadorNome, VENDA_ATENDENTE_ID AS AtendenteId,  VENDA_ATENDENTE_NOME AS AtendenteNome, " +
            //   "VENDA_VENDEDOR_ID AS VendedorId, VENDA_VENDEDOR_NOME AS VendedorNome, VENDA_SUPERVISOR_ID AS SupervisorId, " +
            //   "VENDA_SUPERVISOR_NOME AS SupervisorNome, VENDA_TIPO_PEDIDO AS TipoPedidoVenda, VENDA_STATUS AS  Status, " +
            //   "VENDA_DATA_ELABORACAO AS DataElaboracao, VENDA_DATA_FECHAMENTO AS DataFechamento, VENDA_FORMA_PAGAMENTO AS FormaPagamentoNome, " +
            //   "VENDA_CONDICAO_PAGAMENTO AS  CondicaoPagamentoNome, VENDA_VALOR_TOTAL AS ValorTotal, VENDA_JAH_EXPORTADA_PDV_ECF AS VendaJahExportadaPdvEcf, " +
            //   "VENDA_ESTAH_PAGO AS PedidoEstahPago, VENDA_COMISSAO_INDICADOR AS ComissaoIndicador, VENDA_COMISSAO_ATENDENTE AS ComissaoAtendente, " +
            //   "VENDA_COMISSAO_VENDEDOR AS ComissaoVendedor, VENDA_COMISSAO_SUPERVISOR AS ComissaoSupervisor" +

            //   " FROM  VW_VENDAS " +
            //   " WHERE " + sqlWhere + sqlWhereParceiro + sqlWhereStatus + ORDERBY;

            //var query = _session.CreateSQLQuery(sql);

            //query.AddScalar("Id", NHibernateUtil.Int32);
            //query.AddScalar("ClienteId", NHibernateUtil.Int32);
            //query.AddScalar("ClienteNome", NHibernateUtil.String);
            //query.AddScalar("ClienteCpfCnpj", NHibernateUtil.String);
            //query.AddScalar("TipoCliente", NHibernateUtil.Int32);
            //query.AddScalar("Cidade", NHibernateUtil.String);
            //query.AddScalar("UF", NHibernateUtil.String);
            //query.AddScalar("IndicadorId", NHibernateUtil.Int32);
            //query.AddScalar("IndicadorNome", NHibernateUtil.String);
            //query.AddScalar("AtendenteId", NHibernateUtil.Int32);
            //query.AddScalar("AtendenteNome", NHibernateUtil.String);
            //query.AddScalar("VendedorId", NHibernateUtil.Int32);
            //query.AddScalar("VendedorNome", NHibernateUtil.String);
            //query.AddScalar("SupervisorId", NHibernateUtil.Int32);
            //query.AddScalar("SupervisorNome", NHibernateUtil.String);
            //query.AddScalar("TipoPedidoVenda", NHibernateUtil.Int32);
            //query.AddScalar("Status", NHibernateUtil.Int32);
            //query.AddScalar("DataElaboracao", NHibernateUtil.DateTime);
            //query.AddScalar("DataFechamento", NHibernateUtil.DateTime);
            //query.AddScalar("FormaPagamentoNome", NHibernateUtil.String);
            //query.AddScalar("CondicaoPagamentoNome", NHibernateUtil.String);

            //query.AddScalar("ValorTotal", NHibernateUtil.Double);
            //query.AddScalar("ComissaoIndicador", NHibernateUtil.Double);
            //query.AddScalar("ComissaoAtendente", NHibernateUtil.Double);
            //query.AddScalar("ComissaoVendedor", NHibernateUtil.Double);
            //query.AddScalar("ComissaoSupervisor", NHibernateUtil.Double);

            //query.AddScalar("VendaJahExportadaPdvEcf", NHibernateUtil.Boolean);
            //query.AddScalar("PedidoEstahPago", NHibernateUtil.Boolean);

            //query.SetResultTransformer(Transformers.AliasToBean(typeof(VWVenda)));

            //var list = query.List<VWVenda>().ToList();

            //return list.ToList();

            return query.List().ToList();
        }
        
        public List<VWVendaTransportes> ConsulteListaVWVendaTransportes(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                                                           Pessoa parceiroPesquisa,
                                                                           bool periodoFaturamento,
                                                                           DateTime dataInicialPeriodo,
                                                                           DateTime dataFinalPeriodo,
                                                                           bool statusAberto,
                                                                           bool statusOrcamento,
                                                                           bool statusCancelado,
                                                                           bool statusEmLiberacao,
                                                                           bool statusRecusado,
                                                                           bool statusReservado,
                                                                           bool statusFaturado,
                                                                           bool statusEmitidoNFe,
                                                                           EnumOrdenacaoPesquisaVwVendas ordenacao)
        {
            Expression<Func<VWVendaTransportes, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            if (periodoFaturamento)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataEntrega >= dataInicialPeriodo &&
                                                                                                   vwvenda.DataEntrega <= dataFinalPeriodo);
            }
            else
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataEntrega >= dataInicialPeriodo &&
                                                                                                   vwvenda.DataEntrega <= dataFinalPeriodo);
            }

            if (parceiroPesquisa != null)
            {
                if (pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.IndicadorId == parceiroPesquisa.Id);
                }
                else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.AtendenteId == parceiroPesquisa.Id);
                }
                else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.VendedorId == parceiroPesquisa.Id);
                }
                else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.SupervisorId == parceiroPesquisa.Id);
                }
            }

            Expression<Func<VWVendaTransportes, bool>> expressaoParaConsultaStatus = null;

            if (statusAberto)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.ABERTO);
            }
            if (statusOrcamento)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.ORCAMENTO);
            }
            if (statusCancelado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.CANCELADO);
            }
            if (statusEmLiberacao)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.EMLIBERACAO);
            }
            if (statusRecusado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.RECUSADO);
            }
            if (statusReservado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.RESERVADO);
            }
            if (statusFaturado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.FATURADO);
            }
            if (statusEmitidoNFe)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.EMITIDONFE);
            }

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaStatus);

            var query = _session.QueryOver<VWVendaTransportes>().Where(expressaoParaConsulta);

            if (ordenacao == EnumOrdenacaoPesquisaVwVendas.CODIGO)
            {
                query.OrderBy(x => x.Id).Asc();
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.DATAEMISSAO)
            {
                query.OrderBy(x => x.DataEntrega).Asc();
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.VALORTOTAL)
            {
                query.OrderBy(x => x.ValorTotal).Asc();
            }

            return query.List().ToList();
        }

        public List<VWVenda> ConsulteListaVWVendasPorCliente(Pessoa cliente,
                                                                                           EnumTipoPessoa? tipoPessoaFisicaOuJuridica,
                                                                                           EnumTipoPedidoDeVenda? tipoPedidoDeVenda,
                                                                                           bool periodoFaturamento,
                                                                                           DateTime dataInicialPeriodo,
                                                                                           DateTime dataFinalPeriodo,
                                                                                           bool statusAberto,
                                                                                           bool statusOrcamento,
                                                                                           bool statusCancelado,
                                                                                           bool statusEmLiberacao,
                                                                                           bool statusRecusado,
                                                                                           bool statusReservado,
                                                                                           bool statusFaturado,
                                                                                           bool statusEmitidoNFe,                                                                                           
                                                                                           EnumOrdenacaoPesquisaVwVendas ordenacao,
                                                                                           string formaPagamento = null)
        {
            Expression<Func<VWVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            if (cliente != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.ClienteId == cliente.Id);
            }

            if (tipoPessoaFisicaOuJuridica != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.TipoCliente == tipoPessoaFisicaOuJuridica);
            }

            if (tipoPedidoDeVenda != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.TipoPedidoVenda == tipoPedidoDeVenda);
            }

            if (periodoFaturamento)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataFechamento != null &&
                                                                                                   vwvenda.DataFechamento >= dataInicialPeriodo &&
                                                                                                   vwvenda.DataFechamento <= dataFinalPeriodo);
            }
            else
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataElaboracao >= dataInicialPeriodo &&
                                                                                                   vwvenda.DataElaboracao <= dataFinalPeriodo);
            }

            if (formaPagamento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.FormaPagamentoNome == formaPagamento);
            }

            Expression<Func<VWVenda, bool>> expressaoParaConsultaStatus = null;

            if (statusAberto)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status != EnumStatusPedidoDeVenda.CANCELADO &&
                vwvenda.PedidoEstahPago == false);
            }
            if (statusOrcamento)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.ORCAMENTO);
            }
            if (statusCancelado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.CANCELADO);
            }
            if (statusEmLiberacao)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.EMLIBERACAO);
            }
            if (statusRecusado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.RECUSADO);
            }
            if (statusReservado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.RESERVADO);
            }
            if (statusFaturado)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.FATURADO);
            }
            if (statusEmitidoNFe)
            {
                expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.Status == EnumStatusPedidoDeVenda.EMITIDONFE);
            }

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaStatus);

            var query = _session.QueryOver<VWVenda>().Where(expressaoParaConsulta);

            if (ordenacao == EnumOrdenacaoPesquisaVwVendas.CODIGO)
            {
                query.OrderBy(x => x.Id).Asc();
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.DATAEMISSAO)
            {
                query.OrderBy(x => x.DataElaboracao).Asc();
            }
            else if (ordenacao == EnumOrdenacaoPesquisaVwVendas.VALORTOTAL)
            {
                query.OrderBy(x => x.ValorTotal).Asc();
            }
                        
            return query.List().ToList();
        }

        public List<VWVenda> ConsulteListaVWVendas(int? numeroDocumento,
                                                                               bool periodoFaturamento,
                                                                               DateTime dataInicial,
                                                                               DateTime dataFinal,
                                                                               EnumStatusPedidoDeVenda? statusPedidoDeVenda,
                                                                               bool? vendaJahExportada)
        {
            Expression<Func<VWVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            if (numeroDocumento != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.Id == numeroDocumento);
            }
            else
            {
                if (periodoFaturamento)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataFechamento != null &&
                                                                                                       vwvenda.DataFechamento.GetValueOrDefault() >= dataInicial &&
                                                                                                       vwvenda.DataFechamento.GetValueOrDefault() <= dataFinal);
                }
                else
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataElaboracao >= dataInicial &&
                                                                                                       vwvenda.DataElaboracao <= dataFinal);
                }

                if (statusPedidoDeVenda != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.Status == statusPedidoDeVenda);
                }

                if (vendaJahExportada != null)
                {
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.VendaJahExportadaPdvEcf == vendaJahExportada.Value);
                }
            }

            var query = _session.QueryOver<VWVenda>().Where(expressaoParaConsulta);

            return query.List().ToList();
        }

        public PedidoDeVenda ConsultePedidoFaturadoOuEmitidoNfe(int pedidoId, bool EhReservado=false)
        {
            if (EhReservado)
            {
                return _session.QueryOver<PedidoDeVenda>()
                                        .Where(pedido => pedido.Id == pedidoId &&
                                                                    (pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO ||
                                                                     pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE ||
                                                                     pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.RESERVADO)).Take(1).SingleOrDefault();
            }
            
            return _session.QueryOver<PedidoDeVenda>()
                                        .Where(pedido => pedido.Id == pedidoId &&
                                                                    (pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO ||
                                                                     pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE)).Take(1).SingleOrDefault();
        }
        
        public PedidoDeVenda ConsulteJoinComItens(int pedidoId)
        {
            ItemPedidoDeVenda itemPedidoDeVenda = null;

            return _session.QueryOver<PedidoDeVenda>()
                                        .Left.JoinAlias(pedido => pedido.ListaItens, () => itemPedidoDeVenda)
                                        .Where(pedido => pedido.Id == pedidoId).SingleOrDefault();
        }

        public List<PedidoDeVenda> ConsulteListaPedidosPagosERoteiroConcluido(EnumPesquisaPorFuncao pesquisaPorFuncao,
                                                                                List<Pessoa> parceiroPesquisa,
                                                                                bool periodoFaturamento, DateTime dataInicialPeriodo, 
                                                                                DateTime dataFinalPeriodo)
        {
            Expression<Func<PedidoDeVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            if (periodoFaturamento)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataFechamento >= dataInicialPeriodo &&
                                                                                                   vwvenda.DataFechamento <= dataFinalPeriodo);
            }
            else
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataElaboracao >= dataInicialPeriodo &&
                                                                                                   vwvenda.DataElaboracao <= dataFinalPeriodo);
            }

            Expression<Func<PedidoDeVenda, bool>> expressaoParaConsultaParceiro = null;

            if (parceiroPesquisa.Count != 0)
                for (int i = 0; i < parceiroPesquisa.Count; i++)
                {
                    int idPesquisa = parceiroPesquisa[i].Id;

                    if (pesquisaPorFuncao == EnumPesquisaPorFuncao.INDICADOR)
                    {
                        if (i == 0)
                            expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.Indicador.Id == idPesquisa);
                        else
                            expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.Indicador.Id == idPesquisa);
                    }
                    else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.ATENDENTE)
                    {
                        if (i == 0)
                            expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.Atendente.Id == idPesquisa);
                        else
                            expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.Atendente.Id == idPesquisa);
                    }
                    else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.VENDEDOR)
                    {
                        if (idPesquisa == 0)
                        {
                            if (i == 0)
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.Vendedor.DadosGerais.Razao == null);
                            else
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.Vendedor.DadosGerais.Razao == null);
                        }
                        else
                        {
                            if (i == 0)
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.Vendedor.Id== idPesquisa);
                            else
                                expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.Vendedor.Id == idPesquisa);
                        }
                    }
                    else if (pesquisaPorFuncao == EnumPesquisaPorFuncao.SUPERVISOR)
                    {
                        if (i == 0)
                            expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.AndAlso(vwvenda => vwvenda.Supervisor.Id == idPesquisa);
                        else
                            expressaoParaConsultaParceiro = expressaoParaConsultaParceiro.OrElse(vwvenda => vwvenda.Supervisor.Id == idPesquisa);
                    }
                }

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaParceiro);

            Expression<Func<PedidoDeVenda, bool>> expressaoParaConsultaStatus = null;
                
            expressaoParaConsultaStatus = expressaoParaConsultaStatus.OrElse(vwvenda => vwvenda.EstahPago == true 
                                                                                    && vwvenda.StatusRoteiro == EnumStatusRoteiro.CONCLUIDO
                                                                                    && vwvenda.StatusPedidoVenda != EnumStatusPedidoDeVenda.CANCELADO);
            

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(expressaoParaConsultaStatus);

            var query = _session.QueryOver<PedidoDeVenda>().Where(expressaoParaConsulta);


            return query.List().ToList();
        }

        public List<VWVenda> ConsulteTotalVWVendas(DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool statusEmitidoNFe)
        {
            Expression<Func<VWVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataElaboracao >= dataInicial &&
                                                                                                       vwvenda.DataElaboracao <= dataFinal);
                    expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.PedidoEstahPago == true &&
                                                                                               vwvenda.Status != EnumStatusPedidoDeVenda.CANCELADO);


            var query = _session.QueryOver<VWVenda>().Where(expressaoParaConsulta);

            return query.List().ToList();
        }
        public List<VWVenda> ConsulteTotalVWVendasII(DateTime dataInicial, DateTime dataFinal, EnumStatusPedidoDeVenda? statusPedidoDeVenda, bool statusEmitidoNFe)
        {
            Expression<Func<VWVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataFechamento >= dataInicial &&
                                                                                               vwvenda.DataFechamento <= dataFinal.AddHours(23).AddMinutes(59));
           
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.PedidoEstahPago == true &&
                                                                                       vwvenda.Status != EnumStatusPedidoDeVenda.CANCELADO);


            var query = _session.QueryOver<VWVenda>().Where(expressaoParaConsulta);

            return query.List().ToList();
        }

        public List<VWVenda> ConsulteVWVendasPag(DateTime dataInicial, DateTime dataFinal, string Condicao)
        {
            Expression<Func<VWVenda, bool>> expressaoParaConsulta = pedido => pedido.Id > 0;

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.DataFechamento >= dataInicial &&
                                                                                               vwvenda.DataFechamento <= dataFinal.AddHours(23).AddMinutes(59));
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(vwvenda => vwvenda.PedidoEstahPago == true &&
                                                                                       vwvenda.Status != EnumStatusPedidoDeVenda.CANCELADO
                                                                                       && vwvenda.FormaPagamentoNome == Condicao);


            var query = _session.QueryOver<VWVenda>().Where(expressaoParaConsulta);

            return query.List().ToList();
        }

        public List<CustoFinanceiro> ConsulteListaCustoFinanceiro(List<Pessoa> parceiroPesquisa, DateTime dataInicialPeriodo, DateTime dataFinalPeriodo)
        {
            string idPesquisa = "";
            if (parceiroPesquisa.Count != 0)
            {
                for (int i = 0; i < parceiroPesquisa.Count; i++)
                {
                    idPesquisa += parceiroPesquisa[i].Id + ",";

                }
            }
            idPesquisa = idPesquisa.Substring(0, idPesquisa.Length - 1);

            string sqlWhere = "pedidosvendas.pedido_vendedor_id IN  " + "(" + idPesquisa + ") And pedidosvendasparcelas.parcela_valor > 0 ";
            string innerJoin = " ";
           

            if (dataInicialPeriodo != null)
            {
                sqlWhere = sqlWhere + " AND pedidosvendas.pedido_data_elaboracao Between " + "'" + dataInicialPeriodo.ToString("yyyy-MM-dd 00:00:00") + "' And '" + dataFinalPeriodo.ToString("yyyy-MM-dd 23:59:00") + "'";
            }
            innerJoin = innerJoin + " inner join pedidosvendasparcelas ON pedidosvendas.pedido_id = pedidosvendasparcelas.parcela_pedido_venda_id";
            innerJoin = innerJoin + " inner join pessoas ON pedidosvendas.pedido_vendedor_id = pessoas.pes_id";

            var sql = " select pedidosvendas.pedido_vendedor_id as VendedorId, pedidosvendasparcelas.parcela_valor as ValorVendido, " +
                      " pedido_data_elaboracao as DataVenda, pessoas.pes_razao as VendedorNome" +
                      " FROM  pedidosvendas " +

                innerJoin +

                " WHERE " + sqlWhere;
               
                

            var query = _session.CreateSQLQuery(sql);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(CustoFinanceiro)));

            return query.List<CustoFinanceiro>().ToList();
        }
    }
}

