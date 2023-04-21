using FluentValidation;
using Programax.Infraestrutura.Negocio.Validacoes;
using System.Collections.Generic;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.Repositorio;
using System;

namespace Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio
{
    public class ValidacaoCondicaoPagamento : ValidacaoBase<CondicaoPagamento>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ValidacaoParcelaCondicaoPagamento _validacaoparcelaCondicaoPagamento;

        #endregion

        #region " CONSTRUTOR "

        public ValidacaoCondicaoPagamento()
        {
            _validacaoparcelaCondicaoPagamento = new ValidacaoParcelaCondicaoPagamento();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override FluentValidation.Results.ValidationResult Validate(CondicaoPagamento instance)
        {
            _validacaoparcelaCondicaoPagamento.ListaDeParcelas = instance.ListaDeParcelas;
            RuleFor(condicao => condicao.ListaDeParcelas).SetValidator(_validacaoparcelaCondicaoPagamento);

            return base.Validate(instance);
        }

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

        private void AssineRegraDescricaoObrigatoria()
        {
            RuleFor(condicao => condicao.Descricao)
                .Must(descricao => !string.IsNullOrEmpty(descricao))
                .WithMessage("Descrição é obrigatória.");
        }

        private void AssineRegraCondicaoAVistaPadraoNaoPodeModificar()
        {
            RuleFor(condicao => condicao.Descricao)
                .Must(descricao => !CondicaoEhPadraoAVistaDoSistema())
                .WithMessage("Essa Condição de Pagamento é Padrão do Sistema, Não Pode Ser Alterada.")
                .When(condicao => condicao.Id > 0);
        }

        private void AssineRegraValorDeDisponibilizacaoDaCondicaoObrigatoria()
        {
            RuleFor(condicao => condicao.ValorQueEstaraDisponivel)
                .Must(valorQueEstaraDisponivel => valorQueEstaraDisponivel != null)
                .WithMessage("Valor de disponibilização da condição de pagamento é obrigatorio.")
                .When(condicao => condicao.EstahDisponivelAcimaDeDeterminadoValor);
        }

        private void AssineRegraValorDeDisponibilizacaoDaCondicaoMaiorQueZero()
        {
            RuleFor(condicao => condicao.ValorQueEstaraDisponivel)
                .Must(valorQueEstaraDisponivel => valorQueEstaraDisponivel.Value > 0)
                .WithMessage("Valor de disponibilização da condição de pagamento tem que ser maior que 0(zero).")
                .When(condicao => condicao.EstahDisponivelAcimaDeDeterminadoValor && condicao.ValorQueEstaraDisponivel != null);
        }

        private void AssineRegraListaParcelasObrigatoria()
        {
            RuleFor(condicao => condicao.ListaDeParcelas)
                .Must(listaDeParcelas => listaDeParcelas != null && listaDeParcelas.Count > 0)
                .WithMessage("É necessário informar pelo menos uma parcela.");
        }

        private void AssineRegraSomaPercentualRateioIgualA100()
        {
            RuleFor(condicao => condicao.ListaDeParcelas)
                .Must(listaDeParcelas => PercentualRateioEstahIgualA100(listaDeParcelas))
                .WithMessage("O percentual de rateio deve ser igual a 100.")
                .When(condicao => condicao.ListaDeParcelas != null && condicao.ListaDeParcelas.Count > 0);
        }

        private void AssineRegraValideListaDeParcelas()
        {
            _validacaoparcelaCondicaoPagamento.ValideInclusao();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void AssineRegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraCondicaoAVistaPadraoNaoPodeModificar();
            AssineRegraDescricaoObrigatoria();
            AssineRegraListaParcelasObrigatoria();
            AssineRegraSomaPercentualRateioIgualA100();
            AssineRegraValideListaDeParcelas();
            AssineRegraValorDeDisponibilizacaoDaCondicaoObrigatoria();
            AssineRegraValorDeDisponibilizacaoDaCondicaoMaiorQueZero();
        }

        private bool PercentualRateioEstahIgualA100(IList<ParcelaCondicaoPagamento> listaParcelas)
        {
            double percentualRateio = 0;

            foreach (var parcela in listaParcelas)
            {
                percentualRateio += Math.Round(parcela.PercentualRateio, 2);
            }

            return Math.Round(percentualRateio, 2) == 100;
        }

        private bool CondicaoEhPadraoAVistaDoSistema()
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioCondicaoPagamento>();

            var condicaoDaBase = repositorio.ConsulteSemSessao(ObjetoValidado.Id);

            return condicaoDaBase != null && condicaoDaBase.CondicaoPadraoAVista;
        }

        #endregion
    }
}
