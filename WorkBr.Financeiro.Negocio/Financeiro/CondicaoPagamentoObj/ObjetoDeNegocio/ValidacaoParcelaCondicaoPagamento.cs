using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;

namespace Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio
{
    public class ValidacaoParcelaCondicaoPagamento : ValidacaoBase<ParcelaCondicaoPagamento>
    {
        #region " PROPRIEDADES "

        public IList<ParcelaCondicaoPagamento> ListaDeParcelas { get; set; }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            AssineRegrasComunsAInclusaoEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraPercentualRateioNaoPodeSerZero()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("O percentual de rateio da parcela {0} não pode ser igual a 0(zero).");

            RuleFor(parcela => parcela.PercentualRateio)
                .Must((parcela, percentualRateio) =>
                {
                    if (percentualRateio > 0)
                    {
                        return true;
                    }
                    else
                    {
                        mensagemComposta.ListaDeParametros.Add(parcela.NumeroParcela);

                        return false;
                    }
                })
                .WithMessage(mensagemComposta);
        }

        private void AssineRegraDiasDaParcelaMaiorQueAAnterior()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Os dias da parcela {0} precisam ser maiores que os dias da parcela {1}.");

            RuleFor(parcela => parcela.Dias)
                .Must((parcela, dias) => DiasDaParcelaMaiorQueAAnterior(parcela, mensagemComposta))
                .WithMessage(mensagemComposta);
        }

        #endregion

        #region " MÉTODOS AUXILARES "

        private void AssineRegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraPercentualRateioNaoPodeSerZero();
            AssineRegraDiasDaParcelaMaiorQueAAnterior();
        }

        private bool DiasDaParcelaMaiorQueAAnterior(ParcelaCondicaoPagamento parcela, MensagemComposta mensagem)
        {
            if (parcela.NumeroParcela == 1)
            {
                return true;
            }

            var parcelaAnterior = ListaDeParcelas.FirstOrDefault(parcelaDaLista => parcelaDaLista.NumeroParcela == parcela.NumeroParcela - 1);

            if (parcelaAnterior.Dias < parcela.Dias)
            {
                return true;
            }
            else
            {
                mensagem.ListaDeParametros.Add(parcela.NumeroParcela);
                mensagem.ListaDeParametros.Add(parcelaAnterior.NumeroParcela);

                return false;
            }
        }

        #endregion
    }
}
