using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Programax.Easy.View.ClassesAuxiliares;
using Programax.Easy.View.Componentes;
using Programax.Infraestrutura.Negocio.Utils;
using Programax.Easy.Negocio.Financeiro.PlanoContasDreObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.PlanoContasServ;
using Programax.Easy.Negocio.Financeiro.Enumeradores;
using Programax.Easy.Servico.Financeiro.PlanoContasDreServ;
using Programax.Easy.Negocio.Financeiro.CategoriaObj.ObjetoDeNegocio;
using Programax.Easy.Servico.Financeiro.CategoriaServ;

namespace Programax.Easy.View.Telas.Financeiro.PlanosDeContas
{
    public partial class FormCadastroPlanoContaDre : FormularioPadrao
    {
        #region " VARIÁVEIS PRIVADAS "

        private List<PlanoContaDre> _listaDePlanosDeContas;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraGrid.GridControl gcPlanoDeContas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroPlanoContas;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Columns.GridColumn coluanNatureza;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTipo;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNrPlanoContasContador;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlanoContas;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LookUpEdit cboTipoPlanoDeContas;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.LookUpEdit cboNaturezaPlanoDeContas;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Button btnGravar;
        private Button button2;
        private Button button3;
        private DevExpress.XtraEditors.LookUpEdit cboCategorias;
        private int _idPlanoContas;

        #endregion

        #region " CONSTRUTOR "

        public FormCadastroPlanoContaDre()
        {
            InitializeComponent();

            txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            PreenchaOStatus();
            PreenchaCboTipo();
            PreenchaCboNatureza();
            PreenchaCboCategorias();

            AtualizarGridDePlanoDeContas();

            this.ActiveControl = txtNumeroPlanoContas;

            this.NomeDaTela = "Plano de Contas D.R.E";
        }

        #endregion

        #region " EVENTOS CONTROLES "

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.FecharFormulario();
        }

        private void pbPesquisaPessoa_Click(object sender, EventArgs e)
        {

        }

        private void gcPlanoDeContas_DoubleClick(object sender, EventArgs e)
        {
            SelecionePlanoDeContas();
        }

        private void gcPlanoDeContas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelecionePlanoDeContas();
            }
        }

        private void txtNumeroPlanoContas_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNumeroPlanoContas.Text))
            {
                ServicoPlanoDeContasDre servicoPlanoContasDre = new ServicoPlanoDeContasDre();

                var planoContasDRE = servicoPlanoContasDre.ConsultePlanoDeContasPeloNumero(txtNumeroPlanoContas.Text);

                EditePlanoDeContas(planoContasDRE);
            }
            else
            {
                EditePlanoDeContas(null);
            }
        }

        #endregion

        #region " MÉTODOS AUXILIARES "

        private void LimpeFormulario()
        {
            AtualizarGridDePlanoDeContas();
            EditePlanoDeContas(null, limparNumeroPlanoContas: true, focoNumeroPlanoContas: true);
        }

        private void EditePlanoDeContas(PlanoContaDre planoDeContas, bool limparNumeroPlanoContas = false, bool focoNumeroPlanoContas = false)
        {
            if (planoDeContas != null)
            {
                int? categoriaId = cboCategorias.EditValue.ToInt();
                _idPlanoContas = planoDeContas.Id;
                txtDescricao.Text = planoDeContas.Descricao;
                txtDataCadastro.Text = planoDeContas.DataCadastro.ToString("dd/MM/yyyy");
                txtNumeroPlanoContas.Text = planoDeContas.NumeroPlanoDeContas;
                cboNaturezaPlanoDeContas.EditValue = planoDeContas.NaturezaPlanoContas;
                cboTipoPlanoDeContas.EditValue = planoDeContas.TipoPlanoContas;

                cboStatus.EditValue = planoDeContas.Status;

                cboCategorias.EditValue = planoDeContas.NumeroPlanoContasContador.ToInt();

                if (planoDeContas.PlanoDeContasPadrao)
                {
                    txtDescricao.Enabled = false;
                    txtNumeroPlanoContas.Enabled = false;
                    txtDataCadastro.Enabled = false;

                    cboStatus.Enabled = false;
                    cboNaturezaPlanoDeContas.Enabled = false;
                    cboTipoPlanoDeContas.Enabled = false;
                }

                txtDescricao.Focus();
            }
            else
            {
                _idPlanoContas = 0;
                txtDescricao.Text = string.Empty;

                if (limparNumeroPlanoContas)
                {
                    txtNumeroPlanoContas.Text = string.Empty;
                }

                txtDataCadastro.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

                cboStatus.EditValue = "A";
                cboNaturezaPlanoDeContas.EditValue = null;
                cboTipoPlanoDeContas.EditValue = null;

                txtDescricao.Enabled = true;
                txtNumeroPlanoContas.Enabled = true;
                txtDataCadastro.Enabled = true;

                cboStatus.Enabled = true;
                cboNaturezaPlanoDeContas.Enabled = true;
                cboTipoPlanoDeContas.Enabled = true;

               

                if (focoNumeroPlanoContas)
                {
                    txtNumeroPlanoContas.Focus();
                }
            }
        }

        private void PreenchaOStatus()
        {
            ObjetoParaComboBox objetoComboBoxAtivo = new ObjetoParaComboBox();
            objetoComboBoxAtivo.Valor = "A";
            objetoComboBoxAtivo.Descricao = "Ativo";

            ObjetoParaComboBox objetoComboBoxInativo = new ObjetoParaComboBox();
            objetoComboBoxInativo.Valor = "I";
            objetoComboBoxInativo.Descricao = "Inativo";

            List<ObjetoParaComboBox> listaDeItensParaOComboBox = new List<ObjetoParaComboBox>();
            listaDeItensParaOComboBox.Add(objetoComboBoxAtivo);
            listaDeItensParaOComboBox.Add(objetoComboBoxInativo);

            cboStatus.Properties.DataSource = listaDeItensParaOComboBox;
            cboStatus.Properties.ValueMember = "Valor";
            cboStatus.Properties.DisplayMember = "Descricao";

            cboStatus.EditValue = "A";
        }

        private void PreenchaCboCategorias()
        {
            List<CategoriaFinanceira> categoria = new List<CategoriaFinanceira>();

          
            categoria = new ServicoCategoria().ConsulteLista(string.Empty, null, "A");
            

            categoria.Insert(0, null);

            cboCategorias.Properties.DisplayMember = "Descricao";
            cboCategorias.Properties.ValueMember = "Id";
            cboCategorias.Properties.DataSource = categoria;

            if (cboCategorias.EditValue != null)
            {
                if (!categoria.Exists(categ => categ != null && categ.Id == cboCategorias.EditValue.ToInt()))
                {
                    cboCategorias.EditValue = null;
                }
            }
        }

        private void PreenchaCboNatureza()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumNaturezaPlanoContas>();

            lista.Insert(0, null);

            cboNaturezaPlanoDeContas.Properties.DataSource = lista;
            cboNaturezaPlanoDeContas.Properties.ValueMember = "Valor";
            cboNaturezaPlanoDeContas.Properties.DisplayMember = "Descricao";
        }



        private void PreenchaCboTipo()
        {
            var lista = MetodosAuxiliares.RetorneListaDeValoresEnumerador<EnumTipoPlanoContas>();

            lista.Insert(0, null);

            cboTipoPlanoDeContas.Properties.DataSource = lista;
            cboTipoPlanoDeContas.Properties.ValueMember = "Valor";
            cboTipoPlanoDeContas.Properties.DisplayMember = "Descricao";
        }

        private void AtualizarGridDePlanoDeContas()
        {
            ServicoPlanoDeContasDre servicoPlanoDeContas = new ServicoPlanoDeContasDre();
            _listaDePlanosDeContas = servicoPlanoDeContas.ConsulteLista();

            PreencherGrid();
        }

        private void PreencherGrid()
        {
            List<PlanoDeContasAuxiliar> listaDePlanosDeContasAuxiliares = new List<PlanoDeContasAuxiliar>();

            foreach (var planoDeContas in _listaDePlanosDeContas)
            {
                PlanoDeContasAuxiliar planoDeContasAuxiliar = new PlanoDeContasAuxiliar();
             
                planoDeContasAuxiliar.Descricao = planoDeContas.Descricao;
                planoDeContasAuxiliar.Id = planoDeContas.Id;
                planoDeContasAuxiliar.Natureza = planoDeContas.NaturezaPlanoContas != null ? planoDeContas.NaturezaPlanoContas.GetValueOrDefault().Descricao() : string.Empty;
                planoDeContasAuxiliar.NumeroPlanoDeContas = planoDeContas.NumeroPlanoDeContas;
                planoDeContasAuxiliar.Tipo = planoDeContas.TipoPlanoContas != null ? planoDeContas.TipoPlanoContas.GetValueOrDefault().Descricao() : string.Empty;
                planoDeContasAuxiliar.Status = planoDeContas.Status == "A" ? "ATIVO" : "INATIVO";
                planoDeContasAuxiliar.NumeroPlanoDeContasContador = planoDeContas.NumeroPlanoContasContador;

                listaDePlanosDeContasAuxiliares.Add(planoDeContasAuxiliar);
            }

            gcPlanoDeContas.DataSource = listaDePlanosDeContasAuxiliares;
            gcPlanoDeContas.RefreshDataSource();
        }

        private void SelecionePlanoDeContas()
        {
            if (_listaDePlanosDeContas != null && _listaDePlanosDeContas.Count > 0)
            {
                ServicoPlanoDeContasDre servicoPlanoDeContas = new ServicoPlanoDeContasDre();

                var planoDeContas = servicoPlanoDeContas.Consulte(Convert.ToInt32(colunaId.View.GetFocusedRowCellValue(colunaId)));

                EditePlanoDeContas(planoDeContas);
            }
        }

        private void PesquisePeloId()
        {
            ServicoPlanoDeContasDre servicoPlanoDeContas = new ServicoPlanoDeContasDre();
            var planoDeContas = servicoPlanoDeContas.Consulte(_idPlanoContas);

            EditePlanoDeContas(planoDeContas);
        }

        private void Salve()
        {
            Action actionSalvar = () =>
            {
                var planoDeContasDre = RetornePlanoDeContasEmEdicao();

                ServicoPlanoDeContasDre servicoPlanoDeContasDre = new ServicoPlanoDeContasDre();

                if (planoDeContasDre.Id == 0)
                {
                    servicoPlanoDeContasDre.Cadastre(planoDeContasDre);
                }
                else
                {
                    servicoPlanoDeContasDre.Atualize(planoDeContasDre);
                }

                _idPlanoContas = planoDeContasDre.Id;

                AtualizarGridDePlanoDeContas();
                PesquisePeloId();
            };

            TratamentosDeTela.TrateInclusaoEAtualizacao(actionSalvar);
        }

        private PlanoContaDre RetornePlanoDeContasEmEdicao()
        {
            PlanoContaDre planoDeContas = new PlanoContaDre();
            int? categoriaId = cboCategorias.EditValue.ToInt();

            planoDeContas.Id = _idPlanoContas;
            planoDeContas.Descricao = txtDescricao.Text;
            planoDeContas.DataCadastro = txtDataCadastro.Text.ToDate();
            planoDeContas.NaturezaPlanoContas = (EnumNaturezaPlanoContas?)cboNaturezaPlanoDeContas.EditValue;
            planoDeContas.NumeroPlanoDeContas = txtNumeroPlanoContas.Text;
            planoDeContas.TipoPlanoContas = (EnumTipoPlanoContas?)cboTipoPlanoDeContas.EditValue;
            planoDeContas.Status = cboStatus.EditValue.ToString();
            planoDeContas.NumeroPlanoContasContador = categoriaId.ToString();

            return planoDeContas;
        }

        #endregion

        private void InitializeComponent()
        {
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.gcPlanoDeContas = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroPlanoContas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDescricao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coluanNatureza = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNrPlanoContasContador = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtNumeroPlanoContas = new DevExpress.XtraEditors.TextEdit();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.cboTipoPlanoDeContas = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.cboNaturezaPlanoDeContas = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnGravar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cboCategorias = new DevExpress.XtraEditors.LookUpEdit();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlanoDeContas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoPlanoDeContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNaturezaPlanoDeContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.btnGravar);
            this.painelBotoes.Controls.Add(this.button2);
            this.painelBotoes.Controls.Add(this.button3);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.cboCategorias);
            this.panelConteudo.Controls.Add(this.labelControl6);
            this.panelConteudo.Controls.Add(this.txtDataCadastro);
            this.panelConteudo.Controls.Add(this.gcPlanoDeContas);
            this.panelConteudo.Controls.Add(this.txtNumeroPlanoContas);
            this.panelConteudo.Controls.Add(this.txtDescricao);
            this.panelConteudo.Controls.Add(this.cboTipoPlanoDeContas);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.pbPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.cboNaturezaPlanoDeContas);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Size = new System.Drawing.Size(846, 347);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(492, 52);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(45, 13);
            this.labelControl6.TabIndex = 523;
            this.labelControl6.Text = "Categoria";
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(728, 68);
            this.txtDataCadastro.Name = "txtDataCadastro";
            this.txtDataCadastro.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataCadastro.Properties.Appearance.Options.UseFont = true;
            this.txtDataCadastro.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDataCadastro.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDataCadastro.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDataCadastro.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDataCadastro.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2})(([\\.][0-9]{3})([\\.][0-9]{4})?)?)?";
            this.txtDataCadastro.Properties.MaxLength = 50;
            this.txtDataCadastro.Properties.ReadOnly = true;
            this.txtDataCadastro.Size = new System.Drawing.Size(114, 22);
            this.txtDataCadastro.TabIndex = 514;
            this.txtDataCadastro.TabStop = false;
            // 
            // gcPlanoDeContas
            // 
            this.gcPlanoDeContas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPlanoDeContas.Location = new System.Drawing.Point(4, 96);
            this.gcPlanoDeContas.MainView = this.gridView5;
            this.gcPlanoDeContas.Name = "gcPlanoDeContas";
            this.gcPlanoDeContas.Size = new System.Drawing.Size(838, 247);
            this.gcPlanoDeContas.TabIndex = 522;
            this.gcPlanoDeContas.TabStop = false;
            this.gcPlanoDeContas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcPlanoDeContas.Click += new System.EventHandler(this.gcPlanoDeContas_Click);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colunaId,
            this.colunaNumeroPlanoContas,
            this.colunaDescricao,
            this.colunaStatus,
            this.coluanNatureza,
            this.colunaTipo,
            this.colunaNrPlanoContasContador});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcPlanoDeContas;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.ViewCaption = "Planos de Contas";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Cód Cadastro";
            this.colunaId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colunaId.FieldName = "Id";
            this.colunaId.MinWidth = 45;
            this.colunaId.Name = "colunaId";
            this.colunaId.OptionsColumn.AllowEdit = false;
            this.colunaId.OptionsColumn.AllowFocus = false;
            this.colunaId.OptionsFilter.AllowFilter = false;
            this.colunaId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count)});
            this.colunaId.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaId.Width = 83;
            // 
            // colunaNumeroPlanoContas
            // 
            this.colunaNumeroPlanoContas.AppearanceCell.Options.UseTextOptions = true;
            this.colunaNumeroPlanoContas.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaNumeroPlanoContas.Caption = "Nr. Plano de Contas";
            this.colunaNumeroPlanoContas.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colunaNumeroPlanoContas.FieldName = "NumeroPlanoDeContas";
            this.colunaNumeroPlanoContas.MinWidth = 10;
            this.colunaNumeroPlanoContas.Name = "colunaNumeroPlanoContas";
            this.colunaNumeroPlanoContas.OptionsColumn.AllowEdit = false;
            this.colunaNumeroPlanoContas.OptionsColumn.AllowFocus = false;
            this.colunaNumeroPlanoContas.OptionsFilter.AllowFilter = false;
            this.colunaNumeroPlanoContas.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaNumeroPlanoContas.Visible = true;
            this.colunaNumeroPlanoContas.VisibleIndex = 0;
            this.colunaNumeroPlanoContas.Width = 112;
            // 
            // colunaDescricao
            // 
            this.colunaDescricao.Caption = "Descrição";
            this.colunaDescricao.FieldName = "Descricao";
            this.colunaDescricao.Name = "colunaDescricao";
            this.colunaDescricao.OptionsColumn.AllowEdit = false;
            this.colunaDescricao.OptionsColumn.AllowFocus = false;
            this.colunaDescricao.OptionsFilter.AllowFilter = false;
            this.colunaDescricao.Visible = true;
            this.colunaDescricao.VisibleIndex = 1;
            this.colunaDescricao.Width = 343;
            // 
            // colunaStatus
            // 
            this.colunaStatus.Caption = "Status";
            this.colunaStatus.FieldName = "Status";
            this.colunaStatus.Name = "colunaStatus";
            this.colunaStatus.OptionsColumn.AllowEdit = false;
            this.colunaStatus.OptionsColumn.AllowFocus = false;
            this.colunaStatus.OptionsFilter.AllowFilter = false;
            this.colunaStatus.Visible = true;
            this.colunaStatus.VisibleIndex = 2;
            this.colunaStatus.Width = 94;
            // 
            // coluanNatureza
            // 
            this.coluanNatureza.Caption = "Natureza";
            this.coluanNatureza.FieldName = "Natureza";
            this.coluanNatureza.Name = "coluanNatureza";
            this.coluanNatureza.OptionsColumn.AllowEdit = false;
            this.coluanNatureza.OptionsColumn.AllowFocus = false;
            this.coluanNatureza.OptionsFilter.AllowFilter = false;
            this.coluanNatureza.Visible = true;
            this.coluanNatureza.VisibleIndex = 3;
            this.coluanNatureza.Width = 116;
            // 
            // colunaTipo
            // 
            this.colunaTipo.Caption = "Tipo";
            this.colunaTipo.FieldName = "Tipo";
            this.colunaTipo.Name = "colunaTipo";
            this.colunaTipo.OptionsColumn.AllowEdit = false;
            this.colunaTipo.OptionsColumn.AllowFocus = false;
            this.colunaTipo.OptionsFilter.AllowFilter = false;
            this.colunaTipo.Visible = true;
            this.colunaTipo.VisibleIndex = 4;
            this.colunaTipo.Width = 83;
            // 
            // colunaNrPlanoContasContador
            // 
            this.colunaNrPlanoContasContador.Caption = "Categoria";
            this.colunaNrPlanoContasContador.FieldName = "NumeroPlanoDeContasContador";
            this.colunaNrPlanoContasContador.Name = "colunaNrPlanoContasContador";
            this.colunaNrPlanoContasContador.OptionsColumn.AllowEdit = false;
            this.colunaNrPlanoContasContador.OptionsColumn.AllowFocus = false;
            this.colunaNrPlanoContasContador.OptionsFilter.AllowFilter = false;
            this.colunaNrPlanoContasContador.Visible = true;
            this.colunaNrPlanoContasContador.VisibleIndex = 5;
            this.colunaNrPlanoContasContador.Width = 88;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPlanoDeContas;
            this.gridView2.Name = "gridView2";
            // 
            // txtNumeroPlanoContas
            // 
            this.txtNumeroPlanoContas.EnterMoveNextControl = true;
            this.txtNumeroPlanoContas.Location = new System.Drawing.Point(4, 19);
            this.txtNumeroPlanoContas.Name = "txtNumeroPlanoContas";
            this.txtNumeroPlanoContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPlanoContas.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlanoContas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPlanoContas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPlanoContas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroPlanoContas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroPlanoContas.Properties.Mask.EditMask = "99/";
            this.txtNumeroPlanoContas.Properties.MaxLength = 50;
            this.txtNumeroPlanoContas.Size = new System.Drawing.Size(230, 22);
            this.txtNumeroPlanoContas.TabIndex = 508;
            this.txtNumeroPlanoContas.EditValueChanged += new System.EventHandler(this.txtNumeroPlanoContas_EditValueChanged);
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(268, 19);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 200;
            this.txtDescricao.Size = new System.Drawing.Size(574, 22);
            this.txtDescricao.TabIndex = 509;
            // 
            // cboTipoPlanoDeContas
            // 
            this.cboTipoPlanoDeContas.EnterMoveNextControl = true;
            this.cboTipoPlanoDeContas.Location = new System.Drawing.Point(167, 68);
            this.cboTipoPlanoDeContas.Name = "cboTipoPlanoDeContas";
            this.cboTipoPlanoDeContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoPlanoDeContas.Properties.Appearance.Options.UseFont = true;
            this.cboTipoPlanoDeContas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTipoPlanoDeContas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboTipoPlanoDeContas.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboTipoPlanoDeContas.Properties.DropDownRows = 3;
            this.cboTipoPlanoDeContas.Properties.NullText = "";
            this.cboTipoPlanoDeContas.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboTipoPlanoDeContas.Size = new System.Drawing.Size(157, 22);
            this.cboTipoPlanoDeContas.TabIndex = 511;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(268, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 516;
            this.labelControl1.Text = "Descrição";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(167, 52);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(21, 13);
            this.labelControl5.TabIndex = 521;
            this.labelControl5.Text = "Tipo";
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(240, 19);
            this.pbPesquisaPessoa.Name = "pbPesquisaPessoa";
            this.pbPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaPessoa.TabIndex = 515;
            this.pbPesquisaPessoa.TabStop = false;
            // 
            // cboNaturezaPlanoDeContas
            // 
            this.cboNaturezaPlanoDeContas.EnterMoveNextControl = true;
            this.cboNaturezaPlanoDeContas.Location = new System.Drawing.Point(4, 68);
            this.cboNaturezaPlanoDeContas.Name = "cboNaturezaPlanoDeContas";
            this.cboNaturezaPlanoDeContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNaturezaPlanoDeContas.Properties.Appearance.Options.UseFont = true;
            this.cboNaturezaPlanoDeContas.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNaturezaPlanoDeContas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboNaturezaPlanoDeContas.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descricao")});
            this.cboNaturezaPlanoDeContas.Properties.DropDownRows = 4;
            this.cboNaturezaPlanoDeContas.Properties.NullText = "";
            this.cboNaturezaPlanoDeContas.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboNaturezaPlanoDeContas.Size = new System.Drawing.Size(157, 22);
            this.cboNaturezaPlanoDeContas.TabIndex = 510;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(4, 52);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 13);
            this.labelControl4.TabIndex = 520;
            this.labelControl4.Text = "Natureza";
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(728, 52);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(59, 13);
            this.labDataCadastro.TabIndex = 517;
            this.labDataCadastro.Text = "Dt. Cadastro";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(330, 68);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 2;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(156, 22);
            this.cboStatus.TabIndex = 512;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(330, 52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 519;
            this.labelControl3.Text = "Status";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(4, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(118, 13);
            this.labelControl2.TabIndex = 518;
            this.labelControl2.Text = "Número Plano de Contas";
            // 
            // btnGravar
            // 
            this.btnGravar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGravar.FlatAppearance.BorderSize = 0;
            this.btnGravar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGravar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGravar.Image = global::Programax.Easy.View.Properties.Resources.iconSalvar;
            this.btnGravar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGravar.Location = new System.Drawing.Point(0, 0);
            this.btnGravar.Margin = new System.Windows.Forms.Padding(0);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(100, 40);
            this.btnGravar.TabIndex = 1004;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(100, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 40);
            this.button2.TabIndex = 1006;
            this.button2.Text = " Limpar";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button3.Location = new System.Drawing.Point(200, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 40);
            this.button3.TabIndex = 1005;
            this.button3.TabStop = false;
            this.button3.Text = " Sair";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cboCategorias
            // 
            this.cboCategorias.EnterMoveNextControl = true;
            this.cboCategorias.Location = new System.Drawing.Point(496, 68);
            this.cboCategorias.Name = "cboCategorias";
            this.cboCategorias.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategorias.Properties.Appearance.Options.UseFont = true;
            this.cboCategorias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCategorias.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Cód. Cadatro", 5, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboCategorias.Properties.DropDownRows = 5;
            this.cboCategorias.Properties.NullText = "";
            this.cboCategorias.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboCategorias.Size = new System.Drawing.Size(226, 22);
            this.cboCategorias.TabIndex = 10150;
            // 
            // FormCadastroPlanoContaDre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(892, 458);
            this.Name = "FormCadastroPlanoContaDre";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlanoDeContas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoPlanoDeContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNaturezaPlanoDeContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCategorias.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private void gcPlanoDeContas_Click(object sender, EventArgs e)
        {
            SelecionePlanoDeContas();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Salve();
        }

        private void txtNumeroPlanoContas_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
