using System;
using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    [Funcionalidade(EnumFuncionalidade.CONTASPAGAR)]
    public class ServicoContasPagar : ServicoContasPagarReceber
    {
        public ServicoContasPagar()
            : base()
        {
        }

        public ServicoContasPagar(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
        }

        public override int Cadastre(ContaPagarReceber objetoDeNegocio)
        {
            objetoDeNegocio.TipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;

            return base.Cadastre(objetoDeNegocio);
        }

        public override void CadastreLista(List<ContaPagarReceber> ListaObjetoDeNegocio)
        {
            foreach (var item in ListaObjetoDeNegocio)
            {
                item.TipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;
            }

            base.CadastreLista(ListaObjetoDeNegocio);
        }

        public override void Atualize(ContaPagarReceber objetoDeNegocio)
        {
            objetoDeNegocio.TipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;

            base.Atualize(objetoDeNegocio);
        }

        public override void ValideParcelas(List<ContaPagarReceber> listaDeContasPagarReceber)
        {
            foreach (var item in listaDeContasPagarReceber)
            {
                item.TipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;
            }

            base.ValideParcelas(listaDeContasPagarReceber);
        }

        public override void ValideGeracaoParcelasContasPagarReceber(ContaPagarReceber contaPagarReceber, int quantidadeDeParcelas)
        {
            contaPagarReceber.TipoOperacao = EnumTipoOperacaoContasPagarReceber.PAGAR;

            base.ValideGeracaoParcelasContasPagarReceber(contaPagarReceber, quantidadeDeParcelas);
        }

        public override ContaPagarReceber Consulte(int idObjeto)
        {
            return _repositorioContasPagarReceber.Consulte(idObjeto, EnumTipoOperacaoContasPagarReceber.PAGAR);
        }

        public override List<ContaPagarReceber> ConsulteLista(Pessoa pessoa, EnumTipoOperacaoContasPagarReceber? tipoOperacao,
                                                                                        EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                                        FormaPagamento formaPagamento,
                                                                                        PlanoDeContas planoDeContas,
                                                                                        EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                                        DateTime? dataInicialPeriodo,
                                                                                        DateTime? dataFinalPeriodo,
                                                                                        double? valor=null,
                                                                                        int? categoriaId = null,
                                                                                        int ? Dre = null)
        {
            return _repositorioContasPagarReceber.ConsulteLista(pessoa,
                                                                                        EnumTipoOperacaoContasPagarReceber.PAGAR,
                                                                                        statusContaPagarReceber,
                                                                                        formaPagamento,
                                                                                        planoDeContas,
                                                                                        tipoDataFiltrar,
                                                                                        dataInicialPeriodo,
                                                                                        dataFinalPeriodo,
                                                                                        valor,
                                                                                        categoriaId);
        }
    }
}
