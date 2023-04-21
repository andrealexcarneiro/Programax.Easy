using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio
{
    public class ValidacaoFinanceiroEntrada : ValidacaoBase<FinanceiroEntrada>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAlteracao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAlteracao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegrasFormaPagamentoObrigatoria()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Forma de Pagamento não informada na parcela {0}.");

            RuleFor(financeiro => financeiro.FormaPagamento)
                .Must(formaPagamento =>
                {
                    mensagemComposta.ListaDeParametros.Add(ObjetoValidado.Parcela);

                    return formaPagamento != null;
                })
                .WithMessage(mensagemComposta);
        }

        private void AssineRegrasDataVencimentoObrigatoria()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Data de Vencimento não informada na parcela {0}.");

            RuleFor(financeiro => financeiro.DataVencimento)
                .Must(dataVencimento =>
                {
                    mensagemComposta.ListaDeParametros.Add(ObjetoValidado.Parcela);

                    return dataVencimento > DateTime.MinValue;
                })
                .WithMessage(mensagemComposta);
        }

        private void AssineRegrasValorDuplicataMaiorQueZero()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("O valor da duplicata tem que ser maior que zero na parcela {0}.");

            RuleFor(financeiro => financeiro.ValorDuplicata)
                .Must(valorDuplicata =>
                {
                    mensagemComposta.ListaDeParametros.Add(ObjetoValidado.Parcela);

                    return valorDuplicata > 0;
                })
                .WithMessage(mensagemComposta);
        }

        private void AssineRegrasNumeroDuplicataObrigatoria()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("Nr. Duplicata não informada na parcela {0}.");

            RuleFor(financeiro => financeiro.NumeroDocumento)
                .Must(numeroDocumento =>
                {
                    mensagemComposta.ListaDeParametros.Add(ObjetoValidado.Parcela);

                    return !string.IsNullOrWhiteSpace(numeroDocumento);
                })
                .WithMessage(mensagemComposta);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAlteracao()
        {
            AssineRegrasFormaPagamentoObrigatoria();
            AssineRegrasDataVencimentoObrigatoria();
            AssineRegrasValorDuplicataMaiorQueZero();
            AssineRegrasNumeroDuplicataObrigatoria();
        }

        #endregion
    }
}
