namespace Programax.Easy.View.Telas.Cadastros.EntradaMercadorias
{
    partial class FormPesquisaDeEntradaDeMercadorias
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
            this.txtDataInicialEmissao = new DevExpress.XtraEditors.DateEdit();
            this.labDataCadastro = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalEmissao = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinalEntrada = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataInicialEntrada = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroNfe = new DevExpress.XtraEditors.TextEdit();
            this.labCodigo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.gcNotasEntrada = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colunaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataEmissao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaDataEntrada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaNumeroEntrada = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaRazaoSocialFornecedor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colunaStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnPesquisa = new System.Windows.Forms.PictureBox();
            this.txtRazaoSocialFornecedor = new DevExpress.XtraEditors.TextEdit();
            this.cboStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.painelBotoes.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroNfe.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNotasEntrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazaoSocialFornecedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // painelBotoes
            // 
            this.painelBotoes.Controls.Add(this.flowLayoutPanel1);
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.cboStatus);
            this.panelConteudo.Controls.Add(this.labelControl5);
            this.panelConteudo.Controls.Add(this.txtDataInicialEmissao);
            this.panelConteudo.Controls.Add(this.txtRazaoSocialFornecedor);
            this.panelConteudo.Controls.Add(this.labDataCadastro);
            this.panelConteudo.Controls.Add(this.txtDataFinalEntrada);
            this.panelConteudo.Controls.Add(this.btnPesquisa);
            this.panelConteudo.Controls.Add(this.labCodigo);
            this.panelConteudo.Controls.Add(this.labelControl1);
            this.panelConteudo.Controls.Add(this.labelControl2);
            this.panelConteudo.Controls.Add(this.txtDataFinalEmissao);
            this.panelConteudo.Controls.Add(this.txtNumeroNfe);
            this.panelConteudo.Controls.Add(this.gcNotasEntrada);
            this.panelConteudo.Controls.Add(this.txtDataInicialEntrada);
            this.panelConteudo.Controls.Add(this.labelControl3);
            this.panelConteudo.Controls.Add(this.labelControl4);
            this.panelConteudo.Size = new System.Drawing.Size(567, 318);
            // 
            // txtDataInicialEmissao
            // 
            this.txtDataInicialEmissao.EditValue = "";
            this.txtDataInicialEmissao.EnterMoveNextControl = true;
            this.txtDataInicialEmissao.Location = new System.Drawing.Point(3, 21);
            this.txtDataInicialEmissao.Name = "txtDataInicialEmissao";
            this.txtDataInicialEmissao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialEmissao.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialEmissao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialEmissao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialEmissao.Size = new System.Drawing.Size(136, 22);
            this.txtDataInicialEmissao.TabIndex = 1;
            // 
            // labDataCadastro
            // 
            this.labDataCadastro.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDataCadastro.Location = new System.Drawing.Point(3, 6);
            this.labDataCadastro.Name = "labDataCadastro";
            this.labDataCadastro.Size = new System.Drawing.Size(83, 13);
            this.labDataCadastro.TabIndex = 244;
            this.labDataCadastro.Text = "Dt Inicial Emissão";
            // 
            // txtDataFinalEmissao
            // 
            this.txtDataFinalEmissao.EditValue = "";
            this.txtDataFinalEmissao.EnterMoveNextControl = true;
            this.txtDataFinalEmissao.Location = new System.Drawing.Point(144, 21);
            this.txtDataFinalEmissao.Name = "txtDataFinalEmissao";
            this.txtDataFinalEmissao.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalEmissao.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalEmissao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalEmissao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalEmissao.Size = new System.Drawing.Size(136, 22);
            this.txtDataFinalEmissao.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(144, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 246;
            this.labelControl1.Text = "Dt Final Emissão";
            // 
            // txtDataFinalEntrada
            // 
            this.txtDataFinalEntrada.EditValue = "";
            this.txtDataFinalEntrada.EnterMoveNextControl = true;
            this.txtDataFinalEntrada.Location = new System.Drawing.Point(428, 21);
            this.txtDataFinalEntrada.Name = "txtDataFinalEntrada";
            this.txtDataFinalEntrada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataFinalEntrada.Properties.Appearance.Options.UseFont = true;
            this.txtDataFinalEntrada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinalEntrada.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataFinalEntrada.Size = new System.Drawing.Size(136, 22);
            this.txtDataFinalEntrada.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(428, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 250;
            this.labelControl2.Text = "Dt Final Entrada";
            // 
            // txtDataInicialEntrada
            // 
            this.txtDataInicialEntrada.EditValue = "";
            this.txtDataInicialEntrada.EnterMoveNextControl = true;
            this.txtDataInicialEntrada.Location = new System.Drawing.Point(286, 21);
            this.txtDataInicialEntrada.Name = "txtDataInicialEntrada";
            this.txtDataInicialEntrada.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataInicialEntrada.Properties.Appearance.Options.UseFont = true;
            this.txtDataInicialEntrada.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicialEntrada.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataInicialEntrada.Size = new System.Drawing.Size(136, 22);
            this.txtDataInicialEntrada.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(286, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(81, 13);
            this.labelControl3.TabIndex = 248;
            this.labelControl3.Text = "Dt Inicial Entrada";
            // 
            // txtNumeroNfe
            // 
            this.txtNumeroNfe.EnterMoveNextControl = true;
            this.txtNumeroNfe.Location = new System.Drawing.Point(3, 71);
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
            this.txtNumeroNfe.Size = new System.Drawing.Size(136, 22);
            this.txtNumeroNfe.TabIndex = 5;
            // 
            // labCodigo
            // 
            this.labCodigo.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labCodigo.Location = new System.Drawing.Point(3, 56);
            this.labCodigo.Name = "labCodigo";
            this.labCodigo.Size = new System.Drawing.Size(75, 13);
            this.labCodigo.TabIndex = 631;
            this.labCodigo.Text = "NFe de Entrada";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(144, 56);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(120, 13);
            this.labelControl4.TabIndex = 633;
            this.labelControl4.Text = "Razão Social Fornecedor";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSelecionar);
            this.flowLayoutPanel1.Controls.Add(this.btnFechar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(362, 43);
            this.flowLayoutPanel1.TabIndex = 1047;
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
            this.btnSelecionar.TabIndex = 8;
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
            this.btnFechar.TabIndex = 9;
            this.btnFechar.Text = " Sair";
            this.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // gcNotasEntrada
            // 
            this.gcNotasEntrada.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcNotasEntrada.Location = new System.Drawing.Point(3, 101);
            this.gcNotasEntrada.MainView = this.gridView5;
            this.gcNotasEntrada.Name = "gcNotasEntrada";
            this.gcNotasEntrada.Size = new System.Drawing.Size(561, 211);
            this.gcNotasEntrada.TabIndex = 8;
            this.gcNotasEntrada.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5,
            this.gridView2});            
            this.gcNotasEntrada.DoubleClick += new System.EventHandler(this.gcNotasEntrada_DoubleClick);
            this.gcNotasEntrada.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcNotasEntrada_KeyDown);
            // 
            // gridView5
            // 
            this.gridView5.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView5.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView5.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView5.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView5.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView5.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(119)))), ((int)(((byte)(146)))));
            this.gridView5.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gridView5.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView5.Appearance.HideSelectionRow.Options.UseForeColor = true;
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
            // btnPesquisa
            // 
            this.btnPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisa.Image = global::Programax.Easy.View.Properties.Resources.pesquisar;
            this.btnPesquisa.Location = new System.Drawing.Point(537, 70);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(27, 23);
            this.btnPesquisa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnPesquisa.TabIndex = 637;
            this.btnPesquisa.TabStop = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // txtRazaoSocialFornecedor
            // 
            this.txtRazaoSocialFornecedor.EnterMoveNextControl = true;
            this.txtRazaoSocialFornecedor.Location = new System.Drawing.Point(144, 71);
            this.txtRazaoSocialFornecedor.Name = "txtRazaoSocialFornecedor";
            this.txtRazaoSocialFornecedor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazaoSocialFornecedor.Properties.Appearance.Options.UseFont = true;
            this.txtRazaoSocialFornecedor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRazaoSocialFornecedor.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRazaoSocialFornecedor.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazaoSocialFornecedor.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txtRazaoSocialFornecedor.Properties.Mask.EditMask = "99/";
            this.txtRazaoSocialFornecedor.Properties.MaxLength = 80;
            this.txtRazaoSocialFornecedor.Size = new System.Drawing.Size(278, 22);
            this.txtRazaoSocialFornecedor.TabIndex = 6;
            this.txtRazaoSocialFornecedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRazaoSocialFornecedor_KeyDown);
            // 
            // cboStatus
            // 
            this.cboStatus.Location = new System.Drawing.Point(428, 71);
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
            this.cboStatus.Size = new System.Drawing.Size(103, 22);
            this.cboStatus.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Location = new System.Drawing.Point(428, 56);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(30, 13);
            this.labelControl5.TabIndex = 682;
            this.labelControl5.Text = "Status";
            // 
            // FormPesquisaDeEntradaDeMercadorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 429);
            this.MaximizeBox = false;
            this.Name = "FormPesquisaDeEntradaDeMercadorias";
            this.NomeDaTela = "Pesquisa de Entradas";
            this.Text = "Pesquisa de Entradas";
            this.painelBotoes.ResumeLayout(false);
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEmissao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEmissao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinalEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicialEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroNfe.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcNotasEntrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPesquisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazaoSocialFornecedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit txtDataInicialEmissao;
        private DevExpress.XtraEditors.LabelControl labDataCadastro;
        private DevExpress.XtraEditors.DateEdit txtDataFinalEmissao;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtDataFinalEntrada;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtDataInicialEntrada;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNumeroNfe;
        private DevExpress.XtraEditors.LabelControl labCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnSelecionar;
        private DevExpress.XtraGrid.GridControl gcNotasEntrada;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn colunaId;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataEntrada;
        private DevExpress.XtraGrid.Columns.GridColumn colunaDataEmissao;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colunaNumeroEntrada;
        private DevExpress.XtraGrid.Columns.GridColumn colunaRazaoSocialFornecedor;
        private System.Windows.Forms.PictureBox btnPesquisa;
        private DevExpress.XtraEditors.TextEdit txtRazaoSocialFornecedor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn colunaStatus;
        private DevExpress.XtraEditors.LookUpEdit cboStatus;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}