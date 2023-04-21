using System;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System.Transactions;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ
{
    public class ServicoContasPagarReceberPagamento : ServicoAkilSmallBusiness<ContaPagarReceberPagamento, ValidacaoContasPagarReceberPagamento, ConversorContasPagarReceberPagamento>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioContaPagarReceberPagamento _repositorioContasPagarReceberPagamento;

        #endregion

        #region " CONSTRUTOR "

        public ServicoContasPagarReceberPagamento()
            : base(false, false)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<ContaPagarReceberPagamento> RetorneRepositorio()
        {
            if (_repositorioContasPagarReceberPagamento == null)
            {
                _repositorioContasPagarReceberPagamento = FabricaDeRepositorios.Crie<IRepositorioContaPagarReceberPagamento>();
            }

            return _repositorioContasPagarReceberPagamento;
        }

        public override int Cadastre(ContaPagarReceberPagamento objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                int id = base.Cadastre(objetoDeNegocio);

                GereMovimentacaoCaixaAoPagarOuReceber(objetoDeNegocio);

                scope.Complete();

                return id;
            }
        }

        #endregion

        #region " VALIDAÇÃO "

        public void ValideContaPagarReceberPagamento(ContaPagarReceberPagamento contaPagarReceberParcial)
        {
            ValidacaoContasPagarReceberPagamento validacaoContasPagarReceberPagamento = new ValidacaoContasPagarReceberPagamento();

            validacaoContasPagarReceberPagamento.ValideInclusao();

            validacaoContasPagarReceberPagamento.Valide(contaPagarReceberParcial).AssegureSucesso();
        }

        #endregion

        #region " MOVIMENTAÇÃO CAIXA "

        private void GereMovimentacaoCaixaAoPagarOuReceber(ContaPagarReceberPagamento contaPagarRecberParcial)
        {
            if (contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.BOLETOBANCARIO
                || contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDITOINTERNO
                || contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CREDIARIOPROPRIO
                || contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DEPOSITOBANCARIO 
                || contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.DUPLICATA)
                return;

            if ((contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAOCREDITO
                && contaPagarRecberParcial.ContaPagarReceber.OrigemDocumento == EnumOrigemDocumento.PEDIDODEVENDAS)
                || (contaPagarRecberParcial.FormaPagamento.TipoFormaPagamento == EnumTipoFormaPagamento.CARTAODEBITO
                    && contaPagarRecberParcial.ContaPagarReceber.OrigemDocumento == EnumOrigemDocumento.PEDIDODEVENDAS)
                )
                return;

                var contaPagarReceber = contaPagarRecberParcial.ContaPagarReceber;

            var repositorioCaixa = FabricaDeRepositorios.Crie<IRepositorioCaixa>();
            var repositorioMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioMovimentacaoCaixa>();

            var caixa = repositorioCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);
            var movimentacaoCaixa = repositorioMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            var repositorioItemMovimentacaoCaixa = FabricaDeRepositorios.Crie<IRepositorioItemMovimentacaoCaixa>();

            ItemMovimentacaoCaixa itemMovimentacaoCaixa = new ItemMovimentacaoCaixa();
            itemMovimentacaoCaixa.DataHora = DateTime.Now;

            itemMovimentacaoCaixa.DataHora = DateTime.Now;
            itemMovimentacaoCaixa.FormaPagamento = contaPagarRecberParcial.FormaPagamento;

            itemMovimentacaoCaixa.MovimentacaoCaixa = movimentacaoCaixa;

            itemMovimentacaoCaixa.Parceiro = new Pessoa { Id = contaPagarReceber.Pessoa.Id };
            itemMovimentacaoCaixa.TipoMovimentacao = contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? EnumTipoMovimentacaoCaixa.ENTRADAACRESCIMO : EnumTipoMovimentacaoCaixa.SAIDASANGRIA;
            itemMovimentacaoCaixa.ItemDeEntrada = contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER ? true : false;
            itemMovimentacaoCaixa.Valor = Math.Abs(contaPagarRecberParcial.Valor);
            itemMovimentacaoCaixa.NumeroDocumentoOrigem = contaPagarRecberParcial.Id;

            itemMovimentacaoCaixa.HistoricoMovimentacoes = "Nr. Documento :" + contaPagarReceber.NumeroDocumento + "; Nr Transação: " + contaPagarReceber.Id;

            if (contaPagarReceber.TipoOperacao == EnumTipoOperacaoContasPagarReceber.RECEBER)
            {
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.CONTARECEBER;
            }
            else
            {
                itemMovimentacaoCaixa.OrigemMovimentacaoCaixa = EnumOrigemMovimentacaoCaixa.CONTAPAGAR;
            }

            repositorioItemMovimentacaoCaixa.Cadastre(itemMovimentacaoCaixa);
        }

        #endregion

        public void EstorneRegistro(ContaPagarReceberPagamento pagamentoRealizado)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var pagamento = Consulte(pagamentoRealizado.Id);

                if (pagamento.EstahEstornado)
                {
                    throw new Exception("Este pagamento já está estornado.");
                }

                pagamento.EstahEstornado = true;

                if (pagamento.ContaPagarReceber.ValorPago == 0)
                    pagamento.ContaPagarReceber.ValorPago = 0;
                else                    
                    pagamento.ContaPagarReceber.ValorPago -= pagamento.Valor;

                this.Atualize(pagamento);

                ServicoItemMovimentacaoCaixa servicoItemMovimentacaoCaixa = new ServicoItemMovimentacaoCaixa(false, false);
                var listaItensMovimentacoes = servicoItemMovimentacaoCaixa.ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoCaixa.CONTARECEBER, pagamento.Id);

                if (listaItensMovimentacoes.Count == 0)
                {
                    listaItensMovimentacoes = servicoItemMovimentacaoCaixa.ConsulteListaItensPorNumeroDocumentoOrigemAtivos(EnumOrigemMovimentacaoCaixa.CONTAPAGAR, pagamento.Id);
                }

                if (listaItensMovimentacoes.Count > 0)
                {
                    servicoItemMovimentacaoCaixa.EstorneItemMovimentacaoCaixa(listaItensMovimentacoes[0]);
                }

                ServicoContasPagarReceber servicoContasPagarReceber = new ServicoContasPagarReceber(false, false);
                servicoContasPagarReceber.EstornarContaPagarReceber(pagamento.ContaPagarReceber);

                scope.Complete();
            }
        }
    }
}
