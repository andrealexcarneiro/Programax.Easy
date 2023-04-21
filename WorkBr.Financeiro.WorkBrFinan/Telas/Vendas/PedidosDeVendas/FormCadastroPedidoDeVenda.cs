using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Negocio.Cadastros.ComissaoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.EnderecoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.Enumeradores;
using Programax.Easy.Negocio.Cadastros.PessoaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.ProdutoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Cadastros.TabelaPrecosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.ConfiguracoesSistema.Enumeradores;
using Programax.Easy.Negocio.ConfiguracoesSistema.ParametrosObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CondicaoPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.FormaPagamentoObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Movimentacao.Enumeradores;
using Programax.Easy.Negocio.Vendas.Enumeradores;
using Programax.Easy.Negocio.Vendas.PedidoDeVendaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.ComissaoServ;
using Programax.Easy.Servico.Cadastros.EnderecoServ;
using Programax.Easy.Servico.Cadastros.PessoaServ;
using Programax.Easy.Servico.Cadastros.ProdutoServ;
using Programax.Easy.Servico.ConfiguracoesSistema.ParametrosServ;
using Programax.Easy.Servico.Financeiro.CondicaoPagamentoServ;
using Programax.Easy.Servico.Financeiro.ContasPagarReceberServ;
using Programax.Easy.Servico.Financeiro.FormaPagamentoServ;
using Programax.Easy.Servico.Vendas.PedidoDeVendaServ;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Easy.View.Telas.Cadastros.Comissoes;
using Programax.Easy.View.Telas.Cadastros.Enderecos;
using Programax.Easy.View.Telas.Cadastros.Pessoas;
using Programax.Easy.View.Telas.Cadastros.Produtos;
using Programax.Easy.View.Telas.Financeiro.CondicoesPagamento;
using Programax.Easy.View.Telas.Financeiro.FormasPagamento;
using Programax.Easy.View.Telas.Produtos.TabelaDePreco;
using Programax.Infraestrutura.Negocio.ObjetosDeNegocio;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Report.RelatoriosDevExpress.Vendas;
using DevExpress.XtraReports.UI;
using DevExpress.LookAndFeel;
using Programax.Easy.Servico.Cadastros.TabelaPrecoServ;
using Programax.Easy.Servico.Cadastros.EstadoServ;
using Programax.Easy.Servico.Cadastros.CidadeServ;
using Programax.Easy.Negocio.Cadastros.CidadeObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CrediarioServ;
using Programax.Easy.Negocio.Cadastros.EmpresaObj.ObjetoDeNegocio;
using Programax.Easy.Negocio.Financeiro.CrediarioObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.EmpresaServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Negocio.Financeiro.OperadorasCartaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.OperadorasCartaoServ;
using Programax.Easy.View.Telas.Vendas.Roteiros;
using Programax.Easy.Servico.Vendas.RoteiroServ;
using Programax.Easy.Negocio.Vendas.RoteiroObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Fiscal.NotaFiscalServ;
using Programax.Easy.Negocio.Fiscal.Enumeradores;
using Programax.Easy.Servico.Movimentacao.MovimentacaoServ;
using Programax.Easy.Negocio.Movimentacao.MovimentacaoObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Cadastros.CaixaServ;
using Programax.Easy.Negocio.Cadastros.CaixaObj.ObjetoDeNegocio;
using static Programax.Easy.Report.RelatoriosDevExpress.Vendas.RelatorioVendasClientes;
using static Programax.Easy.Servico.RegistroDeMapeamentos;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;
using Programax.Easy.Servico.Cadastros.TransferenciaServ;
using Programax.Easy.Servico.Cadastros.SubEstoqueServ;

namespace Programax.Easy.View.Telas.Vendas.PedidosDeVendas
{
    public partial class FormCadastroPedidoDeVenda : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<Cidade> _listaCidadesCbo;
        private List<ItemPedidoDeVenda> _listaItensPedidosVenda;
        private List<ParcelaPedidoDeVenda> _listaParcelasPedidoDeVenda;
        private readonly List<BaixaItens> _listaBaixaItens;
        private ItemPedidoDeVenda _itemPedidoDeVendaEmEdicao;
        private ParcelaPedidoDeVenda _parcelaPedidoDeVendaEmEdicao;
        private Produto _produtoEmEdicao;
        private Produto _produtoAnterior;
        private Parametros _parametros;
        private Pessoa _clienteSelecionado;
        private FormaPagamento _formaPagamentoSelecionada;
        private List<FormaPagamento> _listaFormasPagamentos;
        private List<Roteirizacao> _listaDeRoteiros;
        private List<ItemPedidoDeVenda> _listaItensPedidosVendaMov;

        private Pessoa _cliente;
        private bool _variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento;
        private bool _variavelControleEditandoOuLimpandoPedido;
        private bool _variavelControleAlterandoTipoDescontoFechamento;
        private bool _variavelControleAlterandoVolume;

        private ServicoCidade _servicoCidade;

        private Crediario _crediario;

        private Empresa _empresa;
        private List<PedidoDeVenda> _listaTmkGrid;
        private List<PedidoDeVenda> _listaTmkGridII;
        private Caixa _caixa;
        private Negocio.Fiscal.NotaFiscalObj.ObjetoDeNegocio.NotaFiscal varnotafiscal;
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
        private int itemProduto = 0;
        private Boolean Alterar = false;
        private string strEmpresa;
        private bool orcamento = false;
        private bool itemEditado = false;
        #endregion

        #region " CONSTRUTOR "

        public FormCadastroPedidoDeVenda(int numeroPedido = 0)
        {
            InitializeComponent();

            CarregueParametros();

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();

            var empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            if (empresa.DadosEmpresa.NomeFantasia.Length < 8)
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, empresa.DadosEmpresa.NomeFantasia.Length);
            }
            else
            {
                strEmpresa = empresa.DadosEmpresa.NomeFantasia.Substring(0, 8);
            }
          

            _listaItensPedidosVenda = new List<ItemPedidoDeVenda>();
            _listaParcelasPedidoDeVenda = new List<ParcelaPedidoDeVenda>();
            _listaBaixaItens = new List<BaixaItens>();

            _servicoCidade = new ServicoCidade();

            PreenchaCboTipoDocumento();
            PreenchaCboTipoEndereco();
            PreenchaCboFormaPagamento();

            PreenchaCboTabelaPreco();

            PreenchaCboIndicadores();
            PreenchaCboAtendentes();
            PreenchaCboVendedores();
            PreenchaCboSupervisores();
            PreenchaCboTransportadoras();

            PreenchaCboEstados();

            txtUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;
            txtDataElaboracao.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtSituacao.Text = EnumStatusPedidoDeVenda.ABERTO.Descricao();
                        
            TrateUsuarioContemPermissaoAtalhos();

            ApliqueParametros();

            PesquiseEmpresa();

            if (numeroPedido != 0)
            {
                txtId.Text = numeroPedido.ToString();
                BusqueECarreguePedido();
            }
            else
            {
                this.ActiveControl = txtId;
            }
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void txtSomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        private void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (_clienteSelecionado == null || txtIdCliente.Text.ToInt() != _clienteSelecionado.Id)
            {
                if (!string.IsNullOrEmpty(txtIdCliente.Text))
                {
                    ServicoPessoa servicoPessoa = new ServicoPessoa();
                    var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdCliente.Text.ToInt());

                    PreenchaCliente(cliente, true);
                }
                else
                {
                    PreenchaCliente(null);
                }
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
                    if (habilitar == 1)
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
                MySqlDataReader MyReader2;


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
                            txtcashback.Text = valorCash.ToString("#,###,##0.00");

                        }
                    }
                    else
                    {
                        txtcashback.Text = "0,00";
                    }

                }

            }


        }
        private void btnPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPessoaPesquisa formPessoaPesquisa = new FormPessoaPesquisa();

            var cliente = formPessoaPesquisa.PesquisePessoaClienteAtiva();

            if (cliente != null)
            {
                PreenchaCliente(cliente);
            }
        }

        private void btnPesquisaEndereco_Click(object sender, EventArgs e)
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();

            var endereco = formEnderecoPesquisa.PesquiseEndereco();

            if (endereco != null)
            {
                PreenchaDadosEnderecoCep(endereco);
            }
        }

        private void txtCepEndereco_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCepEndereco.Text))
            {
                txtCepEndereco.SelectionStart = 0;
            }
        }

        private void txtCepEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCepEndereco.Text.EstahVazio())
                {
                    PesquiseCepPelaTelaDePesquisa();
                }
                else
                {
                    PesquiseCep();
                }
            }
        }

        private void txtCepEndereco_Leave(object sender, EventArgs e)
        {
            PesquiseCep();
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

        private void rdbDescontoProdutoValor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDescontoProdutoValor.Checked)
            {
                txtDescontoUnitario.Properties.Mask.EditMask = @"[0-9]{1,11}([\.\,][0-9]{0,4})?";
            }
            else
            {
                txtDescontoUnitario.Properties.Mask.EditMask = @"[0-9]{1,2}([\.\,][0-9]{0,2})?";
                txtDescontoUnitario.Text = string.Empty;
            }

            txtDescontoUnitario.Text = string.Empty;

            CalculeValoresTotaisCamposProduto();

            txtDescontoUnitario.Focus();
        }

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

        private void btnInserirAtualizarItem_Click(object sender, EventArgs e)
        {
            InsiraOuAtualizeItemPedido();
        }

        private void btnCancelarItem_Click(object sender, EventArgs e)
        {
            LimpeCamposProduto();
        }

        private void gcItens_DoubleClick(object sender, EventArgs e)
        {
            EditeItem();
        }

        private void btnPesquisaProduto2_Click(object sender, EventArgs e)
        {
            FormPesquisaProduto formPesquisaProduto = new FormPesquisaProduto();
            var produto = formPesquisaProduto.ExibaPesquisaDeProduto();

            if (produto != null)
            {
                PreenchaProduto(produto);
            }
        }

        private void cboTipoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            var tipoDocumento = (EnumTipoPedidoDeVenda?)cboTipoDocumento.EditValue;

            if (tipoDocumento == null || tipoDocumento.Value == EnumTipoPedidoDeVenda.ORCAMENTO)
            {
                btnFecharVenda.Visible = false;
            }
            else
            {
                if (txtSituacao.Text == EnumStatusPedidoDeVenda.ABERTO.Descricao() ||
                    txtSituacao.Text == EnumStatusPedidoDeVenda.ORCAMENTO.Descricao())
                {
                    //btnFecharVenda.Visible = true;
                }
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

        //private void txtQuantidadeProduto_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalculeValoresTotaisCamposProduto();
        //}

        //private void txtDescontoProduto_EditValueChanged(object sender, EventArgs e)
        //{
        //    CalculeValoresTotaisCamposProduto();
        //}

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (gridControl2.RowCount == 0) return;

            if (ValideSeSalvarOuFechar()) return;

            if(ValidaTipoDocumento()) return;

            Action actionSalvar = () =>
            {
                var pedidoDeVenda = RetornePedidoDeVendaEmEdicao();
                if (pedidoDeVenda.StatusRoteiro ==  EnumStatusRoteiro.EMAGENDA )
                {

                }

                if (pedidoDeVenda.ValorTotal == 0)
                {
                    MessageBox.Show("O Pedido esta com valor Total zerada corrija seu valor!", "Não é permitido salvar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    throw new Exception("Não foi permitido salvar a venda! Corrija seu pedido!");
                }

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                if ((EnumTipoPedidoDeVenda)cboTipoDocumento.EditValue == EnumTipoPedidoDeVenda.PEDIDOVENDA)
                {

                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                    bool ReserveEstoqueAoFaturarPedido = _parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido;

                
                    foreach (var itemproduto in _listaItensPedidosVenda)
                    {
                        if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(itemproduto.Quantidade, itemproduto.Produto, ReserveEstoqueAoFaturarPedido))
                        {
                            MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + itemproduto.Produto.Id.ToString() + " - " + itemproduto.Produto.DadosGerais.Descricao.ToString() + " . Está ou ficará menor que zero!", "Não é permitido salvar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            throw new Exception("Não foi permitido salvar a venda! Corrija seu estoque!");

                        }
                    }

                }




                if (Alterar == false)
                {

                    var tipoMensagem = servicoPedidoDeVenda.VerificaEstoqueNegativo(pedidoDeVenda);

                    if ((EnumTipoPedidoDeVenda)cboTipoDocumento.EditValue != EnumTipoPedidoDeVenda.ORCAMENTO)
                    {
                        //Mensagem informando o parâmetro: "Não aceitar estoque negativo". Não deixar fechar o pedido.                
                        if (tipoMensagem.SegundaResposta)
                        {
                            MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + tipoMensagem.PrimeiroConteudo + " . Está ou ficará menor que zero!", "Não é permitido salvar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            throw new Exception("Não foi permitido salvar a venda! Corrija seu estoque!");
                        }
                    }

                    //Mensagem quando preencherem o estoque mínimo. Caso atingir este estoque, o sistema avisa.
                    if (tipoMensagem.TerceiraResposta)
                    {
                        MessageBox.Show("Verifique os seguintes itens: " + tipoMensagem.SegundoConteudo + ".", "O estoque mínimo foi atingido!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if (pedidoDeVenda.Id == 0)
                {
                    servicoPedidoDeVenda.Cadastre(pedidoDeVenda);
                }
                else
                {
                    PedidoDeVenda pedidoBaseItens = new PedidoDeVenda(); 
                    pedidoBaseItens = servicoPedidoDeVenda.Consulte(pedidoDeVenda.Id);
                    List<ItemPedidoDeVenda> listaItens = new List<ItemPedidoDeVenda>();
                    listaItens = pedidoBaseItens.ListaItens.ToList();
                    ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                    bool ReserveEstoqueAoFaturarPedido = _parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido;

                    //foreach (var itemproduto in _listaItensPedidosVenda)
                    //{
                    //    if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(itemproduto.Quantidade, itemproduto.Produto, ReserveEstoqueAoFaturarPedido))
                    //    {
                    //        MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + itemproduto.Produto.Id.ToString() + " - " + itemproduto.Produto.DadosGerais.Descricao.ToString() + " . Está ou ficará menor que zero!", "Não é permitido salvar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        throw new Exception("Não foi permitido salvar a venda! Corrija seu estoque!");
                           
                    //    }
                    //}


                    //Para ignorar a validação do crédito inicial, carrega-se com um valor bem alto
                    if (_parametros.ParametrosFinanceiro.IgnorarCreditoInicial)
                        pedidoDeVenda.SaldoDisponivel = 999999999;
                    else if ((EnumTipoFormaPagamento)pedidoDeVenda.FormaPagamento.Id == EnumTipoFormaPagamento.DINHEIRO ||
                            (EnumTipoFormaPagamento)pedidoDeVenda.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO ||
                            (EnumTipoFormaPagamento)pedidoDeVenda.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                                pedidoDeVenda.SaldoDisponivel = 999999999; //Se for dinheiro, débito ou crédito vai ignorar a validação.

                    servicoPedidoDeVenda.AtualizePedidoJaReservadoComNovosDados(pedidoDeVenda);
           

                    if (_parametros.ParametrosVenda.TrabalharComEstoqueReservado)
                    {
                        if (!_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido)
                        {
                            if (pedidoBaseItens.StatusPedidoVenda == EnumStatusPedidoDeVenda.RESERVADO ||
                                pedidoBaseItens.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO)
                            {
                                pedidoBaseItens.ListaItens = listaItens.ToList();
                                servicoPedidoDeVenda.RemovaReservaEstoque(pedidoBaseItens);
                            }
                        }
                    }
                }

                if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO)
                {
                    MessageBox.Show("Pedido enviado para liberação.");
                }
                if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)

                    if ((EnumTipoPedidoDeVenda)cboTipoDocumento.EditValue != EnumTipoPedidoDeVenda.ORCAMENTO)
                    {

                        if (itemEditado == false)
                        {
                            if (pedidoDeVenda.TipoPedidoVenda == EnumTipoPedidoDeVenda.PEDIDOVENDA)
                            {
                                foreach (var itemproduto in _listaItensPedidosVenda)
                                {
                                    AlteraReserva(itemproduto.Produto.Id, itemproduto.Produto.FormacaoPreco.EstoqueReservado + itemproduto.Quantidade);
                                }
                            }
                           
                           
                        }
                        else
                        {
                            foreach (var itemproduto in _listaItensPedidosVenda)
                            {
                                if (_produtoAnterior.Id != itemproduto.Produto.Id)
                                {
                                    AlteraReserva(_produtoAnterior.Id, _produtoAnterior.FormacaoPreco.EstoqueReservado - itemproduto.Quantidade);
                                    AlteraReserva(itemproduto.Produto.Id, itemproduto.Produto.FormacaoPreco.EstoqueReservado + itemproduto.Quantidade);

                                }
                            }
                        }

                        //if (Alterar == false)
                        //{
                           
                        //}
                }

                MessageBox.Show("Número do pedido/orçamento " + pedidoDeVenda.Id + ".");

                PergunteSeDesejaImprimirPedido(pedidoDeVenda.Id);

                LimpePedidoDeVenda();
            };
            
            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "O pedido/orçamento foi salvo com sucesso.");
        }
        private void AlteraReserva(int produto, double quantidade)
        {
            string conexoesString = System.IO.File.ReadAllText(InfraUtils.RetorneDiretorioAplicacao() + @"\conexoes.json");
            string Quant = quantidade.ToString();
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

            Quant = Quant.ToString().Replace(",", ".");

            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update produtos set PROD_ESTOQUE_RESERVADO = " + Quant +

                            " where prod_id=" + produto;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }
        private void rdbDescontoProdutoPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoUnitario.Focus();
            }
        }

        private void rdbDescontoProdutoValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoUnitario.Focus();
            }
        }

        private void rdbDescontoTotalValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoFechamento.Focus();
            }
        }

        private void rdbDescontoTotalPercentual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescontoFechamento.Focus();
            }
        }

        private void rdbFreteNenhum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDataPrevisaoEntrega.Focus();
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

        private void txtFreteFechamento_EditValueChanged(object sender, EventArgs e)
        {
            RecalculeValoresItens();
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            BusqueECarreguePedido();
            
            itemEditado = false;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimpePedidoDeVenda();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (itemEditado == true)
            {
                this.FecharFormulario();
            }
            else
            {
                this.FecharFormulario();
            }
            
        }

        private void btnExcluirItem_Click(object sender, EventArgs e)
        {
            if (_listaItensPedidosVenda.Count > 0)
            {
                var idItem = colunaId.View.GetFocusedRowCellValue(colunaId).ToInt();

                var itemASerExcluido = _listaItensPedidosVenda.FirstOrDefault(x => x.Id == idItem);

                if (ValideSeItemEstahEmBaixa(itemASerExcluido.PedidoDeVenda.StatusPedidoVenda, itemASerExcluido))
                {
                    return;
                }

                var mensagemConfirmacaoExclusao = "Deseja excluir o item " + itemASerExcluido.Produto.Id + " - " + itemASerExcluido.Produto.DadosGerais.Descricao + " ?";

                if (MessageBox.Show(mensagemConfirmacaoExclusao, "Deseja excluir este item ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)
                    {
                        if (itemASerExcluido.Produto.FormacaoPreco.EstoqueReservado > 0)
                        {
                            AlteraReserva(itemASerExcluido.Produto.Id, itemASerExcluido.Produto.FormacaoPreco.EstoqueReservado - itemASerExcluido.Quantidade);
                        }
                       
                    }
                    _listaItensPedidosVenda.Remove(itemASerExcluido);

                    PreenchaGridItens();
                }
            }
        }
       
       
        private void btnFecharVenda_Click(object sender, EventArgs e)
        {
            if(ValidaTipoDocumento()) return;

            if (ValideSeSalvarOuFechar()) return;

            Action actionFechamentoDePedido = () =>
            {
                var pedidoDeVendaEmEdicao = RetornePedidoDeVendaEmEdicao();

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var tipoMensagem = servicoPedidoDeVenda.VerificaEstoqueNegativo(pedidoDeVendaEmEdicao);
                
                //Mensagem informando o parâmetro: "Não aceitar estoque negativo". Não deixar fechar o pedido.
                if (tipoMensagem.SegundaResposta)
                {
                    MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + tipoMensagem.PrimeiroConteudo + " . Está ou ficará menor que zero!", "Não é permitido fechar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Mensagem quando preencherem o estoque mínimo. Caso atingir este estoque, o sistema avisa.
                if (tipoMensagem.TerceiraResposta)
                {
                    MessageBox.Show("Verifique os seguintes itens: " + tipoMensagem.SegundoConteudo + ".", "O estoque mínimo foi atingido!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                servicoPedidoDeVenda.ValidePedidoDeVenda(pedidoDeVendaEmEdicao);

               
                try
                {
                    //Para ignorar a validação do crédito inicial, carrega-se com um valor bem alto
                    if (_parametros.ParametrosFinanceiro.IgnorarCreditoInicial)
                        pedidoDeVendaEmEdicao.SaldoDisponivel = 999999999;
                    else if ((EnumTipoFormaPagamento)pedidoDeVendaEmEdicao.FormaPagamento.Id == EnumTipoFormaPagamento.DINHEIRO ||
                            (EnumTipoFormaPagamento)pedidoDeVendaEmEdicao.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO ||
                            (EnumTipoFormaPagamento)pedidoDeVendaEmEdicao.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                                pedidoDeVendaEmEdicao.SaldoDisponivel = 999999999; //Se for dinheiro, débito ou crédito vai ignorar a validação.

                    servicoPedidoDeVenda.ValidePedidoParaReserva(pedidoDeVendaEmEdicao);

                    Action actionFechamentoPedido = () =>
                    {
                        servicoPedidoDeVenda.FechePedidoDeVenda(pedidoDeVendaEmEdicao);

                        MessageBox.Show("Número do pedido " + pedidoDeVendaEmEdicao.Id + ".");

                        PergunteSeDesejaImprimirPedido(pedidoDeVendaEmEdicao.Id);
                    };

                    TratamentosDeTela.TrateInclusaoEAtualizacao(actionFechamentoPedido, mensagemDeSucesso: "Pedido fechado com sucesso.");

                    LimpePedidoDeVenda();
                }
                catch (Exception)
                {
                    if (MessageBox.Show("O pedido precisa passar pela liberação, Deseja envia-lo?", "Enviar pedido para liberação", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Action actionLiberacaoPedido = () =>
                        {
                            servicoPedidoDeVenda.EnviePedidoDeVendaParaLiberacao(pedidoDeVendaEmEdicao);

                            MessageBox.Show("Número do pedido " + pedidoDeVendaEmEdicao.Id + ".");

                            PergunteSeDesejaImprimirPedido(pedidoDeVendaEmEdicao.Id);
                        };

                        TratamentosDeTela.TrateInclusaoEAtualizacao(actionLiberacaoPedido, mensagemDeSucesso: "O Pedido foi enviado para liberação.");

                        LimpePedidoDeVenda();
                    }
                }
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionFechamentoDePedido, exibirMensagemDeSucesso: false);
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar este pedido de venda?", "Cancelar pedido de venda", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            Action actionCancelamentoDePedido = () =>
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                servicoPedidoDeVenda.CancelePedidoDeVenda(txtId.Text.ToInt());

                var pedidoDeVendaconsulta = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());

                cancelahistorico(txtId.Text.ToInt());
                AlteraPedidoStatus(txtId.Text.ToInt());
                if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)
                {
                    foreach (var itemproduto in pedidoDeVendaconsulta.ListaItens)
                    {
                        double itemReservado = itemproduto.Produto.FormacaoPreco.EstoqueReservado;
                        double QuantidadeReservado = 0;
                        QuantidadeReservado = itemReservado  - itemproduto.Quantidade;
                        if (QuantidadeReservado < 0)
                        {
                            QuantidadeReservado = 0;
                        }



                        CancelaReserva(itemproduto.Produto.Id, QuantidadeReservado);
                    }
                }
                LimpePedidoDeVenda();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionCancelamentoDePedido, mensagemDeSucesso: "Pedido cancelado com sucesso.");
            
        }
        private void CancelaReserva(int produto, double quantidade)
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

            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update produtos set PROD_ESTOQUE_RESERVADO = " + (quantidade) +

                            " where prod_id=" + produto;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }
        private void AlteraPedidoStatus(int idPedidoDeVenda)
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




            using (var conn = new MySqlConnection(ConectionString))

            {
                conn.Open();

                string Sql = "update historicosatendimento set hisat_status= " + 1 +

                            " where hisat_novo_pedido_id=" + idPedidoDeVenda;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();


            }

        }
        private void cancelahistorico(int pedido)
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

            

            using (var conn = new MySqlConnection(ConectionString))
            {

                conn.Open();
                string Sql = "";


                Sql = "delete from  historicosatendimento " +
                        " where hisat_novo_pedido_id = " + pedido;



                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
            }

           
        }
        private void btnCancelarParcela_Click(object sender, EventArgs e)
        {
            LimpeCamposParcela();
        }

        private void btnAtualizarParcela_Click(object sender, EventArgs e)
        {
            AtualizeParcela();
        }

        private void gcFinanceiro_DoubleClick(object sender, EventArgs e)
        {
            EditeParcelaFinanceiro();
        }

        private void gcFinanceiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditeParcelaFinanceiro();
            }
        }

        private void cboCondicaoPagamento_EditValueChanged(object sender, EventArgs e)
        {
            GereParcelasFinanceiro();
        }

        private void cboFormaPagamento_EditValueChanged(object sender, EventArgs e)
        {
            if (cboFormaPagamento.EditValue != null)
            {
                _formaPagamentoSelecionada = _listaFormasPagamentos.FirstOrDefault(x => x.Id == cboFormaPagamento.EditValue.ToInt());
            }
            else
            {
                _formaPagamentoSelecionada = null;
            }

            PreenchaCboCondicaoPagamento();

            GereParcelasFinanceiro();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {
            FormPesquisaPedidoDeVenda formPesquisaPedidoDeVenda = new FormPesquisaPedidoDeVenda();

            var pedidoDeVenda = formPesquisaPedidoDeVenda.ExibaPesquisaDePedidosDeVenda();

            if (pedidoDeVenda != null)
            {
                PreenchaPedidoDeVenda(pedidoDeVenda);
            }
        }

        private void btnAtalhoCliente_Click(object sender, EventArgs e)
        {
            FormCadastroPessoa formCadastroPessoa = new FormCadastroPessoa();
            formCadastroPessoa.ShowDialog();
        }

        private void btnAtalhoTabelaPreco_Click(object sender, EventArgs e)
        {
            FormTabelaDePreco formTabelaDePreco = new FormTabelaDePreco();
            formTabelaDePreco.ShowDialog();

            PreenchaCboTabelaPreco();
        }

        private void btnAtalhoFormaPagamento_Click(object sender, EventArgs e)
        {
            FormCadastroFormaPagamento formCadastroFormaPagamento = new FormCadastroFormaPagamento();
            formCadastroFormaPagamento.ShowDialog();

            PreenchaCboFormaPagamento();
        }

        private void btnAtalhoCondicaoPagamento_Click(object sender, EventArgs e)
        {
            FormCadastroCondicaoPagamento formCadastroCondicaoPagamento = new FormCadastroCondicaoPagamento();
            formCadastroCondicaoPagamento.ShowDialog();

            PreenchaCboCondicaoPagamento();
        }

        private void btnAtalhoIndicador_Click(object sender, EventArgs e)
        {
            FormCadastroComissao formCadastroComissao = new FormCadastroComissao();
            formCadastroComissao.ShowDialog();

            PreenchaCboIndicadores();
        }

        private void btnAtalhoVendedor_Click(object sender, EventArgs e)
        {
            FormCadastroComissao formCadastroComissao = new FormCadastroComissao();
            formCadastroComissao.ShowDialog();

            PreenchaCboVendedores();
        }

        private void btnAtalhoAtendente_Click(object sender, EventArgs e)
        {
            FormCadastroComissao formCadastroComissao = new FormCadastroComissao();
            formCadastroComissao.ShowDialog();

            PreenchaCboAtendentes();
        }

        private void btnAtalhoSupervisor_Click(object sender, EventArgs e)
        {
            FormCadastroComissao formCadastroComissao = new FormCadastroComissao();
            formCadastroComissao.ShowDialog();

            PreenchaCboSupervisores();
        }

        private void btnAtalhoTransportadora_Click(object sender, EventArgs e)
        {
            FormCadastroPessoa formCadastroPessoa = new FormCadastroPessoa();
            formCadastroPessoa.ShowDialog();

            PreenchaCboTransportadoras();
        }

        private void rdbFreteFob_CheckedChanged(object sender, EventArgs e)
        {
            HabiliteOuDesabiliteFreteEDesconto();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimaPedidoDeVenda(txtId.Text.ToInt());
        }

        private void cboFormaPagamentoFinanceiro_EditValueChanged(object sender, EventArgs e)
        {
            CarregaComboOperadorasDebitoCredito();
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        #region " APLICAÇÃO DE PARAMETROS "

        private void CarregueParametros()
        {
            ServicoParametros servicoParametros = new ServicoParametros();

            _parametros = servicoParametros.ConsulteParametros();

            txtObservacoesGeraisVenda.Text = _parametros.ParametrosVenda.ObservacoesVendaRapida;
        }

        private void ApliqueParametros()
        {
            if (!_parametros.ParametrosVenda.PermiteAlterarAtendente)
            {
                pnlAtendente.Enabled = false;
                cboAtendentes.Size = new Size(pnlAtendente.Width, cboAtendentes.Height);
            }

            if (!_parametros.ParametrosVenda.PermiteAlterarIndicador)
            {
                pnlIndicador.Enabled = false;
                cboIndicadores.Size = new Size(pnlIndicador.Width, cboIndicadores.Height);
            }

            if (!_parametros.ParametrosVenda.PermiteAlterarSupervisor)
            {
                pnlSupervisor.Enabled = false;
                cboSupervisores.Size = new Size(pnlSupervisor.Width, cboSupervisores.Height);
            }

            if (!_parametros.ParametrosVenda.PermiteAlterarVendedor)
            {
                pnlVendedor.Enabled = false;
                cboVendedores.Size = new Size(pnlVendedor.Width, cboVendedores.Height);
            }

            if (!_parametros.ParametrosVenda.PermiteAlterarValorUnitario)
            {
                txtValorUnitarioProduto.Properties.ReadOnly = true;
            }

            HabiliteOuDesabiliteFreteEDesconto();
        }

        private void HabiliteOuDesabiliteFreteEDesconto()
        {
            if (_listaItensPedidosVenda.Count == 0)
            {
                pnlTipoDescontoFechamento.Enabled = false;
                txtDescontoFechamento.Properties.ReadOnly = true;
                txtDescontoFechamento.Text = string.Empty;

                txtFreteFechamento.Text = string.Empty;
                txtFreteFechamento.Properties.ReadOnly = true;
            }
            else
            {
                if (!_parametros.ParametrosVenda.PermiteDescontoNoTotalVenda)
                {
                    pnlTipoDescontoFechamento.Enabled = false;
                    txtDescontoFechamento.Properties.ReadOnly = true;
                }
                else
                {
                    pnlTipoDescontoFechamento.Enabled = true;
                    txtDescontoFechamento.Properties.ReadOnly = false;
                }

                if (rdbFreteFob.Checked)
                {
                    txtFreteFechamento.Properties.ReadOnly = false;
                }
                else
                {
                    txtFreteFechamento.Text = string.Empty;
                    txtFreteFechamento.Properties.ReadOnly = true;
                }
            }
        }

        private void PesquiseEmpresa()
        {
            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            _empresa.DadosEmpresa.Endereco.Cidade.Estado.CarregueLazyLoad();
        }

        #endregion

        #region " PREENCHIMENTO DE COMBOBOX "

        private void PreenchaCboTipoDocumento()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPedidoDeVenda>();

            lista.Insert(0, null);

            cboTipoDocumento.Properties.DisplayMember = "Descricao";
            cboTipoDocumento.Properties.ValueMember = "Valor";
            cboTipoDocumento.Properties.DataSource = lista;
        }

        private void PreenchaCboTipoEndereco()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoEndereco>();

            lista.Insert(0, null);

            cboTipoEndereco.Properties.DisplayMember = "Descricao";
            cboTipoEndereco.Properties.ValueMember = "Valor";
            cboTipoEndereco.Properties.DataSource = lista;
        }

        private void PreenchaCboTabelaPreco()
        {
            List<TabelaPreco> listaTabelaPreco = new List<TabelaPreco>();

            if (_crediario != null && _crediario.TabelaPreco != null)
            {
                listaTabelaPreco.Add(_crediario.TabelaPreco);

                cboTabelaPrecos.EditValue = _crediario.TabelaPreco.Id;
            }
            else
            {
                if (_parametros.ParametrosVenda.ExibirTodasAsTabelasPrecoPedidoVenda)
                {
                    ServicoTabelaPreco servioTabelaPreco = new ServicoTabelaPreco();
                    listaTabelaPreco = servioTabelaPreco.ConsulteListaTabelaPrecosAtivas();
                }
                else
                {
                    ServicoComissao servicoComissao = new ServicoComissao();

                    List<Comissao> listaComissoes = servicoComissao.ConsulteLista(Sessao.PessoaLogada);

                    foreach (var comissao in listaComissoes)
                    {
                        if (comissao.TabelaPreco.Status == "A" && !listaTabelaPreco.Exists(tabelaPreco => tabelaPreco.Id == comissao.TabelaPreco.Id))
                        {
                            listaTabelaPreco.Add(comissao.TabelaPreco);
                        }
                    }
                }

                listaTabelaPreco.Insert(0, null);
            }

            cboTabelaPrecos.Properties.DisplayMember = "NomeTabela";
            cboTabelaPrecos.Properties.ValueMember = "Id";
            cboTabelaPrecos.Properties.DataSource = listaTabelaPreco;

            if (string.IsNullOrEmpty(cboTabelaPrecos.Text))
            {
                cboTabelaPrecos.EditValue = null;
            }

            if (listaTabelaPreco.Count == 2)
            {
                cboTabelaPrecos.EditValue = listaTabelaPreco[1].Id;
            }

            if(_parametros.ParametrosVenda.TabelaPreco != null && _parametros.ParametrosVenda.TabelaPreco.Id != 0)
                cboTabelaPrecos.EditValue = _parametros.ParametrosVenda.TabelaPreco.Id;
        }

        private void PreenchaCboFormaPagamento()
        {
            List<FormaPagamento> lista = new List<FormaPagamento>();

            if (_crediario != null && _crediario.FormaPagamento != null)
            {
                lista.Add(_crediario.FormaPagamento);

                cboFormaPagamento.EditValue = _crediario.FormaPagamento.Id;
            }
            else
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();

                lista = servicoFormaPagamento.ConsulteListaAtivos();

            }

            _listaFormasPagamentos = lista.CloneCompleto();

            cboFormaPagamentoFinanceiro.Properties.DisplayMember = "Descricao";
            cboFormaPagamentoFinanceiro.Properties.ValueMember = "Id";
            cboFormaPagamentoFinanceiro.Properties.DataSource = _listaFormasPagamentos;

            lista.Insert(0, null);

            cboFormaPagamento.Properties.DisplayMember = "Descricao";
            cboFormaPagamento.Properties.ValueMember = "Id";
            cboFormaPagamento.Properties.DataSource = lista;

            if (string.IsNullOrEmpty(cboFormaPagamento.Text))
            {
                cboFormaPagamento.EditValue = null;
            }

            if (lista.Count == 2)
            {
                cboFormaPagamento.EditValue = lista[1].Id;
            }
        }

        private void PreenchaCboCondicaoPagamento()
        {
            List<CondicaoPagamento> listaCondicoes = new List<CondicaoPagamento>();
            ////if (listaCondicoes.Count != 0)
            ////{

            

                    if (_crediario != null && _crediario.CondicaoPagamento != null)
                    {
                        listaCondicoes.Add(_crediario.CondicaoPagamento);

                        cboCondicaoPagamento.EditValue = _crediario.CondicaoPagamento.Id;
                    }
                    else
                    {
                        ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                        var formaPagamento = servicoFormaPagamento.Consulte(cboFormaPagamento.EditValue.ToInt());

                        if (formaPagamento != null &&
                            formaPagamento.ListaCondicoesPagamento != null &&
                            formaPagamento.ListaCondicoesPagamento.Count > 0)
                        {
                            foreach (var item in formaPagamento.ListaCondicoesPagamento)
                            {
                                if (item.CondicaoPagamento.Status == "A")
                                {
                                    listaCondicoes.Add(item.CondicaoPagamento);
                                }
                            }
                        }
                    }

                    listaCondicoes.Insert(0, null);

                    cboCondicaoPagamento.Properties.DisplayMember = "Descricao";
                    cboCondicaoPagamento.Properties.ValueMember = "Id";
                    cboCondicaoPagamento.Properties.DataSource = listaCondicoes;

                    if (string.IsNullOrEmpty(cboCondicaoPagamento.Text))
                    {
                        cboCondicaoPagamento.EditValue = null;
                    }

                    if (listaCondicoes.Count == 2)
                    {
                        cboCondicaoPagamento.EditValue = listaCondicoes[1].Id;
                    }
            }
        //}

        private void PreenchaCboEstados()
        {
            ServicoEstado servicoEstado = new ServicoEstado();

            var listaDeEstados = servicoEstado.ConsulteListaEstados();

            listaDeEstados.Insert(0, null);

            cboEstado.Properties.DataSource = listaDeEstados;
            cboEstado.Properties.DisplayMember = "Nome";
            cboEstado.Properties.ValueMember = "UF";
        }

        private void PreenchaCboCidades()
        {
            string uf = cboEstado.EditValue == null ? string.Empty : cboEstado.EditValue.ToString();

            var listaDeCidades = _servicoCidade.ConsulteListaCidadesAtivasPorEstado(uf);

            _listaCidadesCbo = listaDeCidades.CloneCompleto();
            listaDeCidades.Insert(0, null);

            cboCidade.Properties.DataSource = listaDeCidades;
            cboCidade.Properties.DisplayMember = "Descricao";
            cboCidade.Properties.ValueMember = "Id";
        }

        #endregion

        #region " PREENCHIMENTO DE PESSOA INDICADOR, ATENDENTE, VENDEDOR, SUPERVISOR, TRANSPORTADORA E CLIENTE "

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

            listaObjetoValor.Insert(0, null);

            cboIndicadores.Properties.DisplayMember = "Descricao";
            cboIndicadores.Properties.ValueMember = "Valor";
            cboIndicadores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboIndicadores.Text))
            {
                cboIndicadores.EditValue = null;
            }
        }

        private void PreenchaCboAtendentes()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaAtendentesAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboAtendentes.Properties.DisplayMember = "Descricao";
            cboAtendentes.Properties.ValueMember = "Valor";
            cboAtendentes.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboAtendentes.Text))
            {
                cboAtendentes.EditValue = null;
            }
        }

        private void PreenchaCboVendedores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaVendedoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboVendedores.Properties.DisplayMember = "Descricao";
            cboVendedores.Properties.ValueMember = "Valor";
            cboVendedores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboVendedores.Text))
            {
                cboVendedores.EditValue = null;
            }
        }

        private void PreenchaCboSupervisores()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaSupervisoresAtivos();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboSupervisores.Properties.DisplayMember = "Descricao";
            cboSupervisores.Properties.ValueMember = "Valor";
            cboSupervisores.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboSupervisores.Text))
            {
                cboSupervisores.EditValue = null;
            }
        }

        private void PreenchaCboTransportadoras()
        {
            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var lista = servicoPessoa.ConsulteListaTransportadorasAtivas();

            List<ObjetoDescricaoValor> listaObjetoValor = new List<ObjetoDescricaoValor>();

            lista.ForEach(pessoa =>
            {
                ObjetoDescricaoValor objetoDescricaoValor = new ObjetoDescricaoValor { Descricao = pessoa.Id + " - " + pessoa.DadosGerais.Razao, Valor = pessoa.Id };

                listaObjetoValor.Add(objetoDescricaoValor);
            });

            listaObjetoValor.Insert(0, null);

            cboTransportadoras.Properties.DisplayMember = "Descricao";
            cboTransportadoras.Properties.ValueMember = "Valor";
            cboTransportadoras.Properties.DataSource = listaObjetoValor;

            if (string.IsNullOrWhiteSpace(cboTransportadoras.Text))
            {
                cboTransportadoras.EditValue = null;
            }

            if (_parametros.ParametrosVenda.Transportadora != null)            
                cboTransportadoras.EditValue = _parametros.ParametrosVenda.Transportadora.Id;
            
            if (_parametros.ParametrosVenda.TipoFrete != null)
            {
                rdbFreteARetirar.Checked = _parametros.ParametrosVenda.TipoFrete == EnumTipoFrete.PORCONTADETERCEIROS ? true : false;
                rdbFreteCif.Checked = _parametros.ParametrosVenda.TipoFrete == EnumTipoFrete.PORCONTADOEMITENTE? true : false; //Fornecedor
                rdbFreteFob.Checked = _parametros.ParametrosVenda.TipoFrete == EnumTipoFrete.PORCONTADODESTINATARIOREMETENTE? true:false; //Cliente
                rdbFreteNenhum.Checked = _parametros.ParametrosVenda.TipoFrete == EnumTipoFrete.SEMCOBRANCADEFRETE? true: false;
            }

        }

        private void PreenchaCliente(Pessoa cliente,
                                                 bool exibirMensagemDeNaoEncontrado = false,
                                                 bool preencherEndereco = true,
                                                 bool preencherEmpresa = true,
                                                 bool preencherIndicador = true,
                                                 bool preencherAtendente = true,
                                                 bool preencherVendedor = true,
                                                 bool preencherSupervisor = true,
                                                 bool preencherTabelaPreco = true,
                                                 bool preencherFormaPagamento = true,
                                                 bool preencherCondicaoPagamento = true,
                                                 bool preencherAReceberAberto = true,
                                                 bool preencherSaldoDisponivel = true,
                                                 bool preencherMaiorVenda = true,
                                                 bool preencherTipoCliente = true)
        {
            _clienteSelecionado = cliente;

            PreenchaDadosClienteCabecalho(cliente);

            if (preencherEndereco)
                PreenchaDadosClienteEndereco(cliente);

            if (preencherEmpresa)
                PreenchaDadosClienteEmpresa(cliente);

            if (preencherIndicador)
                PreenchaDadosClienteIndicador(cliente);

            if (preencherAtendente)
                PreenchaDadosClienteAtendente(cliente);

            if (preencherVendedor)
                PreenchaDadosClienteVendedor(cliente);

            if (preencherSupervisor)
                PreenchaDadosClienteSupervisor(cliente);

            if (preencherTabelaPreco)
                PreenchaDadosClienteTabelaPreco(cliente);

            if (preencherFormaPagamento)
                PreenchaDadosClienteFormaPagamento(cliente);

            if (preencherCondicaoPagamento)
                PreenchaDadosClienteCondicaoPagamento(cliente);

            if (preencherAReceberAberto)
                PreenchaDadosClienteAReceberAberto(cliente);

            if (preencherSaldoDisponivel)
                PreenchaDadosClienteSaldoDisponivel();

            if (preencherMaiorVenda)
                PreenchaDadosClienteMaiorVenda(cliente);

            if (preencherTipoCliente)
                PreenchaTipoCliente(cliente);

            if (cliente != null)
            {
                txtIdCliente.Text = cliente.Id.ToString();
                txtNomeCliente.Text = cliente.DadosGerais.Razao;

                FormAvisoClienteInadimplente.ExibaAvisoInadimplente(_clienteSelecionado.Id);
                //Andre
                CarregueAnaliseCredito();

                txtcashback.Text = "0,00";
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

                FormAvisoClienteInadimplente.EscondaAvisoInadimplente();
            }
        }

        #endregion

        #region " ENDEREÇO "

        private void PreenchaDadosEnderecoCep(Endereco endereco)
        {
            if (endereco != null)
            {
                txtCepEndereco.Text = endereco.CEP;
                cboEstado.EditValue = endereco.Cidade.Estado.UF;
                cboCidade.EditValue = endereco.Cidade.Id;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;
            }
        }

        private void PesquiseCep()
        {
            ServicoEndereco servicoEndereco = new ServicoEndereco();

            var endereco = servicoEndereco.Consulte(txtCepEndereco.Text);

            PreenchaDadosEnderecoCep(endereco);
        }

        private void PesquiseCepPelaTelaDePesquisa()
        {
            FormEnderecoPesquisa formEnderecoPesquisa = new FormEnderecoPesquisa();

            var endereco = formEnderecoPesquisa.PesquiseEndereco();

            if (endereco != null)
            {
                PreenchaDadosEnderecoCep(endereco);
            }
        }

        #endregion

        #region " PREENCHIMENTO CAMPOS QUANDO SELECIONA O CLIENTE "

        private void BusqueECarreguePedido()
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                var pedidoDeVenda = servicoPedidoDeVenda.Consulte(txtId.Text.ToInt());

                PreenchaPedidoDeVenda(pedidoDeVenda, exibirMensagemDeNaoEncontrado: true);
            }
        }

        private void PreenchaDadosClienteCabecalho(Pessoa cliente)
        {
            if (cliente != null)
            {
                txtCpfCnpj.Text = cliente.DadosGerais.CpfCnpj;
                txtStatusCliente.Text = cliente.DadosGerais.Status == "A" ? "ATIVO" : "INATIVO";
                txtRazaoSocial.Text = cliente.DadosGerais.Razao;
                txtTipoPessoa.Text = cliente.DadosGerais.TipoPessoa.Descricao();
                txtDataCadastroCliente.Text = cliente.DadosGerais.DataCadastro.ToString("dd/MM/yyyy");
            }
            else
            {
                txtCpfCnpj.Text = string.Empty;
                txtStatusCliente.Text = string.Empty;
                txtRazaoSocial.Text = string.Empty;
                txtTipoPessoa.Text = string.Empty;
                txtDataCadastroCliente.Text = string.Empty;
            }
        }

        private void PreenchaDadosClienteEndereco(Pessoa cliente, EnumTipoEndereco tipoEndereco = EnumTipoEndereco.PRINCIPAL )
        {
            if (cliente != null && cliente.ListaDeEnderecos != null && cliente.ListaDeEnderecos.Count > 0)
            {
                var endereco = cliente.ListaDeEnderecos.FirstOrDefault(end => end.TipoEndereco == tipoEndereco);

                if (endereco == null)
                {
                    endereco = cliente.ListaDeEnderecos.First();
                }

                txtCepEndereco.Text = endereco.CEP;
                txtBairro.Text = endereco.Bairro;
                txtRua.Text = endereco.Rua;

                cboEstado.EditValue = endereco.Cidade.Estado.UF;
                cboCidade.EditValue = endereco.Cidade.Id;

                txtNumero.Text = endereco.Numero;
                txtComplementoEndereco.Text = endereco.Complemento;
                cboTipoEndereco.EditValue = endereco.TipoEndereco;
            }
            else
            {
                txtCepEndereco.Text = string.Empty;
                txtBairro.Text = string.Empty;
                txtRua.Text = string.Empty;

                cboCidade.EditValue = null;
                cboEstado.EditValue = null;

                txtNumero.Text = string.Empty;
                txtComplementoEndereco.Text = string.Empty;
                cboTipoEndereco.EditValue = null;
            }
        }

        private void PreenchaDadosClienteEmpresa(Pessoa cliente)
        {
            if (cliente != null && cliente.EmpresaPessoa != null)
            {

                ServicoClienteRapido servicoCliente = new ServicoClienteRapido();

                cliente = servicoCliente.Consulte(cliente.Id);

                txtRamoAtividade.Text = cliente.EmpresaPessoa.RamoDeAtividade != null ? cliente.EmpresaPessoa.RamoDeAtividade.Descricao : null;
                txtInscricaoEstadual.Text = cliente.EmpresaPessoa.InscricaoEstadual;
                txtInscricaoMunicipal.Text = cliente.EmpresaPessoa.InscricaoMunicipal;
                txtEmailPrincipalNfe.Text = cliente.EmpresaPessoa.EmailPrincipal;

                ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
                var analiseCredito = servicoAnaliseCredito.Consulte(cliente.Id);

                txtLimiteCredito.Text = analiseCredito != null ? analiseCredito.ValorLimiteCredito.ToString("#,###,##0.00") : string.Empty;
            }
            else
            {
                txtRamoAtividade.Text = string.Empty;
                txtInscricaoEstadual.Text = string.Empty;
                txtInscricaoMunicipal.Text = string.Empty;
                txtEmailPrincipalNfe.Text = string.Empty;
                txtLimiteCredito.Text = string.Empty;
            }
        }

        private void PreenchaDadosClienteIndicador(Pessoa cliente)
        {
            cboIndicadores.EditValue = cliente != null && cliente.Atendimento != null && cliente.Atendimento.Indicador != null ? (int?)cliente.Atendimento.Indicador.Id : null;

            if (string.IsNullOrEmpty(cboIndicadores.Text))
            {
                cboIndicadores.EditValue = null;
            }
        }

        private void PreenchaDadosClienteAtendente(Pessoa cliente)
        {
            cboAtendentes.EditValue = cliente != null && cliente.Atendimento != null && cliente.Atendimento.Atendente != null ? (int?)cliente.Atendimento.Atendente.Id : null;

            if (string.IsNullOrEmpty(cboAtendentes.Text))
            {
                cboAtendentes.EditValue = null;
            }
        }

        private void PreenchaDadosClienteVendedor(Pessoa cliente)
        {
            cboVendedores.EditValue = cliente != null && cliente.Atendimento != null && cliente.Atendimento.Vendedor != null ? (int?)cliente.Atendimento.Vendedor.Id : null;

            if (string.IsNullOrEmpty(cboVendedores.Text))
            {
                cboVendedores.EditValue = null;
            }
        }

        private void PreenchaDadosClienteSupervisor(Pessoa cliente)
        {
            cboSupervisores.EditValue = cliente != null && cliente.Atendimento != null && cliente.Atendimento.Supervisor != null ? (int?)cliente.Atendimento.Supervisor.Id : null;

            if (string.IsNullOrEmpty(cboSupervisores.Text))
            {
                cboSupervisores.EditValue = null;
            }
        }

        private void PreenchaDadosClienteTabelaPreco(Pessoa cliente)
        {
            if (cliente != null && cliente.Atendimento != null)
            {
                cboTabelaPrecos.EditValue = cliente.Atendimento.TabelaDePreco != null ? (int?)cliente.Atendimento.TabelaDePreco.Id : null;

                if (string.IsNullOrEmpty(cboTabelaPrecos.Text))
                {
                    cboTabelaPrecos.EditValue = null;
                }
            }
            else
            {
                cboTabelaPrecos.EditValue = null;
            }
        }

        private void PreenchaDadosClienteFormaPagamento(Pessoa cliente)
        {
            if (cliente != null && cliente.Atendimento != null)
            {
                cboFormaPagamento.EditValue = cliente.Atendimento.FormaPagamento != null ? (int?)cliente.Atendimento.FormaPagamento.Id : null;
            }
            else
            {
                cboFormaPagamento.EditValue = null;
            }
        }

        private void PreenchaDadosClienteCondicaoPagamento(Pessoa cliente)
        {
            if (cliente != null && cliente.Atendimento != null)
            {
                cboCondicaoPagamento.EditValue = cliente.Atendimento.CondicaoDePagamento != null ? (int?)cliente.Atendimento.CondicaoDePagamento.Id : null;
            }
            else
            {
                cboCondicaoPagamento.EditValue = null;
            }
        }

        private void PreenchaDadosClienteAReceberAberto(Pessoa cliente)
        {
            if (cliente != null)
            {
                ServicoContasReceber servicoContasReceber = new ServicoContasReceber();

                var listaContasAReceber = servicoContasReceber.ConsulteListaAberto(cliente);

                var valorTotal = listaContasAReceber.Sum(x => x.ValorTotal);

                txtAReceberAberto.Text = valorTotal.ToString("#,###,##0.00");
            }
            else
            {
                txtAReceberAberto.Text = string.Empty;
            }
        }

        private void PreenchaDadosClienteSaldoDisponivel()
        {
            var limite = txtLimiteCredito.Text.ToDouble();
            var aReceber = txtAReceberAberto.Text.ToDouble();

            var saldoDisponivel = limite - aReceber;

            txtSaldoDisponivel.Text = saldoDisponivel.ToString("#,###,##0.00");
        }

        private void PreenchaDadosClienteMaiorVenda(Pessoa cliente)
        {
            if (cliente != null)
            {
                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                PedidoDeVenda pedidoDeVenda = servicoPedidoDeVenda.ConsulteMaiorVenda(cliente);

                if (pedidoDeVenda != null)
                {
                    txtMaiorCompra.Text = pedidoDeVenda.ValorTotal.ToString("#,###,##0.00");
                }
                else
                {
                    txtMaiorCompra.Text = string.Empty;
                }
            }
            else
            {
                txtMaiorCompra.Text = string.Empty;
            }
        }

        private void PreenchaTipoCliente(Pessoa cliente)
        {
            if (cliente != null)
            {
                pnlAtendimento.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)cliente.DadosGerais.TipoCliente).Checked = true;
            }
            else
            {
                rdbConsumidorFinal.Checked = true;
            }
        }

        #endregion

        #region " ABA ITENS "

        private void PreenchaProduto(Produto produto, bool exibirMensagemDeNaoEncontrado = false)
        {
            _produtoEmEdicao = produto;
            double itensDisponiveis;
            double quantidadesubestoque = 0;
            if (produto != null)
            {
                TabelaPreco tabelaPreco = cboTabelaPrecos.EditValue != null ? new TabelaPreco { Id = cboTabelaPrecos.EditValue.ToInt() } : null;

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
                }

                if (produto.Vestuario != null)
                {
                    produto.Vestuario.Tamanho.CarregueLazyLoad();
                    produto.Vestuario.Cor.CarregueLazyLoad();
                }
                double EstoqueReservado = 0;
                double EstoqueDisponivel = 0;

                if (produto.FormacaoPreco.EstoqueReservado < 0)
                {
                    EstoqueReservado = 0;
                }
                else
                {
                    EstoqueReservado = produto.FormacaoPreco.EstoqueReservado;
                }

                txtReserva.Text = EstoqueReservado.ToString("0.000");

                ServicoItemTransferencia servicoItemTransferencia = new ServicoItemTransferencia();
                
                var ItemTransferencia = servicoItemTransferencia.ConsulteProduto(produto.Id);

                foreach (var itemproduto in ItemTransferencia)
                {
                    quantidadesubestoque += itemproduto.QuantidadeEstoque;
                }

                if (ItemTransferencia != null)
                {

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

                if (itensDisponiveis < 0)
                {
                    txtDisponibilidadeItem.ForeColor = Color.Red;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }
                else
                {
                    txtDisponibilidadeItem.ForeColor = Color.Black;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }

                produto.DadosGerais.Unidade.CarregueLazyLoad();

                txtIdProduto.Text = produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = produto.DadosGerais.CodigoDeBarras;
                txtDescricaoProduto.Text = produto.DadosGerais.Descricao;

                var valorUnitario = ServicoPedidoDeVenda.CalculePrecoUnitarioProduto(tabelaPreco, produto);

                txtValorUnitarioProduto.Text = valorUnitario.ToString("0.00");

                txtQuantidadeProduto.Text = "1";

                AltereMascaraQuantidadeProduto();

                txtValorUnitarioProduto.Focus();
            }
            else
            {
                txtIdProduto.Text = string.Empty;
                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtDescricaoProduto.Text = string.Empty;
                txtValorUnitarioProduto.Text = string.Empty;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Produto nao encontrado!", "Produto não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoDeBarrasProduto.Focus();
                }
            }
        }

        private void InsiraOuAtualizeItemPedido()
        {
            if(cboTipoDocumento.EditValue == null)
            {
                MessageBox.Show("Para continuar, informe o tipo de documento.","Inserção de Itens", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if((EnumTipoPedidoDeVenda)cboTipoDocumento.EditValue != EnumTipoPedidoDeVenda.ORCAMENTO)
            {

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
            }


          

            Action actionInserirItem = () =>
            {
             

                ItemPedidoDeVenda itemPedidoVendaII = RetorneItemPedidoDeVendaEmEdicaoII();
                PedidoDeVenda pedidoDeVendaII = RetornePedidoDeVendaEmEdicao();

                if (ValideSeItemEstahEmBaixa(pedidoDeVendaII.StatusPedidoVenda, itemPedidoVendaII))
                {
                    return;
                }
                
                ItemPedidoDeVenda itemPedidoVenda = RetorneItemPedidoDeVendaEmEdicao();
                PedidoDeVenda pedidoDeVenda = RetornePedidoDeVendaEmEdicao();

                itemPedidoVenda.PedidoDeVenda = pedidoDeVenda;

                _listaItensPedidosVenda.Remove(_itemPedidoDeVendaEmEdicao);

                ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

                servicoPedidoDeVenda.ValideItemPedidoVenda(itemPedidoVenda);

                try
                {
                    if (itemPedidoVenda.Produto.FormacaoPreco.EhPromocao == false || itemPedidoVenda.DescontoUnitario != 0)
                        servicoPedidoDeVenda.ValideItemPedidoVendaLiberacao(itemPedidoVenda);

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
                        if (item.Produto.Id == itemPedidoVenda.Produto.Id)
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
                    int posicaoItem = itemPedidoVenda.Id - 1;
                   

                    _listaItensPedidosVenda.Insert(posicaoItem, itemPedidoVenda);
                }

                LimpeCamposProduto();

                PreenchaGridItens();
               
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionInserirItem, exibirMensagemDeSucesso: false);
        }

        private void BloquearCamposTitulo()
        {
            txtIdCliente.Properties.ReadOnly = true;

            cboTabelaPrecos.Enabled = false;
        }

        private void DesbloquearCamposTitulo()
        {
            txtIdCliente.Properties.ReadOnly = false;
            cboTabelaPrecos.Enabled = true;
        }

        private ItemPedidoDeVenda RetorneItemPedidoDeVendaEmEdicao()
        {
            _itemPedidoDeVendaEmEdicao = _itemPedidoDeVendaEmEdicao ?? new ItemPedidoDeVenda();

            var item = _itemPedidoDeVendaEmEdicao.CloneCompleto();

            item.DescontoUnitario = txtDescontoUnitario.Text.ToDouble();
            item.DescontoEhPercentual = rdbDescontoProdutoPercentual.Checked;
            item.TotalDesconto = txtTotalDescontoProduto.Text.ToDouble();
            item.ValorFrete = txtValorFreteProduto.Text.ToDouble();
            item.ValorIcmsST = txtValorIcmsSTProduto.Text.ToDouble();
            item.ValorIpi = txtValorIpiProduto.Text.ToDouble();
            item.Produto = _produtoEmEdicao;
            item.Quantidade = txtQuantidadeProduto.Text.ToDouble();
            item.ValorUnitario = txtValorUnitarioProduto.Text.ToDouble();
            item.ValorTotal = txtValorTotalProduto.Text.ToDouble();

            return item;
        }

        private ItemPedidoDeVenda RetorneItemPedidoDeVendaEmEdicaoII()
        {
            _itemPedidoDeVendaEmEdicao = _itemPedidoDeVendaEmEdicao ?? new ItemPedidoDeVenda();

            var item = _itemPedidoDeVendaEmEdicao.CloneCompleto();

            item.DescontoUnitario = txtDescontoUnitario.Text.ToDouble();
            item.DescontoEhPercentual = rdbDescontoProdutoPercentual.Checked;
            item.TotalDesconto = txtTotalDescontoProduto.Text.ToDouble();
            item.ValorFrete = txtValorFreteProduto.Text.ToDouble();
            item.ValorIcmsST = txtValorIcmsSTProduto.Text.ToDouble();
            item.ValorIpi = txtValorIpiProduto.Text.ToDouble();
            if (_produtoAnterior != null)
            {

                item.Produto = _produtoAnterior;

            }
            else
            {
                item.Produto = _produtoEmEdicao;
            }
            item.Quantidade = txtQuantidadeProduto.Text.ToDouble();
            item.ValorUnitario = txtValorUnitarioProduto.Text.ToDouble();
            item.ValorTotal = txtValorTotalProduto.Text.ToDouble();

            return item;
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
                _produtoAnterior =  itemPedidoVenda.Produto;
                itemEditado = true;
                rdbDescontoProdutoValor.Checked = true;
                rdbDescontoProdutoPercentual.Checked = itemPedidoVenda.DescontoEhPercentual;

                txtIdProduto.Text = itemPedidoVenda.Produto.Id.ToString();
                txtCodigoDeBarrasProduto.Text = itemPedidoVenda.Produto.DadosGerais.CodigoDeBarras;
                txtDescricaoProduto.Text = itemPedidoVenda.Produto.DadosGerais.Descricao;

                txtQuantidadeProduto.Text = itemPedidoVenda.Quantidade.ToString();
                txtValorUnitarioProduto.Text = itemPedidoVenda.ValorUnitario.ToString("0.00");
                txtDescontoUnitario.Text = itemPedidoVenda.DescontoUnitario.ToString("0.00##");
                txtTotalDescontoProduto.Text = itemPedidoVenda.DescontoUnitario.ToString("0.00");
                
                itensDisponiveis = itemPedidoVenda.Produto.FormacaoPreco.Estoque - itemPedidoVenda.Produto.FormacaoPreco.EstoqueReservado;

                if (itensDisponiveis < 0)
                {
                    txtDisponibilidadeItem.ForeColor = Color.Red;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }
                else
                {
                    txtDisponibilidadeItem.ForeColor = Color.Black;
                    txtDisponibilidadeItem.Text = itensDisponiveis.ToString();
                }

                txtValorFreteProduto.Text = itemPedidoVenda.ValorFrete.ToString("0.00");
                txtValorIpiProduto.Text = itemPedidoVenda.ValorIpi != null ? itemPedidoVenda.ValorIpi.Value.ToString("0.00") : string.Empty;
                txtValorIcmsSTProduto.Text = itemPedidoVenda.ValorIcmsST != null ? itemPedidoVenda.ValorIcmsST.Value.ToString("0.00") : string.Empty;
                txtValorTotalProduto.Text = itemPedidoVenda.ValorTotal.ToString("0.00");

                AltereMascaraQuantidadeProduto();

                txtValorUnitarioProduto.Focus();

                btnInserirAtualizarItem.Image = Properties.Resources.icon_atualizar;
            }
            else
            {
                _produtoEmEdicao = null;

                txtCodigoDeBarrasProduto.Text = string.Empty;
                txtDescontoUnitario.Text = string.Empty;
                rdbDescontoProdutoPercentual.Checked = true;
                txtTotalDescontoProduto.Text = string.Empty;
                txtValorFreteProduto.Text = string.Empty;
                txtValorIpiProduto.Text = string.Empty;
                txtValorIcmsSTProduto.Text = string.Empty;
                txtIdProduto.Text = string.Empty;
                txtQuantidadeProduto.Text = string.Empty;
                txtValorUnitarioProduto.Text = string.Empty;
                txtValorTotalProduto.Text = string.Empty;

                txtDisponibilidadeItem.ForeColor = Color.Black;
                txtDisponibilidadeItem.Text = string.Empty;

                txtDescricaoProduto.Text = string.Empty;

                txtCodigoDeBarrasProduto.Focus();

                btnInserirAtualizarItem.Image = Properties.Resources.icones2_19;
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

        private void EditeItem()
        {
            if (_listaItensPedidosVenda != null && _listaItensPedidosVenda.Count > 0)
            {
                var itemPedido = _listaItensPedidosVenda.FirstOrDefault(item => item.Id == colunaId.View.GetFocusedRowCellValue(colunaId).ToInt());
                //itemProduto = itemPedido;
                
                PreenchaCamposItens(itemPedido);
            }
        }

        private void PreenchaGridItens()
        {
            DesbloquearCamposTitulo();

            GereIdFalsoParaOsItens();

            HabiliteOuDesabiliteFreteEDesconto();

            List<ItemGrid> listaItemGrid = new List<ItemGrid>();
           

            foreach (var item in _listaItensPedidosVenda)
            {
                ItemGrid itemGrid = new ItemGrid();
                double Desconto = 0;
                double Valor = 0;
                
               


                itemGrid.Id = item.Id;

               


                itemGrid.CodigoDeBarras = item.Produto.DadosGerais.CodigoDeBarras;
                itemGrid.IdProduto = item.Produto.Id;
                

                itemGrid.Cor = item.Produto.Vestuario != null && item.Produto.Vestuario.Cor != null ? item.Produto.Vestuario.Cor.Descricao : string.Empty;
                itemGrid.Descricao = item.Produto.DadosGerais.Descricao;
               

                itemGrid.Desconto = "R$" + item.TotalDesconto.ToString("0.00");

                itemGrid.MarcaFabricante = item.Produto.Principal != null && item.Produto.Principal != null && item.Produto.Principal.Marca != null ? item.Produto.Principal.Marca.Descricao : string.Empty;
                itemGrid.Modelo = item.Produto.Vestuario != null ? item.Produto.Vestuario.Modelo : string.Empty;
                itemGrid.Quantidade = item.Quantidade.ToDouble();
                itemGrid.Sexo = item.Produto.Vestuario != null && item.Produto.Vestuario.SexoProduto != null ? item.Produto.Vestuario.SexoProduto.Value.Descricao() : string.Empty;
                itemGrid.Tamanho = item.Produto.Vestuario != null && item.Produto.Vestuario.Tamanho != null ? item.Produto.Vestuario.Tamanho.Descricao : string.Empty;
                itemGrid.Unidade = item.Produto.DadosGerais.Unidade != null ? item.Produto.DadosGerais.Unidade.Descricao : string.Empty;
                itemGrid.ValorTotal = item.ValorTotal.ToString("0.00");
                itemGrid.ValorUnitario = item.ValorUnitario.ToString("0.00");
                itemGrid.ItemEstahInconsistente = item.ItemEstahInconsistente;
                if (item.Produto.FormacaoPreco.PercentualDescontoMaximo == null)
                {
                    Desconto = 0;
                }
                else
                {
                    Desconto = item.Produto.FormacaoPreco.PercentualDescontoMaximo.ToDouble();
                }

               
                Valor = item.Produto.FormacaoPreco.ValorVenda.ToDouble() - (item.Produto.FormacaoPreco.ValorVenda.ToDouble() * Desconto) /100;
                itemGrid.QtdMinimo = string.Format("{0:N}", Valor);

                //if (_parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido == true)
                //{

                //    itemGrid. item.Produto.FormacaoPreco.EstoqueReservado += item.Quantidade.ToDouble();
                //}

                listaItemGrid.Add(itemGrid);
                

                BloquearCamposTitulo();
            }

            gcItens.DataSource = listaItemGrid;
            gcItens.RefreshDataSource();
            gcMinimo.DataSource = listaItemGrid;
            gcMinimo.RefreshDataSource();

            PreenchaTotaisEGereParcelas();
        }

        private void GereIdFalsoParaOsItens()
        {
            for (int i = 0; i < _listaItensPedidosVenda.Count; i++)
            {
                _listaItensPedidosVenda[i].Id = i + 1;
            }
        }

        private void PreenchaTotaisEGereParcelas()
        {
            double quantidadeItensVendidos = 0;
            double totalSemDesconto = 0;
            double valorTotal = 0;
            double pesoBruto = 0;
            double pesoLiquido = 0;
            double totalDesconto = 0;
            double totalLiquido = 0;
            double totalIcmsST = 0;

            foreach (var item in _listaItensPedidosVenda)
            {
                quantidadeItensVendidos += item.Quantidade;
                totalSemDesconto += item.ValorUnitario * item.Quantidade;
                totalDesconto += item.TotalDesconto;
                valorTotal += item.ValorTotal;
                totalIcmsST += item.ValorIcmsST.GetValueOrDefault();
                pesoBruto += item.Produto.Principal.PesoBruto.GetValueOrDefault() * item.Quantidade;
                pesoLiquido += item.Produto.Principal.PesoLiquido.GetValueOrDefault() * item.Quantidade;
                totalLiquido += item.ValorTotal;
            }

            txtItensInclusosProdutos.Text = _listaItensPedidosVenda.Count.ToString();

            txtPesoBrutoProdutos.Text = pesoBruto.ToString();
            txtPesoBrutoFechamento.Text = pesoBruto.ToString();

            txtPesoLiquidoProdutos.Text = pesoLiquido.ToString();
            txtPesoLiquidoFehcamento.Text = pesoLiquido.ToString();

            txtDescontoProdutos.Text = totalDesconto.ToString("0.00");

            txtTotalGeralProdutos.Text = valorTotal.ToString("0.00");
            txtTotalItensFechamento.Text = totalSemDesconto.ToString("0.00");

            txtQuantidadeProdutosVendidosFechamento.Text = quantidadeItensVendidos.ToString();

            txtVolume.Text = quantidadeItensVendidos.ToString();

            if (!_variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento && !_variavelControleAlterandoTipoDescontoFechamento)
            {
                if (rdbDescontoTotalPercentual.Checked)
                {
                    ConvertaDescontoFechamentoParaPercentual(totalDesconto);
                }
                else
                {
                    txtDescontoFechamento.Text = totalDesconto.ToString("0.00");
                }
            }

            txtTotalIcmsSTFechamento.Text = totalIcmsST.ToString("#,###,##0.00");

            txtTotalVendaFechamento.Text = totalLiquido.ToString("#,###,##0.00");

            GereParcelasFinanceiro();
        }

        private void ConvertaDescontoFechamentoParaDinheiro(double percentual)
        {
            txtDescontoFechamento.Text = txtDescontoProdutos.Text.ToString();

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

        private void CalculeValoresTotaisCamposProduto()
        {
            if (string.IsNullOrEmpty(txtIdProduto.Text))
            {
                return;
            }

            string ufDestino = cboEstado.EditValue != null ? cboEstado.EditValue.ToString() : string.Empty;
            EnumTipoCliente tipoCliente = (EnumTipoCliente)pnlAtendimento.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(txtIdCliente.Text.ToInt());

            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();

            var descontoTotal = CalculosPedidoDeVenda.CalculeTotalDesconto(txtValorUnitarioProduto.Text.ToDouble(),
                                                                                                              txtQuantidadeProduto.Text.ToDouble(),
                                                                                                              txtDescontoUnitario.Text.ToDouble(),
                                                                                                              rdbDescontoProdutoPercentual.Checked);

            var itemPedido = new ItemPedidoDeVenda();
            itemPedido.Produto = new Produto { Id = txtIdProduto.Text.ToInt() };
            itemPedido.ValorUnitario = txtValorUnitarioProduto.Text.ToDouble();
            itemPedido.Quantidade = txtQuantidadeProduto.Text.ToDouble();
            itemPedido.TotalDesconto = descontoTotal;
            itemPedido.ValorFrete = txtValorFreteProduto.Text.ToDouble();
            itemPedido.ValorIpi = 0;

            //Regime Simples Nacional
            if (_empresa.DadosEmpresa.CodigoRegimeTributario != EnumCodigoRegimeTributario.REGIMENORMAL)
            {
                calculosPedidoDeVenda.DefinaIcmsST(itemPedido, ufDestino, tipoCliente, tipoInscricaoIcms);
            }
            //Regime Normal
            else
            {
                calculosPedidoDeVenda.DefinaIpi(itemPedido, ufDestino, tipoCliente);
                calculosPedidoDeVenda.DefinaIcmsRegimeNormal(itemPedido, ufDestino, tipoCliente, tipoInscricaoIcms);
                calculosPedidoDeVenda.DefinaPis(itemPedido, ufDestino, tipoCliente);
                calculosPedidoDeVenda.DefinaCofins(itemPedido,ufDestino,tipoCliente);
            }
            
            double valorTotalItem = calculosPedidoDeVenda.RetorneValorTotalItem(itemPedido.ValorUnitario,
                                                                                                                   itemPedido.Quantidade,
                                                                                                                   descontoTotal,
                                                                                                                   itemPedido.ValorFrete,
                                                                                                                   itemPedido.ValorIpi,
                                                                                                                   itemPedido.ValorIcmsST);            

            txtTotalDescontoProduto.Text = descontoTotal.ToString("0.00");
            txtValorIcmsSTProduto.Text = itemPedido.ValorIcmsST != null ? itemPedido.ValorIcmsST.Value.ToString("0.00") : string.Empty;
            txtValorTotalProduto.Text = valorTotalItem.ToString("0.00");
            txtValorIpiProduto.Text = itemPedido.ValorIpi != null ? itemPedido.ValorIpi.Value.ToString("0.00") : string.Empty;
        }

        #endregion

        #region " RETORNA PEDIDO DE VENDA EM EDIÇÃO "

        private PedidoDeVenda RetornePedidoDeVendaEmEdicao()
        {
            PedidoDeVenda pedidoDeVenda = new PedidoDeVenda();

            pedidoDeVenda.Atendente = cboAtendentes.EditValue != null ? new Pessoa { Id = cboAtendentes.EditValue.ToInt() } : null;
            pedidoDeVenda.Vendedor = cboVendedores.EditValue != null ? new Pessoa { Id = cboVendedores.EditValue.ToInt() } : null;
            pedidoDeVenda.Supervisor = cboSupervisores.EditValue != null ? new Pessoa { Id = cboSupervisores.EditValue.ToInt() } : null;
            pedidoDeVenda.Indicador = cboIndicadores.EditValue != null ? new Pessoa { Id = cboIndicadores.EditValue.ToInt() } : null;

            pedidoDeVenda.Cliente = !string.IsNullOrEmpty(txtIdCliente.Text) ? new Pessoa { Id = txtIdCliente.Text.ToInt() } : null;

            pedidoDeVenda.Usuario = Sessao.PessoaLogada;

            pedidoDeVenda.EnderecoPedidoDeVenda.CEP = txtCepEndereco.Text;
            pedidoDeVenda.EnderecoPedidoDeVenda.Bairro = txtBairro.Text;
            pedidoDeVenda.EnderecoPedidoDeVenda.Rua = txtRua.Text;

            pedidoDeVenda.EnderecoPedidoDeVenda.Cidade = cboCidade.EditValue != null ? _listaCidadesCbo.FirstOrDefault(x => x.Id == cboCidade.EditValue.ToInt()) : null;

            pedidoDeVenda.EnderecoPedidoDeVenda.Complemento = txtComplementoEndereco.Text;

            pedidoDeVenda.EnderecoPedidoDeVenda.Numero = txtNumero.Text;
            pedidoDeVenda.EnderecoPedidoDeVenda.TipoEndereco = (EnumTipoEndereco?)cboTipoEndereco.EditValue;

            pedidoDeVenda.TipoCliente = (EnumTipoCliente)pnlAtendimento.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            pedidoDeVenda.CondicaoPagamento = cboCondicaoPagamento.EditValue != null ? new CondicaoPagamento { Id = cboCondicaoPagamento.EditValue.ToInt() } : null;
            
            if (orcamento == false)
            {
                pedidoDeVenda.DataElaboracao = txtDataElaboracao.Text.ToDate();
            }
            else
            {
                pedidoDeVenda.DataElaboracao = DateTime.Now.Date;
            }
            pedidoDeVenda.DataPrevisaoEntrega = txtDataPrevisaoEntrega.Text.ToDateNullabel();

            pedidoDeVenda.DataMontagem = txtDataMontagem.Text.ToDateNullabel();

            pedidoDeVenda.DataDesmontagem = txtDataDesmontagem.Text.ToDateNullabel();

            pedidoDeVenda.FormaPagamento = cboFormaPagamento.EditValue != null ? new FormaPagamento { Id = cboFormaPagamento.EditValue.ToInt() } : null;
            pedidoDeVenda.Id = txtId.Text.ToInt();

            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario();
            var analiseCredito = servicoAnaliseCredito.Consulte(txtIdCliente.Text.ToInt());
            analiseCredito = analiseCredito ?? new Crediario();

            ServicoContasReceber servicoContasReceber = new ServicoContasReceber(false, false);
            var listaContasAReceber = servicoContasReceber.ConsulteListaAberto(pedidoDeVenda.Cliente);

            pedidoDeVenda.LimiteDeCredito = analiseCredito.ValorLimiteCredito;
            pedidoDeVenda.AReceberAberto = listaContasAReceber.Sum(x => x.ValorTotal);
            pedidoDeVenda.SaldoDisponivel = pedidoDeVenda.LimiteDeCredito - pedidoDeVenda.AReceberAberto;
            pedidoDeVenda.MaiorCompra = txtMaiorCompra.Text.ToDouble();

            pedidoDeVenda.ObservacoesGeraisVenda = txtObservacoesGeraisVenda.Text;
            pedidoDeVenda.ObservacoesNotaFiscal = txtObservacoesNotaFiscal.Text;
            pedidoDeVenda.TabelaPreco = cboTabelaPrecos.EditValue != null ? new TabelaPreco { Id = cboTabelaPrecos.EditValue.ToInt() } : null;
            pedidoDeVenda.TipoFrete = (EnumTipoFrete)grpFrete.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();

            pedidoDeVenda.TipoPedidoVenda = (EnumTipoPedidoDeVenda?)cboTipoDocumento.EditValue;

            pedidoDeVenda.Transportadora = cboTransportadoras.EditValue != null ? new Pessoa { Id = cboTransportadoras.EditValue.ToInt() } : null;

            pedidoDeVenda.ValorFrete = txtFreteFechamento.Text.ToDouble();

            pedidoDeVenda.Volume = txtVolume.Text.ToInt();

            pedidoDeVenda.Desconto = txtDescontoFechamento.Text.ToDouble();
            pedidoDeVenda.DescontoEhPercentual = rdbDescontoTotalPercentual.Checked;
            pedidoDeVenda.ValorIcmsST = txtTotalIcmsSTFechamento.Text.ToDouble();
            pedidoDeVenda.ValorTotal = txtTotalVendaFechamento.Text.ToDouble();
            pedidoDeVenda.EstahPago = false;

            pedidoDeVenda.ListaItens = _listaItensPedidosVenda;
            pedidoDeVenda.ListaParcelasPedidoDeVenda = _listaParcelasPedidoDeVenda;
            
            
            return pedidoDeVenda;
        }

        #endregion

        #region " CALCULO RATEIO DESCONTO "

        private void RecalculeValoresItens()
        {
            if (_variavelControleEditandoOuLimpandoPedido)
            {
                return;
            }

            string ufDestino = cboEstado.EditValue != null ? cboEstado.EditValue.ToString() : string.Empty;
            EnumTipoCliente tipoCliente = (EnumTipoCliente)pnlAtendimento.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Tag.ToInt();
            var tipoInscricaoIcms = new PessoaMetodosAuxiliares().RetorneTipoInscricaoIcms(txtIdCliente.Text.ToInt());

            CalculosPedidoDeVenda calculosPedidoDeVenda = new CalculosPedidoDeVenda();
            calculosPedidoDeVenda.RecalculeValoresItens(_variavelControleAlterandoTipoDescontoFechamento || _variavelControleConteudoAlteradoDiretoNoCampoDescontoFechamento,
                                                                                 rdbDescontoTotalPercentual.Checked,
                                                                                 txtDescontoFechamento.Text.ToDouble(),
                                                                                 txtFreteFechamento.Text.ToDouble(),
                                                                                 _listaItensPedidosVenda,
                                                                                 ufDestino,
                                                                                 tipoCliente,
                                                                                 tipoInscricaoIcms);

            PreenchaGridItens();
        }

        #endregion

        #region " EDIÇÃO PEDIDO DE VENDA "

        public void PreenchaPedidoDeVenda(PedidoDeVenda pedidoDeVenda, bool exibirMensagemDeNaoEncontrado = false)
        {
            _variavelControleEditandoOuLimpandoPedido = true;
            Alterar = true;
            btnSalvar.Visible = true;
            btnCancelarVenda.Visible = true;            
            btnFecharVenda.Visible = true;

            txtDisponibilidadeItem.ForeColor = Color.Black;
            txtDisponibilidadeItem.Text = string.Empty;

            pnlAtendimento.Enabled = true;
            pnlItens.Enabled = true;
            pnlFechamento.Enabled = true;
            pnlFinanceiro.Enabled = true;

            cboTipoDocumento.Properties.ReadOnly = false;
            txtcashback.Text = "0,00";

            if (pedidoDeVenda != null)
            {
                pedidoDeVenda.Cliente.CarregueLazyLoad();
                pedidoDeVenda.Usuario.CarregueLazyLoad();
                pedidoDeVenda.Vendedor.CarregueLazyLoad();
                pedidoDeVenda.Indicador.CarregueLazyLoad();
                pedidoDeVenda.Atendente.CarregueLazyLoad();
                pedidoDeVenda.Supervisor.CarregueLazyLoad();

                pedidoDeVenda.Transportadora.CarregueLazyLoad();

                pedidoDeVenda.ListaItens.CarregueLazyLoad();
                pedidoDeVenda.ListaParcelasPedidoDeVenda.CarregueLazyLoad();
                orcamento = false;
                if (pedidoDeVenda.TipoPedidoVenda == EnumTipoPedidoDeVenda.ORCAMENTO)
                {
                    orcamento = true;
                    btnFecharVenda.Visible = false;
                }

                foreach (var item in pedidoDeVenda.ListaItens)
                {
                    item.Produto.CarregueLazyLoad();
                }

                if (ValideSePedidoEstahEmBaixa(pedidoDeVenda.StatusPedidoVenda)) 
                {
                    pnlAtendimento.Enabled = false;
                    pnlItens.Enabled = true;
                    pnlFechamento.Enabled = true;
                    pnlFinanceiro.Enabled = true;

                    btnCancelarVenda.Visible = false;
                    btnFecharVenda.Visible = false;                   
                }

                if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.CANCELADO ||
                    pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO ||
                    pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE ||
                    pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.RECUSADO)
                {
                    btnSalvar.Visible = false;
                    btnCancelarVenda.Visible = false;                   
                    btnFecharVenda.Visible = false;

                    pnlAtendimento.Enabled = false;
                    pnlItens.Enabled = false;
                    pnlFechamento.Enabled = false;

                    if(_parametros.ParametrosVenda.LimiteDiarioManha > 0 && pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE
                        && !pedidoDeVenda.EstahPago)
                    {
                        pnlFinanceiro.Enabled = true;
                        pnlFechamento.Enabled = true;
                        btnSalvar.Visible = true;
                    }
                    else
                        pnlFinanceiro.Enabled = false;


                    cboTipoDocumento.Properties.ReadOnly = true;
                }
                else if (pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMLIBERACAO ||
                           pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.RESERVADO ||
                           pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.ORCAMENTO)
                {
                    btnFecharVenda.Visible = false;
                }

                if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                {
                    if(pedidoDeVenda.StatusRoteiro == EnumStatusRoteiro.EMAGENDA || pedidoDeVenda.StatusRoteiro == null)
                        btnAgendar.Visible = true;

                    tblImpressao.Visible = true;
                }

                btnImprimir.Visible = true;
                
                if(!string.IsNullOrEmpty(_parametros.ParametrosVenda.NomeContrato))
                {
                    btnContrato.Visible = true;
                    lblDataDesmontagem.Visible = true;
                    lblDataMotagem.Visible = true;
                    txtDataMontagem.Visible = true;
                    txtDataDesmontagem.Visible = true;
                    txtObservacoesNotaFiscal.Width = 336;
                }

                txtId.Properties.ReadOnly = true;
               
            }
            else
            {
                txtId.Properties.ReadOnly = false;
                btnCancelarVenda.Visible = false;
                btnFecharVenda.Visible = false;

                if (exibirMensagemDeNaoEncontrado)
                {
                    MessageBox.Show("Pedido de Venda nao encontrado!", "Pedido de Venda não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Focus();
                }

                btnImprimir.Visible = false;
                btnContrato.Visible = false;
                btnAgendar.Visible = false;
                tblImpressao.Visible = false;

                lblFinanceiro.Text = "<<Nenhum>>";
                lblFinanceiro.ForeColor = Color.Black;

                txtNotaFiscal.Text = string.Empty;               
            }

            PreenchaTopoEdicao(pedidoDeVenda);
            PreenchaAtendimentoEdicao(pedidoDeVenda);
            PreenchaItensEdicao(pedidoDeVenda);
            PreenchaFechamentoEdicao(pedidoDeVenda);
            PreenchaFinanceiroEdicao(pedidoDeVenda);

            _variavelControleEditandoOuLimpandoPedido = false;

            CarregaStatusRoteiro(pedidoDeVenda);

            CarregaStatusFinanceiro(pedidoDeVenda);

            //CarregaVendas();
            
            if (pedidoDeVenda != null)
            {
                if(_parametros.ParametrosVenda.LimiteDiarioManha > 0)
                {
                    //Roteiro
                   
                    CarregaRoteiroPedido(pedidoDeVenda.Id);
                   
                }                

                //Numero da Nota, se tiver
                CarregaNumeroNotaFiscal(pedidoDeVenda);
               //Andre 
                CarregaItensBaixaPedido(pedidoDeVenda.Id);

                ServicoCaixa servicoCaixa = new ServicoCaixa();

                var caixa = servicoCaixa.Consulte( pedidoDeVenda.Id);

                ConsultaMovCaixaItens(pedidoDeVenda.Id);

                CarregaVendas();

            }
            else
            {
                gcRoteiros.DataSource = null;
            }            
        }
        private void ConsultaMovCaixaItens(int Numero)
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
                string Sql = "SELECT movcaixa_usuario_abertura, pes_razao, movcaixa_caixa, itemcaixa_id FROM movimentacoescaixasitens" + 
                                " Inner Join movimentacoescaixa ON " +
                                " movimentacoescaixasitens.itemcaixa_movimentacao_caixa = movimentacoescaixa.movcaixa_id " +
                                " Inner join pessoas ON " +
                                " movimentacoescaixa.movcaixa_usuario_abertura = pessoas.pes_id " +
                                " where ITEMCAIXA_NUMERO_DOCUMENTO_ORIGEM = " + Numero ;

                MySqlCommand MyCommand = new MySqlCommand(Sql, conn);
                MySqlDataReader MyReader2;


                var returnValue = MyCommand.ExecuteReader();
                while (returnValue.Read())
                {
                    txtUsuarioCaixa.Text = returnValue["movcaixa_usuario_abertura"].ToString() +  " - " + returnValue["pes_razao"].ToString();
                    txtIdCaixa.Text = returnValue["movcaixa_caixa"].ToString();
                    txtRegistroCaixa.Text = returnValue["itemcaixa_id"].ToString();
                }

            }



        }
        //private void PreenchaCamposCaixa(Caixa caixa)
        //{
        //    _caixa = caixa;

        //    if (caixa != null)
        //    {
        //        txtIdCaixa.Text = caixa.Id.ToString();
        //        txtNomeCaixa.Text = caixa.NomeCaixa;
        //        txtUsuarioCaixa.Text = caixa.Funcionario.Id + " - " + caixa.Funcionario.DadosGerais.Razao;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Este usuário não contém um caixa cadastrado!", "Não existe caixa para este usuário");

        //        this.FecharFormulario();
        //    }
        //}

        private void CarregaNumeroNotaFiscal(PedidoDeVenda pedido)
        {
            //if(pedido.StatusPedidoVenda == EnumStatusPedidoDeVenda.EMITIDONFE)
            //{
                var pedidoEncontrado = new ServicoNotaFiscal().ConsulteListaDocumentos(pedido.Id, null, null, EnumTipoDocumento.PEDIDODEVENDAS,
                                                                                       EnumStatusNotaFiscal.AUTORIZADA, null,
                                                                                       EnumTipoDeEmissaoPesquisa.NORMAL, null)
                                                                                       .FirstOrDefault();

                txtNotaFiscal.Text = pedidoEncontrado != null? pedidoEncontrado.IdentificacaoNotaFiscal.NumeroNota.ToString() : string.Empty;

                
            //}
        }

        private void CarregaRoteiroPedido(int pedidoId)
        {

            ServicoEmpresa servicoEmpresa = new ServicoEmpresa();
            _empresa = servicoEmpresa.ConsulteUltimaEmpresa();
            //if (_empresa.DadosEmpresa.Cnpj != "28.879.049/0001-85")
            //{
                var roteiro = new ServicoRoteiro().ConsultePorPedido(pedidoId);


                if (roteiro != null)
                    _listaDeRoteiros = new ServicoRoteirizacao().ConsulteListaCodigoRoteiro(roteiro.RoteirizacaoId);

                preencherGrid(roteiro);
            //}
 
        }


        private void CarregaItensBaixaPedido(int pedidoId)
        {
            var baixaItens = new ServicoMovimentacao().ConsulteListaItensSaidaPorPedido(pedidoId);

            if (baixaItens != null)
                baixaItens = new ServicoMovimentacao().ConsulteListaItensSaidaPorPedido(pedidoId);

            // PreenchaGridBaixas(baixa);

            //restaurar Andre


            List<BaixaItens> movimentacaoItensGrid = new List<BaixaItens>();

            foreach (var baixaitem in baixaItens)
            {
                BaixaItens itemBaixaGrid = new BaixaItens();


                itemBaixaGrid.IdProduto = baixaitem.Produto.Id;
                itemBaixaGrid.Nome = baixaitem.Produto.DadosGerais.Descricao;
                itemBaixaGrid.DataBaixa = baixaitem.MovimentacaoBase.DataMovimentacao.ToString();
                itemBaixaGrid.Movimentacao = baixaitem.MovimentacaoBase.TipoMovimentacao.ToString();

                movimentacaoItensGrid.Add(itemBaixaGrid);
            }

            gcBaixas.DataSource = movimentacaoItensGrid;
            gcBaixas.RefreshDataSource();
           
        }
        private void CarregaVendas()
        {

            ServicoPessoa servicoPessoa = new ServicoPessoa();
            var cliente = servicoPessoa.ConsultePessoaAtiva(txtIdCliente.Text.ToInt());

           // PreenchaCliente(cliente, true);
           
            DateTime dataInicial = new DateTime(2000, 01, 01);
            DateTime dataFinal = DateTime.Now;

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();
            PedidoDeVendaCliente pedidoDeVendaRelatorio = new PedidoDeVendaCliente();

            List<VWVenda> listaVWVendaII = servicoPedidoDeVenda.ConsulteListaVWVendasPorCliente(cliente,
                                                                                                                                                null,
                                                                                                                                           null,
                                                                                                                                                false,
                                                                                                                                                dataInicial,
                                                                                                                                                dataFinal,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                false,
                                                                                                                                                true,
                                                                                                                                                true,
                                                                                                                                                0,
                                                                                                                                                null);







            List<PedidoDeVenda> movimentacaoItensGrid = new List<PedidoDeVenda>();
            foreach (var listasvendasII in listaVWVendaII)
            {
                PedidoDeVenda vendas = new PedidoDeVenda();


                vendas.Id = listasvendasII.Id;
                vendas.DataElaboracao = listasvendasII.DataElaboracao;
                vendas.ValorTotalII = "R$" + listasvendasII.ValorTotal.ToString("0.00");
                if (listasvendasII.VendedorId != 0)
                {
                    vendas.VendedorNome = listasvendasII.VendedorNome.ToString();
                }

                movimentacaoItensGrid.Add(vendas);
            }

            gcVendas.DataSource = movimentacaoItensGrid;
            gcVendas.RefreshDataSource();
            _listaTmkGridII = movimentacaoItensGrid;
        }
       
        private void PreenchaCamposCaixa(Caixa caixa)
        {
            _caixa = caixa;

            if (caixa != null)
            {
                txtIdCaixa.Text = caixa.Id.ToString();
          
                //txtUsuarioCaixa.Text = caixa.Funcionario.Id + " - " + txtUsuarioCaixa.Text;
            }
            //else
            //{
            //    MessageBox.Show("Este usuário não contém um caixa cadastrado!", "Não existe caixa para este usuário");

            //    this.FecharFormulario();
            //}
        }

        private void PreenchaGridBaixas(List<BaixaItens> baixaitens)
        {
            throw new NotImplementedException();
        }

        private void CarregaStatusRoteiro(PedidoDeVenda pedido)
        {
            if (pedido != null)
            {

                Pessoa usuarioAtual = new Pessoa { Id = Sessao.PessoaLogada.Id };

                btnSalvar.Enabled = true;
                btnFecharVenda.Enabled = true;
                btnCancelarVenda.Enabled = true;

                btnAgendar.Visible = true;
                btnDuplicar.Visible = true;

                if (pedido.StatusRoteiro == EnumStatusRoteiro.EMROTA)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.EMROTA.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Green;
                    if (strEmpresa != "SHOPPING")
                    {
                        if (Sessao.GrupoAcesso.Id == 4 || Sessao.GrupoAcesso.Id == 5)
                        {
                            btnSalvar.Enabled = false;
                            btnFecharVenda.Enabled = false;
                            btnCancelarVenda.Enabled = false;
                            btnAgendar.Visible = false;
                            btnDuplicar.Visible = false;
                        }
                    }
                }
                else if (pedido.StatusRoteiro == EnumStatusRoteiro.CONCLUIDO)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.CONCLUIDO.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Blue;
                    if (strEmpresa != "SHOPPING")
                    {
                        if ( Sessao.GrupoAcesso.Id == 4 || Sessao.GrupoAcesso.Id == 5)
                        {
                            btnSalvar.Enabled = false;
                            btnFecharVenda.Enabled = false;
                            btnCancelarVenda.Enabled = false;
                            btnAgendar.Visible = false;
                            btnDuplicar.Visible = false;
                        }
                    }
                }
                else if (pedido.StatusRoteiro == EnumStatusRoteiro.INCONCLUSO)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.INCONCLUSO.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Red;
                    if (strEmpresa != "SHOPPING")
                    {
                        if (Sessao.GrupoAcesso.Id == 4 || Sessao.GrupoAcesso.Id == 5)
                        {
                            btnSalvar.Enabled = false;
                            btnFecharVenda.Enabled = false;
                            btnCancelarVenda.Enabled = false;
                            btnAgendar.Visible = false;
                            btnDuplicar.Visible = false;
                        }
                    }
                }
                else if (pedido.StatusRoteiro == EnumStatusRoteiro.EMAGENDA)
                {
                    lblStatusRoteiro.Text = EnumStatusRoteiro.EMAGENDA.Descricao();
                    lblStatusRoteiro.ForeColor = Color.Black;
                    
                }
                else
                {
                    lblStatusRoteiro.Text = "<<Nenhum>>";
                    lblStatusRoteiro.ForeColor = Color.Brown;
                   
                }



            }
        }

        private void CarregaStatusFinanceiro(PedidoDeVenda pedido)
        {
            if (_parametros.ParametrosFiscais.EmitirNotaSemReceber)
            {
                lblFinanceiro.Visible = true;
                lblLabelFinanceiro.Visible = true;

                if(pedido != null)
                {
                    if (pedido.EstahPago==true)
                    {
                        lblFinanceiro.Text = "PAGO";
                        lblFinanceiro.ForeColor = Color.Blue;
                    }
                    else if(pedido.EstahPago==false)
                    {
                        lblFinanceiro.Text = "NÃO PAGO";
                        lblFinanceiro.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblFinanceiro.Text = "<<Nenhum>>";
                        lblFinanceiro.ForeColor = Color.Black;
                    }
                    
                }
            }
        }

        private void PreenchaTopoEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
                if(_parametros.ParametrosVenda.LimiteDiarioManha == 0) 
                    txtSituacao.Text = pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.FATURADO? "FATURADO": pedidoDeVenda.StatusPedidoVenda.Descricao();
                else
                    txtSituacao.Text = pedidoDeVenda.StatusPedidoVenda.Descricao();

                cboTipoDocumento.EditValue = pedidoDeVenda.TipoPedidoVenda;
                txtId.Text = pedidoDeVenda.Id.ToString();
                txtUsuario.Text = pedidoDeVenda.Usuario.Id + " - " + pedidoDeVenda.Usuario.DadosGerais.Razao;
                txtDataElaboracao.Text = pedidoDeVenda.DataElaboracao.ToString("dd/MM/yyyy");

                bool preencherAReceberAbertoSaldoDisponivelMaiorVenda = pedidoDeVenda.StatusPedidoVenda == EnumStatusPedidoDeVenda.ABERTO;

                PreenchaCliente(pedidoDeVenda.Cliente,
                                        preencherEndereco: false,
                                        preencherEmpresa: true,
                                        preencherIndicador: false,
                                        preencherAtendente: false,
                                        preencherVendedor: false,
                                        preencherSupervisor: false,
                                        preencherTabelaPreco: false,
                                        preencherFormaPagamento: false,
                                        preencherCondicaoPagamento: false,
                                        preencherAReceberAberto: preencherAReceberAbertoSaldoDisponivelMaiorVenda,
                                        preencherSaldoDisponivel: preencherAReceberAbertoSaldoDisponivelMaiorVenda,
                                        preencherMaiorVenda: preencherAReceberAbertoSaldoDisponivelMaiorVenda);
            }
            else
            {
                cboTipoDocumento.EditValue = null;
                txtId.Text = string.Empty;
                txtUsuario.Text = Sessao.PessoaLogada.Id + " - " + Sessao.PessoaLogada.DadosGerais.Razao;
                txtDataElaboracao.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtSituacao.Text = EnumStatusPedidoDeVenda.ABERTO.Descricao();

                PreenchaCliente(null);
            }
        }

        private void PreenchaAtendimentoEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
                cboIndicadores.EditValue = pedidoDeVenda.Indicador != null ? (int?)pedidoDeVenda.Indicador.Id : null;
                cboAtendentes.EditValue = pedidoDeVenda.Atendente != null ? (int?)pedidoDeVenda.Atendente.Id : null;
                cboVendedores.EditValue = pedidoDeVenda.Vendedor != null ? (int?)pedidoDeVenda.Vendedor.Id : null;
                cboSupervisores.EditValue = pedidoDeVenda.Supervisor != null ? (int?)pedidoDeVenda.Supervisor.Id : null;

                cboTabelaPrecos.EditValue = pedidoDeVenda.TabelaPreco != null ? (int?)pedidoDeVenda.TabelaPreco.Id : null;
                cboTipoEndereco.EditValue = pedidoDeVenda.EnderecoPedidoDeVenda.TipoEndereco;

                txtCepEndereco.Text = pedidoDeVenda.EnderecoPedidoDeVenda.CEP;

                cboEstado.EditValue = pedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Estado.UF;
                cboCidade.EditValue = pedidoDeVenda.EnderecoPedidoDeVenda.Cidade.Id;

                txtBairro.Text = pedidoDeVenda.EnderecoPedidoDeVenda.Bairro;
                txtRua.Text = pedidoDeVenda.EnderecoPedidoDeVenda.Rua;
                txtNumero.Text = pedidoDeVenda.EnderecoPedidoDeVenda.Numero;
                txtComplementoEndereco.Text = pedidoDeVenda.EnderecoPedidoDeVenda.Complemento;

                pnlAtendimento.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)pedidoDeVenda.TipoCliente).Checked = true;
            }
        }

        private void PreenchaItensEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
                _listaItensPedidosVenda = pedidoDeVenda.ListaItens.ToList();
            }
            else
            {
                _listaItensPedidosVenda.Clear();
            }

            PreenchaGridItens();
        }

        private void PreenchaFechamentoEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null)
            {
                cboFormaPagamento.EditValue = pedidoDeVenda.FormaPagamento != null ? (int?)pedidoDeVenda.FormaPagamento.Id : null;
                cboCondicaoPagamento.EditValue = pedidoDeVenda.CondicaoPagamento != null ? (int?)pedidoDeVenda.CondicaoPagamento.Id : null;

                cboTransportadoras.EditValue = pedidoDeVenda.Transportadora != null ? (int?)pedidoDeVenda.Transportadora.Id : null;

                grpFrete.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)pedidoDeVenda.TipoFrete).Checked = true;

                txtDataPrevisaoEntrega.Text = pedidoDeVenda.DataPrevisaoEntrega != null ? pedidoDeVenda.DataPrevisaoEntrega.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtDataMontagem.Text = pedidoDeVenda.DataMontagem != null ? pedidoDeVenda.DataMontagem.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtDataDesmontagem.Text = pedidoDeVenda.DataDesmontagem != null ? pedidoDeVenda.DataDesmontagem.Value.ToString("dd/MM/yyyy") : string.Empty;

                txtFreteFechamento.Text = pedidoDeVenda.ValorFrete.ToString("0.00");

                txtItensInclusosProdutos.Text = _listaItensPedidosVenda.Count.ToString();

                txtVolume.Text = pedidoDeVenda.Volume.ToString();

                rdbDescontoTotalValor.Checked = true;
                rdbDescontoTotalPercentual.Checked = pedidoDeVenda.DescontoEhPercentual;

                txtDescontoFechamento.Text = pedidoDeVenda.Desconto.ToString("0.00");

                txtTotalIcmsSTFechamento.Text = pedidoDeVenda.ValorIcmsST.ToString("0.00");

                txtTotalVendaFechamento.Text = pedidoDeVenda.ValorTotal.ToString("0.00");

                txtObservacoesGeraisVenda.Text = pedidoDeVenda.ObservacoesGeraisVenda;
                txtObservacoesNotaFiscal.Text = pedidoDeVenda.ObservacoesNotaFiscal;

                if (pedidoDeVenda.StatusPedidoVenda != EnumStatusPedidoDeVenda.ABERTO)
                {
                    txtLimiteCredito.Text = pedidoDeVenda.LimiteDeCredito.ToString("0.00");
                    txtSaldoDisponivel.Text = pedidoDeVenda.SaldoDisponivel.ToString("0.00");
                    txtAReceberAberto.Text = pedidoDeVenda.AReceberAberto.ToString("0.00");
                    txtMaiorCompra.Text = pedidoDeVenda.MaiorCompra.ToString("0.00");
                }
            }
            else
            {
                cboFormaPagamento.EditValue = null;
                cboCondicaoPagamento.EditValue = null;

                cboTransportadoras.EditValue = null;

                grpFrete.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Tag.ToInt() == (int)EnumTipoFrete.SEMCOBRANCADEFRETE).Checked = true;

                txtDataPrevisaoEntrega.Text = string.Empty;

                txtDataMontagem.Text = string.Empty;

                txtDataDesmontagem.Text = string.Empty;

                txtFreteFechamento.Text = string.Empty;

                rdbDescontoTotalValor.Checked = true;

                txtDescontoFechamento.Text = string.Empty;

                txtTotalIcmsSTFechamento.Text = string.Empty;
                txtTotalVendaFechamento.Text = string.Empty;

                txtObservacoesGeraisVenda.Text = _parametros.ParametrosVenda.ObservacoesVendaRapida;
                txtObservacoesNotaFiscal.Text = string.Empty;

                txtLimiteCredito.Text = string.Empty;
                txtSaldoDisponivel.Text = string.Empty;
                txtAReceberAberto.Text = string.Empty;
                txtMaiorCompra.Text = string.Empty;
            }
        }

        private void PreenchaFinanceiroEdicao(PedidoDeVenda pedidoDeVenda)
        {
            if (pedidoDeVenda != null && pedidoDeVenda.ListaParcelasPedidoDeVenda != null)
            {
                _listaParcelasPedidoDeVenda = pedidoDeVenda.ListaParcelasPedidoDeVenda.ToList();
            }
            else
            {
                _listaParcelasPedidoDeVenda.Clear();
            }

            PreenchaGridParcelasFinanceiro();
        }

        private void LimpePedidoDeVenda()
        {
            PreenchaPedidoDeVenda(null);
            Alterar = false;
            orcamento = false;


            txtId.Focus();
        }

        #endregion

        #region " FINANCEIRO "

        private void EditeParcelaFinanceiro()
        {
            if (_listaParcelasPedidoDeVenda != null && _listaParcelasPedidoDeVenda.Count > 0)
            {
                var parcela = _listaParcelasPedidoDeVenda.FirstOrDefault(item => item.Id == colunaFinanceiroId.View.GetFocusedRowCellValue(colunaFinanceiroId).ToInt());

                PreenchaCamposParcela(parcela);
            }
        }

        private void PreenchaCamposParcela(ParcelaPedidoDeVenda parcelaPedidoDeVenda)
        {
            _parcelaPedidoDeVendaEmEdicao = parcelaPedidoDeVenda;

            if (parcelaPedidoDeVenda != null)
            {
                txtParcelaFinanceiro.Text = parcelaPedidoDeVenda.Parcela;
                txtNumeroDocumentoFinanceiro.Text = parcelaPedidoDeVenda.NumeroDocumento;
                txtCondicaoPagamentoFinanceiro.Text = parcelaPedidoDeVenda.CondicaoPagamento.Id + " - " + parcelaPedidoDeVenda.CondicaoPagamento.Descricao;
                cboFormaPagamentoFinanceiro.EditValue = parcelaPedidoDeVenda.FormaPagamento.Id;
                txtDataVencimento.DateTime = parcelaPedidoDeVenda.DataVencimento;
                txtValorFinanceiro.Text = parcelaPedidoDeVenda.Valor.ToString("0.00");
                cboOperadorasCredito.EditValue = parcelaPedidoDeVenda.Operadoras != null ? parcelaPedidoDeVenda.Operadoras.Id : 0;

                cboFormaPagamentoFinanceiro.Focus();
            }
            else
            {
                txtParcelaFinanceiro.Text = string.Empty;
                txtNumeroDocumentoFinanceiro.Text = string.Empty;
                txtCondicaoPagamentoFinanceiro.Text = string.Empty;
                cboFormaPagamentoFinanceiro.EditValue = null;
                txtDataVencimento.Text = string.Empty;
                txtValorFinanceiro.Text = string.Empty;
                cboOperadorasCredito.EditValue = null;
            }
        }

        private void LimpeCamposParcela()
        {
            PreenchaCamposParcela(null);

            cboFormaPagamentoFinanceiro.Focus();
        }

        private void AtualizeParcela()
        {
            if (_parcelaPedidoDeVendaEmEdicao != null)
            {
                _parcelaPedidoDeVendaEmEdicao.FormaPagamento = cboFormaPagamentoFinanceiro != null ? new FormaPagamento { Id = cboFormaPagamentoFinanceiro.EditValue.ToInt(), Descricao = cboFormaPagamentoFinanceiro.Text } : null;
                _parcelaPedidoDeVendaEmEdicao.DataVencimento = txtDataVencimento.Text.ToDate();
                _parcelaPedidoDeVendaEmEdicao.Valor = txtValorFinanceiro.Text.ToDouble();

                _parcelaPedidoDeVendaEmEdicao.Operadoras = cboOperadorasCredito.EditValue.ToInt() != 0 ? new OperadorasCartao { Id = cboOperadorasCredito.EditValue.ToInt(), Descricao = cboOperadorasCredito.Text } : null;
            }

            if (chkTodasOperadoras.Checked)
            {
                foreach (var itemParcelaOperadora in _listaParcelasPedidoDeVenda)
                {
                    if ((EnumTipoFormaPagamento)itemParcelaOperadora.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAOCREDITO)
                    {
                        AtribuaAOperadora(itemParcelaOperadora);
                    }
                    else if ((EnumTipoFormaPagamento)itemParcelaOperadora.FormaPagamento.Id == EnumTipoFormaPagamento.CARTAODEBITO)
                    {
                        AtribuaAOperadora(itemParcelaOperadora);
                    }
                }

                chkTodasOperadoras.Checked = false;
            }

            PreenchaGridParcelasFinanceiro();

            LimpeCamposParcela();
        }

        private void AtribuaAOperadora(ParcelaPedidoDeVenda parcelaOperadora)
        {
            if (cboOperadorasCredito != null)
                if (cboOperadorasCredito.EditValue != null)
                    parcelaOperadora.Operadoras = new OperadorasCartao { Id = cboOperadorasCredito.EditValue.ToInt(), Descricao = cboOperadorasCredito.Text };
        }


        //private void PreenchaGridBaixas(List<BaixaItens> baixaitens)
        //{
        //    List<BaixaItens> movimentacaoItensGrid = new List<BaixaItens>();

        //    foreach (var baixa in _listaDeBaixas)
        //    {
        //        BaixaItens itemBaixaGrid = new BaixaItens();

               
        //        itemBaixaGrid.IdProduto = itemBaixaGrid.IdProduto;
        //        itemBaixaGrid.Nome = itemBaixaGrid.Nome;
        //        itemBaixaGrid.DataBaixa = itemBaixaGrid.DataBaixa;

        //        movimentacaoItensGrid.Add(itemBaixaGrid);
        //    }

        //    gcBaixas.DataSource = movimentacaoItensGrid;
        //    gcBaixas.RefreshDataSource();
        //}

        private void PreenchaGridParcelasFinanceiro()
        {
            List<ParcelaGrid> listaParcelasGrid = new List<ParcelaGrid>();

            foreach (var parcela in _listaParcelasPedidoDeVenda)
            {
                ParcelaGrid parcelaGrid = new ParcelaGrid();

                parcelaGrid.CondicaoPagamento = parcela.CondicaoPagamento.Id + " - " + parcela.CondicaoPagamento.Descricao;
                parcelaGrid.DataVencimento = parcela.DataVencimento.ToString("dd/MM/yyyy");
                parcelaGrid.FormaPagamento = parcela.FormaPagamento.Id.ToString() + " - " + parcela.FormaPagamento.Descricao;
                parcelaGrid.Id = parcela.Id;
                parcelaGrid.NumeroDocumento = parcela.NumeroDocumento;
                parcelaGrid.Parcela = parcela.Parcela;
                parcelaGrid.Valor = parcela.Valor.ToString("0.00");
               
                parcelaGrid.Operadora = parcela.Operadoras != null ? parcela.Operadoras.Id.ToString() + " - " + parcela.Operadoras.Descricao : null;

                listaParcelasGrid.Add(parcelaGrid);
            }

            gcFinanceiro.DataSource = listaParcelasGrid;
            gcFinanceiro.RefreshDataSource();
        }

        private void GereParcelasFinanceiro()
        {
            _listaParcelasPedidoDeVenda.Clear();

            if (_listaItensPedidosVenda.Count > 0 && cboCondicaoPagamento.EditValue != null && cboFormaPagamento.EditValue != null)
            {
                ServicoFormaPagamento servicoFormaPagamento = new ServicoFormaPagamento();
                ServicoCondicaoPagamento servicoCondicaoPagamento = new ServicoCondicaoPagamento();

                var condicaoPagamento = servicoCondicaoPagamento.Consulte(cboCondicaoPagamento.EditValue.ToInt());
                var formaPagamento = servicoFormaPagamento.Consulte(cboFormaPagamento.EditValue.ToInt());

                int contador = 0;
                int quantidadeDeParcelas = condicaoPagamento.ListaDeParcelas.Count;
                double totalVendaFechamento = txtTotalVendaFechamento.Text.ToDouble();
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
                    
                    totalSomaParcelas += parcelaPedidoDeVenda.Valor;
                    contador++;

                    _listaParcelasPedidoDeVenda.Add(parcelaPedidoDeVenda);
                }

                var diferenca = Math.Round(totalVendaFechamento - totalSomaParcelas, 2);

                _listaParcelasPedidoDeVenda.Last().Valor += diferenca;
            }
            else
            {
                _listaParcelasPedidoDeVenda.Clear();
            }

            LimpeCamposParcela();

            PreenchaGridParcelasFinanceiro();
        }

        public bool ValideSeSalvarOuFechar()
        {
            if (_parametros.ParametrosVenda.LimiteDiarioManha > 0)
            {
                if (cboIndicadores.EditValue.ToInt() == 0)
                {
                    MessageBox.Show("É obrigatório informar o indicador para Fechar ou Salvar o Pedido.", "Fechando o Pedido",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                if (cboVendedores.EditValue.ToInt() == 0)
                {
                    MessageBox.Show("É obrigatório informar o vendedor para Fechar ou Salvar o Pedido.", "Fechando o Pedido",
                                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }

            if (txtIdCliente.Text == string.Empty || txtIdCliente.Text.ToInt() == 0)
            {
                MessageBox.Show("Para continuar, você precisa informar o Cliente.", "Fechar ou Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;                
            }

            return false;
        }

        public bool ValideSePedidoEstahEmBaixa(EnumStatusPedidoDeVenda status)
        {
            int itemEntrada = 0;
            int itemsaida = 0;

            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

            var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedido(txtId.Text.ToInt());

            if (movimentacao.Exists(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA)
                                    && status != EnumStatusPedidoDeVenda.CANCELADO && status != EnumStatusPedidoDeVenda.FATURADO)
            {

                foreach (var itemproduto in movimentacao)
                {
                    if (itemproduto.MovimentacaoBase.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA)
                    //if (movimentacao.Exists(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA))
                    {
                        itemEntrada += 1;
                    }
                    else
                    {
                        itemsaida += 1;
                    }
                    //if (movimentacao.Exists(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA))
                    //{
                    //    MessageBoxAkil.Show("Existe(m) item(s) em baixa ou baixados para este pedido. " +
                    //                 "Portanto, pode ser alterado apenas o financeiro e itens não baixados.", "Alterar Pedido de Venda", MessageBoxButtons.OK);
                    //    return true;
                    //}
                    //return false;
                }

                if (itemsaida != itemEntrada)
                {
                        MessageBoxAkil.Show("Existe(m) item(s) em baixa ou baixados para este pedido. " +
                                     "Portanto, pode ser alterado apenas o financeiro e itens não baixados.", "Alterar Pedido de Venda", MessageBoxButtons.OK);
                        return true;
                }
            }

            return false;

        }
       
        public bool ValideSeItemEstahEmBaixa(EnumStatusPedidoDeVenda status, ItemPedidoDeVenda itemEdicao)
        {
            ServicoMovimentacao servicoMovimentacao = new ServicoMovimentacao();

            var movimentacao = servicoMovimentacao.ConsulteListaItensSaidaPorPedidoEItem(txtId.Text.ToInt(), itemEdicao.Produto.Id);

            if (movimentacao.Exists(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA)
                                    && status != EnumStatusPedidoDeVenda.CANCELADO && status != EnumStatusPedidoDeVenda.FATURADO)
            {
                if (movimentacao.Exists(x => x.Produto.Id == itemEdicao.Produto.Id))
                {

   
                    var listaSaida = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.SAIDA);

                    var listaEntrada = movimentacao.FindAll(x => x.TipoMovimentacao == EnumTipoMovimentacao.ENTRADA);


                    if (listaSaida.Exists(x => x.Produto.Id == itemEdicao.Produto.Id))
                    {
                        double quantEntrada = 0;

                        if (listaEntrada.Exists(y => y.Produto.Id == itemEdicao.Produto.Id))
                        {
                            quantEntrada = listaEntrada.FindAll(x => x.Produto.Id == itemEdicao.Produto.Id).Sum(x => x.Quantidade);
                        }

                        var quantSaida = listaSaida.FindAll(x => x.Produto.Id == itemEdicao.Produto.Id).Sum(x => x.Quantidade);

                        var diferencaQtde = quantSaida - quantEntrada;

                        if (diferencaQtde > 0)
                        {
                            MessageBoxAkil.Show("Este item está baixado! " +
                            "Portanto, não pode ser alterado", "Alterar Pedido de Venda", MessageBoxButtons.OK);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        #endregion

        private bool ValidaTipoDocumento()
        {
            if (cboTipoDocumento.EditValue == null) return false;

            if ((EnumTipoPedidoDeVenda)cboTipoDocumento.EditValue != EnumTipoPedidoDeVenda.PEDIDOVENDA)
            {
                if (MessageBox.Show("Você tem certeza que quer Fechar o Pedido com este Tipo de Documento: " + cboTipoDocumento.Text + "? ", "Fechar Pedido", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return true;
                }
            }

            return false;
        }

        private void TrateUsuarioContemPermissaoAtalhos()
        {
            TrateUsuarioNaoTemPermissaoAtalho(pnlNomeCliente, txtNomeCliente, btnAtalhoCliente, EnumFuncionalidade.PESSOAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlFormaPagamento, cboFormaPagamento, btnAtalhoFormaPagamento, EnumFuncionalidade.FORMAPAGAMENTO);
            TrateUsuarioNaoTemPermissaoAtalho(pnlCondicaoPagamento, cboCondicaoPagamento, btnAtalhoCondicaoPagamento, EnumFuncionalidade.CONDICOESPAGAMENTO);

            TrateUsuarioNaoTemPermissaoAtalho(pnlIndicador, cboIndicadores, btnAtalhoIndicador, EnumFuncionalidade.PESSOAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlAtendente, cboAtendentes, btnAtalhoAtendente, EnumFuncionalidade.PESSOAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlVendedor, cboVendedores, btnAtalhoVendedor, EnumFuncionalidade.PESSOAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlSupervisor, cboSupervisores, btnAtalhoSupervisor, EnumFuncionalidade.PESSOAS);
            TrateUsuarioNaoTemPermissaoAtalho(pnlTransportadora, cboTransportadoras, btnAtalhoTransportadora, EnumFuncionalidade.PESSOAS);
        }

        private void PergunteSeDesejaImprimirPedido(int idPedidoDeVenda)
        {
            if (MessageBox.Show("Deseja imprimir o orçamento / pedido?", "Deseja imprimir", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ImprimaPedidoDeVenda(idPedidoDeVenda);
            }
        }

        private void ImprimaPedidoDeVenda(int idPedidoDeVenda)
        {
            if (_parametros.ParametrosVenda.PedidoEmDuasVias)
            {
                //if (!rdbUmaVia.Checked && !rdbDuasVias.Checked)
                //{
                //    RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(idPedidoDeVenda);
                //    relatorio.GereRelatorio();

                //    using (ReportPrintTool printTool = new ReportPrintTool(relatorio))
                //    {
                //        // Invoke the Ribbon Print Preview form modally, 
                //        // and load the report document into it.
                //        printTool.ShowRibbonPreviewDialog();

                //        // Invoke the Ribbon Print Preview form
                //        // with the specified look and feel setting.
                //        printTool.ShowRibbonPreview(UserLookAndFeel.Default);
                //    }
                //}
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
            else if (_parametros.ParametrosVenda.PedidoEmImpressoraTermica)
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
                if (!rdbUmaVia.Checked && !rdbDuasVias.Checked)
                {
                    RelatorioPedidoVenda relatorio = new RelatorioPedidoVenda(idPedidoDeVenda, (EnumTipoEndereco)cboTipoEndereco.EditValue);
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
                else if (rdbUmaVia.Checked)
                {
                    RelatorioPedidoVendaAgenda relatorio = new RelatorioPedidoVendaAgenda(idPedidoDeVenda);
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
                else
                {
                    RelatorioPedidoVendaDuasViasAgenda relatorioDuasVias = new RelatorioPedidoVendaDuasViasAgenda(idPedidoDeVenda);
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
            }

            rdbUmaVia.Checked = false;
            rdbDuasVias.Checked = false;
        }
        class PedidoAuxiliar
        {
            public int Id { get; set; }

            public string PedidoId { get; set; }

            public string DataEmissao { get; set; }

            public string Valorunitario { get; set; }

            public string Total { get; set; }
          


        }
        private void ImprimaContratoDeVenda(int idPedidoDeVenda)
        {
            RelatorioContratoVenda relatorio = new RelatorioContratoVenda(idPedidoDeVenda, _parametros);
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
        
        private void CarregueAnaliseCredito()
        {
            ServicoCrediario servicoAnaliseCredito = new ServicoCrediario(false, false);
            _crediario = servicoAnaliseCredito.Consulte(txtIdCliente.Text.ToInt());
            if (_crediario == null)
            {
               
                PreenchaCboTabelaPreco();
                PreenchaCboFormaPagamento();
                PreenchaCboCondicaoPagamento();
            }
            else
            {
                if (_crediario.StatusAnaliseCredito == Negocio.Financeiro.Enumeradores.EnumStatusCrediario.BLOQUEADO)
                {
                    MessageBox.Show("Este cliente está bloqueado!", "Cliente bloqueado.");

                    PreenchaCliente(null);

                    txtIdCliente.Focus();
                }
                else
                {
                    _crediario.TabelaPreco.CarregueLazyLoad();
                    _crediario.FormaPagamento.CarregueLazyLoad();
                    _crediario.CondicaoPagamento.CarregueLazyLoad();

                    PreenchaCboTabelaPreco();
                    PreenchaCboFormaPagamento();
                    PreenchaCboCondicaoPagamento();
                }
            }
           
        }

        private void CarregaComboOperadorasDebitoCredito()
        {
            ServicoOperadorasCartao servicoOperadorasCartao = new ServicoOperadorasCartao();

            var operadoras = servicoOperadorasCartao.ConsulteLista();

            List<ObjetoParaComboBox> listaDebito = new List<ObjetoParaComboBox>();
            List<ObjetoParaComboBox> listaCredito = new List<ObjetoParaComboBox>();

            foreach (var item in operadoras)
            {
                ObjetoParaComboBox objeto = new ObjetoParaComboBox();

                if (!item.PermiteParcelamento)
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaDebito.Add(objeto);
                }
                else
                {
                    objeto.Descricao = item.Descricao;
                    objeto.Valor = item.Id;

                    listaCredito.Add(objeto);
                }
            }

            if (cboFormaPagamentoFinanceiro.EditValue == null) return;

            if ((EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CARTAODEBITO)
            {
                cboOperadorasCredito.Properties.DisplayMember = "Descricao";
                cboOperadorasCredito.Properties.ValueMember = "Valor";
                cboOperadorasCredito.Properties.DataSource = listaDebito;
            }
            else if ((EnumTipoFormaPagamento)cboFormaPagamentoFinanceiro.EditValue == EnumTipoFormaPagamento.CARTAOCREDITO)
            {
                cboOperadorasCredito.Properties.DisplayMember = "Descricao";
                cboOperadorasCredito.Properties.ValueMember = "Valor";
                cboOperadorasCredito.Properties.DataSource = listaCredito;
            }
            else
            {
                cboOperadorasCredito.Properties.DisplayMember = "Descricao";
                cboOperadorasCredito.Properties.ValueMember = "Id";
                cboOperadorasCredito.Properties.DataSource = null;
            }
        }

        private void preencherGrid(Roteiro agenda)
        {
            List<RoteiroAuxiliar> listaDeRoteirosAuxiliar = new List<RoteiroAuxiliar>();

            if (_listaDeRoteiros == null || _listaDeRoteiros.Count == 0)
            {
                if(agenda != null)
                {
                    RoteiroAuxiliar RoteiroAuxiliar = new RoteiroAuxiliar();

                    RoteiroAuxiliar.Id = 0;
                    RoteiroAuxiliar.Funcionario = string.Empty;

                    RoteiroAuxiliar.DataCriacao = string.Empty;

                    RoteiroAuxiliar.DataConclusao = string.Empty;

                    RoteiroAuxiliar.Periodo = agenda.Periodo.Descricao();

                    RoteiroAuxiliar.Status = agenda.Status.Descricao();

                    RoteiroAuxiliar.Imagem = Properties.Resources.icons8_linha_vertical_30;

                    listaDeRoteirosAuxiliar.Add(RoteiroAuxiliar);
                    
                    gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                    gcRoteiros.RefreshDataSource();                    
                }
                else
                {
                    gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                    gcRoteiros.RefreshDataSource();                    
                }

                return;
            }

            for (int i = 0; i < _listaDeRoteiros.Count; i++)
            {
                var roteiro = _listaDeRoteiros[i];

                RoteiroAuxiliar RoteiroAuxiliar = new RoteiroAuxiliar();

                RoteiroAuxiliar.Id = roteiro.Id;
                RoteiroAuxiliar.Funcionario = roteiro.PessoaFuncionario != null ? roteiro.PessoaFuncionario.Id + " - " + roteiro.PessoaFuncionario.DadosGerais.Razao : string.Empty;

                RoteiroAuxiliar.DataCriacao = roteiro.DataCriacao.ToString("dd/MM/yyyy");
                RoteiroAuxiliar.DataConclusao = roteiro.DataConclusao != null ? roteiro.DataConclusao.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty;

                RoteiroAuxiliar.Periodo = agenda != null? agenda.Periodo.Descricao():null;

                RoteiroAuxiliar.Status = roteiro.Status.Descricao();

                if (roteiro.Status == EnumStatusRoteiro.EMROTA)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icone_verde;
                }
                else if (roteiro.Status == EnumStatusRoteiro.CONCLUIDO)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icone_azul;
                }
                else if (roteiro.Status == EnumStatusRoteiro.INCONCLUSO)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icone_vermelho;
                }
                else if (roteiro.Status == EnumStatusRoteiro.EMAGENDA)
                {
                    RoteiroAuxiliar.Imagem = Properties.Resources.icons8_linha_vertical_30;                   
                }
                else
                {
                    RoteiroAuxiliar.Imagem = null;
                }

                listaDeRoteirosAuxiliar.Add(RoteiroAuxiliar);

                gcRoteiros.DataSource = listaDeRoteirosAuxiliar;
                gcRoteiros.RefreshDataSource();
            }

        }

        #endregion

        #region " CLASSES AUXILIARES "
        private class ItemBaixaGrid
        {
            public int Id { get; set; }

            public int IdProduto { get; set; }

            public string Nome { get; set; }

            public string DataBaixa { get; set; }

            public string Serie { get; set; }
        }
        
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

            public string ValorUnitario { get; set; }

            public string Desconto { get; set; }

            public string ValorTotal { get; set; }

            public bool ItemEstahInconsistente { get; set; }
            public string QtdMinimo { get; set; }
        }

        private class ParcelaGrid
        {
            public int Id { get; set; }

            public string Parcela { get; set; }

            public string NumeroDocumento { get; set; }

            public string CondicaoPagamento { get; set; }

            public string FormaPagamento { get; set; }

            public string DataVencimento { get; set; }

            public string Valor { get; set; }

            public string Operadora { get; set; }
            public string DataPagamento { get; set; }
        }

        private class RoteiroAuxiliar
        {
            public Image Imagem { get; set; }

            public int Id { get; set; }

            public string DataCriacao { get; set; }

            public string Funcionario { get; set; }

            public string Pedido { get; set; }

            public string Usuario { get; set; }

            public string DataConclusao { get; set; }

            public string Periodo { get; set; }

            public string Status { get; set; }
        }

        #endregion

        private void txtCepEndereco_EditValueChanged(object sender, EventArgs e)
        {
            RecalculeValoresItens();
        }

        private void rdbRevenda_CheckedChanged(object sender, EventArgs e)
        {
            RecalculeValoresItens();
        }

        private void cboEstado_EditValueChanged(object sender, EventArgs e)
        {
            PreenchaCboCidades();

            cboCidade.EditValue = null;
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja duplicar o orçamento / pedido?", "Duplicar Pedido", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
                var pedidoDeVendaEmEdicao = RetornePedidoDeVendaEmEdicao();

            ServicoPedidoDeVenda servicoPedidoDeVenda = new ServicoPedidoDeVenda();

            if (pedidoDeVendaEmEdicao.Id != 0)
            {
                Action actionSalvar = () =>
                {

                    

                    if ((EnumTipoPedidoDeVenda)cboTipoDocumento.EditValue == EnumTipoPedidoDeVenda.PEDIDOVENDA)
                    {

                        ServicoPedidoDeVenda servicoPedido = new ServicoPedidoDeVenda();
                        bool ReserveEstoqueAoFaturarPedido = _parametros.ParametrosVenda.ReserveEstoqueAoFaturarPedido;


                        foreach (var itemproduto in _listaItensPedidosVenda)
                        {
                            if (servicoPedido.VerifiqueItemQuantidadeEstoqueNegativo(itemproduto.Quantidade, itemproduto.Produto, ReserveEstoqueAoFaturarPedido))
                            {
                                MessageBox.Show("O Estoque do(s) seguinte(s) item(s): " + itemproduto.Produto.Id.ToString() + " - " + itemproduto.Produto.DadosGerais.Descricao.ToString() + " . Está ou ficará menor que zero!", "Não é permitido salvar a venda!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                throw new Exception("Não foi permitido salvar a venda! Corrija seu estoque!");

                            }
                        }

                    }


                    pedidoDeVendaEmEdicao.Id = 0;
                    pedidoDeVendaEmEdicao.DataElaboracao = DateTime.Now;

                    servicoPedidoDeVenda.Cadastre(pedidoDeVendaEmEdicao);

                    MessageBox.Show("Número do pedido " + pedidoDeVendaEmEdicao.Id + ".");

                    LimpePedidoDeVenda();

                    PreenchaPedidoDeVenda(pedidoDeVendaEmEdicao);

                    PergunteSeDesejaImprimirPedido(pedidoDeVendaEmEdicao.Id);
                };

                TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar, mensagemDeSucesso: "Pedido duplicado com sucesso.");
            }
        }
        
        private void txtVolume_KeyDown(object sender, KeyEventArgs e)
        {
            _variavelControleAlterandoVolume = true;
        }

        private void txtVolume_EditValueChanged(object sender, EventArgs e)
        {
            if (_variavelControleAlterandoVolume)
            {
                _variavelControleAlterandoVolume = false;
            }
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            ImprimaContratoDeVenda(txtId.Text.ToInt());
        }

        private void cboTipoEndereco_EditValueChanged(object sender, EventArgs e)
        {
            if (cboTipoEndereco.Enabled == true && cboTipoEndereco.EditValue != null )
            {
                var cliente = new ServicoPessoa().ConsulteClienteAtivo(_clienteSelecionado.Id);

                if (cliente != null)
                {
                    if (cliente.ListaDeEnderecos != null)
                    {
                        PreenchaDadosClienteEndereco(cliente, (EnumTipoEndereco)cboTipoEndereco.EditValue);
                    }
                }
            }
        }

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            FormCadastroAgenda formAgenda = new FormCadastroAgenda(_clienteSelecionado, txtId.Text.ToInt());

            formAgenda.ShowDialog();

            BusqueECarreguePedido();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
                CarregaRoteiroPedido(txtId.Text.ToInt());
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_listaDeRoteiros == null ||_listaDeRoteiros.Count == 0) return;

            var roteiroSelecionado = _listaDeRoteiros.FirstOrDefault();

            FormCadastroRoteirizacao formCadastroRoteiros = new FormCadastroRoteirizacao(roteiroSelecionado.Id, roteiroSelecionado.DataCriacao,
                                                                               roteiroSelecionado.DataConclusao, roteiroSelecionado.PessoaFuncionario);
            formCadastroRoteiros.ShowDialog();
            
        }

        private void gcRoteiros_DoubleClick(object sender, EventArgs e)
        {
            if (_listaDeRoteiros == null || _listaDeRoteiros.Count == 0) return;

            var roteiroSelecionado = _listaDeRoteiros.FirstOrDefault();

            if (roteiroSelecionado.Status != EnumStatusRoteiro.CONCLUIDO)
            {
                FormCadastroRoteirizacao formCadastroRoteiros = new FormCadastroRoteirizacao(roteiroSelecionado.Id, roteiroSelecionado.DataCriacao,
                                                                                  roteiroSelecionado.DataConclusao, roteiroSelecionado.PessoaFuncionario);
                formCadastroRoteiros.ShowDialog();
            }
        }

        private void btnExcluirRoteiro_Click(object sender, EventArgs e)
        {
            if (_listaDeRoteiros == null || _listaDeRoteiros.Count == 0) return;

            var roteiroSelecionado = _listaDeRoteiros.FirstOrDefault();

            if (roteiroSelecionado.Status == EnumStatusRoteiro.CONCLUIDO)
            {
                MessageBox.Show("Roteiro Concluído não pode ser excluído por aqui.", "Excluir Roteiro",
                                                                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("O Roteiro selecionado será Excluído(s).\n\nDeseja continuar?", "Excluir Roteiro",
                                               MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            try
            {
                ServicoRoteirizacao servicoRoteirizacao = new ServicoRoteirizacao();

                servicoRoteirizacao.ExcluirRoteirizacaoEAtualizarAgendaEPedido(roteiroSelecionado.Id);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Roteiro Excluído com sucesso!", "Exclusão de Roteiro(s)", MessageBoxButtons.OK, MessageBoxIcon.Information);

            BusqueECarreguePedido();
        }

        private void gcItens_Click(object sender, EventArgs e)
        {

        }

        private void txtId_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtIdCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gcVendas_DoubleClick(object sender, EventArgs e)
        {
            var tmk = _listaTmkGrid.Find(x => x.Id == Convert.ToInt32(gridView9.Columns.View.GetFocusedRowCellValue(colunaId) ));

            txtId.Text = tmk.Id.ToString();

            BusqueECarreguePedido();

           // FormAtendimentoRefiltek formAtender = new FormAtendimentoRefiltek(tmk.Id.ToInt(), tmk.DataCompra.ToDate(), tmk.CodigoCliente.ToString());


        }

        private void gcVendas_Click(object sender, EventArgs e)
        {

        }

        private void txtIdProduto_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtDescontoUnitario_EditValueChanged(object sender, EventArgs e)
        {

        }
    }







}

