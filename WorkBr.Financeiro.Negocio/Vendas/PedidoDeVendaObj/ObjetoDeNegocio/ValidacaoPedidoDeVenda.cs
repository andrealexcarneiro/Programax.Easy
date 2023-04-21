using System;
using System.Linq;
using FluentValidation;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.Repositorio;

namespace Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio
{
    public class ValidacaoPedidoDeVenda : ValidacaoBase<PedidoDeVenda>
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

        #region " VALIDAÇÕES PARA LIBERAÇÃO "

        public void ValidePedidoParaReserva()
        {
            AssineRegraClienteSemCredito();
            AssineRegraItensDescontoAcimaDoPermitido();
            AssineRegraClienteComContaEmAberto();
            AssineRegraClientePrecisaLiberacaoGerente();
        }

        private void AssineRegraClienteSemCredito()
        {   
            RuleFor(pedido => pedido.SaldoDisponivel)
                .Must(saldoDisponivel => saldoDisponivel -
                                                    ObjetoValidado.ListaParcelasPedidoDeVenda
                                                            .Sum(x => x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO ||
                                                                            x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO ||
                                                                            x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO ||
                                                                            x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO ||
                                                                            x.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA ? x.Valor : 0) > 0)
                .WithMessage("Sem saldo de Crédito");
        }

        private void AssineRegraItensDescontoAcimaDoPermitido()
        {
            ValidacaoItemPedidoDeVenda validacaoItemPedidoDeVenda = new ValidacaoItemPedidoDeVenda();
            validacaoItemPedidoDeVenda.ValideItemLiberacao();

            RuleFor(pedido => pedido.ListaItens).SetValidator(validacaoItemPedidoDeVenda);
        }

        private void AssineRegraClienteComContaEmAberto()
        {
            RuleFor(pedido => pedido.Cliente)
                .Must(cliente => !ClientePossuiContaAtrasada())
                .WithMessage("Este cliente possui uma ou mais contas em aberto.");
        }

        private void AssineRegraClientePrecisaLiberacaoGerente()
        {
            RuleFor(pedido => pedido.Cliente)
                .Must(cliente => !ClientePrecisaDeLiberacaoGerencial())
                .WithMessage("Este cliente precisa de liberação do gerente.");
        }

        #endregion

        #region " VALIDAÇÕES PARA ESTORNO "

        public void ValidePedidoEstorno()
        {
            AssineRegraPedidoNfeNaoPodeEstornar();
            AssineRegraTodasContasReceberEstaoInativas();
        }

        public void ValidePedidoAoCancelarRecebimento()
        {
            AssineRegraPedidoNfeNaoPodeEstornar();
            AssineRegraTodasContasReceberEstaoCanceladas();
        }

        private void AssineRegraPedidoNfeNaoPodeEstornar()
        {
            RuleFor(pedido => pedido.StatusPedidoVenda)
                .Must(status => status != EnumStatusPedidoDeVenda.EMITIDONFE)
                .WithMessage("Já foi emitida Nf-e para este pedido de venda.");
        }
        
        private void AssineRegraTodasContasReceberEstaoInativas()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("A(s) seguinte(s) conta(s) a receber com nr. lançamento, não estão inativa(s): {0}.");

            RuleFor(pedido => pedido.StatusPedidoVenda)
                .Must(status => TodasContasReceberEstaoInativas(mensagemComposta))
                .WithMessage(mensagemComposta);
        }

        private void AssineRegraTodasContasReceberEstaoCanceladas()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("A parcela selecionada foi cancelada com sucesso! " +
                "                                                   Porém, a(s) seguinte(s) parcela(s) com nr. lançamento, não estão cancelada(s): {0}." +
                                                                    " Para cancelar o pedido referente a esta(s) parcela(s) é necessário cancelar todas." +
                                                                    " Ao cancelar todas. Todas as parcelas serão inativadas referente ao pedido que as gerou. " +
                                                                    "Caso não queira cancelar todas, apenas a parcela selecionada foi cancelada!");

            RuleFor(pedido => pedido.StatusPedidoVenda)
                .Must(status => TodasContasReceberEstaoCanceladas(mensagemComposta))
                .WithMessage(mensagemComposta);
        }

        private bool TodasContasReceberEstaoInativas(MensagemComposta mensagemComposta)
        {
            string idsContasPagarReceber = string.Empty;

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();

            foreach (var parcela in ObjetoValidado.ListaParcelasPedidoDeVenda)
            {
                if (parcela.ContaPagarReceber != null)
                {
                    var contaPagarReceber = repositorio.Consulte(parcela.ContaPagarReceber.Id);

                    if (contaPagarReceber.Status != EnumStatusContaPagarReceber.INATIVO)
                    {
                        idsContasPagarReceber += string.IsNullOrEmpty(idsContasPagarReceber) ? contaPagarReceber.Id.ToString() : ", " + contaPagarReceber.Id.ToString();
                    }
                }
            }

            mensagemComposta.ListaDeParametros.Add(idsContasPagarReceber);

            return string.IsNullOrEmpty(idsContasPagarReceber);
        }

        private bool TodasContasReceberEstaoCanceladas(MensagemComposta mensagemComposta)
        {
            string idsContasPagarReceber = string.Empty;

            var repositorio = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();

            foreach (var parcela in ObjetoValidado.ListaParcelasPedidoDeVenda)
            {
                if (parcela.ContaPagarReceber != null)
                {
                    var contaPagarReceber = repositorio.Consulte(parcela.ContaPagarReceber.Id);

                    if (contaPagarReceber.Status != EnumStatusContaPagarReceber.CANCELADO)
                    {
                        idsContasPagarReceber += string.IsNullOrEmpty(idsContasPagarReceber) ? contaPagarReceber.Id.ToString() : ", " + contaPagarReceber.Id.ToString();
                    }
                }
            }

            mensagemComposta.ListaDeParametros.Add(idsContasPagarReceber);

            return string.IsNullOrEmpty(idsContasPagarReceber);
        }

        #endregion

        #region " VALIDAÇÕES PARA CANCELAMENTO QUANDO ESTIVER FATURADO OU EMITIDO NFE "

        public void ValidePedidoParaCancelamentoQuandoEstiverFaturadoOuEmitidoNfe()
        {
            AssineRegraUsuarioContemCaixa();
            AssineRegraUsuarioContemCaixaAberto();
        }

        private void AssineRegraUsuarioContemCaixa()
        {
            RuleFor(pedido => pedido.StatusPedidoVenda)
               .Must(status => UsuarioContemCaixa())
               .WithMessage("O usuário logado não contém um caixa cadastrado.");
        }

        private void AssineRegraUsuarioContemCaixaAberto()
        {
            RuleFor(pedido => pedido.StatusPedidoVenda)
               .Must(status => UsuarioContemCaixaAberto())
               .WithMessage("O usuário logado não contém um caixa aberto.");
        }

        #endregion

        #region " VALIDAÇÕES PEDIDO JÁ FATURADO OU EMITIDO NFE "

        public void ValidePedidoJaFaturadoOuEmitidoNfeNaoPodeSerAtualizado()
        {
            AssineRegraPedidoJaFaturadoOuEmitidoNfeNaoPodeSerAtualizado();
        }

        private void AssineRegraPedidoJaFaturadoOuEmitidoNfeNaoPodeSerAtualizado()
        {
            MensagemComposta mensagemComposta = new MensagemComposta("{0}");

            RuleFor(pedido => pedido.StatusPedidoVenda)
               .Must(status => !PedidoEstahFaturadoOuEmitidoNfe(mensagemComposta))
               .WithMessage(mensagemComposta);
        }

        #endregion

        #region " VALIDAÇÕES "

        private void AssineRegraTipoDocumentoObrigatorio()
        {
            RuleFor(pedido => pedido.TipoPedidoVenda)
                .NotNull()
                .WithMessage("Tipo documento não foi informado.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraClienteObrigatorio()
        {
            RuleFor(pedido => pedido.Cliente)
                .NotNull()
                .WithMessage("Cliente não informado.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraTabelaPrecoObrigatoria()
        {
            RuleFor(pedido => pedido.TabelaPreco)
                .NotNull()
                .WithMessage("Tabela preço não informado.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraFormaPagamentoObrigatoria()
        {
            RuleFor(pedido => pedido.FormaPagamento)
                .NotNull()
                .WithMessage("Forma Pagamento não informada.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraCondicaoPagamentoObrigatoria()
        {
            RuleFor(pedido => pedido.CondicaoPagamento)
                .NotNull()
                .WithMessage("Condição Pagamento não informada.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraQuantidadeItensMaiorQueZero()
        {
            RuleFor(pedido => pedido.ListaItens)
                .Must(listaItens => listaItens.Count > 0)
                .WithMessage("Quantidade de itens tem que ser maior que zero.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraValorTotalMaiorOuIgualAZero()
        {
            RuleFor(pedido => pedido.ValorTotal)
                .Must(valorTotal => valorTotal >= 0)
                .WithMessage("Valor Total da venda tem que ser maior ou igual a zero.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraEnderecoObrigatorio()
        {
            RuleFor(pedido => pedido.EnderecoPedidoDeVenda)
                .Must(endereco => EnderecoFoiInformado())
                .WithMessage("Endereço não informado.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraSomaFinanceiroIgualTotalVenda()
        {
            RuleFor(pedido => pedido.ListaParcelasPedidoDeVenda)
                .Must(parcelas => SomaFinanceiroIgualTotalVenda())
                .WithMessage("Soma das parcelas não batem com total da venda.")
                .When(pedido => !pedido.PedidoDoPdv && pedido.StatusPedidoVenda != EnumStatusPedidoDeVenda.EMITIDONFE);
        }

        private void AssineRegraClienteBloqueado()
        {
            RuleFor(pedido => pedido.Cliente)
                .Must(cliente => !ClienteEstahBloqueado())
                .WithMessage("Este cliente está bloqueado.");
        }

        private void AssineRegraClienteConsumidorFinalComLimiteCredito()
        {
            RuleFor(pedido => pedido.Cliente)
                .Must(cliente => !ClienteConsumidorEstahForaDoLimite())
                .WithMessage("Este cliente Consumidor Final está fora do Limite permitido de Venda. Escolha ou Cadastre outro Cliente para continuar!");
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void RegrasComunsAInclusaoEAtualizacao()
        {
            AssineRegraTipoDocumentoObrigatorio();
            AssineRegraClienteObrigatorio();
            AssineRegraTabelaPrecoObrigatoria();
            AssineRegraFormaPagamentoObrigatoria();
            AssineRegraCondicaoPagamentoObrigatoria();
            AssineRegraQuantidadeItensMaiorQueZero();
            AssineRegraValorTotalMaiorOuIgualAZero();
            AssineRegraEnderecoObrigatorio();
            AssineRegraSomaFinanceiroIgualTotalVenda();
            AssineRegraClienteBloqueado();
            AssineRegraClienteConsumidorFinalComLimiteCredito();
        }

        private bool EnderecoFoiInformado()
        {
            if (!ObjetoValidado.EnderecoPedidoDeVenda.ClienteResideExterior)
            {

                return ObjetoValidado.EnderecoPedidoDeVenda != null &&
                      ObjetoValidado.EnderecoPedidoDeVenda.Cidade != null &&
                      !string.IsNullOrWhiteSpace(ObjetoValidado.EnderecoPedidoDeVenda.Bairro) &&
                      !string.IsNullOrWhiteSpace(ObjetoValidado.EnderecoPedidoDeVenda.Rua) &&
                      !string.IsNullOrWhiteSpace(ObjetoValidado.EnderecoPedidoDeVenda.CEP) &&
                      ObjetoValidado.EnderecoPedidoDeVenda.TipoEndereco != null;
            }

            return true;
        }

        private bool SomaFinanceiroIgualTotalVenda()
        {
            var valorTotalParcelas = ObjetoValidado.ListaParcelasPedidoDeVenda.Sum(parcela => parcela.Valor);

            return Math.Round(valorTotalParcelas, 2) == Math.Round(ObjetoValidado.ValorTotal, 2);
        }

        private bool UsuarioContemCaixa()
        {
            var repositorioCaixa = FabricaDeRepositorios.Crie<IRepositorioCaixa>();
            return repositorioCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada) != null;
        }

        private bool UsuarioContemCaixaAberto()
        {
            var repositorioCaixa = FabricaDeRepositorios.Crie<IRepositorioCaixa>();
            var caixa = repositorioCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                return true;
            }

            var repositorioMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();
            return repositorioMovimentacaoCaixa.ConsulteCaixaAberto(caixa) != null;
        }

        private bool PedidoEstahFaturadoOuEmitidoNfe(MensagemComposta mensagemComposta)
        {
            var repositorio = FabricaDeRepositorios.Crie<IRepositorioPedidoDeVenda>();

            var pedidoDaBase = repositorio.ConsulteSemSessao(ObjetoValidado.Id);

            if (pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO)
            {
                mensagemComposta.ListaDeParametros.Add("Este pedido já foi faturado.");
            }
            else if (pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.RECUSADO)
            {
                mensagemComposta.ListaDeParametros.Add("Este pedido já foi recusado.");
            }
            else if (pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.CANCELADO)
            {
                mensagemComposta.ListaDeParametros.Add("Este pedido já foi cancelado.");
            }
            else if (pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE && pedidoDaBase.EstahPago)
            {
                mensagemComposta.ListaDeParametros.Add("Já foi emitido NF-e deste pedido.");
            }

            return pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.RECUSADO ||
                      pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.CANCELADO ||
                      pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO ||
                      pedidoDaBase.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE;
        }

        private bool ClientePossuiContaAtrasada()
        {
            var repositorioContaPagarReceber = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();
            if ((EnumTipoFormaPagamento)ObjetoValidado.FormaPagamento.Id == EnumTipoFormaPagamento.DINHEIRO ||
                (EnumTipoFormaPagamento)ObjetoValidado.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO ||
                (EnumTipoFormaPagamento)ObjetoValidado.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO)
            {
                return false;
            }

            return repositorioContaPagarReceber.PossuiTituloAtrasado(ObjetoValidado.Cliente.Id);
        }

        private bool ClientePrecisaDeLiberacaoGerencial()
        {
            var repositorioAnaliseCredito = FabricaDeRepositorios.Crie<IRepositorioCrediario>();

            var analiseCredito = repositorioAnaliseCredito.Consulte(ObjetoValidado.Cliente.Id);

            if (analiseCredito != null)
                return analiseCredito.StatusAnaliseCredito == EnumStatusCrediario.LIBERACAOGERENTE;
            else
                return false;
        }

        private bool ClienteEstahBloqueado()
        {
            var repositorioAnaliseCredito = FabricaDeRepositorios.Crie<IRepositorioCrediario>();

            var analiseCredito = repositorioAnaliseCredito.Consulte(ObjetoValidado.Cliente.Id);

            if (analiseCredito != null)
                return analiseCredito.StatusAnaliseCredito == EnumStatusCrediario.BLOQUEADO;
            else
                return false;
        }

        private bool ClienteConsumidorEstahForaDoLimite()
        {
            var repositorioAnaliseCredito = FabricaDeRepositorios.Crie<IRepositorioCrediario>();

            var analiseCredito = repositorioAnaliseCredito.Consulte(ObjetoValidado.Cliente.Id);

            if (analiseCredito != null)
                return analiseCredito.Pessoa.DadosGerais.Razao.Contains("CONSUMIDOR FINAL") && ObjetoValidado.ValorTotal > analiseCredito.ValorLimiteCredito;
            else
                return false;
        }

        #endregion
    }
}
