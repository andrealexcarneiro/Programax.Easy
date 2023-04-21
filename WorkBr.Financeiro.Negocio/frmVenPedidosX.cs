using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Programax.Easy.Negocio;
using Programax.Easy.Core.Util;
using FiscalPrinterBematech;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace Programax.Easy.View
{
    public partial class frmVenPedidosX : DevExpress.XtraEditors.XtraForm
    {
        public frmVenPedidosX()
        {
            InitializeComponent();
        }
        public NeUsuarios oNeUsuarios = null;
        public NeEmpresa oNeEmpresa = null;

        #region Propriedades
        private bool Editando { get; set; }
        #endregion -----------------------------

        #region Variáveis
        public int intCodigo { get; set; }
        public int intPedido { get; set; }
        public int intProduto { get; set; }
        public int intFinalidade { get; set; }
        public int _Usuario { get; set; }
        public string varConsumidor { get; set; }
        public string varNome { get; set; }
        public string strFantasia { get; set; }
        public string varEndereco { get; set; }
        public string varCidade { get; set; }
        public string varEstado { get; set; }
        public Boolean I = false;
//        public Boolean booLiberado { get; set; }
        public decimal decEstoque { get; set; }
        public string strDevolucao { get; set; }
        public string strEntregaPedido { get; set; }

        // Impressora Bematech
        private int IRetorno;
        string Cupom = new string((char)32, 6);
        private string detalhe { get; set; }
        private int _Tamanho { get; set; }
        private string det1 { get; set; }
        private string det2 { get; set; }
        private string det3 { get; set; }
        private string det4 { get; set; }
        private string det5 { get; set; }
        private string det6 { get; set; }

        //Produto
        private double varQtde { get; set; }
        private double varPreco { get; set; }
        private double varValorBruto { get; set; }
        private double varPercentualDesconto { get; set; }
        private double varValorDesconto { get; set; }
        private double varValorLiquido { get; set; }

        //Tabela Preços
        private decimal varPerFator { get; set; }
        private decimal varValorFator { get; set; }
        private decimal varPerMinimo { get; set; }
        private decimal varPerMaximo { get; set; }
        private decimal varPerMinimoSup { get; set; }
        private decimal varPerMinimoDir { get; set; }

        private double varPesoBruto { get; set; }
        private double varPesoLiquido { get; set; }
        //Rodape da Tela
        private double varTotalProdutos { get; set; }
        private double varTotalServicos { get; set; }
        //Pesos
        private double varTotalPesoBruto { get; set; }
        private double varTotalPesoLiquido { get; set; }

        private string strPedAutomatico { get; set; }
        private string strPedAutFinalizado { get; set; }
        private string strTipoEntregaPedido { get; set; }

        private Boolean booUsarLote { get; set; }
        private Boolean booUsarAproveitamento { get; set; }
        private Boolean booUsarCCVendedor { get; set; }
        private Boolean booUsarBloqueioVendasAcimaLimite { get; set; }
        private Boolean booUsarIdentificacaoAtendente { get; set; }
        private Boolean booObrigaVendedor{ get; set; }

        public Enumerador.TpPedido TipoPedido { get; set; }
        public Enumerador.TpVenda TipoVenda { get; set; }

        public string _TipoForm { get; set; }
        #endregion 
        
        #region Formulários
        //private void frmVenPedidos_Activated(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        labDescCCItem.Visible = false;
        //        labCCItem.Visible = false;
        //        labTotalCCVendedor.Visible = false;
        //        gbDescTotalCCItem.Visible = false;
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}

        private void frmVenPedido_Load(object sender, EventArgs e)
        {
            try
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.DXCore.Controls.UserSkins.OfficeSkins.Register();
                LookAndFeel.SkinName = "Caramel";

                int _Count = 0;
                for (int i = 0; i <= 1000; i++)
                {
                    _Count = i;
                }
                LerPedido("");
                LerCampos();
                LerCondicaoPagamento("");
                txtCodigoPedido.Focus();

                //LerTabelaPrecos("","");
                // LerAtendentes("", "");

                NeEmpresa _NeEmpresa = new NeEmpresa(Modulo.ConnectionString);
                booUsarLote = _NeEmpresa.UsarLoteProduto();
                booUsarAproveitamento = _NeEmpresa.UsarAproveitamento();
                booUsarBloqueioVendasAcimaLimite = _NeEmpresa.UsarBloqueioVendaAcimaLimite();
                booUsarIdentificacaoAtendente = _NeEmpresa.UsarIdentificacaoAtendendente();
                booUsarCCVendedor = _NeEmpresa.UsarCCVendedor();
                _NeEmpresa = null;

                if (booObrigaVendedor == false)
                {
                    LerVendedores("", "");
                }

                chkGravacaoAutomatica.Enabled = false;
                chkGravacaoAutomatica.Checked = false;

                labDescCCItem.Visible = false;
                labCCItem.Visible = false;
                labTotalCCVendedor.Visible = false;
                gbDescTotalCCItem.Visible = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void frmVenPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (ActiveControl.Name == txtCodigoPedido.Name)
                { // -- > Permite a entrada de valores numericos
                    if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                        e.Handled = true;
                }
                // -- > Passe para outro objeto utilizando a tecla enter
                if (e.KeyChar == (char)13)
                {
                    SendKeys.SendWait("{tab}");

                    e.Handled = true;
                }
                else if (e.KeyChar == (char)27)
                {
                    e.Handled = true;
                    Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemId_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (char.IsLetter(e.KeyChar)) e.Handled = true; return;

                // -- > Passe para outro objeto utilizando a tecla enter
                if (e.KeyChar == (char)13)
                {
                    SendKeys.SendWait("{tab}");
                    e.Handled = true;
                }
                else if (e.KeyChar == (char)27)
                {
                    e.Handled = true;
                    Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Propriedades do Formulário
        private void txtCodigoPedido_Enter(object sender, EventArgs e)
        {
            try
            {
                LimparPedido();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigoPedido_Validating(object sender, CancelEventArgs e)
            {
            try
            {
                PesquisarPedido();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemId_Enter(object sender, EventArgs e)
        {
            try
            {
                LimparItensPedido();

                if (booUsarLote == true)
                {
                    labLote.Visible = true;
                    cboLoteProduto.Visible = true;
                }
                else
                {
                    labLote.Visible = false;
                    cboLoteProduto.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemId_Click(object sender, EventArgs e)
        {
            try
            {
                LimparItensPedido();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtItemId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                PesquisarItemPedido();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigoPessoa_Enter(object sender, EventArgs e)
        {
            try
            {
                LimparPessoa();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigoPessoa_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToInt32("0" + txtCodigoPessoa.Text) > 0)
                {
                    NePessoas _NePessoas = new NePessoas(Modulo.ConnectionString) { Id = Convert.ToInt32("0" + txtCodigoPessoa.Text) };
                    _NePessoas.Pesquisar();

                    if (_NePessoas.Editando)
                    {
                        if (_NePessoas.Razao.Trim().ToUpper() == "CONSUMIDOR")
                        {
                            varConsumidor = _NePessoas.Razao.Trim().ToUpper();
                        }
                        else varConsumidor = "";

                        labNomePessoa.Text = " " + _NePessoas.Razao.Trim() + " / " + _NePessoas.Fantasia.Trim();
                        strFantasia = _NePessoas.Fantasia.Trim();
                        labCidade.Text = _NePessoas.CidadeEndereco.Trim();
                        labEstado.Text = _NePessoas.Estado_Id.Trim();
                        labInscricaoFederal.Text = _NePessoas.InscricaoFederal.Trim();
                        labStatusPessoa.Text = _NePessoas.Status == "A" ? "ATIVO" : "INATIVO";

                        if (varConsumidor == "CONSUMIDOR")
                        {
                            txtNome.Enabled = true; txtEndereco.Enabled = true; txtCidade.Enabled = true; txtEstado.Enabled = true;
                            txtNome.Focus();
                        }
                        else
                        {
                            txtNome.Enabled = false;
                            txtEndereco.Enabled = false;
                            txtCidade.Enabled = false;
                            txtEstado.Enabled = false;
                        }
                        //txtItemId.Focus();
                    }
                    _NePessoas = null;
                }
                else
                {
                    MessageBox.Show(this, "Codigo da Pessoa (Fisica ou Juridica) Inexistente, verifique!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoPessoa.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtItemQuantidade_Enter(object sender, EventArgs e)
        {
            try
            {
                //FeModulo.RemovaCaracter(txtQtde.Text.ToString());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtItemQuantidade_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtItemQuantidade.Text = FeModulo.FormatarNumero(txtItemQuantidade.Text, FeModulo.eCasaDecimais.Tres);
                NeProdutos _NeProdutos = new NeProdutos(Modulo.ConnectionString);
                _NeProdutos.Id = Convert.ToInt32("0" + labItemCodigoProduto.Text);
                _NeProdutos.Pesquisar();
                if (_NeProdutos.Editando)
                {
                    if (booUsarLote == true && labItemCodigoProduto.Text.Trim().Length>0 && txtItemQuantidade.Text.Trim().Length>0)
                    {

                    }

                    if (Convert.ToDecimal(txtItemQuantidade.Text) > Convert.ToDecimal(_NeProdutos.Estoque))
                    {
                        labItemEstoque.Text = FeModulo.FormatarNumero(Convert.ToString(_NeProdutos.Estoque), FeModulo.eCasaDecimais.Tres);
                        MessageBox.Show(" A quantidade informada é maior que o estoque atual neste momento!.  veja o estoque disponível.");
                        txtItemQuantidade.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtitemValorDesconto_Enter(object sender, EventArgs e)
        {
            try
            {
                // txtItemValorDesconto.Text = Convert.ToString(FeModulo.FormatarNumero(txtItemValorDesconto.Text, FeModulo.eCasaDecimais.Duas));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtItemValorDesconto_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                varValorDesconto = Convert.ToDouble("0" + txtItemValorDesconto.Text);
                varValorLiquido = (varValorBruto - varValorDesconto);
                labItemValorLiquido.Text = varValorLiquido.ToString("0.00");
                txtItemValorDesconto.Text = varValorDesconto.ToString("0.00");
                labItemValorLiquido.Text = Convert.ToString(varValorBruto - varValorDesconto);
                labItemValorLiquido.Text = FeModulo.FormatarNumero(Convert.ToString(labItemValorLiquido.Text), FeModulo.eCasaDecimais.Duas);
                if (varValorDesconto > Convert.ToDouble("0" + labItemValorLiquido.Text))
                {
                    MessageBox.Show(this, "O Valor do Desconto não pode ser Maior que o Valor do Produto!" + labItemValorLiquido.Text, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItemValorDesconto.Text = "0,00";
                    txtItemPercentualDesconto.Text = "0";
                    txtItemValorDesconto.Focus();
                } else btnIncluir.Focus();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtItemValorUnitario_Enter(object sender, EventArgs e)
        {
            try
            {
                txtItemValorUnitario.Text = FeModulo.FormatarNumero("0" + txtItemValorUnitario.Text, FeModulo.eCasaDecimais.Duas);
                varPreco = Convert.ToDouble(FeModulo.FormatarNumero("0" + txtItemValorUnitario.Text, FeModulo.eCasaDecimais.Duas));
                varValorBruto = varQtde * varPreco;
                labItemValorBruto.Text = varValorBruto.ToString("0.00");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtItemValorUnitario_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToDouble("0" + txtItemValorUnitario.Text) <= 0)
                {
                    MessageBox.Show(this, "Informe o preço do produto!", "Estoque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemValorUnitario.Focus();
                }
                else
                {
                    txtItemValorUnitario.Text = FeModulo.FormatarNumero("0" + txtItemValorUnitario.Text, FeModulo.eCasaDecimais.Duas);
                    varQtde = Convert.ToDouble(txtItemQuantidade.Text);
                    varPreco = Convert.ToDouble("0" + txtItemValorUnitario.Text);
                    varValorBruto = (varQtde * varPreco);
                    labItemValorBruto.Text = varValorBruto.ToString("0.00");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtCodigoVendedor_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToInt32("0" + txtCodigoVendedor.Text) > 0)
                {
                    NeVendedores _NeVendedores = new NeVendedores(Modulo.ConnectionString) { Id = Convert.ToInt32("0" + txtCodigoVendedor.Text) };
                    _NeVendedores.Pesquisar();

                    if (_NeVendedores.Editando)
                    {
                        labNomeVendedor.Text = " " + _NeVendedores.Nome.Trim();

                        if (booUsarCCVendedor == true)
                        {
                            labDescCCItem.Visible = false;
                            labCCItem.Visible = false;
                        }

                        txtCodigoPrecos.Focus();
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo do Vendedor Inexistente, verifique!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigoVendedor.Focus();
                    }
                    _NeVendedores = null;
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtCodigoPrecos_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToInt32("0" + txtCodigoPrecos.Text) > 0)
                {
                    NeTabelaPrecos _NeTabelaPrecos = new NeTabelaPrecos(Modulo.ConnectionString) { Id = Convert.ToInt32("0" + txtCodigoPrecos.Text) };
                    _NeTabelaPrecos.Pesquisar();

                    if (_NeTabelaPrecos.Editando)
                    {
                        labDescricaoPrecos.Text = " " + _NeTabelaPrecos.Descricao.Trim();
                        varPerFator = _NeTabelaPrecos.FatorValor; 
                        varValorFator = _NeTabelaPrecos.ValorAcrescimo;
                        varPerMinimo = _NeTabelaPrecos.DescontoMaximo; 
                        varPerMaximo = _NeTabelaPrecos.AcrescimoMaximo; 
                        varPerMinimoSup = _NeTabelaPrecos.DescontoMaximoSup;
                        varPerMinimoDir = _NeTabelaPrecos.DescontoMaximoDir;
                        dtValidade.Focus();
                    }
                    else
                    {
                        varPerFator = 0;
                        varValorFator = 0;
                        varPerMinimo = 0;
                        varPerMaximo = 0;
                        varPerMinimoSup = 0;
                        varPerMinimoDir = 0;
                        MessageBox.Show(this, "Codigo da tabela preços Inexistente, verifique!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigoPrecos.Focus();
                    }

                    if (gcPedido.DefaultView.RowCount > 0) AtualizaValorProdutos();

                    //CalculaCondicaoPagamento();
                    cboPagamentoCondicao_Click(sender, null);
                    _NeTabelaPrecos = null;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtAtendentes_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Convert.ToInt32("0" + txtCodigoAtendente.Text) > 0)
                {
                    NeAtendentes _NeAtendentes = new NeAtendentes(Modulo.ConnectionString) { Id = Convert.ToInt32("0" + txtCodigoAtendente.Text) };
                    _NeAtendentes.Pesquisar();

                    if (_NeAtendentes.Editando)
                    {
                        labNomeAtendente.Text = " " + _NeAtendentes.Nome.Trim();
                        xtraTabControl1.SelectedTabPageIndex = 0;
                        txtCodigoPessoa.Focus();
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo do Atendente Inexistente, verifique!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigoAtendente.Focus();
                    }
                    _NeAtendentes = null;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtItemPercentualDesconto_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtItemPercentualDesconto.Text = FeModulo.FormatarNumero("0" + txtItemPercentualDesconto.Text, FeModulo.eCasaDecimais.Uma);
                varPercentualDesconto = Convert.ToDouble(FeModulo.FormatarNumero("0" + txtItemPercentualDesconto.Text, FeModulo.eCasaDecimais.Duas));
                if (varPercentualDesconto > 0)
                {
                    txtItemValorDesconto.Text = Convert.ToString(Convert.ToDouble(labItemValorBruto.Text) * varPercentualDesconto / 100);
                }
                txtItemValorDesconto.Focus();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtValorEntrada_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtValorEntrada.Text = FeModulo.FormatarNumero(txtValorEntrada.Text, FeModulo.eCasaDecimais.Duas);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtNome_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtNome.Text = txtNome.Text.Trim().ToUpper();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtEndereco_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtEndereco.Text = txtEndereco.Text.Trim().ToUpper();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtCidade_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtCidade.Text = txtCidade.Text.Trim().ToUpper();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtEstado_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtEstado.Text = txtEstado.Text.Trim().ToUpper();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void cboPagamentoCondicao_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboPagamentoCondicao.Text.Trim().Length <= 0)
                {
                    cboPagamentoCondicao.Focus();
                    return;
                }
                CalculaCondicaoPagamento();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void chkGravacaoAutomatica_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkGravacaoAutomatica.Checked == false)
                { }

                if (rbtnPedido.Enabled == true)
                { }
                else if (rbtnOrcamento.Enabled == true)
                { }
                else if (rbtnRequisicao.Enabled == true)
                { }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //Aguardando o teste definitivo no momento apropriado...
        private void txtNotaComplementar_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                //NeNfSaida _NeNfSaida = new NeNfSaida(Modulo.ConnectionString);
                //if (_NeNfSaida.VerificaNotaComplementar(Convert.ToInt32(txtNotaComplementar.Text)) == true)
                //{
                //    txtNotaComplementar.Text = _NeNfSaida.Id.ToString();
                //    labChaveNFE.Text = _NeNfSaida.ChaveNFE;
                //}
                //else
                //{
                //    txtNotaComplementar.Text = "";
                //    labChaveNFE.Text = "";
                //}
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region Rotinas
        private void PesquisarPedido()
        {
            try
            {
                // btnExcluir.Enabled = false;

                NePedido _NePedido = new NePedido(Modulo.ConnectionString);
                _NePedido.Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                _NePedido.Pesquisar();
                _TipoForm = _NePedido.Editando == true ? "A" : "I";
                if (_NePedido.Editando)
                {
                    labCodigoEmpresaP.Text = NeUsuarios.EmpId.ToString();

                    if (_NePedido.TipoPedido == "PEDIDO") rbtnPedido.Checked = true;
                    else if (_NePedido.TipoPedido == "ORÇAMENTO") rbtnOrcamento.Checked = true;
                    else if (_NePedido.TipoPedido == "REQUISIÇÃO") rbtnRequisicao.Checked = true;

                    if (_NePedido.TipoEntrega == "N") rbtnNenhum.Checked = true;
                    else if (_NePedido.TipoEntrega == "R") rbtnRetirar.Checked = true;
                    else if (_NePedido.TipoEntrega == "E") rbtnEntregar.Checked = true;

                    DateTime _DateTime;
                    if (!string.IsNullOrEmpty(_NePedido.DataEmissao))
                    {
                        _DateTime = Convert.ToDateTime(_NePedido.DataEmissao);
                        labDataEmissao.Text = _DateTime.ToString("dd/MM/yyy");
                    }
                    else labDataEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    labHoraEmissao.Text = _NePedido.HoraEmissao;

                    if (!string.IsNullOrEmpty(_NePedido.DataValidade))
                    {
                        _DateTime = Convert.ToDateTime(_NePedido.DataValidade);
                        dtValidade.Text = _DateTime.ToString("dd/MM/yyyy");
                    }
                    else dtValidade.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    labStatusPedido.Text = _NePedido.StatusVenda;

                    NeVendedores _NeVendedores = new NeVendedores(Modulo.ConnectionString);
                    _NeVendedores.Id = _NePedido.Vendedor_Id;
                    _NeVendedores.Pesquisar();
                    if (_NeVendedores.Editando)
                    {
                        txtCodigoVendedor.Text = Convert.ToString("0" + _NePedido.Vendedor_Id);
                        labNomeVendedor.Text = _NeVendedores.Nome;
                    }
                    _NeVendedores = null;

                    NeAtendentes _NeAtendentes = new NeAtendentes(Modulo.ConnectionString);
                    _NeAtendentes.Id = _NePedido.Atendente_Id;
                    _NeAtendentes.Pesquisar();
                    if (_NeAtendentes.Editando)
                    {
                        txtCodigoAtendente.Text = Convert.ToString("0" + _NePedido.Atendente_Id);
                        labNomeAtendente.Text = _NeAtendentes.Nome;
                    }
                    _NeAtendentes = null;

                    NeTabelaPrecos _NeTabelaPrecos = new NeTabelaPrecos(Modulo.ConnectionString);
                    _NeTabelaPrecos.Id = _NePedido.TabelaPrecos_Id;
                    _NeTabelaPrecos.Pesquisar();
                    if (_NeTabelaPrecos.Editando)
                    {
                        txtCodigoPrecos.Text = Convert.ToString("0" + _NePedido.TabelaPrecos_Id);
                        labDescricaoPrecos.Text = _NeTabelaPrecos.Descricao;
                    }
                    _NeTabelaPrecos = null;

                    NePessoas _NePessoas = new NePessoas(Modulo.ConnectionString);
                    _NePessoas.Id = Convert.ToInt32("0" + _NePedido.Pessoa_Id);
                    _NePessoas.Pesquisar();
                    if (_NePessoas.Editando)
                    {
                        txtCodigoPessoa.Text = Convert.ToString("0" + _NePessoas.Id);
                        labNomePessoa.Text = _NePessoas.Razao.Trim(); // +" / " + _NePessoas.Fantasia.Trim(); 
                        labCidade.Text = _NePessoas.CidadeEndereco.Trim();
                        labEstado.Text = _NePessoas.Estado_Id.Trim();
                        labInscricaoFederal.Text = _NePessoas.InscricaoFederal.Trim();
                        labStatusPessoa.Text = _NePessoas.Status == "A" ? "ATIVO" : "INATIVO";
                    }

                    cboPagamentoCondicao.EditValue = _NePedido.CondicaoPagto_Id;


                    if (_NePessoas.Razao.Trim().ToUpper() == "CONSUMIDOR")
                    {
                        varConsumidor = _NePessoas.Razao.Trim();
                    }
                    else varConsumidor = "";

                    if (varConsumidor == "CONSUMIDOR")
                    {
                        txtNome.Enabled = true;
                        txtEndereco.Enabled = true;
                        txtCidade.Enabled = true;
                        txtEstado.Enabled = true;
                    }
                    else
                    {
                        txtNome.Enabled = false;
                        txtEndereco.Enabled = false;
                        txtCidade.Enabled = false;
                        txtEstado.Enabled = false;
                    }
                    txtFrete.Text = Convert.ToString("0" + _NePedido.Frete);
                    txtInputValor.Enabled = false;
                    gbVenda.Enabled = false;
                    LerPedidoItens("");

                    // if (!btnExcluir.Enabled) btnExcluir.Enabled = true;
                }
                else
                {
                    labCodigoEmpresaP.Text = NeUsuarios.EmpId.ToString();
                    labDataEmissao.Text = Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy"));
                    labHoraEmissao.Text = Convert.ToString(DateTime.Now.ToString("hh:mm:ss"));
                    labStatusPedido.Text = "ABERTO";
                    dtValidade.EditValue = DateTime.Now.ToString("dd/MM/yyyy");
                    _Usuario = NeUsuarios.UserId;
                    txtCodigoVendedor.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void PesquisarItemPedido()
        {
            try
            {
                Editando = false;
                btnExcluir.Enabled = false;
                btnAlterar.Enabled = false;

                NePedido2Itens _NePedido2Itens = new NePedido2Itens(Modulo.ConnectionString);
                _NePedido2Itens.Pedido_Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                if (txtItemId.Text.Length > 0)
                {
                    if (txtItemId.Text.Length > 0 && txtItemId.Text.Length <= 6)
                        _NePedido2Itens.Produto_Id = Convert.ToInt32("0" + txtItemId.Text);
                    else
                        _NePedido2Itens.CodigoBarras = txtItemId.Text;
                }
                else
                {
                    //txtItemId.Focus();
                    //return;
                }

                _NePedido2Itens.Pesquisar(_NePedido2Itens);

                if (_NePedido2Itens.Editando)
                {
                    labItemCodigoProduto.Text = Convert.ToString("0" + _NePedido2Itens.Produto_Id);

                    ////if (_NePedido2Itens.Finalidade == "1") labItemFinalidadeProduto.Text = "Mercadoria para Revenda";
                    ////else if (_NePedido2Itens.Finalidade == "2") labItemFinalidadeProduto.Text = "Materia Prima";
                    ////else if (_NePedido2Itens.Finalidade == "3") labItemFinalidadeProduto.Text = "Embalagem e Acabamento";
                    ////else if (_NePedido2Itens.Finalidade == "4") labItemFinalidadeProduto.Text = "Uso e Consumo";
                    ////else if (_NePedido2Itens.Finalidade == "5") labItemFinalidadeProduto.Text = "Imobilizado";
                    ////else if (_NePedido2Itens.Finalidade == "6") labItemFinalidadeProduto.Text = "Serviço";
                    ////if (intFinalidade == 6) { labItemFinalidadeProduto.ForeColor = Color.Red; } else { labItemFinalidadeProduto.ForeColor = Color.Blue; }

                    intFinalidade = Convert.ToInt32("0" + _NePedido2Itens.Finalidade);

                    labItemCodigoBarras.Text = _NePedido2Itens.CodigoBarras;
                    labItemDescricaoProduto.Text = _NePedido2Itens.Descricao.Trim();
                    labItemReferencia.Text = _NePedido2Itens.Referencia.Trim();
                    labItemUnidade.Text = _NePedido2Itens.Unidade.Trim();

                    txtItemQuantidade.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.Quantidade), FeModulo.eCasaDecimais.Duas);
                   // txtItemPercentualDesconto.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.PercentualDesconto), FeModulo.eCasaDecimais.Duas);
                    txtItemValorDesconto.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.ValorDesconto), FeModulo.eCasaDecimais.Duas);
                    labItemValorTabela.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.ValorUnitario), FeModulo.eCasaDecimais.Duas);
                    txtItemValorUnitario.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.ValorUnitario), FeModulo.eCasaDecimais.Duas);
                    labItemEstoque.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.Estoque), FeModulo.eCasaDecimais.Tres);
                    decEstoque = _NePedido2Itens.Estoque;
                    if (decEstoque > 0) { labItemEstoque.ForeColor = Color.Green; } else { labItemEstoque.ForeColor = Color.Red; }

                    varQtde = Convert.ToDouble("0" + txtItemQuantidade.Text);
                    varPreco = Convert.ToDouble("0" + txtItemValorUnitario.Text);
                    labItemValorBruto.Text = FeModulo.FormatarNumero(Convert.ToString(varQtde * varPreco), FeModulo.eCasaDecimais.Duas);
                    varValorBruto = (varQtde * varPreco);
                    varValorDesconto = Convert.ToDouble("0" + txtItemValorDesconto.Text);
                    if (varValorBruto > 0) labItemValorLiquido.Text = FeModulo.FormatarNumero(Convert.ToString(varValorBruto - varValorDesconto), FeModulo.eCasaDecimais.Duas);

                    varPesoBruto = Convert.ToDouble(_NePedido2Itens.ValorPesoBruto);
                    varPesoLiquido = Convert.ToDouble(_NePedido2Itens.ValorPesoLiquido);
                    btnExcluir.Enabled = true; btnAlterar.Enabled = true; btnIncluir.Enabled = false;
                }
                else
                {
                    PesquisarItemPedidoProduto();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void PesquisarItemPedidoProduto()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemId.Text.Trim()))
                {
                    NeProdutos _NeProdutos = new NeProdutos(Modulo.ConnectionString);

                    if (txtItemId.Text.Length > 0 && txtItemId.Text.Length <= 6)
                    {
                        _NeProdutos.Id = Convert.ToInt32("0" + txtItemId.Text);
                    }
                    else
                        _NeProdutos.CodigoBarras = txtItemId.Text;

                    _NeProdutos.Pesquisar();
                    if (_NeProdutos.Editando)
                    {
                        labItemCodigoProduto.Text = Convert.ToString("0" + _NeProdutos.Id);

                        ////if (_NeProdutos.FinalidadeProduto == "1") labItemFinalidadeProduto.Text = "Mercadoria para Revenda";
                        ////else if (_NeProdutos.FinalidadeProduto == "2") labItemFinalidadeProduto.Text = "Materia Prima";
                        ////else if (_NeProdutos.FinalidadeProduto == "3") labItemFinalidadeProduto.Text = "Embalagem e Acabamento";
                        ////else if (_NeProdutos.FinalidadeProduto == "4") labItemFinalidadeProduto.Text = "Uso e Consumo";
                        ////else if (_NeProdutos.FinalidadeProduto == "5") labItemFinalidadeProduto.Text = "Imobilizado";
                        ////else if (_NeProdutos.FinalidadeProduto == "6") labItemFinalidadeProduto.Text = "Serviço";
                        ////intFinalidade = Convert.ToInt16("0" + _NeProdutos.FinalidadeProduto);
                        ////if (intFinalidade == 6) { labItemFinalidadeProduto.ForeColor = Color.Red; } else { labItemFinalidadeProduto.ForeColor = Color.Blue; }

                        intFinalidade = _NeProdutos.FinalidadeProduto;

                        labItemCodigoBarras.Text = _NeProdutos.CodigoBarras;
                        labItemDescricaoProduto.Text = _NeProdutos.Descricao.Trim();
                        labItemReferencia.Text = _NeProdutos.Referencia.Trim();
                        labItemUnidade.Text = _NeProdutos.Unidade.Trim();

                        labItemEstoque.Text = FeModulo.FormatarNumero(Convert.ToString(_NeProdutos.Estoque), FeModulo.eCasaDecimais.Tres);
                        decEstoque = _NeProdutos.Estoque;
                        if (decEstoque > 0) { labItemEstoque.ForeColor = Color.Green; } else { labItemEstoque.ForeColor = Color.Red; }

                        varPesoBruto = Convert.ToDouble(_NeProdutos.PesoBruto);
                        varPesoLiquido = Convert.ToDouble(_NeProdutos.PesoLiquido);
                    }
                    else
                    {
                        MessageBox.Show(this, "Codigo Digitado Errado ou Produto Inexistente, Verifique!.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemId.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void Incluir()
        {
            try
            {
                varValorDesconto = Convert.ToDouble("0" + txtItemValorDesconto.Text);
                varValorLiquido = (varValorBruto - varValorDesconto);
                labItemValorLiquido.Text = varValorLiquido.ToString("0.00");
                txtItemValorDesconto.Text = varValorDesconto.ToString("0.00");
                labItemValorLiquido.Text = Convert.ToString(varValorBruto - varValorDesconto);
                labItemValorLiquido.Text = FeModulo.FormatarNumero(Convert.ToString(labItemValorLiquido.Text), FeModulo.eCasaDecimais.Duas);

                if (!Consiste()) return;

                NePedido _NePedido = new NePedido(Modulo.ConnectionString);
                _NePedido.Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                _NePedido.Pessoa_Id = Convert.ToInt32("0" + txtCodigoPessoa.Text);

                _NePedido.Vendedor_Id = Convert.ToInt32("0" + txtCodigoVendedor.EditValue);
                _NePedido.Atendente_Id = Convert.ToInt32("0" + txtCodigoAtendente.EditValue);
                _NePedido.TabelaPrecos_Id = Convert.ToInt32("0" + txtCodigoPrecos.EditValue);
                _NePedido.CondicaoPagto_Id = Convert.ToInt32("0" + cboPagamentoCondicao.EditValue);

                _NePedido.StatusVenda = labStatusPedido.Text; // Aberto Fechado etc...

                if (rbtnPedido.Checked == true) _NePedido.TipoPedido = "P"; //Pedido
                else if (rbtnOrcamento.Checked == true) _NePedido.TipoPedido = "O"; //Orçamento
                else if (rbtnRequisicao.Checked == true) _NePedido.TipoPedido = "R"; //Requisicao

                if (rbtnNenhum.Checked == true) _NePedido.TipoEntrega = "N"; // Nenhum
                if (rbtnRetirar.Checked == true) _NePedido.TipoEntrega = "R"; // Retirar
                if (rbtnEntregar.Checked == true) _NePedido.TipoEntrega = "E"; // Entregar

                if (chkGravacaoAutomatica.Checked == true)
                {
                    _NePedido.Automatico = "S";
                }
                else _NePedido.Automatico = "N";

                string _Data = "";
                FeModulo.IsDate(labDataEmissao.Text, ref _Data);
                _NePedido.DataEmissao = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy") : "";

                _NePedido.HoraEmissao = labHoraEmissao.Text;

                FeModulo.IsDate(dtValidade.Text, ref _Data);
                _NePedido.DataValidade = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy") : "";

                _NePedido.Usuario_Id = NeUsuarios.UserId;

                //if (booUsarLote == true)
                //{
                //    labLote.Visible = true;
                //    cboLoteProduto.Visible = true;
                //}
                //else
                //{
                //    labLote.Visible = false;
                //    cboLoteProduto.Visible = false;
                //}

                _NePedido.oNePedido2Itens.Produto_Id = Convert.ToInt32("0" + labItemCodigoProduto.Text);
                _NePedido.oNePedido2Itens.CodigoBarras = labItemCodigoBarras.Text;
                _NePedido.oNePedido2Itens.Descricao = labItemDescricaoProduto.Text.Trim();
                _NePedido.oNePedido2Itens.Referencia = labItemReferencia.Text.Trim();
                _NePedido.oNePedido2Itens.Unidade = labItemUnidade.Text.Trim();
                _NePedido.oNePedido2Itens.Quantidade = Convert.ToDecimal(txtItemQuantidade.Text);
                _NePedido.oNePedido2Itens.ValorUnitario = Convert.ToDecimal(txtItemValorUnitario.Text);
                _NePedido.oNePedido2Itens.ValorBruto = Convert.ToDecimal(labItemValorBruto.Text);

               // _NePedido.oNePedido2Itens.TipoDesconto = txtItemPercentualDesconto.Text;

                _NePedido.oNePedido2Itens.ValorDesconto = Convert.ToDecimal(txtItemValorDesconto.Text);
                _NePedido.oNePedido2Itens.ValorLiquido = Convert.ToDecimal(labItemValorLiquido.Text);
                _NePedido.oNePedido2Itens.Finalidade = intFinalidade;

                varQtde = Convert.ToDouble(txtItemQuantidade.Text);
                _NePedido.oNePedido2Itens.ValorPesoBruto = Convert.ToDecimal(varPesoBruto * varQtde);
                _NePedido.oNePedido2Itens.ValorPesoLiquido = Convert.ToDecimal(varPesoLiquido * varQtde);

                if (intFinalidade == 6)
                {
                    _NePedido.oNePedido2Itens.Valor_Servicos = Convert.ToDecimal(labItemValorLiquido.Text);
                }
                else _NePedido.oNePedido2Itens.Valor_Produtos = Convert.ToDecimal(labItemValorLiquido.Text);

                long _Identity = 0;

                _NePedido.Consumidor = txtNome.Text.Trim();
                _NePedido.Endereco = txtEndereco.Text.Trim();
                _NePedido.Cidade = txtCidade.Text.Trim();
                _NePedido.Estado = txtEstado.Text.Trim();

                if (_NePedido.Gravar(ref  _Identity) > 0)
                {
                    LerPedidoItens("");
                    MessageBox.Show(this, "Produto gravado com sucesso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _NePedido = null;
                }
                txtCodigoPedido.Text = _Identity.ToString();
                txtItemId.Focus();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void AtualizaCabecalho()
        {
            try
            {
                //if (!Consiste()) return;

                NePedido _NePedido = new NePedido(Modulo.ConnectionString);
                _NePedido.Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                _NePedido.Pessoa_Id = Convert.ToInt32("0" + txtCodigoPessoa.Text);

                _NePedido.Vendedor_Id = Convert.ToInt32("0" + txtCodigoVendedor.EditValue);
                _NePedido.Atendente_Id = Convert.ToInt32("0" + txtCodigoAtendente.EditValue);
                _NePedido.TabelaPrecos_Id = Convert.ToInt32("0" + txtCodigoPrecos.EditValue);
                _NePedido.CondicaoPagto_Id = Convert.ToInt32("0" + cboPagamentoCondicao.EditValue);

                _NePedido.StatusVenda = labStatusPedido.Text;

                if (rbtnPedido.Checked == true) _NePedido.TipoPedido = "P";
                else if (rbtnOrcamento.Checked == true) _NePedido.TipoPedido = "O";
                else if (rbtnRequisicao.Checked == true) _NePedido.TipoPedido = "R";

                string _Data = "";
                FeModulo.IsDate(labDataEmissao.Text, ref _Data);
                _NePedido.DataEmissao = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy") : "";

                _NePedido.HoraEmissao = labHoraEmissao.Text;

                FeModulo.IsDate(dtValidade.Text, ref _Data);
                _NePedido.DataValidade = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy") : "";

                _NePedido.Usuario_Id = NeUsuarios.UserId;

                _NePedido.Total_Produtos = Convert.ToDecimal("0" + labTotalProdutos.Text);
                _NePedido.Total_Servicos = Convert.ToDecimal("0" + labTotalServicos.Text);
                _NePedido.Total_Bruto = Convert.ToDecimal("0" + colunaValorBruto.SummaryItem.SummaryValue);

                _NePedido.Total_Descontos = Convert.ToDecimal("0" + colunaValorDesconto.SummaryItem.SummaryValue);
                _NePedido.Total_Liquido =  (_NePedido.Total_Produtos + _NePedido.Total_Servicos);
                _NePedido.Frete = Convert.ToDecimal("0" + txtFrete.Text);
                _NePedido.ValorTotal =  (_NePedido.Total_Liquido +  _NePedido.Frete);

                _NePedido.Total_PesoBruto = Convert.ToDecimal("0" + labTotalPesoBruto.Text);
                _NePedido.Total_PesoLiquido = Convert.ToDecimal("0" + labTotalPesoLiquido.Text);

                _NePedido.GravarCabecalho();
                txtItemId.Focus();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void Apagar()
        {
            try
            {
                StringBuilder _Builder = new StringBuilder();
                _Builder.Append("Você está prestes a apagar (01) registros(s)");
                _Builder.AppendLine();
                _Builder.AppendLine();
                _Builder.Append("Se clicar em Sim, não será possível desfazer essa operação de exclusão. ");
                _Builder.Append("Você tem certeza que deseja excluir o registro agora?");

                if (MessageBox.Show(this, _Builder.ToString(), "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                NePedido2Itens _NePedido2Itens = new NePedido2Itens(Modulo.ConnectionString);
                _NePedido2Itens.Id = Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId).ToString());
                _NePedido2Itens.Produto_Id = Convert.ToInt32("0" + labItemCodigoProduto.Text);
                _NePedido2Itens.Quantidade = Convert.ToDecimal(txtItemQuantidade.Text);
                if (_NePedido2Itens.Apagar() > 0)
                {
                    LerPedidoItens("");
                    MessageBox.Show(this, "Registro apagado com sucesso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemId.Focus();
                }

                _NePedido2Itens = null;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void GravarVenda()
        {
            string strTipo = "";
            //string strPedAutomatico = "";
           // Boolean booCondicaoLiberou = false;
           // Boolean booGerarComissao = false;
            //Boolean booVendaAcimaLimite = false;
            //Boolean booStatus = false;
           // Boolean booZerado = false;

            try
            {
                #region Verificar se o Sistema esta Bloqueado.

                #endregion

                #region Verificar a Data Limite.
                NeProdutosMovimentoSaldo _NeProdutosMovimentoSaldo = new NeProdutosMovimentoSaldo(Modulo.ConnectionString);
                if (_NeProdutosMovimentoSaldo.DataLimiteMovimento(Convert.ToDateTime(labDataEmissao.Text)) == true)
                {
                    StringBuilder _Builder = new StringBuilder();
                    _Builder.Append("Não é possível Realizar Movimento de Itens");
                    _Builder.AppendLine();
                    _Builder.Append("Anterior a Data do Último Fechamento !. ");
                    _Builder.AppendLine();
                    _Builder.Append("Fechamento de Estoque : " + labDataEmissao.Text);

                    MessageBox.Show(this, _Builder.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region Verificar se o Cadastro da Pessoa Fisica/Juridica esta Ok!
                NePessoas _NePessoas = new NePessoas(Modulo.ConnectionString);
                if (_NePessoas.VerificaCadastro(Convert.ToInt32("0" + txtCodigoPessoa.Text)) == false)
                {
                    StringBuilder _Builder = new StringBuilder();
                    _Builder.Append("Cliente com Irregularidade para Gerar NFe");
                    _Builder.AppendLine();
                    _Builder.Append("Informe a Razão Social ! ");
                    _Builder.AppendLine();
                    _Builder.Append("Deseja Alterar o Cadastro ! " + labDataEmissao.Text);

                    if (MessageBox.Show(this, _Builder.ToString(), "Atenção! Cadastro Incorreto!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    frmCadPessoas _frmCadPessoas = new frmCadPessoas();
                    _frmCadPessoas.ShowDialog();
                }
                #endregion

                if (rbtnPedido.Enabled == true)
                {
                    strTipo = "Pedido";
                    TipoPedido = Enumerador.TpPedido.TpSaida;
                    TipoVenda = Enumerador.TpVenda.TvPedido;
                }
                else if (rbtnRequisicao.Enabled == true)
                {
                    strTipo = "Requisição";
                    TipoPedido = Enumerador.TpPedido.TpSaida;
                    TipoVenda = Enumerador.TpVenda.TvRequisicao;
                }
                else if (rbtnOrcamento.Enabled == true)
                {
                    strTipo = "Orçamento";
                    TipoPedido = Enumerador.TpPedido.TpSaida;
                    TipoVenda = Enumerador.TpVenda.TvOrcamento;
                }

                if (txtCodigoPrecos.Text.Trim().Length <= 0)
                {
                    MessageBox.Show(this, "Informe a tabela de preço do " + strTipo, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCodigoPrecos.Focus();
                    return;
                }

                if (txtCodigoPessoa.Text.Trim().Length <= 0)
                {
                    MessageBox.Show(this, "Informe o nome da Pessoa (Fisica/Juridica).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCodigoPessoa.Focus();
                    return;
                }

                if (TipoPedido == Enumerador.TpPedido.TpSaida)
                {
                    if (cboPagamentoCondicao.EditValue == null)
                    {
                        MessageBox.Show(this, "Informe a condição de pagamento do " + strTipo, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        chkGravacaoAutomatica.Checked = false;
                        cboPagamentoCondicao.Focus();
                        return;
                    }
                }

                if (booUsarIdentificacaoAtendente == true)
                {
                    if (txtCodigoAtendente.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show(this, "Informe o nome do atendente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboPagamentoCondicao.Focus();
                        txtCodigoAtendente.Focus();
                        return;
                    }
                }

                if (TipoPedido == Enumerador.TpPedido.TpComplementar)
                {
                    if (string.IsNullOrEmpty(labChaveNFE.Text))
                    {
                        MessageBox.Show(this, "Informe o Número da NFe que será Complementada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNotaComplementar.Focus();
                        return;
                    }
                }

                if (gcPedido.DefaultView .RowCount <=0)
                {
                    if (TipoPedido == Enumerador.TpPedido.TpSaida)
                    {
                        if (chkGravacaoAutomatica.Checked)
                        {
                             MessageBox.Show(this, "É Obrigatório Informar ao menos 1 (um) produto ou 1 (um) serviço Item no " + strTipo + " !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtItemId.Focus ();
                            return;
                        }
                    }
                }

                if (rbtnOrcamento.Enabled == true && dtValidade.EditValue == null)
                {
                    MessageBox.Show(this, "Informe a Data de Validade do Orçamento!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtValidade.Focus();
                    return;
                }

                if (TipoPedido == Enumerador.TpPedido.TpSaida)
                {
                     if (booObrigaVendedor == true)
                     {
                         //frmVendedorSenha
                     }
                }

                if (booUsarBloqueioVendasAcimaLimite == true)
                {
                    //decimal decTotalCompras = 0;
                    //decimal decTotalLiberado = 0;
                }

                if (chkGravacaoAutomatica.Checked == true)
                {
                    strPedAutomatico = "S";
                } 
                    else 
                {
                    strPedAutFinalizado = "S"; 
                    strPedAutFinalizado = "N";
                }

                if (rbtnNenhum.Checked == true) strEntregaPedido = "N";
                else if (rbtnRetirar.Checked == true) strEntregaPedido = "R";
                else if (rbtnEntregar.Checked == true) strEntregaPedido = "E";

                NePagamentoCondicao _NePagamentoCondicao = new NePagamentoCondicao(Modulo.ConnectionString);
                _NePagamentoCondicao.Id = Convert.ToInt32("0" + cboPagamentoCondicao.EditValue);
                _NePagamentoCondicao.Pesquisar();
                labEntrada.Visible = false;
                txtValorEntrada.Visible = false;
                if (_NePagamentoCondicao.Editando)
                {
                    frmAdmLiberacao _frmAdmLiberacao = new frmAdmLiberacao();
                    _frmAdmLiberacao.CodigoAtual = Convert.ToInt32("0" + txtCodigoPedido.Text);
                    _frmAdmLiberacao.TipoLiberacao = frmAdmLiberacao.eTpLiberacao.ClienteBloqueado;
                    _frmAdmLiberacao.TituloFormulario = "Liberação de Condição de Pagto Especial";
                    _frmAdmLiberacao.Descricao = "Informe o Usuário e a Senha para Liberação da Condição de Pagamento : " + _NePagamentoCondicao.Descricao;
                    _frmAdmLiberacao.ShowDialog();
                    if (_frmAdmLiberacao.Liberado == false)
                    {
                        //if (_frmAdmLiberacao.TipoLiberacao == frmAdmLiberacao.eTpLiberacao.ClienteBloqueado)
                        //{

                        //}
                        cboPagamentoCondicao.Focus();
                        return;
                    }
                    //else booCondicaoLiberou = true;

                }

                strDevolucao = "N";
                //booVendaAcimaLimite = false;
                //booGerarComissao = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void AtualizaValorProdutos()
        {
            try
            {
                //NeProdutos _NeProdutos = new NeProdutos();
                //_NeProdutos.
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

         private void LimparPedido()
        {
            try
            {
                txtCodigoPedido.Text = "";
                labCodigoEmpresaP.Text = "";
                labDataEmissao.Text = "";
                labHoraEmissao.Text = "";
                labStatusPedido.Text = "";
                dtValidade.Text = "";
                txtCodigoVendedor.Text = "";
                labNomeVendedor.Text = "";
                txtCodigoAtendente.Text = "";
                labNomeAtendente.Text = "";
                txtCodigoPrecos.Text = "";
                labDescricaoPrecos.Text = "";
                rbtnPedido.Checked = false;
                rbtnOrcamento.Checked = false;
                rbtnRequisicao.Checked = false;
                LimparPessoa();
                LimparItensPedido();
                gcPedido.DataSource = null;
                cboPagamentoCondicao.EditValue = null;
                labTotalPesoBruto.Text = "";
                labTotalPesoLiquido.Text = "";
                labTotalProdutos.Text = "";
                labTotalServicos.Text = "";
                labSubTotal.Text = "";
                txtFrete.Text = "";
                labValorTotal.Text = "";
                txtInputValor.Text = "";
                //if (btnExcluir.Enabled) btnExcluir.Enabled = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LimparPessoa()
        {
            try
            {
                txtCodigoPessoa.Text = "";
                labNomePessoa.Text = "";
                labCidade.Text = "";
                labEstado.Text = "";
                labInscricaoFederal.Text = "";
                labStatusPessoa.Text = "";

                //if (btnExcluir.Enabled) btnExcluir.Enabled = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LimparItensPedido()
        {
            try
            {
                txtItemId.Text = "";
                labItemCodigoProduto.Text = "";
                labItemCodigoBarras.Text = "";
                labItemDescricaoProduto.Text = "";
                labItemFinalidadeProduto.Text = "";
                labItemReferencia.Text = "";
                labItemUnidade.Text = "";
                labItemEstoque.Text = "0";
                txtItemQuantidade.Text = "";
                labItemValorTabela.Text = "0,00";
                txtItemValorUnitario.Text = "0,00";
                labItemValorBruto.Text = "0,00";
                txtItemPercentualDesconto.Text = "0,0";
                txtItemValorDesconto.Text = "0,00";
                labItemValorLiquido.Text = "0,00";

                if (btnExcluir.Enabled) btnExcluir.Enabled = false;
                if (btnAlterar.Enabled) btnAlterar.Enabled = false;
                btnIncluir.Enabled = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private bool Consiste()
        {
            bool _Result = false;
            try
            {
                //Header do pedido
                if (labDataEmissao.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe a data do pedido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); labDataEmissao.Focus(); }
                else if (txtCodigoVendedor.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe o nome do vendedor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtCodigoVendedor.Focus(); }
                else if (txtCodigoAtendente.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe o nome do atendente.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtCodigoAtendente.Focus(); }
                else if (dtValidade.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe a data de validade.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); dtValidade.Focus(); }
                else if (txtCodigoPrecos.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe qual é a tabela de preços.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtCodigoPrecos.Focus(); }
                else if (txtCodigoPessoa.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe o código da pessoa ( fisca ou jurídica ).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtCodigoPessoa.Focus(); }
                //Boby do pedido
                else if (txtItemQuantidade.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe a quantidade para este item.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtItemQuantidade.Focus(); }
                else if (txtItemValorUnitario.Text.Trim().Length <= 0) { MessageBox.Show(this, "Informe valor unitario para este item.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtItemValorUnitario.Focus(); }
                else
                    _Result = true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return _Result;
        }

        private void GeraNota()
        {
            try
            {
                NePedido _NePedido = new NePedido (Modulo.ConnectionString);
               if (_NePedido.VerificaPedidoAutAberto(Convert.ToInt32("0" + txtCodigoPedido.Text)) == true)
                {
                    StringBuilder _Builder = new StringBuilder();
                    _Builder.Append("Pedido : " + Convert.ToInt32("0" + txtCodigoPedido.Text) + " Gravação Automática não Finalizado ! Impossível Gerar Nota Fiscal. ");
                   _Builder.Append("entre no Pedido, confira as informações e pressione o botão Gravar e Finalizar !");
                    MessageBox.Show(this, _Builder.ToString(),  "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                   return;
                }

                //frmNotaFiscal _frmNotaFiscal = new frmNotaFiscal();
                //_frmNotaFiscal.NumeroPedido = Convert.ToInt32("0" + txtCodigoPedido.Text);
                //_frmNotaFiscal.Estado = labEstado.Text;
                //_frmNotaFiscal.ShowDialog();

                //if (_frmNotaFiscal.NumeroNota != 0)
                //{
                //    NePedido _NePedido = new NePedido(Modulo.ConnectionString);
                   _NePedido.AtualizaStatusVenda(Convert.ToInt32("0" + txtCodigoPedido.Text), "F");
                //}
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CalculaDesconto()
        {
            decimal decValorUnitario = 0;
            decimal decValorTabela = 0;
            decimal varDescUnitProduto = 0;
            decimal varLabCCItem = 0;
            try
            {
                if (txtItemValorUnitario.Text.Trim().Length <= 0) return;
                labItemValorTabela.Text = txtItemValorUnitario.Text;

                if (decValorUnitario < decValorTabela)
                {
                    varDescUnitProduto = (decValorUnitario - decValorTabela);
                    if (varDescUnitProduto > 0)
                    {

                    }
                    else varDescUnitProduto = (varDescUnitProduto * -1);

                    //Conta Corrente do Vendedor.
                    varLabCCItem = ((decValorUnitario - decValorTabela) * Convert.ToDecimal(txtItemQuantidade));
                }
                else
                {
                    varDescUnitProduto = 0;

                    //Conta Corrente do Vendedor.
                    varLabCCItem = ((decValorUnitario - decValorTabela) * Convert.ToDecimal(txtItemQuantidade));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void CalculaCondicaoPagamento()
        {
            decimal decValorParcela = 0;
            int intParcelas = 0;
            decimal decValorMinimo = 0;
            Boolean booEntrada = false;
            try
            {
                // Verificando a condição de Pagamento.
                if (cboPagamentoCondicao.Text.Trim().Length > 0)
                {
                    NePagamentoCondicao _NePagamentoCondicao = new NePagamentoCondicao(Modulo.ConnectionString);
                    _NePagamentoCondicao.Id = Convert.ToInt32("0" + cboPagamentoCondicao.EditValue);
                    _NePagamentoCondicao.Pesquisar();
                    labEntrada.Visible = false;
                    txtValorEntrada.Visible = false;
                    if (_NePagamentoCondicao.Editando)
                    {
                        decValorParcela = Convert.ToDecimal("0" + labValorTotal.Text);
                        if (_NePagamentoCondicao.TipoVenda == "AV")
                        {
                            labCondicaoPagamento.Text = " A Vista : " + decValorParcela;
                            txtValorEntrada.Text = "0,00";
                        }

                        else if (_NePagamentoCondicao.TipoVenda == "AP")
                        {
                            #region
                            intParcelas = _NePagamentoCondicao.NumeroParcelas;
                            decValorMinimo = _NePagamentoCondicao.ValorMinimoParcela;
                            if (_NePagamentoCondicao.Entrada == "S")
                            {
                                booEntrada = true;
                                labEntrada.Visible = true;
                                txtValorEntrada.Visible = true;
                                decValorParcela = (Convert.ToDecimal("0" + labValorTotal.Text) - Convert.ToDecimal("0" + txtValorEntrada.Text));
                            }
                            else
                            {
                                labEntrada.Visible = false;
                                txtValorEntrada.Visible = false;
                                txtValorEntrada.Text = "0,00";
                            }

                            if (booEntrada == true) intParcelas = intParcelas - 1;

                            //Verificando se Plano pode ser Utilizado neste Pedido.
                            if (intParcelas > 0)
                            {
                                if (_NePagamentoCondicao.TipoJuros == 2) // Juro Composto
                                {
                                    //decValorParcela = (verificar DLL);
                                }
                                else if (_NePagamentoCondicao.TipoJuros == 1) // Juro Simples
                                {
                                    if (_NePagamentoCondicao.TaxaJuros == 0)
                                    {
                                        decValorParcela = decValorParcela / intParcelas;
                                    }
                                    else
                                    {
                                        //decValorParcela = (verificar DLL);
                                    }
                                }

                                if (decValorParcela < decValorMinimo)
                                {
                                    MessageBox.Show(this, "O Valor da Parcela não pode ser Inferior ao Valor Minimo " + decValorMinimo, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    labCondicaoPagamento.Text = "";
                                }

                                if (booEntrada == true)
                                {
                                    labCondicaoPagamento.Text = " Entrada + " + intParcelas + " Parc : " + decValorParcela;
                                }
                                else
                                {
                                    labCondicaoPagamento.Text = intParcelas + " Parc : " + decValorParcela + " S/ENTR. ";
                                }
                            }
                            #endregion
                        }
                        labCondicaoPagamento.Text = " A PRAZO : " + decValorParcela + " S/ENTR. ";
                    }
                }
                txtValorEntrada.Text = "0,00";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Botões

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeModulo.VerificaPermissao("", 27, NeUsuarios.UserId, _TipoForm)) Incluir();
                else
                {
                    MessageBox.Show(this, FeModulo._Mensagem, "Atenção !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeModulo.VerificaPermissao("", 27, NeUsuarios.UserId, "E")) Apagar();
                else
                {
                    MessageBox.Show(this, FeModulo._Mensagem, "Atenção !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeModulo.VerificaPermissao("", 27, NeUsuarios.UserId, "O"))
                {
                    if (gcPedido.DefaultView.RowCount > 0)
                    {
                        AtualizaCabecalho();
                        GravarVenda();
                    }
                    else
                    {
                        MessageBox.Show(this, "Você precisa gravar ao menos (1) Item do pedido para continuar ( produto / serviço ) !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(this, FeModulo._Mensagem, "Atenção !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        private void btnRatearDescontos_Click(object sender, EventArgs e)
        {
            try
            {
                if (gcPedido.Views.Count > 0)
                {
                    if (MessageBox.Show(this, "Ratear Descontos entre os Items do pedido.", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }
                else MessageBox.Show(this, "Não existe Itens neste pedido para rateio.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void btnSeleciona_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32("0" + colunaId.View.GetFocusedRowCellValue(colunaId)) > 0)
                {
                    intCodigo = Convert.ToInt32("0" + colunaId.View.GetFocusedRowCellValue(colunaId));
                    Close();
                    return;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void btnCupomX_Click(object sender, EventArgs e)
        {
            string varItem = "";
            string varUnitario = "";
            string varDescValor = "";
            string varQuantidade = "";

            try
            {
                I = false;
                if (txtCodigoPedido.Text.Trim().Length > 0)
                {
                    NePedido _NePedido = new NePedido(Modulo.ConnectionString);
                    _NePedido.Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                    _NePedido.GetPedido();

                    if (_NePedido.Achou)
                    {
                        IRetorno = BemaFI32.Bematech_FI_AbreCupom(labNomeVendedor.Text);
                        BemaFI32.Analisa_iRetorno(IRetorno);

                        if (IRetorno != 1) { Close(); return; }

                        NePedido2Itens _NePedido2Itens = new NePedido2Itens(Modulo.ConnectionString);
                        _NePedido2Itens.Pedido_Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                        StringBuilder _Builder = new StringBuilder();
                        _Builder.Append(" select  pedi2itens_id, pedi2itens_pedi_id, pedi2itens_prod_id, pedi2itens_codbarras, pedi2itens_descricao, ");
                        _Builder.Append(" pedi2itens_referencia, pedi2itens_unidconv, pedi2itens_qtde, pedi2itens_valorunitario, pedi2itens_percentualdesconto, ");
                        _Builder.Append(" pedi2itens_lote_id, pedi2itens_cpl_id, prod_finalidade, prod_pesobruto, prod_pesoliquido, ");
                        _Builder.Append(" convert(decimal(10,2), pedi2itens_valorbruto) as pedi2itens_valorbruto, ");
                        _Builder.Append(" convert(decimal(10,2), pedi2itens_valordesconto) as pedi2itens_valordesconto, ");
                        _Builder.Append(" convert(decimal(10,2), pedi2itens_valorliquido) as pedi2itens_valorliquido, ");
                        _Builder.Append(" convert(decimal(10,3), prod_balanco) as prod_balanco, ");
                        _Builder.Append(" convert(decimal(10,3), prod_entradas) as prod_entradas, ");
                        _Builder.Append(" convert(decimal(10,3), prod_saidas) as prod_saidas, ");
                        _Builder.Append(" convert(decimal(10,2), pedi2itens_valor_produtos) as pedi2itens_valor_produtos, ");
                        _Builder.Append(" convert(decimal(10,2), pedi2itens_valor_servicos) as pedi2itens_valor_servicos, ");
                        _Builder.Append(" convert(decimal(10,3), pedi2itens_valorpesobruto) as pedi2itens_valorpesobruto, ");
                        _Builder.Append(" convert(decimal(10,3), pedi2itens_valorpesoliquido) as pedi2itens_valorpesoliquido ");
                        _Builder.Append(" from pedido2itens INNER JOIN ");
                        _Builder.Append(" Produtos ON pedi2itens_Prod_Id = Prod_Id  ");
                        _Builder.Append(" where pedi2itens_pedi_id='" + _NePedido2Itens.Pedido_Id + "' ");
                        SqlDataReader _Dr = _NePedido2Itens.ConsulteDReader(_Builder.ToString());

                        if (_Dr != null) // -- > Verifique se não é nulo
                            if (_Dr.HasRows) // -- > Verifique se há linhas no resultado final
                                while (_Dr.Read()) // -- > Avance o ponte de leitura.
                                {
                                    varItem = _Dr["pedi2itens_id"].ToString();
                                    _Tamanho = (3 - varItem.Trim().Length); for (int i = 0; i < _Tamanho; i++) { varItem = "0" + varItem; }
                                    varQuantidade = FeModulo.FormatarNumero(_Dr["pedi2itens_qtde"].ToString(), FeModulo.eCasaDecimais.Tres);
                                    varUnitario = FeModulo.FormatarNumero(_Dr["pedi2itens_valorunitario"].ToString(), FeModulo.eCasaDecimais.Tres);
                                    varDescValor = FeModulo.FormatarNumero(_Dr["pedi2itens_valordesconto"].ToString(), FeModulo.eCasaDecimais.Duas);
                                    string NomeProduto = _Dr["pedi2itens_descricao"].ToString();
                                    if (NomeProduto.Length > 29) NomeProduto = NomeProduto.Substring(0, 28);
                                    IRetorno = BemaFI32.Bematech_FI_VendeItem(varItem, NomeProduto, "FF", "F", varQuantidade, 3, varUnitario, "$", varDescValor);
                                    BemaFI32.Analisa_iRetorno(IRetorno);
                                }
                        if (!_Dr.IsClosed) _Dr.Close();
                        _Dr = null;

                        // Tira os espaços.
                        string Cupom = new string('\x20', 14);
                        IRetorno = BemaFI32.Bematech_FI_NumeroCupom(ref Cupom);
                        BemaFI32.Analisa_iRetorno(IRetorno);

                        //using (StreamReader reader = new StreamReader("C:/Retorno.txt"))
                        //Cupom = reader.ReadLine();

                        string varCodPes = txtCodigoPessoa.Text.Trim();
                        _Tamanho = (6 - varCodPes.Trim().Length); for (int i = 0; i < _Tamanho; i++) { varCodPes = "0" + varCodPes; }

                        if (varConsumidor == "CONSUMIDOR")
                        {
                            det1 = varCodPes + "-" + txtNome.Text.Trim();
                            _Tamanho = (48 - det1.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det1 += " "; }
                        }
                        else
                        {
                            det1 = varCodPes + "-" + labNomePessoa.Text.Trim();
                            _Tamanho = (48 - det1.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det1 += " "; }
                        }

                        NePessoas _NePessoas = new NePessoas(Modulo.ConnectionString);
                        _NePessoas.Id = Convert.ToInt32("0" + txtCodigoPessoa.Text);
                        _NePessoas.Pesquisar();
                        if (_NePessoas.Editando)
                        {
                            if (varConsumidor == "CONSUMIDOR")
                            {
                                det2 = "Ender.:" + txtEndereco.Text.Trim();
                                _Tamanho = (48 - det2.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det2 += " "; }

                                det4 = "Cidade:" + txtCidade.Text.Trim() + " / " + txtEstado.Text.Trim();
                                _Tamanho = (48 - det4.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det4 += " "; }
                            }
                            else
                            {
                                det2 = "Ender.:" + _NePessoas.Endereco.Trim() + " " + _NePessoas.Complemento.Trim() + " nr." + _NePessoas.Numero.Trim();
                                _Tamanho = (48 - det2.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det2 += " "; }

                                det3 = "Bairro:" + _NePessoas.Bairro.Trim(); //+ " - " + _NePessoas.Fantasia.Trim();
                                _Tamanho = (48 - det3.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det3 += " "; }

                                det4 = "Cidade:" + _NePessoas.CidadeEndereco.Trim() + " / " + _NePessoas.Estado_Id.Trim();
                                _Tamanho = (48 - det4.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det4 += " "; }
                            }
                        }

                        NePagamentoCondicao _NePagamentoCondicao = new NePagamentoCondicao(Modulo.ConnectionString);
                        _NePagamentoCondicao.Id = Convert.ToInt32("0" + 1);
                        _NePagamentoCondicao.Pesquisar();
                        if (_NePagamentoCondicao.Editando)
                        {
                            det5 = "Pagto.:" + _NePagamentoCondicao.Descricao.Trim() + " Vdr:" + labNomeVendedor.Text.Trim();
                            _Tamanho = (48 - det5.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det5 += " "; }
                        }

                        string varCodPed = txtCodigoPedido.Text.Trim();
                        _Tamanho = (6 - varCodPed.Trim().Length); for (int i = 0; i < _Tamanho; i++) { varCodPed = "0" + varCodPed; }

                        det6 = "Pedido:" + varCodPed.Trim(); // +"   Nr.Cupom:" + Cupom.Trim();
                        _Tamanho = (48 - det6.Trim().Length); for (int i = 0; i < _Tamanho; i++) { det6 += " "; }

                        detalhe = det1 + det2 + det3 + det4 + det5 + det6;

                        IRetorno = BemaFI32.Bematech_FI_FechaCupomResumido("Dinheiro", detalhe);
                        BemaFI32.Analisa_iRetorno(IRetorno);

                        IRetorno = BemaFI32.Bematech_FI_TerminaFechamentoCupom("Obrigado. Volte Sempre!");
                        BemaFI32.Analisa_iRetorno(IRetorno);

                        string _Data = DateTime.Now.ToString();
                        _NePedido.NumeroCupom = Convert.ToInt32("0" + Cupom);
                        _NePedido.DataCupom = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy HH:mm:ss") : "";
                        _NePedido.Consumidor = txtNome.Text;
                        _NePedido.Endereco = txtEndereco.Text;
                        _NePedido.Cidade = txtCidade.Text;
                        _NePedido.Estado = txtEstado.Text;

                        if (_NePedido.AtualizaCupomConsumidor(_NePedido) > 0)
                        {
                            // GERAR PARCELAS NO CADASTRO DE CONTAS
                            //GravarContasParcelas();
                            StringBuilder _Builder2 = new StringBuilder();
                            _Builder2.Append("Operação completada com sucesso! Cupom Fiscal Nr.: " + Cupom);
                            MessageBox.Show(this, _Builder2.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeModulo.VerificaPermissao("", 27, NeUsuarios.UserId, "O"))
                {
                    frmVenFechamentoVenda _frmVenFechamentoVenda = new frmVenFechamentoVenda();
                    _frmVenFechamentoVenda.intCodigo = Convert.ToInt32("0" + txtCodigoPedido.Text);
                    _frmVenFechamentoVenda.ShowDialog();
                }
                else
                {
                    MessageBox.Show(this, FeModulo._Mensagem, "Atenção !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeModulo.VerificaPermissao("", 27, NeUsuarios.UserId, _TipoForm))
                {
                    frmVenPedidosAlteracaoItem _frmVenPedidosAlteracaoItem = new frmVenPedidosAlteracaoItem();
                    _frmVenPedidosAlteracaoItem.intPedido = Convert.ToInt32("0" + txtCodigoPedido.Text);
                    _frmVenPedidosAlteracaoItem.intCodigo = Convert.ToInt32("0" + labItemCodigoProduto.Text);
                    _frmVenPedidosAlteracaoItem.ShowDialog();
                    AtualizaCabecalho();
                    PesquisarPedido();
                }
                else
                {
                    MessageBox.Show(this, FeModulo._Mensagem, "Atenção !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void btnRatearDesconto_Click(object sender, EventArgs e)
        {
            try
            {
                decimal varTotalPedido = 0;
                decimal varTotalDesconto = 0;
                //decimal varDesconto = 0;
                //decimal varTotalProduto = 0;
                //decimal varDescontoItem = 0;
                //decimal varTotalDescontoConcedido = 0;
                decimal varValorMinimo = 0;
                decimal varValorMinimoSup = 0;
                decimal varValorMinimoDir = 0;
                decimal varValor = 0;
                decimal varDescontoMaximo = 0;
                decimal varDescProdutos = 0;

                varTotalPedido = Convert.ToDecimal(labTotalProdutos.Text);

                if (txtInputValor.Enabled == false)
                {
                    txtInputValor.Enabled = true;
                    txtInputValor.Focus();
                }
                else if (Convert.ToDecimal("0" + txtInputValor.Text) > 0)
                {
                    varTotalDesconto = Convert.ToDecimal(txtInputValor.Text);
                    if (varTotalDesconto > varTotalPedido)
                    {
                        StringBuilder _Builder = new StringBuilder();
                        _Builder.Append("O Valor Total do Desconto não pode ser");
                        _Builder.AppendLine();
                        _Builder.Append("maior ou igual ao Valor Total do Pedido !");
                        MessageBox.Show(this, _Builder.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _Builder = null;
                        return;
                    }

                    //booLiberado = false;
                    if (FeModulo.VerificaPermissao("", 159, NeUsuarios.UserId, "A"))
                    {
                        //booLiberado = true;
                        varValor = (Convert.ToDecimal(labTotalProdutos.Text) - varTotalDesconto);
                        varDescProdutos = Convert.ToDecimal(colunaValorDesconto.SummaryItem.SummaryValue.ToString());

                        if (varPerMinimo == 0) varValorMinimo = (Convert.ToDecimal(labTotalProdutos.Text) - varDescProdutos) - (Convert.ToDecimal(labTotalProdutos.Text) - varDescProdutos) * ((varPerMinimo) / 100);
                        if (varPerMinimoSup == 0) varValorMinimoSup = (Convert.ToDecimal(labTotalProdutos.Text) - varDescProdutos) - (Convert.ToDecimal(labTotalProdutos.Text) - varDescProdutos) * ((varPerMinimoSup) / 100);
                        if (varPerMinimoDir == 0) varValorMinimoDir = (Convert.ToDecimal(labTotalProdutos.Text) - varDescProdutos) - (Convert.ToDecimal(labTotalProdutos.Text) - varDescProdutos) * ((varPerMinimoDir) / 100);

                        //Verificando se o Valor Informado é Abaixo do Valor Máximo de Desconto.
                        if (varDescontoMaximo > varValor)
                        {
                            StringBuilder _Builder = new StringBuilder();
                            _Builder.Append("Valor Informado Abaixo do Desconto Máximo Permitido na Tabela !");
                            _Builder.AppendLine();
                            _Builder.Append("Impossível Ratear o Desconto (" + varTotalDesconto + ")");
                            MessageBox.Show(this, _Builder.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _Builder = null;
                            btnRatearDesconto.Focus();
                        }

                        //Verificando se o Valor Informado esta entre maior que o Valor Minimo permitido sem ter uma permissão diferenciada.
                        if (varValor < varValorMinimo) // Valor menor que o valor permitido.
                        {
                            if (varValorMinimoDir > 0 && varValorMinimoSup > 0)
                            {
                                if (varValor < varValorMinimoSup)
                                {
                                    // É Necessário uma permissão de Diretor
                                    frmAdmLiberacao _frmAdmLiberacao = new frmAdmLiberacao();
                                    _frmAdmLiberacao.CodigoAtual = 0;
                                    _frmAdmLiberacao.CodigoCliente = Convert.ToInt32("0" + txtCodigoPessoa.Text);
                                    _frmAdmLiberacao.strDescAuditoria = strFantasia + " - Desconto Liberado : " + varTotalDesconto.ToString("0.00");
                                    _frmAdmLiberacao.TipoLiberacao = frmAdmLiberacao.eTpLiberacao.DescontoMaxDiretor;
                                    _frmAdmLiberacao.TituloFormulario = "Liberação de Desconto Máximo Diretor";
                                    _frmAdmLiberacao.Descricao = "Informe o Usuário e a Senha para Liberação do Desconto Maximo do Rateio do Pedido - Valor : " + varValor.ToString("0.00");
                                    //_frmAdmLiberacao.CodigoPermissao = 191;
                                    //_frmAdmLiberacao.CodigoPermissao2 = 0;
                                    _frmAdmLiberacao.ShowDialog();
                                    if (_frmAdmLiberacao.Liberado == false)
                                    {
                                        MessageBox.Show(this, "Usuário sem Permissão para Liberar Desconto Maximo Diretor !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _frmAdmLiberacao = null;
                                        txtInputValor.Text = "";
                                        txtInputValor.Enabled = false;
                                        btnRatearDesconto.Focus();
                                    }
                                }
                                else
                                {
                                    // É Necessário uma permissão de Supervisor.
                                    frmAdmLiberacao _frmAdmLiberacao = new frmAdmLiberacao();
                                    _frmAdmLiberacao.CodigoAtual = 0;
                                    _frmAdmLiberacao.CodigoCliente = Convert.ToInt32("0" + txtCodigoPessoa.Text);
                                    _frmAdmLiberacao.strDescAuditoria = strFantasia + " - Desconto Liberado : " + varTotalDesconto.ToString("0.00");
                                    _frmAdmLiberacao.TipoLiberacao = frmAdmLiberacao.eTpLiberacao.DescontoMaxSupervisor;
                                    _frmAdmLiberacao.TituloFormulario = "Liberação de Desconto Máximo Supervisor";
                                    _frmAdmLiberacao.Descricao = "Informe o Usuário e a Senha para Liberação do Desconto Maximo do Rateio do Pedido - Valor : " + varValor.ToString("0.00");
                                    _frmAdmLiberacao.CodigoPermissao = 191;
                                    _frmAdmLiberacao.CodigoPermissao2 = 190;
                                    _frmAdmLiberacao.ShowDialog();
                                    if (_frmAdmLiberacao.Liberado == false)
                                    {
                                        MessageBox.Show(this, "Usuário sem Permissão para Liberar Desconto Maximo Supervisor !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _frmAdmLiberacao = null;
                                        btnRatearDesconto.Focus();
                                    }

                                }
                            }
                            else if (varValorMinimoDir > 0 && varValorMinimoSup == 0)
                            {
                                if (varValor < varValorMinimoDir)
                                {
                                    StringBuilder _Builder = new StringBuilder();
                                    _Builder.Append("Valor Informado Abaixo do Valor Desconto Máximo");
                                    _Builder.AppendLine();
                                    _Builder.Append(" Permitido na Tabela !   Faça a Correção do Valor !");
                                    MessageBox.Show(this, _Builder.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnRatearDesconto.Focus();
                                }
                                else
                                {
                                    // É Necessário uma permissão de Diretor
                                    frmAdmLiberacao _frmAdmLiberacao = new frmAdmLiberacao();
                                    _frmAdmLiberacao.CodigoAtual = 0;
                                    _frmAdmLiberacao.CodigoCliente = Convert.ToInt32("0" + txtCodigoPessoa.Text);
                                    _frmAdmLiberacao.strDescAuditoria = strFantasia + " - Desconto Liberado : " + varTotalDesconto.ToString("0.00");
                                    _frmAdmLiberacao.TipoLiberacao = frmAdmLiberacao.eTpLiberacao.DescontoMaxDiretor;
                                    _frmAdmLiberacao.TituloFormulario = "Liberação de Desconto Máximo Diretor";
                                    _frmAdmLiberacao.Descricao = "Informe o Usuário e a Senha para Liberação do Desconto Maximo do Rateio no Pedido - Valor : " + varValor.ToString("0.00");
                                    _frmAdmLiberacao.CodigoPermissao = 191;
                                    _frmAdmLiberacao.CodigoPermissao2 = 0;
                                    _frmAdmLiberacao.ShowDialog();
                                    if (_frmAdmLiberacao.Liberado == false)
                                    {
                                        MessageBox.Show(this, "Usuário sem Permissão para Liberar Desconto Maximo Diretor !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _frmAdmLiberacao = null;
                                        btnRatearDesconto.Focus();
                                    }
                                }
                            }
                            else if (varValorMinimoDir == 0 && varValorMinimoSup > 0)
                            {
                                if (varValor > varValorMinimoSup)
                                {
                                    // É Necessário uma permissão de Supervisor.
                                    frmAdmLiberacao _frmAdmLiberacao = new frmAdmLiberacao();
                                    _frmAdmLiberacao.CodigoAtual = 0;
                                    _frmAdmLiberacao.CodigoCliente = Convert.ToInt32("0" + txtCodigoPessoa.Text);
                                    _frmAdmLiberacao.strDescAuditoria = strFantasia + " - Desconto Liberado : " + varTotalDesconto.ToString("0.00");
                                    _frmAdmLiberacao.TipoLiberacao = frmAdmLiberacao.eTpLiberacao.DescontoMaxSupervisor;
                                    _frmAdmLiberacao.TituloFormulario = "Liberação de Desconto Máximo Supervisor";
                                    _frmAdmLiberacao.Descricao = "Informe o Usuário e a Senha para Liberação do Desconto Maximo do Rateio do Pedido - Valor : " + varValor.ToString("0.00");
                                    _frmAdmLiberacao.CodigoPermissao = 191;
                                    _frmAdmLiberacao.CodigoPermissao2 = 190;
                                    _frmAdmLiberacao.ShowDialog();
                                    if (_frmAdmLiberacao.Liberado == false)
                                    {
                                        MessageBox.Show(this, "Usuário sem Permissão para Liberar Desconto Maximo Supervisor !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _frmAdmLiberacao = null;
                                        btnRatearDesconto.Focus();
                                    }
                                }
                            }
                        }
                    } // Verifica Permissão
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region Grid Control
        private void PreencheGrid(string pComplemento)
        {
            try
            {
                pComplemento = " pedi2itens_pedi_id = ";
                pComplemento += Convert.ToInt32("0" + txtCodigoPedido.Text);
                NePedido2Itens _NePedido2Itens = new NePedido2Itens(Modulo.ConnectionString);
                gcPedido.DataSource = _NePedido2Itens.ListaPedido2Itens(pComplemento);
                _NePedido2Itens = null;

                labTotalPesoBruto.Text = colunaPesoBruto.SummaryItem.SummaryValue.ToString();
                labTotalPesoLiquido.Text = colunaPesoLiquido.SummaryItem.SummaryValue.ToString();
                labTotalProdutos.Text = colunaProdutos.SummaryItem.SummaryValue.ToString();
                labTotalServicos.Text = colunaServicos.SummaryItem.SummaryValue.ToString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string _Sql;
                if (cboCampos.EditValue != null)
                {
                    if (!string.IsNullOrEmpty(cboCampos.EditValue.ToString()))
                    {
                        _Sql = String.Format("{0} LIKE '%{1}%' ", cboCampos.EditValue, txtPesquisa.Text.Trim());

                        if (string.IsNullOrEmpty(txtPesquisa.Text.Trim()))
                        {
                            PreencheGrid("");
                            if (gcPedido.Views.Count > 0) { btnSeleciona.Enabled = true; } else { btnSeleciona.Enabled = false; }
                        }
                        else
                        {
                            PreencheGrid(_Sql);
                            if (gcPedido.Views.Count > 0) { btnSeleciona.Enabled = true; } else { btnSeleciona.Enabled = false; }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        private void gcPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (gcPedido.Views.Count > 0)
                {
                    if (colunaCodigoProduto.View.GetFocusedRowCellValue(colunaCodigoProduto) != null)
                    {
                        txtItemId.Text = colunaCodigoProduto.View.GetFocusedRowCellValue(colunaCodigoProduto).ToString();
                        PesquisarItemPedido();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(this, Ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SomarColunas()
        {
            try
            {
                varTotalServicos = 0; varTotalProdutos = 0; varTotalPesoBruto = 0; varTotalPesoLiquido = 0;
                decimal varSubTotal = 0;
                decimal varFrete = 0;
                decimal varValorTotal = 0;
                NePedido2Itens _NePedido2Itens = new NePedido2Itens(Modulo.ConnectionString);
                _NePedido2Itens.Pedido_Id = Convert.ToInt32("0" + txtCodigoPedido.Text);
                _NePedido2Itens.PesquisarPedidoItens(_NePedido2Itens);
                if (_NePedido2Itens.Editando)
                {
                    labTotalProdutos.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.TotalProdutos), FeModulo.eCasaDecimais.Duas);
                    labTotalServicos.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.TotalServicos), FeModulo.eCasaDecimais.Duas);
                    labTotalPesoBruto.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.TotalPesoBruto), FeModulo.eCasaDecimais.Tres);
                    labTotalPesoLiquido.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.TotalPesoLiquido), FeModulo.eCasaDecimais.Tres);

                    labSubTotal.Text = FeModulo.FormatarNumero(Convert.ToString("0" + _NePedido2Itens.TotalBruto), FeModulo.eCasaDecimais.Duas);
                    varSubTotal = Convert.ToDecimal("0" + labSubTotal.Text);
                    varFrete = Convert.ToDecimal("0" + txtFrete.Text);
                    txtFrete.Text = FeModulo.FormatarNumero(Convert.ToString("0" + txtFrete.Text), FeModulo.eCasaDecimais.Duas);
                    varValorTotal = (varSubTotal - varFrete);
                    labValorTotal.Text = FeModulo.FormatarNumero(Convert.ToString("0" + varValorTotal), FeModulo.eCasaDecimais.Duas);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Lê Área Negocios / Preenche Combo e/ou Grid
        private void LerPedido(string pComplemento)
        {
            try
            {
                NePedido _NePedido = new NePedido(Modulo.ConnectionString);
                gcPedido.DataSource = _NePedido.ListaPedido(pComplemento);
                _NePedido = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LerPedidoItens(string pComplemento)
        {
            try
            {
                pComplemento = " pedi2itens_pedi_id = ";
                pComplemento += Convert.ToInt32("0" + txtCodigoPedido.Text);
                NePedido2Itens _NePedido2Itens = new NePedido2Itens(Modulo.ConnectionString);
                gcPedido.DataSource = _NePedido2Itens.ListaPedido2Itens(pComplemento).Tables[0].DefaultView;
                _NePedido2Itens = null;

                SomarColunas();
                //AtualizaCabecalho();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LerCondicaoPagamento(string pComplemento)
        {
            try
            {
                NePagamentoCondicao _NePagamentoCondicao = new NePagamentoCondicao(Modulo.ConnectionString);
                cboPagamentoCondicao.Properties.DataSource = _NePagamentoCondicao.ListaPagamentoCondicao(pComplemento);
                cboPagamentoCondicao.Properties.ValueMember = "copag_id";
                cboPagamentoCondicao.Properties.DisplayMember = "copag_descricao";
                _NePagamentoCondicao = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LerLotes(string pComplemento)
        {
            try
            {
                NeLotes _NeLotes = new NeLotes(Modulo.ConnectionString);
                cboLoteProduto.Properties.DataSource = _NeLotes.BuscarLotesProduto();
                cboLoteProduto.Properties.ValueMember = "lote_id";
                cboLoteProduto.Properties.DisplayMember = "lote_lote";
                cboLoteProduto.Properties.DisplayMember = "saldo";
                _NeLotes = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LerTabelaPrecos(string pComplemento, string pTipo)
        {
            try
            {
                //pTipo = "cbo";
                //NeTabelaPrecos _NeTabelaPrecos = new NeTabelaPrecos(Modulo.ConnectionString);
                //cboTabelaPrecos.Properties.DataSource = _NeTabelaPrecos.ListaPrecos(pComplemento, pTipo);
                //cboTabelaPrecos.Properties.ValueMember = "tbpre_id";
                //cboTabelaPrecos.Properties.DisplayMember = "tbpre_descricao";
                //_NeTabelaPrecos = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LerVendedores(string pComplemento, string pTipo)
        {
            try
            {
                //pTipo = "cbo";
                //NeVendedores _NeVendedores = new NeVendedores(Modulo.ConnectionString);
                //cboVendedor.Properties.DataSource = _NeVendedores.ListaVendedores(pComplemento, pTipo);
                //cboVendedor.Properties.ValueMember = "vend_id";
                //cboVendedor.Properties.DisplayMember = "vend_nome";
                //_NeVendedores = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void LerAtendentes(string pComplemento, string pTipo)
        {
            try
            {
                //pTipo = "cbo";
                //NeAtendentes _NeAtendentes = new NeAtendentes(Modulo.ConnectionString);
                //cboAtendente.Properties.DataSource = _NeAtendentes.ListaAtendentes(pComplemento, pTipo);
                //cboAtendente.Properties.ValueMember = "aten_id";
                //cboAtendente.Properties.DisplayMember = "aten_nome";
                //_NeAtendentes = null;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        #endregion

        #region Tabelas Temporárias
        //private void LerTipoVenda()
        //{
        //    try
        //    {
        //        //Instanciar a variável para criar tabela temporária
        //        DataSet _Ds = new DataSet();

        //        //Criar uma nova instância para tabela temporária
        //        DataTable dt = new DataTable("TipoVenda");

        //        dt.Columns.Add("Descricao", typeof(string));

        //        _Ds.Tables.Add(dt);

        //        DataRow _Row = _Ds.Tables[0].NewRow(); //Nova linha

        //        _Row["Descricao"] = "AVISTA";
        //        _Ds.Tables[0].Rows.Add(_Row);
        //        _Row = _Ds.Tables[0].NewRow();

        //        _Row["Descricao"] = "APRAZO";
        //        _Ds.Tables[0].Rows.Add(_Row);
        //        _Row = _Ds.Tables[0].NewRow();

        //        cboTipoVenda.Properties.BeginUpdate();
        //        cboTipoVenda.Properties.DataSource = _Ds.Tables[0].DefaultView;
        //        cboTipoVenda.Properties.DisplayMember = _Ds.Tables[0].Columns[0].ToString();
        //        cboTipoVenda.Properties.ValueMember = _Ds.Tables[0].Columns[0].ToString();
        //        cboTipoVenda.Properties.EndUpdate();
        //        cboTipoVenda.EditValue = "AVISTA";
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}

        private void LerCampos()
        {
            try
            {

                //Instanciar a variável para criar tabela temporária
                DataSet _Ds = new DataSet();

                //Criar uma nova instância para tabela temporária
                DataTable dt = new DataTable("Tabela");

                #region Definindo os campos da Tabela
                dt.Columns.Add("Descricao", typeof(string));
                dt.Columns.Add("Campo", typeof(string));
                #endregion

                _Ds.Tables.Add(dt);


                DataRow _Row = _Ds.Tables[0].NewRow(); //Nova linha

                _Row["Descricao"] = "Id";
                _Row["Campo"] = " pedi_id ";
                _Ds.Tables[0].Rows.Add(_Row); //Adicionar as linhas na tabela

                _Row = _Ds.Tables[0].NewRow();
                _Row["Descricao"] = "Razao-Social";
                _Row["Campo"] = " pedi_razao ";
                _Ds.Tables[0].Rows.Add(_Row);

                _Row = _Ds.Tables[0].NewRow();
                _Row["Descricao"] = "Fantasia";
                _Row["Campo"] = " pedi_fantasia ";
                _Ds.Tables[0].Rows.Add(_Row);

                cboCampos.Properties.BeginUpdate();
                cboCampos.Properties.DataSource = _Ds.Tables[0].DefaultView;
                cboCampos.Properties.DisplayMember = _Ds.Tables[0].Columns[0].ToString();
                cboCampos.Properties.ValueMember = _Ds.Tables[0].Columns[1].ToString();
                cboCampos.Properties.EndUpdate();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region Pesquisa Tabela e Retorna consulta para Formulario de Origem
        private void pbPesquisaCadastro_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                frmCadPessoasPesquisa _frmCadPessoasPesquisa = new frmCadPessoasPesquisa();
                _frmCadPessoasPesquisa.ShowDialog();
                if (_frmCadPessoasPesquisa.intCodigo > 0)
                {
                    NePessoas _NePessoas = new NePessoas(Modulo.ConnectionString);
                    _NePessoas.Id = Convert.ToInt32(_frmCadPessoasPesquisa.intCodigo.ToString());
                    txtCodigoPessoa.Text = _frmCadPessoasPesquisa.intCodigo.ToString();
                    _NePessoas.Pesquisar();
                    labNomePessoa.Text = _NePessoas.Razao.Trim() + " / " + _NePessoas.Fantasia.Trim();
                    labCidade.Text = _NePessoas.CidadeEndereco.Trim();
                    labEstado.Text = _NePessoas.Estado_Id.Trim();
                    labInscricaoFederal.Text = _NePessoas.InscricaoFederal.Trim();
                    labStatusPessoa.Text = _NePessoas.Status == "A" ? "ATIVO" : "INATIVO";
                    //txtItemId.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void pbPesquisaProduto_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //LimparItensPedido();
                frmEstProdutosPesquisa _frmEstProdutosPesquisa = new frmEstProdutosPesquisa();
                _frmEstProdutosPesquisa.ShowDialog();
                if (_frmEstProdutosPesquisa.intCodigo > 0)
                {
                    NeProdutos _NeProdutos = new NeProdutos(Modulo.ConnectionString);
                    _NeProdutos.Id = _frmEstProdutosPesquisa.intCodigo;
                    _NeProdutos.Pesquisar();

                    if (_NeProdutos.Editando)
                    {
                        txtItemId.Text = Convert.ToString("0" + _NeProdutos.Id);

                        labItemCodigoProduto.Text = Convert.ToString("0" + _NeProdutos.Id);
                        labItemCodigoBarras.Text = _NeProdutos.CodigoBarras.Trim();
                        labItemDescricaoProduto.Text = _NeProdutos.Descricao.Trim();
                        labItemUnidade.Text = _NeProdutos.Unidade.Trim();
                        labItemEstoque.Text = Convert.ToString("0" + _NeProdutos.Estoque);
                        txtItemValorUnitario.Text = Convert.ToString("0" + _NeProdutos.PrecoCusto);

                        //if (_NeProdutos.FinalidadeProduto == "1") labItemFinalidadeProduto.Text = "Mercadoria para Revenda";
                        //else if (_NeProdutos.FinalidadeProduto == "2") labItemFinalidadeProduto.Text = "Materia Prima";
                        //else if (_NeProdutos.FinalidadeProduto == "3") labItemFinalidadeProduto.Text = "Embalagem e Acabamento";
                        //else if (_NeProdutos.FinalidadeProduto == "4") labItemFinalidadeProduto.Text = "Uso e Consumo";
                        //else if (_NeProdutos.FinalidadeProduto == "5") labItemFinalidadeProduto.Text = "Imobilizado";
                        //else if (_NeProdutos.FinalidadeProduto == "6") labItemFinalidadeProduto.Text = "Serviço";

                        intFinalidade = Convert.ToInt32("0" + _NeProdutos.FinalidadeProduto);

                        varPesoBruto = Convert.ToDouble(_NeProdutos.PesoBruto);
                        varPesoLiquido = Convert.ToDouble(_NeProdutos.PesoLiquido);

                        txtItemQuantidade.Focus();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void pbPesquisaAtendente_MouseClick(object sender, EventArgs e)
        {
            try
            {
                frmTabAtendentes _frmTabAtendentes = new frmTabAtendentes();
                _frmTabAtendentes.ShowDialog();
                if (_frmTabAtendentes.intCodigo > 0)
                {
                    txtCodigoAtendente.Text = _frmTabAtendentes.intCodigo.ToString();
                    labNomeAtendente.Text = _frmTabAtendentes.strNome.Trim();
                    txtCodigoPrecos.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void pbPesquisaVendedor_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                frmTabVendedores _frmTabVendedores = new frmTabVendedores();
                _frmTabVendedores.ShowDialog();
                if (_frmTabVendedores.intCodigo > 0)
                {
                    txtCodigoVendedor.Text = _frmTabVendedores.intCodigo.ToString();
                    labNomeVendedor.Text = _frmTabVendedores.strNome.Trim();
                    txtCodigoAtendente.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void pbPesquisaPrecos_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                frmTabPrecos _frmTabPrecos = new frmTabPrecos();
                _frmTabPrecos.ShowDialog();
                if (_frmTabPrecos.intCodigo > 0)
                {
                    txtCodigoPrecos.Text = _frmTabPrecos.intCodigo.ToString();
                    labDescricaoPrecos.Text = _frmTabPrecos.strDescricao.Trim();
                    txtCodigoPrecos.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void pbPesquisaPedido_Mouseclick(object sender, MouseEventArgs e)
        {
            try
            {
                frmVenPedidosPesquisa _frmVenPedidosPesquisa = new frmVenPedidosPesquisa();
                _frmVenPedidosPesquisa.ShowDialog();
                if (_frmVenPedidosPesquisa.intCodigo > 0)
                {
                    txtCodigoPedido.Text = _frmVenPedidosPesquisa.intCodigo.ToString();
                    PesquisarPedido();
                    //txtItemId.Focus();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        private void txtInputValor_Enter(object sender, EventArgs e)
        {
            try
            {
                FeModulo.RemovaCaracter(txtInputValor.ToString());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtInputValor_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtInputValor.Text = FeModulo.FormatarNumero(txtInputValor.Text, FeModulo.eCasaDecimais.Duas);
                //if (Convert.ToDecimal("0" + txtInputValor.Text) > 0)
                //{
                //    btnRatearDesconto.Enabled = true;
                //}  else btnRatearDesconto.Enabled = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void txtFrete_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                   txtFrete.Text = FeModulo.FormatarNumero(txtFrete.Text, FeModulo.eCasaDecimais.Duas);
                   if (Convert.ToDecimal("0" + txtFrete.Text) > 0)
                   {
                       labValorTotal.Text = Convert.ToString(Convert.ToDecimal("0" + labSubTotal.Text) + Convert.ToDecimal("0" + txtFrete.Text));
                       AtualizaCabecalho();
                      memEntrega.Focus();
                   }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void rbtnNenhum_CheckedChanged(object sender, EventArgs e)
        {

        }



    }
}