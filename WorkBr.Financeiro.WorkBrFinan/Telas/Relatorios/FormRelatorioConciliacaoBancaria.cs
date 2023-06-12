using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.ConciliacaoBancariaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.GruposCategoriasObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Servico.Financeiro.BancoParaMovimentoServl;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.GruposCategoriasServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.ItemMovimentacaoCaixaServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoBancoServ;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Infraestrutura.Negocio.Utils;

namespace Programax.Easy.View.Telas.Relatorios
{
    public partial class FormRelatorioConciliacaoBancaria : Form
    {
        List<GrupoAux> _listaGrupoDetalhe = new List<GrupoAux>();
      
        double _saldoInicial;
        double _saldoEntrada;
        double _saldoSaida;
        double _saldoRecebimento;
        double _saldoPagamento;

        public FormRelatorioConciliacaoBancaria()
        {
            InitializeComponent();

            txtDiasAtrasoSaldo.Text = "2";
        }

        private void CarregaGridMasterDetail()
        {
            _saldoInicial = 0;
            _saldoEntrada = 0;
            _saldoSaida = 0;
            _saldoRecebimento = 0;
            _saldoPagamento = 0;

            _listaGrupoDetalhe = new List<GrupoAux>();

            //1- Saldo Inicial de bancos e caixas 
            carregaSaldoInicialBancosCaixas();

            //Carrega todos os grupos
            var gruposCadastrados = new ServicoGrupoCategoria().ConsulteLista(null, string.Empty, "A");

            foreach (var itemGrupo in gruposCadastrados)
            {
                GrupoAux itemGrupoAux = new GrupoAux();
                
                switch (itemGrupo.Id)
                {
                    case 1://Entradas Operacionais

                        //Recebimentos Operacionais com Detalhes na categoria
                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaRecebimentosOperacionais();

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        _saldoEntrada = _saldoEntrada + itemGrupoAux.TotalCategorias;

                        break;

                    case 2://Saídas Operacionais

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaSaidasOperacionais(itemGrupo.Id);

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        break;

                    case 3://RECEBIMENTO FINANCEIRO

                        //Saldo Operacional = Entradas Op - Saídas Op

                            GrupoAux itemGrupoAuxSaldo1 = new GrupoAux();
                            itemGrupoAuxSaldo1.NomeGrupo = "$ SALDO OPERACIONAL $";
                            itemGrupoAuxSaldo1.TotalCategorias = Math.Round((_saldoEntrada - _saldoSaida), 2);

                            _listaGrupoDetalhe.Add(itemGrupoAuxSaldo1);

                        //**** Fim Saldo Operacional

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesRecebimentos(4); //Recebimento Financeiro

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);                                                        

                        //Saldo do Recebimento para Saldo Financeiro
                        _saldoRecebimento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de entrada para Saldo Final
                        _saldoEntrada = _saldoEntrada + itemGrupoAux.TotalCategorias;

                        break;

                    case 4://PAGAMENTO FINANCEIRO

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(5);//Pagamento Financeiro

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo Pagamento para Saldo Financeiro
                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        break;

                    case 5://RECEBIMENTO DE INVESTIMENTO

                        //Saldo Financeiro = Recebimento - Pagamento

                            GrupoAux itemGrupoAuxSaldo2 = new GrupoAux();
                            itemGrupoAuxSaldo2.NomeGrupo = "$ SALDO FINANCEIRO $";
                            itemGrupoAuxSaldo2.TotalCategorias = Math.Round((_saldoRecebimento - _saldoPagamento), 2);

                            _listaGrupoDetalhe.Add(itemGrupoAuxSaldo2);

                        //**** Fim Saldo Financeiro

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesRecebimentos(6); //Recebimento de Investimentos

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo do Recebimento para Saldo Financeiro
                        _saldoRecebimento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de entrada para Saldo Final
                        _saldoEntrada = _saldoEntrada + itemGrupoAux.TotalCategorias;

                        break;

                    case 6: //PAGAMENTO DE INVESTIMENTO

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividadesPagamentos(7);//Pagamento de Investimentos

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.Sum(x => x.Total);

                        //Saldo Pagamento para Saldo Financeiro
                        _saldoPagamento = itemGrupoAux.TotalCategorias;

                        //Soma do saldo de saída para saldo final
                        _saldoSaida = _saldoSaida + itemGrupoAux.TotalCategorias;

                        break;

                    case 7: //TRANSFERENCIA DE SALDOS

                        //Saldo Investimento = Recebimento - Pagamento

                            GrupoAux itemGrupoAuxSaldo3 = new GrupoAux();
                            itemGrupoAuxSaldo3.NomeGrupo = "$ SALDO INVESTIMENTO $";
                            itemGrupoAuxSaldo3.TotalCategorias = Math.Round((_saldoRecebimento - _saldoPagamento), 2);

                            _listaGrupoDetalhe.Add(itemGrupoAuxSaldo3);

                        //**** Fim Saldo Financeiro

                        itemGrupoAux.NomeGrupo = itemGrupo.Descricao;
                        itemGrupoAux.ListaDeSubGrupos = carregaAtividades(itemGrupo.Id);

                        //Total do Grupo
                        itemGrupoAux.TotalCategorias = itemGrupoAux.ListaDeSubGrupos.FindAll(x => x.NomeSubGrupo.Contains("RECEBIMENTO")).Sum(Y => Y.Total) -
                                                        itemGrupoAux.ListaDeSubGrupos.FindAll(x => x.NomeSubGrupo.Contains("PAGAMENTO")).Sum(Y => Y.Total);

                        _saldoEntrada = _saldoEntrada + itemGrupoAux.TotalCategorias;

                        break;
                }

                if(itemGrupo.Id <= 7)
                    _listaGrupoDetalhe.Add(itemGrupoAux);
            }

            //Saldo Final - Bancos + Caixas + Entradas - Saídas
            GrupoAux itemGrupoAuxSaldo = new GrupoAux();
            itemGrupoAuxSaldo.NomeGrupo = "$ SALDO FINAL $";
            itemGrupoAuxSaldo.TotalCategorias = Math.Round((_saldoInicial + _saldoEntrada - _saldoSaida),2);

            _listaGrupoDetalhe.Add(itemGrupoAuxSaldo);

            //Vai ficar por último, pois carrega o grid completo
            gcGridFluxoCaixa.Refresh();
            gcGridFluxoCaixa.DataSource = _listaGrupoDetalhe;

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

                    case  1:
                        itemSubGrupoAux.NomeSubGrupo = "DINHEIRO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 1 });
                                        
                        break;

                    case 2:
                        itemSubGrupoAux.NomeSubGrupo = "BOLETO BANCARIO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 2 }, 
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                                        
                        break;

                    case 3:
                        itemSubGrupoAux.NomeSubGrupo = "DÉPOSITO BANCÁRIO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 3 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                                                                                
                        break;

                    case 4:
                        itemSubGrupoAux.NomeSubGrupo = "CHEQUE";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 4 }) +
                            retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 4 }, EnumTipoOperacaoContasPagarReceber.RECEBER);
                                                                               
                        break;

                    case 5:
                        itemSubGrupoAux.NomeSubGrupo = "DUPLICATA";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 5 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                                                                                
                        break;

                    case 6:
                        itemSubGrupoAux.NomeSubGrupo = "CREDIÁRIO PRÓPRIO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 6 },
                                                                                                                                EnumTipoOperacaoContasPagarReceber.RECEBER);
                                                                                
                        break;

                    case 7:
                        itemSubGrupoAux.NomeSubGrupo = "CARTÃO DE CRÉDITO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 7 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                                                                                
                        break;

                    case 8:
                        itemSubGrupoAux.NomeSubGrupo = "CARTÃO DE DÉBITO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 8 },
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);

                        //retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 8 });

                        break;

                    case 9:
                        itemSubGrupoAux.NomeSubGrupo = "CREDITO INTERNO";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 9 },
                                                                                                                                EnumTipoOperacaoContasPagarReceber.RECEBER);
                                                                                
                        break;

                    case 10:
                        //itemSubGrupoAux.NomeSubGrupo = "PIX";
                        //itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 4 },
                        //                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);
                        itemSubGrupoAux.NomeSubGrupo = "PIX";
                        itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(2, new FormaPagamento { Id = 10 }) +
                            retorneTotalFormaPagamentoPorCategoriaNoReceber(2, new FormaPagamento { Id = 10 }, EnumTipoOperacaoContasPagarReceber.RECEBER);

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
                                                                                                                                            EnumTipoOperacaoContasPagarReceber.PAGAR);
                                    //Caixa
                                    itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) +//Dinheiro
                                                                                        retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 });//Cheque

                                    //Banco
                                    itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA);
                               if (itemcategoriaAux.Valor != 0)
                                {
                                    itemSubGrupoAux.ListaDeCategorias.Add(itemcategoriaAux);
                                }
                            
                                
                                    
                                
                            }
                            
                        }

                        itemSubGrupoAux.Total = itemSubGrupoAux.ListaDeCategorias.Sum(x => x.Valor);
                        if (itemSubGrupoAux.Total != 0)
                        {
                            listaItemSubGrupoAux.Add(itemSubGrupoAux);
                        }
                            

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
                                                                                                                                        EnumTipoOperacaoContasPagarReceber.PAGAR);
                                //Caixa
                                itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                                    retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                                //Banco
                                itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA);
                                if (itemcategoriaAux.Valor != 0)
                                    {
                                        itemSubGrupoAux.ListaDeCategorias.Add(itemcategoriaAux);
                                    }
                            
                            }

                            //Total Geral do SubGrupo
                            itemSubGrupoAux.Total = itemSubGrupoAux.ListaDeCategorias.Sum(x => x.Valor);
                        if (itemSubGrupoAux.Total != 0)
                        {
                            listaItemSubGrupoAux.Add(itemSubGrupoAux);
                        }
                     
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
                                                                                                                                EnumTipoOperacaoContasPagarReceber.RECEBER);
                        //Caixa
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                            retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                        //Banco
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.ENTRADA);

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
                                                                                                                                EnumTipoOperacaoContasPagarReceber.PAGAR);
                        //Caixa
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                            retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                        //Banco
                        itemcategoriaAux.Valor = itemcategoriaAux.Valor + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA);

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
                                                                                                                        EnumTipoOperacaoContasPagarReceber.RECEBER);
                //Caixa
                itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                    retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                //Banco
                itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.ENTRADA);
                if (itemSubGrupoAux.Total != 0)
                {
                    listaItemSubGrupoAux.Add(itemSubGrupoAux);
                }
         
            } 
                   
            return listaItemSubGrupoAux;
        }

        public List<SubGrupoAux> carregaAtividadesPagamentos(int subGrupoId)
        {
            List<SubGrupoAux> listaItemSubGrupoAux = new List<SubGrupoAux>();

            var categoriasCadastradas = new Programax.Easy.Servico.Financeiro.CategoriaServ.ServicoCategoria().
                                                           ConsulteLista(string.Empty, new SubGrupoCategoria { Id = subGrupoId }, "A");

            foreach (var itemCat in categoriasCadastradas)
            {
                if (itemCat.Mostrar == true)
                {
                    SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                    itemSubGrupoAux.NomeSubGrupo = itemCat.Descricao;

                    //Contas a Receber
                    itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoReceber(itemCat.Id, null,
                                                                                                                            EnumTipoOperacaoContasPagarReceber.RECEBER);

                    //Caixa
                    itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                                                        retorneTotalFormaPagamentoPorCategoriaNoCaixa(itemCat.Id, new FormaPagamento { Id = 4 }); //Cheque

                    //Banco
                    itemSubGrupoAux.Total = itemSubGrupoAux.Total + retorneTotalCategoriaNoBanco(itemCat, EnumTipoMovimentacaoBanco.SAIDA);
                    if (itemSubGrupoAux.Total != 0)
                    {
               
                        listaItemSubGrupoAux.Add(itemSubGrupoAux);
                       

                    }

                }


            }

            return listaItemSubGrupoAux;
        }

        public List<CategoriaAgrupada> retorneDetalheFornecedores(int categoriaId, FormaPagamento formaPagamento,
                                                                       EnumTipoOperacaoContasPagarReceber enumReceberPagar)
        {

            var fornecedores = new ServicoContasPagarReceber().ConsulteLista(null, enumReceberPagar, EnumStatusContaPagarReceber.QUITADO,
                                                                 formaPagamento, null, EnumDataFiltrarContasPagarReceber.PAGAMENTO,
                                                                 txtDataInicialPeriodo.Text.ToDateNullabel(), txtDataFinalPeriodo.Text.ToDateNullabel(), null, categoriaId);

            var groupFornecedor = fornecedores.GroupBy(x => x.Pessoa.Id);

            List<CategoriaAgrupada> listaCategoria = new List<CategoriaAgrupada>();
            foreach (var item in groupFornecedor)
            {
                CategoriaAgrupada itemCategoria = new CategoriaAgrupada();

                itemCategoria.NomeCategoria = fornecedores.Find(x=>x.Pessoa.Id == item.Key).Pessoa.DadosGerais.Razao;

                itemCategoria.Valor = fornecedores.FindAll(x => x.Pessoa.Id == item.Key).Sum(x => x.ValorPago) + //Contas a Pagar
                                                               retorneTotalCategoriaNoBanco(new CategoriaFinanceira { Id = categoriaId }, EnumTipoMovimentacaoBanco.SAIDA, new Pessoa { Id = item.Key }) + //Banco
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
                                                                                                EnumTipoOperacaoContasPagarReceber.PAGAR);
                //Caixa
                itemDetalheCategoria.Valor = itemDetalheCategoria.Valor + retorneTotalFormaPagamentoPorCategoriaNoCaixa(item.Id, new FormaPagamento { Id = 1 }) + //Dinheiro
                                             retorneTotalFormaPagamentoPorCategoriaNoCaixa(item.Id, new FormaPagamento { Id = 4 }); //Cheque

                //Banco
                itemDetalheCategoria.Valor = itemDetalheCategoria.Valor + retorneTotalCategoriaNoBanco(item, EnumTipoMovimentacaoBanco.SAIDA);

                itemDetalheCategoria.Descricao = item.Descricao;

                listaDetalheCategoria.Add(itemDetalheCategoria);
            }

            return listaDetalheCategoria;
        }

        public double retorneTotalFormaPagamentoPorCategoriaNoReceber(int categoriaId, FormaPagamento formaPagamento, 
                                                                        EnumTipoOperacaoContasPagarReceber enumReceberPagar)
        {
            return new ServicoContasPagarReceber().ConsulteLista(null, enumReceberPagar, EnumStatusContaPagarReceber.QUITADO, 
                                                                 formaPagamento, null, EnumDataFiltrarContasPagarReceber.PAGAMENTO,
                                                                 txtDataInicialPeriodo.Text.ToDateNullabel(), txtDataFinalPeriodo.Text.ToDateNullabel(), null, categoriaId)
                                                                 .Sum(x=>x.ValorPago);
        }

        public double retorneTotalFormaPagamentoPorCategoriaNoCaixa(int categoriaId, FormaPagamento formaPagamento, Pessoa pessoa=null)
        {
            return new ServicoItemMovimentacaoCaixa().ConsulteListaPorCategoriasEPagamentos(categoriaId, formaPagamento.Id, 
                                                                                            txtDataInicialPeriodo.Text.ToDateNullabel(), 
                                                                                            txtDataFinalPeriodo.Text.ToDateNullabel(), null).Sum(x=>x.Valor);
        }

        public double retorneTotalCategoriaNoBanco(CategoriaFinanceira categoria, EnumTipoMovimentacaoBanco tipo, Pessoa pessoa=null)
        {   
            return new ServicoItemMovimentacaoBanco().ConsulteListaItens(null, txtDataInicialPeriodo.Text.ToDateNullabel(), txtDataFinalPeriodo.Text.ToDateNullabel(),
                                                                            EnumOrigemMovimentacaoBanco.INFORMACAOMANUAL, string.Empty, tipo,
                                                                            string.Empty, pessoa, categoria,null)
                                                                            .Sum(x => x.Valor);
        }
        public double retorneTotalCategoriaNoBancoII(CategoriaFinanceira categoria, EnumTipoMovimentacaoBanco tipo, Pessoa pessoa = null)
        {
            return new ServicoItemMovimentacaoBanco().ConsulteListaItens(null, txtDataInicialPeriodo.Text.ToDateNullabel(), txtDataFinalPeriodo.Text.ToDateNullabel(),
                                                                            EnumOrigemMovimentacaoBanco.CONTAPAGAR, string.Empty, tipo,
                                                                            string.Empty, pessoa, categoria, null)
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
               
                DateTime dataInicial = txtDataInicialPeriodo.Text.ToDate();
                DateTime dataFinal = txtDataFinalPeriodo.Text.ToDate();

                int diasAtraso = txtDiasAtrasoSaldo.Text.ToInt() == 0 ? 1 : txtDiasAtrasoSaldo.Text.ToInt();  

                //Busca o movimento do banco do loop
                var movimentoBanco = new ServicoMovimentacaoBanco().ConsulteRegistrosDeMovimentoDoBanco(itemBanco.Id, dataInicial.AddDays(- diasAtraso), dataInicial.AddDays(diasAtraso));

                SubGrupoAux itemSubGrupoAux = new SubGrupoAux();

                //Item movimento dos bancos (a apartir do movimento) -> buscar o saldo inicial de todos
                foreach (var movBco in movimentoBanco)
                {
                    var dataInicialconsulta = dataInicial;
                    //Soma todos os saldos encontrados pelo movimento do banco do banco do loop
                    if (dataInicialconsulta.DayOfWeek.ToString() == "Sunday")
                    {
                        dataInicialconsulta = dataInicialconsulta.AddDays(1);
                    }
                    
                    var saldoInicialMovimBanco = new ServicoItemMovimentacaoBanco().ConsulteSaldoItensBancoMovimento(movBco,  24, dataInicialconsulta, dataInicialconsulta); //SaldoIncial

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
                var movimentoCaixa = new ServicoMovimentacaoCaixa().ConsulteLista(itemCaixa, EnumDataFiltrarMovimentacaoCaixa.DATAABERTURA, txtDataInicialPeriodo.Text.ToDateNullabel(),
                                                                                    txtDataInicialPeriodo.Text.ToDateNullabel(), null);

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

        public class SubGrupoAux
        {
            public SubGrupoAux()
            {
                ListaDeCategorias = new List<CategoriaAgrupada>();
            }

            //public int Id { get; set; }

            public string NomeSubGrupo { get; set; }
            public List<CategoriaAgrupada> ListaDeCategorias { get; set; }
            public double Total { get; set; }
        }
        
        public class CategoriaAgrupada
        {
            public CategoriaAgrupada()
            {
                ListaDetalhes = new List<DetalheCategoria>();          
            }

            public List<DetalheCategoria> ListaDetalhes { get; set; }

            public string NomeCategoria { get; set; }            
            public double Valor { get; set; }
        }

        public class DetalheCategoria
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
            gcGridFluxoCaixa.ShowPrintPreview();                    
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
            pcLink1.Component = this.gcGridFluxoCaixa;
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
