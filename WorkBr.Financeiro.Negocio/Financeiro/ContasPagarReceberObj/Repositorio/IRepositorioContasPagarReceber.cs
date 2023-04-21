using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio
{
    public interface IRepositorioContasPagarReceber:IRepositorioBase<ContaPagarReceber>
    {
        ContaPagarReceber Consulte(int id, EnumTipoOperacaoContasPagarReceber tipoOperacao);

        List<ContaPagarReceber> ConsulteLista(Pessoa pessoa,
                                                                  EnumTipoOperacaoContasPagarReceber? tipoOperacao, 
                                                                  EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                  FormaPagamento formaPagamento,
                                                                  PlanoDeContas planoDeContas,
                                                                  EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                  DateTime? dataInicialPeriodo,
                                                                  DateTime? dataFinalPeriodo,
                                                                  double? valor=null,
                                                                  int? categoriaId = null,
                                                                  int? Dre = null  );
        List<ContaPagarReceber> ConsulteListaII(Pessoa pessoa,
                                                                  EnumTipoOperacaoContasPagarReceber? tipoOperacao,
                                                                  EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                  FormaPagamento formaPagamento,
                                                                  PlanoDeContas planoDeContas,
                                                                  EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                  DateTime? dataInicialPeriodo,
                                                                  DateTime? dataFinalPeriodo,
                                                                  double? valor = null,
                                                                  int? categoriaId = null,
                                                                  int? Dre = null);

        ContaPagarReceber ConsultePeloNumeroDocumentoParceiroEDataVencimentoAtivo(string numeroDocumento,
                                                                                                                                    Pessoa parceiro, 
                                                                                                                                    DateTime dataVencimento, 
                                                                                                                                    EnumTipoOperacaoContasPagarReceber tipoOperacao);

        VWTotalAReceber ConsuteTotalARebecerHoje();

        VWTotalAReceberEmAtraso ConsuteTotalARebecerEmAtraso();

        VWTotalAPagar ConsuteTotalAPagarHoje();

        VWTotalAPagarEmAtraso ConsuteTotalAPagarEmAtraso();

        List<VWAPagarAnual> ConsulteTotalAPagarAnual();

        List<VWAReceberAnual> ConsulteTotalAReceberAnual();

        List<VWAPagarMensal> ConsulteTotalAPagarMensal();

        List<VWAReceberMensal> ConsulteTotalAReceberMensal();

        List<VWAReceberTodosDiasDoAno> ConsulteTotalAReceberSemanal(DateTime DataInicial, DateTime DataFinal);

        List<VWAPagarTodosDiasDoAno> ConsulteTotalAPagarSemanal(DateTime DataInicial, DateTime DataFinal);

        List<ContaPagarReceber> ConsulteListaFazendoFetchComParceiroEEnderecos(Pessoa pessoa, 
                                                                                                                       EnumDataFiltrarContasPagarReceber? dataFiltrar,
                                                                                                                       DateTime? dataInicialPeriodo,
                                                                                                                       DateTime? dataFinalPeriodo, 
                                                                                                                       EnumStatusContaPagarReceber? statusContaPagarReceber, 
                                                                                                                       EnumTipoOperacaoContasPagarReceber tipoOperacao,
                                                                                                                       EnumOrdenacaoPesquisaContasPagarReceber ordenacaoPesquisaContasPagarReceber,
                                                                                                                       int? categoriaId = null);

        bool PossuiTituloAtrasado(int pessoaId, EnumTipoOperacaoContasPagarReceber tipoOperacao = EnumTipoOperacaoContasPagarReceber.RECEBER);

        List<ContaPagarReceber> ConsulteListaDeRecebimentoPorPedido(string numeroPedido);
    }
}
