using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programax.Infraestrutura.Negocio.Validacoes;
using FluentValidation;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.Enumeradores;

namespace Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio
{
    public class ValidacaoContasPagarReceber : ValidacaoBase<ContaPagarReceber>
    {
        #region " VARIÁVEIS PRIVADAS "

        private ContaPagarReceber _contaPagarReceberBase;

        #endregion

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

        private void AssineRegraTipoOperacaoObrigatorio()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.TipoOperacao)
                .NotNull()
                .WithMessage("Tipo de Operação não informado.");
        }

        private void AssineRegraParceiroObrigatorio()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.Pessoa)
                .NotNull()
                .WithMessage("Parceiro não informado.");
        }

        private void AssineRegraFormaDePagamentoObrigatorio()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.FormaPagamento)
                .NotNull()
                .WithMessage("Forma de Pagamento não informado.");
        }

        private void AssineRegraNumeroDocumentoObrigatorio()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.NumeroDocumento)
                .Must(numeroDocumento => !string.IsNullOrEmpty(numeroDocumento))
                .WithMessage("Número do documento não informado.");
        }

        private void AssineRegraValorParcelaMaiorQueZero()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.ValorParcela)
                .Must(valorParcela => valorParcela > 0)
                .WithMessage("Valor Parcela tem que ser maior que zero.");
        }

        private void AssineRegraDataVencimentoObrigatorio()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.DataVencimento)
                .Must(dataVencimento => dataVencimento != null && dataVencimento > DateTime.MinValue)
                .WithMessage("Data de Vencimento não informada.");
        }

        private void AssineRegraDataEmissaoObrigatorio()
        {
            RuleFor(contasPagarReceber => contasPagarReceber.DataEmissao)
                .Must(dataEmissao => dataEmissao != null && dataEmissao > DateTime.MinValue)
                .WithMessage("Data de Emissão não informada.");
        }

        private void AssineRegraNumeroDocumentoUnicoParaParceiroNaData()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("O número de documento {0} já existe para este na parceiro na data {1}.");

            RuleFor(contasPagarReceber => contasPagarReceber.Pessoa)
                .Must((contaPagarReceber, pessoa) => !ExisteEsteNumeroDeDocumentoParaEsteParceiroNestaData(contaPagarReceber, mensagemComposta))
                .WithMessage(mensagemComposta)
                .When(contasPagarReceber => contasPagarReceber.Pessoa != null &&
                                                             contasPagarReceber.DataVencimento != null &&
                                                             contasPagarReceber.DataVencimento > DateTime.MinValue &&
                                                             !string.IsNullOrEmpty(contasPagarReceber.NumeroDocumento));
        }

        private void AssineRegraDataPagamentoObrigatoria()
        {
            RuleFor(contaPagarReceber => contaPagarReceber.DataPagamento)
                .Must(dataPagamento => dataPagamento != null && dataPagamento > DateTime.MinValue)
                .WithMessage("Data de Pagamento não informada.")
                .When(contaPagarReceber => contaPagarReceber.Status == EnumStatusContaPagarReceber.QUITADO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CONCILIADOQUITADO);
        }

        private void AssineRegraDataPagamentoNaoDeveSerInformada()
        {   
            RuleFor(contaPagarReceber => contaPagarReceber.DataPagamento)
                .Must(dataPagamento => dataPagamento == null)
                .WithMessage("Data de Pagamento só deve ser informada no momento da baixa.")
                .When(contaPagarReceber => contaPagarReceber.Status != EnumStatusContaPagarReceber.QUITADO & contaPagarReceber.Status != EnumStatusContaPagarReceber.CONCILIADOQUITADO);
                
        }

        private void AssineRegraStatusBaseEstahInativo()
        {
            RuleFor(contaPagarReceber => contaPagarReceber.Status)
                .Must(status => ContaPagarReceberNaoInativo())
                .WithMessage("Título já está inativo.")
                .When(contaPagarReceber => contaPagarReceber.Id > 0);
        }

        private void AssineRegraDataVencimentoMaiorQueDataEmissao()
        {
            RuleFor(contaPagarReceber => contaPagarReceber.DataVencimento)
                .Must((contaPagarReceber, dataVencimento) => dataVencimento >= contaPagarReceber.DataEmissao)
                .WithMessage("Data de Vencimento tem que ser maior ou igual a Data de Emissão.")
                .When(contaPagarReceber => contaPagarReceber.DataVencimento != null && !contaPagarReceber.EhRecebimento);
        }

        private void AssineRegraDataVencimentoMaiorQueDataPedidoElaboracao()
        {
            RuleFor(contaPagarReceber => contaPagarReceber.DataVencimento)
                .Must((contaPagarReceber, dataVencimento) => dataVencimento >= contaPagarReceber.DataPedidoElaboracao)
                .WithMessage("Data de Vencimento tem que ser maior ou igual a Data de Elaboração do Pedido.")
                .When(contaPagarReceber => contaPagarReceber.DataVencimento != null && contaPagarReceber.EhRecebimento);
        }

        private void AssineRegraValorAReceberNaoPodeSerNegativo()
        {
            RuleFor(contaPagarReceber => contaPagarReceber.ValorTotal)
                .Must((contaPagarReceber, valorTotal) => valorTotal >= contaPagarReceber.ValorPago)
                .WithMessage("Valor à receber não pode ser negativo.")
                .When(contaPagarReceber => contaPagarReceber.DataVencimento != null);
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraTipoOperacaoObrigatorio();
            AssineRegraParceiroObrigatorio();
            AssineRegraFormaDePagamentoObrigatorio();
            AssineRegraNumeroDocumentoObrigatorio();
            AssineRegraDataVencimentoObrigatorio();
            AssineRegraDataEmissaoObrigatorio();
            AssineRegraDataPagamentoObrigatoria();

            AssineRegraValorAReceberNaoPodeSerNegativo();

            AssineRegraValorParcelaMaiorQueZero();

            AssineRegraNumeroDocumentoUnicoParaParceiroNaData();

            AssineRegraDataPagamentoNaoDeveSerInformada();

            AssineRegraStatusBaseEstahInativo();

            AssineRegraDataVencimentoMaiorQueDataEmissao();

            AssineRegraDataVencimentoMaiorQueDataPedidoElaboracao();
           
        }

        private bool ExisteEsteNumeroDeDocumentoParaEsteParceiroNestaData(ContaPagarReceber contaPagarReceber, MensagemComposta mensagemComposta)
        {
            IRepositorioContasPagarReceber repositorio = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();

            var contaPagarReceberBase = repositorio.ConsultePeloNumeroDocumentoParceiroEDataVencimentoAtivo(contaPagarReceber.NumeroDocumento,
                                                                                                                                                                        contaPagarReceber.Pessoa,
                                                                                                                                                                        contaPagarReceber.DataVencimento.GetValueOrDefault(),
                                                                                                                                                                        contaPagarReceber.TipoOperacao);

            mensagemComposta.ListaDeParametros.Add(contaPagarReceber.NumeroDocumento);
            mensagemComposta.ListaDeParametros.Add(contaPagarReceber.DataVencimento.GetValueOrDefault().ToString("dd/MM/yyyy"));

            return contaPagarReceberBase != null && contaPagarReceberBase.Id != contaPagarReceber.Id;
        }

        private ContaPagarReceber RetorneContaPagarReceberBase()
        {
            IRepositorioContasPagarReceber repositorio = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();

            _contaPagarReceberBase = repositorio.ConsulteSemSessao(ObjetoValidado.Id);

            return _contaPagarReceberBase;
        }

        private bool ContaPagarReceberNaoEstahQuitado()
        {
            var contaPagarReceberBase = RetorneContaPagarReceberBase();

            return contaPagarReceberBase.Status != EnumStatusContaPagarReceber.QUITADO || contaPagarReceberBase.Status != EnumStatusContaPagarReceber.CONCILIADOQUITADO;
        }

        private bool ContaPagarReceberNaoInativo()
        {
            var contaPagarReceberBase = RetorneContaPagarReceberBase();

            return contaPagarReceberBase.Status != EnumStatusContaPagarReceber.INATIVO;
        }

        #endregion
    }
}
