using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.ChequeObj.Repositorio;
using System.Collections;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.ChequeObj.ObjetoDeNegocio
{
    public class ValidacaoCheque : ValidacaoBase<Cheque>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAInclusaoEAtualizacao();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraClienteObrigatorio()
        {
            RuleFor(cheque => cheque.Pessoa)
                .NotNull()
                .WithMessage("Cliente não informado.");
        }

        private void AssineRegraDataEmissaoObrigatoria()
        {
            RuleFor(cheque => cheque.DataEmissao)
                .Must(dataEmissao => dataEmissao != null && dataEmissao > DateTime.MinValue)
                .WithMessage("Data Emissão não informada.");
        }

        private void AssineRegraDataVencimentoObrigatoria()
        {
            RuleFor(cheque => cheque.DataVencimento)
                .Must(dataVencimento => dataVencimento != null && dataVencimento > DateTime.MinValue)
                .WithMessage("Data Vencimento não informada.");
        }

        private void AssineRegraDataRecebimentoObrigatoria()
        {
            RuleFor(cheque => cheque.DataRecebimento)
                .Must(dataRecebimento => dataRecebimento != null && dataRecebimento > DateTime.MinValue)
                .WithMessage("Data Recebimento não informada.")
                .When(cheque => cheque.StatusCheque == EnumStatusCheque.RECEBIDO);
        }

        private void AssineRegraBancoObrigatorio()
        {
            RuleFor(cheque => cheque.Banco)
                .NotNull()
                .WithMessage("Banco não informado.");
        }

        private void AssineRegraAgenciaObrigatoria()
        {
            RuleFor(cheque => cheque.Agencia)
                .Must(agencia => !string.IsNullOrEmpty(agencia))
                .WithMessage("Agência não informado.");
        }

        private void AssineRegraContaObrigatoria()
        {
            RuleFor(cheque => cheque.Conta)
                .Must(conta => !string.IsNullOrEmpty(conta))
                .WithMessage("Conta não informada.");
        }

        private void AssineRegraDigitoObrigatoria()
        {
            RuleFor(cheque => cheque.Digito)
                .Must(digito => !string.IsNullOrEmpty(digito))
                .WithMessage("Digito não informado.");
        }

        private void AssineRegraNumeroChequeObrigatorio()
        {
            RuleFor(cheque => cheque.NumeroCheque)
                .Must(numeroCheque => !string.IsNullOrEmpty(numeroCheque))
                .WithMessage("Número do Cheque não informado.");
        }

        private void AssineRegraValorMaiorQueZero()
        {
            RuleFor(cheque => cheque.ValorCheque)
                .Must(valor => valor > 0)
                .WithMessage("O Valor do cheque deve ser maior que zero.");
        }

        private void AssineRegraStatusObrigatorio()
        {
            RuleFor(cheque => cheque.StatusCheque)
                .NotNull()
                .WithMessage("Situação não informada.");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraValorMaiorQueZero();
            AssineRegraStatusObrigatorio();
            AssineRegraDataEmissaoObrigatoria();
            AssineRegraDataVencimentoObrigatoria();
            AssineRegraDataRecebimentoObrigatoria();
            AssineRegraBancoObrigatorio();
            AssineRegraNumeroChequeObrigatorio();
            AssineRegraClienteObrigatorio();
            AssineRegraAgenciaObrigatoria();
            AssineRegraContaObrigatoria();
            AssineRegraDigitoObrigatoria();
        }

        private bool ChequeEhUnico()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCheque>();

            Cheque chequeBase = repositorio.Consulte(ObjetoValidado.Banco,
                                                                          ObjetoValidado.Agencia,
                                                                          ObjetoValidado.Conta,
                                                                          ObjetoValidado.Digito,
                                                                          ObjetoValidado.Serie,
                                                                          ObjetoValidado.NumeroCheque);

            return chequeBase == null || chequeBase.Id == ObjetoValidado.Id;
        }

        #endregion
    }
}
