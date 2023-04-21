using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using NHibernate.Transform;
using NHibernate.Criterion;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using System.Collections;
using Programax.Easy.Servico.Cadastros.PessoaServ;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class RepositorioContasPagarReceber : RepositorioBase<ContaPagarReceber>, IRepositorioContasPagarReceber
    {
        public RepositorioContasPagarReceber(ISession sessao)
            : base(sessao)
        {

        }

        public ContaPagarReceber Consulte(int id, EnumTipoOperacaoContasPagarReceber tipoOperacao)
        {
            return _session.QueryOver<ContaPagarReceber>().Where(conta => conta.Id == id && conta.TipoOperacao == tipoOperacao).SingleOrDefault();
        }

        public List<ContaPagarReceber> ConsulteLista(Pessoa pessoa,
                                                                           EnumTipoOperacaoContasPagarReceber? tipoOperacao,
                                                                           EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                           FormaPagamento formaPagamento,
                                                                           PlanoDeContas planoDeContas,
                                                                           EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           double? valor = null,
                                                                           int? categoriaId = null,
                                                                           int? Dre = null )
        {
            //Expression<Func<ContaPagarReceber, bool>> expressaoParaConsulta = contaPagarReceber => contaPagarReceber.Id > 0;

            string sqlWhere = "cpr_id > " + "'" + 0 + "'";

            if (pessoa != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.Pessoa.Id == pessoa.Id);

                sqlWhere = sqlWhere + " AND cpr_pes_id = " + "'" + pessoa.Id + "'";
            }

            if (tipoOperacao != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.TipoOperacao == tipoOperacao);

                sqlWhere = sqlWhere + " AND cpr_tipo_operacao =" + "'" + tipoOperacao.GetHashCode() + "'";
            }

            if (statusContaPagarReceber != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.Status == statusContaPagarReceber);

                sqlWhere = sqlWhere + " AND cpr_status = " + "'" + statusContaPagarReceber.GetHashCode() + "'";
            }

            if (formaPagamento != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.FormaPagamento.Id == formaPagamento.Id);

                sqlWhere = sqlWhere + " AND cpr_forpag_id = " + "'" + formaPagamento.Id + "'";
            }

            if (planoDeContas != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.PlanoDeContas.Id == planoDeContas.Id);

                sqlWhere = sqlWhere + " AND cpr_plc_id = " + "'" + planoDeContas.Id + "'";
            }

            if (tipoDataFiltrar != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.EMISSAO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataEmissao >= dataInicialPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_emissao >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.PAGAMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataPagamento >= dataInicialPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_pagamento >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.VENCIMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataVencimento >= dataInicialPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_vencimento >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
                    }
                }

                if (dataFinalPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.EMISSAO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataEmissao <= dataFinalPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_emissao <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.PAGAMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataPagamento <= dataFinalPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_pagamento <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.VENCIMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataVencimento <= dataFinalPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_vencimento <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
                    }
                }

                if (valor != null && valor != 0)
                {
                    //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => Math.Round(contaPagarReceber.ValorParcela, 2) == Math.Round(valor.Value, 2));

                    string valorReplace = valor.ToString().Replace(',', '.');

                    sqlWhere = sqlWhere + " AND cpr_valor_parcela = " + "'" + valorReplace + "'";
                }

                if (categoriaId != null)
                {
                    //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.CategoriaFinanceira.Id == categoriaId);

                    sqlWhere = sqlWhere + " AND CPR_ID_CATEGORIA = " + "'" + categoriaId + "'";
                }
                if (Dre != null)
                {
                    if (categoriaId != 12)
                    {
                        sqlWhere = sqlWhere + " AND CPR_VALOR_PAGO > 0 ";
                    }

                }
            }

            var sql = "SELECT cpr_id AS Id,  cpr_tipo_operacao AS TipoOperacao,  cpr_status AS Status, cpr_pes_id AS PessoaId, " +
                "cpr_forpag_id AS FormaPagamentoId, " +
                "cpr_pes_usuario_id AS UsuarioId,  cpr_plc_id AS PlanoDeContasId, CPR_CHEQUE_ID AS ChequeId, cpr_historico AS Historico, " +
                "cpr_data_emissao AS DataEmissao, cpr_data_vencimento AS DataVencimento, cpr_data_pagamento AS DataPagamento, " +
                "cpr_valor_parcela AS ValorParcela, cpr_multa AS Multa, cpr_juros AS Juros, cpr_desconto AS Desconto," +
                "cpr_numero_documento AS  NumeroDocumento, CPR_ORIGEM_DOCUMENTO AS OrigemDocumento, cpr_multa_eh_percentual AS MultaEhPercentual, " +
                "cpr_juros_eh_percentual AS JurosEhPercentual, " +
                "cpr_calculo_juros_multas_manual AS ehCalculoDeJurosMultaManual, CPR_VALOR_PAGO AS ValorPago, " +
                "CPR_ID_BANCO_MOV AS BancoParaMovimentoId, " +
                "CPR_ID_CATEGORIA AS CategoriaFinanceiraId, CPR_ID_OPERADORASCARTAO AS OperadorasCartaoId, CPR_ID_CONDICAO_PGTO AS CondicaoPgtoId" +

                " FROM  contaspagarreceber " +
                " WHERE " + sqlWhere;

            var query = _session.CreateSQLQuery(sql);

            query.AddScalar("Id", NHibernateUtil.Int32);
            query.AddScalar("TipoOperacao", NHibernateUtil.Int32);
            query.AddScalar("Status", NHibernateUtil.Int32);
            query.AddScalar("PessoaId", NHibernateUtil.Int32);
            query.AddScalar("FormaPagamentoId", NHibernateUtil.Int32);
            query.AddScalar("UsuarioId", NHibernateUtil.Int32);
            query.AddScalar("PlanoDeContasId", NHibernateUtil.Int32);
            query.AddScalar("ChequeId", NHibernateUtil.Int32);
            query.AddScalar("Historico", NHibernateUtil.String);
            query.AddScalar("DataEmissao", NHibernateUtil.DateTime);
            query.AddScalar("DataVencimento", NHibernateUtil.DateTime);
            query.AddScalar("DataPagamento", NHibernateUtil.DateTime);
            query.AddScalar("ValorParcela", NHibernateUtil.Double);
            query.AddScalar("Multa", NHibernateUtil.Double);
            query.AddScalar("Juros", NHibernateUtil.Double);
            query.AddScalar("Desconto", NHibernateUtil.Double);
            query.AddScalar("NumeroDocumento", NHibernateUtil.String);
            query.AddScalar("OrigemDocumento", NHibernateUtil.Int32);
            query.AddScalar("MultaEhPercentual", NHibernateUtil.Boolean);
            query.AddScalar("JurosEhPercentual", NHibernateUtil.Boolean);
            query.AddScalar("ehCalculoDeJurosMultaManual", NHibernateUtil.Boolean);
            query.AddScalar("ValorPago", NHibernateUtil.Double);
            query.AddScalar("BancoParaMovimentoId", NHibernateUtil.Int32);
            query.AddScalar("CategoriaFinanceiraId", NHibernateUtil.Int32);
            query.AddScalar("OperadorasCartaoId", NHibernateUtil.Int32);
            query.AddScalar("CondicaoPgtoId", NHibernateUtil.Int32);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(ContaPagarReceberPesquisa)));

            var list = query.List<ContaPagarReceberPesquisa>().ToList();

            return ConvertPesquisaPagarReceber(list);

            //return _session.QueryOver<ContaPagarReceber>()
            //     .TransformUsing(Transformers.DistinctRootEntity)
            //     .Where(expressaoParaConsulta).List().ToList();
        }
        public List<ContaPagarReceber> ConsulteListaII(Pessoa pessoa,
                                                                           EnumTipoOperacaoContasPagarReceber? tipoOperacao,
                                                                           EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                           FormaPagamento formaPagamento,
                                                                           PlanoDeContas planoDeContas,
                                                                           EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                           DateTime? dataInicialPeriodo,
                                                                           DateTime? dataFinalPeriodo,
                                                                           double? valor = null,
                                                                           int? categoriaId = null,
                                                                           int? Dre = null)
        {
            //Expression<Func<ContaPagarReceber, bool>> expressaoParaConsulta = contaPagarReceber => contaPagarReceber.Id > 0;

            string sqlWhere = "cpr_id > " + "'" + 0 + "'";

            if (pessoa != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.Pessoa.Id == pessoa.Id);

                sqlWhere = sqlWhere + " AND cpr_pes_id = " + "'" + pessoa.Id + "'";
            }

            if (tipoOperacao != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.TipoOperacao == tipoOperacao);

                sqlWhere = sqlWhere + " AND cpr_tipo_operacao =" + "'" + tipoOperacao.GetHashCode() + "'";
            }

            if (statusContaPagarReceber != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.Status == statusContaPagarReceber);

                sqlWhere = sqlWhere + " AND cpr_status = " + "'" + statusContaPagarReceber.GetHashCode() + "'";
            }

            if (formaPagamento != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.FormaPagamento.Id == formaPagamento.Id);

                sqlWhere = sqlWhere + " AND cpr_forpag_id = " + "'" + formaPagamento.Id + "'";
            }

            if (planoDeContas != null)
            {
                //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.PlanoDeContas.Id == planoDeContas.Id);

                sqlWhere = sqlWhere + " AND cpr_plc_id = " + "'" + planoDeContas.Id + "'";
            }

            if (tipoDataFiltrar != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.EMISSAO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataEmissao >= dataInicialPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_emissao >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.PAGAMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataPagamento >= dataInicialPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_pagamento >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.VENCIMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataVencimento >= dataInicialPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_vencimento >= " + "'" + dataInicialPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 00:00:00") + "'";
                    }
                }

                if (dataFinalPeriodo != null)
                {
                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.EMISSAO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataEmissao <= dataFinalPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_emissao <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.PAGAMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataPagamento <= dataFinalPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_pagamento <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
                    }

                    if (tipoDataFiltrar == EnumDataFiltrarContasPagarReceber.VENCIMENTO)
                    {
                        //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataVencimento <= dataFinalPeriodo.GetValueOrDefault());

                        sqlWhere = sqlWhere + " AND cpr_data_vencimento <= " + "'" + dataFinalPeriodo.GetValueOrDefault().ToString("yyyy-MM-dd 23:59:00") + "'";
                    }
                }

                if (valor != null && valor != 0)
                {
                    //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => Math.Round(contaPagarReceber.ValorParcela, 2) == Math.Round(valor.Value, 2));

                    string valorReplace = valor.ToString().Replace(',', '.');

                    sqlWhere = sqlWhere + " AND cpr_valor_parcela = " + "'" + valorReplace + "'";
                }

                if (categoriaId != null)
                {
                    //expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.CategoriaFinanceira.Id == categoriaId);

                    sqlWhere = sqlWhere + " AND CPR_ID_CATEGORIA = " + "'" + categoriaId + "'";
                }
                //if (Dre != null)
                //{
                //    if (categoriaId != 12)
                //    {
                //        sqlWhere = sqlWhere + " AND CPR_VALOR_PAGO > 0 ";
                //    }

                //}
            }

            var sql = "SELECT cpr_id AS Id,  cpr_tipo_operacao AS TipoOperacao,  cpr_status AS Status, cpr_pes_id AS PessoaId, " +
                "cpr_forpag_id AS FormaPagamentoId, " +
                "cpr_pes_usuario_id AS UsuarioId,  cpr_plc_id AS PlanoDeContasId, CPR_CHEQUE_ID AS ChequeId, cpr_historico AS Historico, " +
                "cpr_data_emissao AS DataEmissao, cpr_data_vencimento AS DataVencimento, cpr_data_pagamento AS DataPagamento, " +
                "cpr_valor_parcela AS ValorParcela, cpr_multa AS Multa, cpr_juros AS Juros, cpr_desconto AS Desconto," +
                "cpr_numero_documento AS  NumeroDocumento, CPR_ORIGEM_DOCUMENTO AS OrigemDocumento, cpr_multa_eh_percentual AS MultaEhPercentual, " +
                "cpr_juros_eh_percentual AS JurosEhPercentual, " +
                "cpr_calculo_juros_multas_manual AS ehCalculoDeJurosMultaManual, CPR_VALOR_PAGO AS ValorPago, " +
                "CPR_ID_BANCO_MOV AS BancoParaMovimentoId, " +
                "CPR_ID_CATEGORIA AS CategoriaFinanceiraId, CPR_ID_OPERADORASCARTAO AS OperadorasCartaoId, CPR_ID_CONDICAO_PGTO AS CondicaoPgtoId" +

                " FROM  contaspagarreceber " +
                " WHERE " + sqlWhere;

            var query = _session.CreateSQLQuery(sql);

            query.AddScalar("Id", NHibernateUtil.Int32);
            query.AddScalar("TipoOperacao", NHibernateUtil.Int32);
            query.AddScalar("Status", NHibernateUtil.Int32);
            query.AddScalar("PessoaId", NHibernateUtil.Int32);
            query.AddScalar("FormaPagamentoId", NHibernateUtil.Int32);
            query.AddScalar("UsuarioId", NHibernateUtil.Int32);
            query.AddScalar("PlanoDeContasId", NHibernateUtil.Int32);
            query.AddScalar("ChequeId", NHibernateUtil.Int32);
            query.AddScalar("Historico", NHibernateUtil.String);
            query.AddScalar("DataEmissao", NHibernateUtil.DateTime);
            query.AddScalar("DataVencimento", NHibernateUtil.DateTime);
            query.AddScalar("DataPagamento", NHibernateUtil.DateTime);
            query.AddScalar("ValorParcela", NHibernateUtil.Double);
            query.AddScalar("Multa", NHibernateUtil.Double);
            query.AddScalar("Juros", NHibernateUtil.Double);
            query.AddScalar("Desconto", NHibernateUtil.Double);
            query.AddScalar("NumeroDocumento", NHibernateUtil.String);
            query.AddScalar("OrigemDocumento", NHibernateUtil.Int32);
            query.AddScalar("MultaEhPercentual", NHibernateUtil.Boolean);
            query.AddScalar("JurosEhPercentual", NHibernateUtil.Boolean);
            query.AddScalar("ehCalculoDeJurosMultaManual", NHibernateUtil.Boolean);
            query.AddScalar("ValorPago", NHibernateUtil.Double);
            query.AddScalar("BancoParaMovimentoId", NHibernateUtil.Int32);
            query.AddScalar("CategoriaFinanceiraId", NHibernateUtil.Int32);
            query.AddScalar("OperadorasCartaoId", NHibernateUtil.Int32);
            query.AddScalar("CondicaoPgtoId", NHibernateUtil.Int32);

            query.SetResultTransformer(Transformers.AliasToBean(typeof(ContaPagarReceberPesquisa)));

            var list = query.List<ContaPagarReceberPesquisa>().ToList();

            return ConvertPesquisaPagarReceber(list);

            //return _session.QueryOver<ContaPagarReceber>()
            //     .TransformUsing(Transformers.DistinctRootEntity)
            //     .Where(expressaoParaConsulta).List().ToList();
        }

        public List<ContaPagarReceber> ConvertPesquisaPagarReceber(List<ContaPagarReceberPesquisa> listaPesquisa)
        {
            var listaPagarReceber = (from item in listaPesquisa
                                 select new ContaPagarReceber
                                 {
                                     Id = item.Id,
                                     DataEmissao = item.DataEmissao,
                                     DataVencimento = item.DataVencimento,
                                     DataPagamento = item.DataPagamento,
                                     TipoOperacao = (EnumTipoOperacaoContasPagarReceber)item.TipoOperacao,
                                     Status = (EnumStatusContaPagarReceber)item.Status,
                                     Pessoa = retorneDescricaoPessoa(item.PessoaId),
                                     Usuario = new Pessoa { Id = item.UsuarioId },
                                     FormaPagamento = new ServicoFormaPagamento().Consulte(item.FormaPagamentoId),
                                     PlanoDeContas = new PlanoDeContas { Id = item.Id },
                                     BancoParaMovimento = new BancoParaMovimento { Id = item.BancoParaMovimentoId },
                                     CategoriaFinanceira = new CategoriaFinanceira { Id = item.CategoriaFinanceiraId },
                                     OperadorasCartao = new OperadorasCartao { Id = item.OperadorasCartaoId },
                                     Historico = item.Historico,
                                     ValorParcela = item.ValorParcela,
                                     Multa = item.Multa,
                                     Juros = item.Juros,
                                     Desconto = item.Desconto,
                                     ehCalculoDeJurosMultaManual = item.ehCalculoDeJurosMultaManual,
                                     ehCalculoMultaAutomatica = item.ehCalculoMultaAutomatica,
                                     ValorPago = item.ValorPago,
                                     NumeroDocumento = item.NumeroDocumento,
                                     OrigemDocumento = (EnumOrigemDocumento)item.OrigemDocumento,
                                     MultaEhPercentual = item.MultaEhPercentual,
                                     JurosEhPercentual = item.JurosEhPercentual,
                                     ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>(),
                                     ListaHistoricoAlteracoesVencimento = new List<HistoricoAlteracaoVencimento>(),
                                     ChequeId = item.ChequeId,
                                     EhConciliacao = item.EhConciliacao,
                                     CondicaoPgtoId = item.CondicaoPgtoId,
                                     EhRecebimento = item.EhRecebimento,
                                     DataPedidoElaboracao = item.DataPedidoElaboracao
                                 }).ToList(); 


            return listaPagarReceber;
        }

        public Pessoa retorneDescricaoPessoa(int pessoaId)
        {
            var sql = "SELECT pes_razao AS Descricao FROM pessoas WHERE pes_id =" + "'" + pessoaId + "'";
            var query = _session.CreateSQLQuery(sql);

            query.AddScalar("Descricao", NHibernateUtil.String);

            var descricao = query.List<string>().ToList().DefaultIfEmpty();

            Pessoa pessoa = new Pessoa();

            pessoa.Id = pessoaId;
            pessoa.DadosGerais.Razao = descricao.FirstOrDefault(); //descricao;

            return pessoa;
        }

        public ContaPagarReceber ConsultePeloNumeroDocumentoParceiroEDataVencimentoAtivo(string numeroDocumento, Pessoa parceiro, DateTime dataVencimento, EnumTipoOperacaoContasPagarReceber tipoOperacao)
        {
            return _session.QueryOver<ContaPagarReceber>().Where(contaPagarReceber => contaPagarReceber.NumeroDocumento == numeroDocumento &&
                                                                                                                                       contaPagarReceber.Pessoa.Id == parceiro.Id &&
                                                                                                                                       contaPagarReceber.DataVencimento == dataVencimento &&
                                                                                                                                       contaPagarReceber.TipoOperacao == tipoOperacao &&
                                                                                                                                       contaPagarReceber.Status != EnumStatusContaPagarReceber.INATIVO).SingleOrDefault();
        }

        public VWTotalAReceber ConsuteTotalARebecerHoje()
        {
            return _session.QueryOver<VWTotalAReceber>().SingleOrDefault();
        }

        public List<VWAReceberAnual> ConsulteTotalAReceberAnual()
        {
            return _session.QueryOver<VWAReceberAnual>().List().ToList();
        }

        public List<VWAPagarAnual> ConsulteTotalAPagarAnual()
        {
            return _session.QueryOver<VWAPagarAnual>().List().ToList();
        }

        public List<VWAReceberMensal> ConsulteTotalAReceberMensal()
        {
            return _session.QueryOver<VWAReceberMensal>().List().ToList();
        }

        public List<VWAPagarMensal> ConsulteTotalAPagarMensal()
        {
            return _session.QueryOver<VWAPagarMensal>().List().ToList();
        }

        public List<VWAReceberTodosDiasDoAno> ConsulteTotalAReceberSemanal(DateTime DataInicial, DateTime DataFinal)
        {
            return _session.QueryOver<VWAReceberTodosDiasDoAno>()
                    .Where(x=>x.Vencimento >= DataInicial && x.Vencimento <= DataFinal).List().ToList();
        }

        public List<VWAPagarTodosDiasDoAno> ConsulteTotalAPagarSemanal(DateTime DataInicial, DateTime DataFinal)
        {
            return _session.QueryOver<VWAPagarTodosDiasDoAno>().Where(x => x.Vencimento >= DataInicial && x.Vencimento <= DataFinal).List().ToList();
        }

        public VWTotalAReceberEmAtraso ConsuteTotalARebecerEmAtraso()
        {
            return _session.QueryOver<VWTotalAReceberEmAtraso>().SingleOrDefault();
        }

        public VWTotalAPagar ConsuteTotalAPagarHoje()
        {
            return _session.QueryOver<VWTotalAPagar>().SingleOrDefault();
        }

        public VWTotalAPagarEmAtraso ConsuteTotalAPagarEmAtraso()
        {
            return _session.QueryOver<VWTotalAPagarEmAtraso>().SingleOrDefault();
        }

        public List<ContaPagarReceber> ConsulteListaFazendoFetchComParceiroEEnderecos(Pessoa pessoa,
                                                                                                                                EnumDataFiltrarContasPagarReceber? dataFiltrar,
                                                                                                                                DateTime? dataInicialPeriodo,
                                                                                                                                DateTime? dataFinalPeriodo,
                                                                                                                                EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                                                                                EnumTipoOperacaoContasPagarReceber tipoOperacao,
                                                                                                                                EnumOrdenacaoPesquisaContasPagarReceber ordenacaoPesquisaContasPagarReceber,
                                                                                                                                 int? categoriaId = null)
        {
            Expression<Func<ContaPagarReceber, bool>> expressaoParaConsulta = contaPagarReceber => contaPagarReceber.Id > 0;

            if (pessoa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.Pessoa.Id == pessoa.Id);
            }

            if (statusContaPagarReceber != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.Status == statusContaPagarReceber.Value);
            }

            if (tipoOperacao != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.TipoOperacao == tipoOperacao);
            }

            if (dataFiltrar != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (dataFiltrar == EnumDataFiltrarContasPagarReceber.EMISSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataEmissao >= dataInicialPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrar == EnumDataFiltrarContasPagarReceber.PAGAMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataPagamento >= dataInicialPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrar == EnumDataFiltrarContasPagarReceber.VENCIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataVencimento >= dataInicialPeriodo.GetValueOrDefault());
                    }
                }

                if (dataFinalPeriodo != null)
                {
                    if (dataFiltrar == EnumDataFiltrarContasPagarReceber.EMISSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataEmissao <= dataFinalPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrar == EnumDataFiltrarContasPagarReceber.PAGAMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataPagamento <= dataFinalPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrar == EnumDataFiltrarContasPagarReceber.VENCIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.DataVencimento <= dataFinalPeriodo.GetValueOrDefault());
                    }

                    if (categoriaId != 0)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(contaPagarReceber => contaPagarReceber.CategoriaFinanceira.Id == categoriaId);
                        
                    }
                }
            }

            Pessoa parceiro = null;
            EnderecoPessoa enderecoPessoa = null;

            var consulta = _session.QueryOver<ContaPagarReceber>()
                .Where(expressaoParaConsulta)
                .Left.JoinAlias(contaPagarReceber => contaPagarReceber.Pessoa, () => parceiro)
                .Left.JoinAlias(contaPagarReceber => parceiro.ListaDeEnderecos, () => enderecoPessoa)
                .TransformUsing(Transformers.DistinctRootEntity);

            if (ordenacaoPesquisaContasPagarReceber == EnumOrdenacaoPesquisaContasPagarReceber.CODIGO)
            {
                consulta.OrderBy(conta => conta.Id).Asc();
            }
            else if (ordenacaoPesquisaContasPagarReceber == EnumOrdenacaoPesquisaContasPagarReceber.DATAVENCIMENTO)
            {
                consulta.OrderBy(conta => conta.DataVencimento).Asc();
            }
            else if (ordenacaoPesquisaContasPagarReceber == EnumOrdenacaoPesquisaContasPagarReceber.NOMEPARCEIRO)
            {
                consulta.OrderBy(conta => parceiro.DadosGerais.NomeFantasia).Asc();
            }

            return consulta.List().ToList();
        }

        public bool PossuiTituloAtrasado(int pessoaId, EnumTipoOperacaoContasPagarReceber tipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER)
        {
            int qtdRegistros = _session.QueryOver<ContaPagarReceber>()
                                                        .Where(conta => conta.Pessoa.Id == pessoaId &&
                                                                       (conta.Status == EnumStatusContaPagarReceber.ABERTO ||
                                                                         conta.Status == EnumStatusContaPagarReceber.ABERTOPRORROGADO) &&
                                                                       conta.DataVencimento < DateTime.Now.Date &&
                                                                       conta.TipoOperacao == tipoOperacao)
                                                        .RowCount();

            return qtdRegistros > 0;
        }

        public List<ContaPagarReceber> ConsulteListaDeRecebimentoPorPedido(string numeroPedido)
        {  
            return _session.QueryOver<ContaPagarReceber>().Where(contaPagarReceber => contaPagarReceber.NumeroDocumento.IsLike (numeroPedido + "%") &&
                                                                 contaPagarReceber.OrigemDocumento == EnumOrigemDocumento.PEDIDODEVENDAS).List().ToList();
        }

      
    }
}
