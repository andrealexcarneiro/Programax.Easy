using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;

namespace Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio
{
    public class CalculosFormacaoPrecoProduto
    {
        public static double CalculePrecoCusto(double precoCompra,
                                                               double valorFreteCompra,
                                                               double percentualIcms,
                                                               double percentualIpi,
                                                               double percentualIcmsSt,
                                                               double percentualReducaoIcms)
        {
            double somaPercentuais = percentualIpi + percentualIcmsSt + percentualReducaoIcms - percentualIcms;

            double precoCusto = (precoCompra + valorFreteCompra) + ((precoCompra + valorFreteCompra) * somaPercentuais / 100);

            return precoCusto;
        }

        public static double CalculeFreteProduto(double TotalEntrada, double TotalFreteEntrada, EnumTipoFrete tipoFrete)
        {
            if (tipoFrete == EnumTipoFrete.PORCONTADODESTINATARIOREMETENTE)
            {
                return TotalFreteEntrada / TotalEntrada;
            }
            else
            {
                return 0;
            }
        }

        public static double CalculePrecoVenda(double precoCusto, double markup)
        {
            return precoCusto * markup;
        }

        public static double CalculePrecoVenda(double precoCusto,
                                                                double percentualDespesasFixas,
                                                                double percentualDespesasVariaveis,
                                                                double percentualImpostos,
                                                                double percentualOutrasDespesas,
                                                                double percentualFrete,
                                                                double percentualComissoes,
                                                                double percentualLucro)
        {
            double somatorioPercentuais = percentualDespesasFixas +
                                                         percentualDespesasVariaveis +
                                                         percentualImpostos +
                                                         percentualOutrasDespesas +
                                                         percentualFrete +
                                                         percentualComissoes +
                                                         percentualLucro;

            //return precoCusto / (double)((100 - somatorioPercentuais) / (double)100);

            return precoCusto + (precoCusto * somatorioPercentuais / (double)100);
        }
    }
}
