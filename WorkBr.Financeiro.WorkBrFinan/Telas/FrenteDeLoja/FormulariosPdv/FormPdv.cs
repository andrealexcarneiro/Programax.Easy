using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.View.Componentes;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio;
using Programax.Easy.Servico.Financeiro.MovimentacaoCaixaServ;
using Programax.Easy.Servico.ConfiguracoesSistema.InformacaoSistemaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.View.Telas.Vendas.VendaRapida;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Vendas.VendaRapidaServ;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using System.Text.RegularExpressions;
using Programax.Easy.View.Telas.Vendas.PedidosDeVendas;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;

namespace Programax.Easy.View.Telas.FrenteDeLoja.FormulariosPdv
{
    public partial class FormPdv : FormularioBase
    {
        #region " VARIAVEIS PRIVADAS "

        private List<ItemPedidoDeVenda> _listaItens;
        private Pessoa _vendedor;
        private Pessoa _cliente;
        private PedidoDeVenda _pedidoPreVenda;
        private TabelaPreco _tabelaPreco;

        private Pessoa _transportadora;
        private EnumTipoFrete _tipoFrete;
        private DateTime? _dataPrevisaoEntrega;
        private double? _valorFrete;

        private Parametros _parametros;

        private bool _campoCodigoAlterado;

        private bool _pesquiseProdutoPeloId;
        private int intLinhaGrid = 0; //Variável que pega a linha atual do grid de produtos
        private bool _isprodutobalanca; //Variável que controla se devo calcular a quantidade quando for da balança
        private Keys _Cancelar; //Se estiver cancelando o PDV não vamos entrar no evento lost focus do desconto
        private double _totalDescontoPorItem;
        private bool _editandoDescontoPorItem=false;
        private bool _editandoValorProduto = false;
        private bool _editandoQuantidadeDoItem=false;
        private bool _EhLeitorDeCodigoDeBarras;
        private bool _variavelControleAlterandoTipoDesconto;

        #endregion

        #region " CONSTRUTOR "

        public FormPdv()
        {
            InicieFormPdv();
        }

        public FormPdv(Pessoa cliente)
        {
            InicieFormPdv();

            _cliente = cliente;

            PreenchaCliente();

            VerificaSePessoaLogadaEhVendedor();
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void tmrHoraAtual_Tick(object sender, EventArgs e)
        {
            lblDataHoraAtual.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InsiraProduto();
            }
        }

        private void FormPdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _Cancelar = e.KeyCode;
                var sairPdv = MessageBoxAkil.Show("Deseja cancelar esta venda?", "cancelar venda", MessageBoxButtons.OKCancel);
                
                if (sairPdv == DialogResult.Cancel)
                {
                    _Cancelar = Keys.F10;
                    return;
                }

                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                AltereModoPesquisa();
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (_Cancelar == Keys.Escape)
                {
                    return;
                }

                if (txtDesconto.Text.ToString() == "" || txtValorProduto.Text.ToString() == "")
                {
                    return;
                }

                //AtualizeProduto(); --> Valores sendo somado com o total ao apertar F3 (esta é a correção)
                FinalizarVenda();
            }
            else if (e.KeyCode == Keys.F4)
            {
                PesquiseProdutos();
            }
            else if (e.KeyCode == Keys.F5)
            {
                CadastreClienteRapido();
            }
            else if (e.KeyCode == Keys.F6)
            {
                CanceleItem();
            }
            else if (e.KeyCode == Keys.F7)
            {
                AltereTabelaDePreco();
            }
            else if (e.KeyCode == Keys.F8)
            {
                AltereFrete();
            }
            else if (e.KeyCode == Keys.F9)
            {
                PesquiseVendedor();
            }
        }

        private void FormPdv_Load(object sender, EventArgs e)
        {
            PreenchaCaixaUsuario();
        }

        private void txtCodigoBarras_EditValueChanged(object sender, EventArgs e)
        {
            _campoCodigoAlterado = true;
        }

        //*** Início Valor Produto
        private void txtValorProduto_GotFocus(object sender, EventArgs e)
        {
            txtValorProduto.SelectAll();
        }

            private void txtValorProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtValorProduto.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }
        }

        private void txtValorProduto_EditValueChanged(object sender, EventArgs e)
        {
            if (_EhLeitorDeCodigoDeBarras) return;

            if (_editandoValorProduto)
            {
                AtualizeProduto();
                _editandoValorProduto = false;
            }
        }

        private void txtValorProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                _EhLeitorDeCodigoDeBarras = true;
                return;
            }
            _editandoValorProduto = true;
        }

        private void txtValorProduto_LostFocus(object sender, EventArgs e)
        {
            _EhLeitorDeCodigoDeBarras = false;
            _editandoValorProduto = false;
        }

        //***Fim Valor do Produto **************

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (e.KeyChar == ',')
                {
                    e.Handled = (txtDesconto.Text.Contains(','));
                }
                else
                    e.Handled = true;
            }            
        }

        private void txtDesconto_EditValueChanged(object sender, EventArgs e)
        {
            if (_EhLeitorDeCodigoDeBarras) return;

            if (_editandoDescontoPorItem)
            {
                AtualizeProduto();
                _editandoDescontoPorItem = false;
            }
        }

        private void txtDesconto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                _EhLeitorDeCodigoDeBarras = true;
                return;
            }
            _editandoDescontoPorItem = true;
        }

        private void txtDesconto_LostFocus(object sender, EventArgs e)
        {
            _EhLeitorDeCodigoDeBarras = false;
            _editandoDescontoPorItem = false;
        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                _EhLeitorDeCodigoDeBarras = true;
                return;
            }
            _editandoQuantidadeDoItem = true;
        }

        private void txtQuantidade_EditValueChanged(object sender, EventArgs e)
        {
            if (_EhLeitorDeCodigoDeBarras) return;

            if (_isprodutobalanca) return;

            if (_editandoQuantidadeDoItem)
            {
                AtualizeProduto();
                _editandoQuantidadeDoItem = false;
            }
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
        private void txtQuantidade_LostFocus(object sender, EventArgs e)
        {
            //Se o produto vem direto da balança não deixo digitar a quantidade
            if (_isprodutobalanca)
            {
                return;
            }
            _EhLeitorDeCodigoDeBarras = false;
            _editandoQuantidadeDoItem = false;
        }

        private void txtCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCodigoBarras.Text.Length >= 3 && _campoCodigoAlterado && e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
            {
                _campoCodigoAlterado = false;
                if (Regex.IsMatch(txtCodigoBarras.Text, @"^[ a-zA-Z á]*$"))
                {
                    FormPesquisaProdutoPdv formPesquisaProdutoPdv = new FormPesquisaProdutoPdv(_tabelaPreco, txtCodigoBarras.Text);
                    var produto = formPesquisaProdutoPdv.PesquiseProduto();

                    if (produto != null)
                    {
                        txtCodigoBarras.Text = produto.Id.ToString();
                        AltereModoPesquisaParaCodigoProduto();
                        InsiraProduto();
                        CarregaFoto(produto);
                    }
                }
            }

            _campoCodigoAlterado = false;
        }

        private void rdbDescontoTotalValor_CheckedChanged(object sender, EventArgs e)
        {
            if (_variavelControleAlterandoTipoDesconto)
            {
                return;
            }

            double desconto = txtDesconto.Text.ToDouble();

            if (rdbDescontoTotalValor.Checked)
            {
                txtDesconto.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,2})?";
                ConvertaDescontoParaDinheiro(desconto);
            }
            else
            {
                txtDesconto.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
                ConvertaDescontoParaPercentual(desconto);
            }
        }

        private void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            FinalizarVenda();
        }

        private void btnConsultarProduto_Click(object sender, EventArgs e)
        {
            PesquiseProdutos();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            CanceleItem();
        }

        private void MenuGrid_Click(object sender, EventArgs e)
        {
            if (gridProdutos.CurrentRow == null) return;

            _listaItens.RemoveAt(gridProdutos.CurrentRow.Index);

            CalculeTotais();
            PreenchaGrid();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void InicieFormPdv()
        {
            InitializeComponent();

            _listaItens = new List<ItemPedidoDeVenda>();

            PreenchaDadosEmpresa();

            ServicoParametros servicoParametros = new ServicoParametros();
            _parametros = servicoParametros.ConsulteParametros();

            _parametros.ParametrosVenda.TabelaPreco.CarregueLazyLoad();
            _tabelaPreco = _parametros.ParametrosVenda.TabelaPreco;

            if (_parametros.ParametrosVenda.PermiteAlterarValorUnitario)
            {
                txtValorProduto.Enabled = true;
            }

            this.ActiveControl = txtCodigoBarras;
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

        private void PreenchaCaixaUsuario()
        {
            ServicoCaixa servicoCaixa = new ServicoCaixa();
            var caixa = servicoCaixa.ConsultePeloFuncionario(Sessao.PessoaLogada);

            if (caixa == null)
            {
                MessageBoxAkil.Show("Usuário logado não contém caixa");

                return;
            }

            ServicoMovimentacaoCaixa servicoMovimentacaoCaixa = new ServicoMovimentacaoCaixa(false, false);
            var movimentacaoCaixa = servicoMovimentacaoCaixa.ConsulteCaixaAberto(caixa);

            lblNumeroCaixa.Text = caixa.Id.ToString("000");
            lblOperadorCaixa.Text = caixa.Funcionario.DadosGerais.Razao;
            lblStausCaixa.Text = movimentacaoCaixa != null ? movimentacaoCaixa.Status.Descricao() : "FECHADO";

            if (movimentacaoCaixa == null || movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO)
            {
                FormCaixaFechadoPdv formCaixaFechadoPdv = new FormCaixaFechadoPdv();
                movimentacaoCaixa = formCaixaFechadoPdv.AbraCaixa();

                if (movimentacaoCaixa == null || movimentacaoCaixa.Status == EnumStatusMovimentacaoCaixa.FECHADO)
                {
                    this.Close();
                }
                else
                {
                    lblStausCaixa.Text = movimentacaoCaixa.Status.Descricao();
                }
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
            
            try
            {
                var pedidoDeVenda = RetornePedidoDeVendaEmEdicao();
                itemPedidoDeVenda.PedidoDeVenda = pedidoDeVenda;

                if (produto.FormacaoPreco.EhPromocao==false)
                {
                    ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                    servicoPedidoDeVenda.ValideItemPedidoVendaLiberacao(itemPedidoDeVenda);
                }
            }
            catch
            {   
                MessageBoxAkil.Show("Desconto acima do Permitido.", "Desconto Acima do Permitido");
                return;
            }

            txtQuantidade.Text = quantidade.ToString("#,##0.00");
            lblDescricaoProduto.Text = produto.DadosGerais.Descricao;
            txtDesconto.Text = descontoUnitario.ToString("#,##0.00");
            txtValorProduto.Text = valorUnitario.ToString("#,##0.00");

            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(quantidade, produto, false))
            {
                MessageBox.Show("O estoque do seguinte item: " + produto.Id + " - " + produto.DadosGerais.Descricao + ". Pode estar zerado ou a quantidade requerida não está disponível!", "Verifique o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _listaItens.Add(itemPedidoDeVenda);
            
            PreenchaGrid();

            CalculeTotais();

            txtCodigoBarras.Text = string.Empty;

            AltereModoPesquisaParaCodigoBarras();
            _editandoDescontoPorItem = false;
            _editandoValorProduto = false;
            _editandoQuantidadeDoItem = false;
        }

        //Valida se deve emitir a mensagem "Desconto acima do Permitido."
        //Vai aceitar o valor caso o valor estiver com a mesma quantidade de caracteres
        private bool QuantidadedeCaracterEmiteMensagem(double ValorDigitado, double ValorOriginal)
        {
            if (txtValorProduto.Text.ToString().Length == ValorOriginal.ToString("0.00").Length-1 || 
                txtValorProduto.Text.ToString().Length == ValorOriginal.ToString("0.00").Length)
                    return true;

            return false;
        }

        //Ao digitar a quantidade ou desconto atualizar o item da venda
        private void AtualizeProduto()
        {            
            //Ao digitar o primeiro produto ainda não terá nada no grid, então deve-se sair
            if (gridProdutos.Rows.Count == 0)
                return;

            double valorUnitario=0;
            double quantidade=0;
            double descontoUnitario=0;

            //Valor do Produto ou valor unitário
            if (txtValorProduto.Text != "")
            {
                if(txtValorProduto.Text !=",")
                    valorUnitario = Double.Parse(txtValorProduto.Text);
            }
            
            //Quantidade
            if (txtQuantidade.Text == ""|| txtQuantidade.Text == ",")
            {
                quantidade = 1;

            } else if (txtQuantidade.Text != "" || double.Parse(txtQuantidade.Text) > 0 || double.TryParse(txtQuantidade.Text, out quantidade))
            {
                quantidade = Double.Parse(txtQuantidade.Text);
            }

            //Valor total inicializa com zero
            double valorTotal = 0;

            //Desconto unitário
            if (txtDesconto.Text == "" || txtDesconto.Text == ",")
            {
                descontoUnitario = 0;

            } else if (txtDesconto.Text != "" || txtDesconto.Text != "," || txtDesconto.Text != null || !String.IsNullOrEmpty(txtDesconto.Text)) 
            {
                descontoUnitario = Double.Parse(txtDesconto.Text);
            }                
            double totalDesconto = 0;
            
            bool ehPercentual = rdbDescontoTotalPercentual.Checked;

            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();

            totalDesconto = CalculosPedidoDeVenda.CalculeTotalDesconto(valorUnitario, quantidade, descontoUnitario, ehPercentual);
            
            valorTotal = calculosPedidoDeVenda.RetorneValorTotalItem(valorUnitario, quantidade, totalDesconto, 0, 0, 0);

            
            //******Vamos validar o desconto!!!
            ItemPedidoDeVenda itemPedidoDeVenda = new ItemPedidoDeVenda();
            itemPedidoDeVenda.Produto = _listaItens[intLinhaGrid].Produto;
            itemPedidoDeVenda.Quantidade = quantidade;
            itemPedidoDeVenda.ValorUnitario = valorUnitario;
            itemPedidoDeVenda.DescontoEhPercentual = ehPercentual;
            itemPedidoDeVenda.DescontoUnitario = descontoUnitario;
            itemPedidoDeVenda.TotalDesconto = totalDesconto;
            itemPedidoDeVenda.ValorTotal = valorTotal;

            try
            {
                var pedidoDeVenda = RetornePedidoDeVendaEmEdicao();
                itemPedidoDeVenda.PedidoDeVenda = pedidoDeVenda;

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
                servicoPedidoDeVenda.ValideItemPedidoVendaLiberacao(itemPedidoDeVenda);
            }
            catch
            {
                if (QuantidadedeCaracterEmiteMensagem(valorUnitario, itemPedidoDeVenda.Produto.FormacaoPreco.ValorVenda.ToDouble()))
                {
                    MessageBoxAkil.Show("Desconto acima do Permitido.", "Desconto Acima do Permitido");
                    return;
                }

                return;
            }

            if(_editandoValorProduto)
            {
                foreach (var item in _listaItens)
                {
                    if (item.Produto.Id == itemPedidoDeVenda.Produto.Id)
                        item.ValorUnitario = valorUnitario;
                }
            }

            ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();

            if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(quantidade, _listaItens[intLinhaGrid].Produto, false))
            {
                MessageBox.Show("O estoque do seguinte item: " + _listaItens[intLinhaGrid].Produto.Id + " - " + _listaItens[intLinhaGrid].Produto.DadosGerais.Descricao + ". não pode ficar menor que zero!", "Verifique o estoque!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Vamos alterar os valores do grid conforme o usuário digitou na quantidade, se for >0
            if (quantidade > 0)
                {
                    gridProdutos.Rows[intLinhaGrid].Cells[3].Value = quantidade;
                    gridProdutos.Rows[intLinhaGrid].Cells[5].Value = valorTotal.ToString("#,##0.00");
                    _listaItens[intLinhaGrid].Quantidade = quantidade;
                    _listaItens[intLinhaGrid].ValorTotal = valorTotal;                    
                }

                _listaItens[intLinhaGrid].DescontoUnitario = descontoUnitario;
                _listaItens[intLinhaGrid].TotalDesconto = totalDesconto;
                _totalDescontoPorItem = totalDesconto;                
                CalculeTotais();
            }

        private void ConvertaDescontoParaPercentual(double valor)
        {
            _variavelControleAlterandoTipoDesconto = true;

            double totalSemDesconto = 0;
                       
            totalSemDesconto += _listaItens[intLinhaGrid].ValorUnitario;// * item.Quantidade;
            
            txtDesconto.Text = ((valor* 100)/ (double)totalSemDesconto).ToString("0.00");

            CalculeTotais();

            _variavelControleAlterandoTipoDesconto = false;
        }

        private void ConvertaDescontoParaDinheiro(double percentual)
        {
            double valorunitario = 0;
            
            valorunitario += _listaItens[intLinhaGrid].ValorUnitario;// * item.Quantidade;

            txtDesconto.Text = ((valorunitario * percentual) / 100).ToString("0.00");
            
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
                itemPedidoGrid.ValorUnitario = item.ValorUnitario.ToString("#,##0.00");
                itemPedidoGrid.ValorTotal = item.ValorTotal.ToString("#,##0.00");

                listaItensPedido.Add(itemPedidoGrid);
                intLinhaGrid = i;
            }

            gridProdutos.DataSource = listaItensPedido;
            if (_listaItens.Count > 0)
            {
                gridProdutos.FirstDisplayedScrollingRowIndex = _listaItens.Count - 1;
                gridProdutos[0, 0].Selected = false;
                gridProdutos[0, intLinhaGrid].Selected = true;
            }
        }

        private void CalculeTotais()
        {
            double total = _listaItens.Sum(item => item.ValorTotal);
            double totalDescontos = _listaItens.Sum(item => item.TotalDesconto);            
            lblTotalVenda.Text = total.ToString("#,##0.00");
            lblTotalDesconto.Text = totalDescontos.ToString("#,##0.00");
        }

        private void CanceleVenda()
        {
            _listaItens.Clear();
            PreenchaGrid();
        }

        private void PesquiseProdutos()
        {
            FormPesquisaProdutoPdv formPesquisaProdutoPdv = new FormPesquisaProdutoPdv(_tabelaPreco);
            var produto = formPesquisaProdutoPdv.PesquiseProduto();

            if (produto != null)
            {
                txtCodigoBarras.Text += produto.Id.ToString();
                AltereModoPesquisaParaCodigoProduto();
                InsiraProduto();
                CarregaFoto(produto);
            }
        }

        private void CarregaFoto(Produto produto)
        {
            //Foto no PDV
            if (produto.DadosGerais.Foto == null)
            {
                picItem.Image = Properties.Resources.produtos;
            }
            else
            {
                picItem.Image = TratamentoDeImagens.ConvertByteToImagem(produto.DadosGerais.Foto).Image;
            }
        }

        public void VerificaSePessoaLogadaEhVendedor()
        {
            var parceiro = Sessao.PessoaLogada;

            ServicoPessoa servicoPessoa = new ServicoPessoa();

            var vendedor = servicoPessoa.ConsulteVendedorAtivo(parceiro.Id);

            if(vendedor != null)
            {
                _vendedor = vendedor;

                PreenchaVendedor();
            }
        }

        private void PesquiseVendedor()
        {
            FormSelecioneVendedor formSelecioneVendedor = new FormSelecioneVendedor();

            if (formSelecioneVendedor.PesquiseVendedor(_vendedor) == System.Windows.Forms.DialogResult.OK)
            {
                _vendedor = formSelecioneVendedor.Vendedor;

                PreenchaVendedor();
            }
        }

        private void PreenchaVendedor()
        {
            if (_vendedor != null)
            {
                lblVendedor.Text = _vendedor.Id + " - " + _vendedor.DadosGerais.Razao;
            }
            else
            {
                lblVendedor.Text = string.Empty;
            }
        }

        private void PreenchaCliente()
        {
            if (_cliente != null)
            {
                if (!string.IsNullOrEmpty(_cliente.DadosGerais.CpfCnpj))
                {
                    lblCliente.Text = _cliente.DadosGerais.CpfCnpj + " - " + _cliente.DadosGerais.Razao;
                }
                else
                {
                    lblCliente.Text = _cliente.DadosGerais.Razao;
                }

                if(!_cliente.DadosGerais.Razao.ToLower().Contains("consumidor"))
                {
                    if (EstahInadimplente(_cliente.Id))
                        pbAlerta.Visible = true;
                    else
                        pbAlerta.Visible = false;
                    
                    //FormAvisoClienteInadimplente.ExibaAvisoInadimplente(_cliente.Id);
                }
                    
            }
            else
            {
                lblCliente.Text = string.Empty;

                //FormAvisoClienteInadimplente.EscondaAvisoInadimplente();
            }
        }

        public bool EstahInadimplente(int pessoaId)
        {
            ServicoContasReceber servicoContasReceber = new ServicoContasReceber();

            var possuiTituloAtrasado = servicoContasReceber.PossuiTituloAtrasado(pessoaId);

            if (!possuiTituloAtrasado)
            {
                return false;
            }
                       
            //**Vamos ver se todas as contas atrasadas são crédito
            Pessoa pessoa = new Pessoa { Id = pessoaId };

            bool NaoEstahInadimplente = false;

            var contasPagarReceber = servicoContasReceber.ConsulteListaAberto(pessoa);

            if (contasPagarReceber != null)
            {
                if (contasPagarReceber.Count(x => x.DataVencimento < DateTime.Now) > 1)
                {
                    var ContasAtrasadas = contasPagarReceber.FindAll(x => x.DataVencimento < DateTime.Now);

                    foreach (var item in ContasAtrasadas)
                    {
                        if ((EnumTipoFormaPagamento)item.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO ||
                            (EnumTipoFormaPagamento)item.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                            NaoEstahInadimplente = false;
                        else
                            return true;
                    }
                }
                else
                {
                    var ContaAtrasada = contasPagarReceber.Find(x => x.DataVencimento < DateTime.Now);

                    if ((EnumTipoFormaPagamento)ContaAtrasada.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO ||
                           (EnumTipoFormaPagamento)ContaAtrasada.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                        NaoEstahInadimplente = false;
                    else
                        return true;
                }                
            }

            return NaoEstahInadimplente;
        }

        private void CanceleItem()
        {
            FormCancelarItemPdv formCancelarItemPdv = new FormCancelarItemPdv(_listaItens);

            var resultado = formCancelarItemPdv.CanceleItem();

            if (resultado == DialogResult.OK)
            {
                _listaItens = formCancelarItemPdv.ListaItens;

                PreenchaGrid();
                CalculeTotais();
            }
        }

        private void PreenchaDadosEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            lblEmpresa.Text = empresa.DadosEmpresa.RazaoSocial;

            lblTelefone.Text = empresa.DadosEmpresa.Telefone;
            lblWhatsApp.Text = empresa.DadosEmpresa.WhatsApp;
            lblFacebook.Text = empresa.DadosEmpresa.Facebook;
            lblTwitter.Text = empresa.DadosEmpresa.Twitter;
            lblInstagram.Text = empresa.DadosEmpresa.Instagram;

            if (string.IsNullOrWhiteSpace(lblTelefone.Text))
            {
                lblTelefone.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(lblWhatsApp.Text))
            {
                lblWhatsApp.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(lblFacebook.Text))
            {
                lblFacebook.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(lblInstagram.Text))
            {
                lblInstagram.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(lblTwitter.Text))
            {
                lblTwitter.Visible = false;
            }

            picFoto.Image = TratamentoDeImagens.ConvertByteToImagem(empresa.DadosEmpresa.Foto).Image.RedimensionarImagem(330, 150, true);

            ServicoInformacaoSistema servicoInformacaoSistema = new ServicoInformacaoSistema();
            var infoSistema = servicoInformacaoSistema.ConsulteUltimaInformacaoSistema();

            lblVersaoSistema.Text = "Versão " + infoSistema.Versao;
        }

        private void CadastreClienteRapido()
        {
            Pessoa clienteParaEdicao = new Pessoa();

            clienteParaEdicao = null;

            FormCadastroClienteVendaRapida formCadastroClienteVendaRapida = new FormCadastroClienteVendaRapida();

            var cliente = formCadastroClienteVendaRapida.CadastreClienteModal(clienteParaEdicao);
            
            if (cliente != null)
            {
                _cliente = cliente;
                PreenchaCliente();
            }
        }
        
        private void PesquiseCliente()
        {
            Pessoa ClienteAnterior = _cliente;

            FormCadastroClienteVendaRapida formCadastroClienteVendaRapida = new FormCadastroClienteVendaRapida();
            _cliente = formCadastroClienteVendaRapida.CadastreClienteModalPdv(_cliente);

            if (_cliente != null)
                PreenchaCliente();
            else
                _cliente = ClienteAnterior;

        }

        private void ImportePreVenda()
        {
            FormPesquisaPedidoVendaPdv formPesquisaPedidoVendaPdv = new FormPesquisaPedidoVendaPdv();
            PedidoDeVenda pedido = formPesquisaPedidoVendaPdv.PesquisePedidoDeVenda();

            if (pedido != null)
            {
                _pedidoPreVenda = pedido;

                _listaItens = pedido.ListaItens.ToList();
                _vendedor = pedido.Vendedor;
                _cliente = pedido.Cliente;

                PreenchaCliente();
                PreenchaGrid();
                PreenchaVendedor();
                CalculeTotais();
            }
        }

        private PedidoDeVenda RetornePedidoDeVendaEmEdicao()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa(false, false);
            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();

            PedidoDeVenda pedidoDeVenda = new PedidoDeVenda();

            pedidoDeVenda.DataElaboracao = DateTime.Now.Date;
            pedidoDeVenda.DataFechamento = DateTime.Now.Date;
            pedidoDeVenda.ListaItens = _listaItens;
            pedidoDeVenda.TabelaPreco = _tabelaPreco;

            pedidoDeVenda.Desconto = _listaItens.Sum(item => item.TotalDesconto);
            pedidoDeVenda.DescontoEhPercentual = false;
            pedidoDeVenda.ValorTotal = _listaItens.Sum(item => item.ValorTotal);

            pedidoDeVenda.Usuario = Sessao.PessoaLogada;
            pedidoDeVenda.Cliente = _cliente;
            pedidoDeVenda.Vendedor = _vendedor;

            pedidoDeVenda.Transportadora = _transportadora;
            pedidoDeVenda.TipoFrete = _tipoFrete;
            pedidoDeVenda.ValorFrete = _valorFrete.GetValueOrDefault();
            pedidoDeVenda.DataPrevisaoEntrega = _dataPrevisaoEntrega;

            pedidoDeVenda.TipoCliente = EnumTipoCliente.CONSUMIDOR;
            pedidoDeVenda.TipoPedidoVenda = EnumTipoPedidoDeVenda.PEDIDOVENDA;

            pedidoDeVenda.EnderecoPedidoDeVenda = new EnderecoPedidoDeVenda();
            pedidoDeVenda.EnderecoPedidoDeVenda.Complemento = empresa.DadosEmpresa.Endereco.Complemento;
            pedidoDeVenda.EnderecoPedidoDeVenda.Numero = empresa.DadosEmpresa.Endereco.Numero;

            pedidoDeVenda.EnderecoPedidoDeVenda.CEP = empresa.DadosEmpresa.Endereco.CEP;
            pedidoDeVenda.EnderecoPedidoDeVenda.Bairro = empresa.DadosEmpresa.Endereco.Bairro;
            pedidoDeVenda.EnderecoPedidoDeVenda.Rua = empresa.DadosEmpresa.Endereco.Rua;
            pedidoDeVenda.EnderecoPedidoDeVenda.Cidade = empresa.DadosEmpresa.Endereco.Cidade;

            pedidoDeVenda.EnderecoPedidoDeVenda.TipoEndereco = EnumTipoEndereco.PRINCIPAL;

            return pedidoDeVenda;
        }

        private void AltereTabelaDePreco()
        {
            FormAlterarTabelaPrecoPdv formAlterarTabelaPrecoPdv = new FormAlterarTabelaPrecoPdv();
            var tabelaAlterada = formAlterarTabelaPrecoPdv.EditeTabelaPreco(_tabelaPreco);

            if (tabelaAlterada == DialogResult.OK)
            {
                _tabelaPreco = formAlterarTabelaPrecoPdv.TabelaPrecoSelecionada;

                foreach (var item in _listaItens)
                {
                    item.ValorUnitario = ServicoVendaRapida.CalculePrecoUnitarioProduto(_tabelaPreco, item.Produto);
                }
            }

            RecalculeValoresProdutos();
        }

        private void RecalculeValoresProdutos()
        {
            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();
            calculosPedidoDeVenda.RecalculeValoresItens(false, false, 0, _valorFrete.GetValueOrDefault(), _listaItens, "GO", EnumTipoCliente.CONSUMIDOR, EnumTipoInscricaoICMS.NAOCONTRIBUINTEICMS);

            PreenchaGrid();
            CalculeTotais();
        }

        private void AltereFrete()
        {
            FormAlterarFretePdv formAlterarFretePdv = new FormAlterarFretePdv();
            var retorno = formAlterarFretePdv.EditeTransportadora(_transportadora, _tipoFrete, _dataPrevisaoEntrega, _valorFrete);

            if (retorno == DialogResult.OK)
            {
                _transportadora = formAlterarFretePdv.Transportadora;
                _tipoFrete = formAlterarFretePdv.TipoFrete;
                _dataPrevisaoEntrega = formAlterarFretePdv.DataPrevisaoEntrega;
                _valorFrete = formAlterarFretePdv.ValorFrete;

                RecalculeValoresProdutos();
            }
        }

        private void FinalizarVenda()
        {
            var pedidoDeVenda = RetornePedidoDeVendaEmEdicao();

            FormFechamentoVendaPdv formFechamentoVendaPdv = new FormFechamentoVendaPdv(pedidoDeVenda);
            var resultado = formFechamentoVendaPdv.RetorneVendaFechadaComSucesso();

            if (resultado == DialogResult.OK)
            {
                FormAgradecimentoPdv formAgradecimentoPdv = new FormAgradecimentoPdv(pedidoDeVenda.ValorTotal, _vendedor, _cliente);
                formAgradecimentoPdv.AbrirTelaModal(true);

                this.Close();
            }
        }

        private void AltereModoPesquisaParaCodigoBarras()
        {
            _pesquiseProdutoPeloId = true;
            AltereModoPesquisa();
        }

        private void AltereModoPesquisaParaCodigoProduto()
        {
            _pesquiseProdutoPeloId = false;
            AltereModoPesquisa();
        }

        private void AltereModoPesquisa()
        {
            if (_pesquiseProdutoPeloId)
            {
                _pesquiseProdutoPeloId = false;
                lblDescricaoCodigoProduto.Text = "Código de Barras do Produto: (F4 para consulta de produtos)";
            }
            else
            {
                _pesquiseProdutoPeloId = true;
                lblDescricaoCodigoProduto.Text = "Código do Produto: (F4 para consulta de produtos)";
            }
        }

        #endregion

        #region " CLASSES AUXILIARES "

        private class ItemPedidoGrid
        {
            public string Item { get; set; }

            public string ProdutoId { get; set; }

            public string Descricao { get; set; }

            public string Quantidade { get; set; }

            public string ValorUnitario { get; set; }

            public string ValorTotal { get; set; }
        }

        #endregion
        
        }
}
