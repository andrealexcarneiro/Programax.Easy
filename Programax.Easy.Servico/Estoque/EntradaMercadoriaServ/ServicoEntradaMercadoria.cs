using System;
using System.Collections.Generic;
using System.Transactions;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Estoque.EntradaMercadoriaObj.Repositorio;
using Programax.Easy.Negocio.Estoque.Enumeradores;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ContasPagarReceberPagamentoObj.Repositorio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.ServicoBase;
using Programax.Infraestrutura.Negocio.Fabricas;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Servico.Servicos;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using System.Linq;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.Repositorio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;

namespace Programax.Easy.Servico.Estoque.EntradaMercadoriaServ
{
    [Funcionalidade(EnumFuncionalidade.ENTRADAMERCADORIAS)]
    public class ServicoEntradaMercadoria : ServicoBase<EntradaMercadoria, ValidacaoEntradaMercadoria, ConversorEntradaMercadoria>
    {
        #region " VARIÁVEIS PRIVADAS "

        private IRepositorioEntradaMercadoria _repositorioEntrada;

        #endregion

        #region " CONSTRUTOR "

        public ServicoEntradaMercadoria()
        {
            RetorneRepositorio();
        }

        #endregion

        #region " MÉTODO SOBRESCRITOS "

        public override IRepositorioBase<EntradaMercadoria> RetorneRepositorio()
        {
            if (_repositorioEntrada == null)
            {
                _repositorioEntrada = FabricaDeRepositorios.Crie<IRepositorioEntradaMercadoria>();
            }

            return _repositorioEntrada;
        }

        public override int Cadastre(EntradaMercadoria objetoDeNegocio)
        {
            objetoDeNegocio.DataCadastro = DateTime.Now.Date;
            objetoDeNegocio.UsuarioCadastro = Sessao.PessoaLogada;

            return base.Cadastre(objetoDeNegocio);
        }

        public override void Atualize(EntradaMercadoria objetoDeNegocio)
        {
            objetoDeNegocio.UsuarioCadastro = Sessao.PessoaLogada;

            base.Atualize(objetoDeNegocio);
        }

        #endregion

        #region " CONCLUSAO E CANCELAMENTO ENTRADA "

        public void ValideConculsao(EntradaMercadoria entradaMercadoria)
        {
            entradaMercadoria.StatusEntrada = EnumStatusEntrada.CONCLUIDA;

            if (entradaMercadoria.Id == 0)
            {
                ValideInclusao(entradaMercadoria);
            }
            else
            {
                ValideAtualizacao(entradaMercadoria);
            }
        }

        public void ConcluaEntrada(EntradaMercadoria entradaMercadoria)
        {
            using (var scope = new TransactionScope())
            {
                entradaMercadoria.StatusEntrada = EnumStatusEntrada.CONCLUIDA;
                entradaMercadoria.UsuarioCadastro = Sessao.PessoaLogada;

                ValideConculsao(entradaMercadoria);

                AtualizaPrecoCustoECompra(entradaMercadoria);
                InsiraMovimentacaoDeEntradaOuSaidaDeProdutos(entradaMercadoria);
                InsiraContasPagar(entradaMercadoria);

                var objetoDeNegocioParaPersistencia = ConvertaObjetoParaPersistencia(entradaMercadoria);

                if (entradaMercadoria.Id == 0)
                {
                    IncluirObjetoNaBaseDeDados(entradaMercadoria, objetoDeNegocioParaPersistencia);
                }
                else
                {
                    AtualizeObjetoNaBaseDeDados(objetoDeNegocioParaPersistencia);
                }

                scope.Complete();
            }
        }

        public void CancelamentoEntrada(int idEntradaMercadoria)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                var entrada = _repositorioEntrada.Consulte(idEntradaMercadoria);

                var statusAnterior = entrada.StatusEntrada;

                entrada.StatusEntrada = EnumStatusEntrada.CANCELADA;

                _repositorioEntrada.Atualize(entrada);

                if (statusAnterior == EnumStatusEntrada.CONCLUIDA)
                {
                    InsiraMovimentacaoDeEntradaOuSaidaDeProdutos(entrada);
                    CancelarContasPagar(entrada);
                }

                scope.Complete();
            }
        }

        private void AtualizaPrecoCustoECompra(EntradaMercadoria entradaMercadoria)
        {
            if (!entradaMercadoria.AtualizaPrecoCusto)
            {
                return;
            }

            List<int> listaIds = entradaMercadoria.ListaDeItens.Select<ItemEntrada, int>(x => x.Produto.Id).ToList();

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            var repositorioProduto = FabricaDeRepositorios.Crie<IRepositorioProduto>();
            var listaProdutos = repositorioProduto.ConsulteListaPorId(listaIds);

            foreach (var item in entradaMercadoria.ListaDeItens)
            {
                var produto = listaProdutos.FirstOrDefault(x => x.Id == item.Produto.Id);

                produto.FormacaoPreco = produto.FormacaoPreco ?? new FormacaoPrecoProduto();

                produto.FormacaoPreco.PrecoCompra = item.ValorUnitario - (item.ValorDesconto / item.Quantidade);
                produto.FormacaoPreco.ValorFreteCompra = item.ValorFrete / item.Quantidade;
                produto.FormacaoPreco.PercentualIcmsCompra = item.PercentualIcms;
                produto.FormacaoPreco.PercentualIcmsSTCompra = item.AliquotaST;
                produto.FormacaoPreco.PercentualReducaoIcmsCompra = item.PercentualReducao;
                produto.FormacaoPreco.PercentualIpiCompra = item.PercentualIpi;
                
                var precoCustoEVenda = CalculePrecoDeCustoEVenda(produto.FormacaoPreco.PrecoCompra.ToDouble(), produto.FormacaoPreco.ValorFreteCompra.ToDouble(), 
                                    produto.FormacaoPreco.PercentualIcmsCompra.ToDouble(), produto.FormacaoPreco.PercentualIpiCompra.ToDouble(),
                                    produto.FormacaoPreco.PercentualDespesasFixasVenda.ToDouble(), produto.FormacaoPreco.PercentualDespesasVariaveisVenda.ToDouble(), 0,
                                    produto.FormacaoPreco.PercentualOutrasDespesasVenda.ToDouble(), produto.FormacaoPreco.PercentualFreteVenda.ToDouble(), produto.FormacaoPreco.PercentualComissoesVenda.ToDouble(),
                                    produto.FormacaoPreco.PercentualLucro.ToDouble());
                
                produto.FormacaoPreco.ValorVenda = produto.FormacaoPreco.Markup != null ? (precoCustoEVenda * produto.FormacaoPreco.Markup) + precoCustoEVenda : produto.FormacaoPreco.ValorVenda;
                
                //Agora, todos os impostos são carregados para empresas do regime Simples ou Normal
            }

            repositorioProduto.AtualizeLista(listaProdutos);
        }
        
        private double CalculePrecoDeCustoEVenda( double precoCompra, double valorFreteCompra, double percentualIcmsCompra, double percentualIpiCompra,
                                                    double percentualDespesasFixas, double percentualDespesasVariaveis, double percentualImpostos, 
                                                    double percentualOutrasDespesas, double percentualFrete, double percentualComissoes, double percentualLucro)
        {
            double precoCusto;

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            ServicoProduto servicoProduto = new ServicoProduto();

            if (empresa.DadosEmpresa.CodigoRegimeTributario == EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                if (empresa.DadosEmpresa.Cnae.Descricao.Contains("Comercio"))
                {
                    

                    precoCusto = servicoProduto.CalculePrecoCusto(precoCompra,
                                                                                                valorFreteCompra,
                                                                                                percentualIcmsCompra, //é informado para retirar, por isso sem sinal, ou seja, (+). Com sinal negativo (-) acrescenta
                                                                                                percentualIpiCompra,
                                                                                                0,
                                                                                                0);
                    
                }
                else
                {
                    precoCusto = servicoProduto.CalculePrecoCusto(precoCompra,
                                                                                               valorFreteCompra,
                                                                                               percentualIcmsCompra, //é informado para retirar, por isso sem sinal, ou seja, (+). Com sinal negativo (-) acrescenta.
                                                                                               -percentualIpiCompra,
                                                                                               0,
                                                                                               0);                   
                }
            }
            else
            {
                    precoCusto = servicoProduto.CalculePrecoCusto(precoCompra,
                                                                                            valorFreteCompra,
                                                                                            0, // No "Regime Simples" não soma e nem subrai o icms de compra.
                                                                                            percentualIpiCompra,
                                                                                            0,
                                                                                            0);                
            }

            return servicoProduto.CalculePrecoVenda(precoCusto,
                                                                                          percentualDespesasFixas,
                                                                                          percentualDespesasVariaveis,
                                                                                          percentualImpostos,
                                                                                          percentualOutrasDespesas,
                                                                                          percentualFrete,
                                                                                          percentualComissoes,
                                                                                          percentualLucro);
        }

        private void InsiraMovimentacaoDeEntradaOuSaidaDeProdutos(EntradaMercadoria entradaMercadoria)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao(false, false);

            MovimentacaoBase movimentacaoBase = new MovimentacaoBase();

            movimentacaoBase.DataCadastro = DateTime.Now;
            movimentacaoBase.DataMovimentacao = entradaMercadoria.DataMovimentacao;
            movimentacaoBase.FornecedorOuCliente = entradaMercadoria.Fornecedor;
            movimentacaoBase.OrigemMovimentacao = EnumOrigemMovimentacao.ENTRADADEMERCADORIA;

            if (entradaMercadoria.StatusEntrada == EnumStatusEntrada.CONCLUIDA)
            {
                movimentacaoBase.Observacoes = entradaMercadoria.Observacoes;
                movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.ENTRADA;
            }
            else
            {
                movimentacaoBase.Observacoes = "ENTRADA CANCELADA\n\nNR. NOTA: " + entradaMercadoria.NumeroNota +
                                                                  "\nSÉRIE: " + entradaMercadoria.Serie +
                                                                  "\nFORNECEDOR: " + entradaMercadoria.Fornecedor.Id + " - " + entradaMercadoria.Fornecedor.DadosGerais.Razao;

                movimentacaoBase.TipoMovimentacao = EnumTipoMovimentacao.SAIDA;
            }

            foreach (var item in entradaMercadoria.ListaDeItens)
            {
                ItemMovimentacao itemMovimentacao = new ItemMovimentacao();

                itemMovimentacao.Produto = item.Produto;
                itemMovimentacao.Quantidade = item.Quantidade;

                movimentacaoBase.ListaDeItens.Add(itemMovimentacao);
            }

            servicoMovimentacao.Cadastre(movimentacaoBase);
        }

        private void InsiraContasPagar(EntradaMercadoria entradaMercadoria)
        {
            ServicoContasPagar servicoContasPagar = new ServicoContasPagar(false, false);

            foreach (var financeiroEntrada in entradaMercadoria.ListaFinanceiroEntrada)
            {
                ContaPagarReceber contaPagarReceber = new ContaPagarReceber();

                contaPagarReceber.FormaPagamento = financeiroEntrada.FormaPagamento;
                contaPagarReceber.NumeroDocumento = financeiroEntrada.NumeroDocumento;
                contaPagarReceber.Pessoa = entradaMercadoria.Fornecedor;
                contaPagarReceber.Status = EnumStatusContaPagarReceber.ABERTO;
                contaPagarReceber.Usuario = Sessao.PessoaLogada;
                contaPagarReceber.ValorParcela = financeiroEntrada.ValorDuplicata;
                contaPagarReceber.DataEmissao = entradaMercadoria.DataEmissao.GetValueOrDefault();
                contaPagarReceber.DataVencimento = financeiroEntrada.DataVencimento;

                servicoContasPagar.Cadastre(contaPagarReceber);

                financeiroEntrada.ContaPagar = contaPagarReceber;
            }
        }

        private void CancelarContasPagar(EntradaMercadoria entradaMercadoria)
        {
            foreach (var financeiroEntrada in entradaMercadoria.ListaFinanceiroEntrada)
            {
                if (financeiroEntrada.ContaPagar != null)
                {
                    var repositorioContaPagarReceber = FabricaDeRepositorios.Crie<IRepositorioContasPagarReceber>();

                    var contaPagarReceber = repositorioContaPagarReceber.Consulte(financeiroEntrada.ContaPagar.Id);

                    contaPagarReceber.Status = EnumStatusContaPagarReceber.INATIVO;

                    repositorioContaPagarReceber.Atualize(contaPagarReceber);
                }
            }
        }

        #endregion

        #region " VALIDAÇÕES "

        public void ValideItem(ItemEntrada item)
        {
            ValidacaoItemEntrada validacaoItemEntrada = new ValidacaoItemEntrada();

            validacaoItemEntrada.ValideInclusao();

            validacaoItemEntrada.Valide(item).AssegureSucesso();
        }

        public void ValideFinanceiro(FinanceiroEntrada financeiroEntrada)
        {
            ValidacaoFinanceiroEntrada validacaoFinanceiroEntrada = new ValidacaoFinanceiroEntrada();
            validacaoFinanceiroEntrada.ValideInclusao();

            validacaoFinanceiroEntrada.Valide(financeiroEntrada).AssegureSucesso();
        }

        #endregion

        #region "Calculos"
        //Retorna o total do frente com precisão
        public double RateieFreteEntrada(double valorFrete, List<ItemEntrada> listaItens)
        {
            double totalBrutoItens = listaItens.Sum(i => i.ValorUnitario * i.Quantidade).ToDouble();
            double fatorRateio;
            double valorFreteRateioComPrecisao=0;

            int totalItens = listaItens.Count;

            for (int contadorItens = 0; contadorItens < totalItens; contadorItens++)
            {
                ItemEntrada item = listaItens[contadorItens];
                
                fatorRateio = (item.Quantidade.ToDouble() * item.ValorUnitario.ToDouble() * (double)100) / totalBrutoItens;

                item.ValorFrete = Math.Round((valorFrete * (fatorRateio / (double)100)), 2);
                
                CalculosPedidoDeVenda calculoPedido = new CalculosPedidoDeVenda();

                item.ValorTotal = calculoPedido.RetorneValorTotalItem(item.ValorUnitario.ToDouble(), item.Quantidade, 0, item.ValorFrete.ToDouble(), 0, 0);

                valorFreteRateioComPrecisao = (valorFreteRateioComPrecisao + item.ValorFrete.ToDouble());
            }

            return valorFreteRateioComPrecisao;
        }

        #endregion

        #region " CONSULTAS "

        public List<EntradaMercadoria> ConsulteLista(DateTime? dataInicialEmissao,
                                                                          DateTime? dataFinalEmissao,
                                                                          DateTime? dataInicialEntrada,
                                                                          DateTime? dataFinalEntrada,
                                                                          string numeroNota,
                                                                          string razaoSocialFornecedor,
                                                                          EnumStatusEntrada? status, int tipo)
        {
            return _repositorioEntrada.ConsulteLista(dataInicialEmissao,
                                                                      dataFinalEmissao,
                                                                      dataInicialEntrada,
                                                                      dataFinalEntrada,
                                                                      numeroNota,
                                                                      razaoSocialFornecedor,
                                                                      status, tipo);
        }

        public List<EntradaMercadoria> ConsulteListaNumero(string numeroNota, int tipo)
                                                                         
        {
            return _repositorioEntrada.ConsulteListaNumero(numeroNota, tipo);
        }

        public EntradaMercadoria ConsulteNotaEntrada(string numeroNota)
        {
            return _repositorioEntrada.ConsulteNotaEntrada(numeroNota);
        }


        public ItemEntrada ConsulteUltimaEntradaProduto(Produto produto)
        {
            return _repositorioEntrada.ConsulteUltimaEntradaProduto(produto);
        }

        #endregion
    }
}
