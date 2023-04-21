using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Gerais;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Vendas.VendaRapidaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Infraestrutura.Negocio.Utils;
using DevExpress.XtraReports.UI;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.LookAndFeel;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Easy.View.Telas.Cadastros.Comissoes;
using Programax.Easy.View.Telas.Vendas.Roteiros;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using System.Data;

using MySql.Data.MySqlClient;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;

namespace Programax.Easy.View.Telas.Vendas.VendaRapida
{
    public partial class FormVendaRapida : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private PedidoDeVenda _pedidoDeVendaEmEdicao;

        private Pessoa _clienteSelecionado;
        private Pessoa _atendenteSelecionado;
        private Pessoa _vendedorSelecionado;

        private EnderecoPessoa _enderecoPessoaSelecionada;

        private Pessoa _transportadoraSelecionada;
        private EnumTipoFrete _tipoFreteSelecionado;
        private DateTime? _dataPrevisaoEntrega;
        private double? _valorFrete;
        private string ConectionStringII;
        private Produto _produtoEmEdicao;

        private TabelaPreco _tabelaPrecoSelecionada;
        private FormaPagamento _formaPagamentoSelecionada;
        private CondicaoPagamento _condicaoPagamentoSelecionada;

        private Parametros _parametros;

        private List<ItemPedidoDeVenda> _listaItensPedidosVenda;
        private ItemPedidoDeVenda _itemPedidoDeVendaEmEdicao;

        private List<ParcelaPedidoDeVenda> _listaParcelasPedidoDeVenda;

        private string _observacoes;

        private bool _variavelControleEditandoOuLimpandoPedido;
        private bool _variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento;
        private bool _variavelControleAlterandoTipoDescontoFechamento;

        private int _NumeroPedidoGerado;

        private Empresa _empresa;
        private string ConectionString;
        private DataSet mDataSet;
        private MySqlDataAdapter mAdapter;
        private Boolean habilitado = false;
        private int dias = 0;
        private int validade = 0;
        private double valor = 0;
        private double percentual = 0;
        private double valorCash = 0;
        private int CodigoCash = 0;
        private int PedidoVendaRefiltek = 0;
        #endregion

        #region " CONSTRUTOR "

        public FormVendaRapida(int pedido)
        {
            InitializeComponent();
            
            PedidoVendaRefiltek = pedido;
            _listaItensPedidosVenda = new List<ItemPedidoDeVenda>();
            _listaParcelasPedidoDeVenda = new List<ParcelaPedidoDeVenda>();

            CarregueParametros();

            PesquiseEmpresa();

            this.ActiveControl = txtIdCliente;
        }

        #endregion

        #region " EVENTOS CONTROLES "

        #region " CLIENTE "

        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (_clienteSelecionado == null || txtIdCliente.Text.ToInt() != _clienteSelecionado.Id)
            {
                if (!string.IsNullOrEmpty(txtIdCliente.Text))
                {
                    ServicoPessoa servicoPessoa = new ServicoPessoa();
                    var cliente = servicoPessoa.ConsulteClienteAtivo(txtIdCliente.Text.ToInt());

                    PreenchaCliente(cliente, true);
                }
                else
                {
                    PreenchaCliente(null);
                }
            }
        }

        private void btnAtalhoCliente_Click(object sender, EventArgs e)
        {
            CadastreClienteRapido();
        }

        private void rdbConsumidorFinal_CheckedChanged(object sender, EventArgs e)
        {
            RecalculeValoresItens();
        }

        private void rdbConsumidorFinal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void rdbRevenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtDescontoFechamento_EditValueChanged(object sender, EventArgs e)
        {
           if (_variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento)
            {
                RecalculeValoresItens();

                _variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento = false;
            }
        }

        private void txtDescontoFechamento_KeyDown(object sender, KeyEventArgs e)
        {
            _variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento = true;
        }

        private void rdbDescontoTotalValor_CheckedChanged(object sender, EventArgs e)
        {
            if (_variavelControleAlterandoTipoDescontoFechamento)
            {
                return;
            }

            double desconto = txtDescontoFechamento.Text.ToDouble();

            if (rdbDescontoTotalValor.Checked)
            {
                txtDescontoFechamento.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
                ConvertaDescontoFechamentoParaDinheiro(desconto);
            }
            else
            {
                txtDescontoFechamento.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
                ConvertaDescontoFechamentoParaPercentual(desconto);
            }
        }

        #endregion

        #region " ATENDENTE "

        private void txtIdAtendente_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAtendente.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var atendente = servicoPessoa.ConsulteAtendenteAtivo(txtIdAtendente.Text.ToInt());

                PreenchaAtendente(atendente, true);
            }
            else
            {
                PreenchaAtendente(null);
            }
        }

        private void btnPesquisaAtendente_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var atendente = formPessoaPesquisa.PesquisePessoaAtendenteAtiva();

            if (atendente != null)
            {
                PreenchaAtendente(atendente);
            }
        }

        #endregion

        #region " VENDEDOR "

        private void txtIdVendedor_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdVendedor.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                var vendedor = servicoPessoa.ConsulteVendedorAtivo(txtIdVendedor.Text.ToInt());

                PreenchaVendedor(vendedor, true);
            }
            else
            {
                PreenchaVendedor(null);
            }
        }

        private void btnPesquisaVendedor_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var vendedor = formPessoaPesquisa.PesquisePessoaVendedoraAtiva();

            if (vendedor != null)
            {
                PreenchaVendedor(vendedor);
            }
        }

        private void btnIndicadores_Click(object sender, EventArgs e)
        {
            FormCadastroComissao formCadastroComissao = new FormCadastroComissao();
            formCadastroComissao.ShowDialog();

            PreenchaCboIndicadores();
        }

        #endregion

        #region " ITENS "

        private void txtCodigoDeBarrasProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigoDeBarrasProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(txtCodigoDeBarrasProduto.Text);

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdProduto.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoAtivo(txtIdProduto.Text.ToInt());

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void txtSerialNumber_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerialNumber.Text))
            {
                ServicoProduto servicoProduto = new ServicoProduto();

                var produto = servicoProduto.ConsulteProdutoSerialNumberAtivo(txtSerialNumber.Text);

                PreenchaProduto(produto, exibirMensagemDeNaoEncontrado: true);
            }
            else
            {
                PreenchaProduto(null);
            }
        }

        private void btnPesquisaProduto2_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProdutoAtivo();

            if (produto != null)
            {
                PreenchaProduto(produto);
            }
        }

        private void txtValorUnitarioProduto_Leave(object sender, EventArgs e)
        {
            CalculeValoresTotaisCamposProduto();
        }

        private void txtDescontoProduto_Leave(object sender, EventArgs e)
        {
            CalculeValoresTotaisCamposProduto();
        }

        private void txtQuantidadeProduto_Leave(object sender, EventArgs e)
        {
            CalculeValoresTotaisCamposProduto();
        }

        //private void txtValorUnitarioProduto_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalculeValoresTotaisCamposProduto();
        //}

        //private void txtDescontoProduto_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalculeValoresTotaisCamposProduto();
        //}

        //private void txtQuantidadeProduto_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalculeValoresTotaisCamposProduto();
        //}

        private void rdbDescontoProdutoValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDescontoProdutoValor.Checked)
            {
                txtDescontoUnitarioProduto.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtDescontoUnitarioProduto.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
            }

            txtDescontoUnitarioProduto.Text = string.Empty;

            CalculeValoresTotaisCamposProduto();

            txtDescontoUnitarioProduto.Focus();
        }

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeItemPedido();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            EditeItem();
        }

        private void gcItens_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeItem();
            }
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (_listaItensPedidosVenda.Count > 0)
            {
                var idItem = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

                var itemASerExcluido = _listaItensPedidosVenda.FirstOrDefault(x => x.Id == idItem);

                var mensagemConfirmacaoExclusao = "Deseja excluir o item " + itemASerExcluido.Produto.Id + " - " + itemASerExcluido.Produto.DadosGerais.Descricao + " ?";

                if (MessageBox.Show(mensagemConfirmacaoExclusao, "Deseja excluir este item ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    _listaItensPedidosVenda.Remove(itemASerExcluido);

                    PreenchaGridItens();
                }
            }
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposProduto();
        }

        private void rdbDescontoProdutoPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoUnitarioProduto.Focus();
            }
        }

        private void rdbDescontoProdutoValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoUnitarioProduto.Focus();
            }
        }

        private void txtQuantidadeProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculeValoresTotaisCamposProduto();
                InsiraOuAtualizeItemPedido();
            }
        }

        #endregion

        #region " EVENTOS BOTÕES OUTRAS OPÇÕES "

        private void btnCadastroCliente_Click(object sender, EventArgs e)
        {
            CadastreClienteRapido();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormFinanceiroVendaRapida formFinanceiroVendaRapida = new FormFinanceiroVendaRapida();
            _listaParcelasPedidoDeVenda = formFinanceiroVendaRapida.EditeFinanceiro(_listaParcelasPedidoDeVenda, txtIdCliente.Text.ToInt());
            
        }

        private void btnTabelaPreco_Click(object sender, EventArgs e)
        {
            FormTabelaPrecoVendaRapida formTabelaPrecoVendaRapida = new FormTabelaPrecoVendaRapida();
            var resultado = formTabelaPrecoVendaRapida.EditeTabelaPreco(_tabelaPrecoSelecionada, txtIdCliente.Text.ToInt());

            if (resultado == DialogResult.OK)
            {
                _tabelaPrecoSelecionada = formTabelaPrecoVendaRapida.TabelaPrecoSelecionada;

                ReprocessarItens();
            }
        }

        private void btnObservacoes_Click(object sender, EventArgs e)
        {
            FormObservacoesVendaRapida formObservacoesVendaRapida = new FormObservacoesVendaRapida();
            _observacoes = formObservacoesVendaRapida.EditeObservacoes(_observacoes);
        }

        private void btnFormaECondicaoPagamento_Click(object sender, EventArgs e)
        {
            FormFormaCondicaoPagamentoVendaRapida formFormaCondicaoPagamentoVendaRapida = new FormFormaCondicaoPagamentoVendaRapida();
            var resultado = formFormaCondicaoPagamentoVendaRapida.EditeFormaECondicaoPagamento(_formaPagamentoSelecionada, _condicaoPagamentoSelecionada, txtIdCliente.Text.ToInt());

            if (resultado == DialogResult.OK)
            {
                _formaPagamentoSelecionada = formFormaCondicaoPagamentoVendaRapida.FormaPagamento;
                _condicaoPagamentoSelecionada = formFormaCondicaoPagamentoVendaRapida.CondicaoPagamento;

                CalculeTotais();
            }
        }

        private void btnTransportadora_Click(object sender, EventArgs e)
        {
            FormTransportadoraVendaRapida formTransportadoraVendaRapida = new FormTransportadoraVendaRapida();
            var resultado = formTransportadoraVendaRapida.EditeTransportadora(_transportadoraSelecionada, _tipoFreteSelecionado, _dataPrevisaoEntrega, _valorFrete);

            if (resultado == DialogResult.OK)
            {
                _transportadoraSelecionada = formTransportadoraVendaRapida.Transportadora;
                _tipoFreteSelecionado = formTransportadoraVendaRapida.TipoFrete;
                _dataPrevisaoEntrega = formTransportadoraVendaRapida.DataPrevisaoEntrega;
                _valorFrete = formTransportadoraVendaRapida.ValorFrete;

                RecalculeValoresItens();
            }
        }

        #endregion

        #region " EVENTOS BARRAS DE BOTÕES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpeVendaRapida();
        }

        private void btnFecharVenda_Click(object sender, EventArgs e)
        {
            if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
            {
                if(cboIndicadores.EditValue.ToInt() == 0)
                {
                    MessageBox.Show("É obrigatório informar o indicador para Fechar o Pedido.","Fechando o Pedido",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtIdVendedor.Text.ToInt() == 0)
                {
                    MessageBox.Show("É obrigatório informar o vendedor para Fechar o Pedido.", "Fechando o Pedido",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            Action actionFechamentoDePedido = () =>
            {
                var pedidoDeVendaEmEdicao = RetornePedidoDeVendaEmEdicao();

                ServicoVendaRapida servicoVendaRapida = new ServicoVendaRapida();

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            

                var tipoMensagem = servicoPedidoDeVenda.VerificaEstoqueNegativo(pedidoDeVendaEmEdicao);

                //Mensagem informando o parâmetro: "Não aceitar estoque negativo". Não deixar fechar o pedido.
                if (tipoMensagem.SegundaResposta)
                {
                    MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + tipoMensagem.PrimeiroConteudo + "Está ou ficará menor que zero!", "Não é permitido fechar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Mensagem quando preencherem o estoque mínimo. Caso atingir este estoque, o sistema avisa.
                if (tipoMensagem.TerceiraResposta)
                {
                    MessageBox.Show("Verifique os seguintes itens: " + tipoMensagem.SegundoConteudo + ".", "O estoque mínimo foi atingido!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                servicoVendaRapida.ValidePedidoDeVenda(pedidoDeVendaEmEdicao);

                try
                {
                    //Para ignorar a validação do crédito inicial, carrega-se com um valor bem alto. 


                    if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)
                    {
                        foreach (var itemproduto in _listaItensPedidosVenda)
                        {
                            AlteraReserva(itemproduto.Produto.Id, itemproduto.Produto.FormacaoPreco.EstoqueReservado);
                        }
                    }
                    if (_parametros.ParametrosFinanceiro.IgnorarCreditoInicial)
                        pedidoDeVendaEmEdicao.SaldoDisponivel = 999999999;
                    else if((EnumTipoFormaPagamento) pedidoDeVendaEmEdicao.FormaPagamento.Id==EnumTipoFormaPagamento.DINHEIRO ||
                            (EnumTipoFormaPagamento) pedidoDeVendaEmEdicao.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO ||
                            (EnumTipoFormaPagamento) pedidoDeVendaEmEdicao.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO) 
                                pedidoDeVendaEmEdicao.SaldoDisponivel = 999999999; //Se for dinheiro, débito ou crédito vai ignorar a validação.

                    servicoVendaRapida.ValidePedidoParaReserva(pedidoDeVendaEmEdicao);

                    Action actionFechamentoPedido = () =>
                    {
                        ServicoProduto servicoProduto = new ServicoProduto();

                      
                        servicoVendaRapida.FechePedidoDeVenda(pedidoDeVendaEmEdicao);

                       
                       

                        _NumeroPedidoGerado = pedidoDeVendaEmEdicao.Id;

                        MessageBox.Show("Número do pedido " + pedidoDeVendaEmEdicao.Id + ".");

                        if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                        {
                            PergunteSeDesejaAgendarPedido(pedidoDeVendaEmEdicao.Id);
                        }

                        PergunteSeDesejaImprimirPedido(pedidoDeVendaEmEdicao.Id);

                        LimpeVendaRapida();

                    };
            
                    TratamentosDeTela.TrateInclusaoEAtualizacao(actionFechamentoPedido, mensagemDeSucesso: "Pedido fechado com sucesso.");
            
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("O pedido precisa passar pela liberação, Deseja envia-lo?", "Enviar pedido para liberação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Action actionLiberacaoPedido = () =>
                        {
                            servicoVendaRapida.EnviePedidoDeVendaParaLiberacao(pedidoDeVendaEmEdicao);

                            MessageBox.Show("Número do pedido " + pedidoDeVendaEmEdicao.Id + ".");
                            //andre pedido de venda
                            _NumeroPedidoGerado = pedidoDeVendaEmEdicao.Id;

                            if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                            {
                                PergunteSeDesejaAgendarPedido(pedidoDeVendaEmEdicao.Id);
                            }

                            PergunteSeDesejaImprimirPedido(pedidoDeVendaEmEdicao.Id);

                            LimpeVendaRapida();
                        };

                        TratamentosDeTela.TrateInclusaoEAtualizacao(actionLiberacaoPedido, mensagemDeSucesso: "O Pedido foi enviado para liberação.");
                    }
                }

            };

           

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionFechamentoDePedido, exibirMensagemDeSucesso: false);

            if (PedidoVendaRefiltek == 0)
            {


       
                return;
            }

            bool refiltek = _parametros.ParametrosVenda.Refiltek;

            if (refiltek == false)
            {
     
                return;
            }
            string conexoesStringII = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoesII.json");

            ConexoesJsonII conexoesII = JsonConvert.DeserializeObject<ConexoesJsonII>(conexoesStringII);

            var item = conexoesII.ConexoesII[IndiceBancoDados];
            string ipServer = !string.IsNullOrEmpty(item.IpPrincipal) ? item.IpPrincipal : "localhost";
            string database = !string.IsNullOrEmpty(item.BancoDadosPrincipal) ? item.BancoDadosPrincipal : "akilsmallbusiness";
            string userId = !string.IsNullOrEmpty(item.UsuarioPrincipal) ? item.UsuarioPrincipal : "root";
            string senha = !string.IsNullOrEmpty(item.SenhaPrincipal) ? item.SenhaPrincipal : "Progr@max-2015";
            int porta = item.PortaSecundaria != 0 ? item.PortaSecundaria : 3306;

            var serverPrincipalOnline = InfraUtils.VerifiqueSeIpEPortaEstahAtivo(ipServer, porta);

            if (serverPrincipalOnline)
            {
                ConectionStringII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";" + "default command timeout = 240";
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
                    StringConexaoII = "Persist Security Info=False;server=" + ipServer + ";port=" + porta + ";database=" + database + ";uid=" + userId + ";pwd=" + senha + ";";
                }
            }

            using (var conn = new MySqlConnection(ConectionStringII))
            {
                conn.Open();

                string sqlWhere = "  pedido_id = " + PedidoVendaRefiltek;
                var sql = " select pedido_id " +

                " FROM  pedidosvendas " +

                    " WHERE " + sqlWhere;



                MySqlCommand MyCommand = new MySqlCommand(sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();

                while (returnValue.Read())
                {
                    using (var connII = new MySqlConnection(ConectionStringII))
                    {

                        connII.Open();

                        string SqlII = "update roteiros set rot_status = " + 2 +
                                        " where rot_pedido_id = " + PedidoVendaRefiltek;


                        MySqlCommand MyCommandII = new MySqlCommand(SqlII, connII);
                        MySqlDataReader MyReaderII;


                        var returnValueII = MyCommandII.ExecuteReader();


                    }

                }


                this.Cursor = Cursors.Default;
            }

          


        }
        private void AlteraReserva(int produto, double quantidade)
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

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
            string quant = quantidade.ToString();

            quant = quant.ToString().Replace(",", ".");


            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update produtos set PROD_ESTOQUE_RESERVADO = '" + quant + "'" + " where prod_id=" + produto;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda(somenteImpressao: true);
            formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();
        }

        #endregion

        #endregion

        #region " MÉTODOS PRIVADOS "        

        #region " PREENCHIMENTO DE CLIENTE, ATENDENTE E VENDEDOR "

        private void PreenchaCliente(Pessoa cliente, bool exibirMensagemDeNaoEncontrado = false)
        {
            _clienteSelecionado = cliente;
            _enderecoPessoaSelecionada = null;

            if (cliente != null)
            {
                _clienteSelecionado.Atendimento.CarregueLazyLoad();

                txtIdCliente.Text = cliente.Id.ToString();
                txtNomeCliente.Text = cliente.DadosGerais.Razao;
                txtCpfCnpj.Text = cliente.DadosGerais.CpfCnpj;

                if (_clienteSelecionado.Atendimento != null && _clienteSelecionado.Atendimento.Vendedor != null)
                    PreenchaVendedor(_clienteSelecionado.Atendimento.Vendedor);
                else
                    PreenchaVendedor(null);

                cliente.ListaDeEnderecos.CarregueLazyLoad();

                pnlDadosCliente.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)cliente.DadosGerais.TipoCliente).Checked = true;

                if (cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
                {
                    _enderecoPessoaSelecionada = cliente.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                    if (_enderecoPessoaSelecionada == null)
                    {
                        _enderecoPessoaSelecionada = cliente.ListaDeEnderecos.First();
                    }
                }

                RecalculeValoresItens();

                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
                var analiseCredito = servicoAnaliseCredito.Consulte(txtIdCliente.Text.ToInt());

                if (analiseCredito != null)
                {
                    if (analiseCredito.StatusAnaliseCredito == EnumStatusCrediario.BLOQUEADO)
                    {
                        MessageBox.Show("Este cliente está bloqueado!", "Cliente bloqueado.");

                        PreenchaCliente(null);

                        txtIdCliente.Focus();

                        return;
                    }

                    if (analiseCredito.TabelaPreco != null)
                    {
                        _tabelaPrecoSelecionada = null;
                    }

                    if (analiseCredito.FormaPagamento != null)
                    {
                        _formaPagamentoSelecionada = null;
                    }

                    if (analiseCredito.CondicaoPagamento != null)
                    {
                        _condicaoPagamentoSelecionada = null;
                    }
                }

                FormAvisoClienteInadimplente.ExibaAvisoInadimplente(_clienteSelecionado.Id);
                txtcaschback.Text = "0,00";
                VerificaCashBack();
                this.Focus();
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Cliente nao encontrado!", "Cliente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdCliente.Focus();
                }

                txtIdCliente.Text = string.Empty;
                txtNomeCliente.Text = string.Empty;
                txtCpfCnpj.Text = string.Empty;
                rdbConsumidorFinal.Checked = true;

                FormAvisoClienteInadimplente.EscondaAvisoInadimplente();
            }
        }
        private void VerificaCashBack()
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "Select * from configcashback";
            
                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    dias = 0;
                    validade = 0;
                    int habilitar = returnValue["habilitar"].ToInt();
                    valor = returnValue["valor"].ToDouble();
                    dias = returnValue["dias"].ToInt();
                    validade = returnValue["validade"].ToInt();
                    percentual = returnValue["percentual"].ToDouble();
                    habilitado = false;
                    if(habilitar == 1)
                    {
                        VerificaDesconto();
                    }


                }

            }



        }
        private void VerificaDesconto()
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");

            ConexoesJson conexoes = JsonConvert.DeserializeObject<ConexoesJson>(conexoesString);

            var item = conexoes.Conexoes[IndiceBancoDados];

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
            mDataSet = new DataSet();
            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "Select * from cashback Where cod_cli = " + txtIdCliente.Text + " And status = 0";

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
               
                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    CodigoCash = returnValue["Codigo"].ToInt();
                    DateTime DataValidade = new DateTime();
                    string DataV = returnValue["datacompra"].ToString();
                    DataValidade = DataV.ToDate();

                    DateTime data2 = DataValidade.AddDays(validade);

                    DateTime DataValida = new DateTime();
                    string Data = returnValue["datacompra"].ToString();
                    DataValida = Data.ToDate();


                    DateTime data3 = DataValida.AddDays(dias);

                    double ValorCompra = returnValue["valor"].ToDouble();
                    if (DateTime.Now >= data3)
                    {
                        if (DateTime.Now <= data2)
                        {
                            valorCash = (ValorCompra.ToDouble()) * percentual.ToDouble() / 100;
                            txtcaschback.Text = valorCash.ToString("#,###,##0.00");

                        }
                    }
                    else
                    {
                        txtcaschback.Text = "0,00";
                    }

                }

            }


        }
        private void PreenchaAtendente(Pessoa atendente, bool exibirMensagemDeNaoEncontrado = false)
        {
            _atendenteSelecionado = atendente;

            if (atendente != null)
            {
                txtIdAtendente.Text = atendente.Id.ToString();
                txtNomeAtendente.Text = atendente.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Atendente nao encontrado!", "Atendente não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdAtendente.Focus();
                }

                txtIdAtendente.Text = string.Empty;
                txtNomeAtendente.Text = string.Empty;
            }
        }

        private void PreenchaVendedor(Pessoa vendedor, bool exibirMensagemDeNaoEncontrado = false)
        {
            _vendedorSelecionado = vendedor;

            if (vendedor != null)
            {
                txtIdVendedor.Text = vendedor.Id.ToString();
                txtNomeVendedor.Text = vendedor.DadosGerais.Razao;
            }
            else
            {
                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Vendedor nao encontrado!", "Vendedor não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdVendedor.Focus();
                }

                txtIdVendedor.Text = string.Empty;
                txtNomeVendedor.Text = string.Empty;
            }
        }

        private void PreenchaCboIndicadores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaIndicadoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            ObjetoDescricaoValor obj = new ObjetoDescricaoValor();
            obj.Valor = 0;
            obj.Descricao = "<Indicadores>";

            listaObjetoValor.Add(obj);

            cboIndicadores.Properties.DisplayMember = "Descricao";
            cboIndicadores.Properties.ValueMember = "Valor";
            cboIndicadores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboIndicadores.Text))
            {
                cboIndicadores.EditValue = 0;
            }
        }

        #endregion

        #region " PARAMETROS E LIMPEZA DO FORMULÁRIO "

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();

            PreenchaAtendente(_parametros.ParametrosVenda.Atendente);
            PreenchaVendedor(_parametros.ParametrosVenda.Vendedor);

            _tabelaPrecoSelecionada = _parametros.ParametrosVenda.TabelaPreco;
            _formaPagamentoSelecionada = _parametros.ParametrosVenda.FormaPagamento;
            _condicaoPagamentoSelecionada = _parametros.ParametrosVenda.CondicaoPagamento;
            _transportadoraSelecionada = _parametros.ParametrosVenda.Transportadora;
            _tipoFreteSelecionado = _parametros.ParametrosVenda.TipoFrete;
            _observacoes = _parametros.ParametrosVenda.ObservacoesVendaRapida;

            PreenchaAtendente(_parametros.ParametrosVenda.Atendente);
            PreenchaCboIndicadores();

            if (_parametros.ParametrosVenda.PermiteAlterarIndicador)
            {
                btnIndicadores.Enabled = true;
            }
            else
            {
                btnIndicadores.Enabled = false;
            }

            if (!_parametros.ParametrosVenda.PermiteAlterarValorUnitario)
            {
                txtValorUnitarioProduto.Properties.ReadOnly = true;
            }
        }
               
        private void LimpeVendaRapida()
        {
            _pedidoDeVendaEmEdicao = null;

            _variavelControleEditandoOuLimpandoPedido = true;
            txtcaschback.Text = "0,00";
            _clienteSelecionado = null;
            _atendenteSelecionado = null;
            _vendedorSelecionado = null;

            _transportadoraSelecionada = null;
            _tipoFreteSelecionado = EnumTipoFrete.SEMCOBRANCADEFRETE;
            _dataPrevisaoEntrega = null;
            _valorFrete = null;

            _produtoEmEdicao = null;

            _tabelaPrecoSelecionada = null;
            _formaPagamentoSelecionada = null;
            _condicaoPagamentoSelecionada = null;

            _itemPedidoDeVendaEmEdicao = null;

            _listaItensPedidosVenda.Clear();

            _listaParcelasPedidoDeVenda.Clear();

            _observacoes = string.Empty;

            PreenchaGridItens();
            PreenchaCliente(_clienteSelecionado);
            PreenchaCamposItens(null);
            PreenchaCboIndicadores();

            cboIndicadores.EditValue = 0;

            //Desabilita o desconto no total
            groupControl6.Enabled = false;

            CarregueParametros();

            txtIdCliente.Focus();

            _variavelControleEditandoOuLimpandoPedido = false;

        }

        #endregion

        #region " MÉTODOS RELACIONADOS AOS ITENS "

        private void PreenchaProduto(Produto produto, bool exibirMensagemDeNaoEncontrado = false)
        {
            double itensDisponiveis;
            _produtoEmEdicao = produto;
            double quantidadesubestoque = 0;

            if (produto != null)
            {
                TabelaPreco tabelaPreco = _tabelaPrecoSelecionada;

                if (tabelaPreco == null)
                {
                    MessageBox.Show("Tabela de Preço não informada!");

                    txtIdProduto.Text = string.Empty;
                    txtCodigoDeBarrasProduto.Text = string.Empty;

                    txtCodigoDeBarrasProduto.Focus();

                    return;
                }

                if (produto.Principal != null)
                {
                    produto.Principal.Marca.CarregueLazyLoad();
                    txtSerialNumber.Text = produto.Principal.CodigoFabricante;
                }

                if (produto.Vestuario != null)
                {
                    produto.Vestuario.Tamanho.CarregueLazyLoad();
                    produto.Vestuario.Cor.CarregueLazyLoad();
                }
                double EstoqueReservado = 0;
                double EstoqueDisponivel = 0;
                EstoqueDisponivel = produto.FormacaoPreco.Estoque.ToDouble() - produto.FormacaoPreco.EstoqueReservado.ToDouble();

                if (produto.FormacaoPreco.EstoqueReservado < 0)
                {
                    EstoqueReservado = 0;
                }
                else
                {
                    EstoqueReservado = produto.FormacaoPreco.EstoqueReservado ;
                }

                txtReserva.Text = EstoqueReservado.ToString("0.000");

                ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(produto.Id);

                if (ItemTransferencia != null)
                {
                    foreach (var itemproduto in ItemTransferencia)
                    {
                        quantidadesubestoque += itemproduto.QuantidadeEstoque;
                    }

                    EstoqueDisponivel = produto.FormacaoPreco.Estoque - quantidadesubestoque - EstoqueReservado;
                }
                else
                {
                    EstoqueDisponivel = produto.FormacaoPreco.Estoque - produto.FormacaoPreco.EstoqueReservado;
                }

                if (EstoqueDisponivel < 0)
                {
                    itensDisponiveis = 0;
                }
                else
                {
                    itensDisponiveis = EstoqueDisponivel;
                }

                if (itensDisponiveis <= 0)
                {
                    txtDisponibilidadeItem.ForeColor = System.Drawing.Color.Red;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }
                else
                {
                    txtDisponibilidadeItem.ForeColor = System.Drawing.Color.Black;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }

                produto.DadosGerais.Unidade.CarregueLazyLoad();

                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;

                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;

                var valorUnitario = ServicoVendaRapida.CalculePrecoUnitarioProduto(tabelaPreco, produto);

                txtValorUnitarioProduto.Text = valorUnitario.ToString("0.00");

                AltereMascaraQuantidadeProduto();

                txtValorUnitarioProduto.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;
                txtValorUnitarioProduto.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto nao encontrado!", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoDeBarrasProduto.Focus();
                }
            }
        }

        private void CalculeValoresTotaisCamposProduto()
        {
            if (string.IsNullOrEmpty(txtIdProduto.Text))
            {                
                return;                
            }

            string ufDestino = _enderecoPessoaSelecionada != null && _enderecoPessoaSelecionada.Cidade != null && _enderecoPessoaSelecionada.Cidade.Estado != null ? _enderecoPessoaSelecionada.Cidade.Estado.UF : string.Empty;
            EnumTipoCliente tipoCliente = (EnumTipoCliente)pnlDadosCliente.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(txtIdCliente.Text.ToInt());

            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();

            var descontoTotal = CalculosPedidoDeVenda.CalculeTotalDesconto(txtValorUnitarioProduto.Text.ToDouble(),
                                                                                                              txtQuantidadeProduto.Text.ToDouble(),
                                                                                                              txtDescontoUnitarioProduto.Text.ToDouble(),
                                                                                                              rdbDescontoProdutoPercentual.Checked);

            var itemPedido = new ItemPedidoDeVenda();
            itemPedido.Produto = new Produto { Id = txtIdProduto.Text.ToInt() };
            itemPedido.ValorUnitario = txtValorUnitarioProduto.Text.ToDouble();
            itemPedido.Quantidade = txtQuantidadeProduto.Text.ToDouble();
            itemPedido.TotalDesconto = descontoTotal;
            itemPedido.ValorFrete = txtvalorFreteProduto.Text.ToDouble();
            itemPedido.ValorIpi = 0;

            double valorTotalItem = 0;
            if (tipoCliente == EnumTipoCliente.REVENDA)
            {

                //Regime Simples Nacional
                if (_empresa.DadosEmpresa.CodigoRegimeTributario != EnumCodigoRegimeTributario.REGIMENORMAL)
                {
                    calculosPedidoDeVenda.DefinaIcmsST(itemPedido, ufDestino, tipoCliente, tipoInscricaoIcms);
                }
                //Regime Normal
                else
                {
                    calculosPedidoDeVenda.DefinaIcmsRegimeNormal(itemPedido, ufDestino, tipoCliente, tipoInscricaoIcms);
                    calculosPedidoDeVenda.DefinaPis(itemPedido, ufDestino, tipoCliente);
                    calculosPedidoDeVenda.DefinaCofins(itemPedido, ufDestino, tipoCliente);
                }

                calculosPedidoDeVenda.DefinaIpi(itemPedido, ufDestino, tipoCliente);

            }

            valorTotalItem = calculosPedidoDeVenda.RetorneValorTotalItem(itemPedido.ValorUnitario,
                                                                                                                       itemPedido.Quantidade,
                                                                                                                       descontoTotal,
                                                                                                                       itemPedido.ValorFrete,
                                                                                                                       itemPedido.ValorIpi,
                                                                                                                       itemPedido.ValorIcmsST);
            

            txtTotalDescontoProduto.Text = descontoTotal.ToString("0.00");
            txtValorIcmsSTProduto.Text = itemPedido.ValorIcmsST != null ? itemPedido.ValorIcmsST.Value.ToString("0.00") : string.Empty;
            txtValorTotalProduto.Text = valorTotalItem.ToString("0.00");
            txtValorIPIProduto.Text = itemPedido.ValorIpi != null ? itemPedido.ValorIpi.Value.ToString("0.00") : string.Empty;
            
        }

        private void InsiraOuAtualizeItemPedido()
        {
            if (_produtoEmEdicao == null || _clienteSelecionado == null)
            {
                return;
            }
           
            ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
            var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(txtIdProduto.Text.ToInt());

          

            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
            bool ReserveEstoqueAoFaturarPedido = _parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido;

            if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(txtQuantidadeProduto.Text.ToDouble(), _produtoEmEdicao, ReserveEstoqueAoFaturarPedido))

            {
                if (ItemTransferencia.Count > 0)
                {
                    ServicoSubEstoque servicoSubEstoque = new ServicoSubEstoque();
                    var subestoque = servicoSubEstoque.Consulte(ItemTransferencia[0].SubEstoque);

                    MessageBox.Show("Este item encontra-se no seguinte subestoque: " + subestoque.Descricao + " "
                                     + "Produto não está disponível!",
                                    "Verifique o estoque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                    MessageBox.Show("O estoque do seguinte item: " + _produtoEmEdicao.Id + " - " + _produtoEmEdicao.DadosGerais.Descricao
                                    + ". Pode estar zerado, Reservado ou a quantidade requerida não está disponível!",
                                    "Verifique o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Action actionInserirItem = () =>
            {
                ItemPedidoDeVenda itemPedidoVenda = RetorneItemPedidoDeVendaEmEdicao();
                PedidoDeVenda pedidoDeVenda = RetornePedidoDeVendaEmEdicao();

                itemPedidoVenda.PedidoDeVenda = pedidoDeVenda;

                ServicoVendaRapida servicoVendaRapida = new ServicoVendaRapida();

                servicoVendaRapida.ValideItemPedidoVenda(itemPedidoVenda);

                try
                {
                    if(itemPedidoVenda.Produto.FormacaoPreco.EhPromocao==false || itemPedidoVenda.DescontoUnitario !=0)
                        servicoVendaRapida.ValideItemPedidoVendaLiberacao(itemPedidoVenda);

                    itemPedidoVenda.ItemEstahInconsistente = false;
                }
                catch
                {
                    itemPedidoVenda.ItemEstahInconsistente = true;
                }
                               
                if (itemPedidoVenda.Id == 0)
                {
                    bool atualizouInclusao = false;
                    foreach (var item in _listaItensPedidosVenda)
                    {
                        if(item.Produto.Id == itemPedidoVenda.Produto.Id)
                        {
                            item.Quantidade += itemPedidoVenda.Quantidade;
                            item.ValorTotal += itemPedidoVenda.ValorTotal;
                            item.DescontoUnitario += itemPedidoVenda.DescontoUnitario;
                            item.TotalDesconto += itemPedidoVenda.TotalDesconto;
                            
                            atualizouInclusao = true;
                        }
                    }
                    if (itemPedidoVenda.Produto.FormacaoPreco.EstoqueReservado < 0)
                    {
                        itemPedidoVenda.Produto.FormacaoPreco.EstoqueReservado = 0;
                    }
                    
                    if (!atualizouInclusao)
                        _listaItensPedidosVenda.Add(itemPedidoVenda);                    
                }
                else
                {
                    _listaItensPedidosVenda.Remove(_itemPedidoDeVendaEmEdicao);

                    int posicaoItem = itemPedidoVenda.Id - 1;

                    _listaItensPedidosVenda.Insert(posicaoItem, itemPedidoVenda);
                }
               
                //Habilita o desconto no total
                groupControl6.Enabled = true;

                LimpeCamposProduto();

                PreenchaGridItens();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, exibirMensagemDeSucesso: false);
        }

        private ItemPedidoDeVenda RetorneItemPedidoDeVendaEmEdicao()
        {
            _itemPedidoDeVendaEmEdicao = _itemPedidoDeVendaEmEdicao ?? new ItemPedidoDeVenda();

            var item = _itemPedidoDeVendaEmEdicao.CloneCompleto();

            item.DescontoUnitario = txtDescontoUnitarioProduto.Text.ToDouble();
            item.DescontoEhPercentual = rdbDescontoProdutoPercentual.Checked;
            item.TotalDesconto = txtTotalDescontoProduto.Text.ToDouble();
            item.ValorFrete = txtvalorFreteProduto.Text.ToDouble();
            item.ValorIcmsST = txtValorIcmsSTProduto.Text.ToDouble();
            item.ValorIpi = txtValorIPIProduto.Text.ToDouble();

            item.Produto = _produtoEmEdicao;
            item.Quantidade = txtQuantidadeProduto.Text.ToDouble();
            item.ValorUnitario = txtValorUnitarioProduto.Text.ToDouble();
            item.ValorTotal = txtValorTotalProduto.Text.ToDouble();

            return item;
        }

        private PedidoDeVenda RetornePedidoDeVendaEmEdicao()
        {
            _pedidoDeVendaEmEdicao = _pedidoDeVendaEmEdicao ?? new PedidoDeVenda();

            _pedidoDeVendaEmEdicao.Atendente = _atendenteSelecionado;
            _pedidoDeVendaEmEdicao.Vendedor = _vendedorSelecionado;

            if (_clienteSelecionado.Atendimento == null)
            {
                _pedidoDeVendaEmEdicao.Indicador = _clienteSelecionado.Atendimento != null ? new Pessoa { Id = _clienteSelecionado.Atendimento.Indicador.Id } : null;
                _pedidoDeVendaEmEdicao.Supervisor = _clienteSelecionado.Atendimento != null ? new Pessoa { Id = _clienteSelecionado.Atendimento.Supervisor.Id } : null;
            }
            else
            {
                _pedidoDeVendaEmEdicao.Indicador = _clienteSelecionado.Atendimento.Indicador != null ? new Pessoa { Id = _clienteSelecionado.Atendimento.Indicador.Id } : null;
                _pedidoDeVendaEmEdicao.Supervisor = _clienteSelecionado.Atendimento.Supervisor != null ? new Pessoa { Id = _clienteSelecionado.Atendimento.Supervisor.Id } : null;
            }
            _pedidoDeVendaEmEdicao.Cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;

            _pedidoDeVendaEmEdicao.Usuario = Sessao.PessoaLogada;

            _pedidoDeVendaEmEdicao.TipoCliente = (EnumTipoCliente)pnlDadosCliente.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            _pedidoDeVendaEmEdicao.CondicaoPagamento = _condicaoPagamentoSelecionada;
            _pedidoDeVendaEmEdicao.DataElaboracao = DateTime.Now.Date;
            _pedidoDeVendaEmEdicao.DataPrevisaoEntrega = _dataPrevisaoEntrega;

            _pedidoDeVendaEmEdicao.FormaPagamento = _formaPagamentoSelecionada;

            if (!string.IsNullOrEmpty(txtIdCliente.Text))
            {
                ServicoPessoa servicoPessoa = new ServicoPessoa();
                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
                var cliente = servicoPessoa.Consulte(txtIdCliente.Text.ToInt());
                cliente.ListaDeEnderecos.CarregueLazyLoad();

                var analiseCredito = servicoAnaliseCredito.Consulte(txtIdCliente.Text.ToInt());
                analiseCredito = analiseCredito ?? new Crediario();

                if (cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
                {
                    var enderecoCliente = cliente.ListaDeEnderecos.FirstOrDefault(endereco => endereco.TipoEndereco == EnumTipoEndereco.PRINCIPAL);

                    if (enderecoCliente == null)
                    {
                        enderecoCliente = cliente.ListaDeEnderecos.First();
                    }

                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.CEP = enderecoCliente.CEP;
                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.Bairro = enderecoCliente.Bairro;
                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.Rua = enderecoCliente.Rua;
                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.Cidade = enderecoCliente.Cidade;
                    if (enderecoCliente.Complemento != null)
                    {
                        if (enderecoCliente.Complemento.Length > 49)
                        {
                            _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.Complemento = enderecoCliente.Complemento.Substring(0, 49);
                        }
                        else
                        {
                            _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.Complemento = enderecoCliente.Complemento;
                        }
                    }
                 
                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.Numero = enderecoCliente.Numero;
                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.TipoEndereco = enderecoCliente.TipoEndereco;
                    _pedidoDeVendaEmEdicao.EnderecoPedidoDeVenda.ClienteResideExterior = enderecoCliente.Pessoa.DadosGerais.PessoaResideExterior;
                }


                _pedidoDeVendaEmEdicao.LimiteDeCredito = analiseCredito.ValorLimiteCredito;

                ServicoContasReceber servicoContasReceber = new ServicoContasReceber();
                var listaContasAReceber = servicoContasReceber.ConsulteListaAberto(cliente);

                _pedidoDeVendaEmEdicao.AReceberAberto = listaContasAReceber.Sum(x => x.ValorTotal);
                _pedidoDeVendaEmEdicao.SaldoDisponivel = _pedidoDeVendaEmEdicao.LimiteDeCredito - _pedidoDeVendaEmEdicao.AReceberAberto;

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                PedidoDeVenda maiorPedidoDeVenda = servicoPedidoDeVenda.ConsulteMaiorVenda(cliente);

                _pedidoDeVendaEmEdicao.MaiorCompra = maiorPedidoDeVenda != null ? maiorPedidoDeVenda.ValorTotal : 0;


            }

            _pedidoDeVendaEmEdicao.ObservacoesGeraisVenda = _observacoes;
            _pedidoDeVendaEmEdicao.TabelaPreco = _tabelaPrecoSelecionada;
            _pedidoDeVendaEmEdicao.TipoFrete = _tipoFreteSelecionado;

            _pedidoDeVendaEmEdicao.TipoPedidoVenda = EnumTipoPedidoDeVenda.PEDIDOVENDA;

            _pedidoDeVendaEmEdicao.Transportadora = _transportadoraSelecionada;

            _pedidoDeVendaEmEdicao.ValorFrete = _valorFrete.GetValueOrDefault();

            double valorTotalSemDesconto = 0;
            double valorDesconto = 0;

            foreach (var item in _listaItensPedidosVenda)
            {
                double valorItemSemDesconto = item.ValorUnitario * item.Quantidade;

                valorTotalSemDesconto += valorItemSemDesconto;
                valorDesconto += valorItemSemDesconto - item.ValorTotal;
            }

            _pedidoDeVendaEmEdicao.Desconto = valorDesconto;
            _pedidoDeVendaEmEdicao.DescontoEhPercentual = false;
            _pedidoDeVendaEmEdicao.ValorTotal = lblTotalVenda.Text.ToDouble();

            _pedidoDeVendaEmEdicao.EstahPago = false;

            _pedidoDeVendaEmEdicao.Volume = lblVolumes.Text.ToInt();

            _pedidoDeVendaEmEdicao.ListaItens = _listaItensPedidosVenda;
            _pedidoDeVendaEmEdicao.ListaParcelasPedidoDeVenda = _listaParcelasPedidoDeVenda;
            _pedidoDeVendaEmEdicao.Indicador = cboIndicadores.EditValue.ToInt() != 0 ? new Pessoa { Id = cboIndicadores.EditValue.ToInt() } : null;

            return _pedidoDeVendaEmEdicao;
        }

        private void LimpeCamposProduto()
        {
            PreenchaCamposItens(null);
        }

        private void PreenchaCamposItens(ItemPedidoDeVenda itemPedidoVenda)
        {
            double itensDisponiveis;
            _itemPedidoDeVendaEmEdicao = itemPedidoVenda;

            if (itemPedidoVenda != null)
            {
                _produtoEmEdicao = itemPedidoVenda.Produto;

                rdbDescontoProdutoValor.Checked = true;

                txtCodigoDeBarrasProduto.Text = itemPedidoVenda.Produto.DadosGerais.CodigoDeBarras;

                rdbDescontoProdutoPercentual.Checked = itemPedidoVenda.DescontoEhPercentual;
                txtDescontoUnitarioProduto.Text = itemPedidoVenda.DescontoUnitario.ToString("0.00");
                
                txtTotalDescontoProduto.Text = itemPedidoVenda.TotalDesconto.ToString("0.00");
                txtIdProduto.Text = itemPedidoVenda.Produto.Id.ToString();
                txtQuantidadeProduto.Text = itemPedidoVenda.Quantidade.ToString();
                txtValorUnitarioProduto.Text = itemPedidoVenda.ValorUnitario.ToString("0.00");
                txtvalorFreteProduto.Text = itemPedidoVenda.ValorFrete.ToString("0.00");
                txtValorIPIProduto.Text = itemPedidoVenda.ValorIpi != null ? itemPedidoVenda.ValorIpi.Value.ToString("0.00") : string.Empty;
                txtValorIcmsSTProduto.Text = itemPedidoVenda.ValorIcmsST != null ? itemPedidoVenda.ValorIcmsST.Value.ToString("0.00") : string.Empty;
                txtValorTotalProduto.Text = itemPedidoVenda.ValorTotal.ToString("0.00");

                itensDisponiveis = itemPedidoVenda.Produto.FormacaoPreco.Estoque - itemPedidoVenda.Produto.FormacaoPreco.EstoqueReservado;

                if (itensDisponiveis < 0)
                {
                    txtDisponibilidadeItem.ForeColor = System.Drawing.Color.Red;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }
                else
                {
                    txtDisponibilidadeItem.ForeColor = System.Drawing.Color.Black;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }

                txtDescricaoProduto.Text = itemPedidoVenda.Produto.DadosGerais.Descricao;

                AltereMascaraQuantidadeProduto();

                txtValorUnitarioProduto.Focus();

                btnInserirAtualizarItem.Image = Properties.Resources.icone_atualizar_branco;
            }
            else
            {
                _produtoEmEdicao = null;

                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtSerialNumber.Text = string.Empty;
                txtDescontoUnitarioProduto.Text = string.Empty;
                rdbDescontoProdutoPercentual.Checked = true;
                txtIdProduto.Text = string.Empty;
                txtQuantidadeProduto.Text = string.Empty;

                txtTotalDescontoProduto.Text = string.Empty;

                txtValorUnitarioProduto.Text = string.Empty;
                txtValorTotalProduto.Text = string.Empty;
                txtvalorFreteProduto.Text = string.Empty;
                txtValorIPIProduto.Text = string.Empty;
                txtValorIcmsSTProduto.Text = string.Empty;

                txtDisponibilidadeItem.ForeColor = System.Drawing.Color.Black;
                txtDisponibilidadeItem.Text = string.Empty;

                txtDescricaoProduto.Text = string.Empty;

                txtCodigoDeBarrasProduto.Focus();

                btnInserirAtualizarItem.Image = Properties.Resources.icone_adicionar_branco;
            }
        }

        private void PreenchaGridItens()
        {
            GereIdFalsoParaOsItens();

            List<ItemGrid> listaItemGrid = new List<ItemGrid>();

            foreach (var item in _listaItensPedidosVenda)
            {
                ItemGrid itemGrid = new ItemGrid();

                itemGrid.Id = item.Id;
                itemGrid.CodigoDeBarras = item.Produto.DadosGerais.CodigoDeBarras;
                itemGrid.IdProduto = item.Produto.Id;
                itemGrid.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                itemGrid.Descricao = item.Produto.DadosGerais.Descricao;
                itemGrid.Desconto = "R$" + item.TotalDesconto.ToString("0.00");

                itemGrid.MarcaFabricante = item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                itemGrid.Modelo = item.Produto.Vestuario != null ? item.Produto.Vestuario.Modelo : string.Empty;
                itemGrid.Quantidade = item.Quantidade.ToDouble();
                itemGrid.Sexo = item.Produto.Vestuario != null && item.Produto.Vestuario.SexoProduto != null ? item.Produto.Vestuario.SexoProduto.Value.Descricao() : string.Empty;
                itemGrid.Tamanho = item.Produto.Vestuario != null && item.Produto.Vestuario.Tamanho != null ? item.Produto.Vestuario.Tamanho.Descricao : string.Empty;
                itemGrid.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Descricao : string.Empty;
                itemGrid.ValorTotal = item.ValorTotal.ToString("0.00");
                itemGrid.ValorUnitario = item.ValorUnitario.ToString("0.00");
                itemGrid.ItemEstahInconsistente = item.ItemEstahInconsistente;
                if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)
                {
                   itemGrid.QuantidadeReservado = item.Quantidade.ToDouble();
                    item.Produto.FormacaoPreco.EstoqueReservado += item.Quantidade.ToDouble();
                }

                listaItemGrid.Add(itemGrid);
            }

            gcItens.DataSource = listaItemGrid;
            gcItens.RefreshDataSource();

            CalculeTotais();
        }

        private void GereIdFalsoParaOsItens()
        {
            for (int i = 0; i < _listaItensPedidosVenda.Count; i++)
            {
                _listaItensPedidosVenda[i].Id = i + 1;
            }
        }

        private void EditeItem()
        {
            if (_listaItensPedidosVenda != null && _listaItensPedidosVenda.Count > 0)
            {
                var itemPedido = _listaItensPedidosVenda.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());

                PreenchaCamposItens(itemPedido);
            }
        }

        private void AltereMascaraQuantidadeProduto()
        {
            if (_produtoEmEdicao.DadosGerais.PermiteVendaFracionada)
            {
                txtQuantidadeProduto.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtQuantidadeProduto.Properties.Mask.EditMask = @"[0-9]{1,11}";
            }
        }

        #endregion

        #region " CALCULOS "

        private void CalculeTotais()
        {
            double total = 0;
            double volume = 0;
            double pesoBruto = 0;
            double pesoLiquido = 0;
            double totalBrutoProdutos = 0;
            double desconto = 0;

            foreach (var item in _listaItensPedidosVenda)
            {
                pesoBruto += item.Produto.Principal.PesoBruto.GetValueOrDefault() * item.Quantidade;
                pesoLiquido += item.Produto.Principal.PesoLiquido.GetValueOrDefault() * item.Quantidade;

                totalBrutoProdutos += item.Quantidade * item.ValorUnitario;

                desconto += item.TotalDesconto;

                total += item.ValorTotal;
                volume += item.Quantidade;
            }

            lblPesoBruto.Text = pesoBruto.ToString("#,###,##0.00");
            lblPesoLiquido.Text = pesoLiquido.ToString("#,###,##0.00");
            lblVolumes.Text = volume.ToString("#,###,##0.00");
            lblTotalVenda.Text = total.ToString("#,###,##0.00");
            lblTotalDesconto.Text = desconto.ToString("#,###,##0.00");

            if (!_variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento && !_variavelControleAlterandoTipoDescontoFechamento)
            {
                if (rdbDescontoTotalPercentual.Checked)
                {
                    ConvertaDescontoFechamentoParaPercentual(desconto);
                }
                else
                {
                    txtDescontoFechamento.Text = desconto.ToString("0.00");
                }
            }

            GereParcelasFinanceiro();
        }

        private void GereParcelasFinanceiro()
        {
            _listaParcelasPedidoDeVenda.Clear();

            if (_listaItensPedidosVenda.Count > 0 && _condicaoPagamentoSelecionada != null)
            {
              ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

                var condicaoPagamento = servicoCondicaoPagamento.Consulte(_condicaoPagamentoSelecionada.Id);
                var formaPagamento = servicoFormaPagamento.Consulte(_formaPagamentoSelecionada.Id);

                int contador = 0;
                int quantidadeDeParcelas = condicaoPagamento.ListaDeParcelas.Count;
                double totalVendaFechamento = lblTotalVenda.Text.ToDouble();
                double totalSomaParcelas = 0;

                foreach (var parcelaCondicao in condicaoPagamento.ListaDeParcelas)
                {
                    ParcelaPedidoDeVenda parcelaPedidoDeVenda = new ParcelaPedidoDeVenda();

                    parcelaPedidoDeVenda.CondicaoPagamento = condicaoPagamento;
                    parcelaPedidoDeVenda.DataVencimento = DateTime.Now.Date.Date.AddDays(parcelaCondicao.Dias);
                    parcelaPedidoDeVenda.FormaPagamento = formaPagamento;
                    parcelaPedidoDeVenda.Id = contador + 1;
                    parcelaPedidoDeVenda.Parcela = (contador + 1).ToString() + "/" + quantidadeDeParcelas;
                    parcelaPedidoDeVenda.Valor = Math.Round(totalVendaFechamento * parcelaCondicao.PercentualRateio / (double)100, 2);

                    totalSomaParcelas += Math.Round(parcelaPedidoDeVenda.Valor, 2);
                    contador++;

                    _listaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
                }

                var diferenca = Math.Round(totalVendaFechamento - totalSomaParcelas, 2);

                _listaParcelasPedidoDeVenda.Last().Valor += Math.Round(diferenca, 2);
            }
            else
            {
                _listaParcelasPedidoDeVenda.Clear();
            }
        }

        private void ReprocessarItens()
        {
            TabelaPreco tabelaPreco = _tabelaPrecoSelecionada;

            if (tabelaPreco == null)
            {
                MessageBox.Show("Tabela de Preço não informada!");

                txtCodigoDeBarrasProduto.Focus();

                return;
            }

            var pedido = RetornePedidoDeVendaEmEdicao();

            foreach (var item in _listaItensPedidosVenda)
            {
                item.ValorUnitario = ServicoVendaRapida.CalculePrecoUnitarioProduto(tabelaPreco, item.Produto);
                item.PedidoDeVenda = pedido;
            }

            RecalculeValoresItens();
        }

        private void RecalculeValoresItens()
        {
            if (_variavelControleEditandoOuLimpandoPedido)
            {
                return;
            }

            string ufDestino = _enderecoPessoaSelecionada != null && _enderecoPessoaSelecionada.Cidade != null && _enderecoPessoaSelecionada.Cidade.Estado != null ? _enderecoPessoaSelecionada.Cidade.Estado.UF : string.Empty;
            EnumTipoCliente tipoCliente = (EnumTipoCliente)pnlDadosCliente.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(txtIdCliente.Text.ToInt());

            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();
            calculosPedidoDeVenda.RecalculeValoresItens(_variavelControleAlterandoTipoDescontoFechamento || _variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento, rdbDescontoTotalPercentual.Checked, txtDescontoFechamento.Text.ToDouble(),
                                                                                 _valorFrete.GetValueOrDefault(),
                                                                                 _listaItensPedidosVenda,
                                                                                 ufDestino,
                                                                                 tipoCliente,
                                                                                 tipoInscricaoIcms);

            PreenchaGridItens();
        }

        private void ConvertaDescontoFechamentoParaDinheiro(double percentual)
        {
            txtDescontoFechamento.Text = lblTotalDesconto.Text.ToString();

            RecalculeValoresItens();
        }

        private void ConvertaDescontoFechamentoParaPercentual(double valor)
        {
            _variavelControleAlterandoTipoDescontoFechamento = true;

            double totalSemDesconto = 0;

            foreach (var item in _listaItensPedidosVenda)
            {
                totalSemDesconto += item.ValorUnitario * item.Quantidade;
            }

            txtDescontoFechamento.Text = (valor / (double)totalSemDesconto * 100).ToString("0.00");

            RecalculeValoresItens();

            _variavelControleAlterandoTipoDescontoFechamento = false;
        }

        #endregion

        #region " CADASTRO DE CLIENTE "

        private void CadastreClienteRapido()
        {
            Pessoa clienteParaEdicao = !txtIdCliente.Text.EstahVazio() ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;

            FormCadastroClienteVendaRapida formCadastroClienteVendaRapida = new FormCadastroClienteVendaRapida();

            var cliente = formCadastroClienteVendaRapida.CadastreClienteModal(clienteParaEdicao);

            if (cliente != null)
            {
                PreenchaCliente(cliente);

                txtIdAtendente.Focus();
            }
        }

        #endregion

        private void PergunteSeDesejaAgendarPedido(int idPedidoDeVenda)
        {
            if (MessageBox.Show("Deseja Agendar o Pedido?", "Deseja Agendar?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                FormCadastroAgenda formAgenda = new FormCadastroAgenda(_clienteSelecionado, idPedidoDeVenda);
                formAgenda.ShowDialog();
            }
        }

        private void PergunteSeDesejaImprimirPedido(int idPedidoDeVenda)
        {
            if (MessageBox.Show("Deseja imprimir o orçamento / pedido?", "Deseja imprimir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (_parametros.ParametrosVenda.PedidoEmDuasVias)
                {
                    RelatorioPedidoVendaDuasVias relatorioDuasVias = new RelatorioPedidoVendaDuasVias(idPedidoDeVenda);
                    relatorioDuasVias.GereRelatorio();

                    using (ReportPrintTool printTool = new ReportPrintTool(relatorioDuasVias))
                    {
                        // Invoke the Ribbon Print Preview form modally, 
                        // and load the report document into it.
                        printTool.ShowRibbonPreviewDialog();

                        // Invoke the Ribbon Print Preview form
                        // with the specified look and feel setting.
                        printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                    }
                }
                else if(_parametros.ParametrosVenda.PedidoEmImpressoraTermica)
                {
                    RelatorioPedidoVendaReduzido relatorioReduzido = new RelatorioPedidoVendaReduzido(idPedidoDeVenda);
                    relatorioReduzido.GereRelatorio();

                    using (ReportPrintTool printTool = new ReportPrintTool(relatorioReduzido))
                    {
                        // Invoke the Ribbon Print Preview form modally, 
                        // and load the report document into it.
                        printTool.ShowRibbonPreviewDialog();

                        // Invoke the Ribbon Print Preview form
                        // with the specified look and feel setting.
                        printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                    }
                }
                else
                {
                    RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(idPedidoDeVenda , EnumTipoEndereco.PRINCIPAL);
                    relatorio.GereRelatorio();

                    using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                    {
                        // Invoke the Ribbon Print Preview form modally, 
                        // and load the report document into it.
                        printTool.ShowRibbonPreviewDialog();

                        // Invoke the Ribbon Print Preview form
                        // with the specified look and feel setting.
                        printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                    }
                }
            }
        }

        private void PesquiseEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            _empresa.DadosEmpresa.Endereco.Cidade.Estado.CarregueLazyLoad();
        }

        public int RetornePedidoVenda(int idCliente)
        {
            txtIdCliente.Text = idCliente.ToString();

            this.ShowDialog();

            return _NumeroPedidoGerado;
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemGrid
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string CodigoDeBarras { get; set; }

            public string Descricao { get; set; }

            public string Unidade { get; set; }

            public string MarcaFabricante { get; set; }

            public string Tamanho { get; set; }

            public string Cor { get; set; }

            public string Sexo { get; set; }

            public string Modelo { get; set; }

            public double Quantidade { get; set; }
            public double QuantidadeReservado { get; set; }

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }

            public bool ItemEstahInconsistente { get; set; }
        }



        #endregion

        private void txtQuantidadeProduto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtIdCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtIdProduto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtValorUnitarioProduto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gcItens_Click(object sender, EventArgs e)
        {

        }
    }
}
