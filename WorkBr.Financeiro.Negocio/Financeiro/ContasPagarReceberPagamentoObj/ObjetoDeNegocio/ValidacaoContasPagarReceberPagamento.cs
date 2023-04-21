using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    public class ValidacaoContasPagarReceberPagamento : ValidacaoBase<ContaPagarReceberPagamento>
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

        private void AssineRegraDataPagamentoObrigatoria()
        {
            RuleFor(cprParcial => cprParcial.DataPagamento)
                .Must(dataPagamento => dataPagamento != DateTime.MinValue)
                .WithMessage("Data de Pagamento não Informada.");
        }

        private void AssineRegraDataPagamentoMenorOuIgualDataAtual()
        {
            RuleFor(cprParcial => cprParcial.DataPagamento.Date)
                .Must(dataPagamento => dataPagamento <= DateTime.Now.Date)
                .WithMessage("Data de Pagamento tem que ser menor ou igual a data atual.");
        }

        private void AssineRegraValorMaiorQueZeroObrigatoria()
        {
            RuleFor(cprParcial => cprParcial.Valor)
                .Must(valor => valor > 0)
                .WithMessage("O valor tem que ser maior que zero.");
        }

        private void AssineRegraCaixaDeveEstarAbertoSeFormaPagamentoForDinheiroOuCartao()
        {
            RuleFor(cprParcial => cprParcial.FormaPagamento)
                .Must(formaPagamento => UsuarioContemCaixaAberto())
                .WithMessage("É necessário ter um caixa aberto para essa forma de pagamento.")
                .When(cprParcial => cprParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DINHEIRO ||
                                                cprParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CHEQUE ||
                                                cprParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO ||
                                                cprParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraDataPagamentoObrigatoria();
            AssineRegraValorMaiorQueZeroObrigatoria();
            AssineRegraDataPagamentoMenorOuIgualDataAtual();
            AssineRegraCaixaDeveEstarAbertoSeFormaPagamentoForDinheiroOuCartao();
        }

        private bool UsuarioContemCaixaAberto()
        {
            var repositorioCaixa = FabricaDeRepositorios.Crie<IRepositorioCaixa>();

            var caixa = repositorioCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            var repositorioMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();

            if(caixa == null) throw new Exception("Não tem caixa para este usuário! Crie um caixa para continuar!");

            return repositorioMovimentacaoCaixa.ConsulteCaixaAberto(caixa) != null;
        }

        #endregion
    }
}
