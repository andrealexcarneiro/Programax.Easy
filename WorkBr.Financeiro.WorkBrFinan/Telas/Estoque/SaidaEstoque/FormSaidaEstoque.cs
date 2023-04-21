using DevExpress.CodeParser;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Gerais;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Servico.Vendas.RecebimentoServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Programax.Easy.Servico.RegistroDeMapeamentos;

namespace Programax.Easy.View.Telas.Estoque.SaidaEstoque
{
    public partial class FormSaidaEstoque : FormularioBase
    {
        #region " VARIAVEIS PRIVADAS "

        private List<ItemPedidoDeVenda> _listaItens;
        private List<ItemPedidoDeVenda> _listaItensBaixa;
        private List<ItemPedidoDeVenda> _listaParaBaixar;
        private List<ItemPedidoDeVenda> _listaItensOriginal;
        private TabelaPreco _tabelaPreco;
        private Parametros _parametros;
        private bool _campoCodigoAlterado;
        private bool _campoPedidoAlterado;
        private int intLinhaGrid = 0; //Variável que pega a linha atual do grid de produtos
        private int _numeroPedidoAnterior;

        private string _codigoBarrasCorrente;

        private Keys _Cancelar; //Se estiver cancelando o PDV não vamos entrar no evento lost focus do desconto  
        private bool _editandoQuantidadeDoItem = false;
        private bool _pesquiseProdutoPeloId;
        private bool _EhLeitorDeCodigoDeBarras;
        private bool _mudaSaldo = true;

        private string ConectionString;
       
        private MySqlDataAdapter mAdapter;

        #endregion

        #region " CONSTRUTOR "

        public FormSaidaEstoque()
        {
            InicieFormSaidaEstoque();
            lblUsuario.Text = Sessao.PessoaLogada.DadosGerais.Razao;

            if (!_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {
                labEstoque.Text = "Estoque";
                labSaldo.Text = "Saldo";
            }

            if(_parametros.ParametrosCadastros.LiberarCampoQtde)
            {
                txtQuantidade.ReadOnly = false;
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void tmrHoraAtual_Tick(object sender, EventArgs e)
        {
            lblDataHoraAtual.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void FormSaidaEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _Cancelar = e.KeyCode;
                var sairPdv = MessageBoxAkil.Show("Deseja cancelar esta listagem de baixa estoque?", "Cancela Baixa", MessageBoxButtons.OKCancel);

                if (sairPdv == DialogResult.Cancel)
                {
                    _Cancelar = Keys.F10;
                    return;
                }

                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                //AltereModoPesquisa();
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (_Cancelar == Keys.Escape)
                {
                    return;
                }

                FinalizaBaixa();

            }
            else if (e.KeyCode == Keys.F4)
            {
                //PesquiseProdutos();
            }
            else if (e.KeyCode == Keys.F5)
            {

            }
            else if (e.KeyCode == Keys.F6)
            {
                //ExcluaItem();
            }
            else if (e.KeyCode == Keys.F7)
            {

            }
            else if (e.KeyCode == Keys.F8)
            {

            }
            else if (e.KeyCode == Keys.F9)
            {

            }
        }

        private void txtCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCodigoBarras.Text.Length >= 3 && _campoCodigoAlterado && e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
            {
                _campoCodigoAlterado = false;
                if (Regex.IsMatch(txtCodigoBarras.Text, @"^[ a-zA-Z á]*$"))
                {
                    //FormPesquisaProdutoPdv formPesquisaProdutoPdv = new FormPesquisaProdutoPdv(_tabelaPreco, txtCodigoBarras.Text);
                    //var produto = formPesquisaProdutoPdv.PesquiseProduto();

                    //if (produto != null)
                    //{
                    //    txtCodigoBarras.Text = produto.Id.ToString();
                    //    AltereModoPesquisaParaCodigoProduto();
                    //    InsiraProduto();
                    //}
                }
            }

            _campoCodigoAlterado = false;
        }

        private void txtCodigoBarras_EditValueChanged(object sender, EventArgs e)
        {
            _campoCodigoAlterado = true;
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _codigoBarrasCorrente = txtCodigoBarras.Text;
                
                InsiraProduto();

                if (_parametros.ParametrosCadastros.LiberarCampoQtde)
                {
                    txtQuantidade.Focus();
                }
            }
        }

        private void txtPedidoVenda_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtPedidoVenda_EditValueChanged(object sender, EventArgs e)
        {
            _campoPedidoAlterado = true;
        }

        private void txtPedidoVenda_KeyUp(object sender, KeyEventArgs e)
        {
            _campoPedidoAlterado = false;            
        }

        private void txtPedidoVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int NumeroPedido = txtPedidoVenda.Text.ToInt();

                if (NumeroPedido == _numeroPedidoAnterior)
                {
                    var sairPdv = MessageBoxAkil.Show("Deseja REINICIAR a baixa de estoque?", "REINICIAR Baixa", MessageBoxButtons.OKCancel);

                    if (sairPdv == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                _numeroPedidoAnterior = NumeroPedido;
                _listaParaBaixar = new List<ItemPedidoDeVenda>();
                _listaItensOriginal = new List<ItemPedidoDeVenda>();
                _listaItens = new List<ItemPedidoDeVenda>();
                _listaItensBaixa = new List<ItemPedidoDeVenda>();
                LimpeTela();
                
                List<ItemPedidoDeVenda> listaItensParaCarregar = new List<ItemPedidoDeVenda>();

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(NumeroPedido);

                if(pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.CANCELADO || 
                    pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO)
                {
                    MessageBoxAkil.Show("Este pedido não pode ser baixado. Pois está: "+ 
                                        pedidoDeVenda.StatusPedidoVenda.Descricao() + ".", "Baixa de Itens", MessageBoxButtons.OK);
                    return;
                }

                if(pedidoDeVenda == null)
                {
                    MessageBoxAkil.Show("Pedido não encontrado", "Busca de Pedido", MessageBoxButtons.OK);
                    return;
                }
                
                if (pedidoDeVenda != null)
                {
                    listaItensParaCarregar = pedidoDeVenda.ListaItens.ToList();
                }

                ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

                var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(NumeroPedido);

                var listaSaida = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA);

                var listaEntrada = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA);

                //Se não tiver nenhuma saída, carrega para a lista para baixar
                if (listaSaida == null || listaSaida.Count == 0)
                {
                    _listaItens = listaItensParaCarregar.ToList();

                    _listaItensOriginal = _listaItens.CloneCompleto();
                    CalculeTotais();
                    PreenchaGrid();
                    _campoPedidoAlterado = false;

                    return;
                }
                                
                if(listaSaida.Count > 0)
                {
                    foreach (var item in pedidoDeVenda.ListaItens)
                    {
                        if (listaSaida.Exists(x=>x.Produto.Id == item.Produto.Id))
                        {
                            double quantEntrada = 0;

                            if (listaEntrada.Exists(y => y.Produto.Id == item.Produto.Id))
                            {
                                  quantEntrada = listaEntrada.FindAll(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade);
                            }
                            
                            var quantPedido = listaItensParaCarregar.FindAll(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade);
                            var quantSaida = listaSaida.FindAll(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade);

                            var diferencaQtde = quantPedido - quantSaida + quantEntrada;
                            
                            
                            if (diferencaQtde > 0)
                            {
                                item.Quantidade = diferencaQtde;
                                _listaItens.Add(item);
                            }
                        }
                        else
                        {
                            _listaItens.Add(item);
                        }
                    }
                }

                if(_listaItens.Count > 0)
                {
                    _listaItensOriginal = _listaItens.CloneCompleto();
                    CalculeTotais();
                    PreenchaGrid();
                    _campoPedidoAlterado = false;
                }                    
                else
                {
                    MessageBoxAkil.Show("Todos os itens deste pedido foi baixado no estoque. Informe outro número de pedido.", "Baixa de Itens", MessageBoxButtons.OK);
                    return;
                }
            }
        }
       
        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
               
        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                
                if (_parametros.ParametrosCadastros.LiberarCampoQtde)
                {
                    RemoveItemQtdeBaixa();
                }
            }

            if (e.KeyCode == Keys.D7)
            {
                _EhLeitorDeCodigoDeBarras = true;
                return;
            }
            _editandoQuantidadeDoItem = true;
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtQuantidade.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtQuantidade_EditValueChanged(object sender, EventArgs e)
        {
            if (_EhLeitorDeCodigoDeBarras) return;

            if (_editandoQuantidadeDoItem)
            {   
                //AtualizeProduto();
                _editandoQuantidadeDoItem = false;
            }
        }

        private void txtQuantidade_LostFocus(object sender, EventArgs e)
        {   
            _editandoQuantidadeDoItem = false;
            _EhLeitorDeCodigoDeBarras = false;
        }

        private void btnFinalizarBaixa_Click(object sender, EventArgs e)
        {
            FinalizaBaixa();
        }

        private void btnCancelaBaixa_Click(object sender, EventArgs e)
        {
            var sairPdv = MessageBoxAkil.Show("Deseja cancelar esta listagem de baixa estoque?", "Cancela Baixa", MessageBoxButtons.OKCancel);

            if (sairPdv == DialogResult.Cancel)
            {
                return;
            }

            this.Close();
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            ExcluaItem();
        }

        private void gridProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    if (MessageBox.Show("Tem certeza que deseja excluir este item!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {
            //        ExcluaItem();
            //    }
            //}
        }

        private void MenuGrid_Click(object sender, EventArgs e)
        {
            //ExcluaItem();
        }
        
        #endregion

        #region " MÉTODOS AUXILIARES "

        private void InicieFormSaidaEstoque()
        {
            InitializeComponent();

            _listaItens = new List<ItemPedidoDeVenda>();
            _listaItensBaixa = new List<ItemPedidoDeVenda>();
            _listaParaBaixar = new List<ItemPedidoDeVenda>();

            PreenchaDadosEmpresa();

            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            _parametros.ParametrosVenda.TabelaPreco.CarregueLazyLoad();
            _tabelaPreco = _parametros.ParametrosVenda.TabelaPreco;

            txtEstoqueReservado.Properties.ReadOnly = true;
            txtEstoqueReservado.TabStop = false;
            txtSaldoReservado.Properties.ReadOnly = true;
            txtSaldoReservado.TabStop = false;

            this.ActiveControl = txtPedidoVenda;
        }

        private void LimpeTela()
        {
            _listaItens.Clear();
            PreenchaGrid();
            lblDescricaoProduto.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            lblTotalQuantidade.Text = string.Empty;
            lblTotalPeso.Text = string.Empty;
            txtEstoqueReservado.Text = string.Empty;
            txtSaldoReservado.Text = string.Empty;
            txtCodigoBarras.Focus();
            _mudaSaldo = true;
        }

        private Produto RetorneProduto()
        {
            bool pesquiseProdutoPeloId = _pesquiseProdutoPeloId;

            ServicoProduto servicoProduto = new ServicoProduto();

            int indiceSeparador = -1;

            var match = Regex.Match(txtCodigoBarras.Text, @"[*|-]");

            if (match.Success)
            {
                indiceSeparador = match.Index;
            }

            var codigoOuCodigoBarras = txtCodigoBarras.Text.Substring(indiceSeparador + 1);

            if (codigoOuCodigoBarras.Length == 13 && codigoOuCodigoBarras[0].ToInt() == _parametros.ParametrosCadastros.PrefixoEan13CodigoBarras)
            {
                codigoOuCodigoBarras = codigoOuCodigoBarras.CodigoProdutoCodigoBarrasBalanca(_parametros.ParametrosCadastros.PrefixoEan13CodigoBarras,
                                                                                                                                                _parametros.ParametrosCadastros.TamahoCodigoBarras).ToInt().ToString();

                pesquiseProdutoPeloId = _parametros.ParametrosCadastros.VinculoProdutoCodigoBarrasBalanca == EnumVinculoProdutoCodigoBarrasBalanca.CODIGOPRODUTO;
            }

            if (pesquiseProdutoPeloId)
            {
                return servicoProduto.ConsulteProdutoAtivo(codigoOuCodigoBarras.ToInt());
            }
            else
            {
                return servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(codigoOuCodigoBarras);
            }
        }

        private Produto RetorneProdutoParaRemoverNaQtde()
        {
            bool pesquiseProdutoPeloId = _pesquiseProdutoPeloId;

            ServicoProduto servicoProduto = new ServicoProduto();

            int indiceSeparador = -1;

            var match = Regex.Match(_codigoBarrasCorrente, @"[*|-]");

            if (match.Success)
            {
                indiceSeparador = match.Index;
            }

            var codigoOuCodigoBarras = _codigoBarrasCorrente.Substring(indiceSeparador + 1);

            if (codigoOuCodigoBarras.Length == 13 && codigoOuCodigoBarras[0].ToInt() == _parametros.ParametrosCadastros.PrefixoEan13CodigoBarras)
            {
                codigoOuCodigoBarras = codigoOuCodigoBarras.CodigoProdutoCodigoBarrasBalanca(_parametros.ParametrosCadastros.PrefixoEan13CodigoBarras,
                                                                                                                                                _parametros.ParametrosCadastros.TamahoCodigoBarras).ToInt().ToString();

                pesquiseProdutoPeloId = _parametros.ParametrosCadastros.VinculoProdutoCodigoBarrasBalanca == EnumVinculoProdutoCodigoBarrasBalanca.CODIGOPRODUTO;
            }

            if (pesquiseProdutoPeloId)
            {
                return servicoProduto.ConsulteProdutoAtivo(codigoOuCodigoBarras.ToInt());
            }
            else
            {
                return servicoProduto.ConsulteProdutoAtivoPeloCodigoDeBarras(codigoOuCodigoBarras);
            }
        }

        private void InsiraProduto()
        {
            if (string.IsNullOrEmpty(txtCodigoBarras.Text))
            {
                MessageBoxAkil.Show("Produto não informado.", "Produto não informado.");
                return;
            }

            var produto = RetorneProduto();

            if (produto == null)
            {
                MessageBoxAkil.Show("Produto não encontrado.", "Aviso");

                return;
            }

            bool produtoBalanca = txtCodigoBarras.Text.CodigoBarrasEhDeBalanca(_parametros.ParametrosCadastros.PrefixoEan13CodigoBarras);

            double valorUnitario = 0;
            double quantidade = 0;
            double valorTotal = 0;
            double descontoUnitario = 0;
            double totalDesconto = 0;

            if (produtoBalanca)
            {
                double pesoOuValor = txtCodigoBarras.Text.ValorTotalOuPesoCodigoBarrasBalanca(_parametros.ParametrosCadastros.PrefixoEan13CodigoBarras,
                                                                                                                                          _parametros.ParametrosCadastros.TamahoCodigoBarras,
                                                                                                                                          _parametros.ParametrosCadastros.TipoCodigoBarrasBalanca == EnumTipoCodigoBarrasBalanca.VALORTOTAL);

                if (_parametros.ParametrosCadastros.TipoCodigoBarrasBalanca == EnumTipoCodigoBarrasBalanca.VALORTOTAL)
                {
                    valorUnitario = CalculosPedidoDeVenda.CalculePrecoUnitarioProduto(_tabelaPreco, produto);
                    quantidade = Math.Round(pesoOuValor / (double)valorUnitario, 4);
                    valorTotal = pesoOuValor;
                }
                else
                {
                    quantidade = pesoOuValor;
                    valorUnitario = CalculosPedidoDeVenda.CalculePrecoUnitarioProduto(_tabelaPreco, produto);
                    valorTotal = Math.Round(valorUnitario * quantidade, 2);
                }
            }
            else
            {
                CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();

                valorUnitario = CalculosPedidoDeVenda.CalculePrecoUnitarioProduto(_tabelaPreco, produto);
                quantidade = RetorneQuantidadeProduto();

                if (produto.DadosGerais.PermiteVendaFracionada)
                    quantidade = buscaQuantidadePedido(produto.Id);

                var match = Regex.Match(txtCodigoBarras.Text, @"[-]");

                if (match.Success)
                {
                    bool ehPercentual = Regex.IsMatch(txtCodigoBarras.Text, @"[%]");

                    int tamanhoDesconto = ehPercentual ? match.Index - 1 : match.Index;

                    descontoUnitario = txtCodigoBarras.Text.Substring(0, tamanhoDesconto).ToDouble();
                    totalDesconto = CalculosPedidoDeVenda.CalculeTotalDesconto(valorUnitario, quantidade, descontoUnitario, ehPercentual);
                }

                valorTotal = calculosPedidoDeVenda.RetorneValorTotalItem(valorUnitario, quantidade, totalDesconto, 0, 0, 0);
                totalDesconto = totalDesconto = CalculosPedidoDeVenda.CalculeTotalDesconto(valorUnitario, quantidade, descontoUnitario, false);
            }
            
            ItemPedidoDeVenda itemPedidoDeVenda = new ItemPedidoDeVenda();
            itemPedidoDeVenda.Produto = produto;
            itemPedidoDeVenda.Quantidade = quantidade;
            itemPedidoDeVenda.ValorUnitario = valorUnitario;
            itemPedidoDeVenda.DescontoEhPercentual = false;
            itemPedidoDeVenda.DescontoUnitario = descontoUnitario;
            itemPedidoDeVenda.TotalDesconto = totalDesconto;
            itemPedidoDeVenda.ValorTotal = valorTotal;
            itemPedidoDeVenda.PedidoDeVenda = new PedidoDeVenda();
            itemPedidoDeVenda.PedidoDeVenda.Id = txtPedidoVenda.Text.ToInt();
            itemPedidoDeVenda.Observacao = " " + txtPessoaQuePegou.Text + " Serie: " + txtSerie.Text;
            
            txtQuantidade.Text = quantidade.ToString("#,##0.00");

            if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {                
                txtSaldoReservado.Text = RetorneSomaQuantidadeItemReservado(quantidade, produto).ToString("0.00");
                if (produto.FormacaoPreco.EstoqueReservado <= 0)
                {
                    produto.FormacaoPreco.EstoqueReservado = quantidade;
                }

                txtEstoqueReservado.Text = produto.FormacaoPreco.EstoqueReservado.ToString();                
            }
            else
            {
                //txtSaldoReservado.Text = RetorneSomaQuantidadeItemEmEstoque(quantidade, produto).ToString("0.00");
                
                //txtSaldoReservado.Text = produto.FormacaoPreco.Estoque.ToString();

                txtEstoqueReservado.Text = produto.FormacaoPreco.Estoque.ToString();
                
            }

            MudeDeCorTextBoxesPorQuantidade();

            lblDescricaoProduto.Text = produto.DadosGerais.Descricao;

            if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {
                if (VerifiqueItemQuantidadeEstoqueNegativo(quantidade, produto))
                    return;
            }
            else
                if (VerificaitensEstoqueNegativoSaidaSemReserva(quantidade, produto))
                    return;

            if (!_parametros.ParametrosCadastros.LiberarCampoQtde)
            {
                RemoveItemParaBaixa(itemPedidoDeVenda);
                txtCodigoBarras.Text = string.Empty;
                txtPessoaQuePegou.Text = string.Empty;
                txtSerie.Text = string.Empty;
            }
            
            //AltereModoPesquisaParaCodigoBarras();           
            _editandoQuantidadeDoItem = false;

            CalculeTotais();
            PreenchaGrid();
        }

        private double buscaQuantidadePedido(int produtoId)
        {
            var pedido = new ServicoPedidoDeVenda().Consulte(_numeroPedidoAnterior);

            var quant = pedido.ListaItens.First(x => x.Produto.Id == produtoId).Quantidade;

            return quant;
        }

        private void RemoveItemQtdeBaixa()
        {
            if (string.IsNullOrEmpty(_codigoBarrasCorrente))
            {
                MessageBoxAkil.Show("Produto não informado.", "Produto não informado.");
                return;
            }

            var produto = RetorneProdutoParaRemoverNaQtde();

            if (produto == null)
            {
                MessageBoxAkil.Show("Produto não encontrado.", "Aviso");

                return;
            }
            
            double quantidade = 0;
            
            ItemPedidoDeVenda itemPedidoDeVenda = new ItemPedidoDeVenda();
            itemPedidoDeVenda.Produto = produto;
                        
            itemPedidoDeVenda.Quantidade = txtQuantidade.Text.ToDouble();
           
            itemPedidoDeVenda.PedidoDeVenda = new PedidoDeVenda();
            itemPedidoDeVenda.PedidoDeVenda.Id = txtPedidoVenda.Text.ToInt();
            itemPedidoDeVenda.Observacao = " " + txtPessoaQuePegou.Text + " Série: " + txtSerie.Text;

            if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {
                txtSaldoReservado.Text = RetorneSomaQuantidadeItemReservado(quantidade, produto).ToString("0.00");

                txtEstoqueReservado.Text = produto.FormacaoPreco.EstoqueReservado.ToString();
            }
            else
            {
                //txtSaldoReservado.Text = RetorneSomaQuantidadeItemEmEstoque(quantidade, produto).ToString("0.00");

                //txtSaldoReservado.Text = produto.FormacaoPreco.Estoque.ToString();

                txtEstoqueReservado.Text = produto.FormacaoPreco.Estoque.ToString();

            }

            MudeDeCorTextBoxesPorQuantidade();

            lblDescricaoProduto.Text = produto.DadosGerais.Descricao;

            if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {
                if (VerifiqueItemQuantidadeEstoqueNegativo(quantidade, produto))
                    return;
            }
            else
                if (VerificaitensEstoqueNegativoSaidaSemReserva(quantidade, produto))
                return;

            RemoveItemParaBaixaPelaQtde(itemPedidoDeVenda);
            txtCodigoBarras.Text = string.Empty;
            txtPessoaQuePegou.Text = string.Empty;
            txtSerie.Text = string.Empty;

            //AltereModoPesquisaParaCodigoBarras();           
            _editandoQuantidadeDoItem = false;

            CalculeTotais();
            PreenchaGrid();
        }

        private void RemoveItemParaBaixaPelaQtde(ItemPedidoDeVenda itemPedidoDeVenda)
        {
            if (_listaItens.Exists(x => x.Produto.Id == itemPedidoDeVenda.Produto.Id))
            {
               if(_listaItensBaixa != null && _listaItensBaixa.Count > 0)
                {
                    if(_listaItensBaixa.Exists(x=>x.Produto.Id == itemPedidoDeVenda.Produto.Id))
                    {
                        MessageBoxAkil.Show("Este item já existe nesta baixa. " +
                                            "Reinicie a baixa e informe a quantidade total para baixa do mesmo. " +
                                            "Caso queira a baixa total.", "Baixa de Itens",
                                            MessageBoxButtons.OK);
                        return;
                    }
                }

                _listaItensBaixa.Add(itemPedidoDeVenda);

                int i = 0;
                foreach (var item in _listaItens)
                {
                    if (item.Produto.Id == itemPedidoDeVenda.Produto.Id)
                    {
                        if(item.Quantidade < txtQuantidade.Text.ToDouble())
                        {
                            MessageBoxAkil.Show("Você digitou a Quantidade de Baixa a maior. Informe novamente.", "Baixa de Itens", MessageBoxButtons.OK);
                            return;
                        }
                        
                        double saldo = _mudaSaldo ? txtEstoqueReservado.Text.ToDouble() : txtSaldoReservado.Text.ToDouble();
                        
                        if (!item.Produto.DadosGerais.PermiteVendaFracionada)
                        {
                            saldo--;
                            item.Quantidade -= txtQuantidade.Text.ToInt();
                        }
                        else
                        {
                            item.Quantidade -= txtQuantidade.Text.ToDouble();
                            saldo = saldo - item.Quantidade;                           
                        }
                        
                        txtSaldoReservado.Text = saldo.ToString();
                        intLinhaGrid = i;
                        _mudaSaldo = false;
                    }

                    if (item.Quantidade == 0 || item.Quantidade < 0)
                    {
                        _mudaSaldo = true;
                        i--;
                        intLinhaGrid = i;
                        _listaItens.Remove(item);
                        return;
                    }
                    i++;
                }
            }
            else
            {
                MessageBoxAkil.Show("Este item não pertence ao pedido informado -Ou- Não há mais item(s) para ser(em) informado(s).", "Baixa de Itens", MessageBoxButtons.OK);
                return;
            }
        }

        private void RemoveItemParaBaixa(ItemPedidoDeVenda itemPedidoDeVenda)
        {
            if (_listaItens.Exists(x => x.Produto.Id == itemPedidoDeVenda.Produto.Id))
            {  
                _listaItensBaixa.Add(itemPedidoDeVenda);
                int i=0;

                foreach (var item in _listaItens)
                {
                    if(item.Produto.Id == itemPedidoDeVenda.Produto.Id)
                    {
                        double saldo = _mudaSaldo ? txtEstoqueReservado.Text.ToDouble() : txtSaldoReservado.Text.ToDouble();

                        if (!item.Produto.DadosGerais.PermiteVendaFracionada)
                        {
                            saldo--;
                            item.Quantidade -= 1;
                        }                            
                        else
                        {
                            saldo = saldo - item.Quantidade;
                            item.Quantidade = 0;
                        }                            

                        txtSaldoReservado.Text = saldo.ToString();
                        intLinhaGrid = i;
                        _mudaSaldo = false;
                    }

                    if (item.Quantidade == 0)
                    {
                        _mudaSaldo = true;
                        i--;
                        intLinhaGrid = i;
                        _listaItens.Remove(item);
                        return;
                    }
                    i++;
                }
            }
            else
            {
                MessageBoxAkil.Show("Este item não pertence ao pedido informado -Ou- Não há mais item(s) para ser(em) informado(s).", "Baixa de Itens", MessageBoxButtons.OK);
                return;
            }            
        }

        private bool VerificaitensEstoqueNegativoSaidaSemReserva(double quantidadeBaixa, Produto produtoBaixa)
        {
            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            ServicoProduto servicoProduto = new ServicoProduto();

            var produto = servicoProduto.Consulte(produtoBaixa.Id);

            double quantidade = _listaItens.Sum(x => x.Id == produtoBaixa.Id ? x.Quantidade : 0) + quantidadeBaixa;

            if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(quantidade, produto, false))
            {
                MessageBox.Show("O estoque do seguinte item: " + produto.Id + " - " + produto.DadosGerais.Descricao + ". O estoque não pode ficar negativo!", "Verifique o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        private bool VerifiqueItemQuantidadeEstoqueNegativo(double quantidade, Produto produto)
        {   
            if (RetorneSomaQuantidadeItemReservado(quantidade, produto) < 0 && _parametros.ParametrosVenda.NaoAceitarEstoqueNegativo)
            {
                MessageBox.Show("Pode não haver vendas realizadas para o seguinte item: " + produto.Id + " - " + produto.DadosGerais.Descricao + ". O estoque reservado não pode ficar negativo!", "Verifique suas vendas e/ou estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        private TipoMensagem VerifiqueTodosItensQuantidadeEstoqueNegativa()
        {
            TipoMensagem tipoMensagem = new TipoMensagem();

            tipoMensagem.PrimeiroConteudo = string.Empty;
            tipoMensagem.PrimeiraResposta = false;
            
            if (_parametros.ParametrosVenda.NaoAceitarEstoqueNegativo)
                for (int i = 0; i < _listaItens.Count; i++)
                {                
                    if (RetorneSomaQuantidadeItemReservado(_listaItens[i].Quantidade, _listaItens[i].Produto) < 0)
                    {
                        tipoMensagem.PrimeiroConteudo += _listaItens[i].Produto.Id + " - " + _listaItens[i].Produto.DadosGerais.Descricao + "; ";                        
                        tipoMensagem.PrimeiraResposta = true;
                    }
                }               
            
            return tipoMensagem;
        }

        private void MudeDeCorTextBoxesPorQuantidade()
        {
            if (txtEstoqueReservado.Text.ToDouble() < 0)
            {
                txtEstoqueReservado.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtEstoqueReservado.ForeColor = System.Drawing.Color.Blue;
            }

            if (txtSaldoReservado.Text.ToDouble() < 0)
            {
                txtSaldoReservado.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtSaldoReservado.ForeColor = System.Drawing.Color.Blue;
            }
        }
            

        private double RetorneSomaQuantidadeItemReservado(double quantidade, Produto produto)
        {   
            var totalQuantidadeItem = _listaItens.Sum(x => x.Produto.Id == produto.Id ? x.Quantidade : 0);
            if (totalQuantidadeItem != 0)
            {
                //if (produto.FormacaoPreco.EstoqueReservado <= 0)
                //{
                    produto.FormacaoPreco.EstoqueReservado = totalQuantidadeItem;
                //}
                return produto.FormacaoPreco.EstoqueReservado - (totalQuantidadeItem);
            }
              
           
            else
            {
                if (produto.FormacaoPreco.EstoqueReservado <= 0)
                {
                    produto.FormacaoPreco.EstoqueReservado = totalQuantidadeItem;
                }
                return produto.FormacaoPreco.EstoqueReservado - (quantidade);
            }   
           
            
           
        }

        private double RetorneSomaQuantidadeItemEmEstoque(double quantidade, Produto produto)
        {
            var totalQuantidadeItem = _listaItens.Sum(x => x.Produto.Id == produto.Id ? x.Quantidade : 0);
            if (totalQuantidadeItem != 0)
                return produto.FormacaoPreco.Estoque - (totalQuantidadeItem);
            else
                return produto.FormacaoPreco.Estoque - (quantidade);
        }

        //Ao digitar a quantidade atualizar o item da venda
        private void AtualizeProduto()
        {
            //Ao digitar o primeiro produto ainda não terá nada no grid, então deve-se sair
            if (gridProdutos.Rows.Count == 0)
                return;

            double quantidade = 0;


            //Quantidade
            if (txtQuantidade.Text == "" || txtQuantidade.Text == ",")
            {
                quantidade = 1;

            } else if (txtQuantidade.Text != "" || double.Parse(txtQuantidade.Text) > 0 || double.TryParse(txtQuantidade.Text, out quantidade))
            {
                quantidade = Double.Parse(txtQuantidade.Text);
            }
            
            //Vamos alterar os valores do grid conforme o usuário digitou na quantidade, se for >0
            if (quantidade > 0)
            {
                //gridProdutos.Rows[intLinhaGrid].Cells["colunaQuantidade"].Value = quantidade;
                //gridProdutos.Rows[intLinhaGrid].Cells["colunaPesoTotal"].Value = quantidade * _listaItens[intLinhaGrid].Produto.Principal.PesoBruto;
                //_listaItens[intLinhaGrid].Quantidade = quantidade;
            }

            if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
            {
                txtSaldoReservado.Text = RetorneSomaQuantidadeItemReservado(quantidade, _listaItens[intLinhaGrid].Produto).ToString("0.00");

                txtEstoqueReservado.Text = _listaItens[intLinhaGrid].Produto.FormacaoPreco.EstoqueReservado.ToString();
            }
            else
            {
                txtSaldoReservado.Text = RetorneSomaQuantidadeItemEmEstoque(quantidade, _listaItens[intLinhaGrid].Produto).ToString("0.00");

                txtEstoqueReservado.Text = _listaItens[intLinhaGrid].Produto.FormacaoPreco.Estoque.ToString();
            }
            
            MudeDeCorTextBoxesPorQuantidade();

            if (VerifiqueItemQuantidadeEstoqueNegativo(quantidade, _listaItens[intLinhaGrid].Produto))
                return;

            CalculeTotais();
        }

        private double RetorneQuantidadeProduto()
        {
            var indiceSeparador = txtCodigoBarras.Text.IndexOf("*");

            if (indiceSeparador == -1)
            {
                return 1;
            }
            else
            {
                return txtCodigoBarras.Text.Substring(0, indiceSeparador).ToDouble();
            }
        }

        private void PreenchaGrid()
        {
            List<ItemPedidoGrid> listaItensPedido = new List<ItemPedidoGrid>();

            for (int i = 0; i < _listaItens.Count; i++)
            {
                var item = _listaItens[i];

                ItemPedidoGrid itemPedidoGrid = new ItemPedidoGrid();

                itemPedidoGrid.Descricao = item.Produto.DadosGerais.Descricao;
                itemPedidoGrid.Item = (i + 1).ToString();
                itemPedidoGrid.ProdutoId = item.Produto.Id.ToString();
                itemPedidoGrid.Quantidade = item.Quantidade.ToString();
                itemPedidoGrid.Peso = item.Produto.Principal.PesoBruto.ToString();
                itemPedidoGrid.PesoTotal = (item.Produto.Principal.PesoBruto * item.Quantidade).ToString();

                listaItensPedido.Add(itemPedidoGrid);
                //intLinhaGrid = i;
            }

            gridProdutos.DataSource = listaItensPedido;
            if (_listaItens.Count > 0)
            {
                gridProdutos.FirstDisplayedScrollingRowIndex = _listaItens.Count - 1;
                gridProdutos[0, 0].Selected = false;
                if (intLinhaGrid > 0 && gridProdutos.RowCount >=2)
                    gridProdutos[0, intLinhaGrid].Selected = true;
                else
                    gridProdutos[0, 0].Selected = true;
            }
        }

        private void CalculeTotais()
        {
            double total = _listaItens.Sum(item => item.Quantidade);
            lblTotalQuantidade.Text = total.ToString("0.00");

            total = _listaItens.Sum(item => item.Produto.Principal.PesoBruto * item.Quantidade).ToDouble();
            lblTotalPeso.Text = total.ToString("0.00");
        }

        private void CanceleVenda()
        {
            _listaItens.Clear();
            PreenchaGrid();
        }

        private void PreenchaDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            lblEmpresa.Text = empresa.DadosEmpresa.RazaoSocial;

            picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(empresa.DadosEmpresa.Foto).Image.RedimensionarImagem(330, 150, true);

            ServicoInformacaoSistema servicoInformacaoSistema = new ServicoInformacaoSistema();
            var infoSistema = servicoInformacaoSistema.ConsulteUltimaInformacaoSistema();
        }

        private void FinalizaBaixa()
        {

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            var pedidoDeVenda = servicoPedidoDeVenda.Consulte(txtPedidoVenda.Text.ToInt());

            if (_parametros.ParametrosVenda.BaixarFaturamento == true)
            {
                if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.ABERTO)
                {
                    MessageBox.Show("Pedido não está fechado, Não pode ser baixado.",
                             "Baixa Pedido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            

            var sairPdv = MessageBoxAkil.Show("Deseja concluir a baixa no estoque?", "finalizar Baixa", MessageBoxButtons.OKCancel);

            if (sairPdv == DialogResult.Cancel)
            {
                return;
            }

            if (_listaItens.Count != 0)
            {                
                var FinalizarPdv = MessageBoxAkil.Show("Ainda existem item(s) para serem informados. Deseja continuar?", "finalizar Baixa", MessageBoxButtons.OKCancel);

                if (FinalizarPdv == DialogResult.Cancel)
                {
                    return;
                }
            }

            foreach (var item in _listaItensOriginal)
            {
                var quantidade = _listaItensBaixa.Where(x => x.Produto.Id == item.Produto.Id).Sum(x => x.Quantidade);

                string obs="";
                if (_listaItensBaixa.Exists(x=>x.Produto.Id == item.Produto.Id))
                {
                     obs= _listaItensBaixa.Where(x => x.Produto.Id == item.Produto.Id).First().Observacao;
                }

                if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                {
                    if (item.Produto.FormacaoPreco.Estoque == 0)
                    {
                        MessageBox.Show("Item com estoque zerado: " + ". O estoque não pode ficar negativo!", "Verifique as vendas ou/e o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (quantidade != 0)
                {   
                    item.Quantidade = quantidade;
                    item.Observacao = obs;
                    _listaParaBaixar.Add(item);
                }
            }

            if (_listaParaBaixar.Count != 0)
            {
                if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                {
                    var tipoMensagem = VerifiqueTodosItensQuantidadeEstoqueNegativa();

                    if (tipoMensagem.PrimeiraResposta)
                    {
                        MessageBox.Show("Pode não haver vendas realizadas para o(s) seguinte(s) item(s): " + tipoMensagem.PrimeiroConteudo + ". O estoque reservado não pode ficar negativo!", "Verifique as vendas ou/e o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                Action actionSalvar = () =>
                {
                    ServicoRecebimento servicoRecebimento = new ServicoRecebimento();
                    servicoRecebimento.MovimenteEstoquePorItem(_listaParaBaixar);
                    _listaParaBaixar = new List<ItemPedidoDeVenda>();
                    _listaItensOriginal = new List<ItemPedidoDeVenda>();
                    _listaItens = new List<ItemPedidoDeVenda>();

                    if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)
                    {
                        foreach (var itemproduto in _listaItensBaixa)
                            
                        {

                            if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                            {
                                ServicoProduto servicoProduto = new ServicoProduto();
                                var produto = servicoProduto.Consulte(itemproduto.Produto.Id);

                                double ItemReservado = produto.FormacaoPreco.EstoqueReservado - itemproduto.Quantidade;
                                //AlteraReserva(itemproduto.Produto.Id, ItemReservado);
                            }
                            
                        }
                    }

                    _listaItensBaixa = new List<ItemPedidoDeVenda>();
                    LimpeTela();
                };




                TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Item(s) baixado(s) com sucesso.");
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

            if(quant.ToInt() < 0)
            {
                quant = "0.00";
            }

            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update produtos set PROD_ESTOQUE_RESERVADO = " + quant +

                            " where prod_id=" + produto;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }

        private void ExcluaItem()
        {
            if (gridProdutos.CurrentRow == null) return;

            _listaItens.RemoveAt(gridProdutos.CurrentRow.Index);

            CalculeTotais();
            PreenchaGrid();
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemPedidoGrid
        {
            public string Item { get; set; }

            public string ProdutoId { get; set; }

            public string Descricao { get; set; }

            public string Quantidade { get; set; }

            public string Peso { get; set; }

            public string PesoTotal { get; set; }
        }


        #endregion

       
    }
}
