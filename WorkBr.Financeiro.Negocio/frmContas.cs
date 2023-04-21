using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Persistencia;
using Negocio;

namespace Front_End
{
    public partial class frmContas : Form
    {
        public frmContas(){InitializeComponent();}

        private string ConnectionString { get { return Properties.Conexao.Default.sConexao.ToString() ; } }
        private int CadastroId { get; set; }
        private PsContas _PsContas = null;
        private char  iKeyPress { get; set; }
        private Int32 varVendaId { get; set; }

        public TableLayoutPanel _TableLayoutPanel = null;

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                _PsContas = new PsContas(ConnectionString); 

                if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
                LerNatureza();
                LerTipo();
                LerLancamentos();
                LerCobranca();
                LerFuncionarios();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void LerLancamentos()
        {
            try
            {
                PsLancamentos _PsLancamentos = new PsLancamentos(ConnectionString);
                cboLancamentos.DataSource = null;
                cboLancamentos.BeginUpdate();
                cboLancamentos.DataSource = _PsLancamentos.LerTabela() ;
                cboLancamentos.DisplayMember = "Nome";
                cboLancamentos.ValueMember = "Id";
                cboLancamentos.EndUpdate();
                cboLancamentos.SelectedIndex = -1;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void LerCobranca()
        {
            try
            {
                PsCobranca  _PsCobranca = new PsCobranca(ConnectionString);
                cboCobranca.DataSource = null;
                cboCobranca.BeginUpdate();
                cboCobranca.DataSource = _PsCobranca.LerTabela();
                cboCobranca.DisplayMember = "Nome";
                cboCobranca.ValueMember = "Id";
                cboCobranca.EndUpdate();
                cboCobranca.SelectedIndex = -1;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void LerFuncionarios()
        {
            try
            {
                PsFuncionarios _PsFuncionarios = new PsFuncionarios(ConnectionString);
                cboFuncionarios.DataSource = null;
                cboFuncionarios.BeginUpdate();
                cboFuncionarios.DataSource = _PsFuncionarios.LerTabela();
                cboFuncionarios.DisplayMember = "Nome";
                cboFuncionarios.ValueMember = "Id";
                cboFuncionarios.EndUpdate();
                cboFuncionarios.SelectedIndex = -1;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void LerNatureza()
        {
            try
            {
                #region Tabela temporária

                DataSet _Ds = new DataSet();
                DataTable dt = new DataTable("Natureza");

                #region Definindo os campos da Tabela
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Descricao", typeof(string));
                #endregion

                _Ds.Tables.Add(dt);

                DataRow _Row = _Ds.Tables[0].NewRow();

                _Row["Id"] = "CR";
                _Row["Descricao"] = "CONTAS RECEBER";
                _Ds.Tables[0].Rows.Add(_Row);
                _Row = _Ds.Tables[0].NewRow();

                _Row["Id"] = "CP";
                _Row["Descricao"] = "CONTAS PAGAR";
                _Ds.Tables[0].Rows.Add(_Row);
                _Row = _Ds.Tables[0].NewRow();

                #endregion

                cboNatureza.BeginUpdate();
                cboNatureza.DataSource = _Ds.Tables[0].DefaultView;
                cboNatureza.DisplayMember = _Ds.Tables[0].Columns[1].ToString();
                cboNatureza.ValueMember = _Ds.Tables[0].Columns[0].ToString();
                cboNatureza.EndUpdate();
                cboNatureza.SelectedIndex = -1;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void LerTipo()
        {
            try
            {
                #region Tabela temporária

                DataSet _Ds = new DataSet();
                DataTable dt = new DataTable("Tipo");

                #region Definindo os campos da Tabela
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Descricao", typeof(string));
                #endregion

                _Ds.Tables.Add(dt);

                DataRow _Row = _Ds.Tables[0].NewRow(); 

                _Row["Id"] = "1";
                _Row["Descricao"] = "CLIENTE";
                _Ds.Tables[0].Rows.Add(_Row);
                _Row = _Ds.Tables[0].NewRow();

                _Row["Id"] = "2";
                _Row["Descricao"] = "FORNECEDOR";
                _Ds.Tables[0].Rows.Add(_Row);
                _Row = _Ds.Tables[0].NewRow();

                _Row["Id"] = "3";
                _Row["Descricao"] = "DESPESAS";
                _Ds.Tables[0].Rows.Add(_Row);
                _Row = _Ds.Tables[0].NewRow();

                //_Row["Id"] = "4";
                //_Row["Descricao"] = "DESPESAS";
                //_Ds.Tables[0].Rows.Add(_Row);
                //_Row = _Ds.Tables[0].NewRow();
                #endregion

                cboTipo.BeginUpdate();
                cboTipo.DataSource = _Ds.Tables[0].DefaultView;
                cboTipo.DisplayMember = _Ds.Tables[0].Columns[1].ToString();
                cboTipo.ValueMember = _Ds.Tables[0].Columns[0].ToString();
                cboTipo.EndUpdate();
                cboTipo.SelectedIndex = -1;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void tmrGrid_Tick(object sender, EventArgs e)
        {
            try
            {

               // tmrGrid.Enabled = false;

                PreencheGrid("");

                if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
                if (_TableLayoutPanel != null)
                    if (_TableLayoutPanel.Visible) _TableLayoutPanel.Visible = false;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FeModulo.Numero(ref e);
                
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void Msgbox(Exception Ex)
        {
            MessageBox.Show(this,
               "Mensagem: " + "\n\r" + "\n\r" + Ex.Message.ToString() + "\n\r" + "\n\r" +
               "Ocorrência: " + "\n\r" + "\n\r" + Ex.StackTrace.ToString() + "\n\r" + "\n\r" +
               "Fonte:" + "\n\r" + "\n\r" + Ex.Source.ToString(), "Atenção", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
        }

 
        private void txtDesconto_Enter(object sender, EventArgs e)
        {
            try
            {
                FeModulo.RemovaCaracter(txtDesconto.Text.ToString());
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtDesconto_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtDesconto.Text = FeModulo.FormatarNumero(txtDesconto.Text.ToString(), FeModulo.eCasaDecimais.Duas);

            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FeModulo.Moeda(txtDesconto, ref e);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtJuros_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtJuros.Text = FeModulo.FormatarNumero(txtJuros.Text.ToString(), FeModulo.eCasaDecimais.Duas);

            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FeModulo.Moeda(txtValor, ref e);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void txtValor_Enter(object sender, EventArgs e)
        {
            try
            {
                FeModulo.RemovaCaracter(txtValor.Text.ToString());
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void txtValor_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                txtValor.Text = FeModulo.FormatarNumero(txtValor.Text.ToString(), FeModulo.eCasaDecimais.Duas);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            
            {
                iKeyPress = e.KeyChar;

                if (e.KeyChar == (char)27)
                {

                    this.Close();
                }
                else
                {

                    FeModulo.Enter(ref e);
                }
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtId_Enter(object sender, EventArgs e)
        {
            try
            {
                txtId.Clear();
                txtIdCadastro.Clear();  
                txtNrDoc.Text = "";
                txtEmissao.Text = "";
                lblNome.Text = "";

                cboFuncionarios.SelectedIndex = -1;
                cboLancamentos.SelectedIndex = -1;
                cboCobranca.SelectedIndex = -1;
                cboNatureza.SelectedIndex = -1;
                cboTipo.SelectedIndex = -1;
                btnQuitarContas.Enabled = true;

                labVencidas.Text = "";
                Limpar();
            }
            catch (Exception Ex)
            {
                Msgbox(Ex); 
            }
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            try
            {
                txtId.Focus();
            }
            catch (Exception Ex)
            {

                Msgbox(Ex); 
            }
        }

        private void Limpar()
            {
            try
            {
                txtNrItemPc.Clear();
                txtNrParcela.Clear();
                txtVencimento.Clear();
                txtValor.Text = "";
                txtJuros.Text = "";
                txtDesconto.Text = "";
                txtComplemento.Text = "";
                txtPontoVenda.Text = "";
                btnGravar.Enabled = true;
                btnExcluirContas.Enabled = false;
                btnExcluirContas2Itens.Enabled = false;
                PreencheGrid("");
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private int Gravar()
        {
            int _Result = 0;

            try
            {
                if (!Consiste()) return 0;

                NeContas _NeContas = new NeContas();
                _NeContas.Id = Convert.ToInt32("0" + txtId.Text.ToString());
                _NeContas.NrParcela = Convert.ToInt32("0" + txtNrParcela.Text.ToString());

                if (_PsContas != null)
                {
                    _NeContas.Limpar();

                    _NeContas.Id = Convert.ToInt32("0" + txtId.Text.ToString());

                    _NeContas.NrDoc = txtNrDoc.Text.ToString();
                    _NeContas.VendedorId = Convert.ToInt32(cboFuncionarios.SelectedValue == null ? 0 : cboFuncionarios.SelectedValue);

                    _NeContas.CadastroId = txtIdCadastro.Text == null ? 0 : Convert.ToInt32("0" + txtIdCadastro.Text.ToString());

                    _NeContas.LancamentoId = Convert.ToInt32(cboLancamentos.SelectedValue == null ? 0 : cboLancamentos.SelectedValue);
                    _NeContas.CobrancaId = Convert.ToInt32(cboCobranca.SelectedValue == null ? 0 : cboCobranca.SelectedValue);
                    _NeContas.Tipo = Convert.ToInt32(cboTipo.SelectedValue == null ? 0 : cboTipo.SelectedValue);
                    _NeContas.Natureza = Convert.ToString(cboNatureza.SelectedValue == null ? "CR" : cboNatureza.SelectedValue);

                    string _Data = "";
                    FeModulo.IsDate(txtEmissao.Text.ToString(), ref _Data);
                    _NeContas.DataEmissao = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy") : "";

                    _NeContas.ValorTotal = _NeContas.ValorTotal + float.Parse("0" + labValor.Text);
                    _NeContas.DescontoTotal = _NeContas.DescontoTotal + float.Parse("0" + labGeral.Text);
                    _NeContas.JurosTotal = _NeContas.JurosTotal + float.Parse("0" + labJuros.Text);

                    _NeContas.NrParcela = Convert.ToInt32("0" + txtNrParcela.Text.ToString());

                    _Data = "";
                    FeModulo.IsDate(txtVencimento.Text.ToString(), ref _Data);
                    _NeContas.DtVencimento = _Data.Length >= 8 ? Convert.ToDateTime(_Data).ToString("dd/MM/yyyy") : "";

                    _NeContas.VrVencimento = float.Parse("0" + txtValor.Text.ToString());
                    _NeContas.VrJuros = float.Parse("0" + txtJuros.Text.ToString());
                    _NeContas.VrDesconto = float.Parse("0" + txtDesconto.Text.ToString());
                    _NeContas.Complemento = txtComplemento.Text.ToString();
                    _NeContas.PontoVenda = txtPontoVenda.Text.ToString();
                    _NeContas.VendaId = varVendaId;

                    int _RecordAffected = 0;
                    _NeContas = _PsContas.GravarProcedure(_NeContas, ref _RecordAffected);

                    txtId.Text = _NeContas.Id.ToString();

                    txtNrItemPc.Focus();

                    PreencheGrid(txtNrDoc.Text.ToString());
                }
                else
                {
                    Limpar();
                }
                txtNrItemPc.FindForm();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return _Result;
        }

        private void MostrarContas(NeContas  pNeContas)
        {
            try
            {
                txtNrDoc.Text = pNeContas.NrDoc.ToString();

                DateTime _DateTime;
                if (!string.IsNullOrEmpty(pNeContas.DataEmissao.ToString()))
                {
                    _DateTime = Convert.ToDateTime(pNeContas.DataEmissao.ToString());
                    txtEmissao.Text = _DateTime.ToString("dd/MM/yyyy").ToString();
                }
                else
                    txtEmissao.Clear();
  
                txtIdCadastro.Text = pNeContas.CadastroId.ToString();   
                cboLancamentos.SelectedValue = pNeContas.LancamentoId;
                cboCobranca.SelectedValue = pNeContas.CobrancaId;
                cboFuncionarios.SelectedValue = pNeContas.VendedorId;
                cboNatureza.SelectedValue = pNeContas.Natureza.ToString();
                cboTipo.SelectedValue = pNeContas.Tipo;
                varVendaId = pNeContas.VendaId;
                PesquisarCadastro(); 
 
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void MostrarCadastro(NeCadastros pNeCadastros)
        {
            try
            {
                DateTime _dtHoje = new DateTime();
                _dtHoje = DateTime.Now;

                lblNome.Text = pNeCadastros.Nome.ToString().Trim() + " / " + pNeCadastros.Fantasia.ToString().Trim();

                // TODAS QUE ESTAO EM ABERTO
                NeContas _NeContas = new NeContas();
                _NeContas.CadastroId = Convert.ToInt32("0" + txtIdCadastro.Text.ToString());

                PsContas _PsContas = new PsContas(ConnectionString);
                _NeContas = _PsContas.PesquisaContas(_NeContas);

                labCP.Text = _NeContas.ContasPagar.ToString();
                labCR.Text = _NeContas.ContasReceber.ToString();
                labSaldo.Text = _NeContas.Saldo.ToString();

                // VENCIDAS
                _NeContas.CadastroId = Convert.ToInt32("0" + txtIdCadastro.Text.ToString());
                _NeContas = _PsContas.PesquisaContasVencidas(_NeContas);
                labVencidas.Text = FeModulo.FormatarNumero(_NeContas.ContasReceber.ToString(), FeModulo.eCasaDecimais.Duas) + " até " + _dtHoje.ToString("dd/MM/yyyy");

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void MostrarParcela(NeContas pNeContas)
        {
            try
            {
                txtNrItemPc.Text  = pNeContas.ParcelaId.ToString();
                txtNrParcela.Text = pNeContas.NrParcela.ToString();

                DateTime _DateTime;
                if (!string.IsNullOrEmpty(pNeContas.DtVencimento.ToString()))
                {
                    _DateTime = Convert.ToDateTime(pNeContas.DtVencimento.ToString());
                    txtVencimento.Text = _DateTime.ToString("dd/MM/yyyy").ToString();
                }
                else
                    txtVencimento.Clear();
               
                txtValor.Text = FeModulo.FormatarNumero(pNeContas.VrVencimento.ToString(), FeModulo.eCasaDecimais.Duas);
                txtJuros.Text = FeModulo.FormatarNumero(pNeContas.VrJuros.ToString(), FeModulo.eCasaDecimais.Duas);
                txtDesconto.Text = FeModulo.FormatarNumero(pNeContas.VrDesconto.ToString(), FeModulo.eCasaDecimais.Duas);
                txtComplemento.Text = pNeContas.Complemento.ToString();
                txtPontoVenda.Text = pNeContas.PontoVenda.ToString();
                varVendaId = Convert.ToInt32("0" + pNeContas.VendaId.ToString());
               
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private bool Consiste()
        {
            bool _Result = false ;
            try
            {

                if (string.IsNullOrEmpty(txtNrDoc.Text))
                {
                    MessageBox.Show(this, "O numero do documento não foi informado.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtNrDoc.Focus();
                }
                else if (txtEmissao.Text.Trim().Length < 8)
                {
                    MessageBox.Show(this, "Informe a data de emissão.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtEmissao.Focus();
                }
                else if (string.IsNullOrEmpty(txtIdCadastro.Text))
                {
                    MessageBox.Show(this, "informe o codigo do cadastro.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtIdCadastro.Focus();
                }
                else if (cboNatureza.SelectedValue ==null )
                {
                    MessageBox.Show(this, "Informe a natureza da conta.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cboNatureza.Focus();
                }
                else if (cboTipo.SelectedValue == null)
                {
                    MessageBox.Show(this, "Informe o Tipo da conta.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cboTipo.Focus();
                }
                else if (cboLancamentos.SelectedValue == null)
                {
                    MessageBox.Show(this, "Informe o Tipo de Lançamento.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cboLancamentos.Focus();
                }
                else if (cboCobranca.SelectedValue == null)
                {
                    MessageBox.Show(this, "Informe o Tipo da Cobrança.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cboCobranca.Focus();
                }
                else if (cboFuncionarios.SelectedValue == null)
                {
                    MessageBox.Show(this, "Informe o vendedor.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    cboFuncionarios.Focus();
                }
                else if (string.IsNullOrEmpty(txtNrParcela.Text))
                {
                    MessageBox.Show(this, "Informe o nr da parcela.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtNrParcela.Focus();
                }
                else if (txtVencimento.Text.Trim().Length < 8)
                {
                    MessageBox.Show(this, "Informe a data de vencimento.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtVencimento.Focus();
                }
                else if (txtVencimento.Text.Trim().Length < 8)
                {
                    MessageBox.Show(this, "Informe a data de vencimento.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtVencimento.Focus();
                }
                else if (float.Parse (txtValor.Text) <=0)
                {
                    MessageBox.Show(this, "Informe o valor da parcela.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtValor.Focus();
                }
                else
                    _Result = true;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
            return _Result;
        }

        private void PreencheGrid(string pNrDoc)
        {
            try
            {
                //dtgContas.DataSource = _PsContas.LerTabelaParcelas(txtNrDoc.Text.ToString());
                dtgContas.DataSource = _PsContas.LerTabelaParcelas(txtId.Text.ToString());
                SomarColunas(); 

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void SomarColunas()
        {
            try
            {
                double varValor = 0;
                double varJuros = 0;
                double varDesconto = 0;
                double varQuitado = 0;
                foreach (DataGridViewRow row in this.dtgContas.Rows)
                {
                    if (row.Cells["I04"].Value != null)
                    {
                        varValor = (varValor + Convert.ToDouble("0" + row.Cells["I04"].Value.ToString()));
                    }
                    if (row.Cells["I05"].Value != null)
                    {
                        varJuros = (varJuros + Convert.ToDouble("0" + row.Cells["I05"].Value.ToString()));
                    }
                    if (row.Cells["I06"].Value != null)
                    {
                        varDesconto = (varDesconto + Convert.ToDouble("0" + row.Cells["I06"].Value.ToString()));
                    }
                    if (row.Cells["I12"].Value != null)
                    {
                        varQuitado = (varQuitado + Convert.ToDouble("0" + row.Cells["I12"].Value.ToString()));
                    }

                }

                labValor.Text = FeModulo.FormatarNumero(varValor.ToString(), FeModulo.eCasaDecimais.Duas);
                labJuros.Text = FeModulo.FormatarNumero(varJuros.ToString(), FeModulo.eCasaDecimais.Duas);
                labDesconto.Text = FeModulo.FormatarNumero(varDesconto.ToString(), FeModulo.eCasaDecimais.Duas);

                double varGeral = 0;
                varGeral = (Convert.ToDouble("0" + labValor.Text) - Convert.ToDouble("0" + labDesconto.Text));
                labGeral.Text = FeModulo.FormatarNumero(varGeral.ToString(), FeModulo.eCasaDecimais.Duas);
                
                labQuitado.Text = FeModulo.FormatarNumero(varQuitado.ToString(), FeModulo.eCasaDecimais.Duas);

                double varPendente = 0;
                varPendente = varGeral - (varQuitado-varDesconto);
                labPendente.Text = FeModulo.FormatarNumero(varPendente.ToString(), FeModulo.eCasaDecimais.Duas);
            
            }

            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private int PesquisarContas()
        {
            int _Result = 0;

            try
            {
                NeContas  _NeContas = new NeContas();
                _NeContas.Id = Convert.ToInt32("0" + txtId.Text.ToString());
                _NeContas = _PsContas.PesquisarContas(_NeContas);

                if(_NeContas.Id>=0)
                MostrarContas(_NeContas);

                _Result = _NeContas.Id;

                if (_Result != 0)
                {
                    txtNrItemPc.Focus();
                }
                else
                {
                    PesquisarCadastro();
                    txtNrDoc.Focus();
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return _Result;
        }

        private int PesquisarCadastro()
        {
            int _Result = 0;
            try
            {
                NeCadastros _NeCadastros = new NeCadastros();
                PsCadastros _PsCadastros = new PsCadastros ();
                _PsCadastros = new PsCadastros(ConnectionString);
                _NeCadastros.Id  = Convert.ToInt32("0" + txtIdCadastro.Text.ToString());
                _NeCadastros = _PsCadastros.Pesquisar(_NeCadastros);

                if (_NeCadastros.Id > 0)
                    MostrarCadastro(_NeCadastros);

                _Result = _NeCadastros.Id;

                if (_Result > 0)
                {
                    txtNrItemPc.Focus();
                }
                else
                {
                    txtId.Focus();
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return _Result;
        }

        private int PesquisarParcela()
        {
            int _Result = 0;

            try
            {
                NeContas _NeContas = new NeContas();
                _NeContas.Id = Convert.ToInt32("0" + txtId.Text.ToString());
                _NeContas.ParcelaId = Convert.ToInt32("0" + txtNrItemPc.Text.ToString());
                _NeContas.NrParcela = Convert.ToInt32("0" + txtNrParcela.Text.ToString());
                _NeContas = _PsContas.PesquisarContas2Itens(_NeContas);

                if (_NeContas.NrParcela >= 0)
                    MostrarParcela(_NeContas);

                _Result = _NeContas.ParcelaId;

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return _Result;
        }

        private void txtNrItemPc_Validating(object sender, CancelEventArgs e)
        {
            FeModulo.RemovaCaracter(txtNrItemPc.Text.ToString());
            txtJuros.Text = "0.00";
            txtDesconto.Text = "0.00";
        }

        private void txtJuros_Keypress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FeModulo.Moeda(txtJuros, ref e);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void txtJuros_Enter(object sender, EventArgs e)
        {
            try
            {
                FeModulo.RemovaCaracter(txtJuros.Text.ToString());
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void txtNrParcela_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FeModulo.Numero(ref e);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Gravar();
        }

        private void txtEmissao_Validating(object sender, CancelEventArgs e)
        {
            string _Data = "";
            if (!FeModulo.IsDate(txtEmissao.Text.ToString(), ref _Data))
            {
                MessageBox.Show("Emissao não está correto.");
                txtEmissao.Focus();
            }
            else
                txtEmissao.Text = _Data;
            txtIdCadastro.Focus();

        }

        private void txtVencimento_Validating(object sender, CancelEventArgs e)
        {
            string _Data = "";
            if (!FeModulo.IsDate(txtVencimento.Text.ToString(), ref _Data))
            {
                MessageBox.Show("Vencimento não está correto.");
                txtVencimento.Focus();
            }
            else
                txtVencimento.Text = _Data;
        }

        private void txtNrItemPc_Enter(object sender, EventArgs e)
        {
            Limpar();
        }

        private void dtgContas_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgContas.CurrentRow.Cells["I02"].Value != null)
                {
                    txtNrItemPc.Text = dtgContas.CurrentRow.Cells["I01"].Value.ToString();  
                    txtNrParcela.Text = dtgContas.CurrentRow.Cells["I02"].Value.ToString();
                    PesquisarParcela();
                    btnQuitarContas.Enabled = true;
                }
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void txtValor_Validating(object sender, EventArgs e)
        {
            try
            {
                txtValor.Text = FeModulo.FormatarNumero(txtValor.Text.ToString(), FeModulo.eCasaDecimais.Duas);    
            }
            catch (Exception Ex)
            {
                Msgbox (Ex);
            }
        }

        private void txtJuros_Validating(object sender, EventArgs e)
        {
            try
            {
                txtJuros.Text = FeModulo.FormatarNumero(txtJuros.Text.ToString(), FeModulo.eCasaDecimais.Duas);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }

        }

        private void btnGravar_click(object sender, EventArgs e)
        {
            Gravar();
        }

        private void btnQuitarContas_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgContas.RowCount <= 0)
                {
                    MessageBox.Show(this, "Não há contas para quitação.", "Atenção", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    btnQuitarContas.Enabled = false;
                    txtNrItemPc.Focus();
                }
                else
                {
                    if (dtgContas.CurrentRow.Cells["I01"].Value != null)
                    {
                        frmContasQuitar _frmContasQuitar = new frmContasQuitar();
                        _frmContasQuitar.ContaI_Id = Convert.ToInt32("0" + dtgContas.CurrentRow.Cells["I01"].Value.ToString());
                        _frmContasQuitar.VendaId = varVendaId;
                        _frmContasQuitar.Quitado = false;
                        _frmContasQuitar.ShowDialog();
                        PreencheGrid("");

                    }
                }

            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtComplemento_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                btnGravar.Focus();
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtIdCadastro_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                PesquisarCadastro();
            }
            catch (Exception Ex)
            {
                
                Msgbox(Ex)  ;
            }
        }


        private void txtIdCadastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FeModulo.Numero(ref e);
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void dtgContas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (btnQuitarContas.Enabled)
                {
                    btnQuitarContas_Click(sender, null);
                    PreencheGrid(txtNrDoc.Text.ToString());
                }
            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

        private void txtIdCadastro_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)//::Pesquisar registros
                {
                    frmPesquisaCadastro _frmPesquisaCadastro = new frmPesquisaCadastro();
                    _frmPesquisaCadastro.ShowDialog();
                    txtIdCadastro.Text = _frmPesquisaCadastro.CodigoId.ToString();
                    PesquisarCadastro();
                    if (string.IsNullOrEmpty(txtIdCadastro.Text.Trim())) txtId.Focus();
                }
            }
            catch (Exception Ex)
            {
                Msgbox(Ex);
            }

        }

        private void txtId_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                PesquisarContas();
            }
            catch (Exception Ex)
            {
                Msgbox(Ex);
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Tem certeza que deseja encerrar o formulário agora?", "Atenção", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception Ex)
            {
                
                Msgbox(Ex);
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)//::Pesquisar registros
                {
                    frmPesquisaContas _frmPesquisaContas = new frmPesquisaContas();
                    _frmPesquisaContas.ShowDialog();
                    txtId.Text = _frmPesquisaContas.CodigoId.ToString();
                    PesquisarContas();
                    if (string.IsNullOrEmpty(txtId.Text.Trim()))
                    {
                        txtId.Focus();
                    }
                    else
                    {
                        PreencheGrid("");
                    }
                }


            }
            catch (Exception Ex)
            {

                Msgbox(Ex);
            }
        }

 
         private void btnExcluirContas2Itens_Click(object sender, EventArgs e)
        {
            if (dtgContas.CurrentRow.Cells["I01"].Value != null)
            {
                NeContas _NeContas = new NeContas();
                _NeContas.ParcelaId = Convert.ToInt32(dtgContas.CurrentRow.Cells["I01"].Value);
                _NeContas = _PsContas.PesquisarContas2ItensID(_NeContas);

                if (!string.IsNullOrEmpty(_NeContas.DtEncerrada.Trim()))
                {
                    MessageBox.Show(this, "Esta parcela não pode ser excluida! Cupom Emitido, não insista! - Data: " + _NeContas.DtEncerrada, "Cadastro de Contas", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    txtNrItemPc.Focus();
                    txtLibera.Text = "";
                    return;
                }

                if (_PsContas.DeletaContas2itens(_NeContas) > 0)
                {
                    MessageBox.Show(this, "Parcela excluida do cadastro de contas : " + _NeContas.ParcelaId.ToString().Trim(), "Atenção", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    PreencheGrid(txtNrDoc.Text.ToString());
                    if (dtgContas.RowCount <= 0)
                    {
                        _NeContas.Id = Convert.ToInt32("0" + txtId.Text.ToString());
                        _NeContas = _PsContas.PesquisarContas(_NeContas);

                        if (_PsContas.DeletaContas(_NeContas) > 0)
                        {
                            MessageBox.Show(this, "Excluido a conta do cliente : " + _NeContas.Id.ToString().Trim(), "Atenção", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                            txtId.Focus();
                        }
                    }
                    txtLibera.Text = "";
                }
            }

        }

         private void btnExcluirContas_Click(object sender, EventArgs e)
         {
             try
             {
                 NeContas _NeContas = new NeContas();
                 _NeContas.Id = Convert.ToInt32("0" + txtId.Text.ToString());
                 _NeContas = _PsContas.PesquisarContas(_NeContas);

                 if (_PsContas.DeletaContas(_NeContas) > 0)
                 {
                     MessageBox.Show(this, "Excluido a conta do cliente : " + _NeContas.Id.ToString().Trim(), "Atenção", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                     txtId.Focus();
                 }
             }
             catch (Exception Ex)
             {
                 Msgbox(Ex);
             }
         }

         private void txtLibera_Click(object sender, CancelEventArgs e)
         {
             try
             {
                 if (txtLibera.Text.Trim() == "117")
                 {
                     btnExcluirContas2Itens.Enabled = true;
                 }
             }
             catch (Exception Ex)
             {

                 Msgbox(Ex); 
             }
         }


        
    }
}
