namespace Programax.Easy.View.Telas.Relatorios
{
    partial class FormRelatorioEntrada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRelatorioEntrada));
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialEmissao = new DevExpress.XtraEditors.DateEdit();
            this.txtRazaoSocialFornecedor = new DevExpress.XtraEditors.TextEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalEntrada = new DevExpress.XtraEditors.DateEdit();
            this.btnPesquisa = new System.Windows.Forms.PictureBox();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalEmissao = new DevExpress.XtraEditors.DateEdit();
            this.txtNumeroNfe = new DevExpress.XtraEditors.TextEdit();
            this.gcNotasEntrada = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataEmissao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataEntrada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroEntrada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaRazaoSocialFornecedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtDataInicialEntrada = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnSelecionar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazaoSocialFornecedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroNfe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNotasEntrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImprimir
            // 
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Programax.Easy.View.Properties.Resources.icone_Imprimir;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnImprimir.Location = new System.Drawing.Point(12, 397);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(110, 40);
            this.btnImprimir.TabIndex = 10120;
            this.btnImprimir.Text = " Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::Programax.Easy.View.Properties.Resources.iconSair1;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSair.Location = new System.Drawing.Point(690, 397);
            this.btnSair.Margin = new System.Windows.Forms.Padding(0);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(100, 40);
            this.btnSair.TabIndex = 10124;
            this.btnSair.TabStop = false;
            this.btnSair.Text = " Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-96, 384);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(927, 10);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10123;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-79, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(908, 10);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10122;
            this.pictureBox1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(297, 29);
            this.labelControl1.TabIndex = 10121;
            this.labelControl1.Text = "RELATÓRIO DE ENTRADA";
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(579, 139);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Properties.Appearance.Options.UseFont = true;
            this.cboStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboStatus.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descricao", "Status")});
            this.cboStatus.Properties.DropDownRows = 5;
            this.cboStatus.Properties.NullText = "";
            this.cboStatus.Properties.PopupFormMinSize = new System.Drawing.Size(20, 20);
            this.cboStatus.Size = new System.Drawing.Size(178, 22);
            this.cboStatus.TabIndex = 10131;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Location = new System.Drawing.Point(579, 124);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(30, 13);
            this.labelControl5.TabIndex = 10140;
            this.labelControl5.Text = "Status";
            // 
            // txtDataInicialEmissao
            // 
            this.txtDataInicialEmissao.EditValue = "";
            this.txtDataInicialEmissao.EnterMoveNextControl = true;
            this.txtDataInicialEmissao.Location = new System.Drawing.Point(12, 87);
            this.txtDataInicialEmissao.Name = "txtDataInicialEmissao";
            this.txtDataInicialEmissao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialEmissao.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialEmissao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialEmissao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialEmissao.Size = new System.Drawing.Size(136, 22);
            this.txtDataInicialEmissao.TabIndex = 10125;
            // 
            // txtRazaoSocialFornecedor
            // 
            this.txtRazaoSocialFornecedor.EnterMoveNextControl = true;
            this.txtRazaoSocialFornecedor.Location = new System.Drawing.Point(12, 139);
            this.txtRazaoSocialFornecedor.Name = "txtRazaoSocialFornecedor";
            this.txtRazaoSocialFornecedor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazaoSocialFornecedor.Properties.Appearance.Options.UseFont = true;
            this.txtRazaoSocialFornecedor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRazaoSocialFornecedor.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRazaoSocialFornecedor.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazaoSocialFornecedor.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRazaoSocialFornecedor.Properties.Mask.EditMask = "99/";
            this.txtRazaoSocialFornecedor.Properties.MaxLength = 80;
            this.txtRazaoSocialFornecedor.Size = new System.Drawing.Size(561, 22);
            this.txtRazaoSocialFornecedor.TabIndex = 10130;
            this.txtRazaoSocialFornecedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRazaoSocialFornecedor_KeyDown);
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(12, 72);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(83, 13);
            this.labDataCadastro.TabIndex = 10133;
            this.labDataCadastro.Text = "Dt Inicial Emissão";
            // 
            // txtDataFinalEntrada
            // 
            this.txtDataFinalEntrada.EditValue = "";
            this.txtDataFinalEntrada.EnterMoveNextControl = true;
            this.txtDataFinalEntrada.Location = new System.Drawing.Point(437, 87);
            this.txtDataFinalEntrada.Name = "txtDataFinalEntrada";
            this.txtDataFinalEntrada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalEntrada.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalEntrada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalEntrada.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalEntrada.Size = new System.Drawing.Size(136, 22);
            this.txtDataFinalEntrada.TabIndex = 10128;
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisa.Location = new System.Drawing.Point(763, 138);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnPesquisa.TabIndex = 10139;
            this.btnPesquisa.TabStop = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(579, 72);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(75, 13);
            this.labCodigo.TabIndex = 10137;
            this.labCodigo.Text = "NFe de Entrada";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(153, 72);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 13);
            this.labelControl2.TabIndex = 10134;
            this.labelControl2.Text = "Dt Final Emissão";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(437, 72);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 10136;
            this.labelControl3.Text = "Dt Final Entrada";
            // 
            // txtDataFinalEmissao
            // 
            this.txtDataFinalEmissao.EditValue = "";
            this.txtDataFinalEmissao.EnterMoveNextControl = true;
            this.txtDataFinalEmissao.Location = new System.Drawing.Point(153, 87);
            this.txtDataFinalEmissao.Name = "txtDataFinalEmissao";
            this.txtDataFinalEmissao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalEmissao.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalEmissao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalEmissao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalEmissao.Size = new System.Drawing.Size(136, 22);
            this.txtDataFinalEmissao.TabIndex = 10126;
            // 
            // txtNumeroNfe
            // 
            this.txtNumeroNfe.EnterMoveNextControl = true;
            this.txtNumeroNfe.Location = new System.Drawing.Point(579, 87);
            this.txtNumeroNfe.Name = "txtNumeroNfe";
            this.txtNumeroNfe.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroNfe.Properties.Appearance.Options.UseFont = true;
            this.txtNumeroNfe.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNumeroNfe.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNumeroNfe.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNumeroNfe.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtNumeroNfe.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumeroNfe.Properties.Mask.EditMask = "99/";
            this.txtNumeroNfe.Properties.MaxLength = 8;
            this.txtNumeroNfe.Size = new System.Drawing.Size(211, 22);
            this.txtNumeroNfe.TabIndex = 10129;
            // 
            // gcNotasEntrada
            // 
            this.gcNotasEntrada.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcNotasEntrada.Location = new System.Drawing.Point(12, 167);
            this.gcNotasEntrada.MainView = this.gridView5;
            this.gcNotasEntrada.Name = "gcNotasEntrada";
            this.gcNotasEntrada.Size = new System.Drawing.Size(778, 211);
            this.gcNotasEntrada.TabIndex = 10132;
            this.gcNotasEntrada.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});
            this.gcNotasEntrada.DoubleClick += new System.EventHandler(this.gcNotasEntrada_DoubleClick);
            this.gcNotasEntrada.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNotasEntrada_KeyDown);
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
            this.colunaDataEmissao,
            this.colunaDataEntrada,
            this.colunaNumeroEntrada,
            this.colunaRazaoSocialFornecedor,
            this.colunaStatus});
            this.gridView5.CustomizationFormBounds = new System.Drawing.Rectangle(703, 467, 216, 178);
            this.gridView5.GridControl = this.gcNotasEntrada;
            this.gridView5.GroupPanelText = "[ Click - Seleciona ] Item da Venda";
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            this.gridView5.OptionsView.ShowIndicator = false;
            this.gridView5.OptionsView.ShowViewCaption = true;
            this.gridView5.PaintStyleName = "Skin";
            this.gridView5.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colunaId, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView5.ViewCaption = "Entradas";
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
            this.colunaId.Visible = true;
            this.colunaId.VisibleIndex = 0;
            this.colunaId.Width = 47;
            // 
            // colunaDataEmissao
            // 
            this.colunaDataEmissao.Caption = "Data Emissão";
            this.colunaDataEmissao.FieldName = "DataEmissao";
            this.colunaDataEmissao.Name = "colunaDataEmissao";
            this.colunaDataEmissao.OptionsColumn.AllowEdit = false;
            this.colunaDataEmissao.OptionsColumn.AllowFocus = false;
            this.colunaDataEmissao.OptionsFilter.AllowFilter = false;
            this.colunaDataEmissao.Visible = true;
            this.colunaDataEmissao.VisibleIndex = 1;
            this.colunaDataEmissao.Width = 86;
            // 
            // colunaDataEntrada
            // 
            this.colunaDataEntrada.AppearanceCell.Options.UseTextOptions = true;
            this.colunaDataEntrada.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colunaDataEntrada.Caption = "Data Entrada";
            this.colunaDataEntrada.DisplayFormat.FormatString = "d";
            this.colunaDataEntrada.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colunaDataEntrada.FieldName = "DataEntrada";
            this.colunaDataEntrada.MinWidth = 10;
            this.colunaDataEntrada.Name = "colunaDataEntrada";
            this.colunaDataEntrada.OptionsColumn.AllowEdit = false;
            this.colunaDataEntrada.OptionsColumn.AllowFocus = false;
            this.colunaDataEntrada.OptionsFilter.AllowFilter = false;
            this.colunaDataEntrada.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.colunaDataEntrada.Visible = true;
            this.colunaDataEntrada.VisibleIndex = 2;
            this.colunaDataEntrada.Width = 86;
            // 
            // colunaNumeroEntrada
            // 
            this.colunaNumeroEntrada.Caption = "Número Entrada";
            this.colunaNumeroEntrada.FieldName = "NumeroNota";
            this.colunaNumeroEntrada.Name = "colunaNumeroEntrada";
            this.colunaNumeroEntrada.OptionsColumn.AllowEdit = false;
            this.colunaNumeroEntrada.OptionsColumn.AllowFocus = false;
            this.colunaNumeroEntrada.OptionsFilter.AllowFilter = false;
            this.colunaNumeroEntrada.Visible = true;
            this.colunaNumeroEntrada.VisibleIndex = 3;
            this.colunaNumeroEntrada.Width = 117;
            // 
            // colunaRazaoSocialFornecedor
            // 
            this.colunaRazaoSocialFornecedor.Caption = "Razão Social Fornecedor";
            this.colunaRazaoSocialFornecedor.FieldName = "RazaoSocialFornecedor";
            this.colunaRazaoSocialFornecedor.Name = "colunaRazaoSocialFornecedor";
            this.colunaRazaoSocialFornecedor.OptionsColumn.AllowEdit = false;
            this.colunaRazaoSocialFornecedor.OptionsColumn.AllowFocus = false;
            this.colunaRazaoSocialFornecedor.OptionsFilter.AllowFilter = false;
            this.colunaRazaoSocialFornecedor.Visible = true;
            this.colunaRazaoSocialFornecedor.VisibleIndex = 4;
            this.colunaRazaoSocialFornecedor.Width = 178;
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
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcNotasEntrada;
            this.gridView2.Name = "gridView2";
            // 
            // txtDataInicialEntrada
            // 
            this.txtDataInicialEntrada.EditValue = "";
            this.txtDataInicialEntrada.EnterMoveNextControl = true;
            this.txtDataInicialEntrada.Location = new System.Drawing.Point(295, 87);
            this.txtDataInicialEntrada.Name = "txtDataInicialEntrada";
            this.txtDataInicialEntrada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialEntrada.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialEntrada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialEntrada.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialEntrada.Size = new System.Drawing.Size(136, 22);
            this.txtDataInicialEntrada.TabIndex = 10127;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(295, 72);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(81, 13);
            this.labelControl4.TabIndex = 10135;
            this.labelControl4.Text = "Dt Inicial Entrada";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Location = new System.Drawing.Point(12, 124);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(120, 13);
            this.labelControl6.TabIndex = 10138;
            this.labelControl6.Text = "Razão Social Fornecedor";
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.Image = global::Programax.Easy.View.Properties.Resources.icone_selecionar1;
            this.btnSelecionar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnSelecionar.Location = new System.Drawing.Point(33, 397);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(115, 40);
            this.btnSelecionar.TabIndex = 10141;
            this.btnSelecionar.Text = " Selecionar";
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Visible = false;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // FormRelatorioEntrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 446);
            this.Controls.Add(this.btnSelecionar);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtDataInicialEmissao);
            this.Controls.Add(this.txtRazaoSocialFornecedor);
            this.Controls.Add(this.labDataCadastro);
            this.Controls.Add(this.txtDataFinalEntrada);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.labCodigo);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtDataFinalEmissao);
            this.Controls.Add(this.txtNumeroNfe);
            this.Controls.Add(this.gcNotasEntrada);
            this.Controls.Add(this.txtDataInicialEntrada);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioEntrada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRelatorioEntrada";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazaoSocialFornecedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroNfe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNotasEntrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit txtDataInicialEmissao;
        private DevExpress.XtraEditors.TextEdit txtRazaoSocialFornecedor;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.DateEdit txtDataFinalEntrada;
        private System.Windows.Forms.PictureBox btnPesquisa;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit txtDataFinalEmissao;
        private DevExpress.XtraEditors.TextEdit txtNumeroNfe;
        private DevExpress.XtraGrid.GridControl gcNotasEntrada;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataEmissao;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataEntrada;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroEntrada;
        private DevExpress.XtraGrid.Columns.GridColumn colunaRazaoSocialFornecedor;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.DateEdit txtDataInicialEntrada;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.Button btnSelecionar;
    }
}