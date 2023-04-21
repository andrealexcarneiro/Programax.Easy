namespace Programax.Easy.View.Telas.Financeiro.PlanosDeContas
{
    partial class FormCadastroPlanoConta 
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
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pbPesquisaPessoa = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboTipoPlanoDeContas = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroPlanoContas = new DevExpress.XtraEditors.TextEdit();
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboNaturezaPlanoDeContas = new DevExpress.XtraEditors.LookUpEdit();
            this.txtDataCadastro = new DevExpress.XtraEditors.TextEdit();
            this.txtNumeroPlanoContasContador = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoPlanoDeContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlanoDeContas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNaturezaPlanoDeContas.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContasContador.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            this.painelBotoes.Location = new System.Drawing.Point(7, 0);
            this.painelBotoes.Size = new System.Drawing.Size(844, 60);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtNumeroPlanoContasContador);
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
            this.btnGravar.TabIndex = 997;
            this.btnGravar.Text = " Salvar";
            this.btnGravar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGravar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Image = global::Programax.Easy.View.Properties.Resources.iconLimpar;
            this.btnLimpar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpar.Location = new System.Drawing.Point(100, 0);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(0);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 40);
            this.btnLimpar.TabIndex = 1000;
            this.btnLimpar.Text = " Limpar";
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(200, 0);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 999;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // pbPesquisaPessoa
            // 
            this.pbPesquisaPessoa.BackColor = System.Drawing.Color.Transparent;
            this.pbPesquisaPessoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPesquisaPessoa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.pbPesquisaPessoa.Location = new System.Drawing.Point(239, 18);
            this.pbPesquisaPessoa.Name = "pbPesquisaPessoa";
            this.pbPesquisaPessoa.Size = new System.Drawing.Size(22, 22);
            this.pbPesquisaPessoa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPesquisaPessoa.TabIndex = 488;
            this.pbPesquisaPessoa.TabStop = false;
            this.pbPesquisaPessoa.Click += new System.EventHandler(this.pbPesquisaPessoa_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(267, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 490;
            this.labelControl1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.EnterMoveNextControl = true;
            this.txtDescricao.Location = new System.Drawing.Point(267, 18);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.Properties.Appearance.Options.UseFont = true;
            this.txtDescricao.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDescricao.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtDescricao.Properties.Mask.EditMask = "99/";
            this.txtDescricao.Properties.MaxLength = 200;
            this.txtDescricao.Size = new System.Drawing.Size(574, 22);
            this.txtDescricao.TabIndex = 3;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Appearance.Options.UseFont = true;
            this.labDataCadastro.Location = new System.Drawing.Point(727, 51);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(59, 13);
            this.labDataCadastro.TabIndex = 494;
            this.labDataCadastro.Text = "Dt. Cadastro";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(3, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(118, 13);
            this.labelControl2.TabIndex = 496;
            this.labelControl2.Text = "Número Plano de Contas";
            // 
            // cboStatus
            // 
            this.cboStatus.EnterMoveNextControl = true;
            this.cboStatus.Location = new System.Drawing.Point(329, 67);
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
            this.cboStatus.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(329, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 498;
            this.labelControl3.Text = "Status";
            // 
            // cboTipoPlanoDeContas
            // 
            this.cboTipoPlanoDeContas.EnterMoveNextControl = true;
            this.cboTipoPlanoDeContas.Location = new System.Drawing.Point(166, 67);
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
            this.cboTipoPlanoDeContas.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(166, 51);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(21, 13);
            this.labelControl5.TabIndex = 502;
            this.labelControl5.Text = "Tipo";
            // 
            // txtNumeroPlanoContas
            // 
            this.txtNumeroPlanoContas.EnterMoveNextControl = true;
            this.txtNumeroPlanoContas.Location = new System.Drawing.Point(3, 18);
            this.txtNumeroPlanoContas.Name = "txtNumeroPlanoContas";
            this.txtNumeroPlanoContas.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPlanoContas.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlanoContas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPlanoContas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPlanoContas.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroPlanoContas.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroPlanoContas.Properties.Mask.EditMask = "([0-9]{1})(([\\.][0-9]{2}){0,6})";
            this.txtNumeroPlanoContas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtNumeroPlanoContas.Properties.MaxLength = 50;
            this.txtNumeroPlanoContas.Size = new System.Drawing.Size(230, 22);
            this.txtNumeroPlanoContas.TabIndex = 2;
            this.txtNumeroPlanoContas.Leave += new System.EventHandler(this.txtNumeroPlanoContas_Leave);
            // 
            // gcPlanoDeContas
            // 
            this.gcPlanoDeContas.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPlanoDeContas.Location = new System.Drawing.Point(3, 95);
            this.gcPlanoDeContas.MainView = this.gridView5;
            this.gcPlanoDeContas.Name = "gcPlanoDeContas";
            this.gcPlanoDeContas.Size = new System.Drawing.Size(838, 247);
            this.gcPlanoDeContas.TabIndex = 504;
            this.gcPlanoDeContas.TabStop = false;
            this.gcPlanoDeContas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcPlanoDeContas.Click += new System.EventHandler(this.gcPlanoDeContas_Click);
            this.gcPlanoDeContas.DoubleClick += new System.EventHandler(this.gcPlanoDeContas_DoubleClick);
            this.gcPlanoDeContas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPlanoDeContas_KeyDown);
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
            this.colunaNumeroPlanoContas.Width = 141;
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
            this.colunaDescricao.Width = 181;
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
            this.colunaStatus.Width = 98;
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
            this.coluanNatureza.Width = 126;
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
            this.colunaTipo.Width = 117;
            // 
            // colunaNrPlanoContasContador
            // 
            this.colunaNrPlanoContasContador.Caption = "Nr. Plano Contas Contador";
            this.colunaNrPlanoContasContador.FieldName = "NumeroPlanoDeContasContador";
            this.colunaNrPlanoContasContador.Name = "colunaNrPlanoContasContador";
            this.colunaNrPlanoContasContador.OptionsColumn.AllowEdit = false;
            this.colunaNrPlanoContasContador.OptionsColumn.AllowFocus = false;
            this.colunaNrPlanoContasContador.OptionsFilter.AllowFilter = false;
            this.colunaNrPlanoContasContador.Visible = true;
            this.colunaNrPlanoContasContador.VisibleIndex = 5;
            this.colunaNrPlanoContasContador.Width = 157;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcPlanoDeContas;
            this.gridView2.Name = "gridView2";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGravar);
            this.flowLayoutPanel1.Controls.Add(this.btnLimpar);
            this.flowLayoutPanel1.Controls.Add(this.btnSair);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(662, 54);
            this.flowLayoutPanel1.TabIndex = 1001;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(3, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(43, 13);
            this.labelControl4.TabIndex = 500;
            this.labelControl4.Text = "Natureza";
            // 
            // cboNaturezaPlanoDeContas
            // 
            this.cboNaturezaPlanoDeContas.EnterMoveNextControl = true;
            this.cboNaturezaPlanoDeContas.Location = new System.Drawing.Point(3, 67);
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
            this.cboNaturezaPlanoDeContas.TabIndex = 4;
            // 
            // txtDataCadastro
            // 
            this.txtDataCadastro.EnterMoveNextControl = true;
            this.txtDataCadastro.Location = new System.Drawing.Point(727, 67);
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
            this.txtDataCadastro.TabIndex = 8;
            this.txtDataCadastro.TabStop = false;
            // 
            // txtNumeroPlanoContasContador
            // 
            this.txtNumeroPlanoContasContador.EnterMoveNextControl = true;
            this.txtNumeroPlanoContasContador.Location = new System.Drawing.Point(491, 67);
            this.txtNumeroPlanoContasContador.Name = "txtNumeroPlanoContasContador";
            this.txtNumeroPlanoContasContador.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPlanoContasContador.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroPlanoContasContador.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroPlanoContasContador.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroPlanoContasContador.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroPlanoContasContador.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtNumeroPlanoContasContador.Properties.Mask.EditMask = "99/";
            this.txtNumeroPlanoContasContador.Properties.MaxLength = 50;
            this.txtNumeroPlanoContasContador.Size = new System.Drawing.Size(230, 22);
            this.txtNumeroPlanoContasContador.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(491, 51);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(126, 13);
            this.labelControl6.TabIndex = 507;
            this.labelControl6.Text = "Nr. Plano Contas Contador";
            // 
            // FormCadastroPlanoConta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 458);
            this.Name = "FormCadastroPlanoConta";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPesquisaPessoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTipoPlanoDeContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlanoDeContas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboNaturezaPlanoDeContas.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCadastro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroPlanoContasContador.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pbPesquisaPessoa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit cboTipoPlanoDeContas;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlanoContas;
        private DevExpress.XtraGrid.GridControl gcPlanoDeContas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroPlanoContas;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDescricao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Columns.GridColumn coluanNatureza;
        private DevExpress.XtraGrid.Columns.GridColumn colunaTipo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cboNaturezaPlanoDeContas;
        private DevExpress.XtraEditors.TextEdit txtDataCadastro;
        private DevExpress.XtraEditors.TextEdit txtNumeroPlanoContasContador;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNrPlanoContasContador;
    }
}