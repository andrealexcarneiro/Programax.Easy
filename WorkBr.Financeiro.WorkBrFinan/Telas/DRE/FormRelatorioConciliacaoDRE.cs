using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.GrupoServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.Cadastros.SubGrupoServ;
using Programax.Easy.Servico.Estoque.EntradaMercadoriaServ;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.Servico.Financeiro.GruposDreServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Infraestrutura.Negocio.Utils;
using static Programax.Easy.Servico.RegistroDeMapeamentos;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioConciliacaoDRE : Form
    {
        List<GrupoAux> _listaGrupoDetalhe = new List<GrupoAux>();

        double _saldoInicial;
        double _saldoEntrada;
        double _customercadoria;
        double _custofrete;
        double _saldoSaida;
        double _saldoOperacional;
        double _saldoDeducoes;
        double _saldoLiquida;
        double _CustoBruto;
        double _LucroLiquido;
        double _Percentual;
        double _saldoRecebimento;
        double _saldoPagamento;
        double _TodasDespesas;
        double _lucroOperacional;
        DateTime dateTimeValue;
        DateTime dateTimeValue2;
        private List<Produto> _listaProdutos;
        private string ConectionString;

        public FormRelatorioConciliacaoDRE()
        {
            InitializeComponent();

        }


        private void CarregaGridMasterDetail()
        {
            _saldoInicial = 0;
            _saldoEntrada = 0;
            _saldoSaida = 0;
            _saldoRecebimento = 0;
            _saldoPagamento = 0;
            _saldoDeducoes = 0;
            _saldoLiquida = 0;
            _saldoOperacional = 0;
            _customercadoria = 0;
            _CustoBruto = 0;
            _TodasDespesas = 0;
            _Percentual = 0;
            _LucroLiquido = 0;
            _lucroOperacional = 0;
            _listaGrupoDetalhe = new List<GrupoAux>();

            //1- Saldo Inicial de bancos e caixas 
            //carregaSaldoInicialBancosCaixas();

            //Carrega todos os grupos
            var gruposCadastrados = new ServicoGrupoDre().ConsulteLista(null, string.Empty, "A");

            foreach (var itemGrupo in gruposCadastrados)
            {
                GrupoAux itemGrupoAux = new GrupoAux();

                switch (itemGrupo.Id)
                {
                    case 1://Entradas Operacionais

                        //Recebimentos Operacionais com Detalhes na categoria

                        ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                        List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteTotalVWVendasII(dateTimeValue, dateTimeValue2, 0, true);

                        foreach (var venda in listaVWVenda)
                        {

                            var vendaII = new ServicoPedidoDeVenda().ConsulteJoinComItens(venda.Id);
                            foreach (var itemProduto in vendaII.ListaItens)
                            {
                                var Custo = new ServicoEntradaMercadoria().ConsulteUltimaEntradaProduto(itemProduto.Produto);

                                if (Custo != null)
                                {
                                    _customercadoria += Custo.Produto.FormacaoPreco.PrecoCompra.ToDouble() * itemProduto.Quantidade;
                                }
                                else
                                {
                                    var produto = new ServicoProduto().Consulte(itemProduto.Produto.Id);
                                    _customercadoria += produto.FormacaoPreco.PrecoCompra.ToDouble() * itemProduto.Quantidade;
                                }
                            }

                            _saldoEntrada += venda.ValorTotal;
                        }

                        //_customercadoria = TotalizaCustoMercadoria();

                        _customercadoria = string.Format("{0:N}", _customercadoria).ToDouble();

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaRecebimentosOperacionaisNew();





                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = _saldoEntrada;



                        break;

                    case 2://Deduções s/ receita bruta

                        var Empresa = new ServicoEmpresa().ConsulteUltimaEmpresa();
                        if (Empresa.DadosEmpresa.AliqCom.ToInt() != 0)
                        {
                            _saldoDeducoes = _saldoEntrada * Empresa.DadosEmpresa.AliqCom.ToDouble() / 100;
                        }

                        //if (Empresa.DadosEmpresa.AliqInd.ToInt() != 0)
                        //{
                        //    _saldoDeducoes = _saldoEntrada * Empresa.DadosEmpresa.AliqInd.ToDouble() / 100;
                        //}

                        //if (Empresa.DadosEmpresa.AliqServ.ToInt() != 0)
                        //{
                        //    _saldoDeducoes = _saldoEntrada * Empresa.DadosEmpresa.AliqServ.ToDouble() / 100;
                        //}
                        _saldoDeducoes = string.Format("{0:N}", _saldoDeducoes).ToDouble();

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();
                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "Simples Nacional Comercio";
                            itemSubGrupoAux.Total = _saldoDeducoes.ToDouble();
                            itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoDeducoes;
                            itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                            listaItemSubGrupoAux.Add(itemSubGrupoAux);
                        }

                        //{
                        //    SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                        //    itemSubGrupoAux.NomeSubGrupo = "Devoluções";
                        //    listaItemSubGrupoAux.Add(itemSubGrupoAux);
                        //}


                        itemGrupoAux.ListaDeSubGrupos = listaItemSubGrupoAux;



                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = _saldoDeducoes;



                        break;

                    case 3://Receita Liquida



                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        List<SubGrupoAux> listaItemSubGrupoReceita = new List<SubGrupoAux>();
                        {
                            //SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            //itemSubGrupoAux.NomeSubGrupo = "Custo Mercadoria Vendida";

                            //itemSubGrupoAux.Total = _customercadoria.ToDouble();
                            //listaItemSubGrupoReceita.Add(itemSubGrupoAux);
                            //   itemGrupoAux.ListaDeSubGrupos = carregaGrupoProdutos(); //Despesa Operacional

                        }


                        //  itemGrupoAux.ListaDeSubGrupos = carregaGrupoProdutos();

                        _saldoLiquida = _saldoEntrada - _saldoDeducoes;


                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = _saldoLiquida;



                        break;
                    case 4://Custo mercadoria

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        List<SubGrupoAux> listaItemSubGrupoCusto = new List<SubGrupoAux>();
                        {
                            //SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            //itemSubGrupoAux.NomeSubGrupo = "Custo Mercadoria Vendida";

                            //itemSubGrupoAux.Total = _customercadoria.ToDouble();
                            //listaItemSubGrupoReceita.Add(itemSubGrupoAux);
                            //   itemGrupoAux.ListaDeSubGrupos = carregaGrupoProdutos(); //Despesa Operacional

                        }


                        itemGrupoAux.ListaDeSubGrupos = carregaGrupoProdutos();

                        // _saldoLiquida = _saldoEntrada - _saldoDeducoes;


                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = _customercadoria;
                        break;

                    case 5://Lucro Bruto

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;


                        //itemGrupoAux.ListaDeSubGrupos = carregaGrupoProdutos(); //Despesa Operacional
                        _CustoBruto = _saldoLiquida - _customercadoria;
                        itemGrupoAux.TotalCategorias = _CustoBruto;
                        break;

                    case 6://Despesa Operacional

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;

                        //itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(10); //Despesa Operacional
                        //_saldoOperacional += itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(7); //Despesa Operacional
                        _saldoOperacional += itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(5); //Despesa Operacional
                        _saldoOperacional += itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);


                        itemGrupoAux.TotalCategorias = _saldoOperacional;


                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(2);

                        break;

                    case 7: //Pessoal 

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(10);//Pessoal
                        //itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(12);

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo Pagamento para Saldo Financeiro
                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        _TodasDespesas = _saldoSaida;

                        break;

                    case 8: //Despesas Comerciais

                        ////Saldo Investimento = Recebimento - Pagamento

                        //    GrupoAux itemGrupoAuxSaldo3 = new GrupoAux();
                        //    itemGrupoAuxSaldo3.NomeGrupo = itemGrupo.Descricao;
                        //itemGrupoAuxSaldo3.TotalCategorias = Math.Round((_saldoRecebimento - _saldoPagamento), 2);

                        //    _listaGrupoDetalhe.Add(itemGrupoAuxSaldo3);

                        ////**** Fim Saldo Financeiro

                        //itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        //itemGrupoAux.ListaDeSubGrupos = carregaAtividades(itemGrupo.Id);

                        ////Total do Grupo
                        //itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.FindAll(x => x.NomeSubGrupo.Contains("RECEBIMENTO")).Sum(Y => Y.Total) -
                        //                                itemGrupoAux.ListaDeSubGrupos.FindAll(x => x.NomeSubGrupo.Contains("PAGAMENTO")).Sum(Y => Y.Total);

                        //_saldoEntrada = _saldoEntrada + itemGrupoAux.TotalCategorias;

                        //break;
                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(0);//Despesas Comerciais


                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo Pagamento para Saldo Financeiro
                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        _TodasDespesas += _saldoSaida;

                        break;
                    case 9: //Despesa gerais

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(11);//Despesas Gerais

                        //itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(11); //Despesa Operacional
                        //_saldoOperacional += itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);


                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo Pagamento para Saldo Financeiro
                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        //itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(11); //Despesa Operacional
                        //_saldoOperacional += itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);


                        ////Total do Grupo
                        //itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        ////Saldo Pagamento para Saldo Financeiro
                        //_saldoPagamento += itemGrupoAux.TotalCategorias;

                        ////Soma do saldo de saída para saldo final
                        //_saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        _TodasDespesas += _saldoSaida;

                        break;
                    case 10: //Despesa tributarias
                        double Total = 0;
                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        List<SubGrupoAux> listaItem = new List<SubGrupoAux>();
                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "INMETRO/AMA/CORPO BOMBEIRO";
                            itemSubGrupoAux.Total = Total;
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItem.Add(itemSubGrupoAux);
                            }

                        }

                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "Licença Sanitária/Ambiental";
                            itemSubGrupoAux.Total = Total;
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItem.Add(itemSubGrupoAux);
                            }
                        }
                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.Total = Total;
                            itemSubGrupoAux.NomeSubGrupo = "IPTU/ITU";
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItem.Add(itemSubGrupoAux);
                            }
                        }


                        itemGrupoAux.ListaDeSubGrupos = listaItem;

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = Total; ;

                        break;
                    case 11: //lucro operacional
                        double Totallucro = 0;
                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        List<SubGrupoAux> listaItemII = new List<SubGrupoAux>();
                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "Rendimentos de Aplicações";
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItemII.Add(itemSubGrupoAux);
                            }

                        }

                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "Venda de Ativo Imobilizado(caixas/papel)";
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItemII.Add(itemSubGrupoAux);
                            }
                        }


                        itemGrupoAux.ListaDeSubGrupos = listaItemII;
                        _lucroOperacional = _CustoBruto - _saldoOperacional;
                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = _CustoBruto - _saldoOperacional;


                        break;
                    case 12: //Despesa Financeira 

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(5);//Pagamento de Investimentos

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo Pagamento para Saldo Financeiro
                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        break;
                    case 13: //Outras Despesas. não operacionais

                        double TotalDespesas = 0;
                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        List<SubGrupoAux> listaItemIII = new List<SubGrupoAux>();
                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "Receita com Venda de Sucatas";
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItemIII.Add(itemSubGrupoAux);
                            }

                        }

                        {
                            SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                            itemSubGrupoAux.NomeSubGrupo = "Multas e infrações Fiscais)";
                            if (itemSubGrupoAux.Total != 0)
                            {
                                listaItemIII.Add(itemSubGrupoAux);
                            }
                        }


                        itemGrupoAux.ListaDeSubGrupos = listaItemIII;

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = TotalDespesas;

                        break;
                    case 14: //Lucro Liquido

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        _LucroLiquido = _lucroOperacional - _saldoPagamento;
                        itemGrupoAux.TotalCategorias = _LucroLiquido;

                        break;
                    case 15: //% Mensal

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;

                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;
                        _Percentual = _LucroLiquido * 100 / _saldoLiquida;
                        itemGrupoAux.TotalCategorias = _Percentual;

                        break;
                        //case 16: //Lucro liquido Final
                        //    itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        //    _LucroLiquido = _saldoLiquida - _saldoOperacional;
                        //    itemGrupoAux.TotalCategorias = _LucroLiquido;

                        //    break;
                        //case 17: //% Mensal

                        //    itemGrupoAux.NomeGrupo = itemGrupo.Descricao;

                        //    _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //    //Soma do saldo de saída para saldo final
                        //    _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;
                        //    _Percentual = _LucroLiquido * 100 / _saldoLiquida;
                        //    itemGrupoAux.TotalCategorias = _Percentual;

                        //    break;

                }

                if (itemGrupo.Id <= 15)
                {
                    _listaGrupoDetalhe.Add(itemGrupoAux);
                }

            }

            //Saldo Final - Bancos + Caixas + Entradas - Saídas
            //GrupoAux itemGrupoAuxSaldo = new GrupoAux();
            //itemGrupoAuxSaldo.NomeGrupo = "$ SALDO FINAL $";
            //itemGrupoAuxSaldo.TotalCategorias = Math.Round((_saldoInicial + _saldoEntrada - _saldoSaida),2);

            //_listaGrupoDetalhe.Add(itemGrupoAuxSaldo);

            //Vai ficar por último, pois carrega o grid completo
            gcGridFluxoDre.Refresh();
            gcGridFluxoDre.DataSource = _listaGrupoDetalhe;

            //CarregaGraficoFluxoCaixa(grupoLista);
        }

        public List<SubGrupoAux> carregaRecebimentosOperacionais()
        {
            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();

            for (int i = 1; i <= 10; i++)
            {
                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                switch (i)
                {
                    //Id = 2 Categoria: Recebimentos Operacionais (Todas as formas de pagamentos, estão nesta categoria).

                    case 1:
                        itemSubGrupoAux.NomeSubGrupo = "DINHEIRO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 1 });

                        break;

                    case 2:
                        itemSubGrupoAux.NomeSubGrupo = "BOLETO BANCARIO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 2 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 3:
                        itemSubGrupoAux.NomeSubGrupo = "DÉPOSITO BANCÁRIO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 3 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 4:
                        itemSubGrupoAux.NomeSubGrupo = "CHEQUE";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 4 }) +
                            retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 4 }, EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 5:
                        itemSubGrupoAux.NomeSubGrupo = "DUPLICATA";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 5 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 6:
                        itemSubGrupoAux.NomeSubGrupo = "CREDIÁRIO PRÓPRIO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 6 },
                                                                                                                                EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 7:
                        itemSubGrupoAux.NomeSubGrupo = "CARTÃO DE CRÉDITO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 7 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 8:
                        itemSubGrupoAux.NomeSubGrupo = "CARTÃO DE DÉBITO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 8 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        //retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 8 });

                        break;

                    case 9:
                        itemSubGrupoAux.NomeSubGrupo = "CREDITO INTERNO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 9 },
                                                                                                                                EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;

                    case 10:
                        //itemSubGrupoAux.NomeSubGrupo = "PIX";
                        //itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 4 },
                        //                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                        itemSubGrupoAux.NomeSubGrupo = "PIX";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 10 }) +
                            retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 10 }, EnumTipoOperacaoContasPagarReceber.RECEBER, 1);

                        break;
                }

                //Add Subgrupo
                listaItemSubGrupoAux.Add(itemSubGrupoAux);

            }

            return listaItemSubGrupoAux;
        }

        public List<SubGrupoAux> carregaRecebimentosOperacionaisNew()
        {
            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();
            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            for (int i = 1; i <= 10; i++)
            {
                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                switch (i)
                {
                    //Id = 2 Categoria: Recebimentos Operacionais (Todas as formas de pagamentos, estão nesta categoria).

                    case 1:
                        itemSubGrupoAux.NomeSubGrupo = "DINHEIRO";
                        List<VWVenda> listaVWVenda = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVenda)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }

                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Total = string.Format("{0:N}", itemSubGrupoAux.Total).ToDouble();

                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();

                        break;

                    case 2:
                        itemSubGrupoAux.NomeSubGrupo = "BOLETO BANCARIO";
                        List<VWVenda> listaVWVendaII = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaII)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;

                    case 3:
                        itemSubGrupoAux.NomeSubGrupo = "DÉPOSITO BANCÁRIO";
                        List<VWVenda> listaVWVendaIII = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaIII)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;

                    case 4:
                        itemSubGrupoAux.NomeSubGrupo = "CHEQUE";
                        List<VWVenda> listaVWVendaIV = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaIV)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;

                    case 5:
                        itemSubGrupoAux.NomeSubGrupo = "DUPLICATA";
                        List<VWVenda> listaVWVendaV = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaV)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();

                        break;

                    case 6:
                        itemSubGrupoAux.NomeSubGrupo = "CREDIÁRIO PRÓPRIO";
                        List<VWVenda> listaVWVendaVI = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaVI)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;

                    case 7:
                        itemSubGrupoAux.NomeSubGrupo = "CARTÃO CRÉDITO";
                        List<VWVenda> listaVWVendaVII = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaVII)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;

                    case 8:
                        itemSubGrupoAux.NomeSubGrupo = "CARTÃO DE DÉBITO";
                        List<VWVenda> listaVWVendaVIII = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaVIII)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        if (itemSubGrupoAux.Total == 0)
                        {
                            itemSubGrupoAux.NomeSubGrupo = "CARTÃO DÉBITO";
                            List<VWVenda> listaVWVendaVIIII = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                            foreach (var venda in listaVWVendaVIIII)
                            {
                                itemSubGrupoAux.Total += venda.ValorTotal;
                            }
                            itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                            itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                            itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        }


                        break;

                    case 9:
                        itemSubGrupoAux.NomeSubGrupo = "CREDITO INTERNO";
                        List<VWVenda> listaVWVendaIX = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaIX)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;

                    case 10:
                        //itemSubGrupoAux.NomeSubGrupo = "PIX";
                        //itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 4 },
                        //                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                        itemSubGrupoAux.NomeSubGrupo = "PIX";
                        List<VWVenda> listaVWVendaX = servicoPedidoDeVenda.ConsulteVWVendasPag(dateTimeValue, dateTimeValue2, itemSubGrupoAux.NomeSubGrupo);

                        foreach (var venda in listaVWVendaX)
                        {
                            itemSubGrupoAux.Total += venda.ValorTotal;
                        }
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total;
                        itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _saldoEntrada;
                        itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();
                        break;
                }

                //Add Subgrupo
                if (itemSubGrupoAux.Total != 0)
                {
                    listaItemSubGrupoAux.Add(itemSubGrupoAux);
                }

            }

            return listaItemSubGrupoAux;
        }
        public List<SubGrupoAux> carregaSaidasOperacionais(int grupoId)
        {
            var subGruposCadastrados = new ServicoSubGrupoCategoria().ConsulteLista(grupoId, string.Empty, "A");

            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();

            foreach (var itemSub in subGruposCadastrados)
            {
                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                switch (itemSub.Id)
                {
                    case 2: //Operacionais

                        itemSubGrupoAux.NomeSubGrupo = itemSub.Descricao;

                        var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().ConsulteLista(string.Empty, itemSub, "A");

                        foreach (var itemCat in categoriasCadastradas)
                        {
                            CategoriaAgrupada itemcategoriaAux = new CategoriaAgrupada();

                            if (itemCat.Descricao == "FORNECEDORES")
                            {
                                //Fornecedores ****
                                SubGrupoAux itemSubGrupoAux2 = new SubGrupoAux();
                                itemSubGrupoAux2.NomeSubGrupo = "FORNECEDORES";

                                if (itemCat.Descricao == "FORNECEDORES")
                                {
                                    itemSubGrupoAux2.ListaDeCategorias = retorneDetalheFornecedores(itemCat.Id, null, EnumTipoOperacaoContasPagarReceber.PAGAR);
                                }

                                itemSubGrupoAux2.Total = itemSubGrupoAux2.ListaDeCategorias.Sum(x => x.Valor);
                                listaItemSubGrupoAux.Add(itemSubGrupoAux2);
                            }
                            else
                            {
                                //Nome da Categoria
                                itemcategoriaAux.NomeCategoria = itemCat.Descricao;

                                //Contas a Pagar
                                itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoReceber(itemCat.Id, null,
                                                                                                                                        EnumTipoOperacaoContasPagarReceber.PAGAR, 0);
                                //Caixa
                                itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) +//Dinheiro
                                                                                    retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 });//Cheque

                                //Banco
                                itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL);

                                itemSubGrupoAux.ListaDeCategorias.Add(itemcategoriaAux);

                            }

                        }

                        itemSubGrupoAux.Total = itemSubGrupoAux.ListaDeCategorias.Sum(x => x.Valor);
                        listaItemSubGrupoAux.Add(itemSubGrupoAux);

                        break;

                    case 3: //Negociações / Parcelamentos
                    case 10: //Pessoal
                    case 11: // Despezas
                    case 12: //Impostos

                        itemSubGrupoAux.NomeSubGrupo = itemSub.Descricao;

                        var categoriasCadastradas2 = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().ConsulteLista(string.Empty, itemSub, "A");

                        foreach (var itemCat in categoriasCadastradas2)
                        {
                            CategoriaAgrupada itemcategoriaAux = new CategoriaAgrupada();

                            itemcategoriaAux.NomeCategoria = itemCat.Descricao;
                            //Contas a Pagar
                            itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoReceber(itemCat.Id, null,
                                                                                                                                    EnumTipoOperacaoContasPagarReceber.PAGAR, 0);
                            //Caixa
                            itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                                retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                            //Banco
                            itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL);

                            itemSubGrupoAux.ListaDeCategorias.Add(itemcategoriaAux);
                        }

                        //Total Geral do SubGrupo
                        itemSubGrupoAux.Total = itemSubGrupoAux.ListaDeCategorias.Sum(x => x.Valor);
                        listaItemSubGrupoAux.Add(itemSubGrupoAux);
                        break;

                }
            }

            return listaItemSubGrupoAux;
        }

        public List<SubGrupoAux> carregaAtividades(int grupoId)
        {
            var subGruposCadastrados = new ServicoSubGrupoCategoria().ConsulteLista(grupoId, string.Empty, "A");

            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();

            foreach (var itemSub in subGruposCadastrados)
            {
                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                //SubGrupo de Recebimentos
                if (itemSub.Descricao.Contains("RECEBIMENTO"))
                {
                    itemSubGrupoAux.NomeSubGrupo = itemSub.Descricao;

                    var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().ConsulteLista(string.Empty, itemSub, "A");

                    foreach (var itemCat in categoriasCadastradas)
                    {
                        CategoriaAgrupada itemcategoriaAux = new CategoriaAgrupada();

                        itemcategoriaAux.NomeCategoria = itemCat.Descricao;
                        //Contas a Receber
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoReceber(itemCat.Id, null,
                                                                                                                                EnumTipoOperacaoContasPagarReceber.RECEBER, 1);
                        //Caixa
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                            retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                        //Banco
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.ENTRADA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL);

                        itemSubGrupoAux.ListaDeCategorias.Add(itemcategoriaAux);

                        //Total Geral do SubGrupo
                        itemSubGrupoAux.Total = itemSubGrupoAux.ListaDeCategorias.Sum(x => x.Valor);
                    }

                    listaItemSubGrupoAux.Add(itemSubGrupoAux);

                }

                SubGrupoAux itemSubGrupoAux2 = new SubGrupoAux();

                //SubGrupo de Pagamentos
                if (itemSub.Descricao.Contains("PAGAMENTO"))
                {
                    //Pagamentos Financeiros 
                    itemSubGrupoAux2.NomeSubGrupo = itemSub.Descricao;

                    var categoriasCadastradas2 = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().ConsulteLista(string.Empty, itemSub, "A");

                    foreach (var itemCat in categoriasCadastradas2)
                    {
                        CategoriaAgrupada itemcategoriaAux = new CategoriaAgrupada();

                        itemcategoriaAux.NomeCategoria = itemCat.Descricao;
                        //Contas a Pagar
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoReceber(itemCat.Id, null,
                                                                                                                                EnumTipoOperacaoContasPagarReceber.PAGAR, 0);
                        //Caixa
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                            retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                        //Banco
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL);

                        itemSubGrupoAux2.ListaDeCategorias.Add(itemcategoriaAux);

                        //Total Geral do SubGrupo
                        itemSubGrupoAux2.Total = itemSubGrupoAux2.ListaDeCategorias.Sum(x => x.Valor);
                    }

                    listaItemSubGrupoAux.Add(itemSubGrupoAux2);

                }
            }

            return listaItemSubGrupoAux;
        }

        public List<SubGrupoAux> carregaAtividadesRecebimentos(int subGrupoId)
        {
            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();

            var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().
                                                           ConsulteLista(string.Empty, new SubGrupoCategoria { Id = subGrupoId }, "A");

            foreach (var itemCat in categoriasCadastradas)
            {
                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                itemSubGrupoAux.NomeSubGrupo = itemCat.Descricao;

                //Contas a Receber
                itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(itemCat.Id, null,
                                                                                                                        EnumTipoOperacaoContasPagarReceber.RECEBER, 1);
                //Caixa
                itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                    retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                //Banco
                itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.ENTRADA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL);

                listaItemSubGrupoAux.Add(itemSubGrupoAux);
            }

            return listaItemSubGrupoAux;
        }

        public List<SubGrupoAux> carregaAtividadesPagamentos(int subGrupoId)
        {
            double TotalGrupo = 0;
            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();

            var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().
                                                           ConsulteLista(string.Empty, new SubGrupoCategoria { Id = subGrupoId }, "A");

            foreach (var itemCat in categoriasCadastradas)
            {
                if (itemCat.MostrarDRE == true)
                {
                    SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                    itemSubGrupoAux.NomeSubGrupo = itemCat.Descricao;

                    //Contas a Receber
                    itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceberII(itemCat.Id, null,
                                                                                                                            EnumTipoOperacaoContasPagarReceber.PAGAR,1);
                    //Caixa
                    itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                        retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                    //Banco

                    EnumOrigemMovimentacaoBanco origem;

                    if (itemCat.Id == 14 )
                    {
                        origem = EnumOrigemMovimentacaoBanco.CONCILIADO;
                    }
                    else
                    {
                        origem = EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL;
                    }


                    itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA, origem);

                    if (itemCat.Descricao.ToString() != "FORNECEDORES")
                    {
                        if (itemCat.Descricao.ToString() != "MUTUO")
                        {
                            if (itemCat.Descricao.ToString() != "FRETE COMPRA DE MERCADORIA")
                            {
                                TotalGrupo += itemSubGrupoAux.Total;
                                if (itemSubGrupoAux.Total != 0)
                                {
                                    listaItemSubGrupoAux.Add(itemSubGrupoAux);
                                }
                            }

                        }

                    }
                }


            }
            foreach (var itemGrupo in listaItemSubGrupoAux)
            {
                itemGrupo.Percentual = itemGrupo.Total * 100 / TotalGrupo;
                itemGrupo.Percentual = string.Format("{0:N}", itemGrupo.Percentual).ToDouble();
            }
            return listaItemSubGrupoAux;
        }
        public List<SubGrupoAux> carregaGrupoProdutos()
        {
            double TotalGrupo = 0;
            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();
            List<SubGrupoAuxII> listaItemSubGrupoAuxII = new List<SubGrupoAuxII>();

            //var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().
            //                                               ConsulteLista(string.Empty, new SubGrupoCategoria { Id = subGrupoId }, "A");
            ServicoSubGrupo servicoGrupo = new ServicoSubGrupo();
            var grupo = servicoGrupo.ConsulteLista();

            //SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
            //itemSubGrupoAux.NomeSubGrupo = "Custo Mercadoria Vendida";

            //itemSubGrupoAux.Total = _customercadoria.ToDouble();

            foreach (var itemCat in grupo)

            {
                if (itemCat.Status == "A")
                {

                    SubGrupoAux itemSubGrupoAux = new SubGrupoAux();
                    itemSubGrupoAux.NomeSubGrupo = itemCat.Descricao;
                    itemSubGrupoAux.Total = TotalizaGrupo(itemCat.Id.ToInt());
                    itemSubGrupoAux.Total = string.Format("{0:N}", itemSubGrupoAux.Total).ToDouble();
                    itemSubGrupoAux.Percentual = itemSubGrupoAux.Total * 100 / _customercadoria;
                    itemSubGrupoAux.Percentual = string.Format("{0:N}", itemSubGrupoAux.Percentual).ToDouble();

                    if (itemSubGrupoAux.Total > 0)
                    {
                        listaItemSubGrupoAux.Add(itemSubGrupoAux);

                    }
                    TotalGrupo += itemSubGrupoAux.Total;

                    //CategoriaAgrupada itemcategoriaAux = new CategoriaAgrupada();

                    //itemcategoriaAux.NomeCategoria = itemCat.Descricao;
                    //itemcategoriaAux.Valor = TotalizaGrupo(itemCat.Id.ToInt());
                    //itemcategoriaAux.Valor = string.Format("{0:N}", itemcategoriaAux.Valor).ToDouble();
                    //itemcategoriaAux.Percentual = itemcategoriaAux.Valor * 100 / _customercadoria;
                    //itemcategoriaAux.Percentual = string.Format("{0:N}", itemcategoriaAux.Percentual).ToDouble();

                    //if (itemcategoriaAux.Valor > 0)
                    //{
                    //    itemSubGrupoAux.ListaDeCategorias.Add(itemcategoriaAux);

                    //}

                    // TotalGrupo += itemcategoriaAux.Valor;
                }

            }


            //listaItemSubGrupoAux.Add(itemSubGrupoAux);
            //TotalGrupo = itemSubGrupoAux.Total;

            SubGrupoAux itemSubGrupoAuxIII = new SubGrupoAux();
            itemSubGrupoAuxIII.NomeSubGrupo = "Frete Sobre Compra";

            itemSubGrupoAuxIII.Total = TotalizaFrete();
            TotalGrupo += itemSubGrupoAuxIII.Total;
            if (itemSubGrupoAuxIII.Total > 0)
            {
                listaItemSubGrupoAux.Add(itemSubGrupoAuxIII);
            }


            foreach (var itemGrupo in listaItemSubGrupoAux)
            {
                itemGrupo.Percentual = itemGrupo.Total * 100 / TotalGrupo;
                itemGrupo.Percentual = string.Format("{0:N}", itemGrupo.Percentual).ToDouble();
            }
            return listaItemSubGrupoAux;
        }
        private double TotalizaGrupo(int Grupo)
        {
            string DataInicial = txtDataInicialPeriodo.Text;
            string DataFinal = txtDataFinalPeriodo.Text;

            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            var dt1 = DateTime.Parse(DataInicial).ToString("yyyy-MM-dd");
            var dt2 = DateTime.Parse(DataFinal).ToString("yyyy-MM-dd");



            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }
            }
            // mDataSet = new DataSet();
            double soma = 0;
            double Valorcusto = 0;

            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();


                string Sql = "Select prod_id, PEDITEM_QUANTIDADE as Quantidade, PROD_PRECO_COMPRA as ValorCompra " +
                                    " From pedidosvendas " +
                                    " Inner join pedidosvendasitens ON " +
                                    " pedidosvendas.pedido_id = pedidosvendasitens.peditem_pedido_id " +
                                    " Inner Join produtos ON " +
                                    " pedidosvendasitens.peditem_produto_id = produtos.prod_id " +
                                    " Where PEDIDO_ESTAH_PAGO = 1 And pedido_status <> 3 And " +
                                    " pedido_data_fechamento >= '" + dt1 + "' And pedido_data_fechamento <=  '" + dt2 + " 23:59:00' And " +
                                    " produtos.prod_subgrp_id = " + Grupo;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    Valorcusto = UltimaEntrada(returnValue["prod_id"].ToInt());
                    if (Valorcusto > 0)
                    {
                        soma += Valorcusto * returnValue["Quantidade"].ToDouble();
                    }
                    else
                    {
                        soma += returnValue["ValorCompra"].ToDouble() * returnValue["Quantidade"].ToDouble();
                    }
                    
                }

                
                return soma;
            }



        }
        private double UltimaEntrada(int Produto)
        {
            string DataInicial = txtDataInicialPeriodo.Text;
            string DataFinal = txtDataFinalPeriodo.Text;

            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            var dt1 = DateTime.Parse(DataInicial).ToString("yyyy-MM-dd");
            var dt2 = DateTime.Parse(DataFinal).ToString("yyyy-MM-dd");



            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }
            }
            // mDataSet = new DataSet();

            double Valorcusto = 0;

            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();


                string Sql = "Select itement_valor_unitario From entradas " +
                                "Inner join entradasitens ON entradas.entrada_id = entradasitens.itement_entrada_id " +
                                "Where itement_prod_id = " + Produto + " And entrada_tipo = 0  And entrada_status = 1 order by itement_id Desc " +
                                "Limit 1";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    Valorcusto = returnValue["itement_valor_unitario"].ToDouble();
                }

            }
            return Valorcusto;
        }
    


        
        private double TotalizaCustoMercadoria()
        {
            string DataInicial = txtDataInicialPeriodo.Text;
            string DataFinal = txtDataFinalPeriodo.Text;

            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            var dt1 = DateTime.Parse(DataInicial).ToString("yyyy-MM-dd");
            var dt2 = DateTime.Parse(DataFinal).ToString("yyyy-MM-dd");



            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }
            }
            // mDataSet = new DataSet();
            double soma = 0;
            double Valorcusto = 0;

            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();


                string Sql = "Select sum(PROD_PRECO_COMPRA * PEDITEM_QUANTIDADE) as Total " +
                                    " From pedidosvendas " +
                                    " Inner join pedidosvendasitens ON " +
                                    " pedidosvendas.pedido_id = pedidosvendasitens.peditem_pedido_id " +
                                    " Inner Join produtos ON " +
                                    " pedidosvendasitens.peditem_produto_id = produtos.prod_id " +
                                    " Where PEDIDO_ESTAH_PAGO = 1 And pedido_status <> 3 And " +
                                    " pedido_data_elaboracao >= '" + dt1 + "' And pedido_data_elaboracao <=  '" + dt2 + "' And prod_subgrp_id is not null"; 
                                    

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {

                    soma += returnValue["Total"].ToDouble() ;


                }
                return soma;
            }



        }
        private double TotalizaFrete()
        {
            string DataInicial = txtDataInicialPeriodo.Text;
            string DataFinal = txtDataFinalPeriodo.Text;

            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

            var dt1 = DateTime.Parse(DataInicial).ToString("yyyy-MM-dd");
            var dt2 = DateTime.Parse(DataFinal).ToString("yyyy-MM-dd");



            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionString = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
            }
            else
            {
                ipServer = !string.IsNullOrEmpty(item.IpSecundario) ? item.IpSecundario : "localhost";
                database = !string.IsNullOrEmpty(item.BancoDadosSecundario) ? item.BancoDadosSecundario : "akilsmallbusiness";
                userId = !string.IsNullOrEmpty(item.UsuarioSecundario) ? item.UsuarioSecundario : "root";
                senha = !string.IsNullOrEmpty(item.SenhaSecundaria) ? item.SenhaSecundaria : "Progr@max-2015";
                porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

                var serverSecundarioOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

                if (serverSecundarioOnline)
                {
                    StringConexao = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
                else
                {
                    //throw new Exception();
                    //throw new Exception("Servidor de banco de dados não encontrado");
                }
            }
            // mDataSet = new DataSet();
            double soma = 0;
            double Valorcusto = 0;

            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();


                string Sql = "Select sum(entrada_valor_frete) ValorFrete " +
                                    " From entradas " +
                                    " Where entrada_data_emissao >= '" + dt1 + "' And entrada_data_emissao <= '" + dt2 + "' ";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;

                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {

                    soma += returnValue["ValorFrete"].ToDouble() ;


                }
                return soma;
            }



        }

        public List<CategoriaAgrupada> retorneDetalheFornecedores(int categoriaId, FormaPagamento formaPagamento,
                                                                       EnumTipoOperacaoContasPagarReceber enumReceberPagar)
        {
           

            var fornecedores = new ServicoContasPagarReceber().ConsulteLista(null, enumReceberPagar, EnumStatusContaPagarReceber.QUITADO,
                                                                 formaPagamento, null, EnumDataFiltrarContasPagarReceber.PAGAMENTO,
                                                                 dateTimeValue, dateTimeValue2, null, categoriaId);

            var groupFornecedor = fornecedores.GroupBy(x => x.Pessoa.Id);

            List<CategoriaAgrupada> listaCategoria = new List<CategoriaAgrupada>();
            foreach (var item in groupFornecedor)
            {
                CategoriaAgrupada itemCategoria = new CategoriaAgrupada();

                itemCategoria.NomeCategoria = fornecedores.Find(x=>x.Pessoa.Id == item.Key).Pessoa.DadosGerais.Razao;

                itemCategoria.Valor = fornecedores.FindAll(x => x.Pessoa.Id == item.Key).Sum(x => x.ValorPago) + //Contas a Pagar
                                                               retorneTotalCategoriaNoBanco(new CategoriaFinanceira { Id = categoriaId }, EnumTipoMovimentacaoBanco.SAIDA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL,new Pessoa { Id = item.Key }) + //Banco
                                                               retorneTotalFormaPagamentoPorCategoriaNoCaixa(categoriaId, new FormaPagamento { Id = 1 }, new Pessoa {Id = item.Key}) + //Caixa Dinheiro
                                                                retorneTotalFormaPagamentoPorCategoriaNoCaixa(categoriaId, new FormaPagamento { Id = 4 }, new Pessoa { Id = item.Key }); // Caixa Cheque
                listaCategoria.Add(itemCategoria);
            }

            return listaCategoria;
        }
        
        //Este método coloca a categoria como detalhe
        public List<DetalheCategoria> retorneOutrosDetalhesSubGrupos(SubGrupoCategoria subGrupo)
        {
            var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().ConsulteLista(string.Empty, subGrupo, "A");

            List<DetalheCategoria> listaDetalheCategoria = new List<DetalheCategoria>();

            foreach (var item in categoriasCadastradas)
            {
                DetalheCategoria itemDetalheCategoria = new DetalheCategoria();

                //Contas a Pagar
                itemDetalheCategoria.Valor = itemDetalheCategoria.Valor + retorneTotalFormaPagamentoPorCategoriaNoReceber(item.Id, null,
                                                                                                EnumTipoOperacaoContasPagarReceber.PAGAR, 0);
                //Caixa
                itemDetalheCategoria.Valor = itemDetalheCategoria.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(item.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                             retorneTotalFormaPagamentoPorCategoriaNoCaixa(item.Id, new FormaPagamento { Id = 4 }); //Cheque

                //Banco
                itemDetalheCategoria.Valor = itemDetalheCategoria.Valor + retorneTotalCategoriaNoBanco(item, EnumTipoMovimentacaoBanco.SAIDA, EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL);

                itemDetalheCategoria.Descricao = item.Descricao;

                listaDetalheCategoria.Add(itemDetalheCategoria);
            }

            return listaDetalheCategoria;
        }
       

        public double retorneTotalFormaPagamentoPorCategoriaNoReceber(int categoriaId, FormaPagamento formaPagamento, 
                                                                        EnumTipoOperacaoContasPagarReceber enumReceberPagar, int? Dre = null)
        {
            return new ServicoContasPagarReceber().ConsulteLista(null, enumReceberPagar,null, 
                                                                 formaPagamento, null, EnumDataFiltrarContasPagarReceber.EMISSAO,
                                                                 dateTimeValue, dateTimeValue2, null, categoriaId, Dre)
                                                                 .Sum(x=>x.ValorParcela);
        }

        public double retorneTotalFormaPagamentoPorCategoriaNoReceberII(int categoriaId, FormaPagamento formaPagamento,
                                                                        EnumTipoOperacaoContasPagarReceber enumReceberPagar, int? Dre = null)
        {
            return new ServicoContasPagarReceber().ConsulteListaII(null, enumReceberPagar, null,
                                                                 formaPagamento, null, EnumDataFiltrarContasPagarReceber.EMISSAO,
                                                                 dateTimeValue, dateTimeValue2, null, categoriaId, Dre)
                                                                 .Sum(x => x.ValorParcela);
        }

        public double retorneTotalFormaPagamentoPorCategoriaNoCaixa(int categoriaId, FormaPagamento formaPagamento, Pessoa pessoa=null)
        {
            return new ServicoItemMovimentacaoCaixa().ConsulteListaPorCategoriasEPagamentos(categoriaId, formaPagamento.Id,
                                                                                            dateTimeValue,
                                                                                            dateTimeValue2, null).Sum(x=>x.Valor);
        }

        public double retorneTotalCategoriaNoBanco(CategoriaFinanceira categoria, EnumTipoMovimentacaoBanco tipo, EnumOrigemMovimentacaoBanco origem,   Pessoa pessoa=null)
        {

            //EnumOrigemMovimentacaoBanco origem,
            return new ServicoItemMovimentacaoBanco().ConsulteListaItens(null, dateTimeValue, dateTimeValue2,
                                                                            origem, string.Empty, tipo,
                                                                            string.Empty, pessoa, categoria,null)
                                                                            .Sum(x => x.Valor);
        }

        public void carregaSaldoInicialBancosCaixas()
        {
            double SaldoBancoValor = 0;
            double SaldoCaixaValor = 0;

            //Saldo de bancos e caixas
            List<SubGrupoAux> ListaSaldo = new List<SubGrupoAux>();

            //Busca os Bancos
            var bancos = new ServicoBancoParaMovimento().ConsulteLista(string.Empty, "A");

            foreach (var itemBanco in bancos)
            {
                SaldoBancoValor = 0;
               
                DateTime dataInicial = dateTimeValue;
                DateTime dataFinal = dateTimeValue2;

                int diasAtraso = 0;  

                //Busca o movimento do banco do loop
                var movimentoBanco = new ServicoMovimentacaoBanco().ConsulteRegistrosDeMovimentoDoBanco(itemBanco.Id, dataInicial.AddDays(- diasAtraso), dataInicial.AddDays(diasAtraso));

                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                //Item movimento dos bancos (a apartir do movimento) -> buscar o saldo inicial de todos
                foreach (var movBco in movimentoBanco)
                {
                    //Soma todos os saldos encontrados pelo movimento do banco do banco do loop
                    var saldoInicialMovimBanco = new ServicoItemMovimentacaoBanco().ConsulteSaldoItensBancoMovimento(movBco,  24, dataInicial, dataInicial); //SaldoIncial

                    SaldoBancoValor = SaldoBancoValor + saldoInicialMovimBanco;
                }

                itemSubGrupoAux.NomeSubGrupo = "Banco: " + itemBanco.NomeBanco;
                itemSubGrupoAux.Total = SaldoBancoValor;

                ListaSaldo.Add(itemSubGrupoAux);
            }

            //Busca os caixas
            var caixas = new ServicoCaixa().ConsulteLista(null, "A", null);

            foreach (var itemCaixa in caixas)
            {
                SaldoCaixaValor = 0;
                //Busca o movimento do caixa do 
                var movimentoCaixa = new ServicoMovimentacaoCaixa().ConsulteLista(itemCaixa, EnumDataFiltrarMovimentacaoCaixa.DATAABERTURA, dateTimeValue,
                                                                                    dateTimeValue2, null);

                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                //Item movimento dos caixas (a apartir do movimento) - buscar o saldo inicial de todos
                foreach (var movCaixa in movimentoCaixa)
                {
                    //Soma todos os saldos encontrados pelo movimento do banco do banco do loop
                    var saldoInicialMovimCaixa = new ServicoItemMovimentacaoCaixa().ConsulteSaldoItensCaixaMovimento(movCaixa.Id, 24).Sum(x => x.Valor); //SaldoIncial

                    SaldoCaixaValor = SaldoCaixaValor + saldoInicialMovimCaixa;
                }

                itemSubGrupoAux.NomeSubGrupo = "Caixa: " + itemCaixa.NomeCaixa;
                itemSubGrupoAux.Total = SaldoCaixaValor;

                ListaSaldo.Add(itemSubGrupoAux);

            }

            //Saldo Inicial - bancos e caixas
            GrupoAux itemGrupoAux = new GrupoAux();
            itemGrupoAux.NomeGrupo = "$ SALDO INICIAL $";
            itemGrupoAux.ListaDeSubGrupos = ListaSaldo;
            itemGrupoAux.TotalCategorias = ListaSaldo.Sum(x => x.Total);

            _saldoInicial = _saldoInicial + itemGrupoAux.TotalCategorias;

            _listaGrupoDetalhe.Add(itemGrupoAux);
        }

        public void CarregaGraficoFluxoCaixa(List<vw_fluxo_caixa> FluxoCaixa)
        {
            List<DadosGraficoFluxoCaixa> DadosGrafico = new List<DadosGraficoFluxoCaixa>();
            var ListaDeDatas = FluxoCaixa.OrderBy(x => x.DATAREALIZADO.Date).GroupBy(x=>x.DATAREALIZADO.Date);

            foreach (var item in ListaDeDatas)
            {
                DadosGraficoFluxoCaixa itemDadosGrafico = new DadosGraficoFluxoCaixa();

                itemDadosGrafico.DataRealizado = item.Key.Date;

                foreach (var itemFluxo in FluxoCaixa)
                {
                    if(itemFluxo.DATAREALIZADO.Date == item.Key.Date)
                    {
                        if (itemFluxo.TIPOMOVIMENTACAO == 0)
                            itemDadosGrafico.ValorEntrada = itemDadosGrafico.ValorEntrada + itemFluxo.VALOR;
                        else
                            itemDadosGrafico.ValorSaida = itemDadosGrafico.ValorSaida + itemFluxo.VALOR;
                    }
                }

                DadosGrafico.Add(itemDadosGrafico);
            }

            chtFluxoCaixa.DataSource = DadosGrafico;
        }

        public class GrupoAux
        {
            public GrupoAux()
            {
                ListaDeSubGrupos = new List<SubGrupoAux>();
            }

            //public int Id { get; set; }
                       
            public string NomeGrupo { get; set; }
            public List<SubGrupoAux> ListaDeSubGrupos { get; set; }

            public double TotalCategorias { get; set; }
            
        }

        public class SubGrupoAuxII
        {
            public SubGrupoAuxII()
            {
                ListaDeCategoriasII = new List<CategoriaAgrupadaII>();
               
            }

            //public int Id { get; set; }

            public string NomeSubGrupoII { get; set; }
            public List<CategoriaAgrupadaII> ListaDeCategoriasII { get; set; }
            public double TotalII { get; set; }
            public double PercentualII { get; set; }
        }
        public class SubGrupoAux
        {
            public SubGrupoAux()
            {
                ListaDeCategorias = new List<CategoriaAgrupada>();
                
            }
            
            //public int Id { get; set; }
            public List<SubGrupoAuxII> ListaDeSubGruposII { get; set; }
            public string NomeSubGrupo { get; set; }
            public List<CategoriaAgrupada> ListaDeCategorias { get; set; }
            public double Total { get; set; }
            public double Percentual { get; set; }
        }

        public class CategoriaAgrupada
        {
            public CategoriaAgrupada()
            {
                ListaDetalhes = new List<DetalheCategoria>();          
            }

            public List<DetalheCategoria> ListaDetalhes { get; set; }
            public List<DetalheCategoriaII> ListaDetalhesII { get; set; }

            public string NomeCategoria { get; set; }            
            public double Valor { get; set; }
            public double Percentual { get; set; }
        }
        public class CategoriaAgrupadaII
        {
            public CategoriaAgrupadaII()
            {
                ListaDetalhesII = new List<DetalheCategoriaII>();
            }

            public List<DetalheCategoriaII> ListaDetalhesII { get; set; }

            public string NomeCategoria { get; set; }
            public double Valor { get; set; }
        }

        public class DetalheCategoria
        {   
            public string Descricao { get; set; }
            //public DateTime DataRealizado { get; set; }           
            public double Valor { get; set; }
        }
        public class DetalheCategoriaII
        {
            public string Descricao { get; set; }
            //public DateTime DataRealizado { get; set; }           
            public double Valor { get; set; }
        }

        public class DadosGraficoFluxoCaixa
        {
            public DateTime DataRealizado { get; set; }
            public double ValorEntrada { get; set; }
            public double ValorSaida { get; set; }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ImprimirGrid();

            this.Cursor = Cursors.Default;
        }

        private void ImprimirGrid()
        {
            gcGridFluxoDre.ShowPrintPreview();                    
        }

        private void ImprimirGrafico()
        {
            CompositeLink composLink = new CompositeLink(new PrintingSystem());
            PrintableComponentLink pcLink1 = new PrintableComponentLink();

            pcLink1.Component = this.chtFluxoCaixa;
            composLink.Links.Add(pcLink1);

            composLink.Landscape = true;

            composLink.ShowPreviewDialog();

            //chtFluxoCaixa.ShowPrintPreview();
        }

        private void ImprimirGridEGrafico()
        {
            // Create objects and define event handlers. 
            CompositeLink composLink = new CompositeLink(new PrintingSystem());

            composLink.CreateMarginalHeaderArea +=
                new CreateAreaEventHandler(composLink_CreateMarginalHeaderArea);

            PrintableComponentLink pcLink1 = new PrintableComponentLink();
            PrintableComponentLink pcLink2 = new PrintableComponentLink();
            Link linkMainReport = new Link();
            linkMainReport.CreateDetailArea +=
                new CreateAreaEventHandler(linkMainReport_CreateDetailArea);
            Link linkGrid1Report = new Link();
            linkGrid1Report.CreateDetailArea +=
                new CreateAreaEventHandler(linkGrid1Report_CreateDetailArea);
            Link linkGrid2Report = new Link();
            linkGrid2Report.CreateDetailArea +=
                new CreateAreaEventHandler(linkGrid2Report_CreateDetailArea);

            // Assign the controls to the printing links. 
            pcLink1.Component = this.gcGridFluxoDre;
            pcLink2.Component = this.chtFluxoCaixa;

            // Populate the collection of links in the composite link. 
            // The order of operations corresponds to the document structure. 
            composLink.Links.Add(linkGrid1Report);
            composLink.Links.Add(pcLink1);
            composLink.Links.Add(linkMainReport);
            composLink.Links.Add(linkGrid2Report);
            composLink.Links.Add(pcLink2);

            // Create the report and show the preview window.
            composLink.Landscape = true;
            composLink.ShowPreviewDialog();
        }

        // Inserts a PageInfoBrick into the top margin to display the time. 
        void composLink_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.DrawPageInfo(PageInfo.DateTime, "{0:hhhh:mmmm:ssss}", Color.Black,
                new RectangleF(0, 0, 200, 50), BorderSide.None);
        }

        // Creates a text header for the first grid. 
        void linkGrid1Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Grupos e Categorias";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(10, 10, 300, 30);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;           
            e.Graph.DrawBrick(tb);
        }

        // Creates an interval between the grids and fills it with color. 
        void linkMainReport_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {

            TextBrick tb = new TextBrick();
            tb.Rect = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            tb.BackColor = Color.Gray;
            e.Graph.DrawBrick(tb);
        }

        // Creates a text header for the second grid. 
        void linkGrid2Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            TextBrick tb = new TextBrick();
            tb.Text = "Gráfico Entrada e Saída";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(10, 10, 300, 30);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;           
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            e.Graph.DrawBrick(tb);
        }

        private void btnPesquisaFluxoCaixa_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
           

            DateTime dataInicial = txtDataInicialPeriodo.Text.ToDate();
            DateTime dataFinal = txtDataFinalPeriodo.Text.ToDate();

            dateTimeValue = dataInicial;
            dateTimeValue2 = dataFinal;

            CarregaGridMasterDetail();

            this.Cursor = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimirGrafico_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            ImprimirGrafico();

            this.Cursor = Cursors.Default;
        }

        private void btnImprimirTodos_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //gcGridFluxoCaixa.

            ImprimirGridEGrafico();

            this.Cursor = Cursors.Default;
        }
    }
}
