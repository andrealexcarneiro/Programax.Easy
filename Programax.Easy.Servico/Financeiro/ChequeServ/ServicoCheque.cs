using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Servico.ServicoBase;
using Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.ChequeObj.Repositorio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using System.Transactions;

namespace Programax.Easy.Servico.Financeiro.ChequeServ
{
    [Funcionalidade(EnumFuncionalidade.CHEQUES)]
    public class ServicoCheque : ServicoAkilSmallBusiness<Cheque, ValidacaoCheque, ConversorCheque>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioCheque _repositorioCheque;

        #endregion

        #region " CONSTRUTOR "

        public ServicoCheque()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<Cheque> RetorneRepositorio()
        {
            if (_repositorioCheque == null)
            {
                _repositorioCheque = FabricaDeRepositorios.Crie<IRepositorioCheque>();
            }

            return _repositorioCheque;
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
            return _repositorioCheque.ConsulteLista(pessoa,
                                                                       dataFiltrarCheque,
                                                                       dataInicialPeriodo,
                                                                       dataFinalPeriodo,
                                                                       banco,
                                                                       numeroCheque,
                                                                       statusAbertoDepositado,
                                                                       statusRecebido,
                                                                       statusDevolvidoPrimeira,
                                                                       statusDevolvidoSegunda,
                                                                       statusReapresentado,
                                                                       statusCustodiadoFactoring,
                                                                       statusInativo);
        }


        public List<Cheque> ConsulteListaChequePorPedido(int numeroCheque)
        {
            return _repositorioCheque.ConsulteListaChequePorPedido(numeroCheque);
        }

        public Cheque ConsulteJoinComItens(int chequeId)
        {
            return _repositorioCheque.ConsulteJoinComItens(chequeId);
        }

        public Cheque ConsulteChequePeloNumeroDocumento(string numeroDocumento)
        {
            return _repositorioCheque.ConsulteChequePeloNumeroDocumento(numeroDocumento);
        }
        
        #endregion
    }
}
