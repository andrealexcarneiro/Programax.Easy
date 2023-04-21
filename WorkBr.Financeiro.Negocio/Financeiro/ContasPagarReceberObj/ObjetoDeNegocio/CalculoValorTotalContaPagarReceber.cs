using System;
using System.Linq;
using Programax.Easy.Negocio.Financeiro.Enumeradores;


namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{   
    public class CalculoValorTotalContaPagarReceber
    {
        public static double CalculeValorTotalContaPagarReceber(ContaPagarReceber contaPagarReceber, bool ehCalculoManual = false, bool ehMultaAutomatica = false)
        {   
            double valorTotal = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count !=0? 
                    contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().Valor: contaPagarReceber.ValorParcela;
            
            var dataCalcularValor = RetorneDataCalculoValor(contaPagarReceber);

            valorTotal += RetorneValorMulta(contaPagarReceber, dataCalcularValor, ehCalculoManual, ehMultaAutomatica);

            valorTotal += RetorneValorJuros(contaPagarReceber, dataCalcularValor, ehCalculoManual);

            valorTotal -= contaPagarReceber.Desconto;
             
            if(contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.PAGAR && contaPagarReceber.Multa == 0 &&
                contaPagarReceber.Desconto ==0  && contaPagarReceber.Juros == 0)
            {
                return contaPagarReceber.ValorParcela;
            }

            return Math.Round(valorTotal,2);
        }

        private static DateTime RetorneDataCalculoValor(ContaPagarReceber contaPagarReceber)
        {
            DateTime dataCalcularValor = contaPagarReceber.DataVencimento.Value;

            if (contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO)
            {
                dataCalcularValor = contaPagarReceber.DataPagamento.GetValueOrDefault().Date;
            }

            return dataCalcularValor;
        }

        private static double RetorneValorMulta(ContaPagarReceber contaPagarReceber, DateTime dataCalcularValor, bool ehCalculoManual, bool ehMultaAutomatica = false)
        {
            double valorParcela = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().Valor : 0; ;
            var dataVencimento = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().DataVencimento: DateTime.Parse("01 / 01 / 0001 00:00:00");
            
            if (ehMultaAutomatica)
            {
                if (dataCalcularValor > dataVencimento || ehCalculoManual)
                {
                    if (contaPagarReceber.MultaEhPercentual)
                    {
                        return valorParcela * contaPagarReceber.Multa / (double)100;
                    }
                    else
                    {
                        return contaPagarReceber.Multa;
                    }
                }
            }
            else
            {
                if (contaPagarReceber.MultaEhPercentual)
                {
                    return valorParcela * contaPagarReceber.Multa / (double)100;
                }
                else
                {
                    return contaPagarReceber.Multa;
                }
            }
            
            if(contaPagarReceber.EhConciliacao || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
            {
                return contaPagarReceber.Multa;
            }

            return 0;
        }

        private static double RetorneValorJuros(ContaPagarReceber contaPagarReceber, DateTime dataCalcularValor, bool ehCalculoManual)
        {
            double valorParcela = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().Valor : 0;
            var DataVencimento = contaPagarReceber.ListaHistoricoAlteracoesVencimento.Count != 0 ? contaPagarReceber.ListaHistoricoAlteracoesVencimento.FirstOrDefault().DataVencimento : contaPagarReceber.DataEmissao;
            
            if (dataCalcularValor > DataVencimento || ehCalculoManual)
            {
                var TotalDiasVencido = (dataCalcularValor - DataVencimento);

                int totalDiasVencido = (int)TotalDiasVencido.TotalDays;

                int contaDias = 1;

                double valorJuros = 0;

                while (totalDiasVencido >= contaDias)
                {
                    if (contaPagarReceber.JurosEhPercentual)
                    {
                        valorJuros = valorJuros + valorParcela * (contaPagarReceber.Juros /30)/ (double)100;

                        valorParcela = valorParcela + valorJuros;
                    }
                    else
                    {
                        valorJuros = contaPagarReceber.Juros;

                        double valorJurosPorDia = valorJuros / 30;

                        return valorJurosPorDia * totalDiasVencido;
                    }

                    if (contaPagarReceber.EhConciliacao || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO)
                    {
                        return contaPagarReceber.Juros;
                    }
                                       
                    contaDias++;
                }

                return valorJuros;
                                               
            }
            
            return 0;
        }
    }
}