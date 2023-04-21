using FluentValidation;
using Programax.Infraestrutura.Negocio.Fabricas;
using System.Linq;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.Repositorio;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using System;

namespace Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio
{
    public class ValidacaoEntradaMercadoria : ValidacaoBase<EntradaMercadoria>
    {
        #region " MÉTODOS SOBRESCRITOS "

        public override void ValideInclusao()
        {
            AssineRegraNotaFiscalJaExisteNaBase();

            RegrasComunsAoInserirEAtualizar();
        }

        public override void ValideAtualizacao()
        {
            RegrasComunsAoInserirEAtualizar();
        }

        #endregion

        #region " REGRAS "

        private void AssineRegraNotaFiscalJaExisteNaBase()
        {
            RuleFor(entrada => entrada.NumeroNota)
                .Must((entrada, numeroNota) => EstaNotaNaoConstaNaBase(entrada))
                .WithMessage("Esta nota já consta no sistema.")
                .When(entrada => entrada.Fornecedor != null &&
                                            !string.IsNullOrEmpty(entrada.NumeroNota) && !string.IsNullOrEmpty(entrada.Serie));
        }

        #region " PROPRIEDADES OBRIGATÓRIAS "

        private void AssineRegraNumeroObrigatorio()
        {
            RuleFor(entrada => entrada.NumeroNota)
                .Must(numeroNota => !string.IsNullOrEmpty(numeroNota))
                .WithMessage("Número da nota não informada.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraSerieObrigatoria()
        {
            RuleFor(entrada => entrada.Serie)
                .Must(serie => !string.IsNullOrEmpty(serie))
                .WithMessage("Serie da nota não informada.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraFornecedorObrigatorio()
        {
            RuleFor(entrada => entrada.Fornecedor)
                .Must(fornecedor => fornecedor != null)
                .WithMessage("Fornecedor não informado.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraModeloDocumentoFiscalObrigatorio()
        {
            RuleFor(entrada => entrada.ModeloDocumentoFiscal)
                .NotNull()
                .WithMessage("O modelo de documento fiscal não foi informado.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraTipoFreteObrigatorio()
        {
            RuleFor(entrada => entrada.TipoFrete)
                .NotNull()
                .WithMessage("O tipo do frete não foi informado.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraNaturezaOperacaoObrigatoria()
        {
            RuleFor(entrada => entrada.NaturezaOperacao)
                .Must(natureza => natureza != null || !string.IsNullOrWhiteSpace(ObjetoValidado.NaturezaOperacaoNota))
                .WithMessage("Natureza da Operação não informada.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        protected void AssineRegraEhNecessarioPeloMenos1Produto()
        {
            RuleFor(entrada => entrada.ListaDeItens)
                .Must(listaDeItens => listaDeItens != null && listaDeItens.Count > 0)
                .WithMessage("É necessário inserir ao menos 1 item.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        #endregion

        #region " VALIDA FECHAMENTO COM OS ITENS "

        private void AssineRegraBaseIcmsFechamentoEstahIgualAosItens()
        {
            RuleFor(entrada => entrada.BaseIcms)
                .Must((entrada, baseIcms) => BaseIcmsFechamentoEstahIgualAosItens(entrada))
                .WithMessage("A base ICMS não está fechando com os itens.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraValorIcmsFechamentoEstahIgualAosItens()
        {
            RuleFor(entrada => entrada.ValorIcms)
                .Must((entrada, valorIcms) => ValorIcmsFechamentoEstahIgualAosItens(entrada))
                .WithMessage("O valor ICMS não está fechando com os itens.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraBaseIcmsStFechamentoEstahIgualAosItens()
        {
            RuleFor(entrada => entrada.BaseIcmsSt)
                .Must((entrada, baseIcms) => BaseIcmsStFechamentoEstahIgualAosItens(entrada))
                .WithMessage("A base ICMS ST não está fechando com os itens.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraValorIcmsStFechamentoEstahIgualAosItens()
        {
            RuleFor(entrada => entrada.ValorIcmsSt)
                .Must((entrada, valorIcms) => ValorIcmsStFechamentoEstahIgualAosItens(entrada))
                .WithMessage("O valor ICMS ST não está fechando com os itens.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraValorIpiFechamentoEstahIgualAosItens()
        {
            RuleFor(entrada => entrada.ValorIpi)
                .Must((entrada, baseIcms) => ValorIpiFechamentoEstahIgualAosItens(entrada))
                .WithMessage("O valor IPI não está fechando com os itens.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegraTotalNotaFechamentoEstahIgualAosItens()
        {
            RuleFor(entrada => entrada.ValorTotalNota)
                .Must((entrada, valorIcms) => ValorTotalNotaFechamentoEstahIgualAosItens(entrada))
                .WithMessage("O total da nota não está fechando com os itens.")
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegrasItens()
        {
            ValidacaoItemEntrada validacaoItemEntrada = new ValidacaoItemEntrada();

            validacaoItemEntrada.ValideInclusao();

            RuleFor(entrada => entrada.ListaDeItens).SetValidator(validacaoItemEntrada)
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        private void AssineRegrasFinanceiro()
        {
            ValidacaoFinanceiroEntrada validacaoFinanceiroEntrada = new ValidacaoFinanceiroEntrada();

            validacaoFinanceiroEntrada.ValideInclusao();

            RuleFor(entrada => entrada.ListaFinanceiroEntrada).SetValidator(validacaoFinanceiroEntrada)
                .When(entrada => entrada.StatusEntrada == EnumStatusEntrada.CONCLUIDA);
        }

        #endregion

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAoInserirEAtualizar()
        {
            AssineRegraNumeroObrigatorio();
            AssineRegraSerieObrigatoria();
            AssineRegraFornecedorObrigatorio();
            AssineRegraEhNecessarioPeloMenos1Produto();
            AssineRegraModeloDocumentoFiscalObrigatorio();
            AssineRegraTipoFreteObrigatorio();
            AssineRegraNaturezaOperacaoObrigatoria();

            AssineRegraBaseIcmsFechamentoEstahIgualAosItens();
            AssineRegraValorIcmsFechamentoEstahIgualAosItens();

            AssineRegraBaseIcmsStFechamentoEstahIgualAosItens();
            AssineRegraValorIcmsStFechamentoEstahIgualAosItens();

            AssineRegraValorIpiFechamentoEstahIgualAosItens();
            AssineRegraTotalNotaFechamentoEstahIgualAosItens();

            AssineRegrasItens();
            AssineRegrasFinanceiro();
        }

        private bool EstaNotaNaoConstaNaBase(EntradaMercadoria entradaMercadoria)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioEntradaMercadoria>();

            var entradaDaBase = repositorio.Consulte(entradaMercadoria.NumeroNota, entradaMercadoria.Serie, entradaMercadoria.Fornecedor);

            return entradaDaBase == null;
        }

        private bool BaseIcmsFechamentoEstahIgualAosItens(EntradaMercadoria entrada)
        {
            var baseIcmsItens = entrada.ListaDeItens.Sum(item => item.BaseIcms.GetValueOrDefault());
            var baseIcmsCapa = entrada.BaseIcms;

            return Math.Round(baseIcmsCapa, 2) == Math.Round(baseIcmsItens, 2);
        }

        private bool ValorIcmsFechamentoEstahIgualAosItens(EntradaMercadoria entrada)
        {
            var valorIcmsItens = entrada.ListaDeItens.Sum(item => item.ValorIcms.GetValueOrDefault());
            var valorIcmsCapa = entrada.ValorIcms;

            return Math.Round(valorIcmsCapa, 2) == Math.Round(valorIcmsItens, 2);
        }

        private bool BaseIcmsStFechamentoEstahIgualAosItens(EntradaMercadoria entrada)
        {
            var baseIcmsStItens = entrada.ListaDeItens.Sum(item => item.BaseIcmsSt.GetValueOrDefault());
            var baseIcmsStCapa = entrada.BaseIcmsSt;

            return Math.Round(baseIcmsStCapa, 2) == Math.Round(baseIcmsStItens, 2);
        }

        private bool ValorIcmsStFechamentoEstahIgualAosItens(EntradaMercadoria entrada)
        {
            var valorIcmsStItens = entrada.ListaDeItens.Sum(item => item.ValorIcmsSt.GetValueOrDefault());
            var valorIcmsStCapa = entrada.ValorIcmsSt;

            return Math.Round(valorIcmsStCapa, 2) == Math.Round(valorIcmsStItens, 2);
        }

        private bool ValorIpiFechamentoEstahIgualAosItens(EntradaMercadoria entrada)
        {
            var valorIpiItens = entrada.ListaDeItens.Sum(item => item.ValorIpi.GetValueOrDefault());
            var valorIpiCapa = entrada.ValorIpi;

            return Math.Round(valorIpiCapa, 2) == Math.Round(valorIpiItens, 2);
        }

        private bool ValorTotalNotaFechamentoEstahIgualAosItens(EntradaMercadoria entrada)
        {
            var valorTotalItensItens = entrada.ListaDeItens.Sum(item => item.ValorTotal.GetValueOrDefault() +
                                                                                                    item.ValorIcmsSt.GetValueOrDefault() -
                                                                                                    item.ValorDesoneracaoProduto.GetValueOrDefault() +
                                                                                                    item.ValorIpi.GetValueOrDefault());
            var valorTotalItensCapa = entrada.ValorTotalNota;

            return Math.Round(valorTotalItensCapa, 2) == Math.Round(valorTotalItensItens, 2);
        }

        #endregion
    }
}
