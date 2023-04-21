using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ChequeObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Servico.Repositorios;
using Programax.Easy.Negocio.Financeiro.VincularChequePedidosObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Financeiro.ChequeServ
{
    public class RepositorioCheque : RepositorioBase<Cheque>, IRepositorioCheque
    {
        #region " CONSTRUTOR "

        public RepositorioCheque(ISession sessao)
            : base(sessao)
        {

        }

        #endregion

        #region " CONSULTAS "

        public List<Cheque> ConsulteLista(Pessoa pessoa,
                                                           EnumDataFiltrarCheque? dataFiltrarCheque,
                                                           DateTime? dataInicialPeriodo,
                                                           DateTime? dataFinalPeriodo,
                                                           Banco banco,
                                                           string numeroCheque,
                                                           bool statusAbertoDepositado,
                                                           bool statusRecebido,
                                                           bool statusDevolvidoPrimeira,
                                                           bool statusDevolvidoSegunda,
                                                           bool statusReapresentado,
                                                           bool statusCustodiadoFactoring,
                                                           bool statusInativo)
        {
            Expression<Func<Cheque, bool>> expressaoParaConsulta = cheque => cheque.Id > 0;

            var filtrosPorPessoaBancoENumeroCheque = RetorneFiltroPorPessoaBancoENumeroCheque(pessoa, banco, numeroCheque);
            var filtrosPorData = RetorneFiltroPorData(dataFiltrarCheque, dataInicialPeriodo, dataFinalPeriodo);
            var filtrosPorStatus = RetorneFiltroPorStatus(statusAbertoDepositado,
                                                                            statusRecebido,
                                                                            statusDevolvidoPrimeira,
                                                                            statusDevolvidoSegunda,
                                                                            statusReapresentado,
                                                                            statusCustodiadoFactoring,
                                                                            statusInativo);

            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosPorPessoaBancoENumeroCheque);
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosPorData);
            expressaoParaConsulta = expressaoParaConsulta.AndAlso(filtrosPorStatus);

            return _session.QueryOver<Cheque>()
                .TransformUsing(Transformers.DistinctRootEntity)
                .Where(expressaoParaConsulta).List().ToList();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private Expression<Func<Cheque, bool>> RetorneFiltroPorPessoaBancoENumeroCheque(Pessoa pessoa, Banco banco, string numeroCheque)
        {
            Expression<Func<Cheque, bool>> expressaoParaConsulta = cheque => cheque.Id > 0;

            if (pessoa != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.Pessoa.Id == pessoa.Id);
            }

            if (banco != null)
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.Banco.Id == banco.Id);
            }

            if (!string.IsNullOrEmpty(numeroCheque))
            {
                expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.NumeroCheque.IsLike("%" + numeroCheque + "%"));
            }

            return expressaoParaConsulta;
        }

        private Expression<Func<Cheque, bool>> RetorneFiltroPorData(EnumDataFiltrarCheque? dataFiltrarCheque,
                                                                                                     DateTime? dataInicialPeriodo,
                                                                                                     DateTime? dataFinalPeriodo)
        {
            Expression<Func<Cheque, bool>> expressaoParaConsulta = null;

            if (dataFiltrarCheque != null)
            {
                if (dataInicialPeriodo != null)
                {
                    if (dataFiltrarCheque == EnumDataFiltrarCheque.EMISSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.DataEmissao >= dataInicialPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrarCheque == EnumDataFiltrarCheque.RECEBIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.DataRecebimento >= dataInicialPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrarCheque == EnumDataFiltrarCheque.VENCIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.DataVencimento >= dataInicialPeriodo.GetValueOrDefault());
                    }
                }

                if (dataFinalPeriodo != null)
                {
                    if (dataFiltrarCheque == EnumDataFiltrarCheque.EMISSAO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.DataEmissao <= dataFinalPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrarCheque == EnumDataFiltrarCheque.RECEBIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.DataRecebimento <= dataFinalPeriodo.GetValueOrDefault());
                    }

                    if (dataFiltrarCheque == EnumDataFiltrarCheque.VENCIMENTO)
                    {
                        expressaoParaConsulta = expressaoParaConsulta.AndAlso(cheque => cheque.DataVencimento <= dataFinalPeriodo.GetValueOrDefault());
                    }
                }
            }

            return expressaoParaConsulta;
        }

        private Expression<Func<Cheque, bool>> RetorneFiltroPorStatus(bool statusAbertoDepositado,
                                                                                                      bool statusRecebido,
                                                                                                      bool statusDevolvidoPrimeira,
                                                                                                      bool statusDevolvidoSegunda,
                                                                                                      bool statusReapresentado,
                                                                                                      bool statusCustodiadoFactoring,
                                                                                                      bool statusInativo)
        {
            Expression<Func<Cheque, bool>> expressaoParaConsulta = null;

            if (statusAbertoDepositado)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.ABERTODEPOSITADO);
            }

            if (statusRecebido)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.RECEBIDO);
            }

            if (statusDevolvidoPrimeira)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.DEVOLVIDO1);
            }

            if (statusDevolvidoSegunda)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.DEVOLVIDO2);
            }

            if (statusReapresentado)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.REAPRESENTADO);
            }

            if (statusCustodiadoFactoring)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.CUSTODIADOFACTORING);
            }

            if (statusInativo)
            {
                expressaoParaConsulta = expressaoParaConsulta.OrElse(cheque => cheque.StatusCheque == EnumStatusCheque.INATIVO);
            }

            return expressaoParaConsulta;
        }

        #endregion


        public Cheque Consulte(Banco banco, string agencia, string conta, string digito, string serie, string numeroCheque)
        {
            return _session.QueryOver<Cheque>().Where(cheque => cheque.Banco.Id == banco.Id &&
                                                                                                cheque.Agencia == agencia &&
                                                                                                cheque.Conta == conta &&
                                                                                                cheque.Digito == digito &&
                                                                                                cheque.Serie == serie &&
                                                                                                cheque.NumeroCheque == numeroCheque).Take(1).SingleOrDefault();
        }

        public List<Cheque> ConsulteListaChequePorPedido(int numeroDoPedido)
        {
            return _session.QueryOver<Cheque>().Where(cheque => cheque.NumeroPedidoVenda == numeroDoPedido).List().ToList();
        }

        public Cheque ConsulteChequePeloNumeroDocumento(string numeroDocumento)
        {
            return _session.QueryOver<Cheque>().Where(cheque => cheque.NumeroDocumento == numeroDocumento).Take(1).SingleOrDefault();
        }

        public Cheque ConsulteJoinComItens(int chequeId)
        {
            VincularChequePedidos itemVinculoCheque = null;

            return _session.QueryOver<Cheque>()
                                        .Left.JoinAlias(cheque => cheque.ListaVinculosDePedidos, () => itemVinculoCheque)
                                        .Where(cheque => cheque.Id == chequeId).SingleOrDefault();
        }
    }
}
