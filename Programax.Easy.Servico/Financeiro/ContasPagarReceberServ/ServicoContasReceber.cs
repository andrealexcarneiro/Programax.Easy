using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Servico.ServicoBase;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    [Funcionalidade(EnumFuncionalidade.CONTASRECEBER)]
    public class ServicoContasReceber : ServicoContasPagarReceber
    {
        public ServicoContasReceber()
            : base()
        {
        }

        public ServicoContasReceber(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
        }

        public override int Cadastre(ContaPagarReceber objetoDeNegocio)
        {
            objetoDeNegocio.TipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;

            return base.Cadastre(objetoDeNegocio);
        }

        public override void CadastreLista(List<ContaPagarReceber> ListaObjetoDeNegocio)
        {
            foreach (var item in ListaObjetoDeNegocio)
            {
                item.TipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;
            }

            base.CadastreLista(ListaObjetoDeNegocio);
        }

        public override void Atualize(ContaPagarReceber objetoDeNegocio)
        {
            objetoDeNegocio.TipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;

            base.Atualize(objetoDeNegocio);
        }

        public override void ValideParcelas(List<ContaPagarReceber> listaDeContasPagarReceber)
        {
            foreach (var item in listaDeContasPagarReceber)
            {
                item.TipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;
            }

            base.ValideParcelas(listaDeContasPagarReceber);
        }

        public override void ValideGeracaoParcelasContasPagarReceber(ContaPagarReceber contaPagarReceber, int quantidadeDeParcelas)
        {
            contaPagarReceber.TipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER;

            base.ValideGeracaoParcelasContasPagarReceber(contaPagarReceber, quantidadeDeParcelas);
        }

        public override ContaPagarReceber Consulte(int idObjeto)
        {
            return _repositorioContasPagarReceber.Consulte(idObjeto, EnumTipoOperacaoContasPagarReceber.RECEBER);
        }

        public List<ContaPagarReceber> ConsulteListaAberto(Pessoa pessoa)
        {
            var listaAberto = ConsulteLista(pessoa, EnumTipoOperacaoContasPagarReceber.RECEBER, EnumStatusContaPagarReceber.ABERTO, null, null, null, null, null, null);
            var listaAbertoProrrogado = ConsulteLista(pessoa, EnumTipoOperacaoContasPagarReceber.RECEBER, EnumStatusContaPagarReceber.ABERTOPRORROGADO, null, null, null, null, null);

            listaAberto.AddRange(listaAbertoProrrogado);

            return listaAberto;
        }

        public override List<ContaPagarReceber> ConsulteLista(Pessoa pessoa, EnumTipoOperacaoContasPagarReceber? tipoOperacao,
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
            return _repositorioContasPagarReceber.ConsulteLista(pessoa,
                                                                                        EnumTipoOperacaoContasPagarReceber.RECEBER,
                                                                                        statusContaPagarReceber,
                                                                                        formaPagamento, planoDeContas,
                                                                                        tipoDataFiltrar,
                                                                                        dataInicialPeriodo,
                                                                                        dataFinalPeriodo,
                                                                                        valor,
                                                                                        categoriaId);
        }

        public bool PossuiTituloAtrasado(int pessoaId)
        {
            return _repositorioContasPagarReceber.PossuiTituloAtrasado(pessoaId);
        }

        public void ConcluaContasReceber(ContaPagarReceber contaPagarReceberClonado, ContaPagarReceberPagamento contaPagarReceberPagamento,
                                        EnumTipoFormaPagamento tipoFormaPagamento, bool conciliacaoEstahHabilitada,
                                         CategoriaFinanceira categoriaFinanceira, BancoParaMovimento banco, double ValorPago, Pessoa usuario, 
                                         OperadorasCartao operadorasCartao)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();
                
                ServicoItemMovimentacaoBanco servicoItemMovimentacaoBanco = new ServicoItemMovimentacaoBanco();

                contaPagarReceberClonado.ListaContasPagarReceberParcial = new List<ContaPagarReceberPagamento>();

                contaPagarReceberClonado.ListaContasPagarReceberParcial.Add(contaPagarReceberPagamento);                

                servicoContasPagarReceberPagamento.CadastreLista(contaPagarReceberClonado.ListaContasPagarReceberParcial.ToList());
                servicoContasPagarReceberPagamento.Cadastre(contaPagarReceberPagamento);

                //Movimentação Bancária
                if (tipoFormaPagamento != EnumTipoFormaPagamento.DINHEIRO && conciliacaoEstahHabilitada)
                {
                    servicoItemMovimentacaoBanco.InsiraMovimentacaoBancaria(contaPagarReceberClonado, false,
                                                        EnumTipoOperacaoContasPagarReceber.RECEBER, contaPagarReceberClonado.DataPagamento.Value,
                                                        categoriaFinanceira, banco, ValorPago, usuario);

                    //Calcular despesas de cartões e lançar no banco
                    if (tipoFormaPagamento != EnumTipoFormaPagamento.CARTAOCREDITO || tipoFormaPagamento != EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        servicoItemMovimentacaoBanco.CalculeDespesasCartoes(contaPagarReceberClonado, false,
                                                       EnumTipoOperacaoContasPagarReceber.PAGAR, contaPagarReceberClonado.DataPagamento.Value,
                                                      banco,
                                                       operadorasCartao,
                                                       ValorPago, usuario);
                    }
                }

                scope.Complete();
            }
        }
    }
}
