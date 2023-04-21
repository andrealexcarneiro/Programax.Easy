using System.Collections.Generic;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.PlanoContasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using System;
using System.Linq;
using Programax.Infraestrutura.Negocio.Validacoes;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.ItemMovimentacaoCaixaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.CaixaObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.MovimentacaoCaixaObj.Repositorio;
using System.Transactions;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberPagamentoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Financeiro.ChequeServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.BancoParaMovimentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;

namespace Programax.Easy.Servico.Financeiro.ContasPagarReceberServ
{
    public class ServicoContasPagarReceber : ServicoAkilSmallBusiness<ContaPagarReceber, ValidacaoContasPagarReceber, ConversorContasPagarReceber>
    {
        protected IRepositorioContasPagarReceber _repositorioContasPagarReceber;        

        private string _observacoesHistoricoVencimento;

        #region " CONSTRUTOR "

        public ServicoContasPagarReceber()
        {
            RetorneRepositorio();
        }

        public ServicoContasPagarReceber(bool verificarPermissao, bool limparSessao)
            : base(verificarPermissao, limparSessao)
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODOS SOBRESCRITOS "

        public override IRepositorioBase<ContaPagarReceber> RetorneRepositorio()
        {
            if (_repositorioContasPagarReceber == null)
            {
                _repositorioContasPagarReceber = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();
            }

            return _repositorioContasPagarReceber;
        }

        public override void CadastreLista(List<ContaPagarReceber> listaObjetoDeNegocio)
        {
            foreach (var item in listaObjetoDeNegocio)
            {
                HistoricoAlteracaoVencimento historico = new HistoricoAlteracaoVencimento();

                historico.DataAlteracao = DateTime.Now;
                historico.DataVencimento = item.DataVencimento.GetValueOrDefault();
                historico.Desconto = item.Desconto;
                historico.Juros = item.Juros;
                historico.Multa = item.Multa;
                historico.NumeroAlteracao = 1;
                historico.Usuario = item.Usuario;
                historico.Valor = item.ValorParcela;

                item.ListaHistoricoAlteracoesVencimento.Add(historico);
            }

            base.CadastreLista(listaObjetoDeNegocio);
        }

        public void Atualize(ContaPagarReceber objetoDeNegocio, string observacoesHistoricoVencimento)
        {
            _observacoesHistoricoVencimento = observacoesHistoricoVencimento;

            Atualize(objetoDeNegocio);
        }

        public override void Atualize(ContaPagarReceber objetoDeNegocio)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                var contaPagarReceberSemSesao = _repositorioContasPagarReceber.ConsulteSemSessao(objetoDeNegocio.Id);

                if (contaPagarReceberSemSesao.DataVencimento != objetoDeNegocio.DataVencimento)
                {
                    HistoricoAlteracaoVencimento historicoAlteracaoVencimento = new HistoricoAlteracaoVencimento();

                    historicoAlteracaoVencimento.DataAlteracao = DateTime.Now;
                    historicoAlteracaoVencimento.DataVencimento = objetoDeNegocio.DataVencimento.GetValueOrDefault();
                    historicoAlteracaoVencimento.Desconto = objetoDeNegocio.Desconto;
                    historicoAlteracaoVencimento.Juros = objetoDeNegocio.Juros;
                    historicoAlteracaoVencimento.Multa = objetoDeNegocio.Multa;
                    historicoAlteracaoVencimento.Usuario = Sessao.PessoaLogada;
                    historicoAlteracaoVencimento.Valor = objetoDeNegocio.ValorParcela;
                    historicoAlteracaoVencimento.ValorTotal = objetoDeNegocio.ValorTotal;
                    historicoAlteracaoVencimento.Observacoes = _observacoesHistoricoVencimento;

                    historicoAlteracaoVencimento.NumeroAlteracao = objetoDeNegocio.ListaHistoricoAlteracoesVencimento.Count + 1;

                    objetoDeNegocio.ListaHistoricoAlteracoesVencimento.Add(historicoAlteracaoVencimento);
                }

                base.Atualize(objetoDeNegocio);

                scope.Complete();
            }
        }

        public void Atualize(ContaPagarReceber objetoDeNegocio, ContaPagarReceberPagamento contaPagarReceberPagamento)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 5, 0)))
            {
                Atualize(objetoDeNegocio);

                ServicoContasPagarReceberPagamento servicoContasPagarReceberPagamento = new ServicoContasPagarReceberPagamento();
                servicoContasPagarReceberPagamento.Cadastre(contaPagarReceberPagamento);

                scope.Complete();
            }
        }

        #endregion

        #region " CONSULTAS "

        public virtual List<ContaPagarReceber> ConsulteLista(Pessoa pessoa, EnumTipoOperacaoContasPagarReceber? tipoOperacao,
                                                                                     EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                                     FormaPagamento formaPagamento,
                                                                                     PlanoDeContas planoDeContas,
                                                                                     EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                                     DateTime? dataInicialPeriodo,
                                                                                     DateTime? dataFinalPeriodo,
                                                                                     double? valor=null,
                                                                                     int? categoriaId = null,
                                                                                     int? Dre = null)
        {

            return _repositorioContasPagarReceber.ConsulteLista(pessoa, tipoOperacao,
                                                                                     statusContaPagarReceber,
                                                                                     formaPagamento,
                                                                                     planoDeContas,
                                                                                     tipoDataFiltrar,
                                                                                     dataInicialPeriodo,
                                                                                     dataFinalPeriodo,
                                                                                     valor, 
                                                                                     categoriaId, Dre);
        }
        public virtual List<ContaPagarReceber> ConsulteListaII(Pessoa pessoa, EnumTipoOperacaoContasPagarReceber? tipoOperacao,
                                                                                     EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                                     FormaPagamento formaPagamento,
                                                                                     PlanoDeContas planoDeContas,
                                                                                     EnumDataFiltrarContasPagarReceber? tipoDataFiltrar,
                                                                                     DateTime? dataInicialPeriodo,
                                                                                     DateTime? dataFinalPeriodo,
                                                                                     double? valor = null,
                                                                                     int? categoriaId = null,
                                                                                     int? Dre = null)
        {

            return _repositorioContasPagarReceber.ConsulteListaII(pessoa, tipoOperacao,
                                                                                     statusContaPagarReceber,
                                                                                     formaPagamento,
                                                                                     planoDeContas,
                                                                                     tipoDataFiltrar,
                                                                                     dataInicialPeriodo,
                                                                                     dataFinalPeriodo,
                                                                                     valor,
                                                                                     categoriaId, Dre);
        }


        public List<ContaPagarReceber> ConsulteListaFazendoFetchComParceiroEEnderecos(Pessoa pessoa,
                                                                                                                                EnumDataFiltrarContasPagarReceber? dataFiltrar,
                                                                                                                                DateTime? dataInicialPeriodo,
                                                                                                                                DateTime? dataFinalPeriodo,
                                                                                                                                EnumStatusContaPagarReceber? statusContaPagarReceber,
                                                                                                                                EnumTipoOperacaoContasPagarReceber tipoOperacao,
                                                                                                                                EnumOrdenacaoPesquisaContasPagarReceber ordenacaoPesquisaContasPagarReceber,
                                                                                                                                int? categoriaId = null)
        {
            return _repositorioContasPagarReceber.ConsulteListaFazendoFetchComParceiroEEnderecos(pessoa,
                                                                                                                                             dataFiltrar,
                                                                                                                                             dataInicialPeriodo,
                                                                                                                                             dataFinalPeriodo,
                                                                                                                                             statusContaPagarReceber,
                                                                                                                                             tipoOperacao,
                                                                                                                                             ordenacaoPesquisaContasPagarReceber,
                                                                                                                                             categoriaId);
        }

        public List<ContaPagarReceber> ConsulteListaDeRecebimentoPorPedido(string numeroPedido)
        {
            return _repositorioContasPagarReceber.ConsulteListaDeRecebimentoPorPedido(numeroPedido);
        }


        public VWTotalAReceber ConsuteTotalARebecerHoje()
        {
            return _repositorioContasPagarReceber.ConsuteTotalARebecerHoje();
        }

        public VWTotalAReceberEmAtraso ConsuteTotalARebecerEmAtraso()
        {
            return _repositorioContasPagarReceber.ConsuteTotalARebecerEmAtraso();
        }

        public VWTotalAPagar ConsuteTotalAPagarHoje()
        {
            return _repositorioContasPagarReceber.ConsuteTotalAPagarHoje();
        }

        public VWTotalAPagarEmAtraso ConsuteTotalAPagarEmAtraso()
        {
            return _repositorioContasPagarReceber.ConsuteTotalAPagarEmAtraso();
        }

        public List<VWAReceberAnual> ConsulteTotalAReceberAnual()
        {
            return _repositorioContasPagarReceber.ConsulteTotalAReceberAnual();
        }

        public List<VWAPagarAnual> ConsulteTotalAPagarAnual()
        {
            return _repositorioContasPagarReceber.ConsulteTotalAPagarAnual();
        }

        public List<VWAReceberMensal> ConsulteTotalAReceberMensal()
        {
            return _repositorioContasPagarReceber.ConsulteTotalAReceberMensal();
        }

        public List<VWAPagarMensal> ConsulteTotalAPagarMensal()
        {
            return _repositorioContasPagarReceber.ConsulteTotalAPagarMensal();
        }

        public List<VWAReceberTodosDiasDoAno> ConsulteTotalAReceberSemanal(DateTime DataInicial, DateTime DataFinal)
        {
            return _repositorioContasPagarReceber.ConsulteTotalAReceberSemanal(DataInicial, DataFinal);
        }

        public List<VWAPagarTodosDiasDoAno> ConsulteTotalAPagarSemanal(DateTime DataInicial, DateTime DataFinal)
        {
            return _repositorioContasPagarReceber.ConsulteTotalAPagarSemanal(DataInicial, DataFinal);
        }
        
        #endregion

        #region " GERAÇÃO DE CONTAS A PAGAR OU RECEBER "

        public List<ContaPagarReceber> GereContasPagarReceber(Pessoa parceiro,
                                                                                            DateTime dataEmissao,
                                                                                            DateTime dataVencimento,
                                                                                            string numeroDocumento,
                                                                                            EnumOrigemDocumento? origemDocumento,
                                                                                            FormaPagamento formaPagamento,
                                                                                            PlanoDeContas planoDeContas,
                                                                                            BancoParaMovimento bancoParaMovimento,
                                                                                            CategoriaFinanceira categoriaFinanceira,
                                                                                            OperadorasCartao operadorasCartao,
                                                                                            string historico,
                                                                                            Pessoa usuario,
                                                                                            EnumPeriodicidade periodicidade,
                                                                                            double valorTotal,
                                                                                            double multa,
                                                                                            double juros,
                                                                                            bool multaEhPercentual,
                                                                                            bool jurosEhPercentual,
                                                                                            int quantidadeParcelas,
                                                                                            double valorEntrada,
                                                                                            int chequeId=0)
        {
            List<ContaPagarReceber> listaDeTitulos = GereParcelasContasPagarReceber(parceiro,
                                                                                                                          dataEmissao,
                                                                                                                          dataVencimento,
                                                                                                                          numeroDocumento,
                                                                                                                          origemDocumento,
                                                                                                                          formaPagamento,
                                                                                                                          planoDeContas,
                                                                                                                          bancoParaMovimento,
                                                                                                                          categoriaFinanceira,
                                                                                                                          operadorasCartao,
                                                                                                                          historico,
                                                                                                                          usuario,
                                                                                                                          periodicidade,
                                                                                                                          valorTotal,
                                                                                                                          multa,
                                                                                                                          juros,
                                                                                                                          multaEhPercentual,
                                                                                                                          jurosEhPercentual,
                                                                                                                          quantidadeParcelas,
                                                                                                                          valorEntrada,
                                                                                                                          chequeId);


            InsiraDiferencaNaPrimeiraOuUltimaParcela(listaDeTitulos, valorTotal, false);

            return listaDeTitulos;
        }

        private List<ContaPagarReceber> GereParcelasContasPagarReceber(Pessoa parceiro,
                                                                                                         DateTime dataEmissao,
                                                                                                         DateTime dataVencimento,
                                                                                                         string numeroDocumento,
                                                                                                         EnumOrigemDocumento? origemDocumento,
                                                                                                         FormaPagamento formaPagamento,
                                                                                                         PlanoDeContas planoDeContas,
                                                                                                         BancoParaMovimento bancoParaMovimento,
                                                                                                         CategoriaFinanceira categoriaFinanceira,
                                                                                                         OperadorasCartao operadorasCartao,
                                                                                                         string historico,
                                                                                                         Pessoa usuario,
                                                                                                         EnumPeriodicidade periodicidade,
                                                                                                         double valorTotal,
                                                                                                         double multa,
                                                                                                         double juros,
                                                                                                         bool multaEhPercentual,
                                                                                                         bool jurosEhPercentual,
                                                                                                         int quantidadeParcelas,
                                                                                                         double valorEntrada,
                                                                                                         int chequeId=0)
        {
            List<ContaPagarReceber> listaDeTitulos = new List<ContaPagarReceber>();

            for (int parcela = 0; parcela < quantidadeParcelas; parcela++)
            {
                ContaPagarReceber contaPagarReceber = new ContaPagarReceber();

                contaPagarReceber.DataEmissao = dataEmissao;
                contaPagarReceber.FormaPagamento = formaPagamento;
                contaPagarReceber.Pessoa = parceiro;
                contaPagarReceber.NumeroDocumento = (parcela + 1) + "-" + numeroDocumento;
                contaPagarReceber.ChequeId = chequeId;
                contaPagarReceber.OrigemDocumento = origemDocumento;
                contaPagarReceber.PlanoDeContas = planoDeContas;
                contaPagarReceber.BancoParaMovimento = bancoParaMovimento;
                contaPagarReceber.CategoriaFinanceira = categoriaFinanceira;
                contaPagarReceber.OperadorasCartao = operadorasCartao;
                contaPagarReceber.Historico = historico;
                contaPagarReceber.Usuario = usuario;
                contaPagarReceber.Multa = multa;
                contaPagarReceber.Juros = juros;
                contaPagarReceber.MultaEhPercentual = multaEhPercentual;
                contaPagarReceber.JurosEhPercentual = jurosEhPercentual;

                contaPagarReceber.ValorParcela = RetorneValorParcela(parcela, valorEntrada, valorTotal, quantidadeParcelas);

                contaPagarReceber.DataVencimento = RetorneDataVencimentoParcela(periodicidade, dataVencimento, parcela);

                listaDeTitulos.Add(contaPagarReceber);
            }

            return listaDeTitulos;
        }

        private double RetorneValorParcela(int parcela, double valorEntrada, double valorTotal, int quantidadeParcelas)
        {
            int quantidadeParcelasSemEntrada = valorEntrada > 0 ? quantidadeParcelas - 1 : quantidadeParcelas;

            double valorPorParcela = (valorTotal - valorEntrada) / (double)quantidadeParcelasSemEntrada;

            if (valorEntrada > 0 && parcela == 0)
            {
                return Math.Round(valorEntrada, 2);
            }
            else
            {
                return Math.Round(valorPorParcela, 2);
            }
        }

        private DateTime RetorneDataVencimentoParcela(EnumPeriodicidade periodicidade, DateTime dataVencimentoPrimeiraParcela, int parcela)
        {
            if (periodicidade == EnumPeriodicidade.UNICA)
            {
                return dataVencimentoPrimeiraParcela;
            }
            else if (periodicidade == EnumPeriodicidade.DIARIO)
            {
                return dataVencimentoPrimeiraParcela.AddDays(parcela);
            }
            else if (periodicidade == EnumPeriodicidade.QUINZENAL)
            {
                return dataVencimentoPrimeiraParcela.AddDays(parcela * 15);
            }
            else if (periodicidade == EnumPeriodicidade.MENSAL)
            {
                return dataVencimentoPrimeiraParcela.AddMonths(parcela);
            }
            else if (periodicidade == EnumPeriodicidade.BIMESTRAL)
            {
                return dataVencimentoPrimeiraParcela.AddMonths(parcela * 2);
            }
            else if (periodicidade == EnumPeriodicidade.TRIMESTRAL)
            {
                return dataVencimentoPrimeiraParcela.AddMonths(parcela * 3);
            }
            else if (periodicidade == EnumPeriodicidade.SEMESTRAL)
            {
                return dataVencimentoPrimeiraParcela.AddMonths(parcela * 6);
            }
            else if (periodicidade == EnumPeriodicidade.ANUAL)
            {
                return dataVencimentoPrimeiraParcela.AddYears(parcela);
            }
            else if (periodicidade == EnumPeriodicidade.TRIENAL)
            {
                return dataVencimentoPrimeiraParcela.AddYears(parcela * 3);
            }
            else if (periodicidade == EnumPeriodicidade.SEMANAL)
            {
                return dataVencimentoPrimeiraParcela.AddDays(parcela * 7);
            }

            return DateTime.MinValue;
        }

        private void InsiraDiferencaNaPrimeiraOuUltimaParcela(List<ContaPagarReceber> listaDeParcelas, double valorTotal, bool inserirDiferencaNaPrimeiraParcela)
        {
            double somaValorParcelas = listaDeParcelas.Sum(parcela => parcela.ValorParcela);

            double diferenca = valorTotal - somaValorParcelas;

            if (inserirDiferencaNaPrimeiraParcela)
            {
                listaDeParcelas.FirstOrDefault().ValorParcela += diferenca;
            }
            else
            {
                listaDeParcelas.LastOrDefault().ValorParcela += diferenca;
            }
        }

        #endregion

        #region " VALIDAÇÃO "

        public virtual void ValideGeracaoParcelasContasPagarReceber(ContaPagarReceber contaPagarReceber, int quantidadeDeParcelas)
        {
            ValidacaoContasPagarReceber validacaoContasPagarReceber = new ValidacaoContasPagarReceber();

            validacaoContasPagarReceber.ValideInclusao();

            var inconsistencias = validacaoContasPagarReceber.Valide(contaPagarReceber);

            inconsistencias.ListaDeInconsistencias.ForEach(x => x.Mensagem = x.Mensagem.Replace("Valor Parcela", "Valor Total"));

            if (quantidadeDeParcelas == 0)
            {
                Inconsistencia inconsistencia = new Inconsistencia { Mensagem = "Quantidade de parcelas tem que ser maior que zero." };

                inconsistencias.ListaDeInconsistencias.Add(inconsistencia);
            }

            inconsistencias.AssegureSucesso();
        }

        public void ValideContaPagarReceber(ContaPagarReceber contaPagarReceber)
        {
            ValidacaoContasPagarReceber validacaoContasPagarReceber = new ValidacaoContasPagarReceber();

            validacaoContasPagarReceber.ValideInclusao();

            validacaoContasPagarReceber.Valide(contaPagarReceber).AssegureSucesso();
        }

        public virtual void ValideParcelas(List<ContaPagarReceber> listaDeContaspagarReceber)
        {
            ValidacaoContasPagarReceber validacaoContasPagarReceber = new ValidacaoContasPagarReceber();

            validacaoContasPagarReceber.ValideInclusao();

            InconsistenciasDeValidacao inconsistencias = new InconsistenciasDeValidacao();

            foreach (var item in listaDeContaspagarReceber)
            {
                var inconsistenciasValidacao = validacaoContasPagarReceber.Valide(item);

                inconsistencias.ListaDeInconsistencias.AddRange(inconsistenciasValidacao.ListaDeInconsistencias);
            }

            inconsistencias.AssegureSucesso();
        }

        #endregion

        #region " BAIXAR OU INATIVAR  "

        public void BaixeContaPagarReceber(ContaPagarReceber contaPagarReceber, ContaPagarReceberPagamento contaPagarReceberPagamento)
        {
            contaPagarReceber.ChequeId = contaPagarReceber.ChequeId == 0? null: contaPagarReceber.ChequeId;

            contaPagarReceber.Status = EnumStatusContaPagarReceber.QUITADO;

            contaPagarReceber.ValorPago = contaPagarReceber.ValorTotal;

            Atualize(contaPagarReceber, contaPagarReceberPagamento);
        }

        public void InativeContaPagarReceber(ContaPagarReceber contaPagarReceber, bool cancelarPedido=true, bool reservePedido=false)
        {
            contaPagarReceber.Status = EnumStatusContaPagarReceber.INATIVO;
            contaPagarReceber.DataPagamento = null;
            
            Atualize(contaPagarReceber);

            if (cancelarPedido)
            {
                if (contaPagarReceber.OrigemDocumento != EnumOrigemDocumento.DIRETOCONTASARECEBER)
                {
                    //Se for pedido. Cancelar o pedido atrelado a esta conta.
                    char delimitador = '-';
                    string[] separaParcela = contaPagarReceber.NumeroDocumento.Split(delimitador);
                    int numeroDuplicata = int.Parse(separaParcela[0].Trim());

                    ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda();

                    var pedidoVenda = servicoPedidoVenda.Consulte(numeroDuplicata);


                    if (pedidoVenda != null && !reservePedido)
                    {
                        servicoPedidoVenda.CancelePedidoDeVenda(pedidoVenda.Id);
                    }
                    else if(reservePedido)
                    {
                        servicoPedidoVenda.CanceleOuRecusePedidoDeVenda(pedidoVenda, EnumStatusPedidoDeVenda.RESERVADO);
                    }
                }
            }
        }

        public void CanceleContaPagarReceber(ContaPagarReceber contaPagarReceber, int numeroPedido)
        {
            contaPagarReceber.Status = EnumStatusContaPagarReceber.CANCELADO;
            contaPagarReceber.DataPagamento = null;
            
            Atualize(contaPagarReceber);

            ServicoPedidoDeVenda servicoPedidoVenda = new ServicoPedidoDeVenda();

            var pedido = servicoPedidoVenda.Consulte(numeroPedido);

            if (pedido != null)
            {
                servicoPedidoVenda.ValidePedidoParaEstornoAoCancelarRecebimentos(pedido);
                servicoPedidoVenda.CancelePedidoDeVenda(numeroPedido);
            }
        }

        public void EstornarContaPagarReceber(ContaPagarReceber contaPagarReceber)
        {
            contaPagarReceber.Status = EnumStatusContaPagarReceber.ABERTO;
            contaPagarReceber.DataPagamento = null;

            Atualize(contaPagarReceber);
        }

        #endregion

        #region " MOVIMENTAÇÃO CAIXA "

        private void GereMovimentacaoCaixaAoPagarOuReceber(ContaPagarReceber contaPagarReceber, ContaPagarReceberPagamento contaPagarRecberParcial)
        {
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
            itemMovimentacaoCaixa.NumeroDocumentoOrigem = contaPagarReceber.Id;

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


        #region "Cheque"

        public void AtualizarChequesContaPagarReceber(ContaPagarReceber contaPagarReceber)
        {
            ServicoCheque servicoCheque = new ServicoCheque();

            var cheque = servicoCheque.Consulte(contaPagarReceber.ChequeId.ToInt());

            if (cheque == null)
            {
                cheque = servicoCheque.ConsulteChequePeloNumeroDocumento(contaPagarReceber.NumeroDocumento);
            }

            if (cheque != null)
            {
                cheque.DataEmissao = contaPagarReceber.DataEmissao;
                cheque.DataVencimento = contaPagarReceber.DataVencimento;

                if(contaPagarReceber.Status == EnumStatusContaPagarReceber.INATIVO || contaPagarReceber.Status == EnumStatusContaPagarReceber.CANCELADO)
                {
                    cheque.StatusCheque = EnumStatusCheque.INATIVO;
                    servicoCheque.Atualize(cheque);
                    return;
                }

                if (contaPagarReceber.DataPagamento != null)
                {
                    cheque.DataRecebimento = contaPagarReceber.DataPagamento;
                    cheque.StatusCheque = EnumStatusCheque.RECEBIDO;
                }
                else if (contaPagarReceber.ListaContasPagarReceberParcial != null )
                {
                    if(contaPagarReceber.ListaContasPagarReceberParcial.Count > 0)
                    {
                        cheque.DataRecebimento = contaPagarReceber.DataPagamento;
                        cheque.StatusCheque = EnumStatusCheque.ABERTODEPOSITADO;
                    }
                }
                
                servicoCheque.Atualize(cheque);
            }
        }


        #endregion
    }
}
