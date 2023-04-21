namespace Programax.Easy.View.Telas.Financeiro.ContasBancarias
{
    partial class FormContaBancariaPesquisa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcContasBancarias = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaPessoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaBanco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaAgencia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaTipoConta = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumero = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.labStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnPesquisaContaBancaria = new System.Windows.Forms.PictureBox();
            this.txtNumeroConta = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBancos = new DevExpress.XtraEditors.LookUpEdit();
            this.labBanco = new DevExpress.XtraEditors.LabelControl();
            this.cboAgencias = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNomePessoa = new DevExpress.XtraEditors.TextEdit();
            this.btnPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl67 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdPessoa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl66 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcContasBancarias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaContaBancaria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroConta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBancos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAgencias.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomePessoa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdPessoa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Size = new System.Drawing.Size(2048, 40);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.cboBancos);
            this.panelConteudo.Controls.Add(this.txtNomePessoa);
            this.panelConteudo.Controls.Add(this.label1);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.btnPesquisaPessoa);
            this.panelConteudo.Controls.Add(this.labBanco);
            this.panelConteudo.Controls.Add(this.txtNumeroConta);
            this.panelConteudo.Controls.Add(this.cboAgencias);
            this.panelConteudo.Controls.Add(this.labelControl67);
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.btnPesquisaContaBancaria);
            this.panelConteudo.Controls.Add(this.txtIdPessoa);
            this.panelConteudo.Controls.Add(this.labStatus);
            this.panelConteudo.Controls.Add(this.gcContasBancarias);
            this.panelConteudo.Controls.Add(this.labelControl66);
            this.panelConteudo.Size = new System.Drawing.Size(901, 317);
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
            this.colunaStatus.VisibleIndex = 5;
            this.colunaStatus.Width = 53;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcContasBancarias;
            this.gridView2.Name = "gridView2";
            // 
            // gcContasBancarias
            // 
            this.gcContasBancarias.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcContasBancarias.Location = new System.Drawing.Point(3, 98);
            this.gcContasBancarias.MainView = this.gridView5;
            this.gcContasBancarias.Name = "gcContasBancarias";
            this.gcContasBancarias.Size = new System.Drawing.Size(893, 213);
            this.gcContasBancarias.TabIndex = 7;
            this.gcContasBancarias.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcContasBancarias.DoubleClick += new System.EventHandler(this.gcNcms_DoubleClick);
            this.gcContasBancarias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNcms_KeyDown);
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
            this.colunaPessoa,
            this.colunaBanco,
            this.colunaAgencia,
            this.colunaTipoConta,
            this.colunaNumero,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcContasBancarias;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Contas Bancárias";
            // 
            // colunaId
            // 
            this.colunaId.AppearanceCell.Options.UseTextOptions = true;
            this.colunaId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.AppearanceHeader.Options.UseTextOptions = true;
            this.colunaId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaId.Caption = "Id";
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
            this.colunaId.Width = 45;
            // 
            // colunaPessoa
            // 
            this.colunaPessoa.Caption = "Pessoa Titular";
            this.colunaPessoa.FieldName = "Pessoa";
            this.colunaPessoa.Name = "colunaPessoa";
            this.colunaPessoa.OptionsColumn.AllowEdit = false;
            this.colunaPessoa.OptionsColumn.AllowFocus = false;
            this.colunaPessoa.OptionsFilter.AllowFilter = false;
            this.colunaPessoa.Visible = true;
            this.colunaPessoa.VisibleIndex = 0;
            this.colunaPessoa.Width = 127;
            // 
            // colunaBanco
            // 
            this.colunaBanco.Caption = "Banco";
            this.colunaBanco.FieldName = "Banco";
            this.colunaBanco.Name = "colunaBanco";
            this.colunaBanco.OptionsColumn.AllowEdit = false;
            this.colunaBanco.OptionsColumn.AllowFocus = false;
            this.colunaBanco.OptionsFilter.AllowFilter = false;
            this.colunaBanco.Visible = true;
            this.colunaBanco.VisibleIndex = 1;
            this.colunaBanco.Width = 108;
            // 
            // colunaAgencia
            // 
            this.colunaAgencia.Caption = "Nome Agência";
            this.colunaAgencia.FieldName = "Agencia";
            this.colunaAgencia.Name = "colunaAgencia";
            this.colunaAgencia.OptionsColumn.AllowEdit = false;
            this.colunaAgencia.OptionsColumn.AllowFocus = false;
            this.colunaAgencia.OptionsFilter.AllowFilter = false;
            this.colunaAgencia.Visible = true;
            this.colunaAgencia.VisibleIndex = 2;
            this.colunaAgencia.Width = 123;
            // 
            // colunaTipoConta
            // 
            this.colunaTipoConta.Caption = "Tipo Conta";
            this.colunaTipoConta.FieldName = "TipoContaBancaria";
            this.colunaTipoConta.Name = "colunaTipoConta";
            this.colunaTipoConta.OptionsColumn.AllowEdit = false;
            this.colunaTipoConta.OptionsColumn.AllowFocus = false;
            this.colunaTipoConta.OptionsFilter.AllowFilter = false;
            this.colunaTipoConta.Visible = true;
            this.colunaTipoConta.VisibleIndex = 3;
            this.colunaTipoConta.Width = 79;
            // 
            // colunaNumero
            // 
            this.colunaNumero.Caption = "Número Agência";
            this.colunaNumero.FieldName = "NumeroContaBancaria";
            this.colunaNumero.Name = "colunaNumero";
            this.colunaNumero.OptionsColumn.AllowEdit = false;
            this.colunaNumero.OptionsColumn.AllowFocus = false;
            this.colunaNumero.OptionsFilter.AllowFilter = false;
            this.colunaNumero.Visible = true;
            this.colunaNumero.VisibleIndex = 4;
            this.colunaNumero.Width = 102;
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(718, 20);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 3;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(148, 22);
            this.cboStatus.TabIndex = 4;
            this.cboStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboStatus_KeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(437, 48);
            this.flowLayoutPanel1.TabIndex = 1049;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(0, 0);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(120, 40);
            this.btnSelecionar.TabIndex = 1047;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnFechar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFechar.Location = new System.Drawing.Point(120, 0);
            this.btnFechar.Margin = new System.Windows.Forms.Padding(0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(100, 40);
            this.btnFechar.TabIndex = 1048;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // labStatus
            // 
            this.labStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStatus.Location = new System.Drawing.Point(718, 3);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(30, 13);
            this.labStatus.TabIndex = 643;
            this.labStatus.Text = "Status";
            // 
            // btnPesquisaContaBancaria
            // 
            this.btnPesquisaContaBancaria.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaContaBancaria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaContaBancaria.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaContaBancaria.Location = new System.Drawing.Point(874, 70);
            this.btnPesquisaContaBancaria.Name = "btnPesquisaContaBancaria";
            this.btnPesquisaContaBancaria.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaContaBancaria.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaContaBancaria.TabIndex = 640;
            this.btnPesquisaContaBancaria.TabStop = false;
            this.btnPesquisaContaBancaria.Click += new System.EventHandler(this.btnPesquisaNcm_Click);
            // 
            // txtNumeroConta
            // 
            this.txtNumeroConta.EnterMoveNextControl = true;
            this.txtNumeroConta.Location = new System.Drawing.Point(546, 20);
            this.txtNumeroConta.Name = "txtNumeroConta";
            this.txtNumeroConta.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroConta.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroConta.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroConta.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroConta.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroConta.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroConta.Properties.Mask.EditMask = "[0-9]{1,30}([-][0-9]{1,30})?";
            this.txtNumeroConta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroConta.Properties.Mask.ShowPlaceHolders = false;
            this.txtNumeroConta.Properties.MaxLength = 50;
            this.txtNumeroConta.Size = new System.Drawing.Size(166, 22);
            this.txtNumeroConta.TabIndex = 3;
            this.txtNumeroConta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(543, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 639;
            this.label1.Text = "Número Conta";
            // 
            // cboBancos
            // 
            this.cboBancos.EnterMoveNextControl = true;
            this.cboBancos.Location = new System.Drawing.Point(3, 20);
            this.cboBancos.Name = "cboBancos";
            this.cboBancos.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBancos.Properties.Appearance.Options.UseFont = true;
            this.cboBancos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboBancos.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboBancos.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Codigo", 6, "Codigo"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboBancos.Properties.NullText = "";
            this.cboBancos.Size = new System.Drawing.Size(265, 22);
            this.cboBancos.TabIndex = 1;
            this.cboBancos.EditValueChanged += new System.EventHandler(this.cboBancos_EditValueChanged);
            // 
            // labBanco
            // 
            this.labBanco.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBanco.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labBanco.Location = new System.Drawing.Point(3, 3);
            this.labBanco.Name = "labBanco";
            this.labBanco.Size = new System.Drawing.Size(31, 13);
            this.labBanco.TabIndex = 645;
            this.labBanco.Text = "Banco";
            // 
            // cboAgencias
            // 
            this.cboAgencias.Enabled = false;
            this.cboAgencias.EnterMoveNextControl = true;
            this.cboAgencias.Location = new System.Drawing.Point(274, 20);
            this.cboAgencias.Name = "cboAgencias";
            this.cboAgencias.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAgencias.Properties.Appearance.Options.UseFont = true;
            this.cboAgencias.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboAgencias.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.cboAgencias.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Descrição")});
            this.cboAgencias.Properties.NullText = "";
            this.cboAgencias.Size = new System.Drawing.Size(266, 22);
            this.cboAgencias.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(274, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 647;
            this.labelControl1.Text = "Agência";
            // 
            // txtNomePessoa
            // 
            this.txtNomePessoa.EnterMoveNextControl = true;
            this.txtNomePessoa.Location = new System.Drawing.Point(133, 70);
            this.txtNomePessoa.Name = "txtNomePessoa";
            this.txtNomePessoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomePessoa.Properties.Appearance.Options.UseFont = true;
            this.txtNomePessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNomePessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNomePessoa.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomePessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNomePessoa.Properties.Mask.EditMask = "99/";
            this.txtNomePessoa.Properties.ReadOnly = true;
            this.txtNomePessoa.Size = new System.Drawing.Size(733, 22);
            this.txtNomePessoa.TabIndex = 6;
            this.txtNomePessoa.TabStop = false;
            // 
            // btnPesquisaPessoa
            // 
            this.btnPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisaPessoa.Location = new System.Drawing.Point(105, 69);
            this.btnPesquisaPessoa.Name = "btnPesquisaPessoa";
            this.btnPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.btnPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPesquisaPessoa.TabIndex = 739;
            this.btnPesquisaPessoa.TabStop = false;
            this.btnPesquisaPessoa.Click += new System.EventHandler(this.btnPesquisaPessoa_Click);
            // 
            // labelControl67
            // 
            this.labelControl67.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl67.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl67.Location = new System.Drawing.Point(3, 54);
            this.labelControl67.Name = "labelControl67";
            this.labelControl67.Size = new System.Drawing.Size(65, 13);
            this.labelControl67.TabIndex = 737;
            this.labelControl67.Text = "Código Titular";
            // 
            // txtIdPessoa
            // 
            this.txtIdPessoa.EnterMoveNextControl = true;
            this.txtIdPessoa.Location = new System.Drawing.Point(3, 70);
            this.txtIdPessoa.Name = "txtIdPessoa";
            this.txtIdPessoa.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdPessoa.Properties.Appearance.Options.UseFont = true;
            this.txtIdPessoa.Properties.Appearance.Options.UseTextOptions = true;
            this.txtIdPessoa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtIdPessoa.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtIdPessoa.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtIdPessoa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIdPessoa.Properties.MaxLength = 10;
            this.txtIdPessoa.Size = new System.Drawing.Size(96, 22);
            this.txtIdPessoa.TabIndex = 5;
            this.txtIdPessoa.Leave += new System.EventHandler(this.txtIdPessoa_Leave);
            // 
            // labelControl66
            // 
            this.labelControl66.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl66.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl66.Location = new System.Drawing.Point(133, 54);
            this.labelControl66.Name = "labelControl66";
            this.labelControl66.Size = new System.Drawing.Size(147, 13);
            this.labelControl66.TabIndex = 738;
            this.labelControl66.Text = "Nome Parceiro Titular da conta";
            // 
            // FormContaBancariaPesquisa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 428);
            this.MaximizeBox = false;
            this.Name = "FormContaBancariaPesquisa";
            this.NomeDaTela = "Pesquisa de Contas Bancárias";
            this.Text = "Pesquisa de Contas Bancárias";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcContasBancarias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaContaBancaria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroConta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboBancos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAgencias.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNomePessoa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdPessoa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gcContasBancarias;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaBanco;
        private DevExpress.XtraGrid.Columns.GridColumn colunaAgencia;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labStatus;
        private System.Windows.Forms.PictureBox btnPesquisaContaBancaria;
        private DevExpress.XtraEditors.TextEdit txtNumeroConta;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumero;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private DevExpress.XtraEditors.LookUpEdit cboBancos;
        private DevExpress.XtraEditors.LabelControl labBanco;
        private DevExpress.XtraGrid.Columns.GridColumn colunaPessoa;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTipoConta;
        private DevExpress.XtraEditors.LookUpEdit cboAgencias;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNomePessoa;
        private System.Windows.Forms.PictureBox btnPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl67;
        private DevExpress.XtraEditors.TextEdit txtIdPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl66;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}